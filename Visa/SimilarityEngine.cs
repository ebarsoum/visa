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
    public interface ISimilarityEngine
    {
        int[] SimilarityVote
        {
            get;
        }

        float[] Distances
        {
            get;
        }

        void Index(IList<Matrix<float>> descriptors);
        void ComputeSimilarities(Matrix<float> queryDescriptor);
    }

    public class SimilarityEngine<T> where T : ISimilarityEngine, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public int[] SimilarityVote
        {
            get
            {
                return _engine.SimilarityVote;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float[] Distances
        {
            get
            {
                return _engine.Distances;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryDescriptor"></param>
        /// <returns></returns>
        public void ComputeSimilarities(Matrix<float> queryDescriptor)
        {
            _engine.ComputeSimilarities(queryDescriptor);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptors"></param>
        public void Index(IList<Matrix<float>> descriptors)
        {
            _engine.Index(descriptors);
        }

        private T _engine = new T();
    }
}
