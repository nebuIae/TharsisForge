﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EO4SaveEdit.Extensions;
using EO4SaveEdit.FileHandlers;

namespace EO4SaveEdit.Editors
{
    public partial class OptionEditor : UserControl
    {
        Mori4Option gameOptions;

        public OptionEditor()
        {
            InitializeComponent();
        }

        public void Initialize(Mori4Option options)
        {
            this.gameOptions = options;

            if (this.gameOptions == null)
            {
                this.Enabled = false;
                return;
            }

            this.Enabled = true;

            SetComboBoxBinding(cmbBGMVolume, "BGMVolume", Mori4Option.VolumeLevels);
            SetComboBoxBinding(cmbSEVolume, "SEVolume", Mori4Option.VolumeLevels);
            SetComboBoxBinding(cmbMessageSpeed, "MessageSpeed", Mori4Option.MessageSpeeds);
            SetComboBoxBinding(cmbBattleSpeed, "BattleSpeed", Mori4Option.BattleSpeeds);
            chkUnlockBGMTest.SetBinding("Checked", gameOptions, "BGMTestUnlocked");
            SetComboBoxBinding(cmbCameraLR, "CameraLeftRight", Mori4Option.CameraSettings);
            SetComboBoxBinding(cmbCameraUD, "CameraUpDown", Mori4Option.CameraSettings);
            chkAutoMapEnable.SetBinding("Checked", gameOptions, "AutoMapEnable");
            SetComboBoxBinding(cmbDifficulty, "Difficulty", Mori4Option.DifficultyLevels);
        }

        private void SetComboBoxBinding(ComboBox comboBox, string dataMember, Dictionary<byte, string> listDataSource)
        {
            comboBox.ValueMember = "Key";
            comboBox.DisplayMember = "Value";
            comboBox.DataSource = new BindingSource(listDataSource, null);
            comboBox.SetBinding("SelectedValue", gameOptions, dataMember);
        }
    }
}
