using System;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                AppLogger.LogInfo("Application starting");

                ConfigurationManager.InitializeConfiguration();

                Application.Run(new TrayApplicationContext());
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