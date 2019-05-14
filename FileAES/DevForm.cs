﻿using FAES;
using FAES_GUI.CustomControls;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FAES_GUI
{
    public partial class DevForm : Form
    {
        private string _overrideLogPath = "";
        private Action _checkUpdateAction;

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

        public void SetCheckUpdateAction(Action action)
        {
            _checkUpdateAction = action;
        }

        private void DoCheckUpdate()
        {
            if (_checkUpdateAction != null)
            {
                this.BeginInvoke(new MethodInvoker(_checkUpdateAction));
            }
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
            string[] input = textbox.Text.Split(' ');
            input[0] = input[0].ToLower();

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
            else if (input[0] == "getlatestversiononbranch" || input[0] == "latestver" || input[0] == "latestversion" || input[0] == "latestvercheck")
            {
                Thread updateCheckThread = new Thread(() =>
                {
                    try
                    {
                        string branch = Program.programManager.GetBranch();

                        if (input.Length > 1 && !string.IsNullOrEmpty(input[1]))
                        {
                            string rawBranchRequest = input[1];

                            if (rawBranchRequest.ToLower() == "stable" || rawBranchRequest.ToLower() == "beta" || rawBranchRequest.ToLower() == "dev")
                                branch = rawBranchRequest.ToLower();
                        }

                        string verCheck = String.Format("https://api.mullak99.co.uk/FAES/IsUpdate.php?app=faes_gui&ver=latest&branch={0}&showver=true", branch);

                        Logging.Log(String.Format("Getting the latest FAES_GUI version number on branch '{0}'.", branch));
                        Logging.Log(String.Format("This process may take a few seconds..."));

                        WebClient webClient = new WebClient();
                        string latestVer = webClient.DownloadString(new Uri(verCheck));

                        if (!String.IsNullOrWhiteSpace(latestVer))
                            Logging.Log(String.Format("Latest FAES_GUI Version on branch '{0}' is '{1}'.", branch, latestVer));
                        else
                            Logging.Log(String.Format("The branch '{0}' does not contain any versions!", branch), Severity.WARN);
                    }
                    catch
                    {
                        Logging.Log(String.Format("Unable to connect to the update server! Please check your internet connection."), Severity.WARN);
                    }
                });
                updateCheckThread.Start();
            }
            else if (input[0] == "checkupdate" || input[0] == "check" || input[0] == "updatecheck")
            {
                try
                {
                    string latestVer = GetLatestVersion();
                    string currentVer = ConvertVersionToNonFormatted(Program.GetVersion());

                    string branch = Program.programManager.GetBranch();
                    string compareVersions = String.Format("https://api.mullak99.co.uk/FAES/CompareVersions.php?app=faes_gui&branch={0}&version1={1}&version2={2}", "dev", currentVer, latestVer);

                    WebClient client = new WebClient();
                    byte[] html = client.DownloadData(compareVersions);
                    UTF8Encoding utf = new UTF8Encoding();
                    string result = utf.GetString(html).ToLower();

                    if (String.IsNullOrEmpty(result) || result == "null")
                        Logging.Log(String.Format("Unable to connect to the update server! Please check your internet connection."), Severity.WARN);
                    else if (result.Contains("not exist in the database!") || result == "version1 is newer than version2")
                        Logging.Log(String.Format("You are on a private build. ({0} is newer than {1}).", currentVer, latestVer));
                    else if (result == "version1 is older than version2")
                        Logging.Log(String.Format("You are on an outdated build. ({0} is older than {1}).", currentVer, latestVer));
                    else if (result == "version1 is equal to version2")
                        Logging.Log(String.Format("You are on the latest build. ({0} is equal to {1}).", currentVer, latestVer));
                    else
                        Logging.Log(String.Format("Unable to connect to the update server! Please check your internet connection."), Severity.WARN);
                }
                catch
                {
                    Logging.Log(String.Format("Unable to connect to the update server! Please check your internet connection."), Severity.WARN);
                }

                DoCheckUpdate();
            }
            else if (input[0] == "spoofversion" || input[0] == "spoof")
            {
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]))
                {
                    string verToSpoof = "";

                    if (input[1].Contains("\"") || input[1].Contains("\'"))
                    {
                        for (int i = 1; i < input.Length; i++)
                        {
                            verToSpoof += input[i].Replace("\"", "").Replace("\'", "");
                            verToSpoof += " ";
                        }
                        verToSpoof.TrimEnd(' ');
                    }
                    else verToSpoof = input[1];

                    if (verToSpoof.ToLower() == "reset" || verToSpoof.ToLower() == "off" || verToSpoof.ToLower() == "false")
                    {
                        Logging.Log(String.Format("Disabled Version Spoofing."));
                        Program.SetSpoofedVersion(false);
                    }
                    else
                    {
                        Logging.Log(String.Format("Enabled Version Spoofing. Spoofing Version: {0}", verToSpoof));
                        Program.SetSpoofedVersion(true, verToSpoof);
                    }
                }
                else
                {
                    Logging.Log(String.Format("Disabled Version Spoofing."));
                    Program.SetSpoofedVersion(false);
                }
            }
            else if (input[0] == "getselectedbranch" || input[0] == "branch" || input[0] == "getbranch")
            {
                Logging.Log(String.Format("FAES_GUI Branch: {0}", Program.programManager.GetBranch()));
            }
            else if (input[0] == "setselectedbranch" || input[0] == "setbranch")
            {
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]))
                {
                    string rawBranchRequest = input[1];
                    string validBranch;

                    if (rawBranchRequest.ToLower() == "stable" || rawBranchRequest.ToLower() == "beta" || rawBranchRequest.ToLower() == "dev")
                    {
                        validBranch = rawBranchRequest.ToLower();
                        Program.programManager.SetBranch(validBranch);
                        Logging.Log(String.Format("FAES_GUI Branch changed to: {0}", validBranch));
                    }
                    else Logging.Log(String.Format("'{0}' is not a valid branch!", rawBranchRequest), Severity.WARN);
                }
                else TooFewArgsError(textbox.Text);
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

        private string GetLatestVersion()
        {
            try
            {
                string latestUrl = String.Format("https://api.mullak99.co.uk/FAES/IsUpdate.php?app=faes_gui&branch={0}&showver=true&version={1}", Program.programManager.GetBranch(), ConvertVersionToNonFormatted(Program.GetVersion()));

                WebClient client = new WebClient();
                byte[] html = client.DownloadData(latestUrl);
                UTF8Encoding utf = new UTF8Encoding();
                if (String.IsNullOrEmpty(utf.GetString(html)) || utf.GetString(html) == "null")
                    return "v0.0.0";
                else
                    return utf.GetString(html);
            }
            catch (Exception)
            {
                return "v0.0.0";
            }
        }

        private string ConvertVersionToNonFormatted(string formattedVersion)
        {

            string[] versionSplit = formattedVersion.Replace("(", "").Replace(")", "").Split(' ');
            string nonFormattedVersion;

            if (versionSplit.Length > 0)
            {
                nonFormattedVersion = versionSplit[0];

                if (versionSplit.Length > 1)
                {
                    if (versionSplit[1].ToUpper()[0] == 'B')
                    {
                        nonFormattedVersion += "-B";
                    }
                    else if (versionSplit[1].ToUpper()[0] == 'D')
                    {
                        nonFormattedVersion += "-DEV";
                    }
                    nonFormattedVersion += versionSplit[1].ToUpper().Replace("BETA", "").Replace("B", "").Replace("DEV", "").Replace("D", "");

                    if (versionSplit.Length > 2)
                    {
                        for (int i = 2; i < versionSplit.Length; i++)
                        {
                            formattedVersion += "-";
                            formattedVersion += versionSplit[i].ToUpper();
                        }
                    }
                }
            }
            else nonFormattedVersion = formattedVersion;

            if (nonFormattedVersion.Contains("-B-"))
                nonFormattedVersion = nonFormattedVersion.Replace("-B-", "-B");
            else if (nonFormattedVersion.Contains("-DEV-"))
                nonFormattedVersion = nonFormattedVersion.Replace("-DEV-", "-DEV");
            nonFormattedVersion = nonFormattedVersion.TrimEnd('-');

            return nonFormattedVersion;
        }
    }
}
