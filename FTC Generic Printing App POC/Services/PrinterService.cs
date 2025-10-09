using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Utils;
using Newtonsoft.Json.Linq;

namespace FTC_Generic_Printing_App_POC.Services
{
    internal class PrinterService : IDisposable
    {
        #region Fields
        // ESC/POS Commands
        private static readonly byte[] ESC_INIT = { 0x1B, 0x40 };                // Initialize printer
        private static readonly byte[] ESC_ALIGN_CENTER = { 0x1B, 0x61, 0x01 };  // Center alignment
        private static readonly byte[] ESC_ALIGN_LEFT = { 0x1B, 0x61, 0x00 };    // Left alignment
        private static readonly byte[] ESC_BOLD_ON = { 0x1B, 0x45, 0x01 };       // Bold on
        private static readonly byte[] ESC_BOLD_OFF = { 0x1B, 0x45, 0x00 };      // Bold off
        private static readonly byte[] ESC_DOUBLE_HEIGHT = { 0x1B, 0x21, 0x10 }; // Double height text
        private static readonly byte[] ESC_NORMAL = { 0x1B, 0x21, 0x00 };        // Normal text
        private static readonly byte[] ESC_CUT = { 0x1D, 0x56, 0x41 };           // Cut paper
        private static readonly byte[] LF = { 0x0A };                            // Line feed
        private static readonly byte[] CR = { 0x0D };                            // Carriage return
        private static readonly byte[] ESC_LINE = { 0x1B, 0x45, 0x2D, 0x1 };     // Underline

        private string printerName;
        private bool isDisposed = false;
        private readonly TemplatesManager templateManager;
        #endregion

        #region Native Methods for Direct Printing  
        [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, ref DOCINFOA di);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        private static extern bool WritePrinter(IntPtr hPrinter, byte[] pBytes, Int32 dwCount, out Int32 dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        #endregion

        #region Constructor & Initialization
        public PrinterService(string printerName = null)
        {
            // If printer name is not provided, use Windows default printer
            if (string.IsNullOrEmpty(printerName))
            {
                try
                {
                    this.printerName = GetDefaultPrinterName();
                    AppLogger.LogInfo($"Using Windows default printer: {this.printerName}");
                }
                catch (Exception ex)
                {
                    AppLogger.LogError("Failed to get default printer name", ex);
                    throw new InvalidOperationException("No default printer is configured on this system", ex);
                }
            }
            else
            {
                this.printerName = printerName;
                AppLogger.LogInfo($"Using specified printer: {this.printerName}");
            }

            templateManager = new TemplatesManager();
        }

        private string GetDefaultPrinterName()
        {
            PrinterSettings settings = new PrinterSettings();
            if (string.IsNullOrEmpty(settings.PrinterName))
            {
                throw new InvalidOperationException("No default printer is configured on this system");
            }

            // TODO: Improved this evaluation
            if (settings.PrinterName.Equals("Default printer is not set."))
            {
                return "No impresora principal";
            }

            return settings.PrinterName;
        }
        #endregion

        #region Core Methods
        public string GetPrinterName()
        {
            return printerName;
        }

        public async Task PrintDocumentAsync(JObject document)
        {
            try
            {
                AppLogger.LogInfo("Starting print process");
                var commands = await templateManager.ProcessDocumentAsync(document);

                SendBytesToPrinter(commands);

                AppLogger.LogInfo("Document printed successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error printing document", ex);
            }
        }
        #endregion

        #region Helper Methods
        public void SendBytesToPrinter(List<byte[]> commands)
        {
            IntPtr hPrinter = IntPtr.Zero;

            try
            {
                if (OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                {
                    DOCINFOA di = new DOCINFOA();
                    di.pDocName = "Document";
                    di.pDataType = "RAW";

                    // Start print job
                    if (StartDocPrinter(hPrinter, 1, ref di))
                    {
                        if (StartPagePrinter(hPrinter))
                        {
                            // Send each command to printer
                            foreach (byte[] command in commands)
                            {
                                int bytesWritten = 0;
                                WritePrinter(hPrinter, command, command.Length, out bytesWritten);
                            }

                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                else
                {
                    throw new Exception($"Failed to open printer '{printerName}'. Check if it is connected and turned on");
                }
            }
            catch (Exception ex)
            {
                if (hPrinter != IntPtr.Zero)
                    ClosePrinter(hPrinter);

                AppLogger.LogError("Error sending data to printer", ex);
                throw;
            }
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // Nothing to dispose for now
                }

                isDisposed = true;
            }
        }

        ~PrinterService()
        {
            Dispose(false);
        }
        #endregion
    }
}
