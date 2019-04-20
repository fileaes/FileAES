using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FAES_GUI.CustomControls
{
    [DefaultEvent("Click")]
    public partial class SettingToggle : UserControl
    {
        private Color _enabledColor = Color.ForestGreen;
        private Color _disabledColor = Color.Red;

        public SettingToggle()
        {
            InitializeComponent();
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            Toggled = !Toggled;
        }

        [Description("If the setting is toggled"), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool Toggled
        {
            get
            {
                return (toggleButton.BackColor == _enabledColor);
            }
            set
            {
                if (value)
                {
                    toggleButton.BackColor = _enabledColor;
                    toggleButton.Text = "Enabled";
                }
                else
                {
                    toggleButton.BackColor = _disabledColor;
                    toggleButton.Text = "Disabled";
                }
            }
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
                toggleButton.Enabled = _enabled;
            }
        }

        private void SettingToggle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, mainPanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
    }
}
