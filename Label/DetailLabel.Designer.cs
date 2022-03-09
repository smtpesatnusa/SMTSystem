namespace SMTPE{
    partial class DetailLabel
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
            this.dataGridViewDetailLabel = new System.Windows.Forms.DataGridView();
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
            this.po_No = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.refreshLbl = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetailLabel)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.Location = new System.Drawing.Point(552, 122);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(200, 26);
            this.tbSearch.TabIndex = 13;
            this.tbSearch.Visible = false;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(498, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Search";
            this.label1.Visible = false;
            // 
            // dataGridViewDetailLabel
            // 
            this.dataGridViewDetailLabel.AllowUserToAddRows = false;
            this.dataGridViewDetailLabel.AllowUserToDeleteRows = false;
            this.dataGridViewDetailLabel.AllowUserToOrderColumns = true;
            this.dataGridViewDetailLabel.AllowUserToResizeRows = false;
            this.dataGridViewDetailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDetailLabel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDetailLabel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetailLabel.Location = new System.Drawing.Point(23, 179);
            this.dataGridViewDetailLabel.Name = "dataGridViewDetailLabel";
            this.dataGridViewDetailLabel.RowHeadersWidth = 51;
            this.dataGridViewDetailLabel.Size = new System.Drawing.Size(729, 400);
            this.dataGridViewDetailLabel.TabIndex = 10;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 648);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 26);
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
            this.txtDisplayPageNo.Location = new System.Drawing.Point(327, 600);
            this.txtDisplayPageNo.Name = "txtDisplayPageNo";
            this.txtDisplayPageNo.ReadOnly = true;
            this.txtDisplayPageNo.Size = new System.Drawing.Size(100, 26);
            this.txtDisplayPageNo.TabIndex = 105;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNextPage.Location = new System.Drawing.Point(441, 599);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 23);
            this.btnNextPage.TabIndex = 104;
            this.btnNextPage.Text = "Next >";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPreviousPage.Location = new System.Drawing.Point(240, 599);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(75, 23);
            this.btnPreviousPage.TabIndex = 103;
            this.btnPreviousPage.Text = "< Prev";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLastPage.Location = new System.Drawing.Point(522, 599);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(75, 23);
            this.btnLastPage.TabIndex = 102;
            this.btnLastPage.Text = "Last >>";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFirstPage.Location = new System.Drawing.Point(159, 599);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(75, 23);
            this.btnFirstPage.TabIndex = 101;
            this.btnFirstPage.Text = "<< First";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // po_No
            // 
            this.po_No.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.po_No.Location = new System.Drawing.Point(130, 127);
            this.po_No.Name = "po_No";
            this.po_No.ReadOnly = true;
            this.po_No.Size = new System.Drawing.Size(200, 26);
            this.po_No.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 27);
            this.label2.TabIndex = 107;
            this.label2.Text = "PO NO";
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.backButton.Image = global::SMTPE.Properties.Resources.icons8_reply_arrow_20;
            this.backButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.backButton.Location = new System.Drawing.Point(672, 70);
            this.backButton.Margin = new System.Windows.Forms.Padding(4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(80, 43);
            this.backButton.TabIndex = 186;
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
            this.refreshLbl.Location = new System.Drawing.Point(691, 157);
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(61, 19);
            this.refreshLbl.TabIndex = 187;
            this.refreshLbl.TabStop = true;
            this.refreshLbl.Text = "Refresh";
            this.refreshLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.refreshLbl_LinkClicked);
            // 
            // DetailLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 674);
            this.Controls.Add(this.refreshLbl);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.po_No);
            this.Controls.Add(this.txtDisplayPageNo);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewDetailLabel);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "DetailLabel";
            this.Text = "Detail Label Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DetailLabel_FormClosing);
            this.Load += new System.EventHandler(this.LabelList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetailLabel)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label1;
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
        public System.Windows.Forms.TextBox po_No;
        private System.Windows.Forms.DataGridView dataGridViewDetailLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.LinkLabel refreshLbl;
    }
}