//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Visa
{
    public partial class ImageViewer : Form
    {
        public ImageViewer(ImageItem imageItem)
        {
            InitializeComponent();

            Image<Bgr, Byte> image = new Image<Bgr,byte>(imageItem.Path);
            MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 0.5, 0.5);

            if (imageItem.Objects != null)
            {
                int objectTypeIndex = 0;
                foreach (var objects in imageItem.Objects)
                {
                    int index = 1;
                    foreach (var subject in objects)
                    {
                        Rectangle rect = subject.Rect;
                        PointF[] pts = new PointF[] 
                        {
                            new PointF(rect.Left, rect.Bottom),
                            new PointF(rect.Right, rect.Bottom),
                            new PointF(rect.Right, rect.Top),
                            new PointF(rect.Left, rect.Top)
                        };
 
                        image.DrawPolyline(Array.ConvertAll<PointF, Point>(pts, Point.Round), true, new Bgr(Color.Red), 2);
                        image.Draw(string.Format("{0} {1}", imageItem.Labels[objectTypeIndex], index), ref font, new Point(rect.Left, Math.Max(0, rect.Top - 4)), new Bgr(Color.Red));
                        ++index;
                    }
                    ++objectTypeIndex;
                }
            }

            pictureBox.Image = image.ToBitmap();
        }
    }
}
