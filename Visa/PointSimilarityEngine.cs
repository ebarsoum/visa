//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Flann;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Visa
{
    public class PointSimilarityEngine : ISimilarityEngine
    {
        /// <summary>
        /// 
        /// </summary>
        public int[] SimilarityVote
        {
            get
            {
                return _similarityVote;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float[] Distances
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDescriptor"></param>
        /// <returns></returns>
        public void ComputeSimilarities(Matrix<float> queryDescriptor)
        {
            ComputeSimilarities(queryDescriptor, 0.1f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDescriptor"></param>
        public void ComputeSimilarities(Matrix<float> queryDescriptor, float distanceThreshold)
        {
            var indices = new Matrix<int>(queryDescriptor.Rows, _topCount);
            var distances = new Matrix<float>(queryDescriptor.Rows, _topCount);

            _flannIndex.KnnSearch(queryDescriptor, indices, distances, _topCount, 24);

            _similarityVote = new int[_indicesRange.Count];
            Parallel.For(0, indices.Rows, i =>
            {
                for (int neigbhorIndex = 0; neigbhorIndex < _topCount; ++neigbhorIndex)
                {
                    if (distances[i, neigbhorIndex] < distanceThreshold)
                    {
                        for (int index = 0; index < _indicesRange.Count; ++index)
                        {
                            if ((_indicesRange[index].Key <= indices[i, neigbhorIndex]) && (_indicesRange[index].Value >= indices[i, neigbhorIndex]))
                            {
                                Interlocked.Increment(ref _similarityVote[index]);
                                break;
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptors"></param>
        public void Index(IList<Matrix<float>> descriptors)
        {
            _descriptors = descriptors;

            int cols = descriptors[0].Cols;
            int rows = descriptors.Sum(a => a.Rows);

            float[,] flatDescriptors = new float[rows, cols];

            int offset = 0;
            uint start = 0;
            uint end = 0;

            _indicesRange.Clear();
            _flannIndexPerImage.Clear();
            foreach (var descriptor in descriptors)
            {
                Buffer.BlockCopy(descriptor.ManagedArray, 0, flatDescriptors, offset, sizeof(float) * descriptor.ManagedArray.Length);
                offset += sizeof(float) * descriptor.ManagedArray.Length;

                end = start + (uint)(descriptor.Rows - 1);

                _indicesRange.Add(new KeyValuePair<uint, uint>(start, end));
                start += (uint)descriptor.Rows;

                _flannIndexPerImage.Add(new Index(descriptor, 4));
            }

            _flatDescriptors = new Matrix<float>(flatDescriptors);

            // 4 KD tree
            _flannIndex = new Index(_flatDescriptors, 4);
        }

        private const int _topCount = 10;
        private List<KeyValuePair<uint, uint>> _indicesRange = new List<KeyValuePair<uint, uint>>();
        private IList<Matrix<float>> _descriptors;
        private List<Index> _flannIndexPerImage = new List<Index>();
        private Matrix<float> _flatDescriptors;
        private Index _flannIndex;
        private int[] _similarityVote;
    }
}
