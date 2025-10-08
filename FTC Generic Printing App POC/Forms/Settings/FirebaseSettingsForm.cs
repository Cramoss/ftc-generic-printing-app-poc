using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Services;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    // TODO: Hide icon from taskbar when form is open.
    public partial class FirebaseSettingsForm : Form
    {
        #region Fields
        private FirebaseService firebaseManager;
        private readonly Regex urlRegex = new Regex(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", RegexOptions.Compiled);
        private readonly Regex projectIdRegex = new Regex(@"^[a-zA-Z0-9-]+$", RegexOptions.Compiled); // Alphanumeric and hyphens
        #endregion

        #region Initialization
        public FirebaseSettingsForm()
        {
            InitializeComponent();
            SetupTextBoxValidation();
        }

        private void SetupTextBoxValidation()
        {
            firebaseDatabaseTextBox.MaxLength = 200;
            firebaseProjectIdTextBox.MaxLength = 200;
            firebaseKeyTextBox.MaxLength = 200;

            // Add validation handlers
            firebaseProjectIdTextBox.KeyPress += ProjectId_KeyPress;
            firebaseKeyTextBox.KeyPress += AlphanumericOnly_KeyPress;
        }

        private void AlphanumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, numbers, and control characters
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                AppLogger.LogWarning($"Blocked non-alphanumeric character: {e.KeyChar}");
            }
        }

        private void ProjectId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, numbers, hyphen, and control characters
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                AppLogger.LogWarning($"Blocked invalid character for Project ID: {e.KeyChar}");
            }
        }

        private void FirebaseSettings_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Firebase settings form opened");
        }
        #endregion

        #region Event Handlers
        private void saveFirebaseSettingsButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            ClearForm();
        }

        private void cleanFirebaseSettingsButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void restoreDefaultFirebaseSettingsButton_Click(object sender, EventArgs e)
        {
            RestoreDefaultSettings();
        }

        private void cancelFirebaseSettingsButton_Click(object sender, EventArgs e)
        {
            CancelSettings();
        }
        #endregion

        #region Core Methods
        private void SaveSettings()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Firebase settings button");

                string databaseUrl = firebaseDatabaseTextBox.Text.Trim();
                string projectId = firebaseProjectIdTextBox.Text.Trim();
                string apiKey = firebaseKeyTextBox.Text.Trim();

                // URL validation for Database URL
                if (string.IsNullOrEmpty(databaseUrl))
                {
                    AppLogger.LogWarning("Validation failed: Firebase Database URL is empty");
                    MessageBox.Show("Por favor ingrese una URL de base de datos válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseDatabaseTextBox.Focus();
                    return;
                }

                if (!IsValidUrl(databaseUrl))
                {
                    AppLogger.LogWarning("Validation failed: Firebase Database URL has invalid format");
                    MessageBox.Show("La URL de base de datos debe tener un formato válido (http:// o https://)", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseDatabaseTextBox.Focus();
                    return;
                }

                // Validate Project ID
                if (string.IsNullOrEmpty(projectId))
                {
                    AppLogger.LogWarning("Validation failed: Firebase Project ID is empty");
                    MessageBox.Show("Por favor ingrese un ID de proyecto válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseProjectIdTextBox.Focus();
                    return;
                }

                if (!IsValidProjectId(projectId))
                {
                    AppLogger.LogWarning("Validation failed: Project ID contains invalid characters");
                    MessageBox.Show("El ID del proyecto solo puede contener letras, números y guiones.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseProjectIdTextBox.Focus();
                    return;
                }

                // Validate API Key
                if (string.IsNullOrEmpty(apiKey))
                {
                    AppLogger.LogWarning("Validation failed: Firebase API Key is empty");
                    MessageBox.Show("Por favor ingrese una API Key válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseKeyTextBox.Focus();
                    return;
                }

                if (!apiKey.All(char.IsLetterOrDigit))
                {
                    AppLogger.LogWarning("Validation failed: API Key contains non-alphanumeric characters");
                    MessageBox.Show("La API Key solo puede contener letras y números.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseKeyTextBox.Focus();
                    return;
                }

                SettingsManager.SaveFirebaseSettings(databaseUrl, projectId, apiKey);

                if (firebaseManager != null)
                {
                    firebaseManager.ReloadSettings();
                }

                AppLogger.LogInfo($"Firebase settings saved successfully");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Firebase settings", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            try
            {
                firebaseDatabaseTextBox.Text = "";
                firebaseProjectIdTextBox.Text = "";
                firebaseKeyTextBox.Text = "";

                AppLogger.LogInfo("Firebase edit settings panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Firebase edit settings panel", ex);
            }
        }

        private void RestoreDefaultSettings()
        {
            try
            {
                AppLogger.LogInfo("User clicked Restore Default Firebase settings button");

                DialogResult result = MessageBox.Show(
                    "¿Está seguro que desea restaurar la configuración de Firebase a los valores predeterminados?" +
                    Environment.NewLine + Environment.NewLine +
                    "Los valores actuales serán reemplazados por los valores predeterminados del archivo defaultSettings.xml",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get default values from defaultSettings.xml
                    string databaseUrl = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_FIREBASE_DB_URL);
                    string projectId = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_FIREBASE_PROJECT_ID);
                    string apiKey = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_FIREBASE_API_KEY);

                    if (string.IsNullOrEmpty(databaseUrl) || string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(apiKey))
                    {
                        AppLogger.LogWarning("Some default Firebase settings values were not found in defaultSettings.xml");
                        MessageBox.Show(
                            "Algunos valores predeterminados no fueron encontrados en el archivo defaultSettings.xml",
                            "Error de restauración",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    SettingsManager.SaveFirebaseSettings(databaseUrl, projectId, apiKey);

                    if (firebaseManager != null)
                    {
                        firebaseManager.ReloadSettings();
                    }

                    AppLogger.LogInfo("Firebase settings restored to default values successfully");
                    MessageBox.Show(
                        "La configuración de Firebase ha sido restaurada a los valores predeterminados correctamente.",
                        "Restauración exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Hide();
                }
                else
                {
                    AppLogger.LogInfo("User cancelled restoring Firebase default settings");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error restoring Firebase default settings", ex);
                MessageBox.Show(
                    "Error al restaurar la configuración predeterminada: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CancelSettings()
        {
            try
            {
                this.Hide();
                ClearForm();
                AppLogger.LogInfo("Firebase settings form hidden and edit panel reset values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error hiding Firebase settings form and resetting edit panel", ex);
            }
        }
        #endregion

        #region Utils
        private bool IsValidUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url) && urlRegex.IsMatch(url);
        }

        private bool IsValidProjectId(string projectId)
        {
            return !string.IsNullOrWhiteSpace(projectId) && projectIdRegex.IsMatch(projectId);
        }
        #endregion
    }
}
