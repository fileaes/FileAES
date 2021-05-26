using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Principal;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;
using System.Threading;

namespace FAES_GUI.MenuPanels
{
    public partial class aboutPanel : UserControl
    {
        private UpdateStatus _appUpdateStatus;
        private string _latestVersion, _lastCheckedBranch;
        private bool _updateThreadRunning = false;
        private bool _updateUI = false;
        private bool _isUpdate = false;

        private Action _onIsUpdateAction;

        public aboutPanel()
        {
            InitializeComponent();

            miscVersionLabel.Text = String.Format("FAES Version: {0}\n\rSSM Version: {1}", ConvertVersionToFormatted(FAES.FileAES_Utilities.GetVersion()), ConvertVersionToFormatted(SimpleSettingsManager.SSM.GetVersion()));
            GetCurrentVersion();
        }
        public enum UpdateStatus
        {
            ServerError,
            AppOutdated,
            AppLatest,
            AppNewer
        };

        public bool IsUpdate()
        {
            return _isUpdate;
        }

        private void GetCurrentVersion()
        {
            currentVerLabel.Text = Program.GetVersion();
        }

        private void UpdateUI()
        {
            if (_updateThreadRunning)
            {
                checkForUpdateButton.Enabled = false;
                updateButton.Visible = false;
                ignoreUpdatesButton.Visible = false;
                forceUpdateButton.Visible = false;
                reinstallCurrentButton.Visible = false;
                reinstallCurrentButton.Enabled = true;
                updateDescLabel.Text = "Checking for updates...";
                latestVerLabel.Text = "Checking...";
            }
            else
            {
                string latestVersion = ConvertVersionToFormatted(_latestVersion);

                if (_appUpdateStatus == UpdateStatus.AppLatest)
                {
                    checkForUpdateButton.Enabled = true;
                    updateButton.Visible = false;
                    ignoreUpdatesButton.Visible = false;
                    forceUpdateButton.Visible = true;
                    reinstallCurrentButton.Visible = true;
                    reinstallCurrentButton.Enabled = true;
                    updateDescLabel.Text = "You are on the latest version!";
                    latestVerLabel.Text = latestVersion;
                    _isUpdate = false;

                    Logging.Log(String.Format("FAES_GUI(AboutPanel): UpdateUI set to AppLatest."), Severity.DEBUG);
                }
                else if (_appUpdateStatus == UpdateStatus.AppNewer)
                {
                    checkForUpdateButton.Enabled = true;
                    updateButton.Visible = false;
                    ignoreUpdatesButton.Visible = false;
                    forceUpdateButton.Visible = true;
                    reinstallCurrentButton.Visible = true;
                    reinstallCurrentButton.Enabled = false;
                    updateDescLabel.Text = "You are on a private build.";
                    latestVerLabel.Text = latestVersion;
                    _isUpdate = false;

                    Logging.Log(String.Format("FAES_GUI(AboutPanel): UpdateUI set to AppNewer."), Severity.DEBUG);
                }
                else if (_appUpdateStatus == UpdateStatus.ServerError)
                {
                    checkForUpdateButton.Enabled = true;
                    updateButton.Visible = false;
                    ignoreUpdatesButton.Visible = false;
                    forceUpdateButton.Visible = false;
                    reinstallCurrentButton.Visible = false;
                    reinstallCurrentButton.Enabled = false;
                    updateDescLabel.Text = "Unable to connect to the update server.";
                    latestVerLabel.Text = "SERVER ERROR!";
                    _isUpdate = false;

                    Logging.Log(String.Format("FAES_GUI(AboutPanel): UpdateUI set to ServerError."), Severity.DEBUG);
                }
                else if (_appUpdateStatus == UpdateStatus.AppOutdated)
                {
                    checkForUpdateButton.Enabled = true;
                    updateButton.Visible = true;
                    ignoreUpdatesButton.Visible = true;
                    forceUpdateButton.Visible = true;
                    reinstallCurrentButton.Visible = true;
                    reinstallCurrentButton.Enabled = true;
                    updateDescLabel.Text = "An update is available!";
                    latestVerLabel.Text = latestVersion;
                    _isUpdate = true;

                    Logging.Log(String.Format("FAES_GUI(AboutPanel): UpdateUI set to AppOutdated."), Severity.DEBUG);
                }
                GetCurrentVersion();
                _updateUI = false;
            }
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

        private bool DoesVersionExist(string version, string branch)
        {
            try
            {
                string latestUrl = String.Format("https://api.mullak99.co.uk/FAES/DoesVersionExist.php?app=faes_gui&branch={0}&version={1}", branch, version);

                WebClient client = new WebClient();
                byte[] html = client.DownloadData(latestUrl);
                UTF8Encoding utf = new UTF8Encoding();
                string result = utf.GetString(html);
                if (String.IsNullOrEmpty(result) || result == "null")
                    return false;
                else if (result == "VersionExists")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CheckForUpdate()
        {
            try
            {
                if (!_updateThreadRunning)
                {
                    _updateThreadRunning = true;

                    Thread threaddedUpdateCheck = new Thread(() =>
                    {
                        try
                        {
                            string updateVersion;

                            Logging.Log(String.Format("Checking for update..."), Severity.DEBUG);
                            UpdateStatus updateInfo = GetUpdateStatus(out updateVersion);

                            if (updateVersion != "v0.0.0")
                                Logging.Log(String.Format("Latest FAES_GUI version: {0}", updateVersion), Severity.DEBUG);
                            else
                                Logging.Log(String.Format("Update check failed!"), Severity.WARN);

                            _appUpdateStatus = updateInfo;
                            _latestVersion = updateVersion;
                            _lastCheckedBranch = Program.programManager.GetBranch();
                            _updateUI = true;
                            _updateThreadRunning = false;

                            if (_appUpdateStatus == UpdateStatus.AppOutdated && !Program.programManager.GetSkipUpdates()) DoIsUpdateAction();
                        }
                        catch { } // Hacky solution to stop VS2019 crashing when viewing some forms...
                    });
                    threaddedUpdateCheck.Start();
                }
            }
            catch { } // Hacky solution to stop VS2019 crashing when viewing some forms...
        }

        public bool IsUpdateCheckRunning()
        {
            return _updateThreadRunning;
        }

        public void SetIsUpdateAction(Action action)
        {
            _onIsUpdateAction = action;
        }

        private void DoIsUpdateAction()
        {
            if (_onIsUpdateAction != null)
            {
                this.BeginInvoke(new MethodInvoker(_onIsUpdateAction));
            }
        }

        private UpdateStatus GetUpdateStatus(out string updateVersion)
        {
            try
            {
                string latestVer = GetLatestVersion();
                string currentVer = ConvertVersionToNonFormatted(Program.GetVersion());
                updateVersion = latestVer;

                if (latestVer == currentVer)
                {
                    return UpdateStatus.AppLatest;
                }
                else if (latestVer != "v0.0.0" && CheckServerConnection())
                {
                    string compareVersions = String.Format("https://api.mullak99.co.uk/FAES/CompareVersions.php?app=faes_gui&branch={0}&version1={1}&version2={2}", "dev", currentVer, latestVer);

                    WebClient client = new WebClient();
                    byte[] html = client.DownloadData(compareVersions);
                    UTF8Encoding utf = new UTF8Encoding();
                    string result = utf.GetString(html).ToLower();

                    if (String.IsNullOrEmpty(result) || result == "null")
                        return UpdateStatus.ServerError;
                    else if (result.Contains("not exist in the database!") || result == "version1 is newer than version2")
                        return UpdateStatus.AppNewer;
                    else if (result == "version1 is older than version2")
                        return UpdateStatus.AppOutdated;
                    else if (result == "version1 is equal to version2")
                        return UpdateStatus.AppLatest;
                    else
                        return UpdateStatus.ServerError;
                }
                else
                {
                    return UpdateStatus.ServerError;
                }
            }
            catch
            {
                updateVersion = "v0.0.0";
                return UpdateStatus.ServerError;
            }
        }

        public static bool CheckServerConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("https://api.mullak99.co.uk/"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static void UpdateSelf(bool doCleanUpdate = false, string version = "latest")
        {
            string installDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:", "").TrimStart(':', '/', '\\');

            if (CheckServerConnection())
                try
                {
                    if (File.Exists(Path.Combine(installDir, "FAES-Updater.exe")))
                        File.Delete(Path.Combine(installDir, "FAES-Updater.exe"));
                    if (File.Exists(Path.Combine(installDir, "updater.pack")))
                        File.Delete(Path.Combine(installDir, "updater.pack"));

                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(new Uri(String.Format("https://api.mullak99.co.uk/FAES/GetDownload.php?app=faes_updater&ver=latest&branch={0}&redirect=true", Program.programManager.GetBranch())), Path.Combine(installDir, "updater.pack"));
                    ZipFile.ExtractToDirectory(Path.Combine(installDir, "updater.pack"), installDir);
                    File.Delete(Path.Combine(installDir, "updater.pack"));
                    Thread.Sleep(100);

                    string args = "";
                    if (doCleanUpdate) args += "--pure ";
                    if (Program.programManager.GetFullInstall())
                    {
                        args += "--full ";
                        args += string.Join(" ", Program.DumpInstallerOptions());
                        args += " ";
                    }
                    if (Program.programManager.GetDevMode()) args += "--verbose ";
                    else args += "--silent ";
                    args += "--branch " + Program.programManager.GetBranch() + " ";
                    args += "--tool faes_gui ";
                    args += "--version " + version + " ";
                    args += "--delay 10 ";
                    args += "--run ";
                    Process.Start(Path.Combine(installDir, "FAES-Updater.exe"), args);

                    Environment.Exit(0);
                }
                catch (Exception e)
                {
                    if (e is UnauthorizedAccessException || e is WebException)
                    {
                        if (!IsRunAsAdmin())
                        {
                            if (MessageBox.Show("You are not running FileAES as an admin, by doing this you cannot update the application in admin protected directories.\n\nDo you want to launch as admin?", "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                RunAsAdmin();
                        }
                        else MessageBox.Show(String.Format("An unexpected error occurred running the updater!\nInstall Directory:{0}\n\n{1}", installDir, e), "Error", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show(String.Format("An unexpected error occurred running the updater!\nInstall Directory:{0}\n\n{1}", installDir, e), "Error", MessageBoxButtons.OK);
                        if (File.Exists(Path.Combine(installDir, "FAES-Updater.exe")))
                            File.Delete(Path.Combine(installDir, "FAES-Updater.exe"));
                        if (File.Exists(Path.Combine(installDir, "updater.pack")))
                            File.Delete(Path.Combine(installDir, "updater.pack"));
                    }
                }
            else
            {
                if (File.Exists(Path.Combine(installDir, "FAES-Updater.exe")))
                    File.Delete(Path.Combine(installDir, "FAES-Updater.exe"));
                if (File.Exists(Path.Combine(installDir, "updater.pack")))
                    File.Delete(Path.Combine(installDir, "updater.pack"));
            }
        }

        private string ConvertVersionToFormatted(string nonFormattedVersion)
        {
            string[] versionSplit = nonFormattedVersion.Replace("_", " ").Split('-');
            string formattedVersion;

            if (versionSplit.Length > 0)
            {
                formattedVersion = versionSplit[0];

                if (versionSplit.Length > 1)
                {
                    if (versionSplit[1].ToUpper()[0] == 'B')
                    {
                        string betaTag = versionSplit[1].ToUpper().Replace("BETA", "").Replace("B", "");
                        formattedVersion += String.Format(" (BETA {0}", betaTag.Replace(" ", ""));
                    }
                    else if (versionSplit[1].ToUpper()[0] == 'D')
                    {
                        string devTag = versionSplit[1].ToUpper().Replace("DEV", "").Replace("D", "");
                        formattedVersion += String.Format(" (DEV{0}", devTag);
                    }
                    else if (versionSplit[1].ToUpper()[0] == 'R')
                    {
                        string rcTag = versionSplit[1].ToUpper().Replace("RC", "").Replace("R", "");
                        formattedVersion += String.Format(" (RC {0}", rcTag.Replace(" ", ""));
                    }
                    else
                    {
                        formattedVersion += String.Format(" ({0}", versionSplit[1].ToUpper());
                    }
                    if (versionSplit.Length > 2)
                    {
                        for (int i = 2; i < versionSplit.Length; i++)
                        {
                            formattedVersion += "-";
                            formattedVersion += versionSplit[i];
                        }
                    }
                    formattedVersion += ")";
                }
            }
            else formattedVersion = nonFormattedVersion;

            Logging.Log(String.Format("ToFormatted: Converted '{0}' to '{1}'.", nonFormattedVersion, formattedVersion), Severity.DEBUG);
            return formattedVersion;
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
                            nonFormattedVersion += "-";
                            nonFormattedVersion += versionSplit[i].ToUpper();
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

            Logging.Log(String.Format("ToNonFormatted: Converted '{0}' to '{1}'.", formattedVersion, nonFormattedVersion), Severity.DEBUG);
            return nonFormattedVersion;
        }

        internal static bool IsRunAsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void RunAsAdmin()
        {
            if (!IsRunAsAdmin())
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Application.ExecutablePath;
                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                }
                catch
                {
                    return;
                }
                Environment.Exit(0);
            }
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_updateThreadRunning || _updateUI) UpdateUI();
                if (Program.programManager.GetBranch() != _lastCheckedBranch) CheckForUpdate();
            }
            catch { } // Hacky solution to stop VS2019 crashing when viewing some forms...
        }

        private void UpdatePanel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, updatePanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void CheckForUpdateButton_Click(object sender, EventArgs e)
        {
            Logging.Log(String.Format("FAES_GUI(AboutPanel): CheckForUpdateButton clicked."), Severity.DEBUG);
            CheckForUpdate();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Logging.Log(String.Format("FAES_GUI(AboutPanel): UpdateButton clicked."), Severity.DEBUG);
            Logging.Log(String.Format("Updating to the latest FAES_GUI version..."), Severity.DEBUG);
            UpdateSelf(!Program.programManager.GetFullInstall(), "latest");
        }

        private void ForceUpdateButton_Click(object sender, EventArgs e)
        {
            Logging.Log(String.Format("FAES_GUI(AboutPanel): ForceUpdateButton clicked."), Severity.DEBUG);
            Logging.Log(String.Format("Forcing the download and installation of the latest FAES_GUI version..."), Severity.DEBUG);
            UpdateSelf(!Program.programManager.GetFullInstall(), "latest");
        }

        private void ReinstallCurrentButton_Click(object sender, EventArgs e)
        {
            Logging.Log(String.Format("FAES_GUI(AboutPanel): ReinstallCurrentButton clicked."), Severity.DEBUG);
            string version = ConvertVersionToNonFormatted(Program.GetVersion());
            string branch = Program.programManager.GetBranch();

            if (DoesVersionExist(version, branch))
            {
                Logging.Log(String.Format("Reinstall of version '{0}' on branch '{1}' started...", version, branch), Severity.DEBUG);
                UpdateSelf(!Program.programManager.GetFullInstall(), version);
            }
            else
            {
                Logging.Log(String.Format("Version '{0}' could not be found for branch '{1}'.", version, branch), Severity.WARN);
                warnLabel.Text = "Unable to find the current version on the server! Aborting reinstall.";
            }
        }

        private void IgnoreUpdatesButton_Click(object sender, EventArgs e)
        {
            Logging.Log(String.Format("FAES_GUI(AboutPanel): IgnoreUpdatesButton clicked."), Severity.DEBUG);
            Program.programManager.SetSkipUpdates(true);
        }
    }
}
