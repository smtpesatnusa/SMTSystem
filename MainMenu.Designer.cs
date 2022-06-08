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
            this.packingListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelPartnumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.custCodeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrapPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.modelMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialMasterToolStripMenuItem_Click = new System.Windows.Forms.ToolStripMenuItem();
            this.materialMasterXMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.departmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 742);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1366, 26);
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
            this.packingListToolStripMenuItem,
            this.labelPartnumberToolStripMenuItem,
            this.scrapPartToolStripMenuItem,
            this.printLabelToolStripMenuItem,
            this.masterDataToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.administrationToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(211, 206);
            // 
            // packingListToolStripMenuItem
            // 
            this.packingListToolStripMenuItem.Name = "packingListToolStripMenuItem";
            this.packingListToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.packingListToolStripMenuItem.Text = "Packing List XM";
            this.packingListToolStripMenuItem.Visible = false;
            this.packingListToolStripMenuItem.Click += new System.EventHandler(this.packingListToolStripMenuItem_Click);
            // 
            // labelPartnumberToolStripMenuItem
            // 
            this.labelPartnumberToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.custCodeControlToolStripMenuItem});
            this.labelPartnumberToolStripMenuItem.Name = "labelPartnumberToolStripMenuItem";
            this.labelPartnumberToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.labelPartnumberToolStripMenuItem.Text = "Label Partnumber";
            this.labelPartnumberToolStripMenuItem.Visible = false;
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.dataToolStripMenuItem.Text = "Data";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // custCodeControlToolStripMenuItem
            // 
            this.custCodeControlToolStripMenuItem.Name = "custCodeControlToolStripMenuItem";
            this.custCodeControlToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.custCodeControlToolStripMenuItem.Text = "Cust Code Control";
            this.custCodeControlToolStripMenuItem.Click += new System.EventHandler(this.custCodeControlToolStripMenuItem_Click);
            // 
            // scrapPartToolStripMenuItem
            // 
            this.scrapPartToolStripMenuItem.Name = "scrapPartToolStripMenuItem";
            this.scrapPartToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.scrapPartToolStripMenuItem.Text = "Scrap Part";
            this.scrapPartToolStripMenuItem.Click += new System.EventHandler(this.scrapPartToolStripMenuItem_Click);
            // 
            // printLabelToolStripMenuItem
            // 
            this.printLabelToolStripMenuItem.Name = "printLabelToolStripMenuItem";
            this.printLabelToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.printLabelToolStripMenuItem.Text = "Print Label";
            this.printLabelToolStripMenuItem.Visible = false;
            this.printLabelToolStripMenuItem.Click += new System.EventHandler(this.printLabelToolStripMenuItem_Click);
            // 
            // masterDataToolStripMenuItem1
            // 
            this.masterDataToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelMasterToolStripMenuItem,
            this.materialMasterToolStripMenuItem_Click,
            this.materialMasterXMToolStripMenuItem});
            this.masterDataToolStripMenuItem1.Name = "masterDataToolStripMenuItem1";
            this.masterDataToolStripMenuItem1.Size = new System.Drawing.Size(210, 24);
            this.masterDataToolStripMenuItem1.Text = "Master Data";
            // 
            // modelMasterToolStripMenuItem
            // 
            this.modelMasterToolStripMenuItem.Name = "modelMasterToolStripMenuItem";
            this.modelMasterToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.modelMasterToolStripMenuItem.Text = "Model Master";
            this.modelMasterToolStripMenuItem.Visible = false;
            this.modelMasterToolStripMenuItem.Click += new System.EventHandler(this.modelMasterToolStripMenuItem_Click);
            // 
            // materialMasterToolStripMenuItem_Click
            // 
            this.materialMasterToolStripMenuItem_Click.Name = "materialMasterToolStripMenuItem_Click";
            this.materialMasterToolStripMenuItem_Click.Size = new System.Drawing.Size(222, 26);
            this.materialMasterToolStripMenuItem_Click.Text = "Material Master";
            this.materialMasterToolStripMenuItem_Click.Click += new System.EventHandler(this.materialMasterToolStripMenuItem_Click_Click);
            // 
            // materialMasterXMToolStripMenuItem
            // 
            this.materialMasterXMToolStripMenuItem.Name = "materialMasterXMToolStripMenuItem";
            this.materialMasterXMToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.materialMasterXMToolStripMenuItem.Text = "Material Master XM";
            this.materialMasterXMToolStripMenuItem.Visible = false;
            this.materialMasterXMToolStripMenuItem.Click += new System.EventHandler(this.materialMasterXMToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // administrationToolStripMenuItem
            // 
            this.administrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.departmentToolStripMenuItem,
            this.userLevelToolStripMenuItem,
            this.userToolStripMenuItem,
            this.userRoleToolStripMenuItem,
            this.changePasswordToolStripMenuItem});
            this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
            this.administrationToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.administrationToolStripMenuItem.Text = "Administration";
            // 
            // departmentToolStripMenuItem
            // 
            this.departmentToolStripMenuItem.Name = "departmentToolStripMenuItem";
            this.departmentToolStripMenuItem.Size = new System.Drawing.Size(208, 26);
            this.departmentToolStripMenuItem.Text = "Department";
            this.departmentToolStripMenuItem.Click += new System.EventHandler(this.departmentToolStripMenuItem_Click);
            // 
            // userLevelToolStripMenuItem
            // 
            this.userLevelToolStripMenuItem.Name = "userLevelToolStripMenuItem";
            this.userLevelToolStripMenuItem.Size = new System.Drawing.Size(208, 26);
            this.userLevelToolStripMenuItem.Text = "User Level";
            this.userLevelToolStripMenuItem.Click += new System.EventHandler(this.userLevelToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(208, 26);
            this.userToolStripMenuItem.Text = "User";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // userRoleToolStripMenuItem
            // 
            this.userRoleToolStripMenuItem.Name = "userRoleToolStripMenuItem";
            this.userRoleToolStripMenuItem.Size = new System.Drawing.Size(208, 26);
            this.userRoleToolStripMenuItem.Text = "User Role";
            this.userRoleToolStripMenuItem.Visible = false;
            this.userRoleToolStripMenuItem.Click += new System.EventHandler(this.userRoleToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(208, 26);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
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
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.Text = "ALFASYS";
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
        private System.Windows.Forms.ToolStripMenuItem packingListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem materialMasterXMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelPartnumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem userLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userRoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrapPartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem custCodeControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialMasterToolStripMenuItem_Click;
        private System.Windows.Forms.ToolStripMenuItem departmentToolStripMenuItem;
    }
}

