namespace FTC_Generic_Printing_App_POC
{
    partial class ConfigurationAdminPasswordPrompt
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
            this.enterAdminPasswordLabel = new System.Windows.Forms.Label();
            this.adminPasswordTextBox = new System.Windows.Forms.TextBox();
            this.cancelPasswordButton = new System.Windows.Forms.Button();
            this.acceptPasswordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enterAdminPasswordLabel
            // 
            this.enterAdminPasswordLabel.AutoSize = true;
            this.enterAdminPasswordLabel.Location = new System.Drawing.Point(20, 20);
            this.enterAdminPasswordLabel.Name = "enterAdminPasswordLabel";
            this.enterAdminPasswordLabel.Size = new System.Drawing.Size(192, 13);
            this.enterAdminPasswordLabel.TabIndex = 0;
            this.enterAdminPasswordLabel.Text = "Ingrese la contraseña de administrador:";
            // 
            // adminPasswordTextBox
            // 
            this.adminPasswordTextBox.Location = new System.Drawing.Point(20, 50);
            this.adminPasswordTextBox.Name = "adminPasswordTextBox";
            this.adminPasswordTextBox.Size = new System.Drawing.Size(350, 20);
            this.adminPasswordTextBox.TabIndex = 1;
            // 
            // cancelPasswordButton
            // 
            this.cancelPasswordButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelPasswordButton.Location = new System.Drawing.Point(270, 80);
            this.cancelPasswordButton.Name = "cancelPasswordButton";
            this.cancelPasswordButton.Size = new System.Drawing.Size(75, 23);
            this.cancelPasswordButton.TabIndex = 2;
            this.cancelPasswordButton.Text = "Cancelar";
            this.cancelPasswordButton.UseVisualStyleBackColor = true;
            this.cancelPasswordButton.Click += new System.EventHandler(this.cancelPasswordButton_Click);
            // 
            // acceptPasswordButton
            // 
            this.acceptPasswordButton.Location = new System.Drawing.Point(190, 80);
            this.acceptPasswordButton.Name = "acceptPasswordButton";
            this.acceptPasswordButton.Size = new System.Drawing.Size(75, 23);
            this.acceptPasswordButton.TabIndex = 3;
            this.acceptPasswordButton.Text = "Aceptar";
            this.acceptPasswordButton.UseVisualStyleBackColor = true;
            this.acceptPasswordButton.Click += new System.EventHandler(this.acceptPasswordButton_Click);
            // 
            // ConfigurationAdminPasswordPrompt
            // 
            this.AcceptButton = this.acceptPasswordButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelPasswordButton;
            this.ClientSize = new System.Drawing.Size(384, 111);
            this.Controls.Add(this.acceptPasswordButton);
            this.Controls.Add(this.cancelPasswordButton);
            this.Controls.Add(this.adminPasswordTextBox);
            this.Controls.Add(this.enterAdminPasswordLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationAdminPasswordPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Verificación de Administrador";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label enterAdminPasswordLabel;
        private System.Windows.Forms.TextBox adminPasswordTextBox;
        private System.Windows.Forms.Button cancelPasswordButton;
        private System.Windows.Forms.Button acceptPasswordButton;
    }
}