namespace FAES_GUI.MenuPanels
{
    partial class filePeakPanel
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
            this.fileInfoPanel = new System.Windows.Forms.Panel();
            this.fileInfoLabel = new System.Windows.Forms.Label();
            this.selectDecryptButton = new System.Windows.Forms.Button();
            this.passTextbox = new System.Windows.Forms.TextBox();
            this.passLabel = new System.Windows.Forms.Label();
            this.passHintLabel = new System.Windows.Forms.Label();
            this.passHintTextbox = new System.Windows.Forms.TextBox();
            this.decryptButton = new System.Windows.Forms.Button();
            this.statusInformation = new System.Windows.Forms.Label();
            this.openFileToDecrypt = new System.Windows.Forms.OpenFileDialog();
            this.decryptionTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new FAES_GUI.CustomControls.TextProgressBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fileContentsTextbox = new System.Windows.Forms.RichTextBox();
            this.fileInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileInfoPanel
            // 
            this.fileInfoPanel.BackColor = System.Drawing.Color.Gray;
            this.fileInfoPanel.Controls.Add(this.fileInfoLabel);
            this.fileInfoPanel.Location = new System.Drawing.Point(0, 40);
            this.fileInfoPanel.Name = "fileInfoPanel";
            this.fileInfoPanel.Size = new System.Drawing.Size(414, 34);
            this.fileInfoPanel.TabIndex = 0;
            // 
            // fileInfoLabel
            // 
            this.fileInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileInfoLabel.ForeColor = System.Drawing.Color.White;
            this.fileInfoLabel.Location = new System.Drawing.Point(3, 3);
            this.fileInfoLabel.Name = "fileInfoLabel";
            this.fileInfoLabel.Size = new System.Drawing.Size(408, 30);
            this.fileInfoLabel.TabIndex = 2;
            this.fileInfoLabel.Text = "PLACEHOLDER FILE NAME";
            this.fileInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectDecryptButton
            // 
            this.selectDecryptButton.AllowDrop = true;
            this.selectDecryptButton.BackColor = System.Drawing.Color.DarkBlue;
            this.selectDecryptButton.FlatAppearance.BorderSize = 0;
            this.selectDecryptButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.selectDecryptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectDecryptButton.ForeColor = System.Drawing.Color.White;
            this.selectDecryptButton.Location = new System.Drawing.Point(0, 0);
            this.selectDecryptButton.Name = "selectDecryptButton";
            this.selectDecryptButton.Size = new System.Drawing.Size(414, 39);
            this.selectDecryptButton.TabIndex = 1;
            this.selectDecryptButton.Text = "Select File/Folder";
            this.selectDecryptButton.UseVisualStyleBackColor = false;
            this.selectDecryptButton.Click += new System.EventHandler(this.selectDecryptButton_Click);
            this.selectDecryptButton.DragDrop += new System.Windows.Forms.DragEventHandler(this.selectDecryptButton_DragDrop);
            this.selectDecryptButton.DragEnter += new System.Windows.Forms.DragEventHandler(this.selectDecryptButton_DragEnter);
            // 
            // passTextbox
            // 
            this.passTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passTextbox.Enabled = false;
            this.passTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passTextbox.ForeColor = System.Drawing.Color.Black;
            this.passTextbox.Location = new System.Drawing.Point(137, 81);
            this.passTextbox.Name = "passTextbox";
            this.passTextbox.PasswordChar = '*';
            this.passTextbox.Size = new System.Drawing.Size(266, 24);
            this.passTextbox.TabIndex = 2;
            this.passTextbox.TextChanged += new System.EventHandler(this.combinedPassword_TextChanged);
            this.passTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.allTextbox_KeyDown);
            // 
            // passLabel
            // 
            this.passLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passLabel.ForeColor = System.Drawing.Color.White;
            this.passLabel.Location = new System.Drawing.Point(3, 83);
            this.passLabel.Name = "passLabel";
            this.passLabel.Size = new System.Drawing.Size(128, 23);
            this.passLabel.TabIndex = 3;
            this.passLabel.Text = "Password:";
            this.passLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // passHintLabel
            // 
            this.passHintLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passHintLabel.ForeColor = System.Drawing.Color.White;
            this.passHintLabel.Location = new System.Drawing.Point(7, 110);
            this.passHintLabel.Name = "passHintLabel";
            this.passHintLabel.Size = new System.Drawing.Size(124, 50);
            this.passHintLabel.TabIndex = 10;
            this.passHintLabel.Text = "Password Hint:";
            this.passHintLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // passHintTextbox
            // 
            this.passHintTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.passHintTextbox.Enabled = false;
            this.passHintTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passHintTextbox.ForeColor = System.Drawing.Color.Black;
            this.passHintTextbox.Location = new System.Drawing.Point(137, 111);
            this.passHintTextbox.MaxLength = 64;
            this.passHintTextbox.Multiline = true;
            this.passHintTextbox.Name = "passHintTextbox";
            this.passHintTextbox.ReadOnly = true;
            this.passHintTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.passHintTextbox.Size = new System.Drawing.Size(266, 49);
            this.passHintTextbox.TabIndex = 3;
            // 
            // decryptButton
            // 
            this.decryptButton.BackColor = System.Drawing.Color.DarkRed;
            this.decryptButton.Enabled = false;
            this.decryptButton.FlatAppearance.BorderSize = 0;
            this.decryptButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.decryptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decryptButton.ForeColor = System.Drawing.Color.White;
            this.decryptButton.Location = new System.Drawing.Point(3, 312);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(408, 42);
            this.decryptButton.TabIndex = 4;
            this.decryptButton.Text = "Peak";
            this.decryptButton.UseVisualStyleBackColor = false;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // statusInformation
            // 
            this.statusInformation.ForeColor = System.Drawing.Color.White;
            this.statusInformation.Location = new System.Drawing.Point(3, 255);
            this.statusInformation.Name = "statusInformation";
            this.statusInformation.Size = new System.Drawing.Size(408, 33);
            this.statusInformation.TabIndex = 13;
            this.statusInformation.Text = "Error: PLACEHOLDER ERROR";
            this.statusInformation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // openFileToDecrypt
            // 
            this.openFileToDecrypt.Filter = "FileAES Files|*.faes;*.mcrypt";
            this.openFileToDecrypt.Title = "Select a file to decrypt";
            // 
            // decryptionTimer
            // 
            this.decryptionTimer.Tick += new System.EventHandler(this.decryptionTimer_Tick);
            // 
            // progressBar
            // 
            this.progressBar.CustomText = "";
            this.progressBar.ForeColor = System.Drawing.Color.ForestGreen;
            this.progressBar.Location = new System.Drawing.Point(3, 289);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.Lime;
            this.progressBar.Size = new System.Drawing.Size(408, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 18;
            this.progressBar.TextColor = System.Drawing.Color.Black;
            this.progressBar.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.progressBar.VisualMode = FAES_GUI.CustomControls.ProgressBarDisplayMode.Percentage;
            // 
            // fileContentsTextbox
            // 
            this.fileContentsTextbox.Location = new System.Drawing.Point(3, 166);
            this.fileContentsTextbox.Name = "fileContentsTextbox";
            this.fileContentsTextbox.ReadOnly = true;
            this.fileContentsTextbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.fileContentsTextbox.Size = new System.Drawing.Size(400, 86);
            this.fileContentsTextbox.TabIndex = 19;
            this.fileContentsTextbox.Text = "";
            // 
            // filePeakPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.Controls.Add(this.fileContentsTextbox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusInformation);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.passHintTextbox);
            this.Controls.Add(this.passHintLabel);
            this.Controls.Add(this.passLabel);
            this.Controls.Add(this.passTextbox);
            this.Controls.Add(this.selectDecryptButton);
            this.Controls.Add(this.fileInfoPanel);
            this.Name = "filePeakPanel";
            this.Size = new System.Drawing.Size(414, 357);
            this.fileInfoPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel fileInfoPanel;
        private System.Windows.Forms.Label fileInfoLabel;
        private System.Windows.Forms.Button selectDecryptButton;
        private System.Windows.Forms.TextBox passTextbox;
        private System.Windows.Forms.Label passLabel;
        private System.Windows.Forms.Label passHintLabel;
        private System.Windows.Forms.TextBox passHintTextbox;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Label statusInformation;
        private System.Windows.Forms.OpenFileDialog openFileToDecrypt;
        private System.Windows.Forms.Timer decryptionTimer;
        private CustomControls.TextProgressBar progressBar;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.RichTextBox fileContentsTextbox;
    }
}
