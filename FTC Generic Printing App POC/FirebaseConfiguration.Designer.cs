namespace FTC_Generic_Printing_App_POC
{
    partial class FirebaseConfiguration
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
            this.firebaseTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.firebaseUrlLabel = new System.Windows.Forms.Label();
            this.firebaseProjectIdLabel = new System.Windows.Forms.Label();
            this.firebaseApiKeyLabel = new System.Windows.Forms.Label();
            this.editFirebaseConfigurationPanel = new System.Windows.Forms.Panel();
            this.configuratFirebaseLabel = new System.Windows.Forms.Label();
            this.cancelFirebaseConfigurationButton = new System.Windows.Forms.Button();
            this.restoreDefaultDefaultConfigurationButton = new System.Windows.Forms.Button();
            this.cleanFirebaseConfigurationButton = new System.Windows.Forms.Button();
            this.saveFirebaseConfigurationButton = new System.Windows.Forms.Button();
            this.firebaseDatabaseTextBox = new System.Windows.Forms.TextBox();
            this.firebaseProjectIdTextBox = new System.Windows.Forms.TextBox();
            this.firebaseKeyTextBox = new System.Windows.Forms.TextBox();
            this.firebaseTableLayoutPanel.SuspendLayout();
            this.editFirebaseConfigurationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // firebaseTableLayoutPanel
            // 
            this.firebaseTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.firebaseTableLayoutPanel.ColumnCount = 2;
            this.firebaseTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.73395F));
            this.firebaseTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.26605F));
            this.firebaseTableLayoutPanel.Controls.Add(this.firebaseKeyTextBox, 1, 2);
            this.firebaseTableLayoutPanel.Controls.Add(this.firebaseProjectIdTextBox, 1, 1);
            this.firebaseTableLayoutPanel.Controls.Add(this.firebaseDatabaseTextBox, 1, 0);
            this.firebaseTableLayoutPanel.Controls.Add(this.firebaseUrlLabel, 0, 0);
            this.firebaseTableLayoutPanel.Controls.Add(this.firebaseProjectIdLabel, 0, 1);
            this.firebaseTableLayoutPanel.Controls.Add(this.firebaseApiKeyLabel, 0, 2);
            this.firebaseTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.firebaseTableLayoutPanel.Name = "firebaseTableLayoutPanel";
            this.firebaseTableLayoutPanel.RowCount = 4;
            this.firebaseTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.firebaseTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.firebaseTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.firebaseTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.firebaseTableLayoutPanel.Size = new System.Drawing.Size(439, 151);
            this.firebaseTableLayoutPanel.TabIndex = 22;
            // 
            // firebaseUrlLabel
            // 
            this.firebaseUrlLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseUrlLabel.AutoSize = true;
            this.firebaseUrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firebaseUrlLabel.Location = new System.Drawing.Point(5, 10);
            this.firebaseUrlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firebaseUrlLabel.Name = "firebaseUrlLabel";
            this.firebaseUrlLabel.Size = new System.Drawing.Size(83, 20);
            this.firebaseUrlLabel.TabIndex = 0;
            this.firebaseUrlLabel.Text = "Database:";
            this.firebaseUrlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firebaseProjectIdLabel
            // 
            this.firebaseProjectIdLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseProjectIdLabel.AutoSize = true;
            this.firebaseProjectIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.firebaseProjectIdLabel.Location = new System.Drawing.Point(5, 47);
            this.firebaseProjectIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firebaseProjectIdLabel.Name = "firebaseProjectIdLabel";
            this.firebaseProjectIdLabel.Size = new System.Drawing.Size(83, 20);
            this.firebaseProjectIdLabel.TabIndex = 1;
            this.firebaseProjectIdLabel.Text = "Project ID:";
            // 
            // firebaseApiKeyLabel
            // 
            this.firebaseApiKeyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseApiKeyLabel.AutoSize = true;
            this.firebaseApiKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.firebaseApiKeyLabel.Location = new System.Drawing.Point(5, 84);
            this.firebaseApiKeyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firebaseApiKeyLabel.Name = "firebaseApiKeyLabel";
            this.firebaseApiKeyLabel.Size = new System.Drawing.Size(39, 20);
            this.firebaseApiKeyLabel.TabIndex = 2;
            this.firebaseApiKeyLabel.Text = "Key:";
            // 
            // editFirebaseConfigurationPanel
            // 
            this.editFirebaseConfigurationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editFirebaseConfigurationPanel.Controls.Add(this.firebaseTableLayoutPanel);
            this.editFirebaseConfigurationPanel.Location = new System.Drawing.Point(10, 60);
            this.editFirebaseConfigurationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.editFirebaseConfigurationPanel.Name = "editFirebaseConfigurationPanel";
            this.editFirebaseConfigurationPanel.Size = new System.Drawing.Size(449, 161);
            this.editFirebaseConfigurationPanel.TabIndex = 23;
            // 
            // configuratFirebaseLabel
            // 
            this.configuratFirebaseLabel.AutoSize = true;
            this.configuratFirebaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.configuratFirebaseLabel.Location = new System.Drawing.Point(6, 9);
            this.configuratFirebaseLabel.Name = "configuratFirebaseLabel";
            this.configuratFirebaseLabel.Size = new System.Drawing.Size(203, 26);
            this.configuratFirebaseLabel.TabIndex = 13;
            this.configuratFirebaseLabel.Text = "Configurar Firebase";
            // 
            // cancelFirebaseConfigurationButton
            // 
            this.cancelFirebaseConfigurationButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelFirebaseConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelFirebaseConfigurationButton.Location = new System.Drawing.Point(357, 225);
            this.cancelFirebaseConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelFirebaseConfigurationButton.Name = "cancelFirebaseConfigurationButton";
            this.cancelFirebaseConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.cancelFirebaseConfigurationButton.TabIndex = 29;
            this.cancelFirebaseConfigurationButton.Text = "Cancelar";
            this.cancelFirebaseConfigurationButton.UseVisualStyleBackColor = true;
            this.cancelFirebaseConfigurationButton.Click += new System.EventHandler(this.cancelFirebaseConfigurationButton_Click);
            // 
            // restoreDefaultDefaultConfigurationButton
            // 
            this.restoreDefaultDefaultConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.restoreDefaultDefaultConfigurationButton.Location = new System.Drawing.Point(217, 225);
            this.restoreDefaultDefaultConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.restoreDefaultDefaultConfigurationButton.Name = "restoreDefaultDefaultConfigurationButton";
            this.restoreDefaultDefaultConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.restoreDefaultDefaultConfigurationButton.TabIndex = 28;
            this.restoreDefaultDefaultConfigurationButton.Text = "Restaurar";
            this.restoreDefaultDefaultConfigurationButton.UseVisualStyleBackColor = true;
            this.restoreDefaultDefaultConfigurationButton.Click += new System.EventHandler(this.restoreDefaultDefaultConfigurationButton_Click);
            // 
            // cleanFirebaseConfigurationButton
            // 
            this.cleanFirebaseConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanFirebaseConfigurationButton.Location = new System.Drawing.Point(113, 225);
            this.cleanFirebaseConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanFirebaseConfigurationButton.Name = "cleanFirebaseConfigurationButton";
            this.cleanFirebaseConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.cleanFirebaseConfigurationButton.TabIndex = 27;
            this.cleanFirebaseConfigurationButton.Text = "Limpiar";
            this.cleanFirebaseConfigurationButton.UseVisualStyleBackColor = true;
            this.cleanFirebaseConfigurationButton.Click += new System.EventHandler(this.cleanFirebaseConfigurationButton_Click);
            // 
            // saveFirebaseConfigurationButton
            // 
            this.saveFirebaseConfigurationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveFirebaseConfigurationButton.Location = new System.Drawing.Point(9, 225);
            this.saveFirebaseConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveFirebaseConfigurationButton.Name = "saveFirebaseConfigurationButton";
            this.saveFirebaseConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.saveFirebaseConfigurationButton.TabIndex = 26;
            this.saveFirebaseConfigurationButton.Text = "Guardar";
            this.saveFirebaseConfigurationButton.UseVisualStyleBackColor = true;
            this.saveFirebaseConfigurationButton.Click += new System.EventHandler(this.saveFirebaseConfigurationButton_Click);
            // 
            // firebaseDatabaseTextBox
            // 
            this.firebaseDatabaseTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseDatabaseTextBox.Location = new System.Drawing.Point(140, 10);
            this.firebaseDatabaseTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firebaseDatabaseTextBox.Name = "firebaseDatabaseTextBox";
            this.firebaseDatabaseTextBox.Size = new System.Drawing.Size(292, 20);
            this.firebaseDatabaseTextBox.TabIndex = 11;
            // 
            // firebaseProjectIdTextBox
            // 
            this.firebaseProjectIdTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseProjectIdTextBox.Location = new System.Drawing.Point(140, 47);
            this.firebaseProjectIdTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firebaseProjectIdTextBox.Name = "firebaseProjectIdTextBox";
            this.firebaseProjectIdTextBox.Size = new System.Drawing.Size(292, 20);
            this.firebaseProjectIdTextBox.TabIndex = 12;
            // 
            // firebaseKeyTextBox
            // 
            this.firebaseKeyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseKeyTextBox.Location = new System.Drawing.Point(140, 84);
            this.firebaseKeyTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firebaseKeyTextBox.Name = "firebaseKeyTextBox";
            this.firebaseKeyTextBox.Size = new System.Drawing.Size(292, 20);
            this.firebaseKeyTextBox.TabIndex = 13;
            // 
            // FirebaseConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelFirebaseConfigurationButton;
            this.ClientSize = new System.Drawing.Size(470, 261);
            this.Controls.Add(this.cancelFirebaseConfigurationButton);
            this.Controls.Add(this.restoreDefaultDefaultConfigurationButton);
            this.Controls.Add(this.cleanFirebaseConfigurationButton);
            this.Controls.Add(this.saveFirebaseConfigurationButton);
            this.Controls.Add(this.configuratFirebaseLabel);
            this.Controls.Add(this.editFirebaseConfigurationPanel);
            this.Name = "FirebaseConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Firebase";
            this.firebaseTableLayoutPanel.ResumeLayout(false);
            this.firebaseTableLayoutPanel.PerformLayout();
            this.editFirebaseConfigurationPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel firebaseTableLayoutPanel;
        private System.Windows.Forms.Label firebaseUrlLabel;
        private System.Windows.Forms.Label firebaseProjectIdLabel;
        private System.Windows.Forms.Label firebaseApiKeyLabel;
        private System.Windows.Forms.Panel editFirebaseConfigurationPanel;
        private System.Windows.Forms.Label configuratFirebaseLabel;
        private System.Windows.Forms.Button cancelFirebaseConfigurationButton;
        private System.Windows.Forms.Button restoreDefaultDefaultConfigurationButton;
        private System.Windows.Forms.Button cleanFirebaseConfigurationButton;
        private System.Windows.Forms.Button saveFirebaseConfigurationButton;
        private System.Windows.Forms.TextBox firebaseKeyTextBox;
        private System.Windows.Forms.TextBox firebaseProjectIdTextBox;
        private System.Windows.Forms.TextBox firebaseDatabaseTextBox;
    }
}