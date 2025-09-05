using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace FTC_Generic_Printing_App_POC
{
    public static class AppLogger
    {
        private static readonly Logger logger;

        static AppLogger()
        {
            if (LogManager.Configuration == null)
            {
                ConfigureDefaultLogging();
            }
            
            logger = LogManager.GetCurrentClassLogger();
        }

        private static void ConfigureDefaultLogging()
        {
            try
            {
                // Create logs directory if it doesn't exist
                string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                var config = new LoggingConfiguration();

                var fileTarget = new FileTarget("fileTarget")
                {
                    FileName = Path.Combine(logDirectory, "ftc-app-${shortdate}.log"),
                    Layout = "${longdate} ${level:uppercase=true} ${logger} ${message} ${exception:format=tostring}"
                };

                config.AddTarget(fileTarget);

                // Add rule for all loggers
                var rule = new LoggingRule("*", LogLevel.Debug, fileTarget);
                config.LoggingRules.Add(rule);

                LogManager.Configuration = config;                
                LogManager.GetCurrentClassLogger().Info("Using default NLog configuration. App.config file may be missing");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to configure default logging: {ex.Message}");
            }
        }

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
    }
}
