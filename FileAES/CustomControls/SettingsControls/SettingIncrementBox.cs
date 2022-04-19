using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;

namespace FAES_GUI.CustomControls
{
    [DefaultEvent("Click")]
    public partial class SettingIncrementBox : UserControl
    {
        private int _maxValue = 2147483647;
        private int _minValue = -2147483647;
        private int _increment = 1;

        public SettingIncrementBox()
        {
            InitializeComponent();
        }

        [Description("Value of the numeric textbox"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(0)]
        public int Value
        {
            get
            {
                try
                {
                    int value = 0;
                    string text = numericTextbox.Text;
                    if (string.IsNullOrWhiteSpace(text)) numericTextbox.Text = "0";
                    int.TryParse(text, out value);

                    if (value < MinValue)
                        value = MinValue;
                    else if (value > MaxValue)
                        value = MaxValue;
                    numericTextbox.Text = value.ToString();
                    return value;
                }
                catch (Exception)
                {
                    int value = 0;

                    if (value < MinValue)
                        value = MinValue;
                    else if (value > MaxValue)
                        value = MaxValue;

                    numericTextbox.Text = value.ToString();
                    return value;
                }
            }
            set
            {
                numericTextbox.Text = Convert.ToString(value);
            }
        }

        [Description("Max value of the numeric textbox"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(2147483647)]
        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
            }
        }

        [Description("Min value of the numeric textbox"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(-2147483647)]
        public int MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
            }
        }

        [Description("Increment value of the numeric textbox"), Category("Data"), Browsable(true), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(1)]
        public int Increment
        {
            get
            {
                return _increment;
            }
            set
            {
                _increment = value;
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
                numericTextbox.Enabled = _enabled;
                upButton.Enabled = _enabled;
                downButton.Enabled = _enabled;
            }
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if ((Value + _increment <= _maxValue) && (Value + _increment >= _minValue))
                Value = Value + _increment;
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if ((Value - _increment <= _maxValue) && (Value - _increment >= _minValue))
                Value = Value - _increment;
        }

        private void SettingIncrementBox_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, mainPanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void numericTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void numericTextbox_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(numericTextbox.Text, "[^0-9]"))
            {
                numericTextbox.Text = Regex.Replace(numericTextbox.Text, "[^0-9]", "");
            }
        }
    }
}
