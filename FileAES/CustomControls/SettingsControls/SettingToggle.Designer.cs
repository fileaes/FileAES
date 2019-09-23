namespace FAES_GUI.CustomControls
{
    partial class SettingToggle
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.settingDesc = new System.Windows.Forms.Label();
            this.settingHeader = new System.Windows.Forms.Label();
            this.toggleButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.Controls.Add(this.settingDesc);
            this.mainPanel.Controls.Add(this.settingHeader);
            this.mainPanel.Controls.Add(this.toggleButton);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(400, 100);
            this.mainPanel.TabIndex = 1;
            // 
            // settingDesc
            // 
            this.settingDesc.BackColor = System.Drawing.Color.Transparent;
            this.settingDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingDesc.ForeColor = System.Drawing.Color.White;
            this.settingDesc.Location = new System.Drawing.Point(3, 28);
            this.settingDesc.Name = "settingDesc";
            this.settingDesc.Size = new System.Drawing.Size(390, 43);
            this.settingDesc.TabIndex = 9;
            this.settingDesc.Text = "Setting Description";
            this.settingDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // settingHeader
            // 
            this.settingHeader.BackColor = System.Drawing.Color.Transparent;
            this.settingHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingHeader.ForeColor = System.Drawing.Color.White;
            this.settingHeader.Location = new System.Drawing.Point(3, 0);
            this.settingHeader.Name = "settingHeader";
            this.settingHeader.Size = new System.Drawing.Size(393, 23);
            this.settingHeader.TabIndex = 8;
            this.settingHeader.Text = "Setting Header";
            this.settingHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // toggleButton
            // 
            this.toggleButton.BackColor = System.Drawing.Color.ForestGreen;
            this.toggleButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.toggleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleButton.ForeColor = System.Drawing.Color.White;
            this.toggleButton.Location = new System.Drawing.Point(3, 72);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(393, 25);
            this.toggleButton.TabIndex = 7;
            this.toggleButton.Text = "Enabled";
            this.toggleButton.UseVisualStyleBackColor = false;
            this.toggleButton.Click += new System.EventHandler(this.toggleButton_Click);
            // 
            // SettingToggle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mainPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SettingToggle";
            this.Size = new System.Drawing.Size(400, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingToggle_Paint);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label settingDesc;
        private System.Windows.Forms.Label settingHeader;
        private System.Windows.Forms.Button toggleButton;
    }
}
