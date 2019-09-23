using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FAES_GUI.MenuPanels
{
    public partial class settingsPanel : UserControl
    {
        private int _cryptoBuffer, _branchIndex;
        private string _logPath, _branch;
        private bool _logToFile, _devMode, _ignoreUpdates;

        public settingsPanel()
        {
            InitializeComponent();

            settingsScrollPanel.HorizontalScroll.Maximum = 0;
            settingsScrollPanel.AutoScroll = false;
            settingsScrollPanel.VerticalScroll.Visible = false;
            settingsScrollPanel.AutoScroll = true;

            branchSelection.AddItem("Stable Releases");
            branchSelection.AddItem("Beta Releases");
            branchSelection.AddItem("Dev Releases");

            LoadSettings();
            RevertSettings();
        }

        public void RevertSettings()
        {
            cryptoStreamSetting.Value = _cryptoBuffer;
            logPathRootSetting.Value = _logPath;
            logToFileSetting.Toggled = _logToFile;
            developerSetting.Toggled = _devMode;
            ignoreUpdatesSetting.Toggled = _ignoreUpdates;

            if (_branch == "dev") branchSelection.SetSelectedIndex(2);
            else if (_branch == "beta") branchSelection.SetSelectedIndex(1);
            else branchSelection.SetSelectedIndex(0);

            _branchIndex = branchSelection.GetSelectedIndex();
        }

        public void LoadSettings()
        {
            try
            {
                _cryptoBuffer = Convert.ToInt32(Program.programManager.GetCryptoStreamBufferSize());
                _logPath = Program.programManager.GetLogPath().Replace("{default}", "").Replace("{d}", "");
                _logToFile = Program.programManager.GetLogToFile();
                _devMode = Program.programManager.GetDevMode();
                _branch = Program.programManager.GetBranch();
                _ignoreUpdates = Program.programManager.GetSkipUpdates();

                cryptoStreamSetting.Value = _cryptoBuffer;
                logPathRootSetting.Value = _logPath;
                logToFileSetting.Toggled = _logToFile;
                developerSetting.Toggled = _devMode;
                ignoreUpdatesSetting.Toggled = _ignoreUpdates;

                if (_branch == "dev") branchSelection.SetSelectedIndex(2);
                else if (_branch == "beta") branchSelection.SetSelectedIndex(1);
                else branchSelection.SetSelectedIndex(0);

                _branchIndex = branchSelection.GetSelectedIndex();
            }
            catch { }
        }

        private void SaveSettings()
        {
            Program.programManager.SetDevMode(developerSetting.Toggled);
            Program.programManager.SetCryptoStreamBufferSize(Convert.ToUInt32(cryptoStreamSetting.Value));
            Program.programManager.SetLogToFile(logToFileSetting.Toggled);
            Program.programManager.SetLogPath(Path.Combine((logPathRootSetting.Value).Replace("{default}", "").Replace("{d}", ""), "{default}"));
            Program.programManager.SetSkipUpdates(ignoreUpdatesSetting.Toggled);

            if (branchSelection.GetSelectedIndex() == 2) Program.programManager.SetBranch("dev");
            else if (branchSelection.GetSelectedIndex() == 1) Program.programManager.SetBranch("beta");
            else Program.programManager.SetBranch("stable");
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            if (cryptoStreamSetting.Value != _cryptoBuffer || logPathRootSetting.Value != _logPath || logToFileSetting.Toggled != _logToFile ||
                developerSetting.Toggled != _devMode || ignoreUpdatesSetting.Toggled != _ignoreUpdates || branchSelection.GetSelectedIndex() != _branchIndex)
            {
                saveSettings.Enabled = true;
                cancelButton.Enabled = true;
            }
            else
            {
                saveSettings.Enabled = false;
                cancelButton.Enabled = false;
            }
        }

        private void buttonPanel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, buttonPanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
            LoadSettings();
            RevertSettings();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Program.programManager.ResetAllSettings();

            LoadSettings();
            RevertSettings();
            SaveSettings();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            RevertSettings();
        }
    }
}
