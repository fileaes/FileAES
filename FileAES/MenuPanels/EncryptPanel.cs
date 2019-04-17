using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            FileAES_Utilities.SetVerboseLogging(true);
        }

        public encryptPanel(FAES_File faesFile)
        {
            Initialise();

            if (!setFileToEncrypt(faesFile))
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
            InitializeComponent();

            ResetFile();
            populateCompressionModes();
            statusInformation.Text = "";

            this.Focus();
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
        }

        public void LockFileSelect(bool lockFile)
        {
            selectEncryptButton.Enabled = !lockFile;
        }

        public bool setFileToEncrypt(FAES_File faesFile)
        {
            if (faesFile.isFileEncryptable())
            {
                _fileToEncrypt = faesFile;
                fileInfoLabel.Text = _fileToEncrypt.getFileName();
                Locked(false);
                encryptButton.Enabled = false;
                this.ActiveControl = passTextbox;
                return true;
            }
            return false;
        }

        private void setNoteLabel(string note, int severity)
        {
            if (severity == 1) statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Warning: " + note; }));
            else if (severity == 2) statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Important: " + note; }));
            else if (severity == 3) statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Error: " + note; }));
            else statusInformation.Invoke(new MethodInvoker(delegate { this.statusInformation.Text = "Note: " + note; }));
        }

        private void Locked(bool lockChanges)
        {
            passTextbox.Enabled = !lockChanges;
            passConfTextbox.Enabled = !lockChanges;
            passHintTextbox.Enabled = !lockChanges;
            encryptButton.Enabled = !lockChanges;
            compressMode.Enabled = !lockChanges;
        }

        private void populateCompressionModes()
        {
            List<string> optimiseModes = FAES.Packaging.CompressionUtils.GetAllOptimiseModesAsStrings();

            compressMode.Items.Clear();

            foreach (string mode in optimiseModes)
            {
                compressMode.Items.Add(mode.Replace("_", " "));
            }

            compressMode.SelectedIndex = 0;
        }

        private void doEncrypt()
        {
            setNoteLabel("Encrypting... Please wait.", 0);

            _inProgress = true;
            _encryptSuccessful = false;

            Thread mainEncryptThread = new Thread(() =>
            {
                try
                {
                    FileAES_Encrypt encrypt = new FileAES_Encrypt(_fileToEncrypt, passTextbox.Text, passHintTextbox.Text);
                    encrypt.SetCompressionMode(FAES.Packaging.CompressionUtils.GetAllOptimiseModes()[compressMode.SelectedIndex]);

                    Thread eThread = new Thread(() =>
                    {
                        _encryptSuccessful = encrypt.encryptFile();
                    });
                    eThread.Start();

                    while (eThread.ThreadState == ThreadState.Running)
                    {
                        _progress = encrypt.GetEncryptionPercentComplete();
                    }

                    {
                        _inProgress = false;
                        Locked(false);

                        if (_encryptSuccessful)
                        {
                            setNoteLabel("Encryption Complete", 0);
                            progressBar.CustomText = "Done";
                            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                            if (_closeAfterOp) Application.Exit();
                            else ResetFile();
                        }
                        else setNoteLabel("Encryption Failed. Try again later.", 1);
                    }
                }
                catch (Exception e)
                {
                    setNoteLabel(FileAES_Utilities.FAES_ExceptionHandling(e, true), 3);
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
                doEncrypt();
                Locked(true);
            }
            else if (passConfTextbox.Text != passTextbox.Text)
            {
                encryptionTimer.Stop();
                progressBar.ProgressColor = Color.Red;
                progressBar.Value = progressBar.Maximum;

                setNoteLabel("Passwords do not match!", 2);
                passConfTextbox.Focus();
            }
            else if (_inProgress) setNoteLabel("Encryption already in progress.", 1);
            else
            {
                encryptionTimer.Stop();
                progressBar.ProgressColor = Color.Red;
                progressBar.Value = progressBar.Maximum;

                setNoteLabel("Encryption Failed. Try again later.", 1);
                encryptButton.Focus();
            }
        }

        private void selectEncryptButton_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (FileList.Length > 1) setNoteLabel("You may only encrypt a single file or folder at a time.", 2);
            else
            {
                FAES_File tFaesFile = new FAES_File(FileList[0]);
                if (!setFileToEncrypt(tFaesFile)) setNoteLabel("Chosen file cannot be encrypted!", 2);
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
                setFileToEncrypt(tFaesFile);
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
