using FTC_Generic_Printing_App_POC.Manager;
using FTC_Generic_Printing_App_POC.Models;
using FTC_Generic_Printing_App_POC.Services;
using FTC_Generic_Printing_App_POC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    public partial class TotemSettingsForm : Form
    {
        #region Fields
        private readonly StoresApiService storesApiService;
        private List<Store> availableStores;
        private bool isLoadingStores = false;
        #endregion

        #region Initialization
        public TotemSettingsForm()
        {
            InitializeComponent();
            storesApiService = new StoresApiService();
            availableStores = new List<Store>();
            PopulateComboBoxes();
            ClearForm();
            SetupEventHandlers();
        }

        private void TotemSettings_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Totem settings form opened");
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

                availableStores = await storesApiService.GetStoresAsync(selectedCountry, selectedBusiness);
                storeComboBox.Items.Clear();

                if (availableStores.Count > 0)
                {
                    foreach (var store in availableStores)
                    {
                        storeComboBox.Items.Add(store);
                    }

                    // Try to select previously saved store
                    var totemSettings = SettingsManager.LoadTotemSettings();
                    if (!string.IsNullOrEmpty(totemSettings.StoreId))
                    {
                        var savedStore = availableStores.FirstOrDefault(s => s.id == totemSettings.StoreId);
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

            idTotemTextBox.MaxLength = 4;
            idTotemTextBox.KeyPress += IdTotemTextBox_KeyPress;
        }

        private void IdTotemTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only alphanumeric characters and control keys (like backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the character
                AppLogger.LogWarning($"Blocked non-alphanumeric character: {e.KeyChar}");
            }
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
            AppLogger.LogInfo("Totem settings form hidden");
        }

        private void saveTotemSettingsButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            ClearForm();
        }

        private void cleanTotemSettingsButton_Click(object sender, EventArgs e)
        {
            ClearForm();
            AppLogger.LogInfo("Totem settings form cleaned");
        }

        private void cancelTotemSettingsButton_Click(object sender, EventArgs e)
        {
            CancelSettings();
        }
        #endregion

        #region Core Methods
        private void SaveSettings()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Totem settings button");

                string idTotem = idTotemTextBox.Text.Trim();
                string selectedCountry = countryComboBox.SelectedItem?.ToString() ?? "";
                string selectedBusiness = businessComboBox.SelectedItem?.ToString() ?? "";

                Store selectedStoreObj = storeComboBox.SelectedItem as Store;
                string selectedStore = selectedStoreObj?.name ?? "";
                string selectedStoreId = selectedStoreObj?.id ?? "";

                if (string.IsNullOrEmpty(idTotem))
                {
                    AppLogger.LogWarning("Validation failed: ID Totem is empty");
                    MessageBox.Show("Por favor ingrese un ID Totem válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    idTotemTextBox.Focus();
                    return;
                }

                if (idTotem.Length > 4)
                {
                    AppLogger.LogWarning("Validation failed: ID Totem exceeds maximum length");
                    MessageBox.Show("El ID Totem no puede tener más de 4 caracteres.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    idTotemTextBox.Focus();
                    return;
                }

                // Alphanumeric characters only validation
                if (!idTotem.All(char.IsLetterOrDigit))
                {
                    AppLogger.LogWarning("Validation failed: ID Totem contains non-alphanumeric characters");
                    MessageBox.Show("El ID Totem solo puede contener letras y números.", "Error de validación",
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

                var totem = new Totem
                {
                    IdTotem = idTotem,
                    Country = selectedCountry,
                    Business = selectedBusiness,
                    Store = selectedStore,
                    StoreId = selectedStoreId
                };

                SettingsManager.SaveTotemSettings(totem);
                NotifyFirebaseSettingsChanged();

                AppLogger.LogInfo($"Totem settings saved with StoreId: {selectedStoreId}, Store: {selectedStore}");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Totem settings", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotifyFirebaseSettingsChanged()
        {
            try
            {
                AppLogger.LogInfo("Reloading Firebase settings");
                FirebaseManager.Instance.ReloadSettings(false);
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Failed to notify Firebase about settings change", ex);
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

                AppLogger.LogInfo("Totem settings controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Totem edit settings panel", ex);
            }
        }

        private void ResetStoreComboBox()
        {
            storeComboBox.Items.Clear();
            storeComboBox.Items.Add("Seleccione país y negocio primero");
            storeComboBox.SelectedIndex = 0;
            storeComboBox.Enabled = false;
        }

        private void CancelSettings()
        {
            try
            {
                this.Hide();
                ClearForm();
                AppLogger.LogInfo("Totem settings form hidden and edit panel reset values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error hiding Totem settings form and resetting edit panel", ex);
            }
        }
        #endregion
    }
}

