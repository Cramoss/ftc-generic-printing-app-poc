using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTC_Generic_Printing_App_POC
{
    public static class AppLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
