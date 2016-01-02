namespace AdClicky
{
    partial class MainForm
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblUrlsToCall = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.btnActionBrowser = new System.Windows.Forms.Button();
            this.grpConfigs = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.chkConfigSimulateBrowser = new System.Windows.Forms.CheckBox();
            this.lblTotalCalls = new System.Windows.Forms.Label();
            this.txtLogger = new System.Windows.Forms.TextBox();
            this.lblLogger = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnFileUrls = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.grpConfigs.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBox2.Location = new System.Drawing.Point(53, 66);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(219, 23);
            this.textBox2.TabIndex = 1;
            // 
            // lblUrlsToCall
            // 
            this.lblUrlsToCall.AutoSize = true;
            this.lblUrlsToCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUrlsToCall.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUrlsToCall.Location = new System.Drawing.Point(12, 12);
            this.lblUrlsToCall.Name = "lblUrlsToCall";
            this.lblUrlsToCall.Size = new System.Drawing.Size(109, 15);
            this.lblUrlsToCall.TabIndex = 3;
            this.lblUrlsToCall.Tag = "URLs to call: ";
            this.lblUrlsToCall.Text = "URLs to call: (none)";
            this.lblUrlsToCall.Click += new System.EventHandler(this.lblUrlsToCall_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Proxy";
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(15, 266);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(895, 164);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // btnActionBrowser
            // 
            this.btnActionBrowser.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActionBrowser.Location = new System.Drawing.Point(718, 200);
            this.btnActionBrowser.Name = "btnActionBrowser";
            this.btnActionBrowser.Size = new System.Drawing.Size(192, 42);
            this.btnActionBrowser.TabIndex = 6;
            this.btnActionBrowser.Text = "DO IT!";
            this.btnActionBrowser.UseVisualStyleBackColor = true;
            this.btnActionBrowser.Click += new System.EventHandler(this.btnActionBrowser_Click);
            // 
            // grpConfigs
            // 
            this.grpConfigs.Controls.Add(this.checkBox3);
            this.grpConfigs.Controls.Add(this.checkBox2);
            this.grpConfigs.Controls.Add(this.chkConfigSimulateBrowser);
            this.grpConfigs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpConfigs.Location = new System.Drawing.Point(718, 12);
            this.grpConfigs.Name = "grpConfigs";
            this.grpConfigs.Size = new System.Drawing.Size(192, 182);
            this.grpConfigs.TabIndex = 7;
            this.grpConfigs.TabStop = false;
            this.grpConfigs.Text = "Configurations";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox3.Location = new System.Drawing.Point(6, 84);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(82, 19);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBox2.Location = new System.Drawing.Point(6, 55);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 19);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Simulate";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // chkConfigSimulateBrowser
            // 
            this.chkConfigSimulateBrowser.AutoSize = true;
            this.chkConfigSimulateBrowser.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConfigSimulateBrowser.Checked = true;
            this.chkConfigSimulateBrowser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConfigSimulateBrowser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkConfigSimulateBrowser.Location = new System.Drawing.Point(6, 26);
            this.chkConfigSimulateBrowser.Name = "chkConfigSimulateBrowser";
            this.chkConfigSimulateBrowser.Size = new System.Drawing.Size(122, 19);
            this.chkConfigSimulateBrowser.TabIndex = 0;
            this.chkConfigSimulateBrowser.Text = "Simulate Browsers";
            this.chkConfigSimulateBrowser.UseVisualStyleBackColor = true;
            // 
            // lblTotalCalls
            // 
            this.lblTotalCalls.AutoSize = true;
            this.lblTotalCalls.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalCalls.Location = new System.Drawing.Point(798, 245);
            this.lblTotalCalls.Name = "lblTotalCalls";
            this.lblTotalCalls.Size = new System.Drawing.Size(33, 15);
            this.lblTotalCalls.TabIndex = 8;
            this.lblTotalCalls.Tag = "Total: ";
            this.lblTotalCalls.Text = "Total";
            // 
            // txtLogger
            // 
            this.txtLogger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogger.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLogger.Location = new System.Drawing.Point(12, 451);
            this.txtLogger.Multiline = true;
            this.txtLogger.Name = "txtLogger";
            this.txtLogger.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogger.Size = new System.Drawing.Size(898, 148);
            this.txtLogger.TabIndex = 9;
            // 
            // lblLogger
            // 
            this.lblLogger.AutoSize = true;
            this.lblLogger.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogger.Location = new System.Drawing.Point(12, 433);
            this.lblLogger.Name = "lblLogger";
            this.lblLogger.Size = new System.Drawing.Size(188, 15);
            this.lblLogger.TabIndex = 11;
            this.lblLogger.Text = "What\'s happening under the hood";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DereferenceLinks = false;
            this.openFileDialog.Filter = "\"Text|*.txt|All|*.*\"";
            // 
            // btnFileUrls
            // 
            this.btnFileUrls.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFileUrls.Location = new System.Drawing.Point(173, 8);
            this.btnFileUrls.Name = "btnFileUrls";
            this.btnFileUrls.Size = new System.Drawing.Size(75, 23);
            this.btnFileUrls.TabIndex = 12;
            this.btnFileUrls.Text = "Load file...";
            this.btnFileUrls.UseVisualStyleBackColor = true;
            this.btnFileUrls.Click += new System.EventHandler(this.btnFileUrls_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(12, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Fake browser";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 611);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFileUrls);
            this.Controls.Add(this.lblLogger);
            this.Controls.Add(this.txtLogger);
            this.Controls.Add(this.lblTotalCalls);
            this.Controls.Add(this.grpConfigs);
            this.Controls.Add(this.btnActionBrowser);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUrlsToCall);
            this.Controls.Add(this.textBox2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.grpConfigs.ResumeLayout(false);
            this.grpConfigs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblUrlsToCall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button btnActionBrowser;
        private System.Windows.Forms.GroupBox grpConfigs;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox chkConfigSimulateBrowser;
        private System.Windows.Forms.Label lblTotalCalls;
        private System.Windows.Forms.TextBox txtLogger;
        private System.Windows.Forms.Label lblLogger;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnFileUrls;
        private System.Windows.Forms.Label label3;
    }
}

