using FTC_Generic_Printing_App_POC.Manager;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    static class Program
    {
        public static TrayApplicationContext TrayContext { get; private set; }

        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                    await FirebaseListenerManager.Instance.StartListeningAsync();
                    AppLogger.LogInfo("Firebase listener started successfully");
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