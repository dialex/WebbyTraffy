namespace WebbyTraffy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTotalUrlsToVisit = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.btnAction = new System.Windows.Forms.Button();
            this.grpConfigs = new System.Windows.Forms.GroupBox();
            this.btnFileProxies = new System.Windows.Forms.Button();
            this.btnFileUrls = new System.Windows.Forms.Button();
            this.chkConfigSimulateCountries = new System.Windows.Forms.CheckBox();
            this.chkConfigSimulateBrowser = new System.Windows.Forms.CheckBox();
            this.lblTotalVisits = new System.Windows.Forms.Label();
            this.txtLogger = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.spinNumberLoops = new System.Windows.Forms.NumericUpDown();
            this.spinLoopDuration = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblConfigAvgReadTime = new System.Windows.Forms.Label();
            this.spinAvgReadTime = new System.Windows.Forms.NumericUpDown();
            this.lblTotalLoops = new System.Windows.Forms.Label();
            this.grpConsole = new System.Windows.Forms.GroupBox();
            this.grpBrowser = new System.Windows.Forms.GroupBox();
            this.grpConfigs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumberLoops)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLoopDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAvgReadTime)).BeginInit();
            this.grpConsole.SuspendLayout();
            this.grpBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalUrlsToVisit
            // 
            this.lblTotalUrlsToVisit.AutoSize = true;
            this.lblTotalUrlsToVisit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTotalUrlsToVisit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalUrlsToVisit.Location = new System.Drawing.Point(6, 31);
            this.lblTotalUrlsToVisit.Name = "lblTotalUrlsToVisit";
            this.lblTotalUrlsToVisit.Size = new System.Drawing.Size(110, 15);
            this.lblTotalUrlsToVisit.TabIndex = 3;
            this.lblTotalUrlsToVisit.Tag = "Target URLs: ";
            this.lblTotalUrlsToVisit.Text = "Target URLs: (none)";
            this.lblTotalUrlsToVisit.Click += new System.EventHandler(this.lblUrlsToCall_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(3, 19);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(615, 424);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.WebBrowserShortcutsEnabled = false;
            // 
            // btnAction
            // 
            this.btnAction.Font = new System.Drawing.Font("Segoe UI Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAction.Image = ((System.Drawing.Image)(resources.GetObject("btnAction.Image")));
            this.btnAction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAction.Location = new System.Drawing.Point(12, 350);
            this.btnAction.Name = "btnAction";
            this.btnAction.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnAction.Size = new System.Drawing.Size(236, 42);
            this.btnAction.TabIndex = 6;
            this.btnAction.Text = "START!";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnActionBrowser_Click);
            // 
            // grpConfigs
            // 
            this.grpConfigs.Controls.Add(this.btnFileProxies);
            this.grpConfigs.Controls.Add(this.btnFileUrls);
            this.grpConfigs.Controls.Add(this.chkConfigSimulateCountries);
            this.grpConfigs.Controls.Add(this.chkConfigSimulateBrowser);
            this.grpConfigs.Controls.Add(this.lblTotalUrlsToVisit);
            this.grpConfigs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpConfigs.Location = new System.Drawing.Point(12, 12);
            this.grpConfigs.Name = "grpConfigs";
            this.grpConfigs.Size = new System.Drawing.Size(233, 159);
            this.grpConfigs.TabIndex = 7;
            this.grpConfigs.TabStop = false;
            this.grpConfigs.Text = "Simulation configs";
            // 
            // btnFileProxies
            // 
            this.btnFileProxies.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFileProxies.Image = global::WebbyTraffy.Properties.Resources.OpenFile;
            this.btnFileProxies.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileProxies.Location = new System.Drawing.Point(147, 70);
            this.btnFileProxies.Name = "btnFileProxies";
            this.btnFileProxies.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnFileProxies.Size = new System.Drawing.Size(80, 23);
            this.btnFileProxies.TabIndex = 15;
            this.btnFileProxies.Text = "Import...";
            this.btnFileProxies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileProxies.UseVisualStyleBackColor = true;
            // 
            // btnFileUrls
            // 
            this.btnFileUrls.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFileUrls.Image = global::WebbyTraffy.Properties.Resources.OpenFile;
            this.btnFileUrls.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileUrls.Location = new System.Drawing.Point(147, 27);
            this.btnFileUrls.Name = "btnFileUrls";
            this.btnFileUrls.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.btnFileUrls.Size = new System.Drawing.Size(80, 23);
            this.btnFileUrls.TabIndex = 12;
            this.btnFileUrls.Text = "Import...";
            this.btnFileUrls.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFileUrls.UseVisualStyleBackColor = true;
            this.btnFileUrls.Click += new System.EventHandler(this.btnFileUrls_Click);
            // 
            // chkConfigSimulateCountries
            // 
            this.chkConfigSimulateCountries.AutoSize = true;
            this.chkConfigSimulateCountries.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConfigSimulateCountries.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkConfigSimulateCountries.Location = new System.Drawing.Point(6, 74);
            this.chkConfigSimulateCountries.Name = "chkConfigSimulateCountries";
            this.chkConfigSimulateCountries.Size = new System.Drawing.Size(124, 19);
            this.chkConfigSimulateCountries.TabIndex = 1;
            this.chkConfigSimulateCountries.Text = "Simulate countries";
            this.chkConfigSimulateCountries.UseVisualStyleBackColor = true;
            // 
            // chkConfigSimulateBrowser
            // 
            this.chkConfigSimulateBrowser.AutoSize = true;
            this.chkConfigSimulateBrowser.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConfigSimulateBrowser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkConfigSimulateBrowser.Location = new System.Drawing.Point(6, 121);
            this.chkConfigSimulateBrowser.Name = "chkConfigSimulateBrowser";
            this.chkConfigSimulateBrowser.Size = new System.Drawing.Size(122, 19);
            this.chkConfigSimulateBrowser.TabIndex = 0;
            this.chkConfigSimulateBrowser.Text = "Simulate browsers";
            this.chkConfigSimulateBrowser.UseVisualStyleBackColor = true;
            // 
            // lblTotalVisits
            // 
            this.lblTotalVisits.AutoSize = true;
            this.lblTotalVisits.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalVisits.Location = new System.Drawing.Point(13, 422);
            this.lblTotalVisits.Name = "lblTotalVisits";
            this.lblTotalVisits.Size = new System.Drawing.Size(74, 15);
            this.lblTotalVisits.TabIndex = 8;
            this.lblTotalVisits.Tag = "Total visits: ";
            this.lblTotalVisits.Text = "Total visits: 0";
            // 
            // txtLogger
            // 
            this.txtLogger.BackColor = System.Drawing.Color.Black;
            this.txtLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLogger.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLogger.ForeColor = System.Drawing.Color.White;
            this.txtLogger.Location = new System.Drawing.Point(3, 19);
            this.txtLogger.Multiline = true;
            this.txtLogger.Name = "txtLogger";
            this.txtLogger.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogger.Size = new System.Drawing.Size(854, 126);
            this.txtLogger.TabIndex = 9;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DereferenceLinks = false;
            this.openFileDialog.Filter = "\"Text|*.txt|All|*.*\"";
            // 
            // picLoading
            // 
            this.picLoading.Image = global::WebbyTraffy.Properties.Resources.LoadingAnimation;
            this.picLoading.Location = new System.Drawing.Point(220, 398);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(28, 42);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoading.TabIndex = 15;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.spinNumberLoops);
            this.groupBox1.Controls.Add(this.spinLoopDuration);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblConfigAvgReadTime);
            this.groupBox1.Controls.Add(this.spinAvgReadTime);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(12, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 152);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Traffic configs";
            // 
            // spinNumberLoops
            // 
            this.spinNumberLoops.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spinNumberLoops.Location = new System.Drawing.Point(169, 26);
            this.spinNumberLoops.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.spinNumberLoops.Name = "spinNumberLoops";
            this.spinNumberLoops.Size = new System.Drawing.Size(58, 23);
            this.spinNumberLoops.TabIndex = 21;
            this.spinNumberLoops.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinNumberLoops.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // spinLoopDuration
            // 
            this.spinLoopDuration.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spinLoopDuration.Location = new System.Drawing.Point(169, 69);
            this.spinLoopDuration.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.spinLoopDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinLoopDuration.Name = "spinLoopDuration";
            this.spinLoopDuration.Size = new System.Drawing.Size(58, 23);
            this.spinLoopDuration.TabIndex = 20;
            this.spinLoopDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinLoopDuration.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 19;
            this.label2.Tag = "";
            this.label2.Text = "Dispersed during (min)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 15);
            this.label1.TabIndex = 18;
            this.label1.Tag = "";
            this.label1.Text = "Number of visits (per URL)";
            // 
            // lblConfigAvgReadTime
            // 
            this.lblConfigAvgReadTime.AutoSize = true;
            this.lblConfigAvgReadTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblConfigAvgReadTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfigAvgReadTime.Location = new System.Drawing.Point(6, 117);
            this.lblConfigAvgReadTime.Name = "lblConfigAvgReadTime";
            this.lblConfigAvgReadTime.Size = new System.Drawing.Size(105, 15);
            this.lblConfigAvgReadTime.TabIndex = 16;
            this.lblConfigAvgReadTime.Tag = "";
            this.lblConfigAvgReadTime.Text = "Visit duration (sec)";
            // 
            // spinAvgReadTime
            // 
            this.spinAvgReadTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spinAvgReadTime.Location = new System.Drawing.Point(169, 113);
            this.spinAvgReadTime.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.spinAvgReadTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinAvgReadTime.Name = "spinAvgReadTime";
            this.spinAvgReadTime.Size = new System.Drawing.Size(58, 23);
            this.spinAvgReadTime.TabIndex = 0;
            this.spinAvgReadTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.spinAvgReadTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTotalLoops
            // 
            this.lblTotalLoops.AutoSize = true;
            this.lblTotalLoops.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalLoops.Location = new System.Drawing.Point(13, 401);
            this.lblTotalLoops.Name = "lblTotalLoops";
            this.lblTotalLoops.Size = new System.Drawing.Size(126, 15);
            this.lblTotalLoops.TabIndex = 17;
            this.lblTotalLoops.Tag = "Total visits (per URL): ";
            this.lblTotalLoops.Text = "Total visits (per URL): 0";
            // 
            // grpConsole
            // 
            this.grpConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConsole.Controls.Add(this.txtLogger);
            this.grpConsole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpConsole.Location = new System.Drawing.Point(12, 456);
            this.grpConsole.Name = "grpConsole";
            this.grpConsole.Size = new System.Drawing.Size(860, 148);
            this.grpConsole.TabIndex = 18;
            this.grpConsole.TabStop = false;
            this.grpConsole.Text = "Output Log";
            // 
            // grpBrowser
            // 
            this.grpBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBrowser.Controls.Add(this.webBrowser);
            this.grpBrowser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpBrowser.Location = new System.Drawing.Point(251, 12);
            this.grpBrowser.Name = "grpBrowser";
            this.grpBrowser.Size = new System.Drawing.Size(621, 446);
            this.grpBrowser.TabIndex = 19;
            this.grpBrowser.TabStop = false;
            this.grpBrowser.Text = "Browser";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 616);
            this.Controls.Add(this.grpBrowser);
            this.Controls.Add(this.grpConsole);
            this.Controls.Add(this.lblTotalLoops);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.lblTotalVisits);
            this.Controls.Add(this.grpConfigs);
            this.MinimumSize = new System.Drawing.Size(16, 655);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.grpConfigs.ResumeLayout(false);
            this.grpConfigs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNumberLoops)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLoopDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAvgReadTime)).EndInit();
            this.grpConsole.ResumeLayout(false);
            this.grpConsole.PerformLayout();
            this.grpBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTotalUrlsToVisit;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.GroupBox grpConfigs;
        private System.Windows.Forms.CheckBox chkConfigSimulateCountries;
        private System.Windows.Forms.CheckBox chkConfigSimulateBrowser;
        private System.Windows.Forms.Label lblTotalVisits;
        private System.Windows.Forms.TextBox txtLogger;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnFileUrls;
        private System.Windows.Forms.Button btnFileProxies;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblTotalLoops;
        private System.Windows.Forms.Label lblConfigAvgReadTime;
        private System.Windows.Forms.NumericUpDown spinAvgReadTime;
        private System.Windows.Forms.GroupBox grpConsole;
        private System.Windows.Forms.GroupBox grpBrowser;
        private System.Windows.Forms.NumericUpDown spinLoopDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown spinNumberLoops;
    }
}

