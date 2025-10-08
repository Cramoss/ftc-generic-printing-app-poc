using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Forms.Prompts
{
    public partial class AdminPasswordPromptForm : Form
    {
        #region Fields
        public bool IsPasswordVerified { get; private set; }
        #endregion

        #region Initialization
        public AdminPasswordPromptForm()
        {
            InitializeComponent();
            adminPasswordTextBox.UseSystemPasswordChar = true;
            IsPasswordVerified = false;
        }
        #endregion

        #region Event Handlers
        private void acceptPasswordButton_Click(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        private void cancelPasswordButton_Click(object sender, EventArgs e)
        {
            CancelPrompt();
        }
        #endregion

        #region Core Methods
        private void ValidatePassword()
        {
            try
            {
                // Get the password from defaultSettings.xml
                string correctPassword = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_ADMIN_PASSWORD);

                if (string.IsNullOrEmpty(correctPassword))
                {
                    // If no password is found in defaultSettings.xml, use a hardcoded one
                    correctPassword = "F4l4b3ll4";
                    AppLogger.LogWarning("Admin password not found in defaultSettings.xml, using hardcoded password");
                }

                if (adminPasswordTextBox.Text == correctPassword)
                {
                    AppLogger.LogInfo("Admin password verification successful");
                    IsPasswordVerified = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    AppLogger.LogWarning("Admin password verification failed");
                    MessageBox.Show("Contraseña incorrecta.", "Error de verificación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    adminPasswordTextBox.SelectAll();
                    adminPasswordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in admin password verification", ex);
                MessageBox.Show("Error al verificar contraseña: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelPrompt()
        {
            IsPasswordVerified = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion
    }
}
