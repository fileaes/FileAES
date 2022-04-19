using FAES;
using System;
using System.IO;

namespace FAES_GUI
{
    internal class Logging
    {
        private static string _lastLogPath;
        private static bool _wasLogPathSuccess = true;
        private static string _logPath;

        public static void Log(string log, Severity severity = Severity.INFO)
        {
            string rawLog;
            switch (severity)
            {
                case Severity.DEBUG:
                    if (FileAES_Utilities.GetVerboseLogging())
                    {
                        rawLog = $"[DEBUG] {log}";
                        Console.WriteLine(rawLog);
                        WriteToLogFile(rawLog);
                    }
                    break;

                case Severity.WARN:
                    rawLog = $"[WARN] {log}";
                    Console.WriteLine(rawLog);
                    WriteToLogFile(rawLog);
                    break;

                case Severity.ERROR:
                    rawLog = $"[ERROR] {log}";
                    Console.WriteLine(rawLog);
                    WriteToLogFile(rawLog);
                    break;

                default:
                    rawLog = $"[INFO] {log}";
                    Console.WriteLine(rawLog);
                    WriteToLogFile(rawLog);
                    break;
            }
        }

        private static void LogPathInit()
        {
            if (string.IsNullOrWhiteSpace(_logPath) || _lastLogPath != _logPath)
                _logPath = Utilities.CreateLogFile(false);
        }

        private static void WriteToLogFile(string log)
        {
            if (Program.programManager != null && Program.programManager.GetLogToFile())
            {
                LogPathInit();
                if (_wasLogPathSuccess)
                {
                    _lastLogPath = _logPath;

                    try
                    {
                        File.AppendAllText(_logPath, log + "\r\n");
                        _wasLogPathSuccess = true;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        _wasLogPathSuccess = false;
                        Log($"You do not have permission to write a log file to this location ({_lastLogPath})!", Severity.ERROR);
                    }
                    catch (Exception e)
                    {
                        _wasLogPathSuccess = false;
                        Log($"An unknown error occurred when writing to the log file ({_lastLogPath})! Exception: {e}", Severity.ERROR);
                    }
                }
            }
        }
    }

    internal enum Severity
    {
        DEBUG,
        INFO,
        WARN,
        ERROR
    };
}
