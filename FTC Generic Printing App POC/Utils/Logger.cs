using System;
using System.IO;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace FTC_Generic_Printing_App_POC.Utils
{
    public static class AppLogger
    {
        #region Fields
        private static readonly Logger logger;
        private static readonly string appName = "FTC Generic Printing App POC"; // TODO: Remove POC on final version
        private static bool debugConsoleInitialized = false;
        #endregion

        #region Properties
        private static string LogDirectory
        {
            get
            {
                string logDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    appName,
                    "logs");

                if (!Directory.Exists(logDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(logDirectory);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to create log directory: {ex.Message}");
                    }
                }

                return logDirectory;
            }
        }
        #endregion

        #region Initialization
        static AppLogger()
        {
            if (LogManager.Configuration == null)
            {
                ConfigureDefaultLogging();
            }

            logger = LogManager.GetCurrentClassLogger();
        }
        #endregion

        #region Core Methods
        private static void ConfigureDefaultLogging()
        {
            try
            {
                // Create a new logging configuration
                var config = new LoggingConfiguration();

                var fileTarget = new FileTarget("fileTarget")
                {
                    FileName = Path.Combine(LogDirectory, appName + "-${shortdate}.log"),
                    Layout = "${longdate} ${level:uppercase=true} ${message} ${exception:format=tostring}",
                    Encoding = Encoding.UTF8,
                };

                // Add a console target for debugging purposes
                var consoleTarget = new ConsoleTarget("consoleTarget")
                {
                    Layout = "${longdate} ${level:uppercase=true} ${message} ${exception:format=tostring}",
                    Encoding = Encoding.UTF8
                };

                config.AddTarget(fileTarget);
                config.AddTarget(consoleTarget);

                // Add rules for all loggers
                var fileRule = new LoggingRule("*", LogLevel.Debug, fileTarget);
                var consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
                config.LoggingRules.Add(fileRule);
                config.LoggingRules.Add(consoleRule);

                LogManager.Configuration = config;

                // Log initialization message
                LogManager.GetCurrentClassLogger().Info($"Logging initialized with UTF-8 encoding. Log files will be written to {LogDirectory}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to configure default logging: {ex.Message}");
            }
        }

        // Method to get the current log file path - useful for troubleshooting
        public static string GetCurrentLogFilePath()
        {
            return Path.Combine(LogDirectory, $"{appName}-{DateTime.Now:yyyy-MM-dd}.log");
        }

        #region Helper Methods
        public static void LogInfo(string message)
        {
            logger.Info(message);
            ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} INFO {message}");
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
            {
                logger.Error(ex, message);
                ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} ERROR {message} - {ex}");
            }
            else
            {
                logger.Error(message);
                ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} ERROR {message}");
            }
        }

        public static void LogWarning(string message)
        {
            logger.Warn(message);
            ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} WARN {message}");
        }

        public static void LogDebug(string message)
        {
            logger.Debug(message);
            ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} DEBUG {message}");
        }

        public static void LogFirebaseEvent(string eventType, string details)
        {
            logger.Info($"FIREBASE-{eventType}: {details}");
            ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} FIREBASE-{eventType}: {details}");
        }

        public static void LogPrintEvent(string eventType, string details)
        {
            logger.Info($"PRINT-{eventType}: {details}");
            ForwardToDebugConsole($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} PRINT-{eventType}: {details}");
        }

        private static void ForwardToDebugConsole(string logMessage)
        {
            try
            {
                // Prevent initialization during logging to avoid circular references
                if (debugConsoleInitialized)
                {
                    Manager.DebugManager.Instance?.AppendLog(logMessage);
                }
            }
            catch
            {
                // Ignore errors in debug console forwarding
            }
        }
        #endregion
    }
}
#endregion