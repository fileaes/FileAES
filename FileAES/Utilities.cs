using System;
using System.IO;
using FAES;
using SimpleSettingsManager;

namespace FAES_GUI
{
    public class SettingsManager
    {
        public const string _programSettingsFileName = "FileAES_ProgSettings.db";

        protected SSM _ssm;
        protected SSM_File _ssmFile;
        protected string _programSettingsPath;

        public SettingsManager()
        {
            _programSettingsPath = Path.Combine(ProgramManager.GetConfigPath(), _programSettingsFileName);
            _ssmFile = new SSM_File(_programSettingsPath, SSM_File.Mode.SQLite);
            _ssm = new SSM(_ssmFile);

            InitSSM();
            UpdateSSM();
        }

        public string GetPath()
        {
            return _ssmFile.GetPath();
        }

        #region SSM Init
        private void InitSSM()
        {
            _ssm.Open();
            _ssm.AddString("CreatedVersion-FAES_GUI", Program.GetVersion(), "The version of FileAES_GUI used when the SSM file was created", "FAES_MetaData");
            _ssm.AddString("CreatedVersion-FAES", FileAES_Utilities.GetVersion(), "The version of FAES used when the SSM file was created.", "FAES_MetaData");

            _ssm.AddString("LastAccessedVersion-FAES_GUI", Program.GetVersion(), "The version of FileAES_GUI used when the SSM file was created", "FAES_MetaData");
            _ssm.AddString("LastAccessedVersion-FAES", FileAES_Utilities.GetVersion(), "The version of FAES used when the SSM file was created.", "FAES_MetaData");

            _ssm.AddBoolean("FullInstall", false, "If the current FAES_GUI install is a 'Full Install'", "FAES_Install");
            _ssm.AddBoolean("AssociateFileTypes", false, "Associate file types with FileAES_GUI.", "FAES_Install");
            _ssm.AddBoolean("StartMenuShortcuts", false, "Create Start Menu shortcut for FileAES_GUI.", "FAES_Install");
            _ssm.AddBoolean("ContextMenus", false, "Add FileAES_GUI to context menus.", "FAES_Install");

            _ssm.AddString("LogPath", "log\\{default}", "The path of the log file. '{default}' will be replaced with the auto-generated log filename.", "FAES_GUI-Paths");
            _ssm.AddUInt32("CryptoStreamBufferSize", 1048576, "The size of the CryptoStream Buffer.", "FAES_Configs");
            _ssm.AddBoolean("LogToFile", false, "Always output FAES log to a file.", "FAES_Configs");
            _ssm.AddBoolean("DeveloperMode", false, "Toggle verbose logging and ability to open the Developer Console.", "FAES_Configs");
            _ssm.AddString("Branch", Program.GetBuild(), "The branch used to determine updates.", "FAES_Configs");
            _ssm.AddBoolean("SkipUpdates", false, "Toggles whether a new update warning should be shown.", "FAES_Configs");
            _ssm.AddBoolean("UseOSTempPath", false, "Toggles whether the OS' Temp path should be used for encryptions.", "FAES_Configs");
            _ssm.Close();
        }

        private void UpdateSSM()
        {
            _ssm.Open();
            _ssm.SetString("LastAccessedVersion-FAES_GUI", Program.GetVersion());
            _ssm.SetString("LastAccessedVersion-FAES", FileAES_Utilities.GetVersion());
            _ssm.Close();
        }
        #endregion
        #region Settings (Getters/Setters)
        public string GetBranch()
        {
            _ssm.Open();
            string returnVal = _ssm.GetString("Branch");
            _ssm.Close();

            if (returnVal.ToLower() == "dev") return "dev";
            else if (returnVal.ToLower() == "beta") return "beta";
            else return "stable";
        }

        public bool ResetBranch()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetString("Branch", Program.GetBuild());

            Logging.Log(String.Format("ResetBranch: {0}", Program.GetBuild()), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetBranch(string branch)
        {
            string b = "stable";
            if (branch.ToLower() == "dev") b = "dev";
            else if (branch.ToLower() == "beta") b = "beta";
            else b = "stable";

            Logging.Log(String.Format("SetBranch: {0}", b), Severity.DEBUG);

            _ssm.Open();
            bool returnVal = _ssm.SetString("Branch", b);
            _ssm.Close();
            return returnVal;
        }

        public string GetLogPath()
        {
            _ssm.Open();
            string returnVal = _ssm.GetString("LogPath");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetLogPath()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetString("LogPath", "log\\{default}");

            Logging.Log(String.Format("ResetLogPath: {0}", "log\\{default}"), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetLogPath(string path)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetString("LogPath", path.Replace('/', '\\').TrimEnd('/', '\\'));

            Logging.Log(String.Format("SetLogPath: {0}", path), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetFullInstall()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("FullInstall");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetFullInstall()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("FullInstall", false);

            Logging.Log(String.Format("ResetFullInstall: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetFullInstall(bool fullInstall)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("FullInstall", fullInstall);

            Logging.Log(String.Format("SetFullInstall: {0}", fullInstall), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetAssociateFileTypes()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("AssociateFileTypes");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetAssociateFileTypes()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("AssociateFileTypes", false);

            Logging.Log(String.Format("ResetAssociateFileTypes: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetAssociateFileTypes(bool associate)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("AssociateFileTypes", associate);

            Logging.Log(String.Format("SetAssociateFileTypes: {0}", associate), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetStartMenuShortcuts()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("StartMenuShortcuts");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetStartMenuShortcuts()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("StartMenuShortcuts", false);

            Logging.Log(String.Format("ResetStartMenuShortcuts: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetStartMenuShortcuts(bool shortcuts)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("StartMenuShortcuts", shortcuts);

            Logging.Log(String.Format("SetStartMenuShortcuts: {0}", shortcuts), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetContextMenus()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("ContextMenus");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetContextMenus()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("ContextMenus", false);

            Logging.Log(String.Format("ResetContextMenus: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetContextMenus(bool contextMenus)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("ContextMenus", contextMenus);

            Logging.Log(String.Format("SetContextMenus: {0}", contextMenus), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetSkipUpdates()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("SkipUpdates");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetSkipUpdates()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("SkipUpdates", false);

            Logging.Log(String.Format("ResetSkipUpdates: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetSkipUpdates(bool skipUpdates)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("SkipUpdates", skipUpdates);

            Logging.Log(String.Format("SetSkipUpdates: {0}", skipUpdates), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetUseOSTemp()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("UseOSTempPath");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetUseOSTemp()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("UseOSTempPath", false);

            Logging.Log(String.Format("ResetSkipUpdates: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetUseOSTemp(bool useOSTemp)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("UseOSTempPath", useOSTemp);

            Logging.Log(String.Format("SetUseOSTemp: {0}", useOSTemp), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetDevMode()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("DeveloperMode");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetDevMode()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("DeveloperMode", false);

            Logging.Log(String.Format("ResetDeveloperMode: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetDevMode(bool devMode)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("DeveloperMode", devMode);

            Logging.Log(String.Format("SetDeveloperMode: {0}", devMode), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public UInt32 GetCryptoStreamBufferSize()
        {
            _ssm.Open();
            UInt32 returnVal = _ssm.GetUInt32("CryptoStreamBufferSize");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetCryptoStreamBufferSize()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetUInt32("CryptoStreamBufferSize", 1048576);

            Logging.Log(String.Format("ResetCryptoStreamBufferSize: {0}", 1048576), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetCryptoStreamBufferSize(UInt32 size)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetUInt32("CryptoStreamBufferSize", size);
            FileAES_Utilities.SetCryptoStreamBuffer(size);

            Logging.Log(String.Format("SetCryptoStreamBufferSize: {0}", size), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool GetLogToFile()
        {
            _ssm.Open();
            bool returnVal = _ssm.GetBoolean("LogToFile");
            _ssm.Close();
            return returnVal;
        }

        public bool ResetLogToFile()
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("LogToFile", false);

            Logging.Log(String.Format("ResetLogToFile: {0}", false), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public bool SetLogToFile(bool state)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("LogToFile", state);

            Logging.Log(String.Format("SetLogToFile: {0}", state), Severity.DEBUG);

            _ssm.Close();
            return returnVal;
        }

        public DateTime GetLastModified()
        {
            return _ssmFile.GetLastModified();
        }
        #endregion
    }

    public class ProgramManager
    {
        protected InstallType _installType = InstallType.AutoDetect;
        protected static string _portablePath = AppDomain.CurrentDomain.BaseDirectory;
        protected static string _fullInstallPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mullak99", "FileAES");
        protected static string _appDataPath = _portablePath;

        protected string[] _currentSubDirsAppData = new string[] { "config", "logs" };
        protected SettingsManager _settingsManager;

        private long _ssmLastModifiedTime;
        private bool _ssmCachedLogToFile, _ssmCachedDevMode, _ssmCachedSkipUpdates, _ssmCachedFullInstall, _ssmCachedAssociateFileTypes, _ssmCachedStartMenuShortcuts, _ssmCachedContextMenus, _ssmCachedOSTemp;
        private string _ssmCachedLogPath, _ssmCachedBranch;
        private UInt32 _ssmCachedCsBuffer;

        public ProgramManager(InstallType installType = InstallType.AutoDetect)
        {
            _installType = installType;

            CreateAppDataDirectory();
            _settingsManager = new SettingsManager();
            _settingsManager.SetFullInstall(_ssmCachedFullInstall);

            CacheVariables();
        }

        private void CacheVariables()
        {
            _ssmLastModifiedTime = File.GetLastWriteTimeUtc(_settingsManager.GetPath()).ToFileTimeUtc();

            _ssmCachedFullInstall = _settingsManager.GetFullInstall();
            _ssmCachedAssociateFileTypes = _settingsManager.GetAssociateFileTypes();
            _ssmCachedStartMenuShortcuts = _settingsManager.GetStartMenuShortcuts();
            _ssmCachedContextMenus = _settingsManager.GetContextMenus();
            _ssmCachedDevMode = _settingsManager.GetDevMode();
            _ssmCachedLogToFile = _settingsManager.GetLogToFile();
            _ssmCachedLogPath = _settingsManager.GetLogPath();
            _ssmCachedCsBuffer = _settingsManager.GetCryptoStreamBufferSize();
            _ssmCachedBranch = _settingsManager.GetBranch();
            _ssmCachedSkipUpdates = _settingsManager.GetSkipUpdates();
            _ssmCachedOSTemp = _settingsManager.GetUseOSTemp();
        }

        private void CreateAppDataDirectory()
        {
            switch (_installType)
            {
                case InstallType.AutoDetect:
                    {
                        if (File.Exists(Path.Combine(GetPotentialConfigPath(_portablePath), SettingsManager._programSettingsFileName)))
                        {
                            _installType = InstallType.PortableInstall;
                            CreateAppDataDirectory();
                            break;
                        }
                        else if (File.Exists(Path.Combine(GetPotentialConfigPath(_fullInstallPath), SettingsManager._programSettingsFileName)))
                        {
                            _installType = InstallType.FullInstall;
                            CreateAppDataDirectory();
                            break;
                        }
                        else
                        {
                            _installType = InstallType.PortableInstall;
                            CreateAppDataDirectory();
                            break;
                        }
                    }
                case InstallType.PortableInstall:
                    {
                        _ssmCachedFullInstall = false;
                        _appDataPath = _portablePath;
                        CreateDirectory(GetConfigPath());
                        break;
                    }
                case InstallType.FullInstall:
                    {
                        _ssmCachedFullInstall = true;
                        _appDataPath = _fullInstallPath;

                        foreach (string subDir in _currentSubDirsAppData)
                        {
                            CreateDirectory(Path.Combine(_appDataPath, subDir));
                        }
                        break;
                    }
            }
        }

        private bool AreCachedVarsLatest()
        {
            return _ssmLastModifiedTime == File.GetLastWriteTimeUtc(_settingsManager.GetPath()).ToFileTimeUtc();
        }

        private void EnsureCachedVarsAreUpdated()
        {
            if (!AreCachedVarsLatest()) CacheVariables();
        }

        #region SettingsManager (Getters/Setters)
        public bool GetLogToFile()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedLogToFile;
        }

        public bool GetFullInstall()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedFullInstall;
        }

        public bool GetAssociateFileTypes()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedAssociateFileTypes;
        }

        public bool GetStartMenuShortcuts()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedStartMenuShortcuts;
        }

        public bool GetContextMenus()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedContextMenus;
        }

        public bool GetDevMode()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedDevMode;
        }

        public bool GetSkipUpdates()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedSkipUpdates;
        }

        public bool GetUseOSTemp()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedOSTemp;
        }

        public string GetLogPath()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedLogPath;
        }

        public string GetBranch()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedBranch;
        }

        public UInt32 GetCryptoStreamBufferSize()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedCsBuffer;
        }

        public bool SetLogToFile(bool logToFile)
        {
            bool changed = _settingsManager.SetLogToFile(logToFile);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetFullInstall(bool fullInstall)
        {
            bool changed = _settingsManager.SetFullInstall(fullInstall);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetAssociateFileTypes(bool associateFileTypes)
        {
            bool changed = _settingsManager.SetAssociateFileTypes(associateFileTypes);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetStartMenuShortcuts(bool startMenuShortcuts)
        {
            bool changed = _settingsManager.SetStartMenuShortcuts(startMenuShortcuts);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetContextMenus(bool contextMenus)
        {
            bool changed = _settingsManager.SetContextMenus(contextMenus);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetDevMode(bool devMode)
        {
            bool changed = _settingsManager.SetDevMode(devMode);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetSkipUpdates(bool skipUpdates)
        {
            bool changed = _settingsManager.SetSkipUpdates(skipUpdates);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetUseOSTemp(bool useOSTemp)
        {
            bool changed = _settingsManager.SetUseOSTemp(useOSTemp);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetLogPath(string logPath)
        {
            bool changed = _settingsManager.SetLogPath(logPath);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetBranch(string branch)
        {
            bool changed = _settingsManager.SetBranch(branch);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetCryptoStreamBufferSize(UInt32 bufferSize)
        {
            bool changed = _settingsManager.SetCryptoStreamBufferSize(bufferSize);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetLogToFile()
        {
            bool changed = _settingsManager.ResetLogToFile();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetFullInstall()
        {
            bool changed = _settingsManager.ResetFullInstall();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetDevMode()
        {
            bool changed = _settingsManager.ResetDevMode();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetSkipUpdates()
        {
            bool changed = _settingsManager.ResetSkipUpdates();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetUseOSTemp()
        {
            bool changed = _settingsManager.ResetUseOSTemp();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetLogPath()
        {
            bool changed = _settingsManager.ResetLogPath();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetBranch()
        {
            bool changed = _settingsManager.ResetBranch();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool ResetCryptoStreamBufferSize()
        {
            bool changed = _settingsManager.ResetCryptoStreamBufferSize();
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public void ResetAllSettings()
        {
            ResetDevMode();
            ResetLogToFile();
            ResetLogPath();
            ResetBranch();
            ResetCryptoStreamBufferSize();
            ResetSkipUpdates();
            ResetUseOSTemp();
        }
        #endregion

        public static string GetAppDataPath()
        {
            return _appDataPath;
        }

        public static string GetConfigPath()
        {
            return GetPotentialConfigPath(_appDataPath);
        }

        private static string GetPotentialConfigPath(string path)
        {
            return Path.Combine(path, "config");
        }

        private static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }

        public enum InstallType
        {
            AutoDetect,
            FullInstall,
            PortableInstall
        };
    }
}
