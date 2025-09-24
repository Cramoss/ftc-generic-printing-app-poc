using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace FTC_Generic_Printing_App_POC
{
    public static class AppLogger
    {
        #region Fields
        private static readonly Logger logger;
        private static readonly string appName = "FTC Generic Printing App POC";
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
                    Layout = "${longdate} ${level:uppercase=true} ${message} ${exception:format=tostring}"
                };

                // Add a console target for debugging purposes
                var consoleTarget = new ConsoleTarget("consoleTarget")
                {
                    Layout = "${longdate} ${level:uppercase=true} ${message} ${exception:format=tostring}"
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
                LogManager.GetCurrentClassLogger().Info($"Logging initialized. Log files will be written to {LogDirectory}");
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
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
                logger.Error(ex, message);
            else
                logger.Error(message);
        }

        public static void LogWarning(string message)
        {
            logger.Warn(message);
        }

        public static void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public static void LogFirebaseEvent(string eventType, string details)
        {
            logger.Info($"FIREBASE-{eventType}: {details}");
        }

        public static void LogPrintEvent(string eventType, string details)
        {
            logger.Info($"PRINT-{eventType}: {details}");
        }
        #endregion
    }
}
#endregion