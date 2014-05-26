//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Visa
{
    public class HogPeopleDetector : IDetector
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                if (_objects != null)
                {
                    return _objects.Length;
                }

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MCvObjectDetection[] Objects
        {
            get
            {
                return _objects;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayLabel
        {
            get
            {
                if (Count > 0)
                {
                    if (this.Count == 1)
                    {
                        return @"Single Person";
                    }
                    else if (this.Count == 2)
                    {
                        return @"Two Person";
                    }
                    else if (this.Count == 3)
                    {
                        return @"Three Person";
                    }
                    else
                    {
                        return @"Crowd";
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Label
        {
            get
            {
                if (Count > 0)
                {
                    return _label;
                }

                return string.Empty;
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

        public HogPeopleDetector(string model)
        {
            using (StreamReader reader = new StreamReader(model))
            {
                string line = string.Empty; 
                if ((line = reader.ReadLine()) != null)
                {
                    string[] modelParams = line.Split(',');
                    if (modelParams.Length > 0)
                    {
                        _modelParameters = new float[modelParams.Length];

                        for (int i = 0; i < modelParams.Length; ++i)
                        {
                            _modelParameters[i] = Convert.ToSingle(modelParams[i]);
                        }

                        //_detector.SetSVMDetector(HOGDescriptor.GetDefaultPeopleDetector());
                        _detector.SetSVMDetector(_modelParameters);
                    }
                }
            }
        }

        /// <summary>
        /// Find the pedestrian in the image
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>The image with pedestrian highlighted.</returns>
        public void Detect(Image<Bgr, Byte> image)
        {
            Rectangle[] people = _detector.DetectMultiScale(image);
            _objects = new MCvObjectDetection[people.Length];

            for(int i = 0; i < _objects.Length; ++i)
            {
                _objects[i].Rect = people[i];
            }
        }

        public IDetector Clone()
        {
            HogPeopleDetector detector = new HogPeopleDetector(_model);

            detector._enabled = _enabled;
            detector._objects = new MCvObjectDetection[_objects.Length];
            _objects.CopyTo(detector._objects, 0);

            return detector;
        }

        private bool _enabled;
        private string _label = @"Person";
        private string _model;
        private float[] _modelParameters;
        private MCvObjectDetection[] _objects;
        private HOGDescriptor _detector = new HOGDescriptor();
    }
}
