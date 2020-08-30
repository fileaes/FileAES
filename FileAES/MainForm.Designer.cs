namespace FAES_GUI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.titleBar = new System.Windows.Forms.Panel();
            this.minButton = new System.Windows.Forms.Button();
            this.titleBarLogo = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.quitButton = new System.Windows.Forms.Button();
            this.sidePanel = new System.Windows.Forms.Panel();
            this.aboutMenuButton = new FAES_GUI.CustomControls.SubMenuButton();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.settingsMenuButton = new FAES_GUI.CustomControls.SubMenuButton();
            this.decryptMenuButton = new FAES_GUI.CustomControls.SubMenuButton();
            this.encryptMenuButton = new FAES_GUI.CustomControls.SubMenuButton();
            this.autoSelectMenuButton = new FAES_GUI.CustomControls.SubMenuButton();
            this.slowToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.autoDetect = new System.Windows.Forms.Label();
            this.openFileDialogAutoSelect = new System.Windows.Forms.OpenFileDialog();
            this.decryptPanel = new FAES_GUI.MenuPanels.decryptPanel();
            this.encryptPanel = new FAES_GUI.MenuPanels.encryptPanel();
            this.aboutPanel = new FAES_GUI.MenuPanels.aboutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.longVersionLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new FAES_GUI.MenuPanels.settingsPanel();
            this.titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarLogo)).BeginInit();
            this.sidePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.titleBar.Controls.Add(this.minButton);
            this.titleBar.Controls.Add(this.titleBarLogo);
            this.titleBar.Controls.Add(this.titleLabel);
            this.titleBar.Controls.Add(this.quitButton);
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(564, 25);
            this.titleBar.TabIndex = 0;
            this.titleBar.Paint += new System.Windows.Forms.PaintEventHandler(this.titleBar_Paint);
            this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            // 
            // minButton
            // 
            this.minButton.BackColor = System.Drawing.Color.Transparent;
            this.minButton.FlatAppearance.BorderSize = 0;
            this.minButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minButton.ForeColor = System.Drawing.Color.LightGray;
            this.minButton.Location = new System.Drawing.Point(514, 1);
            this.minButton.Name = "minButton";
            this.minButton.Size = new System.Drawing.Size(23, 23);
            this.minButton.TabIndex = 3;
            this.minButton.TabStop = false;
            this.minButton.Text = "–";
            this.minButton.UseVisualStyleBackColor = false;
            this.minButton.Click += new System.EventHandler(this.minButton_Click);
            this.minButton.MouseEnter += new System.EventHandler(this.minButton_MouseEnter);
            this.minButton.MouseLeave += new System.EventHandler(this.minButton_MouseLeave);
            this.minButton.MouseHover += new System.EventHandler(this.minButton_MouseHover);
            // 
            // titleBarLogo
            // 
            this.titleBarLogo.Image = global::FAES_GUI.Properties.Resources.Icon;
            this.titleBarLogo.InitialImage = global::FAES_GUI.Properties.Resources.Icon;
            this.titleBarLogo.Location = new System.Drawing.Point(4, 3);
            this.titleBarLogo.Name = "titleBarLogo";
            this.titleBarLogo.Size = new System.Drawing.Size(20, 20);
            this.titleBarLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.titleBarLogo.TabIndex = 2;
            this.titleBarLogo.TabStop = false;
            this.titleBarLogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.titleLabel.Location = new System.Drawing.Point(26, 1);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(300, 25);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "FileAES ";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            // 
            // quitButton
            // 
            this.quitButton.BackColor = System.Drawing.Color.Transparent;
            this.quitButton.FlatAppearance.BorderSize = 0;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitButton.ForeColor = System.Drawing.Color.LightGray;
            this.quitButton.Location = new System.Drawing.Point(540, 1);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(23, 23);
            this.quitButton.TabIndex = 1;
            this.quitButton.TabStop = false;
            this.quitButton.Text = "✖";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            this.quitButton.MouseEnter += new System.EventHandler(this.quitButton_MouseEnter);
            this.quitButton.MouseLeave += new System.EventHandler(this.quitButton_MouseLeave);
            this.quitButton.MouseHover += new System.EventHandler(this.quitButton_MouseHover);
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.Gray;
            this.sidePanel.Controls.Add(this.aboutMenuButton);
            this.sidePanel.Controls.Add(this.copyrightLabel);
            this.sidePanel.Controls.Add(this.settingsMenuButton);
            this.sidePanel.Controls.Add(this.decryptMenuButton);
            this.sidePanel.Controls.Add(this.encryptMenuButton);
            this.sidePanel.Controls.Add(this.autoSelectMenuButton);
            this.sidePanel.Location = new System.Drawing.Point(0, 24);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(149, 384);
            this.sidePanel.TabIndex = 1;
            this.sidePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sidePanel_Paint);
            // 
            // aboutMenuButton
            // 
            this.aboutMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.aboutMenuButton.Location = new System.Drawing.Point(1, 284);
            this.aboutMenuButton.Name = "aboutMenuButton";
            this.aboutMenuButton.Selected = false;
            this.aboutMenuButton.Size = new System.Drawing.Size(147, 66);
            this.aboutMenuButton.TabIndex = 7;
            this.aboutMenuButton.Text = "About";
            this.aboutMenuButton.Click += new System.EventHandler(this.aboutMenuButton_Click);
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.BackColor = System.Drawing.Color.Transparent;
            this.copyrightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copyrightLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.copyrightLabel.Location = new System.Drawing.Point(1, 361);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(146, 21);
            this.copyrightLabel.TabIndex = 2;
            this.copyrightLabel.Text = "© - 2020 | mullak99";
            this.copyrightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.copyrightLabel.Click += new System.EventHandler(this.CopyrightLabel_Click);
            // 
            // settingsMenuButton
            // 
            this.settingsMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.settingsMenuButton.Location = new System.Drawing.Point(1, 218);
            this.settingsMenuButton.Name = "settingsMenuButton";
            this.settingsMenuButton.Selected = false;
            this.settingsMenuButton.Size = new System.Drawing.Size(147, 66);
            this.settingsMenuButton.TabIndex = 6;
            this.settingsMenuButton.Text = "Settings";
            this.settingsMenuButton.Click += new System.EventHandler(this.settingsMenuButton_Click);
            // 
            // decryptMenuButton
            // 
            this.decryptMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.decryptMenuButton.Location = new System.Drawing.Point(1, 152);
            this.decryptMenuButton.Name = "decryptMenuButton";
            this.decryptMenuButton.Selected = false;
            this.decryptMenuButton.Size = new System.Drawing.Size(147, 66);
            this.decryptMenuButton.TabIndex = 5;
            this.decryptMenuButton.Text = "Decrypt";
            this.decryptMenuButton.Click += new System.EventHandler(this.decryptMenuButton_Click);
            // 
            // encryptMenuButton
            // 
            this.encryptMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.encryptMenuButton.Location = new System.Drawing.Point(1, 86);
            this.encryptMenuButton.Name = "encryptMenuButton";
            this.encryptMenuButton.Selected = false;
            this.encryptMenuButton.Size = new System.Drawing.Size(147, 66);
            this.encryptMenuButton.TabIndex = 4;
            this.encryptMenuButton.Text = "Encrypt";
            this.encryptMenuButton.Click += new System.EventHandler(this.encryptMenuButton_Click);
            // 
            // autoSelectMenuButton
            // 
            this.autoSelectMenuButton.BackColor = System.Drawing.Color.Transparent;
            this.autoSelectMenuButton.Location = new System.Drawing.Point(1, 20);
            this.autoSelectMenuButton.Name = "autoSelectMenuButton";
            this.autoSelectMenuButton.Selected = true;
            this.autoSelectMenuButton.Size = new System.Drawing.Size(147, 66);
            this.autoSelectMenuButton.TabIndex = 3;
            this.autoSelectMenuButton.Text = "Auto-Select Drag/Drop";
            this.autoSelectMenuButton.Click += new System.EventHandler(this.autoSelectMenuButton_Click);
            // 
            // slowToolTip
            // 
            this.slowToolTip.AutoPopDelay = 5000;
            this.slowToolTip.InitialDelay = 1000;
            this.slowToolTip.ReshowDelay = 100;
            // 
            // autoDetect
            // 
            this.autoDetect.AllowDrop = true;
            this.autoDetect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.autoDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoDetect.ForeColor = System.Drawing.Color.White;
            this.autoDetect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.autoDetect.Location = new System.Drawing.Point(149, 25);
            this.autoDetect.Name = "autoDetect";
            this.autoDetect.Size = new System.Drawing.Size(414, 357);
            this.autoDetect.TabIndex = 4;
            this.autoDetect.Text = "Select any file to Encrypt/Decrypt";
            this.autoDetect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.autoDetect.Click += new System.EventHandler(this.autoDetect_Click);
            this.autoDetect.DragDrop += new System.Windows.Forms.DragEventHandler(this.autoDetect_DragDrop);
            this.autoDetect.DragEnter += new System.Windows.Forms.DragEventHandler(this.autoDetect_DragEnter);
            // 
            // openFileDialogAutoSelect
            // 
            this.openFileDialogAutoSelect.FileName = "openFileDialogAutoSelect";
            // 
            // decryptPanel
            // 
            this.decryptPanel.Location = new System.Drawing.Point(149, 25);
            this.decryptPanel.Name = "decryptPanel";
            this.decryptPanel.Size = new System.Drawing.Size(414, 357);
            this.decryptPanel.TabIndex = 6;
            // 
            // encryptPanel
            // 
            this.encryptPanel.Location = new System.Drawing.Point(149, 25);
            this.encryptPanel.Name = "encryptPanel";
            this.encryptPanel.Size = new System.Drawing.Size(414, 357);
            this.encryptPanel.TabIndex = 5;
            // 
            // aboutPanel
            // 
            this.aboutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.aboutPanel.Location = new System.Drawing.Point(149, 25);
            this.aboutPanel.Name = "aboutPanel";
            this.aboutPanel.Size = new System.Drawing.Size(414, 357);
            this.aboutPanel.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.longVersionLabel);
            this.panel1.Location = new System.Drawing.Point(0, 383);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 25);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.titleBar_Paint);
            // 
            // longVersionLabel
            // 
            this.longVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.longVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longVersionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.longVersionLabel.Location = new System.Drawing.Point(156, 2);
            this.longVersionLabel.Name = "longVersionLabel";
            this.longVersionLabel.Size = new System.Drawing.Size(405, 21);
            this.longVersionLabel.TabIndex = 3;
            this.longVersionLabel.Text = "FileAES v2.0.0";
            this.longVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // settingsPanel
            // 
            this.settingsPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.settingsPanel.Location = new System.Drawing.Point(149, 25);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(414, 357);
            this.settingsPanel.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(564, 408);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.aboutPanel);
            this.Controls.Add(this.autoDetect);
            this.Controls.Add(this.decryptPanel);
            this.Controls.Add(this.encryptPanel);
            this.Controls.Add(this.settingsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileAES";
            this.titleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleBarLogo)).EndInit();
            this.sidePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.ToolTip slowToolTip;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.PictureBox titleBarLogo;
        private System.Windows.Forms.Label autoDetect;
        private System.Windows.Forms.OpenFileDialog openFileDialogAutoSelect;
        private MenuPanels.encryptPanel encryptPanel;
        private MenuPanels.decryptPanel decryptPanel;
        private System.Windows.Forms.Button minButton;
        private MenuPanels.aboutPanel aboutPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label longVersionLabel;
        private MenuPanels.settingsPanel settingsPanel;
        private CustomControls.SubMenuButton encryptMenuButton;
        private CustomControls.SubMenuButton settingsMenuButton;
        private CustomControls.SubMenuButton decryptMenuButton;
        private CustomControls.SubMenuButton autoSelectMenuButton;
        private CustomControls.SubMenuButton aboutMenuButton;
    }
}

