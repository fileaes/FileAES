using FAES;
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
            base.Text = titleLabel.Text;

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
            string logPath = Utilities.CreateLogFile(true);

            try
            {
                File.WriteAllText(logPath, consoleTextBox.Text);
                Logging.Log($"Log Exported! ({logPath})");
            }
            catch
            {
                Logging.Log($"Log file could not be written to '{logPath}'!", Severity.WARN);
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
                if (input.Length > 1 && !string.IsNullOrEmpty(input[1]) && uint.TryParse(input[1], out uint csBufferTmp))
                {
                    Logging.Log($"CryptoStream Buffer set to {csBufferTmp} bytes");
                    FileAES_Utilities.SetCryptoStreamBuffer(csBufferTmp);
                }
                else TooFewArgsError(textbox.Text);
            }
            else if (input[0] == "getcryptostreambuffer" || input[0] == "getcsbuffer" || input[0] == "getbuffer")
            {
                Logging.Log($"CryptoStream Buffer is {FileAES_Utilities.GetCryptoStreamBuffer()} bytes");
            }
            else if (input[0] == "getfaestempfolder" || input[0] == "gettemp" || input[0] == "gettempfolder")
            {
                Logging.Log($"FAES Temp Folder is: {FileAES_Utilities.GetFaesTempFolder()}");
            }
            else if (input[0] == "getfaesversion" || input[0] == "getfaesver" || input[0] == "faesver")
            {
                Logging.Log($"FAES Version: {FileAES_Utilities.GetVersion()}");
            }
            else if (input[0] == "getfaesuiversion" || input[0] == "getfaesguiversion" || input[0] == "getfaesuiver" || input[0] == "getfaesguiver" || input[0] == "ver" || input[0] == "guiver" || input[0] == "faesguiver")
            {
                Logging.Log($"FAES_GUI Version: {Program.GetVersion()}");
            }
            else if (input[0] == "getssmversion" || input[0] == "getssmver" || input[0] == "ssmver")
            {
                Logging.Log($"SSM Version: {SimpleSettingsManager.SSM.GetVersion()}");
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

                        string verCheck =
                            $"https://api.mullak99.co.uk/FAES/IsUpdate.php?app=faes_gui&ver=latest&branch={branch}&showver=true";

                        Logging.Log($"Getting the latest FAES_GUI version number on branch '{branch}'.");
                        Logging.Log("This process may take a few seconds...");

                        WebClient webClient = new WebClient();
                        string latestVer = webClient.DownloadString(new Uri(verCheck));

                        if (!string.IsNullOrWhiteSpace(latestVer))
                            Logging.Log($"Latest FAES_GUI Version on branch '{branch}' is '{latestVer}'.");
                        else
                            Logging.Log($"The branch '{branch}' does not contain any versions!", Severity.WARN);
                    }
                    catch
                    {
                        Logging.Log("Unable to connect to the update server! Please check your internet connection.", Severity.WARN);
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

                    Program.programManager.GetBranch();
                    string compareVersions =
                        $"https://api.mullak99.co.uk/FAES/CompareVersions.php?app=faes_gui&branch={"dev"}&version1={currentVer}&version2={latestVer}";

                    WebClient client = new WebClient();
                    byte[] html = client.DownloadData(compareVersions);
                    UTF8Encoding utf = new UTF8Encoding();
                    string result = utf.GetString(html).ToLower();

                    if (string.IsNullOrEmpty(result) || result == "null")
                        Logging.Log("Unable to connect to the update server! Please check your internet connection.", Severity.WARN);
                    else if (result.Contains("not exist in the database!") || result == "version1 is newer than version2")
                        Logging.Log($"You are on a private build. ({currentVer} is newer than {latestVer}).");
                    else if (result == "version1 is older than version2")
                        Logging.Log($"You are on an outdated build. ({currentVer} is older than {latestVer}).");
                    else if (result == "version1 is equal to version2")
                        Logging.Log($"You are on the latest build. ({currentVer} is equal to {latestVer}).");
                    else
                        Logging.Log("Unable to connect to the update server! Please check your internet connection.", Severity.WARN);
                }
                catch
                {
                    Logging.Log("Unable to connect to the update server! Please check your internet connection.", Severity.WARN);
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
                        verToSpoof = verToSpoof.TrimEnd(' ');
                    }
                    else verToSpoof = input[1];

                    if (verToSpoof.ToLower() == "reset" || verToSpoof.ToLower() == "off" || verToSpoof.ToLower() == "false")
                    {
                        Logging.Log("Disabled Version Spoofing.");
                        Program.SetSpoofedVersion(false);
                    }
                    else
                    {
                        Logging.Log($"Enabled Version Spoofing. Spoofing Version: {verToSpoof}");
                        Program.SetSpoofedVersion(true, verToSpoof);
                    }
                }
                else
                {
                    Logging.Log("Disabled Version Spoofing.");
                    Program.SetSpoofedVersion(false);
                }
            }
            else if (input[0] == "getselectedbranch" || input[0] == "branch" || input[0] == "getbranch")
            {
                Logging.Log($"FAES_GUI Branch: {Program.programManager.GetBranch()}");
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
                        Logging.Log($"FAES_GUI Branch changed to: {validBranch}");
                    }
                    else Logging.Log($"'{rawBranchRequest}' is not a valid branch!", Severity.WARN);
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

                    Logging.Log($"Log path changed to: {_overrideLogPath}");
                }
                else TooFewArgsError(textbox.Text);
            }
            else if (input[0] == "getlogpath" || input[0] == "logpath")
            {
                _overrideLogPath = Program.programManager.GetLogPath();
                Logging.Log($"Log path set to: {_overrideLogPath}");
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

                    Logging.Log(
                        $"Developer Mode {(dev ? "Enabled" : "Disabled")}! (Setting will be applied next launch)");
                }
                else TooFewArgsError(textbox.Text);
            }
            else if (input[0] == "getdevmode" || input[0] == "getdevelopermode" || input[0] == "getdebugmode" || input[0] == "getdebug" || input[0] == "getdev" || input[0] == "getdeveloper" || input[0] == "developer" || input[0] == "dev" || input[0] == "debug")
            {
                Logging.Log($"Developer Mode is {(Program.programManager.GetDevMode() ? "Enabled" : "Disabled")}!");
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
            else Logging.Log($"Unknown command: {textbox.Text}", Severity.WARN);

            textbox.Clear();
        }

        private void TooFewArgsError(string command)
        {
            Logging.Log($"Too few arguments provided for the '{command}' command!", Severity.WARN);
        }

        private void ConsoleInputTextBox_TextChanged(object sender, EventArgs e)
        {
            sendInputButton.Enabled = !string.IsNullOrWhiteSpace(consoleInputTextBox.Text);
        }

        private string GetLatestVersion()
        {
            try
            {
                string latestUrl =
                    $"https://api.mullak99.co.uk/FAES/IsUpdate.php?app=faes_gui&branch={Program.programManager.GetBranch()}&showver=true&version={ConvertVersionToNonFormatted(Program.GetVersion())}";

                WebClient client = new WebClient();
                byte[] html = client.DownloadData(latestUrl);
                UTF8Encoding utf = new UTF8Encoding();
                if (string.IsNullOrEmpty(utf.GetString(html)) || utf.GetString(html) == "null")
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
