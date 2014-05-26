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
    public interface IDetector
    {
        int Count
        {
            get;
        }

        MCvObjectDetection[] Objects
        {
            get;
        }

        string DisplayLabel
        {
            get;
        }

        string Label
        {
            get;
        }

        bool Enabled
        {
            get;
            set;
        }

        void Detect(Image<Bgr, Byte> image);

        IDetector Clone();
    }

    /// <summary>
    /// 
    /// </summary>
    public class DetectorBase : IDetector
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
        public virtual string DisplayLabel
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="model"></param>
        public DetectorBase(string label, string model)
        {
            _label = label;
            _model = model;
            _detector = new LatentSvmDetector(model);
        }

        /// <summary>
        /// Find the pedestrian in the image
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>The image with pedestrian highlighted.</returns>
        public virtual void Detect(Image<Bgr, Byte> image)
        {
            Detect(image, 0.5f, 0.1f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="overlapThreshold"></param>
        /// <param name="scoreThreshold"></param>
        protected void Detect(Image<Bgr, Byte> image, float overlapThreshold, float scoreThreshold)
        {
            int objectIndex = 0;
            int objectCount = 0;
            var objects = _detector.Detect(image, overlapThreshold);
            foreach (var detected in objects)
            {
                if (detected.score > scoreThreshold)
                {
                    ++objectCount;
                }
            }

            _objects = new MCvObjectDetection[objectCount];
            foreach (var detected in objects)
            {
                if (detected.score > scoreThreshold)
                {
                    _objects[objectIndex] = detected;
                    ++objectIndex;
                }
            }
        }

        protected IDetector Clone<T>(Func<T> Creator) where T : DetectorBase
        {
            T detector = Creator();

            detector._detector = new LatentSvmDetector(_model);
            detector._objects = new MCvObjectDetection[_objects.Length];
            _objects.CopyTo(detector._objects, 0);

            return detector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IDetector Clone()
        {
            return Clone<DetectorBase>(() => new DetectorBase(_label, _model));
        }

        protected bool _enabled;
        protected string _label;
        protected string _model;
        protected MCvObjectDetection[] _objects;
        protected LatentSvmDetector _detector;
    }

    public class GeneralDetector : DetectorBase
    {
        public GeneralDetector(string label, string model)
            : base(label, model)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns></returns>
        public override void Detect(Image<Bgr, Byte> image)
        {
            Detect(image, 0.5f, 0.1f);
        }

        public override IDetector Clone()
        {
            return Clone<GeneralDetector>(() => new GeneralDetector(_label, _model));
        }
    }
}
