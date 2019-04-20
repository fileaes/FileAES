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
        protected string _programSettingsPath;

        public SettingsManager()
        {
            _programSettingsPath = Path.Combine(Program.programManager.GetConfigPath(), _programSettingsFileName);
            _ssm = new SSM(new SSM_File(_programSettingsPath, SSM_File.Mode.SQLite));

            InitSSM();
            UpdateSSM();
        }

        #region SSM Init
        private void InitSSM()
        {
            _ssm.Open();
            _ssm.AddString("CreatedVersion-FAES_GUI", Program.GetVersion(), "The version of FileAES_GUI used when the SSM file was created", "FAES_MetaData");
            _ssm.AddString("CreatedVersion-FAES", FileAES_Utilities.GetVersion(), "The version of FAES used when the SSM file was created.", "FAES_MetaData");

            _ssm.AddString("LastAccessedVersion-FAES_GUI", Program.GetVersion(), "The version of FileAES_GUI used when the SSM file was created", "FAES_MetaData");
            _ssm.AddString("LastAccessedVersion-FAES", FileAES_Utilities.GetVersion(), "The version of FAES used when the SSM file was created.", "FAES_MetaData");

            _ssm.AddString("LogPath", "log\\{default}", "The path of the log file. '{default}' will be replaced with the auto-generated log filename.", "FAES_GUI-Paths");
            _ssm.AddUInt32("CryptoStreamBufferSize", 1048576, "The size of the CryptoStream Buffer.", "FAES_Configs");
            _ssm.AddBoolean("LogToFile", false, "Always output FAES log to a file.", "FAES_Configs");
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
        #endregion
    }

    public class ProgramManager
    {
        protected string _appDataPath;

        protected string[] _currentSubDirsAppData = new string[] { "config" };

        public ProgramManager()
        {
            CreateAppDataDirectory();
            //CacheVariables();
        }

        private void CacheVariables()
        {
            
        }

        private void CreateAppDataDirectory(bool useFullInstall = false)
        {
            if (useFullInstall)
                _appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mullak99", "FileAES");
            else
                _appDataPath = Path.Combine("");

            foreach (string subDir in _currentSubDirsAppData)
            {
                if (!Directory.Exists(Path.Combine(_appDataPath, subDir)))
                    Directory.CreateDirectory(Path.Combine(_appDataPath, subDir));
            }
        }

        public string GetAppDataPath()
        {
            return _appDataPath;
        }

        public string GetConfigPath()
        {
            return Path.Combine(_appDataPath, "config");
        }
    }
}
