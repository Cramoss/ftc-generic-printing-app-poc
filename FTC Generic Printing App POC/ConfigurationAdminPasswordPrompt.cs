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
    public partial class ConfigurationAdminPasswordPrompt : Form
    {
        public bool IsPasswordVerified { get; private set; }

        public ConfigurationAdminPasswordPrompt()
        {
            InitializeComponent();
            adminPasswordTextBox.UseSystemPasswordChar = true;
            IsPasswordVerified = false;
        }

        private void acceptPasswordButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the password from defaultConfig.xml
                string correctPassword = ConfigurationManager.GetValueFromDefaultConfig(DefaultConfigKeys.CONFIG_DEFAULT_ADMIN_PASSWORD);

                if (string.IsNullOrEmpty(correctPassword))
                {
                    // If no password is found in defaultConfig.xml, use a hardcoded one
                    correctPassword = "F4l4b3ll4";
                    AppLogger.LogWarning("Admin password not found in defaultConfig.xml, using hardcoded password");
                }

                if (adminPasswordTextBox.Text == correctPassword)
                {
                    AppLogger.LogInfo("Admin password verification successful");
                    IsPasswordVerified = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    AppLogger.LogWarning("Admin password verification failed");
                    MessageBox.Show("Contraseña incorrecta.", "Error de verificación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    adminPasswordTextBox.SelectAll();
                    adminPasswordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogError("Error in admin password verification", ex);
                MessageBox.Show("Error al verificar contraseña: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelPasswordButton_Click(object sender, EventArgs e)
        {
            IsPasswordVerified = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
