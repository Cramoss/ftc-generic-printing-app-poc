namespace FTC_Generic_Printing_App_POC
{
    partial class StoreApiConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.configurarStoresApiLabel = new System.Windows.Forms.Label();
            this.editStoresApiConfigurationPanel = new System.Windows.Forms.Panel();
            this.storesApiAuthUrlTextBox = new System.Windows.Forms.TextBox();
            this.storesApiClientSecretTextBox = new System.Windows.Forms.TextBox();
            this.storesApiClientIdTextBox = new System.Windows.Forms.TextBox();
            this.storesApiUrlTextBox = new System.Windows.Forms.TextBox();
            this.saveStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.cleanStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.restoreDefaultStoreApiConfigurationButton = new System.Windows.Forms.Button();
            this.cancelStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.storesApiTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.storeApiUrlLabel = new System.Windows.Forms.Label();
            this.storesApiClientSecretLabel = new System.Windows.Forms.Label();
            this.storesApiAuthUrlLabel = new System.Windows.Forms.Label();
            this.storesApiClientIdLabel = new System.Windows.Forms.Label();
            this.editStoresApiConfigurationPanel.SuspendLayout();
            this.storesApiTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // configurarStoresApiLabel
            // 
            this.configurarStoresApiLabel.AutoSize = true;
            this.configurarStoresApiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.configurarStoresApiLabel.Location = new System.Drawing.Point(6, 9);
            this.configurarStoresApiLabel.Name = "configurarStoresApiLabel";
            this.configurarStoresApiLabel.Size = new System.Drawing.Size(224, 26);
            this.configurarStoresApiLabel.TabIndex = 13;
            this.configurarStoresApiLabel.Text = "Configurar Stores API";
            // 
            // editStoresApiConfigurationPanel
            // 
            this.editStoresApiConfigurationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiTableLayoutPanel);
            this.editStoresApiConfigurationPanel.Location = new System.Drawing.Point(10, 60);
            this.editStoresApiConfigurationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.editStoresApiConfigurationPanel.Name = "editStoresApiConfigurationPanel";
            this.editStoresApiConfigurationPanel.Size = new System.Drawing.Size(449, 161);
            this.editStoresApiConfigurationPanel.TabIndex = 22;
            // 
            // storesApiAuthUrlTextBox
            // 
            this.storesApiAuthUrlTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiAuthUrlTextBox.Location = new System.Drawing.Point(137, 45);
            this.storesApiAuthUrlTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiAuthUrlTextBox.Name = "storesApiAuthUrlTextBox";
            this.storesApiAuthUrlTextBox.Size = new System.Drawing.Size(298, 20);
            this.storesApiAuthUrlTextBox.TabIndex = 26;
            // 
            // storesApiClientSecretTextBox
            // 
            this.storesApiClientSecretTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientSecretTextBox.Location = new System.Drawing.Point(137, 119);
            this.storesApiClientSecretTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiClientSecretTextBox.Name = "storesApiClientSecretTextBox";
            this.storesApiClientSecretTextBox.Size = new System.Drawing.Size(298, 20);
            this.storesApiClientSecretTextBox.TabIndex = 24;
            // 
            // storesApiClientIdTextBox
            // 
            this.storesApiClientIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientIdTextBox.Location = new System.Drawing.Point(137, 81);
            this.storesApiClientIdTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiClientIdTextBox.Name = "storesApiClientIdTextBox";
            this.storesApiClientIdTextBox.Size = new System.Drawing.Size(298, 20);
            this.storesApiClientIdTextBox.TabIndex = 22;
            // 
            // storesApiUrlTextBox
            // 
            this.storesApiUrlTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiUrlTextBox.Location = new System.Drawing.Point(137, 9);
            this.storesApiUrlTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiUrlTextBox.Name = "storesApiUrlTextBox";
            this.storesApiUrlTextBox.Size = new System.Drawing.Size(298, 20);
            this.storesApiUrlTextBox.TabIndex = 18;
            // 
            // saveStoresApiConfigurationButton
            // 
            this.saveStoresApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveStoresApiConfigurationButton.Location = new System.Drawing.Point(11, 225);
            this.saveStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveStoresApiConfigurationButton.Name = "saveStoresApiConfigurationButton";
            this.saveStoresApiConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.saveStoresApiConfigurationButton.TabIndex = 18;
            this.saveStoresApiConfigurationButton.Text = "Guardar";
            this.saveStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.saveStoresApiConfigurationButton.Click += new System.EventHandler(this.saveStoresApiConfigurationButton_Click);
            // 
            // cleanStoresApiConfigurationButton
            // 
            this.cleanStoresApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanStoresApiConfigurationButton.Location = new System.Drawing.Point(115, 225);
            this.cleanStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanStoresApiConfigurationButton.Name = "cleanStoresApiConfigurationButton";
            this.cleanStoresApiConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.cleanStoresApiConfigurationButton.TabIndex = 23;
            this.cleanStoresApiConfigurationButton.Text = "Limpiar";
            this.cleanStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.cleanStoresApiConfigurationButton.Click += new System.EventHandler(this.cleanStoresApiConfigurationButton_Click);
            // 
            // restoreDefaultStoreApiConfigurationButton
            // 
            this.restoreDefaultStoreApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.restoreDefaultStoreApiConfigurationButton.Location = new System.Drawing.Point(219, 225);
            this.restoreDefaultStoreApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.restoreDefaultStoreApiConfigurationButton.Name = "restoreDefaultStoreApiConfigurationButton";
            this.restoreDefaultStoreApiConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.restoreDefaultStoreApiConfigurationButton.TabIndex = 24;
            this.restoreDefaultStoreApiConfigurationButton.Text = "Restaurar";
            this.restoreDefaultStoreApiConfigurationButton.UseVisualStyleBackColor = true;
            this.restoreDefaultStoreApiConfigurationButton.Click += new System.EventHandler(this.restoreDefaultStoreApiConfigurationButton_Click);
            // 
            // cancelStoresApiConfigurationButton
            // 
            this.cancelStoresApiConfigurationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelStoresApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelStoresApiConfigurationButton.Location = new System.Drawing.Point(359, 225);
            this.cancelStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelStoresApiConfigurationButton.Name = "cancelStoresApiConfigurationButton";
            this.cancelStoresApiConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.cancelStoresApiConfigurationButton.TabIndex = 25;
            this.cancelStoresApiConfigurationButton.Text = "Cancelar";
            this.cancelStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.cancelStoresApiConfigurationButton.Click += new System.EventHandler(this.cancelStoresApiConfigurationButton_Click);
            // 
            // storesApiTableLayoutPanel
            // 
            this.storesApiTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.storesApiTableLayoutPanel.ColumnCount = 2;
            this.storesApiTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.storesApiTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiAuthUrlTextBox, 1, 1);
            this.storesApiTableLayoutPanel.Controls.Add(this.storeApiUrlLabel, 0, 0);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientSecretLabel, 0, 3);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientIdTextBox, 1, 2);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiAuthUrlLabel, 0, 1);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientIdLabel, 0, 2);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiUrlTextBox, 1, 0);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientSecretTextBox, 1, 3);
            this.storesApiTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.storesApiTableLayoutPanel.Name = "storesApiTableLayoutPanel";
            this.storesApiTableLayoutPanel.RowCount = 4;
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.Size = new System.Drawing.Size(440, 150);
            this.storesApiTableLayoutPanel.TabIndex = 26;
            // 
            // storeApiUrlLabel
            // 
            this.storeApiUrlLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storeApiUrlLabel.AutoSize = true;
            this.storeApiUrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeApiUrlLabel.Location = new System.Drawing.Point(5, 9);
            this.storeApiUrlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storeApiUrlLabel.Name = "storeApiUrlLabel";
            this.storeApiUrlLabel.Size = new System.Drawing.Size(46, 20);
            this.storeApiUrlLabel.TabIndex = 0;
            this.storeApiUrlLabel.Text = "URL:";
            this.storeApiUrlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storesApiClientSecretLabel
            // 
            this.storesApiClientSecretLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientSecretLabel.AutoSize = true;
            this.storesApiClientSecretLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.storesApiClientSecretLabel.Location = new System.Drawing.Point(5, 119);
            this.storesApiClientSecretLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiClientSecretLabel.Name = "storesApiClientSecretLabel";
            this.storesApiClientSecretLabel.Size = new System.Drawing.Size(79, 20);
            this.storesApiClientSecretLabel.TabIndex = 3;
            this.storesApiClientSecretLabel.Text = "C. Secret:";
            // 
            // storesApiAuthUrlLabel
            // 
            this.storesApiAuthUrlLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiAuthUrlLabel.AutoSize = true;
            this.storesApiAuthUrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.storesApiAuthUrlLabel.Location = new System.Drawing.Point(5, 45);
            this.storesApiAuthUrlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiAuthUrlLabel.Name = "storesApiAuthUrlLabel";
            this.storesApiAuthUrlLabel.Size = new System.Drawing.Size(84, 20);
            this.storesApiAuthUrlLabel.TabIndex = 1;
            this.storesApiAuthUrlLabel.Text = "Auth URL:";
            // 
            // storesApiClientIdLabel
            // 
            this.storesApiClientIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientIdLabel.AutoSize = true;
            this.storesApiClientIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.storesApiClientIdLabel.Location = new System.Drawing.Point(5, 81);
            this.storesApiClientIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiClientIdLabel.Name = "storesApiClientIdLabel";
            this.storesApiClientIdLabel.Size = new System.Drawing.Size(74, 20);
            this.storesApiClientIdLabel.TabIndex = 2;
            this.storesApiClientIdLabel.Text = "Client ID:";
            // 
            // StoreApiConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelStoresApiConfigurationButton;
            this.ClientSize = new System.Drawing.Size(470, 261);
            this.Controls.Add(this.cancelStoresApiConfigurationButton);
            this.Controls.Add(this.restoreDefaultStoreApiConfigurationButton);
            this.Controls.Add(this.cleanStoresApiConfigurationButton);
            this.Controls.Add(this.saveStoresApiConfigurationButton);
            this.Controls.Add(this.editStoresApiConfigurationPanel);
            this.Controls.Add(this.configurarStoresApiLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StoreApiConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Stores API";
            this.editStoresApiConfigurationPanel.ResumeLayout(false);
            this.storesApiTableLayoutPanel.ResumeLayout(false);
            this.storesApiTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label configurarStoresApiLabel;
        private System.Windows.Forms.Panel editStoresApiConfigurationPanel;
        private System.Windows.Forms.TextBox storesApiClientSecretTextBox;
        private System.Windows.Forms.TextBox storesApiClientIdTextBox;
        private System.Windows.Forms.TextBox storesApiUrlTextBox;
        private System.Windows.Forms.Button saveStoresApiConfigurationButton;
        private System.Windows.Forms.TextBox storesApiAuthUrlTextBox;
        private System.Windows.Forms.Button cleanStoresApiConfigurationButton;
        private System.Windows.Forms.Button restoreDefaultStoreApiConfigurationButton;
        private System.Windows.Forms.Button cancelStoresApiConfigurationButton;
        private System.Windows.Forms.TableLayoutPanel storesApiTableLayoutPanel;
        private System.Windows.Forms.Label storeApiUrlLabel;
        private System.Windows.Forms.Label storesApiClientSecretLabel;
        private System.Windows.Forms.Label storesApiAuthUrlLabel;
        private System.Windows.Forms.Label storesApiClientIdLabel;
    }
}