namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    partial class TotemSettingsForm
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
            this.idTotemTextBox = new System.Windows.Forms.TextBox();
            this.countryComboBox = new System.Windows.Forms.ComboBox();
            this.businessComboBox = new System.Windows.Forms.ComboBox();
            this.storeComboBox = new System.Windows.Forms.ComboBox();
            this.saveTotemSettingsButton = new System.Windows.Forms.Button();
            this.editTotemSettingsPanel = new System.Windows.Forms.Panel();
            this.totemTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.currentStoreNameLabel = new System.Windows.Forms.Label();
            this.currentCountryLabel = new System.Windows.Forms.Label();
            this.currentBusinessLabel = new System.Windows.Forms.Label();
            this.currentTotemIdLabel = new System.Windows.Forms.Label();
            this.editTotemSettingsLabel = new System.Windows.Forms.Label();
            this.cancelTotemSettingsButton = new System.Windows.Forms.Button();
            this.cleanTotemSettingsButton = new System.Windows.Forms.Button();
            this.editTotemSettingsPanel.SuspendLayout();
            this.totemTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // idTotemTextBox
            // 
            this.idTotemTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.idTotemTextBox.Location = new System.Drawing.Point(211, 17);
            this.idTotemTextBox.Name = "idTotemTextBox";
            this.idTotemTextBox.Size = new System.Drawing.Size(436, 26);
            this.idTotemTextBox.TabIndex = 10;
            // 
            // countryComboBox
            // 
            this.countryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(211, 73);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(436, 28);
            this.countryComboBox.TabIndex = 12;
            // 
            // businessComboBox
            // 
            this.businessComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.businessComboBox.FormattingEnabled = true;
            this.businessComboBox.Location = new System.Drawing.Point(211, 130);
            this.businessComboBox.Name = "businessComboBox";
            this.businessComboBox.Size = new System.Drawing.Size(436, 28);
            this.businessComboBox.TabIndex = 14;
            // 
            // storeComboBox
            // 
            this.storeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storeComboBox.FormattingEnabled = true;
            this.storeComboBox.Location = new System.Drawing.Point(211, 187);
            this.storeComboBox.Name = "storeComboBox";
            this.storeComboBox.Size = new System.Drawing.Size(436, 28);
            this.storeComboBox.TabIndex = 16;
            // 
            // saveTotemSettingsButton
            // 
            this.saveTotemSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveTotemSettingsButton.Location = new System.Drawing.Point(16, 346);
            this.saveTotemSettingsButton.Name = "saveTotemSettingsButton";
            this.saveTotemSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.saveTotemSettingsButton.TabIndex = 17;
            this.saveTotemSettingsButton.Text = "Guardar";
            this.saveTotemSettingsButton.UseVisualStyleBackColor = true;
            this.saveTotemSettingsButton.Click += new System.EventHandler(this.saveTotemSettingsButton_Click);
            // 
            // editTotemSettingsPanel
            // 
            this.editTotemSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editTotemSettingsPanel.Controls.Add(this.totemTableLayoutPanel);
            this.editTotemSettingsPanel.Location = new System.Drawing.Point(15, 92);
            this.editTotemSettingsPanel.Name = "editTotemSettingsPanel";
            this.editTotemSettingsPanel.Size = new System.Drawing.Size(672, 246);
            this.editTotemSettingsPanel.TabIndex = 11;
            // 
            // totemTableLayoutPanel
            // 
            this.totemTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.totemTableLayoutPanel.ColumnCount = 2;
            this.totemTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.19266F));
            this.totemTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.80734F));
            this.totemTableLayoutPanel.Controls.Add(this.storeComboBox, 1, 3);
            this.totemTableLayoutPanel.Controls.Add(this.currentStoreNameLabel, 0, 3);
            this.totemTableLayoutPanel.Controls.Add(this.businessComboBox, 1, 2);
            this.totemTableLayoutPanel.Controls.Add(this.currentCountryLabel, 0, 1);
            this.totemTableLayoutPanel.Controls.Add(this.currentBusinessLabel, 0, 2);
            this.totemTableLayoutPanel.Controls.Add(this.countryComboBox, 1, 1);
            this.totemTableLayoutPanel.Controls.Add(this.idTotemTextBox, 1, 0);
            this.totemTableLayoutPanel.Controls.Add(this.currentTotemIdLabel, 0, 0);
            this.totemTableLayoutPanel.Location = new System.Drawing.Point(4, 5);
            this.totemTableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.totemTableLayoutPanel.Name = "totemTableLayoutPanel";
            this.totemTableLayoutPanel.RowCount = 4;
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.totemTableLayoutPanel.Size = new System.Drawing.Size(658, 232);
            this.totemTableLayoutPanel.TabIndex = 22;
            // 
            // currentStoreNameLabel
            // 
            this.currentStoreNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentStoreNameLabel.AutoSize = true;
            this.currentStoreNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentStoreNameLabel.Location = new System.Drawing.Point(6, 183);
            this.currentStoreNameLabel.Name = "currentStoreNameLabel";
            this.currentStoreNameLabel.Size = new System.Drawing.Size(124, 37);
            this.currentStoreNameLabel.TabIndex = 3;
            this.currentStoreNameLabel.Text = "Tienda:";
            // 
            // currentCountryLabel
            // 
            this.currentCountryLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentCountryLabel.AutoSize = true;
            this.currentCountryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentCountryLabel.Location = new System.Drawing.Point(6, 68);
            this.currentCountryLabel.Name = "currentCountryLabel";
            this.currentCountryLabel.Size = new System.Drawing.Size(88, 37);
            this.currentCountryLabel.TabIndex = 1;
            this.currentCountryLabel.Text = "País:";
            // 
            // currentBusinessLabel
            // 
            this.currentBusinessLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentBusinessLabel.AutoSize = true;
            this.currentBusinessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentBusinessLabel.Location = new System.Drawing.Point(6, 125);
            this.currentBusinessLabel.Name = "currentBusinessLabel";
            this.currentBusinessLabel.Size = new System.Drawing.Size(144, 37);
            this.currentBusinessLabel.TabIndex = 2;
            this.currentBusinessLabel.Text = "Negocio:";
            // 
            // currentTotemIdLabel
            // 
            this.currentTotemIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentTotemIdLabel.AutoSize = true;
            this.currentTotemIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTotemIdLabel.Location = new System.Drawing.Point(6, 11);
            this.currentTotemIdLabel.Name = "currentTotemIdLabel";
            this.currentTotemIdLabel.Size = new System.Drawing.Size(157, 37);
            this.currentTotemIdLabel.TabIndex = 0;
            this.currentTotemIdLabel.Text = "ID Tótem:";
            this.currentTotemIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editTotemSettingsLabel
            // 
            this.editTotemSettingsLabel.AutoSize = true;
            this.editTotemSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.editTotemSettingsLabel.Location = new System.Drawing.Point(9, 14);
            this.editTotemSettingsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.editTotemSettingsLabel.Name = "editTotemSettingsLabel";
            this.editTotemSettingsLabel.Size = new System.Drawing.Size(356, 51);
            this.editTotemSettingsLabel.TabIndex = 12;
            this.editTotemSettingsLabel.Text = "Configurar Totem";
            // 
            // cancelTotemSettingsButton
            // 
            this.cancelTotemSettingsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelTotemSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelTotemSettingsButton.Location = new System.Drawing.Point(538, 346);
            this.cancelTotemSettingsButton.Name = "cancelTotemSettingsButton";
            this.cancelTotemSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.cancelTotemSettingsButton.TabIndex = 18;
            this.cancelTotemSettingsButton.Text = "Cancelar";
            this.cancelTotemSettingsButton.UseVisualStyleBackColor = true;
            this.cancelTotemSettingsButton.Click += new System.EventHandler(this.cancelTotemSettingsButton_Click);
            // 
            // cleanTotemSettingsButton
            // 
            this.cleanTotemSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanTotemSettingsButton.Location = new System.Drawing.Point(172, 346);
            this.cleanTotemSettingsButton.Name = "cleanTotemSettingsButton";
            this.cleanTotemSettingsButton.Size = new System.Drawing.Size(150, 38);
            this.cleanTotemSettingsButton.TabIndex = 19;
            this.cleanTotemSettingsButton.Text = "Limpiar";
            this.cleanTotemSettingsButton.UseVisualStyleBackColor = true;
            this.cleanTotemSettingsButton.Click += new System.EventHandler(this.cleanTotemSettingsButton_Click);
            // 
            // TotemSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelTotemSettingsButton;
            this.ClientSize = new System.Drawing.Size(705, 402);
            this.Controls.Add(this.cleanTotemSettingsButton);
            this.Controls.Add(this.cancelTotemSettingsButton);
            this.Controls.Add(this.saveTotemSettingsButton);
            this.Controls.Add(this.editTotemSettingsLabel);
            this.Controls.Add(this.editTotemSettingsPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TotemSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Tótem";
            this.editTotemSettingsPanel.ResumeLayout(false);
            this.totemTableLayoutPanel.ResumeLayout(false);
            this.totemTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox idTotemTextBox;
        private System.Windows.Forms.ComboBox countryComboBox;
        private System.Windows.Forms.ComboBox businessComboBox;
        private System.Windows.Forms.ComboBox storeComboBox;
        private System.Windows.Forms.Button saveTotemSettingsButton;
        private System.Windows.Forms.Panel editTotemSettingsPanel;
        private System.Windows.Forms.Label editTotemSettingsLabel;
        private System.Windows.Forms.Button cancelTotemSettingsButton;
        private System.Windows.Forms.Button cleanTotemSettingsButton;
        private System.Windows.Forms.TableLayoutPanel totemTableLayoutPanel;
        private System.Windows.Forms.Label currentTotemIdLabel;
        private System.Windows.Forms.Label currentStoreNameLabel;
        private System.Windows.Forms.Label currentCountryLabel;
        private System.Windows.Forms.Label currentBusinessLabel;
    }
}