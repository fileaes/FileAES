using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FAES_GUI.CustomControls
{
    public class TextBoxWriter : TextWriter
    {
        TextBox _output = null;

        public TextBoxWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString());
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

    public class RichTextBoxWriter : TextWriter
    {
        RichTextBox _output = null;
        string _logPath = "FileAES-" + DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString() + ".log";

        public RichTextBoxWriter(RichTextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            _output.AppendText(value.ToString());
        }

        public override void WriteLine(string line)
        {
            AppendWithColour(line);
        }

        private void AppendWithColour(string text)
        {
            _output.SelectionColor = Color.LightGray;
            _output.AppendText(text.ToString());
            _output.AppendText(Environment.NewLine);

            CheckKeyword("[DEBUG]", Color.Violet);
            CheckKeyword("[INFO]", Color.LightBlue);
            CheckKeyword("[WARN]", Color.Yellow);
            CheckKeyword("[ERROR]", Color.Red);

            _output.SelectionStart = _output.TextLength;
            _output.SelectionLength = 0;
            _output.ScrollToCaret();
        }

        private void CheckKeyword(string find, Color color)
        {
            if (_output.Text.Contains(find))
            {
                var matchString = Regex.Escape(find);
                foreach (Match match in Regex.Matches(_output.Text, matchString))
                {
                    _output.Select(match.Index, find.Length);
                    _output.SelectionColor = color;
                    _output.Select(_output.TextLength, 0);
                    _output.SelectionColor = Color.LightGray;
                };
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}
