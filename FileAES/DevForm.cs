using FAES;
using FAES_GUI.CustomControls;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FAES_GUI
{
    public partial class DevForm : Form
    {
        private string _overrideLogPath = "";

        public DevForm()
        {
            InitializeComponent();

            titleLabel.Text += Program.GetVersion();
            this.Text = titleLabel.Text;

            Console.SetOut(new RichTextBoxWriter(consoleTextBox));
        }

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void titleBar_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, titleBar.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void quitButton_MouseEnter(object sender, EventArgs e)
        {
            quitButton.BackColor = Color.Red;
            quitButton.ForeColor = Color.Black;
        }

        private void quitButton_MouseLeave(object sender, EventArgs e)
        {
            quitButton.BackColor = Color.Transparent;
            quitButton.ForeColor = Color.LightGray;
        }

        private void quitButton_MouseHover(object sender, EventArgs e)
        {
            slowToolTip.SetToolTip(quitButton, "Close");
        }

        private void minButton_MouseEnter(object sender, EventArgs e)
        {
            minButton.BackColor = Color.LightGray;
            minButton.ForeColor = Color.White;
        }

        private void minButton_MouseLeave(object sender, EventArgs e)
        {
            minButton.BackColor = Color.Transparent;
            minButton.ForeColor = Color.White;
        }

        private void minButton_MouseHover(object sender, EventArgs e)
        {
            slowToolTip.SetToolTip(minButton, "Minimise");
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void ClearConsole_Click(object sender, EventArgs e)
        {
            consoleTextBox.Clear();
        }

        private void ExportLog_Click(object sender, EventArgs e)
        {
            string logPath = "FileAES-" + DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString() + ".log";

            if (!string.IsNullOrWhiteSpace(_overrideLogPath))
            {
                if (_overrideLogPath.Contains("{default}") || _overrideLogPath.Contains("{d}"))
                    logPath = _overrideLogPath.Replace("{default}", logPath).Replace("{d}", logPath);
                else
                    logPath = _overrideLogPath;

                string dir = Directory.GetParent(logPath).FullName;
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }

            try
            {
                File.WriteAllText(logPath, consoleTextBox.Text);
                AppendWithColour(consoleTextBox, String.Format("[INFO] Log Exported! ({0})", logPath));
            }
            catch
            {
                AppendWithColour(consoleTextBox, String.Format("[WARN] Log file could not be written to '{0}'!", logPath));
            }
        }

        private void ConsoleInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendInputButton.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void SendInputButton_Click(object sender, EventArgs e)
        {
            CommandInput(consoleInputTextBox);
        }

        private void CommandInput(RichTextBox textbox)
        {
            string[] input = textbox.Text.ToLower().Split(' ');

            uint csBufferTmp = 0;
            if (input[0] == "cryptostreambuffer" || input[0] == "csbuffer" || input[0] == "buffer")
            {
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]) && uint.TryParse(input[1], out csBufferTmp))
                {
                    AppendWithColour(consoleTextBox, String.Format("[INFO] CryptoStream Buffer set to {0} bytes", csBufferTmp));
                    FileAES_Utilities.SetCryptoStreamBuffer(csBufferTmp);
                }
                else AppendWithColour(consoleTextBox, String.Format("[WARN] Too few arguments provided for the '{0}' command!", textbox.Text));
            }
            else if (input[0] == "getcryptostreambuffer" || input[0] == "getcsbuffer" || input[0] == "getbuffer")
            {
                AppendWithColour(consoleTextBox, String.Format("[INFO] CryptoStream Buffer is {0} bytes", FileAES_Utilities.GetCryptoStreamBuffer()));
            }
            else if (input[0] == "getfaestempfolder" || input[0] == "gettemp" || input[0] == "gettempfolder")
            {
                AppendWithColour(consoleTextBox, String.Format("[INFO] FAES Temp Folder is: {0}", FileAES_Utilities.GetFaesTempFolder()));
            }
            else if (input[0] == "getfaesversion" || input[0] == "getfaesver" || input[0] == "faesver")
            {
                AppendWithColour(consoleTextBox, String.Format("[INFO] FAES Version: {0}", FileAES_Utilities.GetVersion()));
            }
            else if (input[0] == "getfaesuiversion" || input[0] == "getfaesuiver" || input[0] == "ver")
            {
                AppendWithColour(consoleTextBox, String.Format("[INFO] FAES_GUI Version: {0}", Program.GetVersion()));
            }
            else if (input[0] == "exportlog" || input[0] == "export" || input[0] == "log")
            {
                ExportLog_Click(null, null);
            }
            else if (input[0] == "setlogpath" || input[0] == "logpath")
            {
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]))
                {
                    _overrideLogPath = input[1].Replace("\"", string.Empty).Replace("\'", string.Empty);
                    AppendWithColour(consoleTextBox, String.Format("[INFO] Log path changed to: {0}", _overrideLogPath));
                }
                else AppendWithColour(consoleTextBox, String.Format("[WARN] Too few arguments provided for the '{0}' command!", textbox.Text));
            }
            else if (input[0] == "resetlogpath")
            {
                _overrideLogPath = "";
                AppendWithColour(consoleTextBox, String.Format("[INFO] Log path reset!"));
            }
            else if (input[0] == "clear" || input[0] == "cls")
            {
                clearConsole.PerformClick();
            }
            else AppendWithColour(consoleTextBox, String.Format("[WARN] Unknown command: {0}", textbox.Text));

            textbox.Clear();
        }

        private void ConsoleInputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(consoleInputTextBox.Text))
                sendInputButton.Enabled = false;
            else
                sendInputButton.Enabled = true;
        }

        private void AppendWithColour(RichTextBox textbox, string text)
        {
            textbox.SelectionColor = Color.LightGray;
            textbox.AppendText(text.ToString());
            textbox.AppendText(Environment.NewLine);

            CheckKeyword(textbox, "[DEBUG]", Color.Violet);
            CheckKeyword(textbox, "[INFO]", Color.LightBlue);
            CheckKeyword(textbox, "[WARN]", Color.Yellow);
            CheckKeyword(textbox, "[ERROR]", Color.Red);

            textbox.SelectionStart = textbox.TextLength;
            textbox.SelectionLength = 0;
            textbox.ScrollToCaret();
        }

        private void CheckKeyword(RichTextBox textbox, string find, Color color)
        {
            if (textbox.Text.Contains(find))
            {
                var matchString = Regex.Escape(find);
                foreach (Match match in Regex.Matches(textbox.Text, matchString))
                {
                    textbox.Select(match.Index, find.Length);
                    textbox.SelectionColor = color;
                    textbox.Select(textbox.TextLength, 0);
                    textbox.SelectionColor = Color.LightGray;
                };
            }
        }
    }
}
