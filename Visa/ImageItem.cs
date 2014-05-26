//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Visa
{
    [Serializable()]
    public class ImageItem
    {
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set             
            {
                _path = value;
            }
        }

        public Matrix<float> PointDescriptors
        {
            get 
            {
                return _pointDescriptors;
            }
            set 
            {
                _pointDescriptors = value;
            }
        }

        public Matrix<float> ColorDescriptors
        {
            get
            {
                return _colorDescriptors;
            }
            set
            {
                _colorDescriptors = value;
            }
        }

        public List<string> Labels
        {
            get
            {
                return _labels;
            }
            set 
            {
                _labels = value;
            }
        }

        public List<string> DisplayLabels
        {
            get
            {
                return _displayLabels;
            }
            set
            {
                _displayLabels = value;
            }
        }

        public List<MCvObjectDetection[]> Objects
        {
            get
            {
                return _objects;
            }
            set 
            {
                _objects = value;
            }
        }

        public ImageItem()
        {}

        public ImageItem(string name,
                         string path)
        {
            _name = name;
            _path = path;
        }

        public ImageItem(string name,
                         string path,
                         Matrix<float> pointDescriptors,
                         Matrix<float> colorDescriptors)
        {
            _name = name;
            _path = path;
            _pointDescriptors = pointDescriptors;
            _colorDescriptors = colorDescriptors;
        }

        public ImageItem(string name,
                         string path,
                         List<string> labels,
                         List<string> displayLabels,
                         List<MCvObjectDetection[]> objects)
        {
            _name = name;
            _path = path;
            _labels = labels;
            _displayLabels = displayLabels;
            _objects = objects;
        }

        private string _name;
        private string _path;
        private List<string> _labels;
        private List<string> _displayLabels;
        private List<MCvObjectDetection[]> _objects;
        private Matrix<float> _pointDescriptors;
        private Matrix<float> _colorDescriptors;
    }

    [Serializable()]
    public class ImageItemCollection
    {
        public int Count
        {
            get 
            {
                return _images.Count;
            }
        }

        public List<ImageItem> ImageItems
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
            }
        }

        public ImageItemCollection()
        { 
        }

        public void Add(ImageItem image)
        {
            _images.Add(image);
        }

        public void Clear()
        {
            _images.Clear();
        }

        public void Save(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ImageItemCollection));
                serializer.Serialize(writer, this);
            }
        }

        public void Load(string filename)
        {
            using (Stream fileStream = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ImageItemCollection));
                ImageItemCollection images = (ImageItemCollection)serializer.Deserialize(fileStream);
                _images = images.ImageItems;
            }
        }

        public void Append(ImageItemCollection images)
        {
            foreach (var image in images.ImageItems)
            {
                _images.Add(image);
            }
        }

        private List<ImageItem> _images = new List<ImageItem>();
    }
}
