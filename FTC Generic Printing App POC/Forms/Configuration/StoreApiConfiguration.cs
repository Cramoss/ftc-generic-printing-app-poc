using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FTC_Generic_Printing_App_POC
{
    // TODO: Hide icon from taskbar when form is open.
    public partial class StoreApiConfiguration : Form
    {
        #region Fields
        private readonly StoresApiService apiService;
        #endregion

        #region Initialization
        public StoreApiConfiguration()
        {
            apiService = new StoresApiService();
            InitializeComponent();
        }

        private void StoresApiConfiguration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Stores API Configuration form opened");
        }
        #endregion

        #region Event Handlers
        private void saveStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            ClearForm();
        }

        private void cleanStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void restoreDefaultStoreApiConfigurationButton_Click(object sender, EventArgs e)
        {
            RestoreDefaultConfiguration();
        }

        private void cancelStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            CancelConfiguration();
        }
        #endregion

        #region Core Methods
        private void SaveConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Stores API Configuration button");

                string storesApiUrl = storesApiUrlTextBox.Text.Trim();
                string storesApiAuthUrl = storesApiAuthUrlTextBox.Text.Trim();
                string storesApiClientId = storesApiClientIdTextBox.Text.Trim();
                string storesApiClientSecret = storesApiClientSecretTextBox.Text.Trim();

                // TODO: Improve or add more validations.
                if (string.IsNullOrEmpty(storesApiUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API URL is empty");
                    MessageBox.Show("Por favor ingrese una URL válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiUrlTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(storesApiAuthUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Auth URL is empty");
                    MessageBox.Show("Por favor ingrese una URL válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiUrlTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(storesApiClientId))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Client ID is empty");
                    MessageBox.Show("Por favor ingrese un Client ID válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiClientIdTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(storesApiClientSecret))
                {
                    AppLogger.LogWarning("Validation failed: Stores API Client Secret is empty");
                    MessageBox.Show("Por favor ingrese un Client Secret válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storesApiClientSecretTextBox.Focus();
                    return;
                }

                ConfigurationManager.SaveStoresApiConfiguration(
                    storesApiAuthUrl,
                    storesApiUrl,
                    storesApiClientId,
                    storesApiClientSecret
                );

                apiService.ReloadConfiguration();

                AppLogger.LogInfo($"Store API configuration saved with URL: {storesApiUrl}");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Store API configuration", ex);
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

                AppLogger.LogInfo("Stores API edit configuration panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Stores API edit configuration panel", ex);
            }
        }

        private void RestoreDefaultConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Restore Default Stores API Configuration button");

                DialogResult result = MessageBox.Show(
                    "¿Está seguro que desea restaurar la configuración de Stores API a los valores predeterminados?" +
                    Environment.NewLine + Environment.NewLine,
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get default values from defaultConfig.xml
                    string authUrl = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_AUTH_URL);
                    string storesUrl = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_URL);
                    string clientId = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_CLIENT_ID);
                    string clientSecret = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_STORES_API_CLIENT_SECRET);

                    if (string.IsNullOrEmpty(authUrl) || string.IsNullOrEmpty(storesUrl) ||
                        string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                    {
                        AppLogger.LogWarning("Some default Store API configuration values were not found.");
                        MessageBox.Show(
                            "Algunos valores predeterminados no fueron encontrados en el archivo de configuración predeterminada.",
                            "Error de restauración",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    ConfigurationManager.SaveStoresApiConfiguration(authUrl, storesUrl, clientId, clientSecret);

                    apiService.ReloadConfiguration();

                    AppLogger.LogInfo("Store API configuration restored to default values successfully");
                    MessageBox.Show(
                        "La configuración de Stores API ha sido restaurada a los valores predeterminados correctamente.",
                        "Restauración exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Hide();
                }
                else
                {
                    AppLogger.LogInfo("User cancelled restoring Store API default configuration");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error restoring Store API default configuration", ex);
                MessageBox.Show(
                    "Error al restaurar la configuración predeterminada: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CancelConfiguration()
        {
            try
            {
                this.Hide();
                ClearForm();
                AppLogger.LogInfo("Store API configuration form hidden and edit panel reset values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error hiding Store API configuration form and resetting edit panel", ex);
            }
        }
        #endregion
    }
}
