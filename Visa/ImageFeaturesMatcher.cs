//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Visa
{
    public class ImageFeaturesMatcher : IImageLocalFeaturesMatcher
    {
        public Image<Bgr, Byte> Result
        {
            get
            {
                return _result;
            }
        }

        public DistanceType Distance
        {
            get
            {
                return _distanceType;
            }
            set
            {
                _distanceType = value;
            }
        }

        public bool Match(IImageLocalFeatures feature1, IImageLocalFeatures feature2)
        {
            bool match = false;
            int k = 2;
            double uniquenessThreshold = 0.8;
            Matrix<byte> mask;
            Matrix<int> indices;
            BruteForceMatcher<float> matcher = new BruteForceMatcher<float>(_distanceType);
            matcher.Add(feature1.Descriptors);

            indices = new Matrix<int>(feature2.Descriptors.Rows, k);
            using (Matrix<float> dist = new Matrix<float>(feature2.Descriptors.Rows, k))
            {
                matcher.KnnMatch(feature2.Descriptors, indices, dist, k, null);
                mask = new Matrix<byte>(dist.Rows, 1);
                mask.SetValue(255);
                Features2DToolbox.VoteForUniqueness(dist, uniquenessThreshold, mask);
            }

            int nonZeroCount = CvInvoke.cvCountNonZero(mask);
            if (nonZeroCount >= 25)
            {
                nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(feature1.KeyPoints, feature2.KeyPoints, indices, mask, 1.5, 20);
                if (nonZeroCount >= 6)
                {
                    _homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(feature1.KeyPoints, feature2.KeyPoints, indices, mask, 2);
                    match = true;
                }
            }

            /*
            _result = Features2DToolbox.DrawMatches(feature1.Image, feature1.KeyPoints, feature2.Image, feature2.KeyPoints,
                                                    indices,
                                                    new Bgr(255, 0, 0),
                                                    new Bgr(255, 255, 255),
                                                    mask,
                                                    Features2DToolbox.KeypointDrawType.DEFAULT);

            if (_homography != null)
            {
                Rectangle rect = feature2.Image.ROI;
                PointF[] pts = new PointF[] 
                {
                    new PointF(rect.Left, rect.Bottom),
                    new PointF(rect.Right, rect.Bottom),
                    new PointF(rect.Right, rect.Top),
                    new PointF(rect.Left, rect.Top)
                };

                _homography.ProjectPoints(pts);
                _result.DrawPolyline(Array.ConvertAll<PointF, Point>(pts, Point.Round), true, new Bgr(Color.Red), 5);
            }
            */

            return match;
        }

        public uint Similar(IImageLocalFeatures feature1, IImageLocalFeatures feature2)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            Matrix<byte> mask;
            Matrix<int> indices;
            BruteForceMatcher<float> matcher = new BruteForceMatcher<float>(_distanceType);
            matcher.Add(feature1.Descriptors);

            indices = new Matrix<int>(feature2.Descriptors.Rows, k);
            using (Matrix<float> dist = new Matrix<float>(feature2.Descriptors.Rows, k))
            {
                matcher.KnnMatch(feature2.Descriptors, indices, dist, k, null);
                mask = new Matrix<byte>(dist.Rows, 1);
                mask.SetValue(255);
                Features2DToolbox.VoteForUniqueness(dist, uniquenessThreshold, mask);
            }

            return (uint)CvInvoke.cvCountNonZero(mask);
        }

        private DistanceType _distanceType = DistanceType.L1;
        private HomographyMatrix _homography;
        private Image<Bgr, Byte> _result;
    }
}
