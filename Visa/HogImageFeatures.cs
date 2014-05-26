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
    public class HogImageFeatures : IImageGlobalFeatures
    {
        public Matrix<float> Descriptors
        {
            get
            {
                return _descriptors;
            }
        }

        public float[] DescriptorArray
        {
            get
            {
                return _descriptorArray;
            }
        }


        public void Compute(Image<Bgr, Byte> image, Rectangle roi)
        {
            int width = image.Width;
            int height = image.Height;

            int roiWidth = roi.Width;
            int roiHeight = roi.Height;
            int roiX = roi.X;
            int roiY = roi.Y;

            image.ROI = new Rectangle(roiX, roiY, roiWidth, roiHeight);

            Point[] p = new Point[roiWidth * roiHeight];
            int k = 0;
            for (int i = 0; i < roiWidth; i++)
            {
                for (int j = 0; j < roiHeight; j++)
                {
                    p[k++] = new Point(i, j);
                }
            }

            _descriptorArray = _hogDetector.Compute(image, new Size(8, 8), new Size(0, 0), p);
            _descriptors = new Matrix<float>(_descriptorArray);
        }

        private HOGDescriptor _hogDetector = new HOGDescriptor();
        private Matrix<float> _descriptors;
        private float[] _descriptorArray;
    }
}
