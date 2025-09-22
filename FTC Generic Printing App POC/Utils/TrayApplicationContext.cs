using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace FTC_Generic_Printing_App_POC
{
    public class TrayApplicationContext : ApplicationContext
    {
        #region Fields
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private Configuration configForm;
        private bool isListening = false;
        private FirebaseService _firebaseService;
        #endregion

        #region Initialization
        public TrayApplicationContext()
        {
            InitializeTrayIcon();
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
        #endregion

        #region Core Methods
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

        private void StartListening()
        {
            try
            {
                if (FirebaseService != null && !FirebaseService.IsListening)
                {
                    // Check if the totem configuration is set up correctly
                    bool canStartListening = false;
                    try
                    {
                        FirebaseService.BuildDocumentPath();
                        canStartListening = true;
                    }
                    catch (InvalidOperationException)
                    {
                        ShowTrayMessage("No se puede iniciar la escucha: Configuración de totem inválida");
                        return;
                    }

                    if (canStartListening)
                    {
                        Task.Run(async () =>
                        {
                            try
                            {
                                await FirebaseService.StartListeningAsync();

                                if (FirebaseService.IsListening)
                                {
                                    if (configForm != null && !configForm.IsDisposed)
                                    {
                                        configForm.Invoke(new Action(() =>
                                        {
                                            isListening = true;
                                            trayMenu.Items[1].Text = "Finalizar escuchar";
                                            trayIcon.Text = "FTC Generic Printing App - Escuchando";
                                            ShowTrayMessage("Escuchando eventos en Firebase");
                                        }));
                                    }
                                    else
                                    {
                                        var syncContext = SynchronizationContext.Current;
                                        if (syncContext != null)
                                        {
                                            syncContext.Post(_ =>
                                            {
                                                isListening = true;
                                                trayMenu.Items[1].Text = "Finalizar escuchar";
                                                trayIcon.Text = "FTC Generic Printing App - Escuchando";
                                                ShowTrayMessage("Escuchando eventos en Firebase");
                                            }, null);
                                        }
                                    }
                                }
                                else
                                {
                                    ShowTrayMessage("No se pudo iniciar la escucha en Firebase");
                                }
                            }
                            catch (Exception ex)
                            {
                                AppLogger.LogError("Error starting Firebase listener", ex);

                                if (configForm != null && !configForm.IsDisposed)
                                {
                                    configForm.Invoke(new Action(() =>
                                    {
                                        ShowTrayMessage("Error al iniciar escucha en Firebase");
                                    }));
                                }
                                else
                                {
                                    var syncContext = SynchronizationContext.Current;
                                    if (syncContext != null)
                                    {
                                        syncContext.Post(_ => ShowTrayMessage("Error al iniciar escucha en Firebase"), null);
                                    }
                                }
                            }
                        });
                    }
                }
                else if (FirebaseService != null && FirebaseService.IsListening)
                {
                    isListening = true;
                    trayMenu.Items[1].Text = "Finalizar escuchar";
                    trayIcon.Text = "FTC Generic Printing App - Escuchando";
                    ShowTrayMessage("Ya está escuchando eventos en Firebase");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in StartListening", ex);
                ShowTrayMessage("Error al iniciar escucha en Firebase");
            }
        }

        private void StopListening()
        {
            try
            {
                if (FirebaseService != null && FirebaseService.IsListening)
                {
                    FirebaseService.StopListening();
                }

                isListening = false;
                trayMenu.Items[1].Text = "Escuchar evento";
                trayIcon.Text = "FTC Generic Printing App - Finalizado";
                ShowTrayMessage("Escuchar evento finalizado");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in StopListening", ex);
                ShowTrayMessage("Error al finalizar escucha en Firebase");
            }
        }

        private void ShowTrayMessage(string message)
        {
            trayIcon.ShowBalloonTip(2000, "FTC Printing App", message, ToolTipIcon.Info);
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
            get => _firebaseService;
            set
            {
                _firebaseService = value;

                // Update UI based on current FirebaseService state
                if (_firebaseService != null && _firebaseService.IsListening)
                {
                    isListening = true;
                    if (trayMenu != null && trayMenu.Items.Count > 1)
                    {
                        trayMenu.Items[1].Text = "Finalizar escuchar";
                        trayIcon.Text = "FTC Generic Printing App - Escuchando";
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FirebaseService?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}