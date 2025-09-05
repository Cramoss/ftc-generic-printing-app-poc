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
        private List<Store> availableStores;
        private bool isLoadingStores = false;
        private bool isCurrentlyEditingTotemConfiguration = false;
        private bool isCurrentlyEditingStoresApiConfiguration = false;

        public Configuration()
        {
            InitializeComponent();
            apiService = new ApiService();
            availableStores = new List<Store>();
            PopulateComboBoxes();
            ResetEditTotemConfigurationPanel();
            ResetEditStoresApiConfigurationPanel();
            LoadSavedConfiguration();
            SetupEventHandlers();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Configuration form opened");
        }

        public ConfigurationData GetConfiguration()
        {
            Store selectedStoreObj = storeComboBox.SelectedItem as Store;

            return new ConfigurationData
            {
                IdTotem = idTotemTextBox.Text.Trim(),
                Country = countryComboBox.SelectedItem?.ToString() ?? "",
                Business = businessComboBox.SelectedItem?.ToString() ?? "",
                Store = selectedStoreObj?.name ?? "",
                StoreId = selectedStoreObj?.id ?? ""
            };
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);

            if (value)
            {
                RefreshConfigurationLabels();
                ResetEditTotemConfigurationPanel();
            }
        }


        // Display current configuration
        private void LoadSavedConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading saved configuration..");
                var config = ConfigurationManager.LoadConfiguration();

                idTotemTextBox.Text = config.IdTotem;

                if (!string.IsNullOrEmpty(config.Country))
                {
                    countryComboBox.Text = config.Country;
                    businessComboBox.Enabled = true;
                }

                // Load business second (only if country was loaded and selected)
                if (!string.IsNullOrEmpty(config.Business) && countryComboBox.SelectedIndex != -1)
                {
                    businessComboBox.Text = config.Business;
                    // Stores will be loaded automatically on the OnBusinessChanged event
                }

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


        // Edit Totem configuration
        private async Task LoadStoresAsync()
        {
            if (isLoadingStores) return;

            try
            {
                isLoadingStores = true;

                // Show loading state on the combo box
                storeComboBox.Items.Clear();
                storeComboBox.Items.Add("Cargando tiendas...");
                storeComboBox.SelectedIndex = 0;
                storeComboBox.Enabled = false;

                string selectedCountry = countryComboBox.SelectedItem?.ToString();
                string selectedBusiness = businessComboBox.SelectedItem?.ToString();

                AppLogger.LogInfo($"Loading stores for Country: {selectedCountry}, Business: {selectedBusiness}");

                availableStores = await apiService.GetStoresAsync(selectedCountry, selectedBusiness);
                storeComboBox.Items.Clear();

                if (availableStores.Count > 0)
                {
                    foreach (var store in availableStores)
                    {
                        storeComboBox.Items.Add(store);
                    }

                    // Try to select previously saved store
                    var savedConfig = ConfigurationManager.LoadConfiguration();
                    if (!string.IsNullOrEmpty(savedConfig.StoreId))
                    {
                        var savedStore = availableStores.FirstOrDefault(s => s.id == savedConfig.StoreId);
                        if (savedStore != null)
                        {
                            storeComboBox.SelectedItem = savedStore;
                        }
                        else
                        {
                            // Default to first if saved store was not found
                            storeComboBox.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        storeComboBox.SelectedIndex = 0;
                    }

                    storeComboBox.Enabled = true;
                    AppLogger.LogInfo($"Loaded {availableStores.Count} stores successfully");
                }
                else
                {
                    storeComboBox.Items.Add("No hay tiendas disponibles");
                    storeComboBox.SelectedIndex = 0;
                    storeComboBox.Enabled = false;
                    AppLogger.LogWarning("No stores found for selected country/business combination");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading stores", ex);

                storeComboBox.Items.Clear();
                storeComboBox.Items.Add("Error cargando tiendas");
                storeComboBox.SelectedIndex = 0;
                storeComboBox.Enabled = false;

                MessageBox.Show($"Error al cargar tiendas: {ex.Message}", "Error de conectividad",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoadingStores = false;
            }
        }

        private void PopulateComboBoxes()
        {
            // TODO: Evaluate if countries and business have to keep being hardcoded or not
            try
            {
                countryComboBox.Items.Clear();
                countryComboBox.Items.AddRange(new string[]
                {
                    "Chile", "Peru", "Colombia"
                });
                countryComboBox.SelectedIndex = -1;

                businessComboBox.Items.Clear();
                businessComboBox.Items.AddRange(new string[]
                {
                    "Falabella", "Sodimac", "Tottus"
                });
                businessComboBox.SelectedIndex = -1;
                businessComboBox.Enabled = false;

                ResetStoreComboBox();

                AppLogger.LogInfo("ComboBoxes populated");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error populating ComboBoxes", ex);
            }
        }

        private void SaveTotemConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Totem Configuration button");

                string idTotem = idTotemTextBox.Text.Trim();
                string selectedCountry = countryComboBox.SelectedItem?.ToString() ?? "";
                string selectedBusiness = businessComboBox.SelectedItem?.ToString() ?? "";

                Store selectedStoreObj = storeComboBox.SelectedItem as Store;
                string selectedStore = selectedStoreObj?.name ?? "";
                string selectedStoreId = selectedStoreObj?.id ?? "";

                // Required fields validation
                // TODO: Additional Totem ID value validations??
                if (string.IsNullOrEmpty(idTotem))
                {
                    AppLogger.LogWarning("Validation failed: ID Totem is empty");
                    MessageBox.Show("Por favor ingrese un ID Totem válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    idTotemTextBox.Focus();
                    return;
                }

                if (countryComboBox.SelectedIndex == -1)
                {
                    AppLogger.LogWarning("Validation failed: No country selected");
                    MessageBox.Show("Por favor seleccione un país.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    countryComboBox.Focus();
                    return;
                }

                if (businessComboBox.SelectedIndex == -1)
                {
                    AppLogger.LogWarning("Validation failed: No business type selected");
                    MessageBox.Show("Por favor seleccione un negocio.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    businessComboBox.Focus();
                    return;
                }

                if (selectedStoreObj == null || string.IsNullOrEmpty(selectedStoreId))
                {
                    AppLogger.LogWarning("Validation failed: No valid store selected");
                    MessageBox.Show("Por favor seleccione una tienda válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storeComboBox.Focus();
                    return;
                }

                var config = new ConfigurationData
                {
                    IdTotem = idTotem,
                    Country = selectedCountry,
                    Business = selectedBusiness,
                    Store = selectedStore,
                    StoreId = selectedStoreId
                };

                ConfigurationManager.SaveConfiguration(config);

                AppLogger.LogInfo($"Configuration saved with StoreId: {selectedStoreId}, Store: {selectedStore}");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving configuration", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveTotemConfiguration();
            ResetEditTotemConfigurationPanel();
        }

        private void editTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            if (isCurrentlyEditingTotemConfiguration)
            {
                disableEditTotemConfigurationPanel();
                ResetEditTotemConfigurationPanel();
            }
            else
            {
                enableEditTotemConfigurationPanel();
            }
        }

        private void enableEditTotemConfigurationPanel()
        {
            isCurrentlyEditingTotemConfiguration = true;
            editTotemConfigurationButton.Text = "Cancelar edición";
            foreach (Control control in editTotemConfigurationPanel.Controls)
            {
                control.Enabled = true;
            }
        }

        private void disableEditTotemConfigurationPanel()
        {
            isCurrentlyEditingTotemConfiguration = false;
            editTotemConfigurationButton.Text = "Editar configuración";
            foreach (Control control in editTotemConfigurationPanel.Controls)
            {
                control.Enabled = false;
            }
        }


        // Edit Stores API configuration
        private void saveStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveTotemConfiguration();
            ResetEditTotemConfigurationPanel();
        }

        private void editStoresApiConfigurationButton_Click(object sender, EventArgs e)
        {
            if (isCurrentlyEditingStoresApiConfiguration)
            {
                disableEditStoresApiConfigurationPanel();
                ResetEditStoresApiConfigurationPanel();
            }
            else
            {
                enableEditStoresApiConfigurationPanel();
            }
        }

        private void enableEditStoresApiConfigurationPanel()
        {
            isCurrentlyEditingStoresApiConfiguration = true;
            editStoresApiConfigurationButton.Text = "Cancelar edición";
            foreach (Control control in editStoresApiConfigurationPanel.Controls)
            {
                control.Enabled = true;
            }
        }

        private void disableEditStoresApiConfigurationPanel()
        {
            isCurrentlyEditingStoresApiConfiguration = false;
            editStoresApiConfigurationButton.Text = "Editar configuración";
            foreach (Control control in editStoresApiConfigurationPanel.Controls)
            {
                control.Enabled = false;
            }
        }


        // Testing
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


        // Event handlers
        private void SetupEventHandlers()
        {
            countryComboBox.SelectedIndexChanged += OnCountryChanged;
            businessComboBox.SelectedIndexChanged += OnBusinessChanged;
        }

        private void OnCountryChanged(object sender, EventArgs e)
        {
            if (countryComboBox.SelectedIndex != -1)
            {
                businessComboBox.Enabled = true;
                businessComboBox.SelectedIndex = -1;
                ResetStoreComboBox();
            }
            else
            {
                // No country selected
                businessComboBox.Enabled = false;
                businessComboBox.SelectedIndex = -1;
                ResetStoreComboBox();
            }
        }

        private async void OnBusinessChanged(object sender, EventArgs e)
        {
            if (businessComboBox.SelectedIndex != -1 && countryComboBox.SelectedIndex != -1)
            {
                await LoadStoresAsync();
            }
            else
            {
                // Business not selected
                ResetStoreComboBox();
            }
        }


        // Window management
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden");
        }

        private void exitConfigurationButton_Click(object sender, EventArgs e)
        {
            ResetEditTotemConfigurationPanel();
            ResetEditStoresApiConfigurationPanel();
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden and edit panel reset values");
        }


        // Reset values
        private void ResetStoreComboBox()
        {
            storeComboBox.Items.Clear();
            storeComboBox.Items.Add("Seleccione país y negocio primero");
            storeComboBox.SelectedIndex = 0;
            storeComboBox.Enabled = false;
        }

        private void ResetEditTotemConfigurationPanel()
        {
            try
            {
                idTotemTextBox.Text = "";

                countryComboBox.SelectedIndex = -1;
                businessComboBox.SelectedIndex = -1;
                businessComboBox.Enabled = false;

                ResetStoreComboBox();
                disableEditTotemConfigurationPanel();

                AppLogger.LogInfo("Totem edit configuration panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Totem edit configuration panel", ex);
            }
        }

        private void ResetEditStoresApiConfigurationPanel()
        {
            try
            {
                storesApiUrlTextBox.Text = "";
                storesApiKeyTextBox.Text = "";
                storesApiCliendIdTextBox.Text = "";
                storesApiClientSecretTextBox.Text = "";

                disableEditStoresApiConfigurationPanel();

                AppLogger.LogInfo("Stores API edit configuration panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Stores API edit configuration panel", ex);
            }
        }
    }
}