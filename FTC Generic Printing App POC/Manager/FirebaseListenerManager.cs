using FTC_Generic_Printing_App_POC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Manager
{
    // Manager class to handle Firebase listener lifecycle and state
    public class FirebaseListenerManager
    {
        private static FirebaseListenerManager _instance;
        private static readonly object _lockObject = new object();

        public static FirebaseListenerManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new FirebaseListenerManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public event EventHandler<bool> ListeningStateChanged;

        private FirebaseService _firebaseService;

        private FirebaseListenerManager()
        {
        }

        public void Initialize(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public bool IsListening => _firebaseService?.IsListening ?? false;

        public async Task StartListeningAsync()
        {
            if (_firebaseService == null)
            {
                Services.NotificationService.ShowNotification("Firebase service not initialized", icon: ToolTipIcon.Error);
                return;
            }

            if (_firebaseService.IsListening)
            {
                Services.NotificationService.ShowNotification("Ya está escuchando eventos en Firebase");
                NotifyListeningStateChanged();
                return;
            }

            try
            {
                // Check if the current Totem Firebase path configuration is valid
                try
                {
                    _firebaseService.BuildDocumentPath();
                }
                catch (InvalidOperationException)
                {
                    Services.NotificationService.ShowNotification("No se puede iniciar la escucha: Configuración de totem inválida");
                    return;
                }

                Task.Run(async () =>
                {
                    try
                    {
                        await _firebaseService.ConnectListenerAsync();
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Background task for Firebase listener failed", ex);
                    }
                });

                // Wait a moment to allow the listener to start to update UI state.
                // Maybe not the best way, but Firebase SDK does not provide a callback for successful start.
                // Delay can be adjusted for slower machines, for 3000ms is good for now.
                await Task.Delay(3000);

                if (_firebaseService.IsListening)
                {
                    Services.NotificationService.ShowNotification("Escuchando eventos en Firebase");

                    NotifyListeningStateChanged();
                }
                else
                {
                    AppLogger.LogWarning("Firebase listener timeout. Did not start in the expected time");
                    Services.NotificationService.ShowNotification("No se pudo iniciar la escucha en Firebase");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error starting Firebase listener", ex);
                Services.NotificationService.ShowNotification("Error al iniciar escucha en Firebase", icon: ToolTipIcon.Error);
            }
        }

        public void StopListening()
        {
            if (_firebaseService == null || !_firebaseService.IsListening)
            {
                return;
            }

            try
            {
                _firebaseService.DisconnectListener();
                NotificationService.ShowNotification("Escuchar evento finalizado");
                NotifyListeningStateChanged();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error stopping Firebase listener", ex);
                NotificationService.ShowNotification("Error al finalizar escucha en Firebase", icon: ToolTipIcon.Error);
            }
        }

        public void ReloadConfiguration(bool autoStartListener = false)
        {
            if (_firebaseService != null)
            {
                AppLogger.LogInfo("Reloading totem configuration via FirebaseListenerManager");
                _firebaseService.ReloadTotemConfiguration(autoStartListener);
            }
            else
            {
                AppLogger.LogError("Cannot reload configuration. Firebase service is not initialized");
            }
        }

        private void NotifyListeningStateChanged()
        {
            try
            {
                bool currentState = _firebaseService?.IsListening ?? false;
                ListeningStateChanged?.Invoke(this, currentState);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in NotifyListeningStateChanged", ex);
            }
        }
    }
}
