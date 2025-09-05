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
            apiService = new ApiService();
            InitializeComponent();
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
            totemConfigForm.ShowDialog();

            RefreshConfigurationLabels();
        }

        #endregion

        #region Edit Stores API configuration

        private void editStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Stores API Configuration panel");
            StoreApiConfiguration storeApiConfigForm = new StoreApiConfiguration();
            storeApiConfigForm.ShowDialog();
        }

        #endregion

        #region Testing connectivity

        private async void testNetworkConnectivityButton_Click(object sender, EventArgs e)
        {
            networkStatus.Text = "Probando...";
            testNetworkConnectivityButton.Text = "Probando...";
            testNetworkConnectivityButton.Enabled = false;

            try
            {
                AppLogger.LogInfo("Starting network connectivity test");
                using (var client = new System.Net.NetworkInformation.Ping())
                {
                    var reply = await Task.Run(() => client.Send("8.8.8.8", 3000));
                    if (reply.Status != System.Net.NetworkInformation.IPStatus.Success)
                    {
                        networkStatus.Text = "ERROR";
                        networkStatus.BackColor = Color.LightCoral;
                        AppLogger.LogError("Network connectivity test failed");
                        throw new Exception("Network connectivity test failed");
                    }
                    else
                    {
                        networkStatus.Text = "OK";
                        networkStatus.BackColor = Color.LightGreen;
                        AppLogger.LogInfo("Network connectivity test passed");
                    }
                }
            }
            catch (Exception ex)
            {
                string errorDetails = ex.Message;
                AppLogger.LogError("Connectivity test failed", ex);
            }
            finally
            {
                testNetworkConnectivityButton.Text = "Probar conectividad";
                testNetworkConnectivityButton.Enabled = true;
            }

        }

        private async void testStoresApiConnectivityButton_Click(object sender, EventArgs e)
        {
            storesApiStatus.Text = "Probando...";
            testStoresApiConnectivityButton.Text = "Probando...";
            testStoresApiConnectivityButton.Enabled = false;

            try
            {
                AppLogger.LogInfo("Starting Store API connectivity test");
                apiService.ReloadConfiguration();

                string token = await apiService.GetAccessTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Store API returned empty token");
                }
                storesApiStatus.Text = "OK";
                storesApiStatus.BackColor = Color.LightGreen;
                AppLogger.LogInfo("Store API connectivity test passed");
            }
            catch (Exception ex)
            {
                storesApiStatus.Text = "ERROR";
                storesApiStatus.BackColor = Color.LightCoral;
                AppLogger.LogError("Store API connectivity test failed", ex);
            }
            finally
            {
                testStoresApiConnectivityButton.Text = "Probar conectividad";
                testStoresApiConnectivityButton.Enabled = true;
            }
        }

        private async void testFirebaseConnectivityButton_Click(object sender, EventArgs e)
        {
            firebaseStatus.Text = "Probando...";
            testFirebaseConnectivityButton.Text = "Probando...";
            testFirebaseConnectivityButton.Enabled = false;

            FirebaseManager firebaseManager = null;

            try
            {
                AppLogger.LogInfo("Starting Firebase connectivity tests");
                firebaseManager = new FirebaseManager();

                bool firebaseConnectionTest = await firebaseManager.TestConnectionAsync();
                if (!firebaseConnectionTest)
                {
                    firebaseStatus.Text = "ERROR";
                    firebaseStatus.BackColor = Color.LightGreen;
                    throw new Exception("Firebase basic connection test failed");
                }

                bool firebaseAuthTest = await firebaseManager.TestAuthenticationAsync();
                if (!firebaseAuthTest)
                {
                    firebaseStatus.Text = "ERROR";
                    firebaseStatus.BackColor = Color.LightGreen;
                    throw new Exception("Firebase API key authentication failed");
                }

                bool firebasePathTest = await firebaseManager.TestDocumentPathAsync();
                if (!firebasePathTest)
                {
                    firebaseStatus.Text = "WARN";
                    firebaseStatus.BackColor = Color.Khaki;
                    AppLogger.LogWarning("Firebase path test failed. Document may not exist yet, but this can be normal");
                }
                else
                {
                    AppLogger.LogInfo("Firebase path test passed");
                }

                firebaseStatus.Text = "OK";
                firebaseStatus.BackColor = Color.LightGreen;
                AppLogger.LogInfo("Firebase connectivity tests passed");
            }
            catch (Exception ex)
            {
                firebaseStatus.Text = "ERROR";
                AppLogger.LogError("Firebase connectivity test failed", ex);
                throw new Exception($"Firebase test failed: {ex.Message}");
            }
            finally
            {
                firebaseManager.Dispose();
                testFirebaseConnectivityButton.Text = "Probar conectividad";
                testFirebaseConnectivityButton.Enabled = true;
            }
        }

        // TODO: Implement printing test.
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
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden");
        }

        #endregion
    }
}