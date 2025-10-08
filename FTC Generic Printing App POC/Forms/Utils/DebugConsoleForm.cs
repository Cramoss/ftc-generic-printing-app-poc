using System;
using System.Drawing;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.UI
{
    public partial class DebugConsoleForm : Form
    {
        // TODO: Add log level filtering combobox.
        // TODO: Add "Clear" button to clear logs.
        // TODO: Add Checkbox to toggle auto-scroll.
        // TODO: Add button to copy logs to clipboard.
        // TODO: Add button to open log file location.
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
    }
}