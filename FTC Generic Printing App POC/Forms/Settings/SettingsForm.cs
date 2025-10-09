using FTC_Generic_Printing_App_POC.Forms.Prompts;
using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Services;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    public partial class SettingsForm : Form
    {
        #region Fields
        private readonly StoresApiService storesApiService;
        private readonly PrinterService printerService;
        private bool isStoresApiInfoVisible = false;
        private bool isFirebaseInfoVisible = false;
        private Timer storesApiInfoTimer;
        private Timer firebaseInfoTimer;
        #endregion

        #region Initialization
        public SettingsForm()
        {
            storesApiService = new StoresApiService();
            printerService = new PrinterService();
            InitializeComponent();
            LoadSavedSettings();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            AppLogger.LogInfo("Settings form opened");

            if (FirebaseManager.Instance.IsListening)
            {
                FirebaseManager.Instance.StopListening();
            }

            // Display the current printer name set as default by Windows
            UpdateCurrentPrinterLabel();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);

            if (value)
            {
                if (FirebaseManager.Instance.IsListening)
                {
                    AppLogger.LogInfo("Stopping Firebase listener while settings form is open");
                    FirebaseManager.Instance.StopListening();
                }

                RefreshTotemSettingsLabels();
                RefreshStoresApiSettingsLabels();
            }
        }

        private void LoadSavedSettings()
        {
            try
            {
                AppLogger.LogInfo("Loading saved Totem settings..");
                var totemSettings = SettingsManager.LoadTotemSettings();

                currentTotemId.Text = !string.IsNullOrEmpty(totemSettings.IdTotem) ? totemSettings.IdTotem : "No configurado";
                currentCountry.Text = !string.IsNullOrEmpty(totemSettings.Country) ? totemSettings.Country : "No configurado";
                currentBusiness.Text = !string.IsNullOrEmpty(totemSettings.Business) ? totemSettings.Business : "No configurado";
                currentStore.Text = !string.IsNullOrEmpty(totemSettings.Store) ? totemSettings.Store : "No configurado";
                currentStoreId.Text = !string.IsNullOrEmpty(totemSettings.StoreId) ? totemSettings.StoreId : "No configurado";

                AppLogger.LogInfo("Totem settings loaded into the form and labels updated");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading Totem settings into the form", ex);

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
        private void editTotemSettingsButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Totem settings form");
            TotemSettingsForm totemSettingsForm = new TotemSettingsForm();
            totemSettingsForm.ShowDialog();

            RefreshTotemSettingsLabels();
        }
        private void refreshCurrentTotemSettingsButton_Click(object sender, EventArgs e)
        {
            RefreshTotemSettingsLabels();
        }

        // Stores API
        private void editStoresApiSettingsButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Stores API settings panel");
            StoresApiSettingsForm storesApiSettingsForm = new StoresApiSettingsForm();
            storesApiSettingsForm.ShowDialog();

            RefreshStoresApiSettingsLabels();
        }

        private void refreshCurrentStoresApiSettingsButton_Click(object sender, EventArgs e)
        {
            RefreshStoresApiSettingsLabels();
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
        private void editFirebaseSettingsButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("Opening Firebase settings panel");
            FirebaseSettingsForm firebaseSettingsForm = new FirebaseSettingsForm();
            firebaseSettingsForm.ShowDialog();

            RefreshFirebaseSettingsLabels();
        }

        private void refreshCurrentFirebaseSettingsButton_Click(object sender, EventArgs e)
        {
            RefreshFirebaseSettingsLabels();
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
        private void debugConsoleButton_Click(object sender, EventArgs e)
        {
            DebugManager.ManualToggle();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            ExitSettingsForm();
        }

        private void exitSettingsButton_Click(object sender, EventArgs e)
        {
            ExitSettingsForm();
        }
        #endregion

        #region Core Methods
        private void RefreshTotemSettingsLabels()
        {
            try
            {
                var totemSettings = SettingsManager.LoadTotemSettings();

                currentTotemId.Text = !string.IsNullOrEmpty(totemSettings.IdTotem) ? totemSettings.IdTotem : "No configurado";
                currentCountry.Text = !string.IsNullOrEmpty(totemSettings.Country) ? totemSettings.Country : "No configurado";
                currentBusiness.Text = !string.IsNullOrEmpty(totemSettings.Business) ? totemSettings.Business : "No configurado";
                currentStore.Text = !string.IsNullOrEmpty(totemSettings.Store) ? totemSettings.Store : "No configurado";
                currentStoreId.Text = !string.IsNullOrEmpty(totemSettings.StoreId) ? totemSettings.StoreId : "No configurado";

                AppLogger.LogInfo("Totem settings labels refreshed");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing Totem settings labels", ex);

                currentTotemId.Text = "Error";
                currentCountry.Text = "Error";
                currentBusiness.Text = "Error";
                currentStore.Text = "Error";
                currentStoreId.Text = "Error";
            }
        }

        private void RefreshStoresApiSettingsLabels()
        {
            try
            {
                AppLogger.LogInfo("Refreshing Stores API settings labels");
                var storesApiSettings = SettingsManager.LoadStoresApiSettings();

                if (isStoresApiInfoVisible)
                {
                    AppLogger.LogInfo("Stores API info is currently visible, refreshing with actual values");

                    currentStoresApiUrl.Text = !string.IsNullOrEmpty(storesApiSettings.StoresUrl) ?
                        storesApiSettings.StoresUrl : "No configurado";

                    currentStoresApiAuthUrl.Text = !string.IsNullOrEmpty(storesApiSettings.AuthUrl) ?
                        storesApiSettings.AuthUrl : "No configurado";

                    currentStoresApiClientId.Text = !string.IsNullOrEmpty(storesApiSettings.ClientId) ?
                        storesApiSettings.ClientId : "No configurado";

                    currentStoresApiClientSecret.Text = !string.IsNullOrEmpty(storesApiSettings.ClientSecret) ?
                        storesApiSettings.ClientSecret : "No configurado";
                }
                else
                {
                    currentStoresApiUrl.Text = !string.IsNullOrEmpty(storesApiSettings.StoresUrl) ?
                        "Oculto" : "No configurado";

                    currentStoresApiAuthUrl.Text = !string.IsNullOrEmpty(storesApiSettings.AuthUrl) ?
                        "Oculto" : "No configurado";

                    currentStoresApiClientId.Text = !string.IsNullOrEmpty(storesApiSettings.ClientId) ?
                        "Oculto" : "No configurado";

                    currentStoresApiClientSecret.Text = !string.IsNullOrEmpty(storesApiSettings.ClientSecret) ?
                        "Oculto" : "No configurado";
                }

                AppLogger.LogInfo("Stores API settings labels refreshed successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing Stores API settings labels", ex);

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
                AppLogger.LogInfo("Starting Stores API connectivity test");
                storesApiService.ReloadSettings();

                string token = await storesApiService.GetAccessTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Stores API returned empty token");
                }
                storesApiStatus.Text = "OK";
                storesApiStatus.BackColor = Color.LightGreen;
                AppLogger.LogInfo("Stores API connectivity test passed");
            }
            catch (Exception ex)
            {
                storesApiStatus.Text = "ERROR";
                storesApiStatus.BackColor = Color.LightCoral;
                AppLogger.LogError("Stores API connectivity test failed", ex);
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
            if (isStoresApiInfoVisible)
            {
                AppLogger.LogInfo("Hiding Stores API sensitive information");

                isStoresApiInfoVisible = false;
                RefreshStoresApiSettingsLabels();
                showStoresApiInfoButton.Text = "Mostrar info";

                if (storesApiInfoTimer != null)
                {
                    storesApiInfoTimer.Stop();
                    storesApiInfoTimer.Dispose();
                    storesApiInfoTimer = null;
                }

                return;
            }

            AppLogger.LogInfo("Show Stores API info button clicked");

            using (var passwordPrompt = new AdminPasswordPromptForm())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK && passwordPrompt.IsPasswordVerified)
                {
                    try
                    {
                        AppLogger.LogInfo("Admin password verification successful");

                        var storesApiSettings = SettingsManager.LoadStoresApiSettings();

                        currentStoresApiUrl.Text = !string.IsNullOrEmpty(storesApiSettings.StoresUrl) ?
                            storesApiSettings.StoresUrl : "No configurado";

                        currentStoresApiAuthUrl.Text = !string.IsNullOrEmpty(storesApiSettings.AuthUrl) ?
                            storesApiSettings.AuthUrl : "No configurado";

                        currentStoresApiClientId.Text = !string.IsNullOrEmpty(storesApiSettings.ClientId) ?
                            storesApiSettings.ClientId : "No configurado";

                        currentStoresApiClientSecret.Text = !string.IsNullOrEmpty(storesApiSettings.ClientSecret) ?
                            storesApiSettings.ClientSecret : "No configurado";

                        showStoresApiInfoButton.Text = "Ocultar info";
                        isStoresApiInfoVisible = true;

                        if (storesApiInfoTimer != null)
                        {
                            storesApiInfoTimer.Stop();
                            storesApiInfoTimer.Dispose();
                        }

                        storesApiInfoTimer = new System.Windows.Forms.Timer();
                        storesApiInfoTimer.Interval = 30000;
                        storesApiInfoTimer.Tick += (s, args) =>
                        {
                            isStoresApiInfoVisible = false;
                            RefreshStoresApiSettingsLabels();
                            showStoresApiInfoButton.Text = "Mostrar info";
                            storesApiInfoTimer.Stop();
                            storesApiInfoTimer.Dispose();
                            storesApiInfoTimer = null;
                            AppLogger.LogInfo("Stores API sensitive information hidden automatically after timeout");
                        };
                        storesApiInfoTimer.Start();
                        AppLogger.LogInfo("Stores API sensitive information will be hidden in 30 seconds");
                    }
                    catch (Exception ex)
                    {
                        AppLogger.LogError("Error displaying Stores API information", ex);
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
                RefreshFirebaseSettingsLabels();
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

            using (var passwordPrompt = new AdminPasswordPromptForm())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK && passwordPrompt.IsPasswordVerified)
                {
                    try
                    {
                        AppLogger.LogInfo("Admin password verification successful");

                        var firebaseSettings = SettingsManager.LoadFirebaseSettings();

                        currentFirebaseDatabase.Text = !string.IsNullOrEmpty(firebaseSettings.DatabaseUrl) ?
                            firebaseSettings.DatabaseUrl : "No configurado";

                        currentFirebaseProjectId.Text = !string.IsNullOrEmpty(firebaseSettings.ProjectId) ?
                            firebaseSettings.ProjectId : "No configurado";

                        currentFirebaseApiKey.Text = !string.IsNullOrEmpty(firebaseSettings.ApiKey) ?
                            firebaseSettings.ApiKey : "No configurado";

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
                            RefreshFirebaseSettingsLabels();
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

        private void RefreshFirebaseSettingsLabels()
        {
            try
            {
                AppLogger.LogInfo("Refreshing Firebase settings labels");
                var firebaseSettings = SettingsManager.LoadFirebaseSettings();

                if (isFirebaseInfoVisible)
                {
                    AppLogger.LogInfo("Firebase info is currently visible, refreshing with actual values");

                    currentFirebaseDatabase.Text = !string.IsNullOrEmpty(firebaseSettings.DatabaseUrl) ?
                        firebaseSettings.DatabaseUrl : "No configurado";

                    currentFirebaseProjectId.Text = !string.IsNullOrEmpty(firebaseSettings.ProjectId) ?
                        firebaseSettings.ProjectId : "No configurado";

                    currentFirebaseApiKey.Text = !string.IsNullOrEmpty(firebaseSettings.ApiKey) ?
                        firebaseSettings.ApiKey : "No configurado";
                }
                else
                {
                    currentFirebaseDatabase.Text = !string.IsNullOrEmpty(firebaseSettings.DatabaseUrl) ?
                        "Oculto" : "No configurado";

                    currentFirebaseProjectId.Text = !string.IsNullOrEmpty(firebaseSettings.ProjectId) ?
                        "Oculto" : "No configurado";

                    currentFirebaseApiKey.Text = !string.IsNullOrEmpty(firebaseSettings.ApiKey) ?
                        "Oculto" : "No configurado";
                }

                AppLogger.LogInfo("Firebase settings labels refreshed successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error refreshing Firebase settings labels", ex);

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
                testDocument["id_plantilla"] = "test";
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

        private void UpdateCurrentPrinterLabel()
        {
            try
            {
                string printerName = printerService.GetPrinterName();
                currentPrinter.Text = !string.IsNullOrEmpty(printerName) ? printerName : "No configurada";

                AppLogger.LogInfo($"Current default printer displayed: {printerName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error getting current printer name", ex);
                currentPrinter.Text = "Error";
            }
        }

        private void ExitSettingsForm()
        {
            this.Hide();
            AppLogger.LogInfo("Settings form hidden");

            if (!FirebaseManager.Instance.IsListening)
            {
                AppLogger.LogInfo("Starting Firebase listener after settings form closed");

                // Task.Run to avoid blocking the UI thread
                Task.Run(async () =>
                {
                    try
                    {
                        await FirebaseManager.Instance.StartListeningAsync();
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