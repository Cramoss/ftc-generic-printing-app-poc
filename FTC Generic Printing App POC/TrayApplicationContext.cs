using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    public class TrayApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private Configuration configForm;
        private bool isListening = false;

        public TrayApplicationContext()
        {
            InitializeTrayIcon();
            StartListening();
        }

        private void InitializeTrayIcon()
        {
            // Create the tray menu
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Configuración", null, ShowConfiguration);
            trayMenu.Items.Add("Escuchar evento", null, ToggleListening);
            trayMenu.Items.Add("-"); // Separator
            trayMenu.Items.Add("Salir", null, ExitApplication);

            // TODO: Update default Windows icon.
            // Create the tray icon
            trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Application,
                ContextMenuStrip = trayMenu,
                Text = "FTC Generic Printing App",
                Visible = true
            };

            // Double-click to show configuration
            trayIcon.DoubleClick += ShowConfiguration;
        }

        private void ShowConfiguration(object sender, EventArgs e)
        {
            if (configForm == null || configForm.IsDisposed)
            {
                configForm = new Configuration();
            }

            configForm.Show();
            configForm.WindowState = FormWindowState.Normal;
            configForm.BringToFront();
        }

        private void ToggleListening(object sender, EventArgs e)
        {
            if (isListening)
            {
                StopListening();
            }
            else
            {
                StartListening();
            }
        }

        // TODO: Implement Firebase start listening.
        // Note: This may get deleted since it is kind of redundant.
        private void StartListening()
        {
            isListening = true;
            trayMenu.Items[1].Text = "Finalizar escuchar";
            trayIcon.Text = "FTC Generic Printing App - Escuchando";

            ShowTrayMessage("Escuchando");
        }


        // TODO: Implement Firebase stop listening.
        // Note: This may get deleted since it is kind of redundant.
        private void StopListening()
        {
            isListening = false;
            trayMenu.Items[1].Text = "Escuchar evento";
            trayIcon.Text = "FTC Generic Printing App - Finalizado";

            ShowTrayMessage("Escuchar evento finalizado");
        }

        private void ShowTrayMessage(string message)
        {
            trayIcon.ShowBalloonTip(2000, "FTC Printing App", message, ToolTipIcon.Info);
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            // Clean up
            trayIcon.Visible = false;
            trayIcon.Dispose();

            Application.Exit();
        }
    }
}