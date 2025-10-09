using FTC_Generic_Printing_App_POC.Forms.Settings;
using FTC_Generic_Printing_App_POC.Services;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Manager
{
    public class TrayApplicationManager : ApplicationContext
    {
        #region Fields
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private SettingsForm settingsForm;
        #endregion

        #region Initialization
        public TrayApplicationManager()
        {
            InitializeTrayIcon();
            NotificationService.Initialize(trayIcon);
            FirebaseManager.Instance.ListeningStateChanged += OnListeningStateChanged;
        }

        private void InitializeTrayIcon()
        {
            // Create the tray menu
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Configuración", null, ShowSettings);
            trayMenu.Items.Add("Escuchar evento", null, ToggleListening);
            trayMenu.Items.Add("Consola", null, ShowDebugConsole);
            trayMenu.Items.Add("-");
            trayMenu.Items.Add("Salir", null, ExitApplication);

            // Create the tray icon
            trayIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.app_icon,
                ContextMenuStrip = trayMenu,
                Text = "FTC Generic Printing App POC", // TODO: Remove POC on final version
                Visible = true
            };

            // Double-click to show settings
            trayIcon.DoubleClick += ShowSettings;
        }
        #endregion

        #region Event Handlers
        private void OnListeningStateChanged(object sender, bool isListening)
        {
            try
            {
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

        private void ShowSettings(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new SettingsForm();
            }

            settingsForm.Show();
            settingsForm.WindowState = FormWindowState.Normal;
            settingsForm.BringToFront();
        }

        private async void ToggleListening(object sender, EventArgs e)
        {
            if (FirebaseManager.Instance.IsListening)
            {
                FirebaseManager.Instance.StopListening();
            }
            else
            {
                await FirebaseManager.Instance.StartListeningAsync();
            }
        }

        private void UpdateTrayUI(bool isListening)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in UpdateTrayUI", ex);
            }
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            FTC_Generic_Printing_App_POC.Manager.DebugManager.Instance.Dispose();

            if (FirebaseManager.Instance.IsListening)
            {
                FirebaseManager.Instance.StopListening();
            }

            // Hide and dispose tray icon
            trayIcon.Visible = false;
            trayIcon.Dispose();

            this.Dispose();

            // Exit the application
            Application.Exit();

            // Force termination of any background threads
            Environment.Exit(0);
        }

        private void ShowDebugConsole(object sender, EventArgs e)
        {
            // Toggle the debug console visibility
            DebugManager.Instance.ToggleConsole();
        }
        #endregion

        public FirebaseService FirebaseService
        {
            set
            {
                FirebaseManager.Instance.Initialize(value);

                // Updates the UI based on current Firebase state
                UpdateTrayUI(FirebaseManager.Instance.IsListening);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FTC_Generic_Printing_App_POC.Manager.DebugManager.Instance.Dispose();

                if (FirebaseManager.Instance.IsListening)
                {
                    FirebaseManager.Instance.StopListening();
                }

                // Unsubscribe from event
                FirebaseManager.Instance.ListeningStateChanged -= OnListeningStateChanged;

                // Dispose the tray icon
                if (trayIcon != null)
                {
                    trayIcon.Dispose();
                    trayIcon = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}