
namespace SMTPE
{
    partial class DetailForecastList
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
            this.dataGridViewPlDetail = new System.Windows.Forms.DataGridView();
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.backButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.planBtn = new System.Windows.Forms.Button();
            this.tbDestination = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPortLoading = new System.Windows.Forms.TextBox();
            this.tbPaymentTerm = new System.Windows.Forms.TextBox();
            this.tbInvoiceDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbIncoterms = new System.Windows.Forms.TextBox();
            this.tbShipTerm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPackingListNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.totalLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewInbound = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlDetail)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInbound)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPlDetail
            // 
            this.dataGridViewPlDetail.AllowUserToAddRows = false;
            this.dataGridViewPlDetail.AllowUserToDeleteRows = false;
            this.dataGridViewPlDetail.AllowUserToResizeRows = false;
            this.dataGridViewPlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPlDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewPlDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlDetail.Location = new System.Drawing.Point(16, 422);
            this.dataGridViewPlDetail.Name = "dataGridViewPlDetail";
            this.dataGridViewPlDetail.ReadOnly = true;
            this.dataGridViewPlDetail.RowHeadersWidth = 51;
            this.dataGridViewPlDetail.RowTemplate.Height = 30;
            this.dataGridViewPlDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPlDetail.Size = new System.Drawing.Size(1334, 299);
            this.dataGridViewPlDetail.TabIndex = 10;
            this.dataGridViewPlDetail.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewPlDetail_CellFormatting);
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
            this.statusStrip1.Size = new System.Drawing.Size(1366, 26);
            this.statusStrip1.TabIndex = 59;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Image = global::SMTPE.Properties.Resources.icons8_reply_arrow_20;
            this.backButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.backButton.Location = new System.Drawing.Point(1262, 70);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(76, 40);
            this.backButton.TabIndex = 58;
            this.backButton.Text = "Back";
            this.backButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.planBtn);
            this.groupBox1.Controls.Add(this.tbDestination);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbPortLoading);
            this.groupBox1.Controls.Add(this.tbPaymentTerm);
            this.groupBox1.Controls.Add(this.tbInvoiceDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbIncoterms);
            this.groupBox1.Controls.Add(this.tbShipTerm);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbPackingListNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.totalLbl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 145);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1334, 256);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packing List";
            // 
            // planBtn
            // 
            this.planBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.planBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.planBtn.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.planBtn.Image = global::SMTPE.Properties.Resources.icons8_export_excel_20;
            this.planBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.planBtn.Location = new System.Drawing.Point(1195, 27);
            this.planBtn.Margin = new System.Windows.Forms.Padding(4);
            this.planBtn.Name = "planBtn";
            this.planBtn.Size = new System.Drawing.Size(80, 43);
            this.planBtn.TabIndex = 194;
            this.planBtn.Text = "Export";
            this.planBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.planBtn.UseVisualStyleBackColor = true;
            this.planBtn.Click += new System.EventHandler(this.planBtn_Click);
            // 
            // tbDestination
            // 
            this.tbDestination.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDestination.Location = new System.Drawing.Point(835, 187);
            this.tbDestination.Margin = new System.Windows.Forms.Padding(4);
            this.tbDestination.Name = "tbDestination";
            this.tbDestination.ReadOnly = true;
            this.tbDestination.Size = new System.Drawing.Size(440, 26);
            this.tbDestination.TabIndex = 54;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(682, 190);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 19);
            this.label9.TabIndex = 53;
            this.label9.Text = "Destination";
            // 
            // tbPortLoading
            // 
            this.tbPortLoading.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPortLoading.Location = new System.Drawing.Point(835, 136);
            this.tbPortLoading.Margin = new System.Windows.Forms.Padding(4);
            this.tbPortLoading.Name = "tbPortLoading";
            this.tbPortLoading.ReadOnly = true;
            this.tbPortLoading.Size = new System.Drawing.Size(440, 26);
            this.tbPortLoading.TabIndex = 52;
            // 
            // tbPaymentTerm
            // 
            this.tbPaymentTerm.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPaymentTerm.Location = new System.Drawing.Point(188, 186);
            this.tbPaymentTerm.Margin = new System.Windows.Forms.Padding(4);
            this.tbPaymentTerm.Name = "tbPaymentTerm";
            this.tbPaymentTerm.ReadOnly = true;
            this.tbPaymentTerm.Size = new System.Drawing.Size(440, 26);
            this.tbPaymentTerm.TabIndex = 51;
            // 
            // tbInvoiceDate
            // 
            this.tbInvoiceDate.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInvoiceDate.Location = new System.Drawing.Point(835, 87);
            this.tbInvoiceDate.Margin = new System.Windows.Forms.Padding(4);
            this.tbInvoiceDate.Name = "tbInvoiceDate";
            this.tbInvoiceDate.ReadOnly = true;
            this.tbInvoiceDate.Size = new System.Drawing.Size(440, 26);
            this.tbInvoiceDate.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(682, 90);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 19);
            this.label3.TabIndex = 42;
            this.label3.Text = "Invoice Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(682, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 19);
            this.label7.TabIndex = 50;
            this.label7.Text = "Port of  Loading";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(35, 186);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 19);
            this.label8.TabIndex = 49;
            this.label8.Text = "Payment Term";
            // 
            // tbIncoterms
            // 
            this.tbIncoterms.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIncoterms.Location = new System.Drawing.Point(188, 135);
            this.tbIncoterms.Margin = new System.Windows.Forms.Padding(4);
            this.tbIncoterms.Name = "tbIncoterms";
            this.tbIncoterms.ReadOnly = true;
            this.tbIncoterms.Size = new System.Drawing.Size(440, 26);
            this.tbIncoterms.TabIndex = 48;
            // 
            // tbShipTerm
            // 
            this.tbShipTerm.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbShipTerm.Location = new System.Drawing.Point(188, 84);
            this.tbShipTerm.Margin = new System.Windows.Forms.Padding(4);
            this.tbShipTerm.Name = "tbShipTerm";
            this.tbShipTerm.ReadOnly = true;
            this.tbShipTerm.Size = new System.Drawing.Size(440, 26);
            this.tbShipTerm.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 138);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 19);
            this.label4.TabIndex = 46;
            this.label4.Text = "Incoterms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 84);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 19);
            this.label5.TabIndex = 45;
            this.label5.Text = "Ship term";
            // 
            // tbPackingListNo
            // 
            this.tbPackingListNo.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPackingListNo.Location = new System.Drawing.Point(188, 42);
            this.tbPackingListNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbPackingListNo.Name = "tbPackingListNo";
            this.tbPackingListNo.ReadOnly = true;
            this.tbPackingListNo.Size = new System.Drawing.Size(440, 26);
            this.tbPackingListNo.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 19);
            this.label2.TabIndex = 41;
            this.label2.Text = "Packinglist NO.";
            // 
            // totalLbl
            // 
            this.totalLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalLbl.AutoSize = true;
            this.totalLbl.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLbl.Location = new System.Drawing.Point(1297, 222);
            this.totalLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.totalLbl.Name = "totalLbl";
            this.totalLbl.Size = new System.Drawing.Size(14, 19);
            this.totalLbl.TabIndex = 40;
            this.totalLbl.Text = "-";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1178, 222);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 39;
            this.label1.Text = "Total Data :";
            // 
            // dataGridViewInbound
            // 
            this.dataGridViewInbound.AllowUserToAddRows = false;
            this.dataGridViewInbound.AllowUserToDeleteRows = false;
            this.dataGridViewInbound.AllowUserToResizeRows = false;
            this.dataGridViewInbound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewInbound.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewInbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInbound.Location = new System.Drawing.Point(16, 422);
            this.dataGridViewInbound.Name = "dataGridViewInbound";
            this.dataGridViewInbound.ReadOnly = true;
            this.dataGridViewInbound.RowHeadersWidth = 51;
            this.dataGridViewInbound.RowTemplate.Height = 30;
            this.dataGridViewInbound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInbound.Size = new System.Drawing.Size(1334, 238);
            this.dataGridViewInbound.TabIndex = 61;
            this.dataGridViewInbound.Visible = false;
            // 
            // DetailPackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.dataGridViewPlDetail);
            this.Controls.Add(this.dataGridViewInbound);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "DetailPackingList";
            this.Text = "Detail Packing List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PackingList_FormClosing);
            this.Load += new System.EventHandler(this.PackingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlDetail)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInbound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button backButton;
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label totalLbl;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tbDestination;
        public System.Windows.Forms.TextBox tbPortLoading;
        public System.Windows.Forms.TextBox tbPaymentTerm;
        public System.Windows.Forms.TextBox tbIncoterms;
        public System.Windows.Forms.TextBox tbShipTerm;
        public System.Windows.Forms.TextBox tbInvoiceDate;
        public System.Windows.Forms.TextBox tbPackingListNo;
        private System.Windows.Forms.Button planBtn;
        private System.Windows.Forms.DataGridView dataGridViewPlDetail;
        private System.Windows.Forms.DataGridView dataGridViewInbound;
    }
}