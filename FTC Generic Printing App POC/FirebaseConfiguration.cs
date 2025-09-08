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
    // TODO: Hide icon from taskbar when form is open.
    public partial class FirebaseConfiguration : Form
    {
        #region Fields
        private FirebaseManager firebaseManager;
        #endregion

        #region Initialization
        public FirebaseConfiguration()
        {
            InitializeComponent();
        }

        private void FirebaseConfiguration_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            AppLogger.LogInfo("Firebase Configuration form opened");
        }
        #endregion

        #region Event Handlers
        private void saveFirebaseConfigurationButton_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
            ClearForm();
        }

        private void cleanFirebaseConfigurationButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void restoreDefaultDefaultConfigurationButton_Click(object sender, EventArgs e)
        {
            RestoreDefaultConfiguration();
        }

        private void cancelFirebaseConfigurationButton_Click(object sender, EventArgs e)
        {
            CancelConfiguration();
        }
        #endregion

        #region Core Methods
        private void SaveConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Save Firebase Configuration button");

                string databaseUrl = firebaseDatabaseTextBox.Text.Trim();
                string projectId = firebaseProjectIdTextBox.Text.Trim();
                string apiKey = firebaseKeyTextBox.Text.Trim();

                // TODO: Improve or add more validations.
                if (string.IsNullOrEmpty(databaseUrl))
                {
                    AppLogger.LogWarning("Validation failed: Firebase Database URL is empty");
                    MessageBox.Show("Por favor ingrese una URL de base de datos válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseDatabaseTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(projectId))
                {
                    AppLogger.LogWarning("Validation failed: Firebase Project ID is empty");
                    MessageBox.Show("Por favor ingrese un ID de proyecto válido.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseProjectIdTextBox.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(apiKey))
                {
                    AppLogger.LogWarning("Validation failed: Firebase API Key is empty");
                    MessageBox.Show("Por favor ingrese una API Key válida.", "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    firebaseKeyTextBox.Focus();
                    return;
                }

                ConfigurationManager.SaveFirebaseConfiguration(databaseUrl, projectId, apiKey);

                if (firebaseManager != null)
                {
                    firebaseManager.ReloadConfiguration();
                }

                AppLogger.LogInfo($"Firebase configuration saved successfully");
                this.Hide();
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error saving Firebase configuration", ex);
                MessageBox.Show("Error al guardar configuración: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            try
            {
                firebaseDatabaseTextBox.Text = "";
                firebaseProjectIdTextBox.Text = "";
                firebaseKeyTextBox.Text = "";

                AppLogger.LogInfo("Firebase edit configuration panel controls reset to default values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error resetting Firebase edit configuration panel", ex);
            }
        }

        private void RestoreDefaultConfiguration()
        {
            try
            {
                AppLogger.LogInfo("User clicked Restore Default Firebase Configuration button");

                DialogResult result = MessageBox.Show(
                    "¿Está seguro que desea restaurar la configuración de Firebase a los valores predeterminados?" +
                    Environment.NewLine + Environment.NewLine +
                    "Los valores actuales serán reemplazados por los valores predeterminados del archivo defaultConfig.xml.",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get default values from defaultConfig.xml
                    string databaseUrl = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_FIREBASE_DB_URL);
                    string projectId = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_FIREBASE_PROJECT_ID);
                    string apiKey = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_FIREBASE_API_KEY);

                    if (string.IsNullOrEmpty(databaseUrl) || string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(apiKey))
                    {
                        AppLogger.LogWarning("Some default Firebase configuration values were not found in defaultConfig.xml");
                        MessageBox.Show(
                            "Algunos valores predeterminados no fueron encontrados en el archivo defaultConfig.xml.",
                            "Error de restauración",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    ConfigurationManager.SaveFirebaseConfiguration(databaseUrl, projectId, apiKey);

                    if (firebaseManager != null)
                    {
                        firebaseManager.ReloadConfiguration();
                    }

                    AppLogger.LogInfo("Firebase configuration restored to default values successfully");
                    MessageBox.Show(
                        "La configuración de Firebase ha sido restaurada a los valores predeterminados correctamente.",
                        "Restauración exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.Hide();
                }
                else
                {
                    AppLogger.LogInfo("User cancelled restoring Firebase default configuration");
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error restoring Firebase default configuration", ex);
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
                AppLogger.LogInfo("Firebase configuration form hidden and edit panel reset values");
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error hiding Firebase configuration form and resetting edit panel", ex);
            }
        }
        #endregion
    }
}
