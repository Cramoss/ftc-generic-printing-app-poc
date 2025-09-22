using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void ShowNotification(string message, string title = "FTC Printing App", ToolTipIcon icon = ToolTipIcon.Info)
        {
            if (_trayIcon != null)
            {
                _trayIcon.ShowBalloonTip(2000, title, message, icon);
            }
        }
    }
}
