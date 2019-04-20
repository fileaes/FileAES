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
        private bool _logToFile;

        public settingsPanel()
        {
            InitializeComponent();

            settingsScrollPanel.HorizontalScroll.Maximum = 0;
            settingsScrollPanel.AutoScroll = false;
            settingsScrollPanel.VerticalScroll.Visible = false;
            settingsScrollPanel.AutoScroll = true;

            versionLabel.Text = String.Format("FileAES Version: {0}\n\rFAES Version: {1}", Program.GetVersion(), FAES.FileAES_Utilities.GetVersion());

            LoadSettings();
            RevertSettings();
        }

        public void RevertSettings()
        {
            cryptoStreamSetting.Value = _cryptoBuffer;
            settingLogPathRoot.Value = _logPath;
            settingLogToFile.Toggled = _logToFile;
        }

        public void LoadSettings()
        {
            _cryptoBuffer = Convert.ToInt32(Program.settingsManager.GetCryptoStreamBufferSize());
            _logPath = Program.settingsManager.GetLogPath().Replace("{default}", "").Replace("{d}", "");
            _logToFile = Program.settingsManager.GetLogToFile();

            cryptoStreamSetting.Value = _cryptoBuffer;
            settingLogPathRoot.Value = _logPath;
            settingLogToFile.Toggled = _logToFile;
        }

        private void SaveSettings()
        {
            Program.settingsManager.SetCryptoStreamBufferSize(Convert.ToUInt32(cryptoStreamSetting.Value));
            Program.settingsManager.SetLogToFile(settingLogToFile.Toggled);
            Program.settingsManager.SetLogPath(Path.Combine((settingLogPathRoot.Value).Replace("{default}", "").Replace("{d}", ""), "{default}"));
        }

        private void Runtime_Tick(object sender, EventArgs e)
        {
            if (cryptoStreamSetting.Value != _cryptoBuffer || settingLogPathRoot.Value != _logPath || settingLogToFile.Toggled != _logToFile)
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
            Program.settingsManager.ResetCryptoStreamBufferSize();
            Program.settingsManager.ResetLogToFile();
            Program.settingsManager.ResetLogPath();

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
