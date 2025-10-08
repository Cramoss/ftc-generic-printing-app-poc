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
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.copyClipboardButton = new System.Windows.Forms.Button();
            this.openLogFolderButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(12, 12);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(750, 505);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            // 
            // copyClipboardButton
            // 
            this.copyClipboardButton.Location = new System.Drawing.Point(12, 524);
            this.copyClipboardButton.Name = "copyClipboardButton";
            this.copyClipboardButton.Size = new System.Drawing.Size(177, 23);
            this.copyClipboardButton.TabIndex = 1;
            this.copyClipboardButton.Text = "Copiar al portapapeles";
            this.copyClipboardButton.UseVisualStyleBackColor = true;
            this.copyClipboardButton.Click += new System.EventHandler(this.copyClipboardButton_Click);
            // 
            // openLogFolderButton
            // 
            this.openLogFolderButton.Location = new System.Drawing.Point(196, 524);
            this.openLogFolderButton.Name = "openLogFolderButton";
            this.openLogFolderButton.Size = new System.Drawing.Size(169, 23);
            this.openLogFolderButton.TabIndex = 2;
            this.openLogFolderButton.Text = "Abrir carpeta de logs";
            this.openLogFolderButton.UseVisualStyleBackColor = true;
            this.openLogFolderButton.Click += new System.EventHandler(this.openLogFolderButton_Click);
            // 
            // DebugConsoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 595);
            this.Controls.Add(this.openLogFolderButton);
            this.Controls.Add(this.copyClipboardButton);
            this.Controls.Add(this.logTextBox);
            this.Name = "DebugConsoleForm";
            this.Text = "Consola - FTC Generic Printing App";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Button copyClipboardButton;
        private System.Windows.Forms.Button openLogFolderButton;
    }
}