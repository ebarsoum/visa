//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Visa
{
    public class ColorHistogramPerRegionImageFeature : IImageGlobalFeatures
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
            using (Image<Hsv, Byte> hsvImage = image.Convert<Hsv, byte>())
            {
                Image<Gray, Byte>[] channels = hsvImage.Split();

                Rectangle roi1 = new Rectangle(0                   , 0               , image.Width / 2, image.Height / 2);
                Rectangle roi2 = new Rectangle(image.Width / 2     , 0               , image.Width / 2, image.Height / 2);
                Rectangle roi3 = new Rectangle(0                   , image.Height / 2, image.Width / 2, image.Height / 2);
                Rectangle roi4 = new Rectangle(image.Width / 2     , image.Height / 2, image.Width / 2, image.Height / 2);
                Rectangle roiCenter = new Rectangle(image.Width / 4, image.Height / 4, image.Width / 2, image.Height / 2);

                DenseHistogram hist = new DenseHistogram(64, new RangeF(0.0f, 255.0f));
                double factor = 100.0;

                List<float> totalHist = new List<float>();
                for (int i = 0; i < channels.Length; ++i)
                {
                    float[] histRaw1 = new float[64];
                    float[] histRaw2 = new float[64];
                    float[] histRaw3 = new float[64];
                    float[] histRaw4 = new float[64];
                    float[] histRawCenter = new float[64];

                    Image<Gray, byte>[] tempImages = new Image<Gray, byte>[] { channels[i] };

                    // Top Left
                    channels[i].ROI = roi1;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw1, 0);
                    totalHist.AddRange(histRaw1);

                    // Top Right
                    channels[i].ROI = roi2;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw2, 0);
                    totalHist.AddRange(histRaw2);

                    // Bottom Left
                    channels[i].ROI = roi3;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw3, 0);
                    totalHist.AddRange(histRaw3);

                    // Bottom Right
                    channels[i].ROI = roi4;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw4, 0);
                    totalHist.AddRange(histRaw4);

                    // Center
                    channels[i].ROI = roiCenter;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRawCenter, 0);
                    totalHist.AddRange(histRawCenter);

                    tempImages = null;
                }

                _descriptors = new Matrix<float>(totalHist.ToArray());
                _descriptors = _descriptors.Transpose();
            }
        }

        private Matrix<float> _descriptors;
    }
}
