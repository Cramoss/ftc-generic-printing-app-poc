using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    public class TrayApplicationContext : ApplicationContext
    {
        #region Fields
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private Configuration configForm;
        #endregion

        #region Initialization
        public TrayApplicationContext()
        {
            InitializeTrayIcon();
            NotificationService.Initialize(trayIcon);
            FirebaseListenerManager.Instance.ListeningStateChanged += OnListeningStateChanged;
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
                Text = "FTC Generic Printing App POC", // TODO: Remove POC on final version
                Visible = true
            };

            // Double-click to show configuration
            trayIcon.DoubleClick += ShowConfiguration;
        }
        #endregion

        #region Event Handlers
        private void OnListeningStateChanged(object sender, bool isListening)
        {
            try
            {
                AppLogger.LogInfo($"Received listening state changed event: {isListening}");

                // Forces UI updates happen on the UI thread
                if (trayMenu.InvokeRequired)
                {
                    trayMenu.BeginInvoke(new Action(() => UpdateTrayUI(isListening)));
                }
                else
                {
                    UpdateTrayUI(isListening);
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in OnListeningStateChanged", ex);
            }
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

        private async void ToggleListening(object sender, EventArgs e)
        {
            if (FirebaseListenerManager.Instance.IsListening)
            {
                FirebaseListenerManager.Instance.StopListening();
            }
            else
            {
                await FirebaseListenerManager.Instance.StartListeningAsync();
            }
        }

        private void UpdateTrayUI(bool isListening)
        {
            try
            {
                AppLogger.LogInfo($"Updating tray UI for listening state: {isListening}");

                if (isListening)
                {
                    trayMenu.Items[1].Text = "Finalizar escuchar";
                    trayIcon.Text = "FTC Generic Printing App - Escuchando";
                }
                else
                {
                    trayMenu.Items[1].Text = "Escuchar evento";
                    trayIcon.Text = "FTC Generic Printing App - Finalizado";
                }

                // Force a refresh of the tray menu
                trayMenu.Refresh();

                AppLogger.LogInfo($"Updated tray UI for listening state: {isListening}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in UpdateTrayUI", ex);
            }
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();

            Application.Exit();
        }
        #endregion

        public FirebaseService FirebaseService
        {
            set
            {
                FirebaseListenerManager.Instance.Initialize(value);

                // Updates the UI based on current Firebase state
                UpdateTrayUI(FirebaseListenerManager.Instance.IsListening);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unsubscribe from event
                FirebaseListenerManager.Instance.ListeningStateChanged -= OnListeningStateChanged;
                trayIcon?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}