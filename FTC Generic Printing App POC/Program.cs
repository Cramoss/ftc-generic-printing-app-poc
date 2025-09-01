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

            // Start with tray application in the background instead of main visible window
            Application.Run(new TrayApplicationContext());
        }
    }
}