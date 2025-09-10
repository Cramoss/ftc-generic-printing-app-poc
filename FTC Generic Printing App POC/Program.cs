using System;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    static class Program
    {
        public static TrayApplicationContext TrayContext { get; private set; }
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                AppLogger.LogInfo("Application starting");

                // Initialize configuration. Ensures default values are in app.config file.
                ConfigurationManager.InitializeConfiguration();

                FirebaseService firebaseService = new FirebaseService();

                // Check if the configuration is valid before starting listener
                var config = ConfigurationManager.LoadTotemConfiguration();
                bool configValid = ConfigurationManager.IsConfigurationValid(config);

                if (configValid)
                {
                    firebaseService.StartListeningAsync().Wait();
                    AppLogger.LogInfo("Firebase listener started successfully");
                }
                else
                {
                    AppLogger.LogWarning("Firebase listener not started: Invalid Totem configuration");
                }

                TrayContext = new TrayApplicationContext();
                TrayContext.FirebaseService = firebaseService;

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