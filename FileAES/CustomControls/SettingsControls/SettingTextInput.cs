using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;

namespace FAES_GUI.CustomControls
{
    [DefaultEvent("Click")]
    public partial class SettingTextInput : UserControl
    {
        public SettingTextInput()
        {
            InitializeComponent();
        }

        [Description("Value of the textbox"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("Text")]
        public string Value
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
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
        [EditorAttribute("System.ComponentModel.Design.MultilineStringEditor, System.Design", "System.Drawing.Design.UITypeEditor")]
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

        private void SettingTextInput_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, mainPanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {

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
                textBox.Enabled = _enabled;
            }
        }
    }
}
