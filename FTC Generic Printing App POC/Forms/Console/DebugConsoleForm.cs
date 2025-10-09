using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace FTC_Generic_Printing_App_POC.Forms
{
    public partial class DebugConsoleForm : Form
    {
        private const int MaxLogEntries = 1000;
        // Store logs in memory for filtering
        private List<LogEntry> logEntries = new List<LogEntry>();
        // Show all logs by default
        private LogLevel currentFilterLevel = LogLevel.Debug;
        // Flag to indicate if all logs should be shown
        private bool showAllLogs = true;
        // Regex for parsing log messages
        private readonly Regex logRegex = new Regex(@"(\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})\s+(\w+)\s+(.+)");

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

            autoScrollCheckBox.Checked = true;
            autoScrollCheckBox.CheckedChanged += AutoScrollCheckBox_CheckedChanged;

            SetupLogLevelComboBox();

            // Form closing event to hide instead of close
            this.FormClosing += DebugConsoleForm_FormClosing;
        }

        private void SetupLogLevelComboBox()
        {
            logLevelComboBox.Items.Add("Por defecto");
            foreach (LogLevel level in Enum.GetValues(typeof(LogLevel)))
            {
                logLevelComboBox.Items.Add(level.ToString());
            }

            logLevelComboBox.SelectedIndex = 0;
            logLevelComboBox.SelectedIndexChanged += LogLevelComboBox_SelectedIndexChanged;
        }

        private void LogLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLevel = logLevelComboBox.SelectedItem.ToString();

            if (selectedLevel == "Por defecto")
            {
                showAllLogs = true;
                currentFilterLevel = LogLevel.Debug;
            }
            else
            {
                showAllLogs = false;
                currentFilterLevel = (LogLevel)Enum.Parse(typeof(LogLevel), selectedLevel);
            }

            RefreshLogDisplay();
        }

        private void RefreshLogDisplay()
        {
            // Remember the current position
            int currentPosition = logTextBox.SelectionStart;
            bool wasAtEnd = (currentPosition == logTextBox.TextLength);

            logTextBox.Clear();

            // Apply the filter and re-display logs
            foreach (var entry in logEntries)
            {
                if (ShouldDisplayLog(entry.Level))
                {
                    DisplayLogEntry(entry);
                }
            }

            // Restore position or scroll to end
            if (wasAtEnd && autoScrollCheckBox.Checked)
            {
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.ScrollToCaret();
            }
            else
            {
                // Try to keep position similar to where it was before
                int newPosition = Math.Min(currentPosition, logTextBox.TextLength);
                logTextBox.SelectionStart = newPosition;
            }
        }

        private bool ShouldDisplayLog(LogLevel level)
        {
            if (showAllLogs)
                return true;

            return level == currentFilterLevel;
        }

        private void AutoScrollCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string state = autoScrollCheckBox.Checked ? "enabled" : "disabled";
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

            var parsedLevel = ParseLogLevel(logMessage);
            if (parsedLevel.HasValue)
            {
                level = parsedLevel.Value;
            }

            // Create a new log entry
            var entry = new LogEntry
            {
                Message = logMessage,
                Level = level,
                Timestamp = DateTime.Now
            };

            // Add to our collection with size limit
            logEntries.Add(entry);

            // Keep collection size in check
            if (logEntries.Count > MaxLogEntries)
            {
                // Remove oldest entries when we exceed the limit
                int removeCount = logEntries.Count - MaxLogEntries;
                logEntries.RemoveRange(0, removeCount);
            }

            // Only display if it passes the current filter
            if (ShouldDisplayLog(level))
            {
                DisplayLogEntry(entry);
            }
        }

        private LogLevel? ParseLogLevel(string logMessage)
        {
            var match = logRegex.Match(logMessage);
            if (match.Success && match.Groups.Count >= 3)
            {
                string levelStr = match.Groups[2].Value.Trim().ToUpper();

                // Map common log level strings to our enum
                switch (levelStr)
                {
                    case "ERROR":
                        return LogLevel.Error;
                    case "WARN":
                    case "WARNING":
                        return LogLevel.Warning;
                    case "INFO":
                        return LogLevel.Info;
                    case "DEBUG":
                        return LogLevel.Debug;
                    default:
                        // If it contains ERROR, treat as error
                        if (levelStr.Contains("ERROR"))
                            return LogLevel.Error;
                        // If it contains WARN, treat as warning
                        else if (levelStr.Contains("WARN"))
                            return LogLevel.Warning;
                        break;
                }
            }

            return null; // Could not parse level
        }

        private void DisplayLogEntry(LogEntry entry)
        {
            int startIndex = logTextBox.TextLength;
            logTextBox.AppendText(entry.Message + Environment.NewLine);
            int endIndex = logTextBox.TextLength;

            logTextBox.SelectionStart = startIndex;
            logTextBox.SelectionLength = endIndex - startIndex;

            // Set color based on log level
            switch (entry.Level)
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

            if (autoScrollCheckBox.Checked)
            {
                logTextBox.ScrollToCaret();
            }
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
                logEntries.Clear();
                logTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al limpiar la consola: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.LogError("Error clearing console logs", ex);
            }
        }

        // Class to store log entries for filtering
        private class LogEntry
        {
            public string Message { get; set; }
            public LogLevel Level { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}