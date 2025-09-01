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
        public Configuration()
        {
            InitializeComponent();
            PopulateComboBoxes();
            LoadSavedConfiguration();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Configuration form opened");
        }

        // TODO: Populate combo boxes with the response from Stores API call.
        private void PopulateComboBoxes()
        {
            try
            {
                countryComboBox.Items.Clear();
                countryComboBox.Items.AddRange(new string[]
                {
                    "Chile", "Peru", "Colombia",
                });

                businessComboBox.Items.Clear();
                businessComboBox.Items.AddRange(new string[]
                {
                    "Falabella", "Sodimac", "Tottus",
                });

                storeComboBox.Items.Clear();
                storeComboBox.Items.AddRange(new string[]
                {
                    "Store 001 - Main Branch", "Store 002 - Downtown", "Store 003 - Mall Location",
                });

                AppLogger.LogInfo("ComboBoxes populated successfully");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error populating ComboBoxes", ex);
            }
        }

        private void LoadSavedConfiguration()
        {
            try
            {
                AppLogger.LogInfo("Loading saved configuration..");

                var config = ConfigurationManager.LoadConfiguration();

                idTotemTextBox.Text = config.IdTotem;

                if (!string.IsNullOrEmpty(config.Country))
                    countryComboBox.Text = config.Country;
                else if (countryComboBox.Items.Count > 0)
                    countryComboBox.SelectedIndex = 0;

                if (!string.IsNullOrEmpty(config.Business))
                    businessComboBox.Text = config.Business;
                else if (businessComboBox.Items.Count > 0)
                    businessComboBox.SelectedIndex = 0;

                if (!string.IsNullOrEmpty(config.Store))
                    storeComboBox.Text = config.Store;
                else if (storeComboBox.Items.Count > 0)
                    storeComboBox.SelectedIndex = 0;

                AppLogger.LogInfo("Configuration loaded into form");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error loading configuration into form", ex);
            }
        }

        private void SaveConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Configuration");

                // Fetch values from form
                string idTotem = idTotemTextBox.Text.Trim();
                string selectedCountry = countryComboBox.SelectedItem?.ToString() ?? "";
                string selectedBusiness = businessComboBox.SelectedItem?.ToString() ?? "";
                string selectedStore = storeComboBox.SelectedItem?.ToString() ?? "";

                // Required fields validation
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

                if (storeComboBox.SelectedIndex == -1)
                {
                    AppLogger.LogWarning("Validation failed: No store selected");
                    MessageBox.Show("Por favor seleccione una tienda.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    storeComboBox.Focus();
                    return;
                }

                var config = new ConfigurationData
                {
                    IdTotem = idTotem,
                    Country = selectedCountry,
                    Business = selectedBusiness,
                    Store = selectedStore
                };

                // Save configuration
                ConfigurationManager.SaveConfiguration(config);

                MessageBox.Show("Configuración guardada!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving configuration", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handlers
        private void label2_Click(object sender, EventArgs e) { }

        private void saveConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        // TODO: Implement connectivity test.
        // TODO: Update status info on disabled text boxes.
        private void testConnectivityButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("User clicked Test Connectivity");
            MessageBox.Show("Probando conectividad a Firebase...",
                "Test de conectividad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // TODO: Implement ticket print test.
        private void testTicketPrintButton_Click(object sender, EventArgs e)
        {
            AppLogger.LogInfo("User clicked Test Ticket Print");
            try
            {
                MessageBox.Show("Sending test ticket to printer...", "Test de impresión",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                AppLogger.LogPrintEvent("TEST", "Test ticket print requested by user");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Print test failed", ex);
                MessageBox.Show("Error al imprimir: " + ex.Message, "Error de test de impresión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            AppLogger.LogInfo("Configuration form hidden");
        }

        public ConfigurationData GetConfiguration()
        {
            return new ConfigurationData
            {
                IdTotem = idTotemTextBox.Text.Trim(),
                Country = countryComboBox.SelectedItem?.ToString() ?? "",
                Business = businessComboBox.SelectedItem?.ToString() ?? "",
                Store = storeComboBox.SelectedItem?.ToString() ?? ""
            };
        }
    }

    public class ConfigurationData
    {
        public string IdTotem { get; set; } = "";
        public string Country { get; set; } = "";
        public string Business { get; set; } = "";
        public string Store { get; set; } = "";
    }
}