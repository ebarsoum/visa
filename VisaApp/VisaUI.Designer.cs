namespace Visa
{
    partial class VisaUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAndAnnotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appendIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.annotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoAnnotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appendAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAnnotationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportHogFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.queryPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.annotationCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.surfCheckBox = new System.Windows.Forms.CheckBox();
            this.phistCheckBox = new System.Windows.Forms.CheckBox();
            this.queryImageBox = new System.Windows.Forms.PictureBox();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listView = new System.Windows.Forms.ListView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.trainIndoorOutdoorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.queryPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryImageBox)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.detectorsToolStripMenuItem,
            this.trainToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1193, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAndAnnotateToolStripMenuItem,
            this.browseToolStripMenuItem,
            this.inToolStripMenuItem,
            this.annotateToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.fileToolStripMenuItem.Text = "&Images";
            // 
            // openAndAnnotateToolStripMenuItem
            // 
            this.openAndAnnotateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.openAndAnnotateToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openAndAnnotateToolStripMenuItem.Name = "openAndAnnotateToolStripMenuItem";
            this.openAndAnnotateToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.openAndAnnotateToolStripMenuItem.Text = "Open and Annotate";
            this.openAndAnnotateToolStripMenuItem.Click += new System.EventHandler(this.openAndAnnotateToolStripMenuItem_Click);
            // 
            // browseToolStripMenuItem
            // 
            this.browseToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.browseToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.browseToolStripMenuItem.Name = "browseToolStripMenuItem";
            this.browseToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.browseToolStripMenuItem.Text = "Browse";
            this.browseToolStripMenuItem.Click += new System.EventHandler(this.browseToolStripMenuItem_Click);
            // 
            // inToolStripMenuItem
            // 
            this.inToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.inToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.indexToolStripMenuItem,
            this.loadIndexToolStripMenuItem,
            this.appendIndexToolStripMenuItem,
            this.saveIndexToolStripMenuItem});
            this.inToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.inToolStripMenuItem.Name = "inToolStripMenuItem";
            this.inToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.inToolStripMenuItem.Text = "Indexation";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.indexToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.indexToolStripMenuItem.Text = "Index";
            this.indexToolStripMenuItem.Click += new System.EventHandler(this.indexToolStripMenuItem_Click);
            // 
            // loadIndexToolStripMenuItem
            // 
            this.loadIndexToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadIndexToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.loadIndexToolStripMenuItem.Name = "loadIndexToolStripMenuItem";
            this.loadIndexToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.loadIndexToolStripMenuItem.Text = "Load Index";
            this.loadIndexToolStripMenuItem.Click += new System.EventHandler(this.loadIndexToolStripMenuItem_Click);
            // 
            // appendIndexToolStripMenuItem
            // 
            this.appendIndexToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.appendIndexToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.appendIndexToolStripMenuItem.Name = "appendIndexToolStripMenuItem";
            this.appendIndexToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.appendIndexToolStripMenuItem.Text = "Append Index";
            this.appendIndexToolStripMenuItem.Click += new System.EventHandler(this.appendIndexToolStripMenuItem_Click);
            // 
            // saveIndexToolStripMenuItem
            // 
            this.saveIndexToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveIndexToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveIndexToolStripMenuItem.Name = "saveIndexToolStripMenuItem";
            this.saveIndexToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.saveIndexToolStripMenuItem.Text = "Save Index";
            this.saveIndexToolStripMenuItem.Click += new System.EventHandler(this.saveIndexToolStripMenuItem_Click);
            // 
            // annotateToolStripMenuItem
            // 
            this.annotateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.annotateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoAnnotateToolStripMenuItem,
            this.loadAnnotationToolStripMenuItem,
            this.appendAnnotationToolStripMenuItem,
            this.saveAnnotationToolStripMenuItem});
            this.annotateToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.annotateToolStripMenuItem.Name = "annotateToolStripMenuItem";
            this.annotateToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.annotateToolStripMenuItem.Text = "Annotation";
            // 
            // autoAnnotateToolStripMenuItem
            // 
            this.autoAnnotateToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.autoAnnotateToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.autoAnnotateToolStripMenuItem.Name = "autoAnnotateToolStripMenuItem";
            this.autoAnnotateToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.autoAnnotateToolStripMenuItem.Text = "Auto-Annotate";
            this.autoAnnotateToolStripMenuItem.Click += new System.EventHandler(this.autoAnnotateToolStripMenuItem_Click);
            // 
            // loadAnnotationToolStripMenuItem
            // 
            this.loadAnnotationToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadAnnotationToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.loadAnnotationToolStripMenuItem.Name = "loadAnnotationToolStripMenuItem";
            this.loadAnnotationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.loadAnnotationToolStripMenuItem.Text = "Load Annotation";
            this.loadAnnotationToolStripMenuItem.Click += new System.EventHandler(this.loadAnnotationToolStripMenuItem_Click);
            // 
            // appendAnnotationToolStripMenuItem
            // 
            this.appendAnnotationToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.appendAnnotationToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.appendAnnotationToolStripMenuItem.Name = "appendAnnotationToolStripMenuItem";
            this.appendAnnotationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.appendAnnotationToolStripMenuItem.Text = "Append Annotation";
            this.appendAnnotationToolStripMenuItem.Click += new System.EventHandler(this.appendAnnotationToolStripMenuItem_Click);
            // 
            // saveAnnotationToolStripMenuItem
            // 
            this.saveAnnotationToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveAnnotationToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveAnnotationToolStripMenuItem.Name = "saveAnnotationToolStripMenuItem";
            this.saveAnnotationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.saveAnnotationToolStripMenuItem.Text = "Save Annotation";
            this.saveAnnotationToolStripMenuItem.Click += new System.EventHandler(this.saveAnnotationToolStripMenuItem_Click);
            // 
            // detectorsToolStripMenuItem
            // 
            this.detectorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.detectorsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.detectorsToolStripMenuItem.Name = "detectorsToolStripMenuItem";
            this.detectorsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.detectorsToolStripMenuItem.Text = "Detectors";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // trainToolStripMenuItem
            // 
            this.trainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportHogFeaturesToolStripMenuItem,
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem,
            this.trainIndoorOutdoorToolStripMenuItem});
            this.trainToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.trainToolStripMenuItem.Name = "trainToolStripMenuItem";
            this.trainToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.trainToolStripMenuItem.Text = "Features Extrator";
            // 
            // exportHogFeaturesToolStripMenuItem
            // 
            this.exportHogFeaturesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exportHogFeaturesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportHogFeaturesToolStripMenuItem.Name = "exportHogFeaturesToolStripMenuItem";
            this.exportHogFeaturesToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.exportHogFeaturesToolStripMenuItem.Text = "Export Pedestrian Hog Features";
            this.exportHogFeaturesToolStripMenuItem.Click += new System.EventHandler(this.exportHogFeaturesToolStripMenuItem_Click);
            // 
            // exportInOutdoorHistogramFeaturesToolStripMenuItem
            // 
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem.Name = "exportInOutdoorHistogramFeaturesToolStripMenuItem";
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem.Text = "Export Indoor Outdoor Histogram Features";
            this.exportInOutdoorHistogramFeaturesToolStripMenuItem.Click += new System.EventHandler(this.exportInOutdoorHistogramFeaturesToolStripMenuItem_Click);
            // 
            // queryPanel
            // 
            this.queryPanel.BackColor = System.Drawing.Color.Gray;
            this.queryPanel.Controls.Add(this.groupBox2);
            this.queryPanel.Controls.Add(this.groupBox1);
            this.queryPanel.Controls.Add(this.queryImageBox);
            this.queryPanel.Controls.Add(this.selectImageButton);
            this.queryPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.queryPanel.Location = new System.Drawing.Point(1033, 24);
            this.queryPanel.Name = "queryPanel";
            this.queryPanel.Size = new System.Drawing.Size(160, 764);
            this.queryPanel.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.annotationCheckedListBox);
            this.groupBox2.Location = new System.Drawing.Point(16, 462);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 299);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Annotations";
            // 
            // annotationCheckedListBox
            // 
            this.annotationCheckedListBox.BackColor = System.Drawing.Color.Gray;
            this.annotationCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.annotationCheckedListBox.ForeColor = System.Drawing.Color.Black;
            this.annotationCheckedListBox.FormattingEnabled = true;
            this.annotationCheckedListBox.Location = new System.Drawing.Point(8, 19);
            this.annotationCheckedListBox.Name = "annotationCheckedListBox";
            this.annotationCheckedListBox.Size = new System.Drawing.Size(113, 255);
            this.annotationCheckedListBox.TabIndex = 0;
            this.annotationCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.annotationCheckedListBox_ItemCheck);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.surfCheckBox);
            this.groupBox1.Controls.Add(this.phistCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(19, 207);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 77);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Features";
            // 
            // surfCheckBox
            // 
            this.surfCheckBox.AutoSize = true;
            this.surfCheckBox.Checked = true;
            this.surfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.surfCheckBox.Location = new System.Drawing.Point(16, 22);
            this.surfCheckBox.Name = "surfCheckBox";
            this.surfCheckBox.Size = new System.Drawing.Size(55, 17);
            this.surfCheckBox.TabIndex = 2;
            this.surfCheckBox.Text = "SURF";
            this.surfCheckBox.UseVisualStyleBackColor = true;
            this.surfCheckBox.CheckedChanged += new System.EventHandler(this.featureSelectionCheckBox_CheckedChanged);
            // 
            // phistCheckBox
            // 
            this.phistCheckBox.AutoSize = true;
            this.phistCheckBox.Checked = true;
            this.phistCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.phistCheckBox.Location = new System.Drawing.Point(16, 45);
            this.phistCheckBox.Name = "phistCheckBox";
            this.phistCheckBox.Size = new System.Drawing.Size(80, 17);
            this.phistCheckBox.TabIndex = 3;
            this.phistCheckBox.Text = "PHistogram";
            this.phistCheckBox.UseVisualStyleBackColor = true;
            this.phistCheckBox.CheckedChanged += new System.EventHandler(this.featureSelectionCheckBox_CheckedChanged);
            // 
            // queryImageBox
            // 
            this.queryImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.queryImageBox.Location = new System.Drawing.Point(1, 0);
            this.queryImageBox.Name = "queryImageBox";
            this.queryImageBox.Size = new System.Drawing.Size(158, 188);
            this.queryImageBox.TabIndex = 1;
            this.queryImageBox.TabStop = false;
            // 
            // selectImageButton
            // 
            this.selectImageButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.selectImageButton.ForeColor = System.Drawing.Color.White;
            this.selectImageButton.Location = new System.Drawing.Point(19, 301);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(119, 33);
            this.selectImageButton.TabIndex = 0;
            this.selectImageButton.Text = "Search";
            this.selectImageButton.UseVisualStyleBackColor = false;
            this.selectImageButton.Click += new System.EventHandler(this.selectImageButton_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.ForeColor = System.Drawing.Color.White;
            this.listView.GridLines = true;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(0, 24);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1033, 764);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.Gray;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 788);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1193, 22);
            this.statusStrip.TabIndex = 3;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // trainIndoorOutdoorToolStripMenuItem
            // 
            this.trainIndoorOutdoorToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.trainIndoorOutdoorToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.trainIndoorOutdoorToolStripMenuItem.Name = "trainIndoorOutdoorToolStripMenuItem";
            this.trainIndoorOutdoorToolStripMenuItem.Size = new System.Drawing.Size(299, 22);
            this.trainIndoorOutdoorToolStripMenuItem.Text = "Train Indoor Outdoor";
            this.trainIndoorOutdoorToolStripMenuItem.Click += new System.EventHandler(this.trainIndoorOutdoorToolStripMenuItem_Click);
            // 
            // VisaUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 810);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.queryPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "VisaUI";
            this.Text = "Visual Image Search and Annotation  (VISA)";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.queryPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queryImageBox)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel queryPanel;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.PictureBox queryImageBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem trainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportHogFeaturesToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStripMenuItem annotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoAnnotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAnnotationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appendAnnotationToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem exportInOutdoorHistogramFeaturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.CheckBox phistCheckBox;
        private System.Windows.Forms.CheckBox surfCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem openAndAnnotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appendIndexToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox annotationCheckedListBox;
        private System.Windows.Forms.ToolStripMenuItem detectorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainIndoorOutdoorToolStripMenuItem;
    }
}

