
namespace SMTPE
{
    partial class ScrapPartnumberList
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
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewScrapPartList = new System.Windows.Forms.DataGridView();
            this.truncateMasterMaterialLbl = new System.Windows.Forms.LinkLabel();
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.txtDisplayPageNo = new System.Windows.Forms.TextBox();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.addScrapPartButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.refreshLbl = new System.Windows.Forms.LinkLabel();
            this.exportBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCust = new System.Windows.Forms.ComboBox();
            this.allDate = new System.Windows.Forms.CheckBox();
            this.filterBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScrapPartList)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.Location = new System.Drawing.Point(796, 122);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(200, 26);
            this.tbSearch.TabIndex = 13;
            this.tbSearch.Visible = false;
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(742, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Search";
            this.label1.Visible = false;
            // 
            // dataGridViewScrapPartList
            // 
            this.dataGridViewScrapPartList.AllowUserToAddRows = false;
            this.dataGridViewScrapPartList.AllowUserToDeleteRows = false;
            this.dataGridViewScrapPartList.AllowUserToOrderColumns = true;
            this.dataGridViewScrapPartList.AllowUserToResizeRows = false;
            this.dataGridViewScrapPartList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewScrapPartList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewScrapPartList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScrapPartList.Location = new System.Drawing.Point(23, 179);
            this.dataGridViewScrapPartList.Name = "dataGridViewScrapPartList";
            this.dataGridViewScrapPartList.ReadOnly = true;
            this.dataGridViewScrapPartList.RowHeadersWidth = 51;
            this.dataGridViewScrapPartList.Size = new System.Drawing.Size(973, 494);
            this.dataGridViewScrapPartList.TabIndex = 10;
            this.dataGridViewScrapPartList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewScrapPartList_CellFormatting);
            // 
            // truncateMasterMaterialLbl
            // 
            this.truncateMasterMaterialLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.truncateMasterMaterialLbl.AutoSize = true;
            this.truncateMasterMaterialLbl.BackColor = System.Drawing.Color.Transparent;
            this.truncateMasterMaterialLbl.Location = new System.Drawing.Point(856, 157);
            this.truncateMasterMaterialLbl.Name = "truncateMasterMaterialLbl";
            this.truncateMasterMaterialLbl.Size = new System.Drawing.Size(73, 19);
            this.truncateMasterMaterialLbl.TabIndex = 61;
            this.truncateMasterMaterialLbl.TabStop = true;
            this.truncateMasterMaterialLbl.Text = "Delete All";
            this.truncateMasterMaterialLbl.Visible = false;
            this.truncateMasterMaterialLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tbTruncateMasterMaterial_LinkClicked);
            // 
            // toolStripUsername
            // 
            this.toolStripUsername.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripUsername.Name = "toolStripUsername";
            this.toolStripUsername.Size = new System.Drawing.Size(156, 20);
            this.toolStripUsername.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(287, 20);
            this.toolStripStatusLabel1.Text = "Developed by IT-PE SMT Dept with ❤  | ";
            // 
            // dateTimeNow
            // 
            this.dateTimeNow.BackColor = System.Drawing.SystemColors.Control;
            this.dateTimeNow.Name = "dateTimeNow";
            this.dateTimeNow.Size = new System.Drawing.Size(14, 20);
            this.dateTimeNow.Text = "-";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Open Sans", 9F);
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUsername,
            this.toolStripStatusLabel1,
            this.dateTimeNow});
            this.statusStrip1.Location = new System.Drawing.Point(0, 742);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 26);
            this.statusStrip1.TabIndex = 59;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // txtDisplayPageNo
            // 
            this.txtDisplayPageNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtDisplayPageNo.Location = new System.Drawing.Point(451, 700);
            this.txtDisplayPageNo.Name = "txtDisplayPageNo";
            this.txtDisplayPageNo.ReadOnly = true;
            this.txtDisplayPageNo.Size = new System.Drawing.Size(100, 26);
            this.txtDisplayPageNo.TabIndex = 100;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNextPage.Location = new System.Drawing.Point(565, 699);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 99;
            this.btnNextPage.Text = "Next >";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPreviousPage.Location = new System.Drawing.Point(364, 699);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(75, 23);
            this.btnPreviousPage.TabIndex = 98;
            this.btnPreviousPage.Text = "< Prev";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLastPage.Location = new System.Drawing.Point(646, 699);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(75, 23);
            this.btnLastPage.TabIndex = 97;
            this.btnLastPage.Text = "Last >>";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFirstPage.Location = new System.Drawing.Point(283, 699);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(75, 23);
            this.btnFirstPage.TabIndex = 96;
            this.btnFirstPage.Text = "<< First";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // addScrapPartButton
            // 
            this.addScrapPartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addScrapPartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addScrapPartButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addScrapPartButton.Image = global::SMTPE.Properties.Resources.icons8_add_20;
            this.addScrapPartButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.addScrapPartButton.Location = new System.Drawing.Point(806, 69);
            this.addScrapPartButton.Name = "addScrapPartButton";
            this.addScrapPartButton.Size = new System.Drawing.Size(108, 41);
            this.addScrapPartButton.TabIndex = 60;
            this.addScrapPartButton.Text = "Scrap Part";
            this.addScrapPartButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.addScrapPartButton.UseVisualStyleBackColor = true;
            this.addScrapPartButton.Click += new System.EventHandler(this.addScrapPartButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Image = global::SMTPE.Properties.Resources.icons8_reply_arrow_20;
            this.backButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.backButton.Location = new System.Drawing.Point(920, 70);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(76, 40);
            this.backButton.TabIndex = 58;
            this.backButton.Text = "Back";
            this.backButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // refreshLbl
            // 
            this.refreshLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshLbl.AutoSize = true;
            this.refreshLbl.BackColor = System.Drawing.Color.Transparent;
            this.refreshLbl.Location = new System.Drawing.Point(935, 157);
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(61, 19);
            this.refreshLbl.TabIndex = 102;
            this.refreshLbl.TabStop = true;
            this.refreshLbl.Text = "Refresh";
            this.refreshLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.refreshLbl_LinkClicked);
            // 
            // exportBtn
            // 
            this.exportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportBtn.Enabled = false;
            this.exportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportBtn.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.exportBtn.Image = global::SMTPE.Properties.Resources.icons8_export_excel_20;
            this.exportBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exportBtn.Location = new System.Drawing.Point(719, 69);
            this.exportBtn.Margin = new System.Windows.Forms.Padding(4);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(80, 41);
            this.exportBtn.TabIndex = 195;
            this.exportBtn.Text = "Export";
            this.exportBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbCust);
            this.groupBox1.Controls.Add(this.allDate);
            this.groupBox1.Controls.Add(this.filterBtn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePickerFrom);
            this.groupBox1.Controls.Add(this.dateTimePickerTo);
            this.groupBox1.Location = new System.Drawing.Point(23, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 88);
            this.groupBox1.TabIndex = 196;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            this.groupBox1.Visible = false;
            // 
            // cmbCust
            // 
            this.cmbCust.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCust.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCust.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCust.Font = new System.Drawing.Font("Open Sans", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCust.FormattingEnabled = true;
            this.cmbCust.IntegralHeight = false;
            this.cmbCust.Location = new System.Drawing.Point(11, 22);
            this.cmbCust.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCust.MaxDropDownItems = 10;
            this.cmbCust.Name = "cmbCust";
            this.cmbCust.Size = new System.Drawing.Size(192, 26);
            this.cmbCust.TabIndex = 205;
            // 
            // allDate
            // 
            this.allDate.AutoSize = true;
            this.allDate.BackColor = System.Drawing.Color.Transparent;
            this.allDate.Location = new System.Drawing.Point(210, 24);
            this.allDate.Name = "allDate";
            this.allDate.Size = new System.Drawing.Size(48, 23);
            this.allDate.TabIndex = 202;
            this.allDate.Text = "All";
            this.allDate.UseVisualStyleBackColor = false;
            // 
            // filterBtn
            // 
            this.filterBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterBtn.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filterBtn.Location = new System.Drawing.Point(11, 55);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(76, 26);
            this.filterBtn.TabIndex = 201;
            this.filterBtn.Text = "Load";
            this.filterBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.filterBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(400, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 19);
            this.label2.TabIndex = 200;
            this.label2.Text = "To";
            this.label2.Visible = false;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(264, 21);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(131, 26);
            this.dateTimePickerFrom.TabIndex = 197;
            this.dateTimePickerFrom.Visible = false;
            this.dateTimePickerFrom.ValueChanged += new System.EventHandler(this.dateTimePickerFrom_ValueChanged);
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTo.Location = new System.Drawing.Point(431, 21);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(131, 26);
            this.dateTimePickerTo.TabIndex = 199;
            this.dateTimePickerTo.Visible = false;
            this.dateTimePickerTo.ValueChanged += new System.EventHandler(this.dateTimePickerTo_ValueChanged);
            // 
            // ScrapPartnumberList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.refreshLbl);
            this.Controls.Add(this.txtDisplayPageNo);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.truncateMasterMaterialLbl);
            this.Controls.Add(this.addScrapPartButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewScrapPartList);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "ScrapPartnumberList";
            this.Text = "Scrap Part";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScrapPartnumberList_FormClosing);
            this.Load += new System.EventHandler(this.ScrapPartnumberList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScrapPartList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewScrapPartList;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button addScrapPartButton;
        private System.Windows.Forms.LinkLabel truncateMasterMaterialLbl;
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        internal System.Windows.Forms.TextBox txtDisplayPageNo;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.LinkLabel refreshLbl;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.CheckBox allDate;
        private System.Windows.Forms.ComboBox cmbCust;
    }
}