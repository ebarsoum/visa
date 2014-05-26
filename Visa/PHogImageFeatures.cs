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
    public class PHogImageFeatures : IImageGlobalFeatures
    {
        public Matrix<float> Descriptors
        {
            get
            {
                return _descriptors;
            }
        }

        public void Compute(Image<Bgr, Byte> image)
        {
            int width = image.Width;
            int height = image.Height;

            int roiWidth = 64;
            int roiHeight = 128;
            int roiX = 16;
            int roiY = 16;

            if (width > 2 * roiWidth)
            {
                roiX = (int)(width / 2) - (int)(roiWidth / 2);
            }

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

            _descriptors = new Matrix<float>(_hogDetector.Compute(image, new Size(8, 8), new Size(0, 0), p));
        }

        private HOGDescriptor _hogDetector = new HOGDescriptor();
        private Matrix<float> _descriptors;
    }
}
