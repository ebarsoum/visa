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
    public class PeopleDetector : DetectorBase
    {
        public override string DisplayLabel
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

        public PeopleDetector(string model) : base(@"Person", model)
        {
        }


        /// <summary>
        /// Find the pedestrian in the image
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>The image with pedestrian highlighted.</returns>
        public override void Detect(Image<Bgr, Byte> image)
        {
            Detect(image, 0.5f, 0.1f);
        }

        public override IDetector Clone()
        {
            return Clone<PeopleDetector>(() => new PeopleDetector(_model));
        }
    }
}
