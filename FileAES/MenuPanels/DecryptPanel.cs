using FAES;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FAES_GUI.MenuPanels
{
    public partial class decryptPanel : UserControl
    {
        private FAES_File _fileToDecrypt;

        private bool _inProgress = false;
        private bool _decryptSuccessful;
        private bool _closeAfterOp = false;
        private decimal _progress = 0;

        public decryptPanel()
        {
            Initialise();
        }

        public decryptPanel(FAES_File faesFile)
        {
            Initialise();

            if (!SetFileToDecrypt(faesFile))
                throw new Exception("Input file cannot be decrypted!");
        }

        public bool GetInProgress()
        {
            return _inProgress;
        }

        public void setCloseAfterOperationSuccessful(bool close)
        {
            _closeAfterOp = close;
        }

        private void Initialise()
        {
            Logging.Log(String.Format("FAES_GUI(DecryptPanel): Initialising..."), Severity.DEBUG);
            InitializeComponent();

            ResetFile();
            statusInformation.Text = "";
            Logging.Log(String.Format("FAES_GUI(DecryptPanel): Initilisation Complete."), Severity.DEBUG);
        }

        public void ResetFile()
        {
            decryptionTimer.Stop();
            progressBar.ProgressColor = Color.Lime;
            progressBar.Value = progressBar.Minimum;

            Locked(true);
            _fileToDecrypt = null;
            fileInfoLabel.Text = "No File Selected!";
            progressBar.CustomText = "";
            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.Percentage;
            passTextbox.Text = "";
            passHintTextbox.Text = "";
            encryptedFileMetaData.Text = "";

            Logging.Log(String.Format("FAES_GUI(ResetFile): Cleared selected file."), Severity.DEBUG);
        }

        public void LockFileSelect(bool lockFile)
        {
            selectDecryptButton.Enabled = !lockFile;
        }

        public bool SetFileToDecrypt(FAES_File faesFile)
        {
            if (faesFile.isFileDecryptable())
            {
                _fileToDecrypt = faesFile;
                fileInfoLabel.Text = _fileToDecrypt.getFileName();
                SetMetaData();
                Locked(false);
                decryptButton.Enabled = false;
                this.ActiveControl = passTextbox;
                Logging.Log(String.Format("FAES_GUI(SetFileToDecrypt): '{0}'", _fileToDecrypt.getPath()), Severity.DEBUG);

                return true;
            }
            return false;
        }

        private void SetNote(string note, int severity)
        {
            Logging.Log(String.Format("FAES_GUI(SetNote({1})): '{0}'", note, severity), Severity.DEBUG);

            switch (severity)
            {
                case 1:
                    statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Warning: " + note; }));
                    break;
                case 2:
                    statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Important: " + note; }));
                    break;
                case 3:
                    statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Error: " + note; }));
                    break;
                default:
                    statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Note: " + note; }));
                    break;
            }
        }

        private void SetMetaData()
        {
            int timestamp = _fileToDecrypt.GetEncryptionTimeStamp();
            string version = _fileToDecrypt.GetEncryptionVersion();
            string compression = _fileToDecrypt.GetEncryptionCompressionMode();

            encryptedFileMetaData.ResetText();

            if (timestamp >= 0)
                encryptedFileMetaData.Text += String.Format("Encrypted on {0} at {1}.", FileAES_Utilities.UnixTimeStampToDateTime((double)timestamp).ToString("dd/MM/yyyy"), FileAES_Utilities.UnixTimeStampToDateTime((double)timestamp).ToString("hh:mm:ss tt"));
            else
                encryptedFileMetaData.Text += String.Format("This file does not contain a encryption date. This is likely due to this file being encrypted using an older FAES version.");

            encryptedFileMetaData.Text += (Environment.NewLine + String.Format("FAES {0} was used.", version));

            if (compression == "LGYZIP")
                encryptedFileMetaData.Text += (Environment.NewLine + "Compressed with LEGACYZIP.");
            else
                encryptedFileMetaData.Text += (Environment.NewLine + String.Format("Compressed with {0}.", compression));

            passHintTextbox.Text = _fileToDecrypt.GetPasswordHint();
        }

        private void Locked(bool lockChanges)
        {
            passTextbox.Enabled = !lockChanges;
            passHintTextbox.Enabled = !lockChanges;
            decryptButton.Enabled = !lockChanges;
        }

        private void Decrypt()
        {
            Logging.Log(String.Format("FAES_GUI(Decrypt): Started!'"), Severity.DEBUG);

            SetNote("Decrypting... Please wait.", 0);

            _inProgress = true;
            _decryptSuccessful = false;

            Thread mainDecryptThread = new Thread(() =>
            {
                try
                {
                    FileAES_Decrypt decrypt = new FileAES_Decrypt(_fileToDecrypt, passTextbox.Text);

                    Thread dThread = new Thread(() =>
                    {
                        _decryptSuccessful = decrypt.decryptFile();
                    });
                    dThread.Start();

                    while (dThread.ThreadState == ThreadState.Running)
                    {
                        _progress = decrypt.GetDecryptionPercentComplete();
                    }

                    {
                        _inProgress = false;
                        Locked(false);

                        if (_decryptSuccessful)
                        {
                            Logging.Log(String.Format("FAES_GUI(Decrypt): Finished successfully!'"), Severity.DEBUG);
                            SetNote("Decryption Complete", 0);
                            progressBar.CustomText = "Done";
                            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                            if (_closeAfterOp) Application.Exit();
                            else ResetFile();
                        }
                        else
                        {
                            decryptionTimer.Stop();
                            progressBar.ProgressColor = Color.Red;
                            progressBar.Value = progressBar.Maximum;

                            Logging.Log(String.Format("FAES_GUI(Decrypt): Finished unsuccessfully!'"), Severity.DEBUG);
                            SetNote("Password Incorrect!", 3);
                            progressBar.CustomText = "Password Incorrect!";
                            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                            passTextbox.Focus();
                        }
                    }
                }
                catch (Exception e)
                {
                    SetNote(FileAES_Utilities.FAES_ExceptionHandling(e, true), 3);
                }
            });
            mainDecryptThread.Start();
        }

        private void decryptionTimer_Tick(object sender, EventArgs e)
        {
            if (_progress < 100)
            {
                if (_progress < 99) progressBar.CustomText = "Decrypting";
                else progressBar.CustomText = "Uncompressing";
                progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                progressBar.Value = Convert.ToInt32(Math.Ceiling(_progress));
            }
            else
            {
                progressBar.CustomText = "Finishing";
                progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                progressBar.Value = 100;
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (_fileToDecrypt.isFileDecryptable() && !_inProgress && passTextbox.Text.Length > 3)
            {
                progressBar.ProgressColor = Color.Lime;
                progressBar.Value = progressBar.Minimum;
                decryptionTimer.Start();
                Decrypt();
                Locked(true);
            }
            else if (_inProgress) SetNote("Decryption already in progress.", 1);
            else
            {
                decryptionTimer.Stop();
                progressBar.ProgressColor = Color.Red;
                progressBar.Value = progressBar.Maximum;

                SetNote("Decryption Failed. Try again later.", 1);
                decryptButton.Focus();
            }
        }

        private void selectDecryptButton_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (FileList.Length > 1) SetNote("You may only decrypt a single file or folder at a time.", 2);
            else
            {
                FAES_File tFaesFile = new FAES_File(FileList[0]);
                if (!SetFileToDecrypt(tFaesFile)) SetNote("Chosen file cannot be decrypted!", 2);
            }
        }

        private void selectDecryptButton_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
            {
                String[] strGetFormats = e.Data.GetFormats();
                e.Effect = DragDropEffects.None;
            }
        }

        private void selectDecryptButton_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                ResetFile();
            }
            else if (openFileToDecrypt.ShowDialog() == DialogResult.OK)
            {
                FAES_File tFaesFile = new FAES_File(openFileToDecrypt.FileName);
                SetFileToDecrypt(tFaesFile);
            }
        }

        private void combinedPassword_TextChanged(object sender, EventArgs e)
        {
            if (passTextbox.Text.Length > 3) decryptButton.Enabled = true;
            else if (decryptButton.Enabled) decryptButton.Enabled = false;
        }

        private void allTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                decryptButton_Click(sender, e);
            }
        }
    }
}