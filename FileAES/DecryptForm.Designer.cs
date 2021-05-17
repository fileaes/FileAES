﻿namespace FAES_GUI
{
    partial class DecryptForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecryptForm));
            this.titleBar = new System.Windows.Forms.Panel();
            this.titleBarLogo = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.quitButton = new System.Windows.Forms.Button();
            this.slowToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.decryptPanel = new FAES_GUI.MenuPanels.decryptPanel();
            this.titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.titleBar.Controls.Add(this.titleBarLogo);
            this.titleBar.Controls.Add(this.titleLabel);
            this.titleBar.Controls.Add(this.quitButton);
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(416, 25);
            this.titleBar.TabIndex = 1;
            this.titleBar.Paint += new System.Windows.Forms.PaintEventHandler(this.titleBar_Paint);
            this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
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
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.titleLabel.Location = new System.Drawing.Point(26, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(300, 25);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Decrypt | FileAES ";
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
            this.quitButton.Location = new System.Drawing.Point(392, 1);
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
            // slowToolTip
            // 
            this.slowToolTip.AutoPopDelay = 5000;
            this.slowToolTip.InitialDelay = 1000;
            this.slowToolTip.ReshowDelay = 100;
            // 
            // decryptPanel
            // 
            this.decryptPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.decryptPanel.Location = new System.Drawing.Point(1, -21);
            this.decryptPanel.Name = "decryptPanel";
            this.decryptPanel.Size = new System.Drawing.Size(414, 357);
            this.decryptPanel.TabIndex = 2;
            // 
            // DecryptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.ClientSize = new System.Drawing.Size(416, 337);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.decryptPanel);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DecryptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileAES: Decrypt";
            this.titleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleBarLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.PictureBox titleBarLogo;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button quitButton;
        private MenuPanels.decryptPanel decryptPanel;
        private System.Windows.Forms.ToolTip slowToolTip;
    }
}