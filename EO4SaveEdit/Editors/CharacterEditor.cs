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
    public partial class CharacterEditor : UserControl, IEditorControl
    {
        Mori4Game gameData;
        Character currentCharacter;

        ComboBox[] charaEquipmentComboBoxes;

        public CharacterEditor()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        public void Initialize(SaveDataHandler handler)
        {
            this.gameData = handler.GameDataFile;

            if (this.Enabled = (this.gameData != null))
            {
                charaEquipmentComboBoxes = new ComboBox[] { cmbCharacterWeapon, cmbCharacterEquipment, cmbCharacterArmor1, cmbCharacterArmor2 };

                cmbCharacterClass.DisplayMember = "Value";
                cmbCharacterClass.ValueMember = "Key";
                cmbCharacterClass.DataSource = new BindingSource(XmlHelper.ClassNames[SaveDataHandler.SaveLanguage], null);

                cmbCharacterSubclass.DisplayMember = "Value";
                cmbCharacterSubclass.ValueMember = "Key";
                cmbCharacterSubclass.DataSource = new BindingSource(XmlHelper.ClassNames[SaveDataHandler.SaveLanguage], null);

                foreach (ComboBox comboBox in charaEquipmentComboBoxes)
                {
                    comboBox.DisplayMember = "Value";
                    comboBox.ValueMember = "Key";
                    comboBox.DataSource = new BindingSource(XmlHelper.EquipmentNames[SaveDataHandler.SaveLanguage], null);
                }

                lbCharacters.DataSource = this.gameData.Characters;
            }
        }

        private void InitializeControls(Character character)
        {
            currentCharacter = character;

            txtCharacterName.SetBinding("Text", currentCharacter, "Name");
            nudCharacterLevel.SetBinding("Value", currentCharacter, "Level");
            txtCharacterCurrentHP.SetBinding("Text", currentCharacter, "CurrentHP");
            txtCharacterCurrentTP.SetBinding("Text", currentCharacter, "CurrentTP");
            txtCharacterEXP.SetBinding("Text", currentCharacter, "CurrentEXP");

            cmbCharacterClass.SetBinding("SelectedValue", currentCharacter, "Class");
            cmbCharacterSubclass.SetBinding("SelectedValue", currentCharacter, "Subclass");

            ImageHelper.InitializePortraitComboBox(icmbCharacterPortrait, cmbCharacterClass, currentCharacter);

            chkCharacterIsGuildCardChara.SetBinding("Checked", currentCharacter, "IsGuildCardCharacter");
            txtCharacterOriginGuildName.SetBinding("Text", currentCharacter, "OriginGuildName");

            cmbCharacterWeapon.SetBinding("SelectedIndex", currentCharacter.WeaponSlot, "ItemID");
            cmbCharacterEquipment.SetBinding("SelectedIndex", currentCharacter.EquipmentSlot, "ItemID");
            cmbCharacterArmor1.SetBinding("SelectedIndex", currentCharacter.ArmorSlot1, "ItemID");
            cmbCharacterArmor2.SetBinding("SelectedIndex", currentCharacter.ArmorSlot2, "ItemID");

            txtCharacterAvailSkillPoints.SetBinding("Text", currentCharacter, "AvailableSkillPoints");

            nudSTRBoost.SetBinding("Value", currentCharacter, "BookSTRModifier");
            nudTECBoost.SetBinding("Value", currentCharacter, "BookTECModifier");
            nudVITBoost.SetBinding("Value", currentCharacter, "BookVITModifier");
            nudAGIBoost.SetBinding("Value", currentCharacter, "BookAGIModifier");
            nudLUCBoost.SetBinding("Value", currentCharacter, "BookLUCModifier");
        }

        private void lbCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeControls((sender as ListBox).SelectedItem as Character);
        }

        private void btnSkillEditor_Click(object sender, EventArgs e)
        {
            SkillEditorDialog sed = new SkillEditorDialog(currentCharacter.Class, currentCharacter.MainSkillLevels, currentCharacter.Subclass, currentCharacter.SubSkillLevels);
            sed.ShowDialog();
        }

        private void btnWeaponEffectsEdit_Click(object sender, EventArgs e)
        {
            ShowEffectEditor(currentCharacter.WeaponSlot);
        }

        private void btnEquipEffectsEdit_Click(object sender, EventArgs e)
        {
            ShowEffectEditor(currentCharacter.EquipmentSlot);
        }

        private void btnArmor1EffectsEdit_Click(object sender, EventArgs e)
        {
            ShowEffectEditor(currentCharacter.ArmorSlot1);
        }

        private void btnArmor2EffectsEdit_Click(object sender, EventArgs e)
        {
            ShowEffectEditor(currentCharacter.ArmorSlot2);
        }

        private void ShowEffectEditor(Item slot)
        {
            EffectEditorDialog eed = new EffectEditorDialog(slot);
            eed.ShowDialog();
        }

        private void lbCharacters_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                Character selectedCharacter = (e.ListItem as Character);
                if (selectedCharacter.Name == string.Empty)
                    e.Value = "(No name)";
                else
                    e.Value = selectedCharacter.Name;
            }
        }

        private void btnCharacterEditWeaponEffect_Click(object sender, EventArgs e)
        {
            EffectEditorDialog eed = new EffectEditorDialog(currentCharacter.WeaponSlot);
            eed.ShowDialog();
        }

        private void btnCharacterEditEquipEffect_Click(object sender, EventArgs e)
        {
            EffectEditorDialog eed = new EffectEditorDialog(currentCharacter.EquipmentSlot);
            eed.ShowDialog();
        }

        private void btnCharacterEditArmor1Effect_Click(object sender, EventArgs e)
        {
            EffectEditorDialog eed = new EffectEditorDialog(currentCharacter.ArmorSlot1);
            eed.ShowDialog();
        }

        private void btnCharacterEditArmor2Effect_Click(object sender, EventArgs e)
        {
            EffectEditorDialog eed = new EffectEditorDialog(currentCharacter.ArmorSlot2);
            eed.ShowDialog();
        }

        private void btnCharacterSkillEditor_Click(object sender, EventArgs e)
        {
            SkillEditorDialog sed = new SkillEditorDialog(
                currentCharacter.Class, currentCharacter.MainSkillLevels,
                currentCharacter.Subclass, currentCharacter.SubSkillLevels);

            sed.ShowDialog();
        }
    }
}
