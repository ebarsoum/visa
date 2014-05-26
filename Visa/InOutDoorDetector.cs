//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Visa
{
    public class InOutDoorDetector : IDetector
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MCvObjectDetection[] Objects
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayLabel
        {
            get
            {
                return Label;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
        }

        /// <summary>
        /// Enable or disable this detector.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        public InOutDoorDetector(string model)
        {
            _model = model;
            _detector.Load(model);
        }

        /// <summary>
        /// Find the pedestrian in the image
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>The image with pedestrian highlighted.</returns>
        public void Detect(Image<Bgr, Byte> image)
        {
            ColorHistogramInOutDoorFeature inoutdoorFeature = new ColorHistogramInOutDoorFeature();
            inoutdoorFeature.Compute(image);
            float response = _detector.Predict(inoutdoorFeature.Descriptors);
            if (response > 0.0f)
            {
                _label = @"Outdoor";
            }
            else if (response < 0.0f)
            {
                _label = @"Indoor";
            }
            else
            {
                _label = @"";
            }
        }

        public IDetector Clone()
        {
            InOutDoorDetector detector = new InOutDoorDetector(_model);

            detector._enabled = _enabled;
            detector._label = _label;

            return detector;
        }

        private bool _enabled;
        private string _label;
        private string _model;
        private SVM _detector = new SVM();
    }
}
