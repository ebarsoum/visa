//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Visa
{
    public interface IImageGlobalFeatures
    {
        /// <summary>
        /// Each row is a descriptor vector for a specific KeyPoints
        /// </summary>
        Matrix<float> Descriptors
        {
            get;
        }
    }

    public interface IImageLocalFeatures
    {
        /// <summary>
        /// Detected KeyPoints
        /// </summary>
        VectorOfKeyPoint KeyPoints
        {
            get;
        }

        /// <summary>
        /// Each row is a descriptor vector for a specific KeyPoints
        /// </summary>
        Matrix<float> Descriptors
        {
            get;
        }
    }

    public interface IImageLocalFeaturesMatcher
    {
        /// <summary>
        /// Strict match.
        /// </summary>
        /// <param name="feature1"></param>
        /// <param name="feature2"></param>
        /// <returns></returns>
        bool Match(IImageLocalFeatures feature1, IImageLocalFeatures feature2);

        /// <summary>
        /// Higher the returned value, the closer both features together.
        /// </summary>
        /// <param name="feature1"></param>
        /// <param name="feature2"></param>
        /// <returns></returns>
        uint Similar(IImageLocalFeatures feature1, IImageLocalFeatures feature2);
    }
}
