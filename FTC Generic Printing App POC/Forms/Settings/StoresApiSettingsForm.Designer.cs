namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    partial class StoresApiSettingsForm
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
            this.editStoresApiSettingsLabel = new System.Windows.Forms.Label();
            this.editStoresApiSettingsPanel = new System.Windows.Forms.Panel();
            this.storesApiTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.storesApiAuthUrlTextBox = new System.Windows.Forms.TextBox();
            this.storesApiUrlLabel = new System.Windows.Forms.Label();
            this.storesApiClientSecretLabel = new System.Windows.Forms.Label();
            this.storesApiClientIdTextBox = new System.Windows.Forms.TextBox();
            this.storesApiAuthUrlLabel = new System.Windows.Forms.Label();
            this.storesApiClientIdLabel = new System.Windows.Forms.Label();
            this.storesApiUrlTextBox = new System.Windows.Forms.TextBox();
            this.storesApiClientSecretTextBox = new System.Windows.Forms.TextBox();
            this.saveStoresApiSettingsButton = new System.Windows.Forms.Button();
            this.cleanStoresApiSettingsButton = new System.Windows.Forms.Button();
            this.restoreDefaultStoresApiSettingsButton = new System.Windows.Forms.Button();
            this.cancelStoresApiSettingsButton = new System.Windows.Forms.Button();
            this.editStoresApiSettingsPanel.SuspendLayout();
            this.storesApiTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // editStoresApiSettingsLabel
            // 
            this.editStoresApiSettingsLabel.AutoSize = true;
            this.editStoresApiSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.editStoresApiSettingsLabel.Location = new System.Drawing.Point(9, 14);
            this.editStoresApiSettingsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.editStoresApiSettingsLabel.Name = "editStoresApiSettingsLabel";
            this.editStoresApiSettingsLabel.Size = new System.Drawing.Size(440, 51);
            this.editStoresApiSettingsLabel.TabIndex = 13;
            this.editStoresApiSettingsLabel.Text = "Configurar Stores API";
            // 
            // editStoresApiSettingsPanel
            // 
            this.editStoresApiSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editStoresApiSettingsPanel.Controls.Add(this.storesApiTableLayoutPanel);
            this.editStoresApiSettingsPanel.Location = new System.Drawing.Point(15, 92);
            this.editStoresApiSettingsPanel.Name = "editStoresApiSettingsPanel";
            this.editStoresApiSettingsPanel.Size = new System.Drawing.Size(672, 246);
            this.editStoresApiSettingsPanel.TabIndex = 22;
            // 
            // storesApiTableLayoutPanel
            // 
            this.storesApiTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.storesApiTableLayoutPanel.ColumnCount = 2;
            this.storesApiTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.storesApiTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiAuthUrlTextBox, 1, 1);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiUrlLabel, 0, 0);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientSecretLabel, 0, 3);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientIdTextBox, 1, 2);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiAuthUrlLabel, 0, 1);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientIdLabel, 0, 2);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiUrlTextBox, 1, 0);
            this.storesApiTableLayoutPanel.Controls.Add(this.storesApiClientSecretTextBox, 1, 3);
            this.storesApiTableLayoutPanel.Location = new System.Drawing.Point(4, 5);
            this.storesApiTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.storesApiTableLayoutPanel.Name = "storesApiTableLayoutPanel";
            this.storesApiTableLayoutPanel.RowCount = 4;
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.storesApiTableLayoutPanel.Size = new System.Drawing.Size(660, 231);
            this.storesApiTableLayoutPanel.TabIndex = 26;
            // 
            // storesApiAuthUrlTextBox
            // 
            this.storesApiAuthUrlTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiAuthUrlTextBox.Location = new System.Drawing.Point(204, 74);
            this.storesApiAuthUrlTextBox.Name = "storesApiAuthUrlTextBox";
            this.storesApiAuthUrlTextBox.Size = new System.Drawing.Size(445, 26);
            this.storesApiAuthUrlTextBox.TabIndex = 26;
            // 
            // storesApiUrlLabel
            // 
            this.storesApiUrlLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiUrlLabel.AutoSize = true;
            this.storesApiUrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storesApiUrlLabel.Location = new System.Drawing.Point(6, 11);
            this.storesApiUrlLabel.Name = "storesApiUrlLabel";
            this.storesApiUrlLabel.Size = new System.Drawing.Size(89, 37);
            this.storesApiUrlLabel.TabIndex = 0;
            this.storesApiUrlLabel.Text = "URL:";
            this.storesApiUrlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storesApiClientSecretLabel
            // 
            this.storesApiClientSecretLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientSecretLabel.AutoSize = true;
            this.storesApiClientSecretLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.storesApiClientSecretLabel.Location = new System.Drawing.Point(6, 182);
            this.storesApiClientSecretLabel.Name = "storesApiClientSecretLabel";
            this.storesApiClientSecretLabel.Size = new System.Drawing.Size(158, 37);
            this.storesApiClientSecretLabel.TabIndex = 3;
            this.storesApiClientSecretLabel.Text = "C. Secret:";
            // 
            // storesApiClientIdTextBox
            // 
            this.storesApiClientIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientIdTextBox.Location = new System.Drawing.Point(204, 131);
            this.storesApiClientIdTextBox.Name = "storesApiClientIdTextBox";
            this.storesApiClientIdTextBox.Size = new System.Drawing.Size(445, 26);
            this.storesApiClientIdTextBox.TabIndex = 22;
            // 
            // storesApiAuthUrlLabel
            // 
            this.storesApiAuthUrlLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiAuthUrlLabel.AutoSize = true;
            this.storesApiAuthUrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.storesApiAuthUrlLabel.Location = new System.Drawing.Point(6, 68);
            this.storesApiAuthUrlLabel.Name = "storesApiAuthUrlLabel";
            this.storesApiAuthUrlLabel.Size = new System.Drawing.Size(165, 37);
            this.storesApiAuthUrlLabel.TabIndex = 1;
            this.storesApiAuthUrlLabel.Text = "Auth URL:";
            // 
            // storesApiClientIdLabel
            // 
            this.storesApiClientIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientIdLabel.AutoSize = true;
            this.storesApiClientIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.storesApiClientIdLabel.Location = new System.Drawing.Point(6, 125);
            this.storesApiClientIdLabel.Name = "storesApiClientIdLabel";
            this.storesApiClientIdLabel.Size = new System.Drawing.Size(147, 37);
            this.storesApiClientIdLabel.TabIndex = 2;
            this.storesApiClientIdLabel.Text = "Client ID:";
            // 
            // storesApiUrlTextBox
            // 
            this.storesApiUrlTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiUrlTextBox.Location = new System.Drawing.Point(204, 17);
            this.storesApiUrlTextBox.Name = "storesApiUrlTextBox";
            this.storesApiUrlTextBox.Size = new System.Drawing.Size(445, 26);
            this.storesApiUrlTextBox.TabIndex = 18;
            // 
            // storesApiClientSecretTextBox
            // 
            this.storesApiClientSecretTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storesApiClientSecretTextBox.Location = new System.Drawing.Point(204, 188);
            this.storesApiClientSecretTextBox.Name = "storesApiClientSecretTextBox";
            this.storesApiClientSecretTextBox.Size = new System.Drawing.Size(445, 26);
            this.storesApiClientSecretTextBox.TabIndex = 24;
            // 
            // saveStoresApiSettingsButton
            // 
            this.saveStoresApiSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveStoresApiSettingsButton.Location = new System.Drawing.Point(16, 346);
            this.saveStoresApiSettingsButton.Name = "saveStoresApiSettingsButton";
            this.saveStoresApiSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.saveStoresApiSettingsButton.TabIndex = 18;
            this.saveStoresApiSettingsButton.Text = "Guardar";
            this.saveStoresApiSettingsButton.UseVisualStyleBackColor = true;
            this.saveStoresApiSettingsButton.Click += new System.EventHandler(this.saveStoresApiSettingsButton_Click);
            // 
            // cleanStoresApiSettingsButton
            // 
            this.cleanStoresApiSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanStoresApiSettingsButton.Location = new System.Drawing.Point(172, 346);
            this.cleanStoresApiSettingsButton.Name = "cleanStoresApiSettingsButton";
            this.cleanStoresApiSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.cleanStoresApiSettingsButton.TabIndex = 23;
            this.cleanStoresApiSettingsButton.Text = "Limpiar";
            this.cleanStoresApiSettingsButton.UseVisualStyleBackColor = true;
            this.cleanStoresApiSettingsButton.Click += new System.EventHandler(this.cleanStoresApiSettingsButton_Click);
            // 
            // restoreDefaultStoresApiSettingsButton
            // 
            this.restoreDefaultStoresApiSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.restoreDefaultStoresApiSettingsButton.Location = new System.Drawing.Point(328, 346);
            this.restoreDefaultStoresApiSettingsButton.Name = "restoreDefaultStoresApiSettingsButton";
            this.restoreDefaultStoresApiSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.restoreDefaultStoresApiSettingsButton.TabIndex = 24;
            this.restoreDefaultStoresApiSettingsButton.Text = "Restaurar";
            this.restoreDefaultStoresApiSettingsButton.UseVisualStyleBackColor = true;
            this.restoreDefaultStoresApiSettingsButton.Click += new System.EventHandler(this.restoreDefaultStoresApiSettingsButton_Click);
            // 
            // cancelStoresApiSettingsButton
            // 
            this.cancelStoresApiSettingsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelStoresApiSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelStoresApiSettingsButton.Location = new System.Drawing.Point(538, 346);
            this.cancelStoresApiSettingsButton.Name = "cancelStoresApiSettingsButton";
            this.cancelStoresApiSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.cancelStoresApiSettingsButton.TabIndex = 25;
            this.cancelStoresApiSettingsButton.Text = "Cancelar";
            this.cancelStoresApiSettingsButton.UseVisualStyleBackColor = true;
            this.cancelStoresApiSettingsButton.Click += new System.EventHandler(this.cancelStoresApiSettingsButton_Click);
            // 
            // StoresApiSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelStoresApiSettingsButton;
            this.ClientSize = new System.Drawing.Size(705, 402);
            this.Controls.Add(this.cancelStoresApiSettingsButton);
            this.Controls.Add(this.restoreDefaultStoresApiSettingsButton);
            this.Controls.Add(this.cleanStoresApiSettingsButton);
            this.Controls.Add(this.saveStoresApiSettingsButton);
            this.Controls.Add(this.editStoresApiSettingsPanel);
            this.Controls.Add(this.editStoresApiSettingsLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StoresApiSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Stores API";
            this.editStoresApiSettingsPanel.ResumeLayout(false);
            this.storesApiTableLayoutPanel.ResumeLayout(false);
            this.storesApiTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label editStoresApiSettingsLabel;
        private System.Windows.Forms.Panel editStoresApiSettingsPanel;
        private System.Windows.Forms.TextBox storesApiClientSecretTextBox;
        private System.Windows.Forms.TextBox storesApiClientIdTextBox;
        private System.Windows.Forms.TextBox storesApiUrlTextBox;
        private System.Windows.Forms.Button saveStoresApiSettingsButton;
        private System.Windows.Forms.TextBox storesApiAuthUrlTextBox;
        private System.Windows.Forms.Button cleanStoresApiSettingsButton;
        private System.Windows.Forms.Button restoreDefaultStoresApiSettingsButton;
        private System.Windows.Forms.Button cancelStoresApiSettingsButton;
        private System.Windows.Forms.TableLayoutPanel storesApiTableLayoutPanel;
        private System.Windows.Forms.Label storesApiUrlLabel;
        private System.Windows.Forms.Label storesApiClientSecretLabel;
        private System.Windows.Forms.Label storesApiAuthUrlLabel;
        private System.Windows.Forms.Label storesApiClientIdLabel;
    }
}