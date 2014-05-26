//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa
{
    public class AnnotationEngine
    {
        public HashSet<string> Labels
        {
            get 
            {
                return _labels;
            }
        }

        public List<ImageItem> ImageItems
        {
            get 
            {
                return _images.ImageItems;
            }
        }

        public Dictionary<string, IDetector> Detectors
        {
            get
            {
                return _detectors;
            }
        }

        public void AddDetector(string name, IDetector detector, bool enabled = true)
        {
            _detectors[name] = detector;
            detector.Enabled = enabled;
        }

        public void AnnotateAnImage(string path)
        {
            Object annotateLock = new Object();

            _labels.Clear();
            _images.Clear();

            List<string> labels = new List<string>();
            List<string> displayLabels = new List<string>();
            List<MCvObjectDetection[]> objects = new List<MCvObjectDetection[]>();
            using (Image<Bgr, Byte> img = new Image<Bgr, Byte>(path))
            {
                Parallel.ForEach(_detectors.Values, detector =>
                {
                    if (detector.Enabled)
                    {
                        detector.Detect(img);
                        if (!string.IsNullOrEmpty(detector.Label))
                        {
                            lock (annotateLock)
                            {
                                labels.Add(detector.Label);
                                displayLabels.Add(detector.DisplayLabel);
                                if (detector.Objects != null)
                                {
                                    objects.Add(detector.Objects);
                                }

                                _labels.Add(detector.DisplayLabel);
                            }
                        }
                    }
                });

                _images.Add(new ImageItem(Path.GetFileName(path), path, labels, displayLabels, objects));
            }
        }

        public void Annotate(DirectoryInfo folder)
        {
            var imagesOnDisk = FileHelper.GetImages(folder);

            _labels.Clear();
            _images.Clear();
            Object annotateLock = new Object();

            Parallel.ForEach(imagesOnDisk, image =>
            {
                List<string> labels = new List<string>();
                List<string> displayLabels = new List<string>();
                List<MCvObjectDetection[]> objects = new List<MCvObjectDetection[]>();
                using (Image<Bgr, Byte> img = new Image<Bgr, Byte>(image.FullName))
                {
                    foreach (var detector in _detectors.Values)
                    {
                        if (detector.Enabled)
                        {
                            detector.Detect(img);
                            if (!string.IsNullOrEmpty(detector.Label))
                            {
                                labels.Add(detector.Label);
                                displayLabels.Add(detector.DisplayLabel);
                                if (detector.Objects != null)
                                {
                                    objects.Add(detector.Objects);
                                }

                                _labels.Add(detector.DisplayLabel);
                            }
                        }
                    }

                    lock (annotateLock)
                    {
                        _images.Add(new ImageItem(image.Name, image.FullName, labels, displayLabels, objects));
                    }
                }
            });
        }

        public IEnumerable<ImageItem> Filter(HashSet<string> labels)
        {
            foreach (var image in _images.ImageItems)
            {
                List<string> remainingLabels = new List<string>();
                foreach(var label in image.DisplayLabels)
                {
                    if (labels.Contains(label))
                    {
                        remainingLabels.Add(label);
                    }
                }

                if (remainingLabels.Count > 0)
                {
                    yield return image;
                }
            }
        }

        public void SaveAnnotation(string filePath)
        {
            _images.Save(filePath);
        }

        public void LoadAnnotation(string filePath)
        {
            _images.Load(filePath);

            _labels.Clear();
            foreach (var image in _images.ImageItems)
            {
                foreach (var label in image.DisplayLabels)
                {
                    _labels.Add(label);
                }
            }
        }

        public void AppendAnnotation(string filePath)
        {
            ImageItemCollection images = new ImageItemCollection();

            images.Load(filePath);
            _images.Append(images);

            foreach (var image in images.ImageItems)
            {
                foreach (var label in image.DisplayLabels)
                {
                    _labels.Add(label);
                }
            }
        }

        private Dictionary<string, IDetector> _detectors = new Dictionary<string, IDetector>();
        private ImageItemCollection _images = new ImageItemCollection();
        private HashSet<string> _labels = new HashSet<string>();
    }
}
