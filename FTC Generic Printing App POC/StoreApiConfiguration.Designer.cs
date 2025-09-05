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
            this.label1 = new System.Windows.Forms.Label();
            this.editStoresApiConfigurationPanel = new System.Windows.Forms.Panel();
            this.saveStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.storesApiClientSecretTextBox = new System.Windows.Forms.TextBox();
            this.storesApiClientSecretEditLabel = new System.Windows.Forms.Label();
            this.storesApiClientIdTextBox = new System.Windows.Forms.TextBox();
            this.storesApiClientIdEditLabel = new System.Windows.Forms.Label();
            this.storesApiUrlTextBox = new System.Windows.Forms.TextBox();
            this.storesApiUrlEditLabel = new System.Windows.Forms.Label();
            this.storesApiAuthUrlTextBox = new System.Windows.Forms.TextBox();
            this.storesApiAuthUrlEditLabel = new System.Windows.Forms.Label();
            this.cleanStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.restoreDefaultStoreApiConfigurationButton = new System.Windows.Forms.Button();
            this.cancelStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.editStoresApiConfigurationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 37);
            this.label1.TabIndex = 13;
            this.label1.Text = "Configurar Store API";
            // 
            // editStoresApiConfigurationPanel
            // 
            this.editStoresApiConfigurationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiAuthUrlTextBox);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiAuthUrlEditLabel);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiClientSecretTextBox);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiClientSecretEditLabel);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiClientIdTextBox);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiClientIdEditLabel);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiUrlTextBox);
            this.editStoresApiConfigurationPanel.Controls.Add(this.storesApiUrlEditLabel);
            this.editStoresApiConfigurationPanel.Location = new System.Drawing.Point(11, 71);
            this.editStoresApiConfigurationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.editStoresApiConfigurationPanel.Name = "editStoresApiConfigurationPanel";
            this.editStoresApiConfigurationPanel.Size = new System.Drawing.Size(356, 330);
            this.editStoresApiConfigurationPanel.TabIndex = 22;
            // 
            // saveStoresApiConfigurationButton
            // 
            this.saveStoresApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveStoresApiConfigurationButton.Location = new System.Drawing.Point(87, 538);
            this.saveStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveStoresApiConfigurationButton.Name = "saveStoresApiConfigurationButton";
            this.saveStoresApiConfigurationButton.Size = new System.Drawing.Size(200, 40);
            this.saveStoresApiConfigurationButton.TabIndex = 18;
            this.saveStoresApiConfigurationButton.Text = "Guardar";
            this.saveStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.saveStoresApiConfigurationButton.Click += new System.EventHandler(this.saveStoresApiConfigurationButton_Click);
            // 
            // storesApiClientSecretTextBox
            // 
            this.storesApiClientSecretTextBox.Location = new System.Drawing.Point(24, 267);
            this.storesApiClientSecretTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiClientSecretTextBox.Name = "storesApiClientSecretTextBox";
            this.storesApiClientSecretTextBox.Size = new System.Drawing.Size(315, 20);
            this.storesApiClientSecretTextBox.TabIndex = 24;
            // 
            // storesApiClientSecretEditLabel
            // 
            this.storesApiClientSecretEditLabel.AutoSize = true;
            this.storesApiClientSecretEditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storesApiClientSecretEditLabel.Location = new System.Drawing.Point(19, 236);
            this.storesApiClientSecretEditLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiClientSecretEditLabel.Name = "storesApiClientSecretEditLabel";
            this.storesApiClientSecretEditLabel.Size = new System.Drawing.Size(151, 29);
            this.storesApiClientSecretEditLabel.TabIndex = 23;
            this.storesApiClientSecretEditLabel.Text = "Client Secret";
            // 
            // storesApiClientIdTextBox
            // 
            this.storesApiClientIdTextBox.Location = new System.Drawing.Point(35, 195);
            this.storesApiClientIdTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiClientIdTextBox.Name = "storesApiClientIdTextBox";
            this.storesApiClientIdTextBox.Size = new System.Drawing.Size(306, 20);
            this.storesApiClientIdTextBox.TabIndex = 22;
            // 
            // storesApiClientIdEditLabel
            // 
            this.storesApiClientIdEditLabel.AutoSize = true;
            this.storesApiClientIdEditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storesApiClientIdEditLabel.Location = new System.Drawing.Point(19, 164);
            this.storesApiClientIdEditLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiClientIdEditLabel.Name = "storesApiClientIdEditLabel";
            this.storesApiClientIdEditLabel.Size = new System.Drawing.Size(104, 29);
            this.storesApiClientIdEditLabel.TabIndex = 21;
            this.storesApiClientIdEditLabel.Text = "Client ID";
            // 
            // storesApiUrlTextBox
            // 
            this.storesApiUrlTextBox.Location = new System.Drawing.Point(22, 47);
            this.storesApiUrlTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiUrlTextBox.Name = "storesApiUrlTextBox";
            this.storesApiUrlTextBox.Size = new System.Drawing.Size(319, 20);
            this.storesApiUrlTextBox.TabIndex = 18;
            // 
            // storesApiUrlEditLabel
            // 
            this.storesApiUrlEditLabel.AutoSize = true;
            this.storesApiUrlEditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storesApiUrlEditLabel.Location = new System.Drawing.Point(19, 16);
            this.storesApiUrlEditLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiUrlEditLabel.Name = "storesApiUrlEditLabel";
            this.storesApiUrlEditLabel.Size = new System.Drawing.Size(60, 29);
            this.storesApiUrlEditLabel.TabIndex = 10;
            this.storesApiUrlEditLabel.Text = "URL";
            // 
            // storesApiAuthUrlTextBox
            // 
            this.storesApiAuthUrlTextBox.Location = new System.Drawing.Point(22, 122);
            this.storesApiAuthUrlTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiAuthUrlTextBox.Name = "storesApiAuthUrlTextBox";
            this.storesApiAuthUrlTextBox.Size = new System.Drawing.Size(319, 20);
            this.storesApiAuthUrlTextBox.TabIndex = 26;
            // 
            // storesApiAuthUrlEditLabel
            // 
            this.storesApiAuthUrlEditLabel.AutoSize = true;
            this.storesApiAuthUrlEditLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storesApiAuthUrlEditLabel.Location = new System.Drawing.Point(19, 91);
            this.storesApiAuthUrlEditLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiAuthUrlEditLabel.Name = "storesApiAuthUrlEditLabel";
            this.storesApiAuthUrlEditLabel.Size = new System.Drawing.Size(113, 29);
            this.storesApiAuthUrlEditLabel.TabIndex = 25;
            this.storesApiAuthUrlEditLabel.Text = "Auth URL";
            // 
            // cleanStoresApiConfigurationButton
            // 
            this.cleanStoresApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanStoresApiConfigurationButton.Location = new System.Drawing.Point(87, 579);
            this.cleanStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanStoresApiConfigurationButton.Name = "cleanStoresApiConfigurationButton";
            this.cleanStoresApiConfigurationButton.Size = new System.Drawing.Size(200, 40);
            this.cleanStoresApiConfigurationButton.TabIndex = 23;
            this.cleanStoresApiConfigurationButton.Text = "Limpiar campos";
            this.cleanStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.cleanStoresApiConfigurationButton.Click += new System.EventHandler(this.cleanStoresApiConfigurationButton_Click);
            // 
            // restoreDefaultStoreApiConfigurationButton
            // 
            this.restoreDefaultStoreApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.restoreDefaultStoreApiConfigurationButton.Location = new System.Drawing.Point(87, 623);
            this.restoreDefaultStoreApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.restoreDefaultStoreApiConfigurationButton.Name = "restoreDefaultStoreApiConfigurationButton";
            this.restoreDefaultStoreApiConfigurationButton.Size = new System.Drawing.Size(200, 40);
            this.restoreDefaultStoreApiConfigurationButton.TabIndex = 24;
            this.restoreDefaultStoreApiConfigurationButton.Text = "Restaurar configuración";
            this.restoreDefaultStoreApiConfigurationButton.UseVisualStyleBackColor = true;
            this.restoreDefaultStoreApiConfigurationButton.Click += new System.EventHandler(this.restoreDefaultStoreApiConfigurationButton_Click);
            // 
            // cancelStoresApiConfigurationButton
            // 
            this.cancelStoresApiConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelStoresApiConfigurationButton.Location = new System.Drawing.Point(87, 667);
            this.cancelStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelStoresApiConfigurationButton.Name = "cancelStoresApiConfigurationButton";
            this.cancelStoresApiConfigurationButton.Size = new System.Drawing.Size(200, 40);
            this.cancelStoresApiConfigurationButton.TabIndex = 25;
            this.cancelStoresApiConfigurationButton.Text = "Cancelar";
            this.cancelStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.cancelStoresApiConfigurationButton.Click += new System.EventHandler(this.cancelStoresApiConfigurationButton_Click);
            // 
            // StoreApiConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 744);
            this.Controls.Add(this.cancelStoresApiConfigurationButton);
            this.Controls.Add(this.restoreDefaultStoreApiConfigurationButton);
            this.Controls.Add(this.cleanStoresApiConfigurationButton);
            this.Controls.Add(this.saveStoresApiConfigurationButton);
            this.Controls.Add(this.editStoresApiConfigurationPanel);
            this.Controls.Add(this.label1);
            this.Name = "StoreApiConfiguration";
            this.Text = "StoreIdApiConfiguration";
            this.editStoresApiConfigurationPanel.ResumeLayout(false);
            this.editStoresApiConfigurationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel editStoresApiConfigurationPanel;
        private System.Windows.Forms.TextBox storesApiClientSecretTextBox;
        private System.Windows.Forms.Label storesApiClientSecretEditLabel;
        private System.Windows.Forms.TextBox storesApiClientIdTextBox;
        private System.Windows.Forms.Label storesApiClientIdEditLabel;
        private System.Windows.Forms.TextBox storesApiUrlTextBox;
        private System.Windows.Forms.Label storesApiUrlEditLabel;
        private System.Windows.Forms.Button saveStoresApiConfigurationButton;
        private System.Windows.Forms.TextBox storesApiAuthUrlTextBox;
        private System.Windows.Forms.Label storesApiAuthUrlEditLabel;
        private System.Windows.Forms.Button cleanStoresApiConfigurationButton;
        private System.Windows.Forms.Button restoreDefaultStoreApiConfigurationButton;
        private System.Windows.Forms.Button cancelStoresApiConfigurationButton;
    }
}