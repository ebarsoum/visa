//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visa
{
    public class SurfImageFeatures : IImageLocalFeatures
    {
        public Image<Gray, Byte> Image
        {
            get
            {
                return _image;
            }
        }

        public VectorOfKeyPoint KeyPoints
        {
            get
            {
                return _keyPoints;
            }
        }

        public Matrix<float> Descriptors
        {
            get
            {
                return _descriptors;
            }
        }

        public void Compute(Image<Gray, Byte> image)
        {
            _image = image;

            _keyPoints = _surfDetector.DetectKeyPointsRaw(_image, null);
            _descriptors = _surfDetector.ComputeDescriptorsRaw(_image, null, _keyPoints);
        }

        private Image<Gray, Byte> _image;
        private SURFDetector _surfDetector = new SURFDetector(500, false);
        private VectorOfKeyPoint _keyPoints;
        private Matrix<float> _descriptors;
    }
}
