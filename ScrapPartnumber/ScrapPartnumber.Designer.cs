
namespace SMTPE
{ 
    partial class ScrapPartnumber
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
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tbpnSN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNew = new System.Windows.Forms.Button();
            this.tbPrint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRequestBy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.tbPrfNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbscrapQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewPRFList = new System.Windows.Forms.DataGridView();
            this.backButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPRFList)).BeginInit();
            this.SuspendLayout();
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
            // tbpnSN
            // 
            this.tbpnSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbpnSN.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpnSN.Location = new System.Drawing.Point(243, 187);
            this.tbpnSN.MaxLength = 60;
            this.tbpnSN.Name = "tbpnSN";
            this.tbpnSN.Size = new System.Drawing.Size(384, 35);
            this.tbpnSN.TabIndex = 198;
            this.tbpnSN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbpnSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbpnSN_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 27);
            this.label2.TabIndex = 199;
            this.label2.Text = "Part No SN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbNew);
            this.groupBox1.Controls.Add(this.tbPrint);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbRequestBy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbDepartment);
            this.groupBox1.Controls.Add(this.tbPrfNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbscrapQty);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbpnSN);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(32, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(973, 291);
            this.groupBox1.TabIndex = 200;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scrap Part";
            // 
            // tbNew
            // 
            this.tbNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbNew.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbNew.Image = global::SMTPE.Properties.Resources.icons8_add_20__2_;
            this.tbNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbNew.Location = new System.Drawing.Point(790, 232);
            this.tbNew.Margin = new System.Windows.Forms.Padding(4);
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(152, 43);
            this.tbNew.TabIndex = 208;
            this.tbNew.Text = "New Transaction";
            this.tbNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tbNew.UseVisualStyleBackColor = true;
            this.tbNew.Click += new System.EventHandler(this.tbNew_Click);
            // 
            // tbPrint
            // 
            this.tbPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbPrint.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbPrint.Image = global::SMTPE.Properties.Resources.icons8_print_20;
            this.tbPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbPrint.Location = new System.Drawing.Point(664, 232);
            this.tbPrint.Margin = new System.Windows.Forms.Padding(4);
            this.tbPrint.Name = "tbPrint";
            this.tbPrint.Size = new System.Drawing.Size(114, 43);
            this.tbPrint.TabIndex = 202;
            this.tbPrint.Text = "Print Label";
            this.tbPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tbPrint.UseVisualStyleBackColor = true;
            this.tbPrint.Click += new System.EventHandler(this.tbPrint_Click);
            this.tbPrint.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPrint_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 27);
            this.label5.TabIndex = 207;
            this.label5.Text = "Requested By";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbRequestBy
            // 
            this.cmbRequestBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbRequestBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbRequestBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequestBy.Font = new System.Drawing.Font("Open Sans", 12F);
            this.cmbRequestBy.FormattingEnabled = true;
            this.cmbRequestBy.IntegralHeight = false;
            this.cmbRequestBy.Location = new System.Drawing.Point(243, 135);
            this.cmbRequestBy.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRequestBy.MaxDropDownItems = 10;
            this.cmbRequestBy.Name = "cmbRequestBy";
            this.cmbRequestBy.Size = new System.Drawing.Size(384, 35);
            this.cmbRequestBy.TabIndex = 206;
            this.cmbRequestBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbRequestBy_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 27);
            this.label4.TabIndex = 205;
            this.label4.Text = "Department";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDepartment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.Font = new System.Drawing.Font("Open Sans", 12F);
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.IntegralHeight = false;
            this.cmbDepartment.Location = new System.Drawing.Point(243, 85);
            this.cmbDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDepartment.MaxDropDownItems = 10;
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(384, 35);
            this.cmbDepartment.TabIndex = 204;
            this.cmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDepartment_KeyDown);
            // 
            // tbPrfNo
            // 
            this.tbPrfNo.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPrfNo.Location = new System.Drawing.Point(243, 31);
            this.tbPrfNo.MaxLength = 7;
            this.tbPrfNo.Name = "tbPrfNo";
            this.tbPrfNo.Size = new System.Drawing.Size(384, 35);
            this.tbPrfNo.TabIndex = 203;
            this.tbPrfNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbPrfNo.TextChanged += new System.EventHandler(this.tbPrfNo_TextChanged);
            this.tbPrfNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPrfNo_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 27);
            this.label3.TabIndex = 202;
            this.label3.Text = "PRF No";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbscrapQty
            // 
            this.tbscrapQty.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbscrapQty.Location = new System.Drawing.Point(243, 236);
            this.tbscrapQty.MaxLength = 10;
            this.tbscrapQty.Name = "tbscrapQty";
            this.tbscrapQty.Size = new System.Drawing.Size(384, 35);
            this.tbscrapQty.TabIndex = 201;
            this.tbscrapQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbscrapQty.TextChanged += new System.EventHandler(this.tbscrapQty_TextChanged);
            this.tbscrapQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbscrapQty_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 27);
            this.label1.TabIndex = 200;
            this.label1.Text = "Scrap QTY";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGridViewPRFList
            // 
            this.dataGridViewPRFList.AllowUserToAddRows = false;
            this.dataGridViewPRFList.AllowUserToDeleteRows = false;
            this.dataGridViewPRFList.AllowUserToOrderColumns = true;
            this.dataGridViewPRFList.AllowUserToResizeRows = false;
            this.dataGridViewPRFList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPRFList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPRFList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPRFList.Location = new System.Drawing.Point(32, 440);
            this.dataGridViewPRFList.Name = "dataGridViewPRFList";
            this.dataGridViewPRFList.ReadOnly = true;
            this.dataGridViewPRFList.RowHeadersWidth = 51;
            this.dataGridViewPRFList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPRFList.Size = new System.Drawing.Size(973, 292);
            this.dataGridViewPRFList.TabIndex = 201;
            this.dataGridViewPRFList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewPRFList_CellFormatting);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.backButton.Image = global::SMTPE.Properties.Resources.icons8_reply_arrow_20;
            this.backButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.backButton.Location = new System.Drawing.Point(916, 70);
            this.backButton.Margin = new System.Windows.Forms.Padding(4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(80, 43);
            this.backButton.TabIndex = 186;
            this.backButton.Text = "Back";
            this.backButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // ScrapPartnumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.dataGridViewPRFList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "ScrapPartnumber";
            this.Sizable = false;
            this.Text = "Scrap Partnumber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScrapPartnumber_FormClosing);
            this.Load += new System.EventHandler(this.LabelPartnumber_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPRFList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox tbpnSN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbscrapQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrfNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRequestBy;
        private System.Windows.Forms.DataGridView dataGridViewPRFList;
        private System.Windows.Forms.Button tbPrint;
        private System.Windows.Forms.Button tbNew;
    }
}