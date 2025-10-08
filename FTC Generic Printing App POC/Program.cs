using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Services;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    static class Program
    {
        public static TrayApplicationManager TrayContext { get; private set; }
        // TODO: Remove POC on final version
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

                    // Ensures default values are in app.config file.
                    SettingsManager.InitializeSettings();

                    FirebaseService firebaseService = new FirebaseService();
                    FirebaseManager.Instance.Initialize(firebaseService);

                    // Initialize the debug console manager
                    var debugConsoleManager = DebugManager.Instance;

                    // Flag the debug console as initialized
                    typeof(AppLogger).GetField("debugConsoleInitialized", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)?.SetValue(null, true);

                    TrayContext = new TrayApplicationManager();
                    TrayContext.FirebaseService = firebaseService;

                    bool configValid = SettingsManager.AreSettingsValid(firebaseService.GetCurrentTotem());

                    if (configValid)
                    {
                        // If we put await, then the UI thread gets locked.
                        // Maybe it can be resolved on the future but it is not necessary for now.
                        FirebaseManager.Instance.StartListeningAsync();
                    }
                    else
                    {
                        AppLogger.LogWarning("Firebase listener not started: Invalid Totem settings");
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