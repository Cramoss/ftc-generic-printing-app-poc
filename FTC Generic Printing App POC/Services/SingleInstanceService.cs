using System;
using System.Threading;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Services
{
    // Service to ensure only one instance of the application runs at the same time
    public class SingleInstanceService : IDisposable
    {
        #region Fields
        private Mutex _mutex = null;
        private readonly string _mutexName;
        private bool _ownsMutex = false;
        #endregion

        #region Initialization
        public SingleInstanceService(string mutexName)
        {
            _mutexName = mutexName ?? throw new ArgumentNullException(nameof(mutexName));
        }
        #endregion

        #region Core Methods
        // Checks if another instance of this application is already running by trying to create a named mutex.
        // If the mutex already exists, it means another instance is already running.
        public bool TryAcquireSingleInstance()
        {
            try
            {
                _mutex = new Mutex(true, _mutexName, out _ownsMutex);
                return _ownsMutex;
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error checking for single instance: {ex.Message}", ex);
                return true;
            }
        }

        public void ShowAlreadyRunningMessage()
        {
            MessageBox.Show("Una instancia de la aplicación ya está activa. " +
                            "Por favor, ciérrala antes de iniciar otra e intenta nuevamente.",
                            "FTC Generic Printing App",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
        #endregion

        #region IDisposable Implementation
        // Releases the mutex when the service is closed/disposed
        public void Dispose()
        {
            if (_mutex != null)
            {
                if (_ownsMutex)
                {
                    _mutex.ReleaseMutex();
                }
                _mutex.Dispose();
                _mutex = null;
            }
        }
        #endregion
    }
}
