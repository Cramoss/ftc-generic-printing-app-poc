using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Services;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    public partial class StoresApiSettingsForm : Form
    {
        #region Fields
        private readonly StoresApiService storesApiService;
        private readonly Regex urlRegex = new Regex(@"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$", RegexOptions.Compiled);
        #endregion

        #region Initialization
        public StoresApiSettingsForm()
        {
            storesApiService = new StoresApiService();
            InitializeComponent();
            SetupTextBoxValidation();
        }

        private void SetupTextBoxValidation()
        {
            storesApiUrlTextBox.MaxLength = 200;
            storesApiAuthUrlTextBox.MaxLength = 200;
            storesApiClientIdTextBox.MaxLength = 200;
            storesApiClientSecretTextBox.MaxLength = 200;

            // Set up validation handlers for alphanumeric fields
            storesApiClientIdTextBox.KeyPress += AlphanumericOnly_KeyPress;
            storesApiClientSecretTextBox.KeyPress += AlphanumericOnly_KeyPress;
        }

        private void StoresApiSettings_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Stores API settings form opened");
        }
        #endregion

        #region Event Handlers
        private void saveStoresApiSettingsButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            ClearForm();
        }

        private void cleanStoresApiSettingsButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void restoreDefaultStoresApiSettingsButton_Click(object sender, EventArgs e)
        {
            RestoreDefaultSettings();
        }

        private void cancelStoresApiSettingsButton_Click(object sender, EventArgs e)
        {
            CancelSettings();
        }
        #endregion

        #region Core Methods
        private void SaveSettings()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Stores API settings button");

                string storesApiUrl = storesApiUrlTextBox.Text.Trim();
                string storesApiAuthUrl = storesApiAuthUrlTextBox.Text.Trim();
                string storesApiClientId = storesApiClientIdTextBox.Text.Trim();
                string storesApiClientSecret = storesApiClientSecretTextBox.Text.Trim();

                // Stores API URL format validation
                if (string.IsNullOrEmpty(storesApiUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API URL is empty");
                    MessageBox.Show("Por favor ingrese una URL válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiUrlTextBox.Focus();
                    return;
                }

                if (!IsValidUrl(storesApiUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API URL has invalid format");
                    MessageBox.Show("La URL debe tener un formato válido (http:// o https://)", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiUrlTextBox.Focus();
                    return;
                }

                // AUTH URL format validation
                if (string.IsNullOrEmpty(storesApiAuthUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Auth URL is empty");
                    MessageBox.Show("Por favor ingrese una URL válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiAuthUrlTextBox.Focus();
                    return;
                }

                if (!IsValidUrl(storesApiAuthUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Auth URL has invalid format");
                    MessageBox.Show("La URL de autenticación debe tener un formato válido (http:// o https://)", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiAuthUrlTextBox.Focus();
                    return;
                }

                // Validate Client ID
                if (string.IsNullOrEmpty(storesApiClientId))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Client ID is empty");
                    MessageBox.Show("Por favor ingrese un Client ID válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiClientIdTextBox.Focus();
                    return;
                }

                if (!storesApiClientId.All(char.IsLetterOrDigit))
                {
                    AppLogger.LogWarning("Validation failed: Client ID contains non-alphanumeric characters");
                    MessageBox.Show("El Client ID solo puede contener letras y números.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiClientIdTextBox.Focus();
                    return;
                }

                // Validate Client Secret
                if (string.IsNullOrEmpty(storesApiClientSecret))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Client Secret is empty");
                    MessageBox.Show("Por favor ingrese un Client Secret válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiClientSecretTextBox.Focus();
                    return;
                }

                if (!storesApiClientSecret.All(char.IsLetterOrDigit))
                {
                    AppLogger.LogWarning("Validation failed: Client Secret contains non-alphanumeric characters");
                    MessageBox.Show("El Client Secret solo puede contener letras y números.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiClientSecretTextBox.Focus();
                    return;
                }

                SettingsManager.SaveStoresApiSettings(
                    storesApiAuthUrl,
                    storesApiUrl,
                    storesApiClientId,
                    storesApiClientSecret
                );

                storesApiService.ReloadSettings();

                AppLogger.LogInfo($"Stores API settings saved with URL: {storesApiUrl}");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Stores API settings", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            try
            {
                storesApiUrlTextBox.Text = "";
                storesApiAuthUrlTextBox.Text = "";
                storesApiClientIdTextBox.Text = "";
                storesApiClientSecretTextBox.Text = "";

                AppLogger.LogInfo("Stores API edit settings panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Stores API edit settings panel", ex);
            }
        }

        private void RestoreDefaultSettings()
        {
            try
            {
                AppLogger.LogInfo("User clicked Restore Default Stores API settings button");

                DialogResult result = MessageBox.Show(
                    "¿Está seguro que desea restaurar la configuración de Stores API a los valores predeterminados?" +
                    Environment.NewLine + Environment.NewLine,
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get default values from defaultSettings.xml
                    string authUrl = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_AUTH_URL);
                    string storesUrl = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_URL);
                    string clientId = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_CLIENT_ID);
                    string clientSecret = SettingsManager.GetValueFromDefaultSettings(DefaultSettingsKeys.SETTINGS_DEFAULT_STORES_API_CLIENT_SECRET);

                    if (string.IsNullOrEmpty(authUrl) || string.IsNullOrEmpty(storesUrl) ||
                        string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                    {
                        AppLogger.LogWarning("Some default Stores API settings values were not found");
                        MessageBox.Show(
                            "Algunos valores predeterminados no fueron encontrados en el archivo de configuración predeterminada.",
                            "Error de restauración",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    SettingsManager.SaveStoresApiSettings(authUrl, storesUrl, clientId, clientSecret);

                    storesApiService.ReloadSettings();

                    AppLogger.LogInfo("Stores API settings restored to default values successfully");
                    MessageBox.Show(
                        "La configuración de Stores API ha sido restaurada a los valores predeterminados correctamente.",
                        "Restauración exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Hide();
                }
                else
                {
                    AppLogger.LogInfo("User cancelled restoring Stores API default settings");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error restoring Stores API default settings", ex);
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
                AppLogger.LogInfo("Stores API settings form hidden and edit panel reset values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error hiding Stores API settings form and resetting edit panel", ex);
            }
        }
        private void AlphanumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, numbers, and control characters
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                AppLogger.LogWarning($"[StoresAPI] Blocked non-alphanumeric character: {e.KeyChar}");
            }
        }
        #endregion

        #region Utils
        private bool IsValidUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url) && urlRegex.IsMatch(url);
        }
        #endregion
    }
}
