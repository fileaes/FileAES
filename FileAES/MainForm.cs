﻿using FAES;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FAES_GUI
{
    public partial class MainForm : Form
    {
        private bool _closeAfterOperation = false;

        private DevForm _devForm;

        public MainForm(FAES_File faesFile = null)
        {
            InitializeComponent();
            autoDetect.BringToFront();

            titleLabel.Text += Program.GetVersion();
            this.Text = titleLabel.Text;

            if (FileAES_Utilities.GetVerboseLogging())
            {
                _devForm = new DevForm();

                // Hacky solution to the RichTextBox Console.SetOut causing issues if the DevForm is not opened at least once before encryption/decryption (otherwise it hangs)
                _devForm.Show();
                _devForm.Hide();
            }

            autoSelectMenuButton.registerDetoggles(new CustomControls.SubMenuButton[3] { encryptMenuButton, decryptMenuButton, settingsMenuButton });
            encryptMenuButton.registerDetoggles(new CustomControls.SubMenuButton[3] { autoSelectMenuButton, decryptMenuButton, settingsMenuButton });
            decryptMenuButton.registerDetoggles(new CustomControls.SubMenuButton[3] { autoSelectMenuButton, encryptMenuButton, settingsMenuButton });
            settingsMenuButton.registerDetoggles(new CustomControls.SubMenuButton[3] { autoSelectMenuButton, encryptMenuButton, decryptMenuButton });

            if (faesFile != null)
            {
                _closeAfterOperation = true;

                encryptPanel.setCloseAfterOperationSuccessful(_closeAfterOperation);
                decryptPanel.setCloseAfterOperationSuccessful(_closeAfterOperation);

                FAESMenuHandler(faesFile);
            }
        }

        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Logging.Log(String.Format("FAES_GUI(MainGUI): Quit Button pressed. Exiting..."), Severity.DEBUG);
            Environment.Exit(0);
        }

        private void minButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void sidePanel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, sidePanel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void titleBar_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, titleBar.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void quitButton_MouseEnter(object sender, EventArgs e)
        {
            quitButton.BackColor = Color.Red;
            quitButton.ForeColor = Color.Black;
        }

        private void quitButton_MouseLeave(object sender, EventArgs e)
        {
            quitButton.BackColor = Color.Transparent;
            quitButton.ForeColor = Color.LightGray;
        }

        private void quitButton_MouseHover(object sender, EventArgs e)
        {
            slowToolTip.SetToolTip(quitButton, "Close");
        }

        private void minButton_MouseEnter(object sender, EventArgs e)
        {
            minButton.BackColor = Color.LightGray;
            minButton.ForeColor = Color.White;
        }

        private void minButton_MouseLeave(object sender, EventArgs e)
        {
            minButton.BackColor = Color.Transparent;
            minButton.ForeColor = Color.White;
        }

        private void minButton_MouseHover(object sender, EventArgs e)
        {
            slowToolTip.SetToolTip(minButton, "Minimise");
        }

        private void autoDetect_Click(object sender, EventArgs e)
        {
            if (openFileDialogAutoSelect.ShowDialog() == DialogResult.OK)
            {
                FAESMenuHandler(new FAES_File(openFileDialogAutoSelect.FileName));
            }
        }

        private void autoDetect_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            FAESMenuHandler(new FAES_File(FileList[0]));
        }

        private void autoDetect_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
            {
                String[] strGetFormats = e.Data.GetFormats();
                e.Effect = DragDropEffects.None;
            }
        }

        private void FAESMenuHandler(FAES_File faesFile)
        {
            if (faesFile.isFileEncryptable())
            {
                Logging.Log(String.Format("FAES_GUI(MainGUI): FAESMenuHandler detected a valid, encryptable file! ({0})", faesFile.getPath()), Severity.DEBUG);

                encryptMenuButton_Click(null, null);
                encryptMenuButton.Selected = true;
                autoSelectMenuButton.Selected = false;
                encryptPanel.SetFileToEncrypt(faesFile);
            }
            else if (faesFile.isFileDecryptable())
            {
                Logging.Log(String.Format("FAES_GUI(MainGUI): FAESMenuHandler detected a valid, decryptable file! ({0})", faesFile.getPath()), Severity.DEBUG);

                decryptMenuButton_Click(null, null);
                decryptMenuButton.Selected = true;
                autoSelectMenuButton.Selected = false;
                decryptPanel.SetFileToDecrypt(faesFile);
            }
            else Logging.Log(String.Format("FAES_GUI(MainGUI): FAESMenuHandler detected an invalid file! ({0})", faesFile.getPath()), Severity.DEBUG);
        }

        private void autoSelectMenuButton_Click(object sender, EventArgs e)
        {
            autoDetect.BringToFront();
            Logging.Log(String.Format("FAES_GUI(MainGUI): AutoSelectPanel Active."), Severity.DEBUG);
        }

        private void encryptMenuButton_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift) encryptPanel.ResetFile();
            encryptPanel.BringToFront();
            Logging.Log(String.Format("FAES_GUI(MainGUI): EncryptPanel Active."), Severity.DEBUG);
        }

        private void decryptMenuButton_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift) decryptPanel.ResetFile();
            decryptPanel.BringToFront();
            Logging.Log(String.Format("FAES_GUI(MainGUI): DecryptPanel Active."), Severity.DEBUG);
        }

        private void settingsMenuButton_Click(object sender, EventArgs e)
        {
            settingsPanel.BringToFront();
            settingsPanel.LoadSettings();
            Logging.Log(String.Format("FAES_GUI(MainGUI): SettingsPanel Active."), Severity.DEBUG);
        }

        private void CopyrightLabel_Click(object sender, EventArgs e)
        {
            if (FileAES_Utilities.GetVerboseLogging())
            {
                if (_devForm.WindowState == FormWindowState.Minimized && _devForm.Visible)
                {
                    Logging.Log(String.Format("FAES_GUI(MainGUI): DevForm detected in a minimised state. Setting its WindowState to Normal."), Severity.DEBUG);
                    _devForm.WindowState = FormWindowState.Normal;
                }
                else
                {
                    _devForm.Visible = !_devForm.Visible;
                    if (_devForm.Visible) _devForm.WindowState = FormWindowState.Normal;
                    Logging.Log(String.Format("FAES_GUI(MainGUI): DevForm visibility changed to: {0}.", _devForm.Visible ? "Shown" : "Hidden"), Severity.DEBUG);
                }
            }
        }
    }
}
