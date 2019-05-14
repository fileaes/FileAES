namespace FAES_GUI.MenuPanels
{
    partial class aboutPanel
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.updatePanel = new System.Windows.Forms.Panel();
            this.updateDescLabel = new System.Windows.Forms.Label();
            this.currentVerLabel = new System.Windows.Forms.Label();
            this.latestVerLabel = new System.Windows.Forms.Label();
            this.miscVersionLabel = new System.Windows.Forms.Label();
            this.checkForUpdateButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.ignoreUpdatesButton = new System.Windows.Forms.Button();
            this.forceUpdateButton = new System.Windows.Forms.Button();
            this.reinstallCurrentButton = new System.Windows.Forms.Button();
            this.Runtime = new System.Windows.Forms.Timer(this.components);
            this.warnLabel = new System.Windows.Forms.Label();
            this.updatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // versionLabel
            // 
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(29, 48);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(150, 82);
            this.versionLabel.TabIndex = 1;
            this.versionLabel.Text = "Current Version:\r\n\r\nLatest Version:";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // updatePanel
            // 
            this.updatePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.updatePanel.Controls.Add(this.warnLabel);
            this.updatePanel.Controls.Add(this.reinstallCurrentButton);
            this.updatePanel.Controls.Add(this.forceUpdateButton);
            this.updatePanel.Controls.Add(this.ignoreUpdatesButton);
            this.updatePanel.Controls.Add(this.updateButton);
            this.updatePanel.Controls.Add(this.checkForUpdateButton);
            this.updatePanel.Controls.Add(this.latestVerLabel);
            this.updatePanel.Controls.Add(this.currentVerLabel);
            this.updatePanel.Controls.Add(this.updateDescLabel);
            this.updatePanel.Controls.Add(this.versionLabel);
            this.updatePanel.Location = new System.Drawing.Point(-1, 61);
            this.updatePanel.Name = "updatePanel";
            this.updatePanel.Size = new System.Drawing.Size(416, 303);
            this.updatePanel.TabIndex = 2;
            this.updatePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdatePanel_Paint);
            // 
            // updateDescLabel
            // 
            this.updateDescLabel.BackColor = System.Drawing.Color.Transparent;
            this.updateDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateDescLabel.ForeColor = System.Drawing.Color.White;
            this.updateDescLabel.Location = new System.Drawing.Point(5, 4);
            this.updateDescLabel.Name = "updateDescLabel";
            this.updateDescLabel.Size = new System.Drawing.Size(407, 44);
            this.updateDescLabel.TabIndex = 2;
            this.updateDescLabel.Text = "An update is available...";
            this.updateDescLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // currentVerLabel
            // 
            this.currentVerLabel.BackColor = System.Drawing.Color.Transparent;
            this.currentVerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentVerLabel.ForeColor = System.Drawing.Color.White;
            this.currentVerLabel.Location = new System.Drawing.Point(179, 50);
            this.currentVerLabel.Name = "currentVerLabel";
            this.currentVerLabel.Size = new System.Drawing.Size(208, 40);
            this.currentVerLabel.TabIndex = 3;
            this.currentVerLabel.Text = "v2.0.0";
            // 
            // latestVerLabel
            // 
            this.latestVerLabel.BackColor = System.Drawing.Color.Transparent;
            this.latestVerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latestVerLabel.ForeColor = System.Drawing.Color.White;
            this.latestVerLabel.Location = new System.Drawing.Point(179, 90);
            this.latestVerLabel.Name = "latestVerLabel";
            this.latestVerLabel.Size = new System.Drawing.Size(208, 40);
            this.latestVerLabel.TabIndex = 4;
            this.latestVerLabel.Text = "v2.0.0";
            // 
            // miscVersionLabel
            // 
            this.miscVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.miscVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miscVersionLabel.ForeColor = System.Drawing.Color.White;
            this.miscVersionLabel.Location = new System.Drawing.Point(4, 4);
            this.miscVersionLabel.Name = "miscVersionLabel";
            this.miscVersionLabel.Size = new System.Drawing.Size(407, 54);
            this.miscVersionLabel.TabIndex = 3;
            this.miscVersionLabel.Text = "FAES Version:\r\nSSM Version:";
            this.miscVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkForUpdateButton
            // 
            this.checkForUpdateButton.BackColor = System.Drawing.Color.DarkCyan;
            this.checkForUpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkForUpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForUpdateButton.ForeColor = System.Drawing.Color.White;
            this.checkForUpdateButton.Location = new System.Drawing.Point(4, 263);
            this.checkForUpdateButton.Name = "checkForUpdateButton";
            this.checkForUpdateButton.Size = new System.Drawing.Size(408, 30);
            this.checkForUpdateButton.TabIndex = 6;
            this.checkForUpdateButton.Text = "Check for Update";
            this.checkForUpdateButton.UseVisualStyleBackColor = false;
            this.checkForUpdateButton.Click += new System.EventHandler(this.CheckForUpdateButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.Color.ForestGreen;
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateButton.ForeColor = System.Drawing.Color.White;
            this.updateButton.Location = new System.Drawing.Point(4, 195);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(202, 30);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "Update Now";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ignoreUpdatesButton
            // 
            this.ignoreUpdatesButton.BackColor = System.Drawing.Color.Chocolate;
            this.ignoreUpdatesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ignoreUpdatesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ignoreUpdatesButton.ForeColor = System.Drawing.Color.White;
            this.ignoreUpdatesButton.Location = new System.Drawing.Point(210, 195);
            this.ignoreUpdatesButton.Name = "ignoreUpdatesButton";
            this.ignoreUpdatesButton.Size = new System.Drawing.Size(202, 30);
            this.ignoreUpdatesButton.TabIndex = 8;
            this.ignoreUpdatesButton.Text = "Ignore Updates";
            this.ignoreUpdatesButton.UseVisualStyleBackColor = false;
            this.ignoreUpdatesButton.Click += new System.EventHandler(this.IgnoreUpdatesButton_Click);
            // 
            // forceUpdateButton
            // 
            this.forceUpdateButton.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.forceUpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.forceUpdateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forceUpdateButton.ForeColor = System.Drawing.Color.White;
            this.forceUpdateButton.Location = new System.Drawing.Point(4, 229);
            this.forceUpdateButton.Name = "forceUpdateButton";
            this.forceUpdateButton.Size = new System.Drawing.Size(202, 30);
            this.forceUpdateButton.TabIndex = 9;
            this.forceUpdateButton.Text = "Force Update";
            this.forceUpdateButton.UseVisualStyleBackColor = false;
            this.forceUpdateButton.Click += new System.EventHandler(this.ForceUpdateButton_Click);
            // 
            // reinstallCurrentButton
            // 
            this.reinstallCurrentButton.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.reinstallCurrentButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reinstallCurrentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reinstallCurrentButton.ForeColor = System.Drawing.Color.White;
            this.reinstallCurrentButton.Location = new System.Drawing.Point(210, 229);
            this.reinstallCurrentButton.Name = "reinstallCurrentButton";
            this.reinstallCurrentButton.Size = new System.Drawing.Size(202, 30);
            this.reinstallCurrentButton.TabIndex = 10;
            this.reinstallCurrentButton.Text = "Reinstall";
            this.reinstallCurrentButton.UseVisualStyleBackColor = false;
            this.reinstallCurrentButton.Click += new System.EventHandler(this.ReinstallCurrentButton_Click);
            // 
            // Runtime
            // 
            this.Runtime.Enabled = true;
            this.Runtime.Tick += new System.EventHandler(this.Runtime_Tick);
            // 
            // warnLabel
            // 
            this.warnLabel.BackColor = System.Drawing.Color.Transparent;
            this.warnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warnLabel.ForeColor = System.Drawing.Color.White;
            this.warnLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.warnLabel.Location = new System.Drawing.Point(5, 142);
            this.warnLabel.Name = "warnLabel";
            this.warnLabel.Size = new System.Drawing.Size(407, 47);
            this.warnLabel.TabIndex = 11;
            this.warnLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // aboutPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.miscVersionLabel);
            this.Controls.Add(this.updatePanel);
            this.Name = "aboutPanel";
            this.Size = new System.Drawing.Size(414, 357);
            this.updatePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Panel updatePanel;
        private System.Windows.Forms.Label updateDescLabel;
        private System.Windows.Forms.Label latestVerLabel;
        private System.Windows.Forms.Label currentVerLabel;
        private System.Windows.Forms.Label miscVersionLabel;
        private System.Windows.Forms.Button forceUpdateButton;
        private System.Windows.Forms.Button ignoreUpdatesButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button checkForUpdateButton;
        private System.Windows.Forms.Button reinstallCurrentButton;
        private System.Windows.Forms.Timer Runtime;
        private System.Windows.Forms.Label warnLabel;
    }
}
