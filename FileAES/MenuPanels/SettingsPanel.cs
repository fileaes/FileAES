using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FAES_GUI.MenuPanels
{
    public partial class settingsPanel : UserControl
    {
        private int _cryptoBuffer;
        private string _logPath;
        private bool _logToFile, _devMode;

        public settingsPanel()
        {
            InitializeComponent();

            settingsScrollPanel.HorizontalScroll.Maximum = 0;
            settingsScrollPanel.AutoScroll = false;
            settingsScrollPanel.VerticalScroll.Visible = false;
            settingsScrollPanel.AutoScroll = true;

            versionLabel.Text = String.Format("FileAES Version: {0}\n\rFAES Version: {1}\n\rSSM Version: {2}", Program.GetVersion(), FAES.FileAES_Utilities.GetVersion(), SimpleSettingsManager.SSM.GetVersion());

            LoadSettings();
            RevertSettings();
        }

        public void RevertSettings()
        {
            cryptoStreamSetting.Value = _cryptoBuffer;
            logPathRootSetting.Value = _logPath;
            logToFileSetting.Toggled = _logToFile;
            developerSetting.Toggled = _devMode;
        }

        public void LoadSettings()
        {
            _cryptoBuffer = Convert.ToInt32(Program.programManager.GetCryptoStreamBufferSize());
            _logPath = Program.programManager.GetLogPath().Replace("{default}", "").Replace("{d}", "");
            _logToFile = Program.programManager.GetLogToFile();
            _devMode = Program.programManager.GetDevMode();

            cryptoStreamSetting.Value = _cryptoBuffer;
            logPathRootSetting.Value = _logPath;
            logToFileSetting.Toggled = _logToFile;
            developerSetting.Toggled = _devMode;
        }

        private void SaveSettings()
        {
            Program.programManager.SetDevMode(developerSetting.Toggled);
            Program.programManager.SetCryptoStreamBufferSize(Convert.ToUInt32(cryptoStreamSetting.Value));
            Program.programManager.SetLogToFile(logToFileSetting.Toggled);
            Program.programManager.SetLogPath(Path.Combine((logPathRootSetting.Value).Replace("{default}", "").Replace("{d}", ""), "{default}"));
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            if (cryptoStreamSetting.Value != _cryptoBuffer || logPathRootSetting.Value != _logPath || logToFileSetting.Toggled != _logToFile || developerSetting.Toggled != _devMode)
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
