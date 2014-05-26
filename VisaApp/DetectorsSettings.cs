//--------------------------------------------------------------------
// Copyright (c) 2014, Emad Barsoum All rights reserved.
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visa
{
    public partial class DetectorsSettings : Form
    {
        public DetectorsSettings(Dictionary<string, IDetector> detectors)
        {
            InitializeComponent();

            _detectors = detectors;
            foreach (var detector in _detectors)
            {
                checkedListBox.Items.Add(detector.Key, detector.Value.Enabled);
            }

            _initialized = true;
        }

        private bool _initialized = false;
        private Dictionary<string, IDetector> _detectors;

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _detectors[(string)checkedListBox.Items[e.Index]].Enabled = (CheckState.Checked == e.NewValue);
        }
    }
}
