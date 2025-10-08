using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace FTC_Generic_Printing_App_POC.Forms
{
    public partial class DebugConsoleForm : Form
    {
        // TODO: Add log level filtering combobox.
        // TODO: Add Checkbox to toggle auto-scroll.
        // TODO: Improve text colors for better readability.
        public DebugConsoleForm()
        {
            InitializeComponent();

            this.Text = "FTC Generic Printing App Debug Console";
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.ShowInTaskbar = true;
            this.TopMost = true;

            // Log styles
            logTextBox.ReadOnly = true;
            logTextBox.BackColor = Color.Black;
            logTextBox.ForeColor = Color.LightGray;
            logTextBox.Font = new Font("Consolas", 9F);
            logTextBox.ScrollBars = RichTextBoxScrollBars.Both;

            // Form closing event to hide instead of close
            this.FormClosing += DebugConsoleForm_FormClosing;
        }

        private void DebugConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide instead of close when user clicks X
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        public void AppendLog(string logMessage, LogLevel level = LogLevel.Info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, LogLevel>(AppendLog), new object[] { logMessage, level });
                return;
            }

            int startIndex = logTextBox.TextLength;
            logTextBox.AppendText(logMessage + Environment.NewLine);
            int endIndex = logTextBox.TextLength;

            logTextBox.SelectionStart = startIndex;
            logTextBox.SelectionLength = endIndex - startIndex;

            // Set color based on log level
            switch (level)
            {
                case LogLevel.Error:
                    logTextBox.SelectionColor = Color.Red;
                    break;
                case LogLevel.Warning:
                    logTextBox.SelectionColor = Color.Yellow;
                    break;
                case LogLevel.Info:
                    logTextBox.SelectionColor = Color.LightGreen;
                    break;
                case LogLevel.Debug:
                    logTextBox.SelectionColor = Color.LightGray;
                    break;
            }

            // Reset selection
            logTextBox.SelectionStart = endIndex;
            logTextBox.SelectionLength = 0;

            // Always auto-scroll
            logTextBox.ScrollToCaret();
        }

        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }

        private void copyClipboardButton_Click(object sender, EventArgs e)
        {
            try
            {
                string allText = logTextBox.Text;

                if (string.IsNullOrEmpty(allText))
                {
                    MessageBox.Show("No hay contenido para copiar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Split the text into lines
                string[] lines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                // Get last 500 lines
                string[] lastLines = lines.Length <= 500 ?
                    lines :
                    lines.Skip(Math.Max(0, lines.Length - 500)).ToArray();

                // Join the lines
                string clipboardText = string.Join(Environment.NewLine, lastLines);

                var staThread = new System.Threading.Thread(() =>
                {
                    try
                    {
                        Clipboard.SetText(clipboardText);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError($"Error in clipboard thread: {ex.Message}", ex);
                    }
                });

                // Set thread to STA mode
                staThread.SetApartmentState(System.Threading.ApartmentState.STA);
                staThread.Start();
                staThread.Join(); // Wait for the thread to complete

                AppLogger.LogInfo("Copied last 500 lines into clipboard.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar al portapapeles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.LogError("Error copiando logs al portapapeles", ex);
            }
        }

        private void openLogFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                string logFilePath = AppLogger.GetCurrentLogFilePath();
                string logDirectory = Path.GetDirectoryName(logFilePath);

                if (!Directory.Exists(logDirectory))
                {
                    MessageBox.Show($"El directorio de logs no existe: {logDirectory}",
                        "Directorio no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                System.Diagnostics.Process.Start("explorer.exe", logDirectory);
                AppLogger.LogInfo($"Opened log directory: {logDirectory}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el directorio de logs: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.LogError("Error opening log directory", ex);
            }
        }

        private void clearLogsButton_Click(object sender, EventArgs e)
        {
            try
            {
                logTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar la consola: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.LogError("Error clearing console logs", ex);
            }
        }
    }
}