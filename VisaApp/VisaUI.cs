//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visa
{
    public partial class VisaUI : Form
    {
        private ImageDatabase _imageDatabase = new ImageDatabase();
        private AnnotationEngine _annotationEngine = new AnnotationEngine();
        private bool _disableAnnotationCheckbox = false;

        public VisaUI()
        {
            InitializeComponent();

            _annotationEngine.AddDetector(@"inoutdoor", new InOutDoorDetector(@".\models\inoutdoor\inoutdoor.model"), true);
            _annotationEngine.AddDetector(@"human", new HogPeopleDetector(@".\models\hog\pedestrian.model"), false);
            _annotationEngine.AddDetector(@"person", new PeopleDetector(@".\models\voc2009\person.xml"), true);
            _annotationEngine.AddDetector(@"aeroplane", new GeneralDetector(@"aeroplane", @".\models\voc2009\aeroplane.xml"), false);
            _annotationEngine.AddDetector(@"bicycle", new GeneralDetector(@"bicycle", @".\models\voc2009\bicycle.xml"), false);
            _annotationEngine.AddDetector(@"bird", new GeneralDetector(@"bird", @".\models\voc2009\bird.xml"), false);
            _annotationEngine.AddDetector(@"boat", new GeneralDetector(@"boat", @".\models\voc2009\boat.xml"), false);
            _annotationEngine.AddDetector(@"bottle", new GeneralDetector(@"bottle", @".\models\voc2009\bottle.xml"), false);
            _annotationEngine.AddDetector(@"bus", new GeneralDetector(@"bus", @".\models\voc2009\bus.xml"), false);
            _annotationEngine.AddDetector(@"car", new GeneralDetector(@"car", @".\models\voc2009\car.xml"), false);
            _annotationEngine.AddDetector(@"cat", new GeneralDetector(@"cat", @".\models\voc2009\cat.xml"), false);
            _annotationEngine.AddDetector(@"chair", new GeneralDetector(@"chair", @".\models\voc2009\chair.xml"), false);
            _annotationEngine.AddDetector(@"cow", new GeneralDetector(@"cow", @".\models\voc2009\cow.xml"), false);
            _annotationEngine.AddDetector(@"diningtable", new GeneralDetector(@"diningtable", @".\models\voc2009\diningtable.xml"), false);
            _annotationEngine.AddDetector(@"dog", new GeneralDetector(@"dog", @".\models\voc2009\dog.xml"), false);
            _annotationEngine.AddDetector(@"horse", new GeneralDetector(@"horse", @".\models\voc2009\horse.xml"), false);
            _annotationEngine.AddDetector(@"motorbike", new GeneralDetector(@"motorbike", @".\models\voc2009\motorbike.xml"), false);
            _annotationEngine.AddDetector(@"pottedplant", new GeneralDetector(@"pottedplant", @".\models\voc2009\pottedplant.xml"), false);
            _annotationEngine.AddDetector(@"sheep", new GeneralDetector(@"sheep", @".\models\voc2009\sheep.xml"), false);
            _annotationEngine.AddDetector(@"sofa", new GeneralDetector(@"sofa", @".\models\voc2009\sofa.xml"), false);
            _annotationEngine.AddDetector(@"train", new GeneralDetector(@"train", @".\models\voc2009\train.xml"), false);
            _annotationEngine.AddDetector(@"tvmonitor", new GeneralDetector(@"tvmonitor", @".\models\voc2009\tvmonitor.xml"), false);
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var folder = new DirectoryInfo(folderBrowserDialog.SelectedPath);

                annotationCheckedListBox.Items.Clear();
                imageList.Images.Clear();
                imageList.ColorDepth = ColorDepth.Depth24Bit;
                listView.Items.Clear();
                listView.View = View.LargeIcon;

                var annotationFilesOnDisk = FileHelper.GetAnnotationFiles(folder);
                if (annotationFilesOnDisk.Count > 0)
                {
                    _annotationEngine.LoadAnnotation(annotationFilesOnDisk[0].FullName);

                    int index = 0;
                    foreach (var imageItem in _annotationEngine.ImageItems)
                    {
                        imageList.Images.Add(ImageHelper.GetThumbnailImage(imageItem.Path, 128));
                        listView.Items.Add(imageItem.Name);
                        listView.Items[index].ImageIndex = index;
                        listView.Items[index].Tag = imageItem;

                        listView.Items[index].Text = @"";
                        foreach (var displayLabel in imageItem.DisplayLabels)
                        {
                            if (!string.IsNullOrEmpty(displayLabel))
                            {
                                listView.Items[index].Text += displayLabel + @";";
                            }
                        }

                        ++index;
                    }

                    _disableAnnotationCheckbox = true;
                    HashSet<string> labels = _annotationEngine.Labels;
                    foreach (var label in labels)
                    {
                        annotationCheckedListBox.Items.Add(label, true);
                    }
                    _disableAnnotationCheckbox = false;
                }
                else 
                {
                    int index = 0;
                    var imagesOnDisk = FileHelper.GetImages(folder);

                    foreach (var image in imagesOnDisk)
                    {
                        imageList.Images.Add(ImageHelper.GetThumbnailImage(image.FullName, 128));
                        listView.Items.Add(image.Name);
                        listView.Items[index].ImageIndex = index;
                        listView.Items[index].Tag = new ImageItem(image.Name, image.FullName);
                        listView.Items[index].Text = image.Name;

                        ++index;
                    }
                }
            }
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _imageDatabase.Index(new DirectoryInfo(folderBrowserDialog.SelectedPath));
            }
        }

        private void selectImageButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) 
            {
                try
                {
                    queryImageBox.ImageLocation = openFileDialog.FileName;
                    queryImageBox.SizeMode = PictureBoxSizeMode.Zoom;

                    Search();
                }
                catch (IOException)
                {
                }
            }
        }

        private void Search()
        {
            ImageFeatureSelection features = new ImageFeatureSelection();
            if (surfCheckBox.Checked)
            {
                features |= ImageFeatureSelection.Surf;
            }
            if (phistCheckBox.Checked)
            {
                features |= ImageFeatureSelection.PHistogram;
            }

            Stopwatch stopwatch = new Stopwatch();
            toolStripStatusLabel.Text = "";

            try
            {
                stopwatch.Start();

                var results = _imageDatabase.Query(queryImageBox.ImageLocation, features);

                imageList.Images.Clear();
                imageList.ColorDepth = ColorDepth.Depth24Bit;
                listView.Items.Clear();
                listView.View = View.LargeIcon;

                int index = 0;
                foreach (var result in results)
                {
                    imageList.Images.Add(ImageHelper.GetThumbnailImage(result.Path, 128));
                    listView.Items.Add("");
                    listView.Items[index].ImageIndex = index;
                    listView.Items[index].Tag = result;

                    ++index;
                }

                stopwatch.Stop();
                toolStripStatusLabel.Text = string.Format("Took {0} seconds to finish.", (float)stopwatch.ElapsedMilliseconds / 1000.0f);
            }
            catch (IOException)
            {
            }
        }

        private void saveIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Index files (*.inx)|*.inx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _imageDatabase.SaveIndex(saveFileDialog.FileName);
            }
        }

        private void loadIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Index files (*.inx)|*.inx";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _imageDatabase.LoadIndex(openFileDialog.FileName);
            }
        }

        private void exportHogFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string root = folderBrowserDialog.SelectedPath;
                HogImageFeatures hogFeature = new HogImageFeatures();
                DirectoryInfo trainPosDir = new DirectoryInfo(root + @"\Train\pos");
                DirectoryInfo trainNegDir = new DirectoryInfo(root + @"\Train\neg");
                DirectoryInfo testPosDir  = new DirectoryInfo(root + @"\Test\pos");
                DirectoryInfo testNegDir  = new DirectoryInfo(root + @"\Test\neg");                

                var imagesOnDisk = FileHelper.GetImages(trainPosDir);
                using (StreamWriter file = new StreamWriter(root + @"\train_pos.csv"))
                {
                    ExportCrop96x160HogToCsv(file, imagesOnDisk, hogFeature);
                }

                imagesOnDisk = FileHelper.GetImages(trainNegDir);
                using (StreamWriter file = new StreamWriter(root + @"\train_neg.csv"))
                {
                    ExportHogToCsv(file, imagesOnDisk, hogFeature);
                }

                imagesOnDisk = FileHelper.GetImages(testPosDir);
                using (StreamWriter file = new StreamWriter(root + @"\test_pos.csv"))
                {
                    ExportCrop70x134HogToCsv(file, imagesOnDisk, hogFeature);
                }

                imagesOnDisk = FileHelper.GetImages(testNegDir);
                using (StreamWriter file = new StreamWriter(root + @"\test_neg.csv"))
                {
                    ExportHogToCsv(file, imagesOnDisk, hogFeature);
                }
            }
        }

        private void ExportCrop96x160HogToCsv(StreamWriter file, List<FileInfo> imagesOnDisk, HogImageFeatures hogFeature)
        {
            Rectangle roi = new Rectangle(16, 16, 64, 128);

            foreach (var image in imagesOnDisk)
            {
                using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                {
                    hogFeature.Compute(tempImage, roi);
                    var descriptor = string.Join(",", hogFeature.DescriptorArray);
                    file.WriteLine(descriptor);
                }
            }
        }

        private void ExportCrop70x134HogToCsv(StreamWriter file, List<FileInfo> imagesOnDisk, HogImageFeatures hogFeature)
        {
            Rectangle roi = new Rectangle(3, 3, 64, 128);

            foreach (var image in imagesOnDisk)
            {
                using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                {
                    hogFeature.Compute(tempImage, roi);
                    var descriptor = string.Join(",", hogFeature.DescriptorArray);
                    file.WriteLine(descriptor);
                }
            }
        }

        private void ExportHogToCsv(StreamWriter file, List<FileInfo> imagesOnDisk, HogImageFeatures hogFeature)
        {
            foreach (var image in imagesOnDisk)
            {
                using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                {
                    int offsetX = 0;
                    int offsetY = 0;
                    int width  = tempImage.Width;
                    int height = tempImage.Height;

                    while ((offsetY + 128) <= height)
                    {
                        while ((offsetX + 64) <= width)
                        {
                            hogFeature.Compute(tempImage, new Rectangle(offsetX, offsetY, 64, 128));
                            var descriptor = string.Join(",", hogFeature.DescriptorArray);
                            file.WriteLine(descriptor);

                            offsetX += 32;
                        }
                        offsetY += 64;
                    }
                }
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            var item = listView.SelectedItems[0];
            ImageItem imageItem = (ImageItem)item.Tag;

            ImageViewer viewer = new ImageViewer(imageItem);
            viewer.ShowDialog();
        }

        private void ExportInOutdoorHistToCsv(StreamWriter file, List<FileInfo> imagesOnDisk, ColorHistogramInOutDoorFeature inoutdoorFeature)
        {
            foreach (var image in imagesOnDisk)
            {
                using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                {
                    inoutdoorFeature.Compute(tempImage);
                    var descriptor = string.Join(",", inoutdoorFeature.DescriptorArray);
                    file.WriteLine(descriptor);
                }
            }
        }

        private void exportInOutdoorHistogramFeaturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string root = folderBrowserDialog.SelectedPath;
                ColorHistogramInOutDoorFeature inoutdoorFeature = new ColorHistogramInOutDoorFeature();
                DirectoryInfo trainPosDir = new DirectoryInfo(root + @"\Train\pos");
                DirectoryInfo trainNegDir = new DirectoryInfo(root + @"\Train\neg");
                DirectoryInfo testPosDir = new DirectoryInfo(root + @"\Test\pos");
                DirectoryInfo testNegDir = new DirectoryInfo(root + @"\Test\neg");

                var imagesOnDisk = FileHelper.GetImages(trainPosDir);
                using (StreamWriter file = new StreamWriter(root + @"\train_pos.csv"))
                {
                    ExportInOutdoorHistToCsv(file, imagesOnDisk, inoutdoorFeature);
                }

                imagesOnDisk = FileHelper.GetImages(trainNegDir);
                using (StreamWriter file = new StreamWriter(root + @"\train_neg.csv"))
                {
                    ExportInOutdoorHistToCsv(file, imagesOnDisk, inoutdoorFeature);
                }

                imagesOnDisk = FileHelper.GetImages(testPosDir);
                using (StreamWriter file = new StreamWriter(root + @"\test_pos.csv"))
                {
                    ExportInOutdoorHistToCsv(file, imagesOnDisk, inoutdoorFeature);
                }

                imagesOnDisk = FileHelper.GetImages(testNegDir);
                using (StreamWriter file = new StreamWriter(root + @"\test_neg.csv"))
                {
                    ExportInOutdoorHistToCsv(file, imagesOnDisk, inoutdoorFeature);
                }
            }
        }

        private void featureSelectionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void openAndAnnotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            Stopwatch stopwatch = new Stopwatch();
            toolStripStatusLabel.Text = "";

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) 
            {
                stopwatch.Start();

                annotationCheckedListBox.Items.Clear();
                imageList.Images.Clear();
                imageList.ColorDepth = ColorDepth.Depth24Bit;
                listView.Items.Clear();
                listView.View = View.LargeIcon;

                _annotationEngine.AnnotateAnImage(openFileDialog.FileName);
                imageList.Images.Add(ImageHelper.GetThumbnailImage(openFileDialog.FileName, 128));

                if (_annotationEngine.ImageItems.Count > 0)
                {
                    ImageItem imageItem = _annotationEngine.ImageItems[0];

                    listView.Items.Add(imageItem.Name);
                    listView.Items[0].ImageIndex = 0;
                    listView.Items[0].Tag = imageItem;

                    listView.Items[0].Text = @"";
                    foreach (var displayLabel in imageItem.DisplayLabels)
                    {
                        if (!string.IsNullOrEmpty(displayLabel))
                        {
                            listView.Items[0].Text += displayLabel + @";";
                        }
                    }

                    _disableAnnotationCheckbox = true;
                    HashSet<string> labels = _annotationEngine.Labels;
                    foreach(var label in labels)
                    {
                        annotationCheckedListBox.Items.Add(label, true);
                    }
                    _disableAnnotationCheckbox = false;
                }

                stopwatch.Stop();
                toolStripStatusLabel.Text = string.Format("Took {0} seconds to finish.", (float)stopwatch.ElapsedMilliseconds / 1000.0f);
            }
        }

        private void appendIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Index files (*.inx)|*.inx";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _imageDatabase.AppendIndex(openFileDialog.FileName);
            }
        }

        private void autoAnnotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _annotationEngine.Annotate(new DirectoryInfo(folderBrowserDialog.SelectedPath));
            }
        }

        private void loadAnnotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Annotation files (*.ant)|*.ant";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _annotationEngine.LoadAnnotation(openFileDialog.FileName);
            }
        }

        private void appendAnnotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Annotation files (*.ant)|*.ant";
            openFileDialog.FilterIndex = 1;

            openFileDialog.Multiselect = false;

            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _annotationEngine.AppendAnnotation(openFileDialog.FileName);
            }
        }

        private void saveAnnotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Annotation files (*.ant)|*.ant";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _annotationEngine.SaveAnnotation(saveFileDialog.FileName);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectorsSettings settings = new DetectorsSettings(_annotationEngine.Detectors);
            settings.ShowDialog();
        }

        private void annotationCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_disableAnnotationCheckbox)
            {
                return;
            }

            HashSet<string> checkedItems = new HashSet<string>();

            foreach (var item in annotationCheckedListBox.CheckedItems)
            {
                if ((string)annotationCheckedListBox.Items[e.Index] != (string)item)
                {
                    checkedItems.Add((string)item);
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                checkedItems.Add((string)annotationCheckedListBox.Items[e.Index]);
            }

            if (_annotationEngine.ImageItems.Count > 0)
            {
                imageList.Images.Clear();
                listView.Items.Clear();

                int index = 0;
                foreach (var imageItem in _annotationEngine.Filter(checkedItems))
                {
                    imageList.Images.Add(ImageHelper.GetThumbnailImage(imageItem.Path, 128));
                    listView.Items.Add(imageItem.Name);
                    listView.Items[index].ImageIndex = index;
                    listView.Items[index].Tag = imageItem;

                    listView.Items[index].Text = @"";
                    foreach (var displayLabel in imageItem.DisplayLabels)
                    {
                        if (!string.IsNullOrEmpty(displayLabel))
                        {
                            listView.Items[index].Text += displayLabel + @";";
                        }
                    }

                    ++index;
                }
            }
        }

        private void trainIndoorOutdoorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult dialogResult = this.folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string root = folderBrowserDialog.SelectedPath;
                ColorHistogramInOutDoorFeature inoutdoorFeature = new ColorHistogramInOutDoorFeature();
                DirectoryInfo trainPosDir = new DirectoryInfo(root + @"\Train\pos");
                DirectoryInfo trainNegDir = new DirectoryInfo(root + @"\Train\neg");
                DirectoryInfo testPosDir = new DirectoryInfo(root + @"\Test\pos");
                DirectoryInfo testNegDir = new DirectoryInfo(root + @"\Test\neg");

                var posImagesOnDisk = FileHelper.GetImages(trainPosDir);
                var negImagesOnDisk = FileHelper.GetImages(trainNegDir);

                int trainingCount = posImagesOnDisk.Count + negImagesOnDisk.Count;

                int offset = 0;
                int index = 0;
                int dimension = 3 * 2 * 64;
               
                Matrix<float> trainClasses = new Matrix<float>(trainingCount, 1);

                float[,] flatDescriptors = new float[trainingCount, dimension];
                foreach (var image in posImagesOnDisk)
                {
                    using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                    {
                        inoutdoorFeature.Compute(tempImage);
                        var descriptor = inoutdoorFeature.Descriptors;
                        Buffer.BlockCopy(descriptor.ManagedArray, 0, flatDescriptors, offset, sizeof(float) * descriptor.ManagedArray.Length);
                        offset += sizeof(float) * descriptor.ManagedArray.Length;

                        trainClasses[index, 0] = 1.0f;
                        ++index;
                    }
                }

                foreach (var image in negImagesOnDisk)
                {
                    using (Image<Bgr, Byte> tempImage = new Image<Bgr, Byte>(image.FullName))
                    {
                        inoutdoorFeature.Compute(tempImage);
                        var descriptor = inoutdoorFeature.Descriptors;
                        Buffer.BlockCopy(descriptor.ManagedArray, 0, flatDescriptors, offset, sizeof(float) * descriptor.ManagedArray.Length);
                        offset += sizeof(float) * descriptor.ManagedArray.Length;

                        trainClasses[index, 0] = -1.0f;
                        ++index;
                    }
                }

                Matrix<float> trainData = new Matrix<float>(flatDescriptors);
                using (SVM model = new SVM())
                {
                    SVMParams p = new SVMParams();
                    p.KernelType = Emgu.CV.ML.MlEnum.SVM_KERNEL_TYPE.LINEAR;
                    p.SVMType = Emgu.CV.ML.MlEnum.SVM_TYPE.C_SVC;
                    p.C = 1;
                    p.TermCrit = new MCvTermCriteria(1000, 0.01);

                    bool trained = model.TrainAuto(trainData, trainClasses, null, null, p.MCvSVMParams, 5);

                    model.Save(root + @"\inoutdoor.model");
                }
            }
        }
    }
}
