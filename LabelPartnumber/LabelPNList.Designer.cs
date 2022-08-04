
namespace SMTPE
{
    partial class LabelPNList
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
            this.dataGridViewLabelPNList = new System.Windows.Forms.DataGridView();
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.refreshLbl = new System.Windows.Forms.LinkLabel();
            this.txtDisplayPageNo = new System.Windows.Forms.TextBox();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.BackButton = new MaterialSkin.Controls.MaterialButton();
            this.exportButton = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLabelPNList)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
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
            // 
            // dataGridViewLabelPNList
            // 
            this.dataGridViewLabelPNList.AllowUserToAddRows = false;
            this.dataGridViewLabelPNList.AllowUserToDeleteRows = false;
            this.dataGridViewLabelPNList.AllowUserToOrderColumns = true;
            this.dataGridViewLabelPNList.AllowUserToResizeRows = false;
            this.dataGridViewLabelPNList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLabelPNList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLabelPNList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLabelPNList.Location = new System.Drawing.Point(23, 179);
            this.dataGridViewLabelPNList.Name = "dataGridViewLabelPNList";
            this.dataGridViewLabelPNList.ReadOnly = true;
            this.dataGridViewLabelPNList.RowHeadersWidth = 51;
            this.dataGridViewLabelPNList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLabelPNList.Size = new System.Drawing.Size(973, 494);
            this.dataGridViewLabelPNList.TabIndex = 10;
            this.dataGridViewLabelPNList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewLabelPNList_CellFormatting);
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
            this.statusStrip1.Location = new System.Drawing.Point(3, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1018, 26);
            this.statusStrip1.TabIndex = 59;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // refreshLbl
            // 
            this.refreshLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshLbl.AutoSize = true;
            this.refreshLbl.BackColor = System.Drawing.Color.Transparent;
            this.refreshLbl.Location = new System.Drawing.Point(935, 157);
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(61, 19);
            this.refreshLbl.TabIndex = 106;
            this.refreshLbl.TabStop = true;
            this.refreshLbl.Text = "Refresh";
            this.refreshLbl.Visible = false;
            // 
            // txtDisplayPageNo
            // 
            this.txtDisplayPageNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtDisplayPageNo.Location = new System.Drawing.Point(460, 693);
            this.txtDisplayPageNo.Name = "txtDisplayPageNo";
            this.txtDisplayPageNo.ReadOnly = true;
            this.txtDisplayPageNo.Size = new System.Drawing.Size(100, 26);
            this.txtDisplayPageNo.TabIndex = 195;
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNextPage.Location = new System.Drawing.Point(574, 692);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(75, 27);
            this.btnNextPage.TabIndex = 194;
            this.btnNextPage.Text = "Next >";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPreviousPage.Location = new System.Drawing.Point(373, 692);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(75, 27);
            this.btnPreviousPage.TabIndex = 193;
            this.btnPreviousPage.Text = "< Prev";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLastPage.Location = new System.Drawing.Point(655, 692);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(75, 27);
            this.btnLastPage.TabIndex = 192;
            this.btnLastPage.Text = "Last >>";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnFirstPage.Location = new System.Drawing.Point(292, 692);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(75, 27);
            this.btnFirstPage.TabIndex = 191;
            this.btnFirstPage.Text = "<< First";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // BackButton
            // 
            this.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BackButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BackButton.Depth = 0;
            this.BackButton.HighEmphasis = true;
            this.BackButton.Icon = global::SMTPE.Properties.Resources.icons8_reply_arrow_20;
            this.BackButton.Location = new System.Drawing.Point(909, 76);
            this.BackButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.BackButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.BackButton.Name = "BackButton";
            this.BackButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BackButton.Size = new System.Drawing.Size(87, 36);
            this.BackButton.TabIndex = 262;
            this.BackButton.Text = "Back";
            this.BackButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BackButton.UseAccentColor = false;
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exportButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.exportButton.Depth = 0;
            this.exportButton.HighEmphasis = true;
            this.exportButton.Icon = global::SMTPE.Properties.Resources.icons8_export_excel_20;
            this.exportButton.Location = new System.Drawing.Point(795, 76);
            this.exportButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.exportButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.exportButton.Name = "exportButton";
            this.exportButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.exportButton.Size = new System.Drawing.Size(104, 36);
            this.exportButton.TabIndex = 264;
            this.exportButton.Text = "Export";
            this.exportButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.exportButton.UseAccentColor = false;
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // LabelPNList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.txtDisplayPageNo);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.refreshLbl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewLabelPNList);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "LabelPNList";
            this.Sizable = false;
            this.Text = "Label Partnumber List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabelList_FormClosing);
            this.Load += new System.EventHandler(this.LabelList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLabelPNList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewLabelPNList;
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.LinkLabel refreshLbl;
        internal System.Windows.Forms.TextBox txtDisplayPageNo;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnFirstPage;
        private MaterialSkin.Controls.MaterialButton BackButton;
        private MaterialSkin.Controls.MaterialButton exportButton;
    }
}