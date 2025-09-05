using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC
{
    public partial class Configuration : Form
    {
        private readonly ApiService apiService;

        public Configuration()
        {
            InitializeComponent();

            ResetEditStoresApiConfigurationPanel();
            LoadSavedConfiguration();
        }

        #region Load configuration

        private void Configuration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Configuration form opened");
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);

            if (value)
            {
                RefreshConfigurationLabels();
            }
        }

        #endregion

        #region Display current configuration

        private void LoadSavedConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading saved configuration..");
                var config = ConfigurationManager.LoadConfiguration();

                // Store selection will be handled after stores are loaded in the LoadStoresAsync method

                currentTotemId.Text = !string.IsNullOrEmpty(config.IdTotem) ? config.IdTotem : "No configurado";
                currentCountry.Text = !string.IsNullOrEmpty(config.Country) ? config.Country : "No configurado";
                currentBusiness.Text = !string.IsNullOrEmpty(config.Business) ? config.Business : "No configurado";
                currentStore.Text = !string.IsNullOrEmpty(config.Store) ? config.Store : "No configurado";
                currentStoreId.Text = !string.IsNullOrEmpty(config.StoreId) ? config.StoreId : "No configurado";

                AppLogger.LogInfo("Configuration loaded into the form and labels updated");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading configuration into the form", ex);

                // TODO: Use constant ERROR
                currentTotemId.Text = "Error";
                currentCountry.Text = "Error";
                currentBusiness.Text = "Error";
                currentStore.Text = "Error";
                currentStoreId.Text = "Error";
            }
        }

        private void RefreshConfigurationLabels()
        {
            try
            {
                var config = ConfigurationManager.LoadConfiguration();

                currentTotemId.Text = !string.IsNullOrEmpty(config.IdTotem) ? config.IdTotem : "No configurado";
                currentCountry.Text = !string.IsNullOrEmpty(config.Country) ? config.Country : "No configurado";
                currentBusiness.Text = !string.IsNullOrEmpty(config.Business) ? config.Business : "No configurado";
                currentStore.Text = !string.IsNullOrEmpty(config.Store) ? config.Store : "No configurado";
                currentStoreId.Text = !string.IsNullOrEmpty(config.StoreId) ? config.StoreId : "No configurado";

                AppLogger.LogInfo("Configuration labels refreshed");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing configuration labels", ex);

                currentTotemId.Text = "Error";
                currentCountry.Text = "Error";
                currentBusiness.Text = "Error";
                currentStore.Text = "Error";
                currentStoreId.Text = "Error";
            }
        }

        #endregion

        #region Edit Totem configuration

        private void editTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Totem Configuration form");
            TotemConfiguration totemConfigForm = new TotemConfiguration();
            totemConfigForm.ShowDialog(); // Shows the form as a modal dialog

            // Refresh the configuration labels after the dialog is closed
            RefreshConfigurationLabels();
        }

        #endregion

        #region Edit Stores API configuration

        private void saveStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveStoresApiConfiguration();
        }

        private void saveTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveStoresApiConfiguration();
        }

        private void SaveStoresApiConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Stores API Configuration button");

                string storesApiUrl = storesApiUrlTextBox.Text.Trim();
                string storesApiKey = storesApiKeyTextBox.Text.Trim();
                string storesApiClientId = storesApiClientIdTextBox.Text.Trim();
                string storesApiClientSecret = storesApiClientSecretTextBox.Text.Trim();

                // Required fields validation
                if (string.IsNullOrEmpty(storesApiUrl))
                {
                    AppLogger.LogWarning("Validation failed: Stores API URL is empty");
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

                ConfigurationManager.SaveStoreApiConfiguration(
                    storesApiUrl, 
                    storesApiUrl, // TODO: Handle edit of Auth URL
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

        private void editStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            // TODO: Opeen Stores API Configuration form
        }

        #endregion

        #region Testing connectivity

        private async void testConnectivityButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("User clicked Test Connectivity button");

            try
            {
                networkStatusLabel.Text = "...";
                storesApiStatusLabel.Text = "...";
                firebaseStatusLabel.Text = "...";

                testConnectivityButton.Text = "Probando...";
                testConnectivityButton.Enabled = false;

                AppLogger.LogInfo("Starting network connectivity test");
                using (var client = new System.Net.NetworkInformation.Ping())
                {
                    var reply = await Task.Run(() => client.Send("8.8.8.8", 3000));
                    if (reply.Status != System.Net.NetworkInformation.IPStatus.Success)
                    {
                        networkStatusLabel.Text = "ERROR";
                        AppLogger.LogError("Network connectivity test failed");
                        throw new Exception("Network connectivity test failed");
                    }
                    else
                    {
                        networkStatusLabel.Text = "OK";
                        AppLogger.LogInfo("Network connectivity test passed");
                    }
                }

                AppLogger.LogInfo("Starting Store API connectivity test");

                apiService.ReloadConfiguration();
                try
                {
                    string token = await apiService.GetAccessTokenAsync();
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new Exception("Store API returned empty token");
                    }
                    storesApiStatusLabel.Text = "OK";
                    AppLogger.LogInfo("Store API connectivity test passed");
                }
                catch (Exception ex)
                {
                    storesApiStatusLabel.Text = "ERROR";
                    AppLogger.LogError("Store API connectivity test failed", ex);
                    throw new Exception($"Store API test failed: {ex.Message}");
                }

                AppLogger.LogInfo("Starting Firebase connectivity tests");
                var firebaseManager = new FirebaseManager();

                try
                {
                    bool firebaseConnectionTest = await firebaseManager.TestConnectionAsync();
                    if (!firebaseConnectionTest)
                    {
                        firebaseStatusLabel.Text = "ERROR";
                        throw new Exception("Firebase basic connection test failed");
                    }

                    bool firebaseAuthTest = await firebaseManager.TestAuthenticationAsync();
                    if (!firebaseAuthTest)
                    {
                        firebaseStatusLabel.Text = "ERROR";
                        throw new Exception("Firebase API key authentication failed");
                    }

                    bool firebasePathTest = await firebaseManager.TestDocumentPathAsync();
                    if (!firebasePathTest)
                    {
                        firebaseStatusLabel.Text = "WARN";
                        AppLogger.LogWarning("Firebase path test failed. Document may not exist yet, but this can be normal");
                    }
                    else
                    {
                        AppLogger.LogInfo("Firebase path test passed");
                    }

                    firebaseStatusLabel.Text = "OK";
                    AppLogger.LogInfo("Firebase connectivity tests passed");
                }
                catch (Exception ex)
                {
                    firebaseStatusLabel.Text = "ERROR";
                    AppLogger.LogError("Firebase connectivity test failed", ex);
                    throw new Exception($"Firebase test failed: {ex.Message}");
                }
                finally
                {
                    firebaseManager.Dispose();
                }

                testConnectivityButton.Text = "Probar conectividad";
                testConnectivityButton.Enabled = true;

                AppLogger.LogInfo("All connectivity tests completed successfully");
            }
            catch (Exception ex)
            {
                testConnectivityButton.Text = "Probar conectividad";
                testConnectivityButton.Enabled = true;

                string errorDetails = ex.Message;
                AppLogger.LogError("Connectivity test failed", ex);
            }
        }

        private void testTicketPrintButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("User clicked Test Ticket Print button");
            try
            {
                AppLogger.LogPrintEvent("TEST", "Test ticket print requested by user");

                // TODO: Call printing method
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Print test failed", ex);
                MessageBox.Show("Error al imprimir: " + ex.Message, "Error de impresión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


        #region Window management

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden");
        }

        private void exitConfigurationButton_Click(object sender, EventArgs e)
        {
            ResetEditStoresApiConfigurationPanel();
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden and edit panel reset values");
        }

        #endregion

        #region Reset values

   

        

        private void ResetEditStoresApiConfigurationPanel()
        {
            try
            {
                storesApiUrlTextBox.Text = "";
                storesApiKeyTextBox.Text = "";
                storesApiClientIdTextBox.Text = "";
                storesApiClientSecretTextBox.Text = "";

                AppLogger.LogInfo("Stores API edit configuration panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Stores API edit configuration panel", ex);
            }
        }

        #endregion
    }
}