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
    public partial class TotemConfiguration : Form
    {
        #region Fields
        private readonly StoresApiService apiService;
        private List<Store> availableStores;
        private bool isLoadingStores = false;
        #endregion

        #region Initialization
        public TotemConfiguration()
        {
            InitializeComponent();
            apiService = new StoresApiService();
            availableStores = new List<Store>();
            PopulateComboBoxes();
            ClearForm();
            SetupEventHandlers();
        }

        private void TotemConfiguration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Totem Configuration form opened");
        }

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
                    var savedConfig = ConfigurationManager.LoadTotemConfiguration();
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
        #endregion

        #region Event Handlers
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            AppLogger.LogInfo("Totem configuration form hidden");
        }

        private void saveTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            ClearForm();
        }

        private void cleanTotemConfigurationButton_Click(object sender, EventArgs e)
        {
            ClearForm();
            AppLogger.LogInfo("Totem configuration form cleaned");
        }

        private void cancelTotemConfgurationButton_Click(object sender, EventArgs e)
        {
            CancelConfiguration();
        }
        #endregion

        #region Core Methods
        private void SaveConfiguration()
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

                // TODO: Improve or add more validations.
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

                ConfigurationManager.SaveTotemConfiguration(config);

                AppLogger.LogInfo($"Totem configuration saved with StoreId: {selectedStoreId}, Store: {selectedStore}");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Totem configuration", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            try
            {
                idTotemTextBox.Text = "";

                countryComboBox.SelectedIndex = -1;
                businessComboBox.SelectedIndex = -1;
                businessComboBox.Enabled = false;

                ResetStoreComboBox();

                AppLogger.LogInfo("Totem configuration controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Totem edit configuration panel", ex);
            }
        }

        private void ResetStoreComboBox()
        {
            storeComboBox.Items.Clear();
            storeComboBox.Items.Add("Seleccione país y negocio primero");
            storeComboBox.SelectedIndex = 0;
            storeComboBox.Enabled = false;
        }

        private void CancelConfiguration()
        {
            try
            {
                this.Hide();
                ClearForm();
                AppLogger.LogInfo("Totem configuration form hidden and edit panel reset values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error hiding Totem configuration form and resetting edit panel", ex);
            }
        }
        #endregion
    }
}

