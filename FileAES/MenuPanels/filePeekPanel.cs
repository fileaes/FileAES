﻿using FAES;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FAES_GUI.MenuPanels
{
    public partial class filePeekPanel : UserControl
    {
        private FAES_File _fileToPeek;

        private bool _inProgress;
        private bool _decryptSuccessful;
        private decimal _progress;
        private Thread _mainDecryptThread, _faesThread;

        public filePeekPanel()
        {
            Initialise();
        }

        public filePeekPanel(FAES_File faesFile)
        {
            Initialise();

            if (!SetFileToDecrypt(faesFile))
                throw new Exception("Input file cannot be decrypted!");
        }

        public bool GetInProgress()
        {
            return _inProgress;
        }

        private void Initialise()
        {
            Logging.Log("FAES_GUI(PeekPanel): Initialising...", Severity.DEBUG);
            InitializeComponent();

            ResetFile();
            statusInformation.Text = "";
            Logging.Log("FAES_GUI(PeekPanel): Initilisation Complete.", Severity.DEBUG);
        }

        public void ResetFile()
        {
            decryptionTimer.Stop();
            progressBar.ProgressColor = Color.Lime;
            progressBar.Value = progressBar.Minimum;

            Locked(true);
            _fileToPeek = null;
            fileInfoLabel.Text = "No File Selected!";
            progressBar.CustomText = "";
            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.Percentage;
            passTextbox.Text = "";
            passHintTextbox.Text = "";
            fileContentsTextbox.Text = "(Not currently peeking a file)";

            Logging.Log("FAES_GUI(ResetFile): Cleared selected file.", Severity.DEBUG);
        }

        public void LockFileSelect(bool lockFile)
        {
            selectDecryptButton.Enabled = !lockFile;
        }

        public bool SetFileToDecrypt(FAES_File faesFile)
        {
            if (faesFile.IsFileDecryptable())
            {
                _fileToPeek = faesFile;
                fileInfoLabel.Text = _fileToPeek.GetFileName();
                SetMetaData();
                Locked(false);
                decryptButton.Enabled = false;
                this.ActiveControl = passTextbox;
                Logging.Log($"FAES_GUI(SetFileToDecrypt): '{_fileToPeek.GetPath()}'", Severity.DEBUG);

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

        private void Delete(string pathToDelete)
        {
            if (Directory.Exists(Path.GetDirectoryName(pathToDelete)))
                Directory.Delete(Path.GetDirectoryName(pathToDelete) ?? string.Empty, true);
        }

        private void SetMetaData()
        {
            passHintTextbox.Text = _fileToPeek.GetPasswordHint();
        }

        private void Locked(bool lockChanges)
        {
            passTextbox.Enabled = !lockChanges;
            passHintTextbox.Enabled = !lockChanges;
            decryptButton.Enabled = !lockChanges;
        }

        private void Decrypt()
        {
            Logging.Log("FAES_GUI(Decrypt): Started!'", Severity.DEBUG);
            string pathOverride = Path.Combine(Path.GetDirectoryName(_fileToPeek.GetPath()) ?? string.Empty, "faesPeekFilePath_" + new Random().Next(), "peekFile" + FileAES_Utilities.ExtentionUFAES);
            string dirName = Path.GetDirectoryName(pathOverride);
            string password = passTextbox.Text;
            string finalPath = Path.Combine(dirName ?? string.Empty, _fileToPeek.GetOriginalFileName());

            DirectoryInfo di = Directory.CreateDirectory(dirName ?? string.Empty);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            SetNote("Decrypting... Please wait.", 0);

            _inProgress = true;
            _decryptSuccessful = false;

            _mainDecryptThread = new Thread(() =>
            {
                try
                {
                    FileAES_Utilities.SetCryptoStreamBuffer(Program.programManager.GetCryptoStreamBufferSize());
                    FileAES_Decrypt decrypt = new FileAES_Decrypt(_fileToPeek, password, false);
                    decrypt.DebugMode = FileAES_Utilities.GetVerboseLogging();

                    _faesThread = new Thread(() =>
                    {
                        try
                        {
                            _decryptSuccessful = decrypt.DecryptFile(pathOverride);
                        }
                        catch (Exception e)
                        {
                            SetNote(FileAES_Utilities.FAES_ExceptionHandling(e, Program.IsVerbose()).Replace("ERROR:", ""), 3);
                        }
                    });
                    _faesThread.Start();

                    while (_faesThread.ThreadState == ThreadState.Running)
                    {
                        _progress = decrypt.GetPercentComplete();
                    }

                    {
                        _inProgress = false;
                        Invoke(new MethodInvoker(() => Locked(false)));

                        if (_decryptSuccessful)
                        {
                            Logging.Log("FAES_GUI(Decrypt): Finished successfully!'", Severity.DEBUG);
                            SetNote("Decryption Complete", 0);
                            progressBar.CustomText = "Done";
                            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;

                            Invoke(new MethodInvoker(() =>
                            {
                                fileContentsTextbox.Text = File.ReadAllText(finalPath);
                            }));
                            Delete(pathOverride);
                        }
                        else
                        {
                            decryptionTimer.Stop();
                            progressBar.ProgressColor = Color.Red;
                            progressBar.Value = progressBar.Maximum;

                            Logging.Log("FAES_GUI(Decrypt): Finished unsuccessfully!'", Severity.DEBUG);
                            if (!statusInformation.Text.ToLower().Contains("error"))
                            {
                                SetNote("Password Incorrect!", 3);
                                progressBar.CustomText = "Password Incorrect!";
                                progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
                                Invoke(new Action(() => passTextbox.Focus()));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    SetNote(FileAES_Utilities.FAES_ExceptionHandling(e, Program.IsVerbose()).Replace("ERROR:", ""), 3);
                }
                finally
                {
                    Delete(pathOverride);
                }
            });
            _mainDecryptThread.Start();
        }

        private void decryptionTimer_Tick(object sender, EventArgs e)
        {
            if (_progress == 0)
            {
                progressBar.CustomText = "Starting";
                progressBar.Value = Convert.ToInt32(Math.Ceiling(_progress));
            }
            else if (_progress <= 50)
            {
                progressBar.CustomText = "Decrypting";
                progressBar.Value = Convert.ToInt32(Math.Ceiling(_progress));
            }
            else if (_progress < 100)
            {
                progressBar.CustomText = "Decompressing";
                progressBar.Value = Convert.ToInt32(Math.Ceiling(_progress));
            }
            else
            {
                progressBar.CustomText = "Finishing";
                progressBar.Value = 99;
            }
            progressBar.VisualMode = CustomControls.ProgressBarDisplayMode.TextAndPercentage;
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (_fileToPeek.IsFileDecryptable() && !_inProgress && passTextbox.Text.Length > 3)
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
                e.Data.GetFormats();
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