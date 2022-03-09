namespace SMTPE
{
    partial class MainMenu
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.bOMSAPVsLLVsPRoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labelPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modelMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUsername,
            this.toolStripStatusLabel1,
            this.dateTimeNow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1099, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripUsername
            // 
            this.toolStripUsername.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripUsername.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripUsername.Name = "toolStripUsername";
            this.toolStripUsername.Size = new System.Drawing.Size(156, 20);
            this.toolStripUsername.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(287, 20);
            this.toolStripStatusLabel1.Text = "Developed by IT-PE SMT Dept with ❤  | ";
            // 
            // dateTimeNow
            // 
            this.dateTimeNow.Name = "dateTimeNow";
            this.dateTimeNow.Size = new System.Drawing.Size(14, 20);
            this.dateTimeNow.Text = "-";
            // 
            // bOMSAPVsLLVsPRoToolStripMenuItem
            // 
            this.bOMSAPVsLLVsPRoToolStripMenuItem.Name = "bOMSAPVsLLVsPRoToolStripMenuItem";
            this.bOMSAPVsLLVsPRoToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelPrintToolStripMenuItem,
            this.masterDataToolStripMenuItem1,
            this.logOutToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(211, 104);
            // 
            // labelPrintToolStripMenuItem
            // 
            this.labelPrintToolStripMenuItem.Name = "labelPrintToolStripMenuItem";
            this.labelPrintToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.labelPrintToolStripMenuItem.Text = "Packing List";
            // 
            // masterDataToolStripMenuItem1
            // 
            this.masterDataToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelMasterToolStripMenuItem,
            this.materialMasterToolStripMenuItem});
            this.masterDataToolStripMenuItem1.Name = "masterDataToolStripMenuItem1";
            this.masterDataToolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.masterDataToolStripMenuItem1.Text = "Master Data";
            // 
            // modelMasterToolStripMenuItem
            // 
            this.modelMasterToolStripMenuItem.Name = "modelMasterToolStripMenuItem";
            this.modelMasterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.modelMasterToolStripMenuItem.Text = "Model Master";
            this.modelMasterToolStripMenuItem.Click += new System.EventHandler(this.modelMasterToolStripMenuItem_Click);
            // 
            // materialMasterToolStripMenuItem
            // 
            this.materialMasterToolStripMenuItem.Name = "materialMasterToolStripMenuItem";
            this.materialMasterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.materialMasterToolStripMenuItem.Text = "Material Master";
            this.materialMasterToolStripMenuItem.Click += new System.EventHandler(this.materialMasterToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 486);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "Generate Inbound App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMenu_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.Timer timer;
        public System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem bOMSAPVsLLVsPRoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem modelMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelPrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem materialMasterToolStripMenuItem;
    }
}

