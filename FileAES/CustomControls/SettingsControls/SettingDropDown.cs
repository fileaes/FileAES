﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FAES_GUI.CustomControls
{
    [DefaultEvent("Click")]
    public partial class SettingDropDown : UserControl
    {
        private Color _enabledColor = Color.ForestGreen;
        private Color _disabledColor = Color.Red;

        public SettingDropDown()
        {
            InitializeComponent();

            dropDownBox.MouseWheel += new MouseEventHandler(DropDownBox_MouseWheel);
        }

        public void AddItem(string name)
        {
            dropDownBox.Items.Add(name);
        }

        public void ClearItems()
        {
            dropDownBox.Items.Clear();
        }

        public string GetSelectedItem()
        {
            return dropDownBox.SelectedText;
        }

        public int GetSelectedIndex()
        {
            return dropDownBox.SelectedIndex;
        }

        public void SetSelectedItem(string selected)
        {
            dropDownBox.SelectedText = selected;
        }

        public void SetSelectedIndex(int selected)
        {
            dropDownBox.SelectedIndex = selected;
        }

        [Description("Text displayed as the title of the setting toggle"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("Setting Header")]
        public string HeaderText
        {
            get
            {
                return settingHeader.Text;
            }
            set
            {
                settingHeader.Text = value;
            }
        }

        [Description("Text displayed as the description of the setting toggle"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("Setting Description")]
        public string DescriptionText
        {
            get
            {
                return settingDesc.Text;
            }
            set
            {
                settingDesc.Text = value;
            }
        }

        bool _enabled = true;
        [Description("Indicated whether the control is enabled."), Category("Behaviour"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(true)]
        public new bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                dropDownBox.Enabled = _enabled;
            }
        }

        private void SettingToggle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, mainPanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void DropDownBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
    }
}
