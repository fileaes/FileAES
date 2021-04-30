using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FAES;
using System.Threading;

namespace FAES_GUI.MenuPanels
{
    public partial class encryptPanel : UserControl
    {
        private FAES_File _fileToEncrypt;

        private bool _inProgress = false;
        private bool _encryptSuccessful;
        private bool _closeAfterOp = false;
        private decimal _progress = 0;

        public encryptPanel()
        {
            Initialise();
        }

        public encryptPanel(FAES_File faesFile)
        {
            Initialise();

            if (!SetFileToEncrypt(faesFile))
                throw new Exception("Input file cannot be encrypted!");
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
            Logging.Log(String.Format("FAES_GUI(EncryptPanel): Initialising..."), Severity.DEBUG);
            InitializeComponent();

            ResetFile();
            PopulateCompressionModes();
            statusInformation.Text = "";

            this.Focus();
            Logging.Log(String.Format("FAES_GUI(EncryptPanel): Initilisation Complete."), Severity.DEBUG);
        }

        public void ResetFile()
        {
            encryptionTimer.Stop();
            progressBar.ProgressColor = Color.Lime;
            progressBar.Value = progressBar.Minimum;

            Locked(true);
            _fileToEncrypt = null;
            passTextbox.Text = "";
            passConfTextbox.Text = "";
            passHintTextbox.Text = "";
            fileInfoLabel.Text = "No File Selected!";
            progressBar.CustomText = "";
            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.Percentage;

            Logging.Log(String.Format("FAES_GUI(ResetFile): Cleared selected file."), Severity.DEBUG);
        }

        public void LockFileSelect(bool lockFile)
        {
            selectEncryptButton.Enabled = !lockFile;
        }

        public bool SetFileToEncrypt(FAES_File faesFile)
        {
            if (faesFile.isFileEncryptable())
            {
                _fileToEncrypt = faesFile;
                fileInfoLabel.Text = _fileToEncrypt.getFileName();
                Locked(false);
                encryptButton.Enabled = false;
                this.ActiveControl = passTextbox;
                Logging.Log(String.Format("FAES_GUI(SetFileToEncrypt): '{0}'", _fileToEncrypt.getPath()), Severity.DEBUG);

                return true;
            }
            return false;
        }

        private void SetNote(string note, int severity)
        {
            Logging.Log(String.Format("FAES_GUI(SetNote({1})): '{0}'", note, severity), Severity.DEBUG);
            string message;

            switch (severity)
            {
                case 1:
                    message = "Warning: " + note;
                    break;
                case 2:
                    message = "Important: " + note;
                    break;
                case 3:
                    message = "Error: " + note;
                    break;
                default:
                    message = "Note: " + note;
                    break;
            }

            statusInformation.Invoke(new MethodInvoker(delegate
            {
                this.statusInformation.Text = message;
                this.toolTip.SetToolTip(statusInformation, message);
            }));
        }

        private void Locked(bool lockChanges)
        {
            passTextbox.Enabled = !lockChanges;
            passConfTextbox.Enabled = !lockChanges;
            passHintTextbox.Enabled = !lockChanges;
            encryptButton.Enabled = !lockChanges;
            compressMode.Enabled = !lockChanges;
            deleteOriginal.Enabled = !lockChanges;
            overwriteDuplicate.Enabled = !lockChanges;
        }

        private void PopulateCompressionModes()
        {
            List<string> optimiseModes = FAES.Packaging.CompressionUtils.GetAllOptimiseModesAsStrings();

            compressMode.Items.Clear();

            foreach (string mode in optimiseModes)
            {
                compressMode.Items.Add(mode.Replace("_", " "));
            }

            compressMode.SelectedIndex = 0;
        }

        private void Encrypt()
        {
            string password = passTextbox.Text;
            string passHint = passHintTextbox.Text;
            int compressIndex = compressMode.SelectedIndex;
            bool delAfterEnc = deleteOriginal.Checked;
            bool ovDup = overwriteDuplicate.Checked;

            Logging.Log(String.Format("FAES_GUI(Encrypt): Started!'"), Severity.DEBUG);

            SetNote("Encrypting... Please wait.", 0);

            _inProgress = true;
            _encryptSuccessful = false;

            Thread mainEncryptThread = new Thread(() =>
            {
                try
                {

                    FileAES_Encrypt encrypt = new FileAES_Encrypt(_fileToEncrypt, password, passHint, FAES.Packaging.CompressionUtils.GetAllOptimiseModes()[compressIndex]);
                    encrypt.SetDeleteAfterEncrypt(delAfterEnc);
                    encrypt.SetOverwriteDuplicate(ovDup);
                    encrypt.DebugMode = FileAES_Utilities.GetVerboseLogging();

                    Thread eThread = new Thread(() =>
                    {
                        try
                        {
                            _encryptSuccessful = encrypt.encryptFile();
                        }
                        catch (Exception e)
                        {
                            SetNote(FileAES_Utilities.FAES_ExceptionHandling(e, Program.IsVerbose()).Replace("ERROR:", ""), 3);
                        }
                    });
                    eThread.Start();

                    while (eThread.ThreadState == ThreadState.Running)
                    {
                        _progress = encrypt.GetEncryptionPercentComplete();
                    }

                    {
                        _inProgress = false;

                        this.Invoke(new MethodInvoker(() => Locked(false)));

                        if (_encryptSuccessful)
                        {
                            Logging.Log(String.Format("FAES_GUI(Encrypt): Finished successfully!'"), Severity.DEBUG);
                            SetNote("Encryption Complete", 0);
                            progressBar.CustomText = "Done";
                            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                            if (_closeAfterOp) Application.Exit();
                            else ResetFile();
                        }
                        else
                        {
                            Logging.Log(String.Format("FAES_GUI(Encrypt): Finished unsuccessfully!'"), Severity.DEBUG);
                            if (!statusInformation.Text.ToLower().Contains("error")) SetNote("Encryption Failed. Try again later.", 1);
                        }
                    }
                }
                catch (Exception e)
                {
                    SetNote(FileAES_Utilities.FAES_ExceptionHandling(e, Program.IsVerbose()).Replace("ERROR:", ""), 3);
                }
            });
            mainEncryptThread.Start();
        }

        private void encryptionTimer_Tick(object sender, EventArgs e)
        {
            if (_progress < 100)
            {
                if (_progress > 0) progressBar.CustomText = "Encrypting";
                else progressBar.CustomText = "Compressing";
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

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if (_fileToEncrypt.isFileEncryptable() && !_inProgress && passConfTextbox.Text == passTextbox.Text)
            {
                progressBar.ProgressColor = Color.Lime;
                progressBar.Value = progressBar.Minimum;
                encryptionTimer.Start();
                Encrypt();
                Locked(true);
            }
            else if (passConfTextbox.Text != passTextbox.Text)
            {
                encryptionTimer.Stop();
                progressBar.ProgressColor = Color.Red;
                progressBar.Value = progressBar.Maximum;

                SetNote("Passwords do not match!", 2);
                passConfTextbox.Focus();
            }
            else if (_inProgress) SetNote("Encryption already in progress.", 1);
            else
            {
                encryptionTimer.Stop();
                progressBar.ProgressColor = Color.Red;
                progressBar.Value = progressBar.Maximum;

                SetNote("Encryption Failed. Try again later.", 1);
                encryptButton.Focus();
            }
        }

        private void selectEncryptButton_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (FileList.Length > 1) SetNote("You may only encrypt a single file or folder at a time.", 2);
            else
            {
                FAES_File tFaesFile = new FAES_File(FileList[0]);
                if (!SetFileToEncrypt(tFaesFile)) SetNote("Chosen file cannot be encrypted!", 2);
            }
        }

        private void selectEncryptButton_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
            {
                String[] strGetFormats = e.Data.GetFormats();
                e.Effect = DragDropEffects.None;
            }
        }

        private void selectEncryptButton_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                ResetFile();
            }
            else if (openFileToEncrypt.ShowDialog() == DialogResult.OK)
            {
                FAES_File tFaesFile = new FAES_File(openFileToEncrypt.FileName);
                SetFileToEncrypt(tFaesFile);
            }
        }

        private void combinedPassword_TextChanged(object sender, EventArgs e)
        {
            if (passTextbox.Text.Length > 3 && passConfTextbox.Text.Length > 3) encryptButton.Enabled = true;
            else if (encryptButton.Enabled) encryptButton.Enabled = false;
        }

        private void passHintTextbox_TextChanged(object sender, EventArgs e)
        {
            passHintTextbox.Text = passHintTextbox.Text.Replace(Environment.NewLine, "");
        }

        private void allTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                encryptButton_Click(sender, e);
            }
        }
    }
}
