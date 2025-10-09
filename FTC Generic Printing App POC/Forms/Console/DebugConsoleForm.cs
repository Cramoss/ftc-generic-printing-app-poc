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
        #region Fields
        private const int MaxLogEntries = 1000;
        private const int MaxClipboardLines = 500;

        private readonly List<LogEntry> logEntries = new List<LogEntry>();
        private readonly Regex logRegex = new Regex(@"(\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})\s+(\w+)\s+(.+)");

        private LogLevel currentFilterLevel = LogLevel.Debug;
        private bool showAllLogs = true;
        #endregion

        #region Enums
        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }
        #endregion

        #region Initialization
        public DebugConsoleForm()
        {
            InitializeComponent();
            ConfigureForm();
            ConfigureControls();
            RegisterEventHandlers();
        }

        private void ConfigureForm()
        {
            Text = "FTC Generic Printing App Debug Console";
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            ShowInTaskbar = true;
            TopMost = true;
        }

        private void ConfigureControls()
        {
            logTextBox.ReadOnly = true;
            logTextBox.BackColor = Color.Black;
            logTextBox.ForeColor = Color.LightGray;
            logTextBox.Font = new Font("Consolas", 9F);
            logTextBox.ScrollBars = RichTextBoxScrollBars.Both;

            autoScrollCheckBox.Checked = true;

            SetupLogLevelComboBox();
        }

        private void RegisterEventHandlers()
        {
            FormClosing += DebugConsoleForm_FormClosing;
            autoScrollCheckBox.CheckedChanged += AutoScrollCheckBox_CheckedChanged;
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
        #endregion

        #region Event Handlers
        private void DebugConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide instead of close when user clicks X
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void AutoScrollCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Event handler registered for potential future use
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

        private void copyClipboardButton_Click(object sender, EventArgs e)
        {
            CopyLogsToClipboard();
        }

        private void openLogFolderButton_Click(object sender, EventArgs e)
        {
            OpenLogFolder();
        }

        private void clearLogsButton_Click(object sender, EventArgs e)
        {
            ClearLogs();
        }
        #endregion

        #region Core Methods
        private void CopyLogsToClipboard()
        {
            try
            {
                string allText = logTextBox.Text;

                if (string.IsNullOrEmpty(allText))
                {
                    MessageBox.Show("No hay contenido para copiar.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Get the last MaxClipboardLines lines
                string[] lines = allText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] lastLines = lines.Length <= MaxClipboardLines ?
                    lines :
                    lines.Skip(Math.Max(0, lines.Length - MaxClipboardLines)).ToArray();

                string clipboardText = string.Join(Environment.NewLine, lastLines);

                // Copy text to clipboard in STA thread
                CopyTextToClipboardInStaThread(clipboardText);

                AppLogger.LogInfo($"Copied last {MaxClipboardLines} lines into clipboard.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al copiar al portapapeles: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppLogger.LogError("Error copiando logs al portapapeles", ex);
            }
        }

        private void CopyTextToClipboardInStaThread(string text)
        {
            var staThread = new System.Threading.Thread(() =>
            {
                try
                {
                    Clipboard.SetText(text);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError($"Error in clipboard thread: {ex.Message}", ex);
                }
            });

            staThread.SetApartmentState(System.Threading.ApartmentState.STA);
            staThread.Start();
            // Wait for thread completion
            staThread.Join();
        }

        private void OpenLogFolder()
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

        private void ClearLogs()
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
        public void AppendLog(string logMessage, LogLevel level = LogLevel.Info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, LogLevel>(AppendLog), new object[] { logMessage, level });
                return;
            }

            // Get actual log level
            var parsedLevel = ParseLogLevel(logMessage);
            if (parsedLevel.HasValue)
            {
                level = parsedLevel.Value;
            }

            StoreLogEntry(logMessage, level);

            // Show it if it passes the current level filter
            if (ShouldDisplayLog(level))
            {
                DisplayLogEntry(logEntries.Last());
            }
        }
        #endregion

        #region Private Methods
        private void StoreLogEntry(string message, LogLevel level)
        {
            var entry = new LogEntry
            {
                Message = message,
                Level = level,
                Timestamp = DateTime.Now
            };

            logEntries.Add(entry);

            // Maintain max size limit
            if (logEntries.Count > MaxLogEntries)
            {
                int removeCount = logEntries.Count - MaxLogEntries;
                logEntries.RemoveRange(0, removeCount);
            }
        }

        private LogLevel? ParseLogLevel(string logMessage)
        {
            var match = logRegex.Match(logMessage);
            if (match.Success && match.Groups.Count >= 3)
            {
                string levelStr = match.Groups[2].Value.Trim().ToUpper();

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
                        if (levelStr.Contains("ERROR"))
                            return LogLevel.Error;
                        else if (levelStr.Contains("WARN"))
                            return LogLevel.Warning;
                        break;
                }
            }

            return null;
        }

        private bool ShouldDisplayLog(LogLevel level)
        {
            return showAllLogs || level == currentFilterLevel;
        }

        private void RefreshLogDisplay()
        {
            // Save current state
            int currentPosition = logTextBox.SelectionStart;
            bool wasAtEnd = (currentPosition == logTextBox.TextLength);

            // Clear and repopulate
            logTextBox.Clear();
            foreach (var entry in logEntries)
            {
                if (ShouldDisplayLog(entry.Level))
                {
                    DisplayLogEntry(entry);
                }
            }

            // Restore position or auto-scroll (if checked)
            if (wasAtEnd && autoScrollCheckBox.Checked)
            {
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.ScrollToCaret();
            }
            else
            {
                int newPosition = Math.Min(currentPosition, logTextBox.TextLength);
                logTextBox.SelectionStart = newPosition;
            }
        }

        private void DisplayLogEntry(LogEntry entry)
        {
            int startIndex = logTextBox.TextLength;
            logTextBox.AppendText(entry.Message + Environment.NewLine);
            int endIndex = logTextBox.TextLength;

            // Apply color formatting
            logTextBox.SelectionStart = startIndex;
            logTextBox.SelectionLength = endIndex - startIndex;
            logTextBox.SelectionColor = GetColorForLogLevel(entry.Level);

            // Reset selection
            logTextBox.SelectionStart = endIndex;
            logTextBox.SelectionLength = 0;

            if (autoScrollCheckBox.Checked)
            {
                logTextBox.ScrollToCaret();
            }
        }

        private Color GetColorForLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error: return Color.Red;
                case LogLevel.Warning: return Color.Yellow;
                case LogLevel.Info: return Color.LightGreen;
                case LogLevel.Debug:
                default: return Color.LightGray;
            }
        }
        #endregion

        #region Helper Classes
        private class LogEntry
        {
            public string Message { get; set; }
            public LogLevel Level { get; set; }
            public DateTime Timestamp { get; set; }
        }
        #endregion
    }
}