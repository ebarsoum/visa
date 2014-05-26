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
    public class ColorHistogramInOutDoorFeature : IImageGlobalFeatures
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

        public void Compute(Image<Bgr, Byte> image)
        {
            using (Image<Hsv, Byte> hsvImage = image.Convert<Hsv, byte>())
            {
                Image<Gray, Byte>[] channels = hsvImage.Split();

                Rectangle roi1 = new Rectangle(0, 0               , image.Width, image.Height / 2);
                Rectangle roi2 = new Rectangle(0, image.Height / 2, image.Width, image.Height / 2);

                DenseHistogram hist = new DenseHistogram(64, new RangeF(0.0f, 255.0f));
                double factor = 100.0;

                List<float> totalHist = new List<float>();
                for (int i = 0; i < channels.Length; ++i)
                {
                    float[] histRaw1 = new float[64];
                    float[] histRaw2 = new float[64];

                    Image<Gray, byte>[] tempImages = new Image<Gray, byte>[] { channels[i] };

                    // Top
                    channels[i].ROI = roi1;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw1, 0);
                    totalHist.AddRange(histRaw1);

                    // Bottom
                    channels[i].ROI = roi2;
                    hist.Calculate(tempImages, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw2, 0);
                    totalHist.AddRange(histRaw2);

                    tempImages = null;
                }

                _descriptorArray = totalHist.ToArray();
                _descriptors = new Matrix<float>(totalHist.ToArray());
                _descriptors = _descriptors.Transpose();
            }
        }

        private float[] _descriptorArray;
        private Matrix<float> _descriptors;
    }
}
