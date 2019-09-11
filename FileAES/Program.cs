using FAES;
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
        private const string devAppendTag = "DEV190911-1";
        private const string betaAppendTag = "";

        private static bool _verbose = false;
        private static bool _purgeTemp = false;
        private static bool _headless = false;
        private static bool _getFaesVersion = false;
        private static bool _getVersion = false;
        private static bool _showProgress = false;
        private static bool _overwriteDuplicates = false;
        private static bool _deleteOriginalFile = true;
        private static string _directory = null;
        private static string _password;
        private static string _passwordHint = null;
        private static string _compressionMethod = null;
        private static int _compressionLevel = 7;
        private static ushort _progressSleep = 5000;
        private static List<string> _strippedArgs = new List<string>();

        private static string _spoofedVersion = "v2.0.0";
        private static bool _useSpoofedVersion = false;

        public static FAES_File faesFile;
        public static ProgramManager programManager;

        [STAThread]
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            for (int i = 0; i < args.Length; i++)
            {
                args[i].ToLower();

                string strippedArg = args[i];

                if (Directory.Exists(args[i])) _directory = args[i];
                else if (File.Exists(args[i])) _directory = args[i];

                strippedArg = strippedArg.TrimStart('-', '/', '\\');

                if (strippedArg == "verbose" || strippedArg == "v" || strippedArg == "developer" || strippedArg == "dev" || strippedArg == "debug") _verbose = true;
                else if (strippedArg == "password" || strippedArg == "p" && !string.IsNullOrEmpty(args[i + 1])) _password = args[i + 1];
                else if (String.IsNullOrEmpty(_passwordHint) && (strippedArg == "hint" || strippedArg == "passwordhint" || strippedArg == "h") && !string.IsNullOrEmpty(args[i + 1])) _passwordHint = args[i + 1];
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
                else if (String.IsNullOrEmpty(_compressionMethod) && (strippedArg == "compression" || strippedArg == "compressionmethod" || strippedArg == "c") && !string.IsNullOrEmpty(args[i + 1])) _compressionMethod = args[i + 1].ToUpper();
                else if ((strippedArg == "level" || strippedArg == "compressionlevel" || strippedArg == "l") && !string.IsNullOrEmpty(args[i + 1])) Int32.TryParse(args[i + 1], out _compressionLevel);
                else if (strippedArg == "overwrite" || strippedArg == "overwriteduplicates" || strippedArg == "o") _overwriteDuplicates = true;
                else if (strippedArg == "preserveoriginal" || strippedArg == "original" || strippedArg == "po") _deleteOriginalFile = false;

                _strippedArgs.Add(strippedArg);
            }
            FileAES_Utilities.SetVerboseLogging(_verbose);
            SSM.SetVerboseLogging(_verbose);

            try
            {
                if (File.Exists("FAES-Updater.exe")) File.Delete("FAES-Updater.exe");
            }
            catch { }

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

            if (!String.IsNullOrEmpty(_directory) && !String.IsNullOrEmpty(_password) && _headless)
            {
                faesFile = new FAES_File(_directory);

                Console.WriteLine("FileAES-GUI (FileAES) only has basic command-line support. Use FileAES-CLI for improved command-line functionality.");

                try
                {
                    if (_compressionLevel < 0 || _compressionLevel > 9)
                    {
                        Console.WriteLine("You have not specified a valid compression level! Please choose a value between 0 and 9.");
                        return;
                    }
                    else
                    {
                        if (faesFile.isFileEncryptable())
                        {
                            FileAES_Encrypt encrypt = new FileAES_Encrypt(faesFile, _password, _passwordHint, Optimise.Balanced, null, _deleteOriginalFile, _overwriteDuplicates);

                            if (!String.IsNullOrEmpty(_compressionMethod))
                            {
                                switch (_compressionMethod)
                                {
                                    case "ZIP":
                                        {
                                            encrypt.SetCompressionMode(CompressionMode.ZIP, _compressionLevel);
                                            break;
                                        }
                                    case "TAR":
                                        {
                                            encrypt.SetCompressionMode(CompressionMode.TAR, _compressionLevel);
                                            break;
                                        }
                                    case "LZMA":
                                        {
                                            encrypt.SetCompressionMode(CompressionMode.LZMA, _compressionLevel);
                                            break;
                                        }
                                    case "LGYZIP":
                                    case "LEGACYZIP":
                                    case "LEGACY":
                                        {
                                            encrypt.SetCompressionMode(CompressionMode.LGYZIP, _compressionLevel);
                                            break;
                                        }
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
                                    if (_verbose) Console.WriteLine("[INFO] Progress: {0}%", percentComplete);
                                    else Console.WriteLine("Progress: {0}%", percentComplete);
                                    Thread.Sleep(_progressSleep);
                                }
                            });

                            Thread eThread = new Thread(() =>
                            {
                                try
                                {
                                    if (encrypt.encryptFile())
                                    {
                                        if (_showProgress)
                                        {
                                            if (_verbose) Console.WriteLine("[INFO] Progress: 100%");
                                            else Console.WriteLine("Progress: 100%");
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

                                    if (_verbose) Console.WriteLine("[INFO] Progress: {0}%", percentComplete);
                                    else Console.WriteLine("Progress: {0}%", percentComplete);
                                    Thread.Sleep(_progressSleep);
                                }
                            });

                            Thread dThread = new Thread(() =>
                            {
                                try
                                {
                                    if (decrypt.decryptFile())
                                    {
                                        if (_showProgress)
                                        {
                                            if (_verbose) Console.WriteLine("[INFO] Progress: 100%");
                                            else Console.WriteLine("Progress: 100%");
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
            else
            {
                programManager = new ProgramManager();
                if (!FileAES_Utilities.GetVerboseLogging()) FileAES_Utilities.SetVerboseLogging(programManager.GetDevMode());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!String.IsNullOrEmpty(_directory))
                {
                    faesFile = new FAES_File(_directory);

                    if (faesFile.isFileEncryptable())
                        Application.Run(new EncryptForm(faesFile));
                    else if(faesFile.isFileDecryptable())
                        Application.Run(new DecryptForm(faesFile));
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
                string[] ver = (typeof(FAES_GUI.Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version).Split('.');
                if (IsDevBuild())
                    return "v" + ver[0] + "." + ver[1] + "." + ver[2] + " (" + devAppendTag + ")";
                else if (IsBetaBuild())
                    return "v" + ver[0] + "." + ver[1] + "." + ver[2] + " (" + betaAppendTag + ")";
                else
                    return "v" + ver[0] + "." + ver[1] + "." + ver[2];
            }
            else return _spoofedVersion;
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
            return (!String.IsNullOrEmpty(devAppendTag) && String.IsNullOrEmpty(betaAppendTag));
        }

        public static bool IsDevBuild()
        {
            return !String.IsNullOrEmpty(devAppendTag);
        }

        public static string GetBuild()
        {
            if (IsDevBuild()) return "dev";
            else if (IsBetaBuild()) return "beta";
            else return "stable";
        }

        public static string GetBuildDateFormatted()
        {
            return GetBuildDate().ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        public static DateTime GetBuildDate()
        {
            return new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
        }
    }
}
