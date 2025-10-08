using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Services
{
    public class NotificationService
    {
        private static NotifyIcon _trayIcon;

        public static void Initialize(NotifyIcon trayIcon)
        {
            _trayIcon = trayIcon;
        }

        // TODO: Remove POC from notification title
        public static void ShowNotification(string message, string title = "FTC Printing App POC", ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (_trayIcon != null)
            {
                _trayIcon.ShowBalloonTip(2000, title, message, icon);
            }
        }
    }
}
