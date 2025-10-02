using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    static class Program
    {
        public static TrayApplicationContext TrayContext { get; private set; }
        private const string AppMutexName = "FTC_Generic_Printing_App_POC_SingleInstanceMutex";

        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create the single instance service
            using (var singleInstanceService = new SingleInstanceService(AppMutexName))
            {
                // Check if another instance is already running
                if (!singleInstanceService.TryAcquireSingleInstance())
                {
                    singleInstanceService.ShowAlreadyRunningMessage();
                    return;
                }

                try
                {
                    AppLogger.LogInfo("Application starting");

                    // Initialize configuration. Ensures default values are in app.config file.
                    ConfigurationManager.InitializeConfiguration();

                    FirebaseService firebaseService = new FirebaseService();
                    FirebaseListenerManager.Instance.Initialize(firebaseService);

                    TrayContext = new TrayApplicationContext();
                    TrayContext.FirebaseService = firebaseService;

                    bool configValid = ConfigurationManager.IsConfigurationValid(firebaseService.GetCurrentConfiguration());

                    if (configValid)
                    {
                        // If we put await, then the UI thread gets locked.
                        // Maybe it can be resolved on the future but it is not necessary for now.
                        FirebaseListenerManager.Instance.StartListeningAsync();
                    }
                    else
                    {
                        AppLogger.LogWarning("Firebase listener not started: Invalid Totem configuration");
                    }

                    Application.Run(TrayContext);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("Unhandled exception in application", ex);
                    MessageBox.Show($"Error: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    AppLogger.LogInfo("Application exiting");
                }
            }
        }
    }
}