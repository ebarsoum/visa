//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Flann;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visa
{
    public class GlobalSimilarityEngine : ISimilarityEngine
    {
        /// <summary>
        /// 
        /// </summary>
        public int[] SimilarityVote
        {
            get 
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float[] Distances
        {
            get 
            {
                return _topDistances;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDescriptor"></param>
        /// <returns></returns>
        public void ComputeSimilarities(Matrix<float> queryDescriptor)
        {
            ComputeSimilarities(queryDescriptor, 0.6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDescriptor"></param>
        public void ComputeSimilarities(Matrix<float> queryDescriptor, double distanceThreshold)
        {
            var indices = new Matrix<int>(queryDescriptor.Rows, _topCount);
            var distances = new Matrix<float>(queryDescriptor.Rows, _topCount);

            _flannIndex.KnnSearch(queryDescriptor, indices, distances, _topCount, 24);

            for (int i = 0; i < _topDistances.Length; i++)
            {
                _topDistances[i] = float.MaxValue;
            }

            for (int i = 0; i < indices.Cols; i++)
            {
                _topDistances[indices[0, i]] = distances.Data[0, i];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptors"></param>
        public void Index(IList<Matrix<float>> descriptors)
        {
            int cols = descriptors[0].Cols;
            int rows = descriptors.Sum(a => a.Rows);

            float[,] flatDescriptors = new float[rows, cols];

            int offset = 0;
            uint start = 0;
            uint end = 0;

            foreach (var descriptor in descriptors)
            {
                Buffer.BlockCopy(descriptor.ManagedArray, 0, flatDescriptors, offset, sizeof(float) * descriptor.ManagedArray.Length);
                offset += sizeof(float) * descriptor.ManagedArray.Length;

                end = start + (uint)(descriptor.Rows - 1);

                start += (uint)descriptor.Rows;
            }

            _flatDescriptors = new Matrix<float>(flatDescriptors);
            _flannIndex = new Index(_flatDescriptors, 1);
            _topDistances = new float[descriptors.Count];
        }

        private const int _topCount = 25;
        private Matrix<float> _flatDescriptors;
        private Index _flannIndex;
        private float[] _topDistances;
    }
}
