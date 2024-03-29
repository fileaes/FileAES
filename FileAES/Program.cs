﻿using FAES;
using FAES.Packaging;
using SimpleSettingsManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FAES_GUI
{
    static class Program
    {
        private const string devAppendTag = "";
        private const string betaAppendTag = "RC 3";

        private static bool _doFilePeek;
        private static bool _verbose;
        private static bool _purgeTemp;
        private static bool _headless;
        private static bool _getFaesVersion;
        private static bool _getVersion;
        private static bool _showProgress;
        private static bool _overwriteDuplicates;
        private static bool _deleteOriginalFile = true;
        private static bool _genFullInstallConfig;
        private static bool _associateFileTypes;
        private static bool _startMenuShortcuts;
        private static bool _contextMenus;
        private static string _installBranch;
        private static string _directory;
        private static string _password;
        private static string _passwordHint;
        private static string _compressionMethod;
        private static int _compressionLevel = 7;
        private static ushort _progressSleep = 5000;
        internal static List<string> _strippedArgs = new List<string>();

        private static readonly List<string> _supportedPeekFiles = new List<string> {".TXT", ".MD", ".LOG"};

        private static string _spoofedVersion = "v2.0.0";
        private static bool _useSpoofedVersion;

        public static FAES_File faesFile;
        public static ProgramManager programManager;

        [STAThread]
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            for (int i = 0; i < args.Length; i++)
            {
                string strippedArg = args[i].ToLower();

                if (Directory.Exists(args[i])) _directory = args[i];
                else if (File.Exists(args[i])) _directory = args[i];

                strippedArg = strippedArg.TrimStart('-', '/', '\\');

                if (strippedArg == "verbose" || strippedArg == "v" || strippedArg == "developer" || strippedArg == "dev" || strippedArg == "debug") _verbose = true;
                else if (strippedArg == "password" || strippedArg == "p" && !string.IsNullOrEmpty(args[i + 1])) _password = args[i + 1];
                else if (string.IsNullOrEmpty(_passwordHint) && (strippedArg == "hint" || strippedArg == "passwordhint" || strippedArg == "h") && !string.IsNullOrEmpty(args[i + 1])) _passwordHint = args[i + 1];
                else if (strippedArg == "purgetemp" || strippedArg == "deletetemp") _purgeTemp = true;
                else if (strippedArg == "headless" || strippedArg == "cli" || strippedArg == "commandline") _headless = true;
                else if (strippedArg == "showprogress" || strippedArg == "progress" || strippedArg == "prog")
                {
                    if (!string.IsNullOrEmpty(args[i + 1]) && UInt16.TryParse(args[i + 1], out _progressSleep)) { }
                    _showProgress = true;
                }
                else if (strippedArg == "faesversion" || strippedArg == "faes" || strippedArg == "faesver") _getFaesVersion = true;
                else if (strippedArg == "faesguiversion" || strippedArg == "faesguiver" || strippedArg == "faesgui" || strippedArg == "guiver" || strippedArg == "ver")
                {
                    _getVersion = true;
                    _getFaesVersion = true;
                }
                else if (string.IsNullOrEmpty(_compressionMethod) && (strippedArg == "compression" || strippedArg == "compressionmethod" || strippedArg == "c") && !string.IsNullOrEmpty(args[i + 1])) _compressionMethod = args[i + 1].ToUpper();
                else if ((strippedArg == "level" || strippedArg == "compressionlevel" || strippedArg == "l") && !string.IsNullOrEmpty(args[i + 1])) Int32.TryParse(args[i + 1], out _compressionLevel);
                else if (strippedArg == "overwrite" || strippedArg == "overwriteduplicates" || strippedArg == "o") _overwriteDuplicates = true;
                else if (strippedArg == "preserveoriginal" || strippedArg == "original" || strippedArg == "po") _deleteOriginalFile = false;
                else if (strippedArg == "genfullinstallconfig") _genFullInstallConfig = true;
                else if (strippedArg == "associatefiletypes" || strippedArg == "filetypes") _associateFileTypes = true;
                else if (strippedArg == "startmenushortcuts" || strippedArg == "startmenu") _startMenuShortcuts = true;
                else if (strippedArg == "contextmenus" || strippedArg == "context") _contextMenus = true;
                else if (strippedArg == "installbranch" && !string.IsNullOrEmpty(args[i + 1])) _installBranch = args[i + 1];
                else if (strippedArg == "peek" || strippedArg == "filepeek") _doFilePeek = true;

                _strippedArgs.Add(strippedArg);
            }
            FileAES_Utilities.SetVerboseLogging(_verbose);
            SSM.SetVerboseLogging(_verbose);

            try
            {
                if (File.Exists("FAES-Updater.exe")) File.Delete("FAES-Updater.exe");
            }
            catch
            {
                // ignored
            }

            if (_purgeTemp)
            {
                FileAES_Utilities.PurgeTempFolder();
            }

            if (_getVersion)
            {
                Console.WriteLine("Current FileAES Version: {0}", GetVersion());
                Console.WriteLine("Current FileAES Build Date: {0}", GetBuildDateFormatted());
            }

            if (_getFaesVersion)
            {
                Console.WriteLine("Current FAES Version: {0}", FileAES_Utilities.GetVersion());
            }

            if (!string.IsNullOrEmpty(_directory) && !string.IsNullOrEmpty(_password) && _headless)
            {
                faesFile = new FAES_File(_directory);

                Console.WriteLine("FileAES-GUI (FileAES) only has basic command-line support. Use FileAES-CLI for improved command-line functionality.");

                try
                {
                    if (_compressionLevel < 0 || _compressionLevel > 9)
                    {
                        Console.WriteLine("You have not specified a valid compression level! Please choose a value between 0 and 9.");
                    }
                    else
                    {
                        if (faesFile.IsFileEncryptable())
                        {
                            FileAES_Encrypt encrypt = new FileAES_Encrypt(faesFile, _password, _passwordHint, Optimise.Balanced, null, _deleteOriginalFile, _overwriteDuplicates);

                            if (!string.IsNullOrEmpty(_compressionMethod))
                            {
                                switch (_compressionMethod)
                                {
                                    case "ZIP":
                                        encrypt.SetCompressionMode(CompressionMode.ZIP, _compressionLevel);
                                        break;
                                    case "TAR":
                                        encrypt.SetCompressionMode(CompressionMode.TAR, _compressionLevel);
                                        break;
                                    case "LZMA":
                                        encrypt.SetCompressionMode(CompressionMode.LZMA, _compressionLevel);
                                        break;
                                    case "GZIP":
                                        encrypt.SetCompressionMode(CompressionMode.GZIP, _compressionLevel);
                                        break;
                                    case "LGYZIP":
                                    case "LEGACYZIP":
                                    case "LEGACY":
                                        encrypt.SetCompressionMode(CompressionMode.LGYZIP, _compressionLevel);
                                        break;
                                    default:
                                        Console.WriteLine("Unknown Compression Method: {0}", _compressionMethod);
                                        return;
                                }
                            }

                            Thread progressThread = new Thread(() =>
                            {
                                while (_showProgress)
                                {
                                    ushort percentComplete = Convert.ToUInt16(encrypt.GetEncryptionPercentComplete());
                                    Console.WriteLine(_verbose ? "[INFO] Progress: {0}%" : "Progress: {0}%", percentComplete);
                                    Thread.Sleep(_progressSleep);
                                }
                            });

                            Thread eThread = new Thread(() =>
                            {
                                try
                                {
                                    if (encrypt.EncryptFile())
                                    {
                                        if (_showProgress)
                                        {
                                            Console.WriteLine(_verbose ? "[INFO] Progress: 100%" : "Progress: 100%");
                                        }

                                        Console.WriteLine("Encryption on {0} succeeded!", faesFile.getFaesType().ToLower());
                                    }
                                    else
                                    {
                                        Console.WriteLine("Encryption on {0} failed!", faesFile.getFaesType().ToLower());
                                    }
                                }
                                catch (Exception e)
                                {
                                    progressThread.Abort();
                                    HandleException(e);
                                }
                            });

                            if (_showProgress) progressThread.Start();
                            eThread.Start();

                            while (eThread.ThreadState == ThreadState.Running)
                            { }

                            progressThread.Abort();
                        }
                        else
                        {
                            FileAES_Decrypt decrypt = new FileAES_Decrypt(faesFile, _password, _deleteOriginalFile, _overwriteDuplicates);

                            Thread progressThread = new Thread(() =>
                            {
                                while (_showProgress)
                                {
                                    ushort percentComplete = Convert.ToUInt16(decrypt.GetDecryptionPercentComplete());

                                    Console.WriteLine(_verbose ? "[INFO] Progress: {0}%" : "Progress: {0}%", percentComplete);
                                    Thread.Sleep(_progressSleep);
                                }
                            });

                            Thread dThread = new Thread(() =>
                            {
                                try
                                {
                                    if (decrypt.DecryptFile())
                                    {
                                        if (_showProgress)
                                        {
                                            Console.WriteLine(_verbose ? "[INFO] Progress: 100%" : "Progress: 100%");
                                        }

                                        Console.WriteLine("Decryption on {0} succeeded!", faesFile.getFaesType().ToLower());
                                    }
                                    else
                                    {
                                        Console.WriteLine("Decryption on {0} failed!", faesFile.getFaesType().ToLower());
                                        Console.WriteLine("Ensure that you entered the correct password!");
                                        Console.WriteLine("Password Hint: {0}", faesFile.GetPasswordHint());
                                    }
                                }
                                catch (Exception e)
                                {
                                    progressThread.Abort();
                                    HandleException(e);
                                }
                            });

                            if (_showProgress) progressThread.Start();
                            dThread.Start();

                            while (dThread.ThreadState == ThreadState.Running)
                            { }

                            progressThread.Abort();
                        }
                    }
                }
                catch (Exception e)
                {
                    HandleException(e);
                }
            }
            else if (_genFullInstallConfig)
            {
                programManager = new ProgramManager(ProgramManager.InstallType.FullInstall);
                programManager.SetBranch(_installBranch);
                programManager.SetAssociateFileTypes(_associateFileTypes);
                programManager.SetStartMenuShortcuts(_startMenuShortcuts);
                programManager.SetContextMenus(_contextMenus);

                Application.Exit();
            }
            else
            {
                programManager = new ProgramManager();
                if (!FileAES_Utilities.GetVerboseLogging()) FileAES_Utilities.SetVerboseLogging(programManager.GetDevMode());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!string.IsNullOrEmpty(_directory))
                {
                    faesFile = new FAES_File(_directory);

                    if (faesFile.IsFileEncryptable())
                        Application.Run(new EncryptForm(faesFile));
                    else if (faesFile.IsFileDecryptable())
                    {
                        if (_doFilePeek && IsFileValidForPeek(faesFile))
                            Application.Run(new PeekForm(faesFile));
                        else
                            Application.Run(new DecryptForm(faesFile));
                    }

                }
                else
                    Application.Run(new MainForm());
            }
        }

        internal static void HandleException(Exception e)
        {
            if (!_verbose)
                Console.WriteLine(FileAES_Utilities.FAES_ExceptionHandling(e));
            else
            {
                Console.WriteLine("[ERROR] Verbose Mode Enabled: Showing Full Exception...\n");
                Console.WriteLine(e.ToString());
                Console.WriteLine("\n\nConsole held open. Press any key to exit.");
                Console.ReadKey();
            }
        }

        public static bool IsFileValidForPeek(FAES_File file)
        {
            if (file.IsFileDecryptable())
                return _supportedPeekFiles.Contains(Path.GetExtension(file.GetOriginalFileName()).ToUpper());

            return false;
        }

        public static bool SetPath(string path)
        {
            if (Directory.Exists(path) || File.Exists(path))
            {
                _directory = path;
                return true;
            }
            return false;
        }

        public static string GetPath()
        {
            return _directory;
        }

        public static string GetPassword()
        {
            return _password;
        }

        public static string GetVersion()
        {
            if (!_useSpoofedVersion)
            {
                string[] ver = (typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version).Split('.');
                if (IsDevBuild())
                    return "v" + ver[0] + "." + ver[1] + "." + ver[2] + " (" + devAppendTag + ")";
                if (IsBetaBuild())
                    return "v" + ver[0] + "." + ver[1] + "." + ver[2] + " (" + betaAppendTag + ")";
                return "v" + ver[0] + "." + ver[1] + "." + ver[2];
            }
            return _spoofedVersion;
        }

        public static void SetSpoofedVersion(bool useSpoofed, string formattedVersion = "v2.0.0")
        {
            _useSpoofedVersion = useSpoofed;
            _spoofedVersion = formattedVersion;
        }

        public static bool IsStableBuild()
        {
            return (!IsDevBuild() && !IsBetaBuild());
        }

        public static bool IsBetaBuild()
        {
            return !string.IsNullOrWhiteSpace(betaAppendTag);
        }

        public static bool IsDevBuild()
        {
            return !string.IsNullOrWhiteSpace(devAppendTag);
        }

        public static string GetBuild()
        {
            if (IsDevBuild()) return "dev";
            if (IsBetaBuild()) return "beta";
            return "stable";
        }

        public static string GetBuildDateFormatted()
        {
            return GetBuildDate().ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        public static DateTime GetBuildDate()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
        }

        public static bool IsVerbose()
        {
            return _verbose;
        }

        public static string[] DumpInstallerOptions()
        {
            List<string> options = new List<string>();

            if (programManager.GetAssociateFileTypes())
                options.Add("--associatefiletypes");
            if (programManager.GetStartMenuShortcuts())
                options.Add("--startmenushortcuts");
            if (programManager.GetContextMenus())
                options.Add("--contextmenus");

            return options.ToArray();
        }
    }
}
