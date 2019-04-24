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

            _overrideLogPath = Program.programManager.GetLogPath();

            _overrideLogPath = _overrideLogPath.Replace('/', '\\').TrimStart('/', '\\');

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
                Logging.Log(String.Format("Log Exported! ({0})", logPath));
            }
            catch
            {
                Logging.Log(String.Format("Log file could not be written to '{0}'!", logPath), Severity.WARN);
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

            if (input[0] == "cryptostreambuffer" || input[0] == "csbuffer" || input[0] == "buffer")
            {
                uint csBufferTmp = 0;
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]) && uint.TryParse(input[1], out csBufferTmp))
                {
                    Logging.Log(String.Format("CryptoStream Buffer set to {0} bytes", csBufferTmp));
                    FileAES_Utilities.SetCryptoStreamBuffer(csBufferTmp);
                }
                else TooFewArgsError(textbox.Text);
            }
            else if (input[0] == "getcryptostreambuffer" || input[0] == "getcsbuffer" || input[0] == "getbuffer")
            {
                Logging.Log(String.Format("CryptoStream Buffer is {0} bytes", FileAES_Utilities.GetCryptoStreamBuffer()));
            }
            else if (input[0] == "getfaestempfolder" || input[0] == "gettemp" || input[0] == "gettempfolder")
            {
                Logging.Log(String.Format("FAES Temp Folder is: {0}", FileAES_Utilities.GetFaesTempFolder()));
            }
            else if (input[0] == "getfaesversion" || input[0] == "getfaesver" || input[0] == "faesver")
            {
                Logging.Log(String.Format("FAES Version: {0}", FileAES_Utilities.GetVersion()));
            }
            else if (input[0] == "getfaesuiversion" || input[0] == "getfaesguiversion" || input[0] == "getfaesuiver" || input[0] == "getfaesguiver" || input[0] == "ver" || input[0] == "guiver" || input[0] == "faesguiver")
            {
                Logging.Log(String.Format("FAES_GUI Version: {0}", Program.GetVersion()));
            }
            else if (input[0] == "getssmversion" || input[0] == "getssmver" || input[0] == "ssmver")
            {
                Logging.Log(String.Format("SSM Version: {0}", SimpleSettingsManager.SSM.GetVersion()));
            }
            else if (input[0] == "exportlog" || input[0] == "export" || input[0] == "log")
            {
                ExportLog_Click(null, null);
            }
            else if (input[0] == "setlogpath")
            {
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]))
                {
                    _overrideLogPath = input[1].Replace("\"", string.Empty).Replace("\'", string.Empty);
                    Program.programManager.SetLogPath(_overrideLogPath);

                    Logging.Log(String.Format("Log path changed to: {0}", _overrideLogPath));
                }
                else TooFewArgsError(textbox.Text);
            }
            else if (input[0] == "getlogpath" || input[0] == "logpath")
            {
                _overrideLogPath = Program.programManager.GetLogPath();
                Logging.Log(String.Format("Log path set to: {0}", _overrideLogPath));
            }
            else if (input[0] == "resetlogpath")
            {
                Program.programManager.ResetLogPath();
                Logging.Log("Log path reset!");
            }
            else if (input[0] == "setdevmode" || input[0] == "setdevelopermode" || input[0] == "setdebugmode" || input[0] == "setdebug" || input[0] == "setdev" || input[0] == "setdeveloper")
            {
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]))
                {
                    bool dev = false;
                    if (input[1] == "1" || input[1] == "true" || input[1] == "t" || input[1] == "y" || input[1] == "yes") dev = true;

                    Program.programManager.SetDevMode(dev);

                    Logging.Log(String.Format("Developer Mode {0}! (Setting will be applied next launch)", dev ? "Enabled" : "Disabled"));
                }
                else TooFewArgsError(textbox.Text);
            }
            else if (input[0] == "getdevmode" || input[0] == "getdevelopermode" || input[0] == "getdebugmode" || input[0] == "getdebug" || input[0] == "getdev" || input[0] == "getdeveloper" || input[0] == "developer" || input[0] == "dev" || input[0] == "debug")
            {
                Logging.Log(String.Format("Developer Mode is {0}!", Program.programManager.GetDevMode() ? "Enabled" : "Disabled"));
            }
            else if (input[0] == "resetdevmode" || input[0] == "resetdevelopermode" || input[0] == "resetdebugmode" || input[0] == "resetdebug" || input[0] == "resetdev" || input[0] == "resetdeveloper")
            {
                Program.programManager.ResetDevMode();
                Logging.Log("Developer Mode reset!");
            }
            else if (input[0] == "clear" || input[0] == "cls")
            {
                clearConsole.PerformClick();
            }
            else Logging.Log(String.Format("Unknown command: {0}", textbox.Text), Severity.WARN);

            textbox.Clear();
        }

        private void TooFewArgsError(string command)
        {
            Logging.Log(String.Format("Too few arguments provided for the '{0}' command!", command), Severity.WARN);
        }

        private void ConsoleInputTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(consoleInputTextBox.Text)) sendInputButton.Enabled = false;
            else sendInputButton.Enabled = true;
        }
    }
}
