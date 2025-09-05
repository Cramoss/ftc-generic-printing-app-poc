namespace FTC_Generic_Printing_App_POC
{
    partial class Configuration
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
            this.totemPanel = new System.Windows.Forms.Panel();
            this.editTotemConfigurationButton = new System.Windows.Forms.Button();
            this.totemConfigurationLabel = new System.Windows.Forms.Label();
            this.connectivityLabel = new System.Windows.Forms.Label();
            this.connectivityPanel = new System.Windows.Forms.Panel();
            this.firebaseStatusLabel = new System.Windows.Forms.Label();
            this.storesApiStatusLabel = new System.Windows.Forms.Label();
            this.networkStatusLabel = new System.Windows.Forms.Label();
            this.storesApiTestLabel = new System.Windows.Forms.Label();
            this.testConnectivityButton = new System.Windows.Forms.Button();
            this.firebaseTestLabel = new System.Windows.Forms.Label();
            this.internetTestLabel = new System.Windows.Forms.Label();
            this.printPanel = new System.Windows.Forms.Panel();
            this.printTestButton = new System.Windows.Forms.Button();
            this.printLabel = new System.Windows.Forms.Label();
            this.currentConfigurationPanel = new System.Windows.Forms.Panel();
            this.currentStoreId = new System.Windows.Forms.Label();
            this.currentStore = new System.Windows.Forms.Label();
            this.currentBusiness = new System.Windows.Forms.Label();
            this.currentCountry = new System.Windows.Forms.Label();
            this.currentTotemId = new System.Windows.Forms.Label();
            this.currentStoreIdLabel = new System.Windows.Forms.Label();
            this.currentStoreNameLabel = new System.Windows.Forms.Label();
            this.currentBusinessLabel = new System.Windows.Forms.Label();
            this.currentCountryLabel = new System.Windows.Forms.Label();
            this.currentTotemIdLabel = new System.Windows.Forms.Label();
            this.exitConfigurationButton = new System.Windows.Forms.Button();
            this.storesApiPanel = new System.Windows.Forms.Panel();
            this.editStoresApiConfigurationButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.totemPanel.SuspendLayout();
            this.connectivityPanel.SuspendLayout();
            this.printPanel.SuspendLayout();
            this.currentConfigurationPanel.SuspendLayout();
            this.storesApiPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // totemPanel
            // 
            this.totemPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.totemPanel.Controls.Add(this.editTotemConfigurationButton);
            this.totemPanel.Location = new System.Drawing.Point(11, 213);
            this.totemPanel.Margin = new System.Windows.Forms.Padding(2);
            this.totemPanel.Name = "totemPanel";
            this.totemPanel.Size = new System.Drawing.Size(243, 328);
            this.totemPanel.TabIndex = 0;
            // 
            // editTotemConfigurationButton
            // 
            this.editTotemConfigurationButton.Location = new System.Drawing.Point(9, 8);
            this.editTotemConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.editTotemConfigurationButton.Name = "editTotemConfigurationButton";
            this.editTotemConfigurationButton.Size = new System.Drawing.Size(219, 25);
            this.editTotemConfigurationButton.TabIndex = 19;
            this.editTotemConfigurationButton.Text = "Editar configuración";
            this.editTotemConfigurationButton.UseVisualStyleBackColor = true;
            this.editTotemConfigurationButton.Click += new System.EventHandler(this.editTotemConfigurationButton_Click);
            // 
            // totemConfigurationLabel
            // 
            this.totemConfigurationLabel.AutoSize = true;
            this.totemConfigurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totemConfigurationLabel.Location = new System.Drawing.Point(8, 192);
            this.totemConfigurationLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totemConfigurationLabel.Name = "totemConfigurationLabel";
            this.totemConfigurationLabel.Size = new System.Drawing.Size(83, 29);
            this.totemConfigurationLabel.TabIndex = 1;
            this.totemConfigurationLabel.Text = "Tótem";
            // 
            // connectivityLabel
            // 
            this.connectivityLabel.AutoSize = true;
            this.connectivityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectivityLabel.Location = new System.Drawing.Point(5, 557);
            this.connectivityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.connectivityLabel.Name = "connectivityLabel";
            this.connectivityLabel.Size = new System.Drawing.Size(153, 29);
            this.connectivityLabel.TabIndex = 2;
            this.connectivityLabel.Text = "Conectividad";
            // 
            // connectivityPanel
            // 
            this.connectivityPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.connectivityPanel.Controls.Add(this.firebaseStatusLabel);
            this.connectivityPanel.Controls.Add(this.storesApiStatusLabel);
            this.connectivityPanel.Controls.Add(this.networkStatusLabel);
            this.connectivityPanel.Controls.Add(this.storesApiTestLabel);
            this.connectivityPanel.Controls.Add(this.testConnectivityButton);
            this.connectivityPanel.Controls.Add(this.firebaseTestLabel);
            this.connectivityPanel.Controls.Add(this.internetTestLabel);
            this.connectivityPanel.Location = new System.Drawing.Point(11, 578);
            this.connectivityPanel.Margin = new System.Windows.Forms.Padding(2);
            this.connectivityPanel.Name = "connectivityPanel";
            this.connectivityPanel.Size = new System.Drawing.Size(243, 158);
            this.connectivityPanel.TabIndex = 3;
            // 
            // firebaseStatusLabel
            // 
            this.firebaseStatusLabel.AutoSize = true;
            this.firebaseStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.firebaseStatusLabel.Location = new System.Drawing.Point(131, 81);
            this.firebaseStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firebaseStatusLabel.Name = "firebaseStatusLabel";
            this.firebaseStatusLabel.Size = new System.Drawing.Size(46, 25);
            this.firebaseStatusLabel.TabIndex = 13;
            this.firebaseStatusLabel.Text = "N/A";
            // 
            // storesApiStatusLabel
            // 
            this.storesApiStatusLabel.AutoSize = true;
            this.storesApiStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.storesApiStatusLabel.Location = new System.Drawing.Point(131, 46);
            this.storesApiStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiStatusLabel.Name = "storesApiStatusLabel";
            this.storesApiStatusLabel.Size = new System.Drawing.Size(46, 25);
            this.storesApiStatusLabel.TabIndex = 12;
            this.storesApiStatusLabel.Text = "N/A";
            // 
            // networkStatusLabel
            // 
            this.networkStatusLabel.AutoSize = true;
            this.networkStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.networkStatusLabel.Location = new System.Drawing.Point(131, 14);
            this.networkStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.networkStatusLabel.Name = "networkStatusLabel";
            this.networkStatusLabel.Size = new System.Drawing.Size(46, 25);
            this.networkStatusLabel.TabIndex = 11;
            this.networkStatusLabel.Text = "N/A";
            // 
            // storesApiTestLabel
            // 
            this.storesApiTestLabel.AutoSize = true;
            this.storesApiTestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storesApiTestLabel.Location = new System.Drawing.Point(6, 44);
            this.storesApiTestLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.storesApiTestLabel.Name = "storesApiTestLabel";
            this.storesApiTestLabel.Size = new System.Drawing.Size(132, 29);
            this.storesApiTestLabel.TabIndex = 10;
            this.storesApiTestLabel.Text = "Stores API:";
            // 
            // testConnectivityButton
            // 
            this.testConnectivityButton.Location = new System.Drawing.Point(21, 125);
            this.testConnectivityButton.Margin = new System.Windows.Forms.Padding(2);
            this.testConnectivityButton.Name = "testConnectivityButton";
            this.testConnectivityButton.Size = new System.Drawing.Size(153, 25);
            this.testConnectivityButton.TabIndex = 9;
            this.testConnectivityButton.Text = "Probar conectividad";
            this.testConnectivityButton.UseVisualStyleBackColor = true;
            this.testConnectivityButton.Click += new System.EventHandler(this.testConnectivityButton_Click);
            // 
            // firebaseTestLabel
            // 
            this.firebaseTestLabel.AutoSize = true;
            this.firebaseTestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firebaseTestLabel.Location = new System.Drawing.Point(6, 79);
            this.firebaseTestLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.firebaseTestLabel.Name = "firebaseTestLabel";
            this.firebaseTestLabel.Size = new System.Drawing.Size(115, 29);
            this.firebaseTestLabel.TabIndex = 1;
            this.firebaseTestLabel.Text = "Firebase:";
            // 
            // internetTestLabel
            // 
            this.internetTestLabel.AutoSize = true;
            this.internetTestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.internetTestLabel.Location = new System.Drawing.Point(6, 12);
            this.internetTestLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.internetTestLabel.Name = "internetTestLabel";
            this.internetTestLabel.Size = new System.Drawing.Size(99, 29);
            this.internetTestLabel.TabIndex = 0;
            this.internetTestLabel.Text = "Internet:";
            // 
            // printPanel
            // 
            this.printPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.printPanel.Controls.Add(this.printTestButton);
            this.printPanel.Location = new System.Drawing.Point(268, 688);
            this.printPanel.Margin = new System.Windows.Forms.Padding(2);
            this.printPanel.Name = "printPanel";
            this.printPanel.Size = new System.Drawing.Size(236, 48);
            this.printPanel.TabIndex = 4;
            // 
            // printTestButton
            // 
            this.printTestButton.Location = new System.Drawing.Point(47, 11);
            this.printTestButton.Margin = new System.Windows.Forms.Padding(2);
            this.printTestButton.Name = "printTestButton";
            this.printTestButton.Size = new System.Drawing.Size(147, 25);
            this.printTestButton.TabIndex = 0;
            this.printTestButton.Text = "Imprimir ticket de prueba";
            this.printTestButton.UseVisualStyleBackColor = true;
            this.printTestButton.Click += new System.EventHandler(this.testTicketPrintButton_Click);
            // 
            // printLabel
            // 
            this.printLabel.AutoSize = true;
            this.printLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printLabel.Location = new System.Drawing.Point(265, 667);
            this.printLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.printLabel.Name = "printLabel";
            this.printLabel.Size = new System.Drawing.Size(120, 29);
            this.printLabel.TabIndex = 5;
            this.printLabel.Text = "Impresión";
            // 
            // currentConfigurationPanel
            // 
            this.currentConfigurationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.currentConfigurationPanel.Controls.Add(this.currentStoreId);
            this.currentConfigurationPanel.Controls.Add(this.currentStore);
            this.currentConfigurationPanel.Controls.Add(this.currentBusiness);
            this.currentConfigurationPanel.Controls.Add(this.currentCountry);
            this.currentConfigurationPanel.Controls.Add(this.currentTotemId);
            this.currentConfigurationPanel.Controls.Add(this.currentStoreIdLabel);
            this.currentConfigurationPanel.Controls.Add(this.currentStoreNameLabel);
            this.currentConfigurationPanel.Controls.Add(this.currentBusinessLabel);
            this.currentConfigurationPanel.Controls.Add(this.currentCountryLabel);
            this.currentConfigurationPanel.Controls.Add(this.currentTotemIdLabel);
            this.currentConfigurationPanel.Location = new System.Drawing.Point(8, 8);
            this.currentConfigurationPanel.Margin = new System.Windows.Forms.Padding(2);
            this.currentConfigurationPanel.Name = "currentConfigurationPanel";
            this.currentConfigurationPanel.Size = new System.Drawing.Size(637, 167);
            this.currentConfigurationPanel.TabIndex = 9;
            // 
            // currentStoreId
            // 
            this.currentStoreId.AutoSize = true;
            this.currentStoreId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.currentStoreId.Location = new System.Drawing.Point(112, 133);
            this.currentStoreId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentStoreId.Name = "currentStoreId";
            this.currentStoreId.Size = new System.Drawing.Size(46, 25);
            this.currentStoreId.TabIndex = 9;
            this.currentStoreId.Text = "N/A";
            // 
            // currentStore
            // 
            this.currentStore.AutoSize = true;
            this.currentStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.currentStore.Location = new System.Drawing.Point(112, 102);
            this.currentStore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentStore.Name = "currentStore";
            this.currentStore.Size = new System.Drawing.Size(46, 25);
            this.currentStore.TabIndex = 8;
            this.currentStore.Text = "N/A";
            // 
            // currentBusiness
            // 
            this.currentBusiness.AutoSize = true;
            this.currentBusiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.currentBusiness.Location = new System.Drawing.Point(136, 77);
            this.currentBusiness.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentBusiness.Name = "currentBusiness";
            this.currentBusiness.Size = new System.Drawing.Size(46, 25);
            this.currentBusiness.TabIndex = 7;
            this.currentBusiness.Text = "N/A";
            // 
            // currentCountry
            // 
            this.currentCountry.AutoSize = true;
            this.currentCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.currentCountry.Location = new System.Drawing.Point(136, 48);
            this.currentCountry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentCountry.Name = "currentCountry";
            this.currentCountry.Size = new System.Drawing.Size(46, 25);
            this.currentCountry.TabIndex = 6;
            this.currentCountry.Text = "N/A";
            // 
            // currentTotemId
            // 
            this.currentTotemId.AutoSize = true;
            this.currentTotemId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.currentTotemId.Location = new System.Drawing.Point(136, 21);
            this.currentTotemId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentTotemId.Name = "currentTotemId";
            this.currentTotemId.Size = new System.Drawing.Size(46, 25);
            this.currentTotemId.TabIndex = 5;
            this.currentTotemId.Text = "N/A";
            // 
            // currentStoreIdLabel
            // 
            this.currentStoreIdLabel.AutoSize = true;
            this.currentStoreIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentStoreIdLabel.Location = new System.Drawing.Point(16, 133);
            this.currentStoreIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentStoreIdLabel.Name = "currentStoreIdLabel";
            this.currentStoreIdLabel.Size = new System.Drawing.Size(124, 29);
            this.currentStoreIdLabel.TabIndex = 4;
            this.currentStoreIdLabel.Text = "ID Tienda:";
            // 
            // currentStoreNameLabel
            // 
            this.currentStoreNameLabel.AutoSize = true;
            this.currentStoreNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentStoreNameLabel.Location = new System.Drawing.Point(16, 102);
            this.currentStoreNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentStoreNameLabel.Name = "currentStoreNameLabel";
            this.currentStoreNameLabel.Size = new System.Drawing.Size(95, 29);
            this.currentStoreNameLabel.TabIndex = 3;
            this.currentStoreNameLabel.Text = "Tienda:";
            // 
            // currentBusinessLabel
            // 
            this.currentBusinessLabel.AutoSize = true;
            this.currentBusinessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentBusinessLabel.Location = new System.Drawing.Point(14, 71);
            this.currentBusinessLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentBusinessLabel.Name = "currentBusinessLabel";
            this.currentBusinessLabel.Size = new System.Drawing.Size(111, 29);
            this.currentBusinessLabel.TabIndex = 2;
            this.currentBusinessLabel.Text = "Negocio:";
            // 
            // currentCountryLabel
            // 
            this.currentCountryLabel.AutoSize = true;
            this.currentCountryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.currentCountryLabel.Location = new System.Drawing.Point(14, 44);
            this.currentCountryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentCountryLabel.Name = "currentCountryLabel";
            this.currentCountryLabel.Size = new System.Drawing.Size(66, 29);
            this.currentCountryLabel.TabIndex = 1;
            this.currentCountryLabel.Text = "País:";
            // 
            // currentTotemIdLabel
            // 
            this.currentTotemIdLabel.AutoSize = true;
            this.currentTotemIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTotemIdLabel.Location = new System.Drawing.Point(14, 18);
            this.currentTotemIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentTotemIdLabel.Name = "currentTotemIdLabel";
            this.currentTotemIdLabel.Size = new System.Drawing.Size(118, 29);
            this.currentTotemIdLabel.TabIndex = 0;
            this.currentTotemIdLabel.Text = "ID Tótem:";
            // 
            // exitConfigurationButton
            // 
            this.exitConfigurationButton.Location = new System.Drawing.Point(207, 748);
            this.exitConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitConfigurationButton.Name = "exitConfigurationButton";
            this.exitConfigurationButton.Size = new System.Drawing.Size(100, 25);
            this.exitConfigurationButton.TabIndex = 20;
            this.exitConfigurationButton.Text = "Salir";
            this.exitConfigurationButton.UseVisualStyleBackColor = true;
            this.exitConfigurationButton.Click += new System.EventHandler(this.exitConfigurationButton_Click);
            // 
            // storesApiPanel
            // 
            this.storesApiPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.storesApiPanel.Controls.Add(this.editStoresApiConfigurationButton);
            this.storesApiPanel.Location = new System.Drawing.Point(272, 213);
            this.storesApiPanel.Margin = new System.Windows.Forms.Padding(2);
            this.storesApiPanel.Name = "storesApiPanel";
            this.storesApiPanel.Size = new System.Drawing.Size(236, 328);
            this.storesApiPanel.TabIndex = 21;
            // 
            // editStoresApiConfigurationButton
            // 
            this.editStoresApiConfigurationButton.Location = new System.Drawing.Point(9, 8);
            this.editStoresApiConfigurationButton.Margin = new System.Windows.Forms.Padding(2);
            this.editStoresApiConfigurationButton.Name = "editStoresApiConfigurationButton";
            this.editStoresApiConfigurationButton.Size = new System.Drawing.Size(219, 25);
            this.editStoresApiConfigurationButton.TabIndex = 20;
            this.editStoresApiConfigurationButton.Text = "Editar configuración";
            this.editStoresApiConfigurationButton.UseVisualStyleBackColor = true;
            this.editStoresApiConfigurationButton.Click += new System.EventHandler(this.editStoresApiConfigurationButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(265, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 29);
            this.label1.TabIndex = 22;
            this.label1.Text = "Stores API";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(524, 213);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 328);
            this.panel1.TabIndex = 23;
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 781);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.storesApiPanel);
            this.Controls.Add(this.exitConfigurationButton);
            this.Controls.Add(this.printLabel);
            this.Controls.Add(this.currentConfigurationPanel);
            this.Controls.Add(this.printPanel);
            this.Controls.Add(this.connectivityPanel);
            this.Controls.Add(this.connectivityLabel);
            this.Controls.Add(this.totemConfigurationLabel);
            this.Controls.Add(this.totemPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Configuration";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Configuration_Load);
            this.totemPanel.ResumeLayout(false);
            this.connectivityPanel.ResumeLayout(false);
            this.connectivityPanel.PerformLayout();
            this.printPanel.ResumeLayout(false);
            this.currentConfigurationPanel.ResumeLayout(false);
            this.currentConfigurationPanel.PerformLayout();
            this.storesApiPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel totemPanel;
        private System.Windows.Forms.Label totemConfigurationLabel;
        private System.Windows.Forms.Label connectivityLabel;
        private System.Windows.Forms.Panel connectivityPanel;
        private System.Windows.Forms.Label firebaseTestLabel;
        private System.Windows.Forms.Label internetTestLabel;
        private System.Windows.Forms.Button testConnectivityButton;
        private System.Windows.Forms.Panel printPanel;
        private System.Windows.Forms.Button printTestButton;
        private System.Windows.Forms.Label printLabel;
        private System.Windows.Forms.Label storesApiTestLabel;
        private System.Windows.Forms.Panel currentConfigurationPanel;
        private System.Windows.Forms.Label currentStoreIdLabel;
        private System.Windows.Forms.Label currentStoreNameLabel;
        private System.Windows.Forms.Label currentBusinessLabel;
        private System.Windows.Forms.Label currentCountryLabel;
        private System.Windows.Forms.Label currentTotemIdLabel;
        private System.Windows.Forms.Label currentStoreId;
        private System.Windows.Forms.Label currentStore;
        private System.Windows.Forms.Label currentBusiness;
        private System.Windows.Forms.Label currentCountry;
        private System.Windows.Forms.Label currentTotemId;
        private System.Windows.Forms.Button editTotemConfigurationButton;
        private System.Windows.Forms.Button exitConfigurationButton;
        private System.Windows.Forms.Label firebaseStatusLabel;
        private System.Windows.Forms.Label storesApiStatusLabel;
        private System.Windows.Forms.Label networkStatusLabel;
        private System.Windows.Forms.Panel storesApiPanel;
        private System.Windows.Forms.Button editStoresApiConfigurationButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}