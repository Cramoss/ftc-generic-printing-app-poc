namespace FTC_Generic_Printing_App_POC.Forms.Settings
{
    partial class FirebaseSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirebaseSettingsForm));
            this.firebaseTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.firebaseKeyTextBox = new System.Windows.Forms.TextBox();
            this.firebaseProjectIdTextBox = new System.Windows.Forms.TextBox();
            this.firebaseDatabaseTextBox = new System.Windows.Forms.TextBox();
            this.firebaseUrlLabel = new System.Windows.Forms.Label();
            this.firebaseProjectIdLabel = new System.Windows.Forms.Label();
            this.firebaseApiKeyLabel = new System.Windows.Forms.Label();
            this.editFirebaseSettingsPanel = new System.Windows.Forms.Panel();
            this.editFirebaseSettingsLabel = new System.Windows.Forms.Label();
            this.cancelFirebaseSettingsButton = new System.Windows.Forms.Button();
            this.restoreDefaultFirebaseSettingsButton = new System.Windows.Forms.Button();
            this.cleanFirebaseSettingsButton = new System.Windows.Forms.Button();
            this.saveFirebaseSettingsButton = new System.Windows.Forms.Button();
            this.firebaseTableLayoutPanel.SuspendLayout();
            this.editFirebaseSettingsPanel.SuspendLayout();
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
            // firebaseKeyTextBox
            // 
            this.firebaseKeyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseKeyTextBox.Location = new System.Drawing.Point(140, 84);
            this.firebaseKeyTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firebaseKeyTextBox.Name = "firebaseKeyTextBox";
            this.firebaseKeyTextBox.Size = new System.Drawing.Size(292, 20);
            this.firebaseKeyTextBox.TabIndex = 13;
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
            // firebaseDatabaseTextBox
            // 
            this.firebaseDatabaseTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.firebaseDatabaseTextBox.Location = new System.Drawing.Point(140, 10);
            this.firebaseDatabaseTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firebaseDatabaseTextBox.Name = "firebaseDatabaseTextBox";
            this.firebaseDatabaseTextBox.Size = new System.Drawing.Size(292, 20);
            this.firebaseDatabaseTextBox.TabIndex = 11;
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
            // editFirebaseSettingsPanel
            // 
            this.editFirebaseSettingsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editFirebaseSettingsPanel.Controls.Add(this.firebaseTableLayoutPanel);
            this.editFirebaseSettingsPanel.Location = new System.Drawing.Point(10, 60);
            this.editFirebaseSettingsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.editFirebaseSettingsPanel.Name = "editFirebaseSettingsPanel";
            this.editFirebaseSettingsPanel.Size = new System.Drawing.Size(449, 161);
            this.editFirebaseSettingsPanel.TabIndex = 23;
            // 
            // editFirebaseSettingsLabel
            // 
            this.editFirebaseSettingsLabel.AutoSize = true;
            this.editFirebaseSettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.editFirebaseSettingsLabel.Location = new System.Drawing.Point(6, 9);
            this.editFirebaseSettingsLabel.Name = "editFirebaseSettingsLabel";
            this.editFirebaseSettingsLabel.Size = new System.Drawing.Size(203, 26);
            this.editFirebaseSettingsLabel.TabIndex = 13;
            this.editFirebaseSettingsLabel.Text = "Configurar Firebase";
            // 
            // cancelFirebaseSettingsButton
            // 
            this.cancelFirebaseSettingsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelFirebaseSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cancelFirebaseSettingsButton.Location = new System.Drawing.Point(357, 225);
            this.cancelFirebaseSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelFirebaseSettingsButton.Name = "cancelFirebaseSettingsButton";
            this.cancelFirebaseSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.cancelFirebaseSettingsButton.TabIndex = 29;
            this.cancelFirebaseSettingsButton.Text = "Cancelar";
            this.cancelFirebaseSettingsButton.UseVisualStyleBackColor = true;
            this.cancelFirebaseSettingsButton.Click += new System.EventHandler(this.cancelFirebaseSettingsButton_Click);
            // 
            // restoreDefaultFirebaseSettingsButton
            // 
            this.restoreDefaultFirebaseSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.restoreDefaultFirebaseSettingsButton.Location = new System.Drawing.Point(217, 225);
            this.restoreDefaultFirebaseSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.restoreDefaultFirebaseSettingsButton.Name = "restoreDefaultFirebaseSettingsButton";
            this.restoreDefaultFirebaseSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.restoreDefaultFirebaseSettingsButton.TabIndex = 28;
            this.restoreDefaultFirebaseSettingsButton.Text = "Restaurar";
            this.restoreDefaultFirebaseSettingsButton.UseVisualStyleBackColor = true;
            this.restoreDefaultFirebaseSettingsButton.Click += new System.EventHandler(this.restoreDefaultFirebaseSettingsButton_Click);
            // 
            // cleanFirebaseSettingsButton
            // 
            this.cleanFirebaseSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cleanFirebaseSettingsButton.Location = new System.Drawing.Point(113, 225);
            this.cleanFirebaseSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.cleanFirebaseSettingsButton.Name = "cleanFirebaseSettingsButton";
            this.cleanFirebaseSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.cleanFirebaseSettingsButton.TabIndex = 27;
            this.cleanFirebaseSettingsButton.Text = "Limpiar";
            this.cleanFirebaseSettingsButton.UseVisualStyleBackColor = true;
            this.cleanFirebaseSettingsButton.Click += new System.EventHandler(this.cleanFirebaseSettingsButton_Click);
            // 
            // saveFirebaseSettingsButton
            // 
            this.saveFirebaseSettingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.saveFirebaseSettingsButton.Location = new System.Drawing.Point(9, 225);
            this.saveFirebaseSettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveFirebaseSettingsButton.Name = "saveFirebaseSettingsButton";
            this.saveFirebaseSettingsButton.Size = new System.Drawing.Size(100, 25);
            this.saveFirebaseSettingsButton.TabIndex = 26;
            this.saveFirebaseSettingsButton.Text = "Guardar";
            this.saveFirebaseSettingsButton.UseVisualStyleBackColor = true;
            this.saveFirebaseSettingsButton.Click += new System.EventHandler(this.saveFirebaseSettingsButton_Click);
            // 
            // FirebaseSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelFirebaseSettingsButton;
            this.ClientSize = new System.Drawing.Size(470, 261);
            this.Controls.Add(this.cancelFirebaseSettingsButton);
            this.Controls.Add(this.restoreDefaultFirebaseSettingsButton);
            this.Controls.Add(this.cleanFirebaseSettingsButton);
            this.Controls.Add(this.saveFirebaseSettingsButton);
            this.Controls.Add(this.editFirebaseSettingsLabel);
            this.Controls.Add(this.editFirebaseSettingsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FirebaseSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Firebase- FTC Generic Printing App POC";
            this.firebaseTableLayoutPanel.ResumeLayout(false);
            this.firebaseTableLayoutPanel.PerformLayout();
            this.editFirebaseSettingsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel firebaseTableLayoutPanel;
        private System.Windows.Forms.Label firebaseUrlLabel;
        private System.Windows.Forms.Label firebaseProjectIdLabel;
        private System.Windows.Forms.Label firebaseApiKeyLabel;
        private System.Windows.Forms.Panel editFirebaseSettingsPanel;
        private System.Windows.Forms.Label editFirebaseSettingsLabel;
        private System.Windows.Forms.Button cancelFirebaseSettingsButton;
        private System.Windows.Forms.Button restoreDefaultFirebaseSettingsButton;
        private System.Windows.Forms.Button cleanFirebaseSettingsButton;
        private System.Windows.Forms.Button saveFirebaseSettingsButton;
        private System.Windows.Forms.TextBox firebaseKeyTextBox;
        private System.Windows.Forms.TextBox firebaseProjectIdTextBox;
        private System.Windows.Forms.TextBox firebaseDatabaseTextBox;
    }
}