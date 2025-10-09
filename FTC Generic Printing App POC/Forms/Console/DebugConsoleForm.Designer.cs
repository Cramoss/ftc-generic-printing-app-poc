namespace FTC_Generic_Printing_App_POC.Forms
{
    partial class DebugConsoleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugConsoleForm));
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.copyClipboardButton = new System.Windows.Forms.Button();
            this.openLogFolderButton = new System.Windows.Forms.Button();
            this.clearLogsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonsGroupBox = new System.Windows.Forms.GroupBox();
            this.logLevelComboBox = new System.Windows.Forms.ComboBox();
            this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.logGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.buttonsGroupBox.SuspendLayout();
            this.logGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(3, 16);
            this.logTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(472, 352);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            // 
            // copyClipboardButton
            // 
            this.copyClipboardButton.Location = new System.Drawing.Point(5, 48);
            this.copyClipboardButton.Margin = new System.Windows.Forms.Padding(2);
            this.copyClipboardButton.Name = "copyClipboardButton";
            this.copyClipboardButton.Size = new System.Drawing.Size(130, 25);
            this.copyClipboardButton.TabIndex = 1;
            this.copyClipboardButton.Text = "Copiar al portapapeles";
            this.copyClipboardButton.UseVisualStyleBackColor = true;
            this.copyClipboardButton.Click += new System.EventHandler(this.copyClipboardButton_Click);
            // 
            // openLogFolderButton
            // 
            this.openLogFolderButton.Location = new System.Drawing.Point(171, 48);
            this.openLogFolderButton.Margin = new System.Windows.Forms.Padding(2);
            this.openLogFolderButton.Name = "openLogFolderButton";
            this.openLogFolderButton.Size = new System.Drawing.Size(130, 25);
            this.openLogFolderButton.TabIndex = 2;
            this.openLogFolderButton.Text = "Abrir carpeta de logs";
            this.openLogFolderButton.UseVisualStyleBackColor = true;
            this.openLogFolderButton.Click += new System.EventHandler(this.openLogFolderButton_Click);
            // 
            // clearLogsButton
            // 
            this.clearLogsButton.Location = new System.Drawing.Point(345, 48);
            this.clearLogsButton.Margin = new System.Windows.Forms.Padding(2);
            this.clearLogsButton.Name = "clearLogsButton";
            this.clearLogsButton.Size = new System.Drawing.Size(130, 25);
            this.clearLogsButton.TabIndex = 3;
            this.clearLogsButton.Text = "Limpiar";
            this.clearLogsButton.UseVisualStyleBackColor = true;
            this.clearLogsButton.Click += new System.EventHandler(this.clearLogsButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonsGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.logGroupBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.96722F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.03279F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 461);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // buttonsGroupBox
            // 
            this.buttonsGroupBox.Controls.Add(this.logLevelComboBox);
            this.buttonsGroupBox.Controls.Add(this.autoScrollCheckBox);
            this.buttonsGroupBox.Controls.Add(this.copyClipboardButton);
            this.buttonsGroupBox.Controls.Add(this.clearLogsButton);
            this.buttonsGroupBox.Controls.Add(this.openLogFolderButton);
            this.buttonsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsGroupBox.Location = new System.Drawing.Point(3, 380);
            this.buttonsGroupBox.Name = "buttonsGroupBox";
            this.buttonsGroupBox.Size = new System.Drawing.Size(478, 78);
            this.buttonsGroupBox.TabIndex = 5;
            this.buttonsGroupBox.TabStop = false;
            this.buttonsGroupBox.Text = "Controles";
            // 
            // logLevelComboBox
            // 
            this.logLevelComboBox.FormattingEnabled = true;
            this.logLevelComboBox.Location = new System.Drawing.Point(345, 15);
            this.logLevelComboBox.Name = "logLevelComboBox";
            this.logLevelComboBox.Size = new System.Drawing.Size(130, 21);
            this.logLevelComboBox.TabIndex = 5;
            // 
            // autoScrollCheckBox
            // 
            this.autoScrollCheckBox.AutoSize = true;
            this.autoScrollCheckBox.Location = new System.Drawing.Point(9, 19);
            this.autoScrollCheckBox.Name = "autoScrollCheckBox";
            this.autoScrollCheckBox.Size = new System.Drawing.Size(156, 17);
            this.autoScrollCheckBox.TabIndex = 4;
            this.autoScrollCheckBox.Text = "Desplazamiento automático";
            this.autoScrollCheckBox.UseVisualStyleBackColor = true;
            // 
            // logGroupBox
            // 
            this.logGroupBox.Controls.Add(this.logTextBox);
            this.logGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logGroupBox.Location = new System.Drawing.Point(3, 3);
            this.logGroupBox.Name = "logGroupBox";
            this.logGroupBox.Size = new System.Drawing.Size(478, 371);
            this.logGroupBox.TabIndex = 5;
            this.logGroupBox.TabStop = false;
            this.logGroupBox.Text = "Logs";
            // 
            // DebugConsoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "DebugConsoleForm";
            this.Text = "Consola - FTC Generic Printing App POC";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.buttonsGroupBox.ResumeLayout(false);
            this.buttonsGroupBox.PerformLayout();
            this.logGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Button copyClipboardButton;
        private System.Windows.Forms.Button openLogFolderButton;
        private System.Windows.Forms.Button clearLogsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox buttonsGroupBox;
        private System.Windows.Forms.GroupBox logGroupBox;
        private System.Windows.Forms.CheckBox autoScrollCheckBox;
        private System.Windows.Forms.ComboBox logLevelComboBox;
    }
}