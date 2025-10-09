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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TotemSettingsForm));
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
            this.idTotemTextBox.Location = new System.Drawing.Point(142, 10);
            this.idTotemTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.idTotemTextBox.Name = "idTotemTextBox";
            this.idTotemTextBox.Size = new System.Drawing.Size(292, 20);
            this.idTotemTextBox.TabIndex = 10;
            // 
            // countryComboBox
            // 
            this.countryComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.countryComboBox.FormattingEnabled = true;
            this.countryComboBox.Location = new System.Drawing.Point(142, 46);
            this.countryComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.countryComboBox.Name = "countryComboBox";
            this.countryComboBox.Size = new System.Drawing.Size(292, 21);
            this.countryComboBox.TabIndex = 12;
            // 
            // businessComboBox
            // 
            this.businessComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.businessComboBox.FormattingEnabled = true;
            this.businessComboBox.Location = new System.Drawing.Point(142, 83);
            this.businessComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.businessComboBox.Name = "businessComboBox";
            this.businessComboBox.Size = new System.Drawing.Size(292, 21);
            this.businessComboBox.TabIndex = 14;
            // 
            // storeComboBox
            // 
            this.storeComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.storeComboBox.FormattingEnabled = true;
            this.storeComboBox.Location = new System.Drawing.Point(142, 120);
            this.storeComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.storeComboBox.Name = "storeComboBox";
            this.storeComboBox.Size = new System.Drawing.Size(292, 21);
            this.storeComboBox.TabIndex = 16;
            // 
            // saveTotemSettingsButton
            // 
            this.saveTotemSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveTotemSettingsButton.Location = new System.Drawing.Point(11, 225);
            this.saveTotemSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveTotemSettingsButton.Name = "saveTotemSettingsButton";
            this.saveTotemSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.saveTotemSettingsButton.TabIndex = 17;
            this.saveTotemSettingsButton.Text = "Guardar";
            this.saveTotemSettingsButton.UseVisualStyleBackColor = true;
            this.saveTotemSettingsButton.Click += new System.EventHandler(this.saveTotemSettingsButton_Click);
            // 
            // editTotemSettingsPanel
            // 
            this.editTotemSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editTotemSettingsPanel.Controls.Add(this.totemTableLayoutPanel);
            this.editTotemSettingsPanel.Location = new System.Drawing.Point(10, 60);
            this.editTotemSettingsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.editTotemSettingsPanel.Name = "editTotemSettingsPanel";
            this.editTotemSettingsPanel.Size = new System.Drawing.Size(449, 161);
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
            this.totemTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.totemTableLayoutPanel.Name = "totemTableLayoutPanel";
            this.totemTableLayoutPanel.RowCount = 4;
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.totemTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.totemTableLayoutPanel.Size = new System.Drawing.Size(439, 151);
            this.totemTableLayoutPanel.TabIndex = 22;
            // 
            // currentStoreNameLabel
            // 
            this.currentStoreNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentStoreNameLabel.AutoSize = true;
            this.currentStoreNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentStoreNameLabel.Location = new System.Drawing.Point(5, 121);
            this.currentStoreNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentStoreNameLabel.Name = "currentStoreNameLabel";
            this.currentStoreNameLabel.Size = new System.Drawing.Size(61, 20);
            this.currentStoreNameLabel.TabIndex = 3;
            this.currentStoreNameLabel.Text = "Tienda:";
            // 
            // currentCountryLabel
            // 
            this.currentCountryLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentCountryLabel.AutoSize = true;
            this.currentCountryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentCountryLabel.Location = new System.Drawing.Point(5, 47);
            this.currentCountryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentCountryLabel.Name = "currentCountryLabel";
            this.currentCountryLabel.Size = new System.Drawing.Size(43, 20);
            this.currentCountryLabel.TabIndex = 1;
            this.currentCountryLabel.Text = "País:";
            // 
            // currentBusinessLabel
            // 
            this.currentBusinessLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentBusinessLabel.AutoSize = true;
            this.currentBusinessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentBusinessLabel.Location = new System.Drawing.Point(5, 84);
            this.currentBusinessLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentBusinessLabel.Name = "currentBusinessLabel";
            this.currentBusinessLabel.Size = new System.Drawing.Size(71, 20);
            this.currentBusinessLabel.TabIndex = 2;
            this.currentBusinessLabel.Text = "Negocio:";
            // 
            // currentTotemIdLabel
            // 
            this.currentTotemIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.currentTotemIdLabel.AutoSize = true;
            this.currentTotemIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTotemIdLabel.Location = new System.Drawing.Point(5, 10);
            this.currentTotemIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentTotemIdLabel.Name = "currentTotemIdLabel";
            this.currentTotemIdLabel.Size = new System.Drawing.Size(79, 20);
            this.currentTotemIdLabel.TabIndex = 0;
            this.currentTotemIdLabel.Text = "ID Tótem:";
            this.currentTotemIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // editTotemSettingsLabel
            // 
            this.editTotemSettingsLabel.AutoSize = true;
            this.editTotemSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.editTotemSettingsLabel.Location = new System.Drawing.Point(6, 9);
            this.editTotemSettingsLabel.Name = "editTotemSettingsLabel";
            this.editTotemSettingsLabel.Size = new System.Drawing.Size(180, 26);
            this.editTotemSettingsLabel.TabIndex = 12;
            this.editTotemSettingsLabel.Text = "Configurar Totem";
            // 
            // cancelTotemSettingsButton
            // 
            this.cancelTotemSettingsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelTotemSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelTotemSettingsButton.Location = new System.Drawing.Point(359, 225);
            this.cancelTotemSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelTotemSettingsButton.Name = "cancelTotemSettingsButton";
            this.cancelTotemSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.cancelTotemSettingsButton.TabIndex = 18;
            this.cancelTotemSettingsButton.Text = "Cancelar";
            this.cancelTotemSettingsButton.UseVisualStyleBackColor = true;
            this.cancelTotemSettingsButton.Click += new System.EventHandler(this.cancelTotemSettingsButton_Click);
            // 
            // cleanTotemSettingsButton
            // 
            this.cleanTotemSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanTotemSettingsButton.Location = new System.Drawing.Point(115, 225);
            this.cleanTotemSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanTotemSettingsButton.Name = "cleanTotemSettingsButton";
            this.cleanTotemSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.cleanTotemSettingsButton.TabIndex = 19;
            this.cleanTotemSettingsButton.Text = "Limpiar";
            this.cleanTotemSettingsButton.UseVisualStyleBackColor = true;
            this.cleanTotemSettingsButton.Click += new System.EventHandler(this.cleanTotemSettingsButton_Click);
            // 
            // TotemSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelTotemSettingsButton;
            this.ClientSize = new System.Drawing.Size(470, 261);
            this.Controls.Add(this.cleanTotemSettingsButton);
            this.Controls.Add(this.cancelTotemSettingsButton);
            this.Controls.Add(this.saveTotemSettingsButton);
            this.Controls.Add(this.editTotemSettingsLabel);
            this.Controls.Add(this.editTotemSettingsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TotemSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Tótem - FTC Generic Printing App POC";
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