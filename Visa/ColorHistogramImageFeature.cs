//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visa
{
    public class ColorHistogramImageFeature : IImageGlobalFeatures
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

                double factor = 100.0;
                List<float> totalHist = new List<float>();
                DenseHistogram hist = new DenseHistogram(64, new RangeF(0.0f, 255.0f));

                for (int i = 0; i < channels.Length; ++i)
                {
                    float[] histRaw = new float[64];

                    hist.Calculate(new Image<Gray, byte>[] { channels[i] }, false, null);
                    hist.Normalize(factor);
                    hist.MatND.ManagedArray.CopyTo(histRaw, 0);
                    totalHist.AddRange(histRaw);
                }

                _descriptors = new Matrix<float>(totalHist.ToArray());
                _descriptors = _descriptors.Transpose();
            }
        }

        private Matrix<float> _descriptors;
    }
}
