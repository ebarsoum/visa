//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa
{
    [Flags]
    public enum ImageFeatureSelection
    {
        Surf = 1,
        PHistogram = 2
    }

    public class ImageDatabase
    {
        public void Index(DirectoryInfo folder)
        {
            var imagesOnDisk = FileHelper.GetImages(folder);

            _images.Clear();
            _imageGlobalColorDescriptors.Clear();
            _imageLocalDescriptors.Clear();
            
            foreach (var image in imagesOnDisk)
            {
                ColorHistogramPerRegionImageFeature colorFeature = new ColorHistogramPerRegionImageFeature();
                SurfImageFeatures features = new SurfImageFeatures();
                using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                {
                    using (Image<Gray, Byte> tempGrayImage = tempImage.Convert<Gray, byte>())
                    {
                        colorFeature.Compute(tempImage);
                        features.Compute(tempGrayImage);

                        var imageItem = new ImageItem(image.Name, image.FullName, features.Descriptors, colorFeature.Descriptors);

                        if ((colorFeature.Descriptors != null) && (features.Descriptors != null))
                        {
                            _images.Add(imageItem);
                            _imageGlobalColorDescriptors.Add(colorFeature.Descriptors);
                            _imageLocalDescriptors.Add(features.Descriptors);
                        }
                    }
                }
            }

            _localSimilarityEngine.Index(_imageLocalDescriptors);
            _globalSimilarityEngine.Index(_imageGlobalColorDescriptors);
        }

        public List<ImageItem> Query(string filePath, ImageFeatureSelection features)
        {
            return Query(new Image<Bgr, Byte>(filePath), features);
        }

        public List<ImageItem> Query(FileInfo queryFile, ImageFeatureSelection features)
        {
            return Query(queryFile.FullName, features);
        }

        public List<ImageItem> Query(Image<Bgr, Byte> queryImage, ImageFeatureSelection features)
        {
            List<ImageItem> sortedResults = new List<ImageItem>();
            List<Task> tasks = new List<Task>();
            Task<List<KeyValuePair<ImageItem, float>>> phistTask = null;
            Task<List<KeyValuePair<ImageItem, int>>> surfTask = null;

            if (features.HasFlag(ImageFeatureSelection.PHistogram))
            {
                phistTask = Task<List<KeyValuePair<ImageItem, float>>>.Factory.StartNew(() => QueryUsingPHistogram(queryImage));
                tasks.Add(phistTask);
            }

            if (features.HasFlag(ImageFeatureSelection.Surf))
            {
                surfTask = Task<List<KeyValuePair<ImageItem, int>>>.Factory.StartNew(() => QueryUsingSurf(queryImage));
                tasks.Add(surfTask);
            }

            Task[] computeSimilarityTasks = tasks.ToArray();
            Task.WaitAll(computeSimilarityTasks);

            HashSet<string> files = new HashSet<string>();
            if (phistTask != null)
            {
                List<KeyValuePair<ImageItem, float>> phistResults = phistTask.Result;
                foreach (var kvp in phistResults)
                {
                    sortedResults.Add(kvp.Key);
                    files.Add(kvp.Key.Path);
                }
            }

            if (surfTask != null)
            {
                List<KeyValuePair<ImageItem, int>> surfResults = surfTask.Result;
                foreach (var kvp in surfResults)
                {
                    if (!files.Contains(kvp.Key.Path))
                    {
                        sortedResults.Add(kvp.Key);
                    }
                }
            }

            return sortedResults;
        }

        private List<KeyValuePair<ImageItem, float>> QueryUsingPHistogram(Image<Bgr, Byte> queryImage)
        {
            List<KeyValuePair<ImageItem, float>> results = new List<KeyValuePair<ImageItem, float>>();
            ColorHistogramPerRegionImageFeature queryFeature = new ColorHistogramPerRegionImageFeature();

            queryFeature.Compute(queryImage);
            _globalSimilarityEngine.ComputeSimilarities(queryFeature.Descriptors);

            float[] distances = _globalSimilarityEngine.Distances;
            for (int index = 0; index < _imageGlobalColorDescriptors.Count; ++index)
            {
                if (distances[index] <= 10000.0f)
                {
                    results.Add(new KeyValuePair<ImageItem, float>(_images.ImageItems[index], distances[index]));
                }
            }

            results.Sort(delegate(KeyValuePair<ImageItem, float> first, KeyValuePair<ImageItem, float> second)
            {
                if (first.Value > second.Value)
                {
                    return 1;
                }
                if (first.Value < second.Value)
                {
                    return -1;
                }

                return 0;
            });

            return results;
        }

        private List<KeyValuePair<ImageItem, int>> QueryUsingSurf(Image<Bgr, Byte> queryImage)
        {
            List<KeyValuePair<ImageItem, int>> results = new List<KeyValuePair<ImageItem, int>>();
            SurfImageFeatures queryFeatures = new SurfImageFeatures();

            queryFeatures.Compute(queryImage.Convert<Gray, byte>());
            _localSimilarityEngine.ComputeSimilarities(queryFeatures.Descriptors);

            int[] similarityVote = _localSimilarityEngine.SimilarityVote;
            for (int index = 0; index < _images.Count; ++index)
            {
                if (similarityVote[index] > 50)
                {
                    results.Add(new KeyValuePair<ImageItem, int>(_images.ImageItems[index], similarityVote[index]));
                }
            }

            results.Sort(delegate(KeyValuePair<ImageItem, int> first, KeyValuePair<ImageItem, int> second)
            {
                if (first.Value > second.Value)
                {
                    return -1;
                }
                if (first.Value < second.Value)
                {
                    return 1;
                }

                return 0;
            });

            return results;
        }

        public void SaveIndex(string filePath)
        {
            _images.Save(filePath);
        }

        public void LoadIndex(string filePath)
        {
            _images.Load(filePath);

            _imageLocalDescriptors.Clear();
            _imageGlobalColorDescriptors.Clear();

            foreach(var image in _images.ImageItems)
            {
                _imageLocalDescriptors.Add(image.PointDescriptors);
                _imageGlobalColorDescriptors.Add(image.ColorDescriptors);
            }

            _localSimilarityEngine.Index(_imageLocalDescriptors);
            _globalSimilarityEngine.Index(_imageGlobalColorDescriptors);
        }

        public void AppendIndex(string filePath)
        {
            ImageItemCollection images = new ImageItemCollection();

            images.Load(filePath);
            _images.Append(images);

            foreach (var image in images.ImageItems)
            {
                _imageLocalDescriptors.Add(image.PointDescriptors);
                _imageGlobalColorDescriptors.Add(image.ColorDescriptors);
            }

            _localSimilarityEngine.Index(_imageLocalDescriptors);
            _globalSimilarityEngine.Index(_imageGlobalColorDescriptors);
        }

        public Image<Bgr, Byte> Test(FileInfo imageFile1, FileInfo imageFile2)
        {
            return Test(new Image<Bgr, Byte>(imageFile1.FullName),
                        new Image<Bgr, Byte>(imageFile2.FullName));
        }

        public Image<Bgr, Byte> Test(Image<Bgr, Byte> image1, Image<Bgr, Byte> image2)
        {
            ImageFeaturesMatcher imageMatcher = new ImageFeaturesMatcher();
            SurfImageFeatures features1 = new SurfImageFeatures();
            SurfImageFeatures features2 = new SurfImageFeatures();

            features1.Compute(image1.Convert<Gray, byte>());
            features2.Compute(image2.Convert<Gray, byte>());

            imageMatcher.Match(features1, features2);

            return imageMatcher.Result;
        }

        private ImageItemCollection _images = new ImageItemCollection();
        private List<Matrix<float>> _imageLocalDescriptors = new List<Matrix<float>>();
        private List<Matrix<float>> _imageGlobalColorDescriptors = new List<Matrix<float>>();
        private SimilarityEngine<PointSimilarityEngine> _localSimilarityEngine = new SimilarityEngine<PointSimilarityEngine>();
        private SimilarityEngine<GlobalSimilarityEngine> _globalSimilarityEngine = new SimilarityEngine<GlobalSimilarityEngine>();
    }
}
