namespace FAES_GUI.CustomControls
{
    partial class SettingIncrementBox
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
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.numericTextbox = new System.Windows.Forms.TextBox();
            this.settingDesc = new System.Windows.Forms.Label();
            this.settingHeader = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainPanel.Controls.Add(this.downButton);
            this.mainPanel.Controls.Add(this.upButton);
            this.mainPanel.Controls.Add(this.numericTextbox);
            this.mainPanel.Controls.Add(this.settingDesc);
            this.mainPanel.Controls.Add(this.settingHeader);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(400, 100);
            this.mainPanel.TabIndex = 1;
            // 
            // downButton
            // 
            this.downButton.BackColor = System.Drawing.Color.Teal;
            this.downButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.ForeColor = System.Drawing.Color.White;
            this.downButton.Location = new System.Drawing.Point(373, 74);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(21, 23);
            this.downButton.TabIndex = 12;
            this.downButton.Text = "▼";
            this.downButton.UseVisualStyleBackColor = false;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // upButton
            // 
            this.upButton.BackColor = System.Drawing.Color.Teal;
            this.upButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.ForeColor = System.Drawing.Color.White;
            this.upButton.Location = new System.Drawing.Point(349, 74);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(21, 23);
            this.upButton.TabIndex = 11;
            this.upButton.Text = "▲";
            this.upButton.UseVisualStyleBackColor = false;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // numericTextbox
            // 
            this.numericTextbox.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextbox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextbox.Location = new System.Drawing.Point(3, 75);
            this.numericTextbox.Name = "numericTextbox";
            this.numericTextbox.Size = new System.Drawing.Size(342, 22);
            this.numericTextbox.TabIndex = 10;
            this.numericTextbox.Text = "0";
            this.numericTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextbox.TextChanged += new System.EventHandler(this.numericTextbox_TextChanged);
            this.numericTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericTextbox_KeyPress);
            // 
            // settingDesc
            // 
            this.settingDesc.BackColor = System.Drawing.Color.Transparent;
            this.settingDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingDesc.ForeColor = System.Drawing.Color.White;
            this.settingDesc.Location = new System.Drawing.Point(3, 28);
            this.settingDesc.Name = "settingDesc";
            this.settingDesc.Size = new System.Drawing.Size(393, 43);
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
            // SettingIncrementBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mainPanel);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SettingIncrementBox";
            this.Size = new System.Drawing.Size(400, 100);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingIncrementBox_Paint);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label settingDesc;
        private System.Windows.Forms.Label settingHeader;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.TextBox numericTextbox;
    }
}
