namespace FAES_GUI
{
    partial class DevForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevForm));
            this.quitButton = new System.Windows.Forms.Button();
            this.titleBar = new System.Windows.Forms.Panel();
            this.titleBarLogo = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.slowToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.consoleTextBox = new System.Windows.Forms.RichTextBox();
            this.clearConsole = new System.Windows.Forms.Button();
            this.exportLog = new System.Windows.Forms.Button();
            this.minButton = new System.Windows.Forms.Button();
            this.titleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.titleBarLogo)).BeginInit();
            this.SuspendLayout();
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
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.titleBar.Controls.Add(this.minButton);
            this.titleBar.Controls.Add(this.titleBarLogo);
            this.titleBar.Controls.Add(this.titleLabel);
            this.titleBar.Controls.Add(this.quitButton);
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(416, 25);
            this.titleBar.TabIndex = 2;
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
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.titleLabel.Location = new System.Drawing.Point(26, 1);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(300, 25);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Developer | FileAES ";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            // 
            // slowToolTip
            // 
            this.slowToolTip.AutoPopDelay = 5000;
            this.slowToolTip.InitialDelay = 1000;
            this.slowToolTip.ReshowDelay = 100;
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.BackColor = System.Drawing.Color.Black;
            this.consoleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consoleTextBox.ForeColor = System.Drawing.Color.LightGray;
            this.consoleTextBox.Location = new System.Drawing.Point(12, 31);
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.ReadOnly = true;
            this.consoleTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.consoleTextBox.Size = new System.Drawing.Size(392, 241);
            this.consoleTextBox.TabIndex = 3;
            this.consoleTextBox.Text = "";
            // 
            // clearConsole
            // 
            this.clearConsole.BackColor = System.Drawing.Color.Red;
            this.clearConsole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearConsole.ForeColor = System.Drawing.Color.White;
            this.clearConsole.Location = new System.Drawing.Point(12, 293);
            this.clearConsole.Name = "clearConsole";
            this.clearConsole.Size = new System.Drawing.Size(121, 32);
            this.clearConsole.TabIndex = 4;
            this.clearConsole.Text = "Clear";
            this.clearConsole.UseVisualStyleBackColor = false;
            this.clearConsole.Click += new System.EventHandler(this.ClearConsole_Click);
            // 
            // exportLog
            // 
            this.exportLog.BackColor = System.Drawing.Color.ForestGreen;
            this.exportLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exportLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportLog.ForeColor = System.Drawing.Color.White;
            this.exportLog.Location = new System.Drawing.Point(283, 293);
            this.exportLog.Name = "exportLog";
            this.exportLog.Size = new System.Drawing.Size(121, 32);
            this.exportLog.TabIndex = 5;
            this.exportLog.Text = "Export Log";
            this.exportLog.UseVisualStyleBackColor = false;
            this.exportLog.Click += new System.EventHandler(this.ExportLog_Click);
            // 
            // minButton
            // 
            this.minButton.BackColor = System.Drawing.Color.Transparent;
            this.minButton.FlatAppearance.BorderSize = 0;
            this.minButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minButton.ForeColor = System.Drawing.Color.LightGray;
            this.minButton.Location = new System.Drawing.Point(366, 1);
            this.minButton.Name = "minButton";
            this.minButton.Size = new System.Drawing.Size(23, 23);
            this.minButton.TabIndex = 4;
            this.minButton.TabStop = false;
            this.minButton.Text = "–";
            this.minButton.UseVisualStyleBackColor = false;
            this.minButton.Click += new System.EventHandler(this.minButton_Click);
            this.minButton.MouseEnter += new System.EventHandler(this.minButton_MouseEnter);
            this.minButton.MouseLeave += new System.EventHandler(this.minButton_MouseLeave);
            this.minButton.MouseHover += new System.EventHandler(this.minButton_MouseHover);
            // 
            // DevForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(416, 337);
            this.Controls.Add(this.exportLog);
            this.Controls.Add(this.clearConsole);
            this.Controls.Add(this.consoleTextBox);
            this.Controls.Add(this.titleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DevForm";
            this.Text = "FileAES: Developer Menu";
            this.titleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleBarLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.PictureBox titleBarLogo;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ToolTip slowToolTip;
        private System.Windows.Forms.RichTextBox consoleTextBox;
        private System.Windows.Forms.Button clearConsole;
        private System.Windows.Forms.Button exportLog;
        private System.Windows.Forms.Button minButton;
    }
}