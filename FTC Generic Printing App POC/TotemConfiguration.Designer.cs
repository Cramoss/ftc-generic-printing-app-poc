namespace FTC_Generic_Printing_App_POC
{
    partial class TotemConfiguration
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
            this.saveTotemConfigurationButton = new System.Windows.Forms.Button();
            this.editTotemConfigurationPanel = new System.Windows.Forms.Panel();
            this.totemTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.currentTotemIdLabel = new System.Windows.Forms.Label();
            this.currentStoreNameLabel = new System.Windows.Forms.Label();
            this.currentCountryLabel = new System.Windows.Forms.Label();
            this.currentBusinessLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelTotemConfgurationButton = new System.Windows.Forms.Button();
            this.cleanTotemConfigurationButton = new System.Windows.Forms.Button();
            this.editTotemConfigurationPanel.SuspendLayout();
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
            // saveTotemConfigurationButton
            // 
            this.saveTotemConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveTotemConfigurationButton.Location = new System.Drawing.Point(11, 225);
            this.saveTotemConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveTotemConfigurationButton.Name = "saveTotemConfigurationButton";
            this.saveTotemConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.saveTotemConfigurationButton.TabIndex = 17;
            this.saveTotemConfigurationButton.Text = "Guardar";
            this.saveTotemConfigurationButton.UseVisualStyleBackColor = true;
            this.saveTotemConfigurationButton.Click += new System.EventHandler(this.saveTotemConfigurationButton_Click);
            // 
            // editTotemConfigurationPanel
            // 
            this.editTotemConfigurationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editTotemConfigurationPanel.Controls.Add(this.totemTableLayoutPanel);
            this.editTotemConfigurationPanel.Location = new System.Drawing.Point(10, 60);
            this.editTotemConfigurationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.editTotemConfigurationPanel.Name = "editTotemConfigurationPanel";
            this.editTotemConfigurationPanel.Size = new System.Drawing.Size(449, 161);
            this.editTotemConfigurationPanel.TabIndex = 11;
            // 
            // totemTableLayoutPanel
            // 
            this.totemTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.totemTableLayoutPanel.ColumnCount = 2;
            this.totemTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.19266F));
            this.totemTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.80734F));
            this.totemTableLayoutPanel.Controls.Add(this.storeComboBox, 1, 3);
            this.totemTableLayoutPanel.Controls.Add(this.currentTotemIdLabel, 0, 0);
            this.totemTableLayoutPanel.Controls.Add(this.currentStoreNameLabel, 0, 3);
            this.totemTableLayoutPanel.Controls.Add(this.businessComboBox, 1, 2);
            this.totemTableLayoutPanel.Controls.Add(this.currentCountryLabel, 0, 1);
            this.totemTableLayoutPanel.Controls.Add(this.currentBusinessLabel, 0, 2);
            this.totemTableLayoutPanel.Controls.Add(this.countryComboBox, 1, 1);
            this.totemTableLayoutPanel.Controls.Add(this.idTotemTextBox, 1, 0);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 26);
            this.label1.TabIndex = 12;
            this.label1.Text = "Configurar Totem";
            // 
            // cancelTotemConfgurationButton
            // 
            this.cancelTotemConfgurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelTotemConfgurationButton.Location = new System.Drawing.Point(359, 225);
            this.cancelTotemConfgurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelTotemConfgurationButton.Name = "cancelTotemConfgurationButton";
            this.cancelTotemConfgurationButton.Size = new System.Drawing.Size(100, 25);
            this.cancelTotemConfgurationButton.TabIndex = 18;
            this.cancelTotemConfgurationButton.Text = "Cancelar";
            this.cancelTotemConfgurationButton.UseVisualStyleBackColor = true;
            this.cancelTotemConfgurationButton.Click += new System.EventHandler(this.cancelTotemConfgurationButton_Click);
            // 
            // cleanTotemConfigurationButton
            // 
            this.cleanTotemConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanTotemConfigurationButton.Location = new System.Drawing.Point(115, 225);
            this.cleanTotemConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanTotemConfigurationButton.Name = "cleanTotemConfigurationButton";
            this.cleanTotemConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.cleanTotemConfigurationButton.TabIndex = 19;
            this.cleanTotemConfigurationButton.Text = "Limpiar";
            this.cleanTotemConfigurationButton.UseVisualStyleBackColor = true;
            this.cleanTotemConfigurationButton.Click += new System.EventHandler(this.cleanTotemConfigurationButton_Click);
            // 
            // TotemConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 261);
            this.Controls.Add(this.cleanTotemConfigurationButton);
            this.Controls.Add(this.cancelTotemConfgurationButton);
            this.Controls.Add(this.saveTotemConfigurationButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editTotemConfigurationPanel);
            this.Name = "TotemConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Tótem";
            this.editTotemConfigurationPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button saveTotemConfigurationButton;
        private System.Windows.Forms.Panel editTotemConfigurationPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelTotemConfgurationButton;
        private System.Windows.Forms.Button cleanTotemConfigurationButton;
        private System.Windows.Forms.TableLayoutPanel totemTableLayoutPanel;
        private System.Windows.Forms.Label currentTotemIdLabel;
        private System.Windows.Forms.Label currentStoreNameLabel;
        private System.Windows.Forms.Label currentCountryLabel;
        private System.Windows.Forms.Label currentBusinessLabel;
    }
}