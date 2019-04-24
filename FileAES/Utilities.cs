using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAES;
using SimpleSettingsManager;

namespace FAES_GUI
{
    public class SettingsManager
    {
        private const string _programSettingsFileName = "FileAES_ProgSettings.db";

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

            _ssm.AddString("LogPath", "log\\{default}", "The path of the log file. '{default}' will be replaced with the auto-generated log filename.", "FAES_GUI-Paths");
            _ssm.AddUInt32("CryptoStreamBufferSize", 1048576, "The size of the CryptoStream Buffer.", "FAES_Configs");
            _ssm.AddBoolean("LogToFile", false, "Always output FAES log to a file.", "FAES_Configs");
            _ssm.AddBoolean("DeveloperMode", false, "Toggle verbose logging and ability to open the Developer Console.", "FAES_Configs");
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
            _ssm.Close();
            return returnVal;
        }

        public bool SetLogPath(string path)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetString("LogPath", path.Replace('/', '\\').TrimEnd('/', '\\'));
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
            _ssm.Close();
            return returnVal;
        }

        public bool SetFullInstall(bool fullInstall)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("FullInstall", fullInstall);
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
            _ssm.Close();
            return returnVal;
        }

        public bool SetDevMode(bool devMode)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("DeveloperMode", devMode);
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
            _ssm.Close();
            return returnVal;
        }

        public bool SetCryptoStreamBufferSize(UInt32 size)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetUInt32("CryptoStreamBufferSize", size);
            FileAES_Utilities.SetCryptoStreamBuffer(size);
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
            _ssm.Close();
            return returnVal;
        }

        public bool SetLogToFile(bool state)
        {
            _ssm.Open();
            bool returnVal = _ssm.SetBoolean("LogToFile", state);
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
        protected static string _appDataPath;
        protected bool _fullInstall;

        protected string[] _currentSubDirsAppData = new string[] { "config", "logs" };
        protected SettingsManager _settingsManager;

        private long _ssmLastModifiedTime;
        private bool _ssmCachedLogToFile, _ssmCachedDevMode;
        private string _ssmCachedLogPath;
        private UInt32 _ssmCachedCsBuffer;

        public ProgramManager(bool fullInstall = false)
        {
            _fullInstall = fullInstall;

            CreateAppDataDirectory(_fullInstall);

            _settingsManager = new SettingsManager();
            _settingsManager.SetFullInstall(fullInstall);

            CacheVariables();
        }

        private void CacheVariables()
        {
            _ssmLastModifiedTime = File.GetLastWriteTimeUtc(_settingsManager.GetPath()).ToFileTimeUtc();

            _fullInstall = _settingsManager.GetFullInstall();
            _ssmCachedDevMode = _settingsManager.GetDevMode();
            _ssmCachedLogToFile = _settingsManager.GetLogToFile();
            _ssmCachedLogPath = _settingsManager.GetLogPath();
            _ssmCachedCsBuffer = _settingsManager.GetCryptoStreamBufferSize();
        }

        private void CreateAppDataDirectory(bool fullInstall)
        {
            if (fullInstall)
            {
                _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mullak99", "FileAES");

                foreach (string subDir in _currentSubDirsAppData)
                {
                    CreateDirectory(Path.Combine(_appDataPath, subDir));
                }
            }
            else
            {
                _appDataPath = Path.Combine("");

                CreateDirectory(GetConfigPath());
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
            return _fullInstall;
        }

        public bool GetDevMode()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedDevMode;
        }

        public string GetLogPath()
        {
            EnsureCachedVarsAreUpdated();
            return _ssmCachedLogPath;
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

        public bool SetDevMode(bool devMode)
        {
            bool changed = _settingsManager.SetDevMode(devMode);
            if (changed) EnsureCachedVarsAreUpdated();
            return changed;
        }

        public bool SetLogPath(string logPath)
        {
            bool changed = _settingsManager.SetLogPath(logPath);
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

        public bool ResetLogPath()
        {
            bool changed = _settingsManager.ResetLogPath();
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
            ResetCryptoStreamBufferSize();
        }
        #endregion

        public static string GetAppDataPath()
        {
            return _appDataPath;
        }

        public static string GetConfigPath()
        {
            return Path.Combine(_appDataPath, "config");
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
    }
}
