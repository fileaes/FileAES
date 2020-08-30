namespace FAES_GUI.MenuPanels
{
    partial class settingsPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settingsPanel));
            this.settingsScrollPanel = new System.Windows.Forms.Panel();
            this.ignoreUpdatesSetting = new FAES_GUI.CustomControls.SettingToggle();
            this.branchSelection = new FAES_GUI.CustomControls.SettingDropDown();
            this.developerSetting = new FAES_GUI.CustomControls.SettingToggle();
            this.logPathRootSetting = new FAES_GUI.CustomControls.SettingTextInput();
            this.logToFileSetting = new FAES_GUI.CustomControls.SettingToggle();
            this.cryptoStreamSetting = new FAES_GUI.CustomControls.SettingIncrementBox();
            this.saveSettings = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.runtime = new System.Windows.Forms.Timer(this.components);
            this.settingsScrollPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsScrollPanel
            // 
            this.settingsScrollPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.settingsScrollPanel.Controls.Add(this.ignoreUpdatesSetting);
            this.settingsScrollPanel.Controls.Add(this.branchSelection);
            this.settingsScrollPanel.Controls.Add(this.developerSetting);
            this.settingsScrollPanel.Controls.Add(this.logPathRootSetting);
            this.settingsScrollPanel.Controls.Add(this.logToFileSetting);
            this.settingsScrollPanel.Controls.Add(this.cryptoStreamSetting);
            this.settingsScrollPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsScrollPanel.Name = "settingsScrollPanel";
            this.settingsScrollPanel.Size = new System.Drawing.Size(414, 316);
            this.settingsScrollPanel.TabIndex = 0;
            // 
            // ignoreUpdatesSetting
            // 
            this.ignoreUpdatesSetting.BackColor = System.Drawing.Color.Transparent;
            this.ignoreUpdatesSetting.DescriptionText = "Toggles whether the program automatically opens the \'About\' menu when an update i" +
    "s available for download.";
            this.ignoreUpdatesSetting.ForeColor = System.Drawing.Color.White;
            this.ignoreUpdatesSetting.HeaderText = "Ignore Updates";
            this.ignoreUpdatesSetting.Location = new System.Drawing.Point(-1, 495);
            this.ignoreUpdatesSetting.Name = "ignoreUpdatesSetting";
            this.ignoreUpdatesSetting.Size = new System.Drawing.Size(400, 100);
            this.ignoreUpdatesSetting.TabIndex = 6;
            this.ignoreUpdatesSetting.Toggled = true;
            // 
            // branchSelection
            // 
            this.branchSelection.BackColor = System.Drawing.Color.Transparent;
            this.branchSelection.DescriptionText = "Determines the branch used to determine if an update is availible for FileAES. Un" +
    "less you knowm what you are doing, it is recommended to keep on the Stable branc" +
    "h.";
            this.branchSelection.ForeColor = System.Drawing.Color.White;
            this.branchSelection.HeaderText = "FileAES Branch";
            this.branchSelection.Location = new System.Drawing.Point(-1, 99);
            this.branchSelection.Name = "branchSelection";
            this.branchSelection.Size = new System.Drawing.Size(400, 100);
            this.branchSelection.TabIndex = 5;
            // 
            // developerSetting
            // 
            this.developerSetting.BackColor = System.Drawing.Color.Transparent;
            this.developerSetting.DescriptionText = resources.GetString("developerSetting.DescriptionText");
            this.developerSetting.ForeColor = System.Drawing.Color.White;
            this.developerSetting.HeaderText = "Developer Mode";
            this.developerSetting.Location = new System.Drawing.Point(-1, 0);
            this.developerSetting.Name = "developerSetting";
            this.developerSetting.Size = new System.Drawing.Size(400, 100);
            this.developerSetting.TabIndex = 4;
            this.developerSetting.Toggled = true;
            // 
            // logPathRootSetting
            // 
            this.logPathRootSetting.BackColor = System.Drawing.Color.Transparent;
            this.logPathRootSetting.DescriptionText = resources.GetString("logPathRootSetting.DescriptionText");
            this.logPathRootSetting.ForeColor = System.Drawing.Color.White;
            this.logPathRootSetting.HeaderText = "Log File Path";
            this.logPathRootSetting.Location = new System.Drawing.Point(-1, 396);
            this.logPathRootSetting.Name = "logPathRootSetting";
            this.logPathRootSetting.Size = new System.Drawing.Size(400, 100);
            this.logPathRootSetting.TabIndex = 3;
            this.logPathRootSetting.Value = "";
            // 
            // logToFileSetting
            // 
            this.logToFileSetting.BackColor = System.Drawing.Color.Transparent;
            this.logToFileSetting.DescriptionText = "Toggles whether the program will automatically log directly to a file. This setti" +
    "ng works best with Developer Mode toggled.";
            this.logToFileSetting.Enabled = false;
            this.logToFileSetting.ForeColor = System.Drawing.Color.White;
            this.logToFileSetting.HeaderText = "Log to file";
            this.logToFileSetting.Location = new System.Drawing.Point(-1, 297);
            this.logToFileSetting.Name = "logToFileSetting";
            this.logToFileSetting.Size = new System.Drawing.Size(400, 100);
            this.logToFileSetting.TabIndex = 2;
            this.logToFileSetting.Toggled = true;
            // 
            // cryptoStreamSetting
            // 
            this.cryptoStreamSetting.BackColor = System.Drawing.Color.Transparent;
            this.cryptoStreamSetting.DescriptionText = "Changes the size of the CryptoStream buffer used when encrypting and decrypting f" +
    "iles using FAES. (Size is in bytes)";
            this.cryptoStreamSetting.ForeColor = System.Drawing.Color.White;
            this.cryptoStreamSetting.HeaderText = "CryptoStream Buffer";
            this.cryptoStreamSetting.Location = new System.Drawing.Point(-1, 198);
            this.cryptoStreamSetting.MinValue = 1024;
            this.cryptoStreamSetting.Name = "cryptoStreamSetting";
            this.cryptoStreamSetting.Size = new System.Drawing.Size(400, 100);
            this.cryptoStreamSetting.TabIndex = 1;
            this.cryptoStreamSetting.Tag = "";
            this.cryptoStreamSetting.Value = 1048576;
            // 
            // saveSettings
            // 
            this.saveSettings.BackColor = System.Drawing.Color.ForestGreen;
            this.saveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveSettings.ForeColor = System.Drawing.Color.White;
            this.saveSettings.Location = new System.Drawing.Point(240, 6);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(170, 32);
            this.saveSettings.TabIndex = 5;
            this.saveSettings.Text = "Save and Apply";
            this.saveSettings.UseVisualStyleBackColor = false;
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // resetButton
            // 
            this.resetButton.BackColor = System.Drawing.Color.Red;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.ForeColor = System.Drawing.Color.White;
            this.resetButton.Location = new System.Drawing.Point(95, 6);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(139, 32);
            this.resetButton.TabIndex = 6;
            this.resetButton.Text = "Reset to Default";
            this.resetButton.UseVisualStyleBackColor = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.resetButton);
            this.buttonPanel.Controls.Add(this.saveSettings);
            this.buttonPanel.Location = new System.Drawing.Point(0, 316);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(414, 41);
            this.buttonPanel.TabIndex = 7;
            this.buttonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonPanel_Paint);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.DarkOrange;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(4, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(85, 32);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // runtime
            // 
            this.runtime.Enabled = true;
            this.runtime.Tick += new System.EventHandler(this.Runtime_Tick);
            // 
            // settingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.settingsScrollPanel);
            this.Name = "settingsPanel";
            this.Size = new System.Drawing.Size(414, 357);
            this.settingsScrollPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel settingsScrollPanel;
        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Button resetButton;
        private CustomControls.SettingIncrementBox cryptoStreamSetting;
        private CustomControls.SettingToggle logToFileSetting;
        private CustomControls.SettingTextInput logPathRootSetting;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Timer runtime;
        private CustomControls.SettingToggle developerSetting;
        private CustomControls.SettingDropDown branchSelection;
        private CustomControls.SettingToggle ignoreUpdatesSetting;
    }
}
