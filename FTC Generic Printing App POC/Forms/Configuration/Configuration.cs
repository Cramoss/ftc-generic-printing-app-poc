using FTC_Generic_Printing_App_POC.Manager;
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
        #region Fields
        private readonly StoresApiService apiService;
        private readonly Services.PrinterService printerService;
        private bool isStoreApiInfoVisible = false;
        private bool isFirebaseInfoVisible = false;
        private System.Windows.Forms.Timer storeApiInfoTimer;
        private System.Windows.Forms.Timer firebaseInfoTimer;
        #endregion

        #region Initialization
        public Configuration()
        {
            apiService = new StoresApiService();
            printerService = new Services.PrinterService();
            InitializeComponent();
            LoadSavedConfiguration();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            AppLogger.LogInfo("Configuration form opened");

            if (FirebaseListenerManager.Instance.IsListening)
            {
                FirebaseListenerManager.Instance.StopListening();
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);

            if (value)
            {
                if (FirebaseListenerManager.Instance.IsListening)
                {
                    AppLogger.LogInfo("Stopping Firebase listener while configuration form is open");
                    FirebaseListenerManager.Instance.StopListening();
                }

                RefreshTotemConfigurationLabels();
                RefreshStoresApiConfigurationLabels();
            }
        }

        private void LoadSavedConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading saved Totem configuration..");
                var config = ConfigurationManager.LoadTotemConfiguration();

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

                // TODO: Use constant ERROR label
                currentTotemId.Text = "Error";
                currentCountry.Text = "Error";
                currentBusiness.Text = "Error";
                currentStore.Text = "Error";
                currentStoreId.Text = "Error";
            }
        }
        #endregion

        #region Event Handlers
        // Totem
        private void editTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Totem configuration form");
            TotemConfiguration totemConfigForm = new TotemConfiguration();
            totemConfigForm.ShowDialog();

            RefreshTotemConfigurationLabels();
        }
        private void refreshCurrentTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            RefreshTotemConfigurationLabels();
        }

        // Stores API
        private void editStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Stores API configuration panel");
            StoreApiConfiguration storeApiConfigForm = new StoreApiConfiguration();
            storeApiConfigForm.ShowDialog();

            RefreshStoresApiConfigurationLabels();
        }

        private void refreshCurrentStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            RefreshStoresApiConfigurationLabels();
        }

        private async void testStoresApiConnectivityButton_Click(object sender, EventArgs e)
        {
            TestStoresApiConnectivity();
        }


        private void showStoresApiInfoButton_Click(object sender, EventArgs e)
        {
            ShowStoresApiInfo();
        }

        // Firebase
        private void editFirebaseConfigurationButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Firebase configuration panel");
            FirebaseConfiguration firebaseConfigurationForm = new FirebaseConfiguration();
            firebaseConfigurationForm.ShowDialog();

            RefreshFirebaseConfigurationLabels();
        }

        private void refreshCurrentFirebaseConfigurationButton_Click(object sender, EventArgs e)
        {
            RefreshFirebaseConfigurationLabels();
        }

        private async void testFirebaseConnectivityButton_Click(object sender, EventArgs e)
        {
            TestFirebaseConnectivity();
        }

        private void showFirebaseInfoButton_Click(object sender, EventArgs e)
        {
            ShowFirebaseInfo();
        }

        // Printer
        private void testDocumentPrintButton_Click(object sender, EventArgs e)
        {
            TestPrinter();
        }

        // Network
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            ExitConfigurationForm();
        }

        private void exitConfigurationButton_Click(object sender, EventArgs e)
        {
            ExitConfigurationForm();
        }
        #endregion

        #region Core Methods
        private void RefreshTotemConfigurationLabels()
        {
            try
            {
                var config = ConfigurationManager.LoadTotemConfiguration();

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

        private void RefreshStoresApiConfigurationLabels()
        {
            try
            {
                AppLogger.LogInfo("Refreshing Store API configuration labels");
                var storeApiConfig = ConfigurationManager.LoadStoresApiConfiguration();

                if (isStoreApiInfoVisible)
                {
                    AppLogger.LogInfo("Store API info is currently visible, refreshing with actual values");

                    currentStoresApiUrl.Text = !string.IsNullOrEmpty(storeApiConfig.StoresUrl) ?
                        storeApiConfig.StoresUrl : "No configurado";

                    currentStoresApiAuthUrl.Text = !string.IsNullOrEmpty(storeApiConfig.AuthUrl) ?
                        storeApiConfig.AuthUrl : "No configurado";

                    currentStoresApiClientId.Text = !string.IsNullOrEmpty(storeApiConfig.ClientId) ?
                        storeApiConfig.ClientId : "No configurado";

                    currentStoresApiClientSecret.Text = !string.IsNullOrEmpty(storeApiConfig.ClientSecret) ?
                        storeApiConfig.ClientSecret : "No configurado";
                }
                else
                {
                    currentStoresApiUrl.Text = !string.IsNullOrEmpty(storeApiConfig.StoresUrl) ?
                        "Oculto" : "No configurado";

                    currentStoresApiAuthUrl.Text = !string.IsNullOrEmpty(storeApiConfig.AuthUrl) ?
                        "Oculto" : "No configurado";

                    currentStoresApiClientId.Text = !string.IsNullOrEmpty(storeApiConfig.ClientId) ?
                        "Oculto" : "No configurado";

                    currentStoresApiClientSecret.Text = !string.IsNullOrEmpty(storeApiConfig.ClientSecret) ?
                        "Oculto" : "No configurado";
                }

                AppLogger.LogInfo("Store API configuration labels refreshed successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing Store API configuration labels", ex);

                currentStoresApiUrl.Text = "Error";
                currentStoresApiAuthUrl.Text = "Error";
                currentStoresApiClientId.Text = "Error";
                currentStoresApiClientSecret.Text = "Error";
            }
        }

        private async void TestStoresApiConnectivity()
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

        // TODO: Option to show full value (maybe increase label size??)
        // TODO: Avoid redundant code with ShowFirebaseInfo()
        // Note: Values shown are hidden again after 30 seconds.
        // Note 2: This button requires admin password verification.
        private void ShowStoresApiInfo()
        {
            if (isStoreApiInfoVisible)
            {
                AppLogger.LogInfo("Hiding Store API sensitive information");

                isStoreApiInfoVisible = false;
                RefreshStoresApiConfigurationLabels();
                showStoresApiInfoButton.Text = "Mostrar info";

                if (storeApiInfoTimer != null)
                {
                    storeApiInfoTimer.Stop();
                    storeApiInfoTimer.Dispose();
                    storeApiInfoTimer = null;
                }

                return;
            }

            AppLogger.LogInfo("Show Stores API info button clicked");

            using (var passwordPrompt = new ConfigurationAdminPasswordPrompt())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK && passwordPrompt.IsPasswordVerified)
                {
                    try
                    {
                        AppLogger.LogInfo("Admin password verification successful");

                        var storeApiConfig = ConfigurationManager.LoadStoresApiConfiguration();

                        currentStoresApiUrl.Text = !string.IsNullOrEmpty(storeApiConfig.StoresUrl) ?
                            storeApiConfig.StoresUrl : "No configurado";

                        currentStoresApiAuthUrl.Text = !string.IsNullOrEmpty(storeApiConfig.AuthUrl) ?
                            storeApiConfig.AuthUrl : "No configurado";

                        currentStoresApiClientId.Text = !string.IsNullOrEmpty(storeApiConfig.ClientId) ?
                            storeApiConfig.ClientId : "No configurado";

                        currentStoresApiClientSecret.Text = !string.IsNullOrEmpty(storeApiConfig.ClientSecret) ?
                            storeApiConfig.ClientSecret : "No configurado";

                        showStoresApiInfoButton.Text = "Ocultar info";
                        isStoreApiInfoVisible = true;

                        if (storeApiInfoTimer != null)
                        {
                            storeApiInfoTimer.Stop();
                            storeApiInfoTimer.Dispose();
                        }

                        storeApiInfoTimer = new System.Windows.Forms.Timer();
                        storeApiInfoTimer.Interval = 30000;
                        storeApiInfoTimer.Tick += (s, args) =>
                        {
                            isStoreApiInfoVisible = false;
                            RefreshStoresApiConfigurationLabels();
                            showStoresApiInfoButton.Text = "Mostrar info";
                            storeApiInfoTimer.Stop();
                            storeApiInfoTimer.Dispose();
                            storeApiInfoTimer = null;
                            AppLogger.LogInfo("Store API sensitive information hidden automatically after timeout");
                        };
                        storeApiInfoTimer.Start();
                        AppLogger.LogInfo("Store API sensitive information will be hidden in 30 seconds");
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Error displaying Store API information", ex);
                        MessageBox.Show("Error al mostrar la información: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void TestFirebaseConnectivity()
        {
            firebaseStatus.Text = "Probando...";
            testFirebaseConnectivityButton.Text = "Probando...";
            testFirebaseConnectivityButton.Enabled = false;

            FirebaseService firebaseManager = null;

            try
            {
                AppLogger.LogInfo("Starting Firebase connectivity tests");
                firebaseManager = new FirebaseService();

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
                firebaseStatus.BackColor = Color.LightCoral;
                AppLogger.LogError("Firebase connectivity test failed", ex);
            }
            finally
            {
                firebaseManager.Dispose();
                testFirebaseConnectivityButton.Text = "Probar conectividad";
                testFirebaseConnectivityButton.Enabled = true;
            }
        }

        // TODO: Option to show full value (maybe increase label size??)
        // TODO: Avoid redundant code with ShowStoresApiInfo()
        // Note: Values shown are hidden again after 30 seconds.
        // Note 2: This button requires admin password verification.
        private void ShowFirebaseInfo()
        {
            if (isFirebaseInfoVisible)
            {
                AppLogger.LogInfo("Hiding Firebase sensitive information");

                isFirebaseInfoVisible = false;
                RefreshFirebaseConfigurationLabels();
                showFirebaseInfoButton.Text = "Mostrar info";

                if (firebaseInfoTimer != null)
                {
                    firebaseInfoTimer.Stop();
                    firebaseInfoTimer.Dispose();
                    firebaseInfoTimer = null;
                }

                return;
            }

            AppLogger.LogInfo("Show Firebase info button clicked");

            using (var passwordPrompt = new ConfigurationAdminPasswordPrompt())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK && passwordPrompt.IsPasswordVerified)
                {
                    try
                    {
                        AppLogger.LogInfo("Admin password verification successful");

                        var firebaseConfig = ConfigurationManager.LoadFirebaseConfiguration();

                        currentFirebaseDatabase.Text = !string.IsNullOrEmpty(firebaseConfig.DatabaseUrl) ?
                            firebaseConfig.DatabaseUrl : "No configurado";

                        currentFirebaseProjectId.Text = !string.IsNullOrEmpty(firebaseConfig.ProjectId) ?
                            firebaseConfig.ProjectId : "No configurado";

                        currentFirebaseApiKey.Text = !string.IsNullOrEmpty(firebaseConfig.ApiKey) ?
                            firebaseConfig.ApiKey : "No configurado";

                        showFirebaseInfoButton.Text = "Ocultar info";
                        isFirebaseInfoVisible = true;

                        if (firebaseInfoTimer != null)
                        {
                            firebaseInfoTimer.Stop();
                            firebaseInfoTimer.Dispose();
                        }

                        firebaseInfoTimer = new System.Windows.Forms.Timer();
                        firebaseInfoTimer.Interval = 30000;
                        firebaseInfoTimer.Tick += (s, args) =>
                        {
                            isFirebaseInfoVisible = false;
                            RefreshFirebaseConfigurationLabels();
                            showFirebaseInfoButton.Text = "Mostrar info";
                            firebaseInfoTimer.Stop();
                            firebaseInfoTimer.Dispose();
                            firebaseInfoTimer = null;
                            AppLogger.LogInfo("Firebase sensitive information hidden automatically after timeout");
                        };
                        firebaseInfoTimer.Start();
                        AppLogger.LogInfo("Firebase sensitive information will be hidden in 30 seconds");
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Error displaying Firebase information", ex);
                        MessageBox.Show("Error al mostrar la información: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RefreshFirebaseConfigurationLabels()
        {
            try
            {
                AppLogger.LogInfo("Refreshing Firebase configuration labels");
                var firebaseConfig = ConfigurationManager.LoadFirebaseConfiguration();

                if (isFirebaseInfoVisible)
                {
                    AppLogger.LogInfo("Firebase info is currently visible, refreshing with actual values");

                    currentFirebaseDatabase.Text = !string.IsNullOrEmpty(firebaseConfig.DatabaseUrl) ?
                        firebaseConfig.DatabaseUrl : "No configurado";

                    currentFirebaseProjectId.Text = !string.IsNullOrEmpty(firebaseConfig.ProjectId) ?
                        firebaseConfig.ProjectId : "No configurado";

                    currentFirebaseApiKey.Text = !string.IsNullOrEmpty(firebaseConfig.ApiKey) ?
                        firebaseConfig.ApiKey : "No configurado";
                }
                else
                {
                    currentFirebaseDatabase.Text = !string.IsNullOrEmpty(firebaseConfig.DatabaseUrl) ?
                        "Oculto" : "No configurado";

                    currentFirebaseProjectId.Text = !string.IsNullOrEmpty(firebaseConfig.ProjectId) ?
                        "Oculto" : "No configurado";

                    currentFirebaseApiKey.Text = !string.IsNullOrEmpty(firebaseConfig.ApiKey) ?
                        "Oculto" : "No configurado";
                }

                AppLogger.LogInfo("Firebase configuration labels refreshed successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing Firebase configuration labels", ex);

                currentFirebaseDatabase.Text = "Error";
                currentFirebaseProjectId.Text = "Error";
                currentFirebaseApiKey.Text = "Error";
            }
        }

        private void TestPrinter()
        {
            AppLogger.LogInfo("Starting printer test");
            testDocumentPrintButton.Text = "Probando...";
            testDocumentPrintButton.Enabled = false;

            try
            {
                var testDocument = new Newtonsoft.Json.Linq.JObject();
                testDocument["template"] = "test";
                testDocument["data"] = "Documento de prueba";

                printerService.PrintDocumentAsync(testDocument).Wait();

                AppLogger.LogInfo("Printer test completed successfully");
                MessageBox.Show(
                    "Document de prueba enviado a la impresora correctamente.",
                    "Prueba de Impresión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Printer test failed", ex);
                MessageBox.Show(
                    $"Error al imprimir documento de prueba: {ex.Message}",
                    "Error de Impresión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                testDocumentPrintButton.Text = "Probar";
                testDocumentPrintButton.Enabled = true;
            }
        }

        private void ExitConfigurationForm()
        {
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden");

            if (!FirebaseListenerManager.Instance.IsListening)
            {
                AppLogger.LogInfo("Starting Firebase listener after configuration form closed");

                // Task.Run to avoid blocking the UI thread
                Task.Run(async () =>
                {
                    try
                    {
                        await FirebaseListenerManager.Instance.StartListeningAsync();
                        AppLogger.LogInfo("Firebase listener started successfully after form closed");
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Error starting Firebase listener after form closed", ex);
                    }
                });
            }

        }
        #endregion
    }
}