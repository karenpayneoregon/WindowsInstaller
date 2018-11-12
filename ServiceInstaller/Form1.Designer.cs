namespace ServiceInstaller
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtServiceStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.installServiceButton = new System.Windows.Forms.Button();
            this.uninstallServiceButton = new System.Windows.Forms.Button();
            this.startServiceButton = new System.Windows.Forms.Button();
            this.stopServiceButton = new System.Windows.Forms.Button();
            this.applicationCloseButton = new System.Windows.Forms.Button();
            this.checkBoxTopMost = new System.Windows.Forms.CheckBox();
            this.chkStart = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtServiceStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 246);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(269, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtServiceStatus
            // 
            this.txtServiceStatus.Name = "txtServiceStatus";
            this.txtServiceStatus.Size = new System.Drawing.Size(78, 17);
            this.txtServiceStatus.Text = "Service status";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // installServiceButton
            // 
            this.installServiceButton.Location = new System.Drawing.Point(20, 31);
            this.installServiceButton.Margin = new System.Windows.Forms.Padding(2);
            this.installServiceButton.Name = "installServiceButton";
            this.installServiceButton.Size = new System.Drawing.Size(104, 36);
            this.installServiceButton.TabIndex = 0;
            this.installServiceButton.Text = "Install Service";
            this.installServiceButton.UseVisualStyleBackColor = true;
            this.installServiceButton.Click += new System.EventHandler(this.installServiceButton_Click);
            // 
            // uninstallServiceButton
            // 
            this.uninstallServiceButton.Location = new System.Drawing.Point(20, 71);
            this.uninstallServiceButton.Margin = new System.Windows.Forms.Padding(2);
            this.uninstallServiceButton.Name = "uninstallServiceButton";
            this.uninstallServiceButton.Size = new System.Drawing.Size(104, 36);
            this.uninstallServiceButton.TabIndex = 2;
            this.uninstallServiceButton.Text = "Uninstall Service";
            this.uninstallServiceButton.UseVisualStyleBackColor = true;
            this.uninstallServiceButton.Click += new System.EventHandler(this.uninstallServiceButton_Click);
            // 
            // startServiceButton
            // 
            this.startServiceButton.Location = new System.Drawing.Point(20, 114);
            this.startServiceButton.Margin = new System.Windows.Forms.Padding(2);
            this.startServiceButton.Name = "startServiceButton";
            this.startServiceButton.Size = new System.Drawing.Size(104, 36);
            this.startServiceButton.TabIndex = 3;
            this.startServiceButton.Text = "Start Service";
            this.startServiceButton.UseVisualStyleBackColor = true;
            this.startServiceButton.Click += new System.EventHandler(this.startServiceButton_Click);
            // 
            // stopServiceButton
            // 
            this.stopServiceButton.Location = new System.Drawing.Point(144, 114);
            this.stopServiceButton.Margin = new System.Windows.Forms.Padding(2);
            this.stopServiceButton.Name = "stopServiceButton";
            this.stopServiceButton.Size = new System.Drawing.Size(104, 36);
            this.stopServiceButton.TabIndex = 4;
            this.stopServiceButton.Text = "Stop Service";
            this.stopServiceButton.UseVisualStyleBackColor = true;
            this.stopServiceButton.Click += new System.EventHandler(this.stopServiceButton_Click);
            // 
            // applicationCloseButton
            // 
            this.applicationCloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.applicationCloseButton.Location = new System.Drawing.Point(144, 166);
            this.applicationCloseButton.Margin = new System.Windows.Forms.Padding(2);
            this.applicationCloseButton.Name = "applicationCloseButton";
            this.applicationCloseButton.Size = new System.Drawing.Size(104, 36);
            this.applicationCloseButton.TabIndex = 6;
            this.applicationCloseButton.Text = "Close";
            this.applicationCloseButton.UseVisualStyleBackColor = true;
            this.applicationCloseButton.Click += new System.EventHandler(this.applicationCloseButton_Click);
            // 
            // checkBoxTopMost
            // 
            this.checkBoxTopMost.AutoSize = true;
            this.checkBoxTopMost.Checked = global::ServiceInstaller.Properties.Settings.Default.TopMostSetting;
            this.checkBoxTopMost.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::ServiceInstaller.Properties.Settings.Default, "TopMostSetting", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxTopMost.Location = new System.Drawing.Point(20, 176);
            this.checkBoxTopMost.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxTopMost.Name = "checkBoxTopMost";
            this.checkBoxTopMost.Size = new System.Drawing.Size(67, 17);
            this.checkBoxTopMost.TabIndex = 5;
            this.checkBoxTopMost.Text = "Topmost";
            this.checkBoxTopMost.UseVisualStyleBackColor = true;
            this.checkBoxTopMost.CheckedChanged += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            // 
            // chkStart
            // 
            this.chkStart.AutoSize = true;
            this.chkStart.Location = new System.Drawing.Point(129, 42);
            this.chkStart.Name = "chkStart";
            this.chkStart.Size = new System.Drawing.Size(99, 17);
            this.chkStart.TabIndex = 1;
            this.chkStart.Text = "Start with install";
            this.chkStart.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 268);
            this.Controls.Add(this.chkStart);
            this.Controls.Add(this.checkBoxTopMost);
            this.Controls.Add(this.applicationCloseButton);
            this.Controls.Add(this.stopServiceButton);
            this.Controls.Add(this.startServiceButton);
            this.Controls.Add(this.uninstallServiceButton);
            this.Controls.Add(this.installServiceButton);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Installer";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtServiceStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button installServiceButton;
        private System.Windows.Forms.Button uninstallServiceButton;
        private System.Windows.Forms.Button startServiceButton;
        private System.Windows.Forms.Button stopServiceButton;
        private System.Windows.Forms.Button applicationCloseButton;
        private System.Windows.Forms.CheckBox checkBoxTopMost;
        private System.Windows.Forms.CheckBox chkStart;
    }
}

