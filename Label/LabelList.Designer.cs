namespace SMTPE{
    partial class LabelList
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
            this.dataGridViewLabelList = new System.Windows.Forms.DataGridView();
            this.tbTruncateSAPRef = new System.Windows.Forms.LinkLabel();
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.refreshLbl = new System.Windows.Forms.LinkLabel();
            this.importLabelListButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLabelList)).BeginInit();
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
            // dataGridViewLabelList
            // 
            this.dataGridViewLabelList.AllowUserToAddRows = false;
            this.dataGridViewLabelList.AllowUserToDeleteRows = false;
            this.dataGridViewLabelList.AllowUserToOrderColumns = true;
            this.dataGridViewLabelList.AllowUserToResizeRows = false;
            this.dataGridViewLabelList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLabelList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewLabelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLabelList.Location = new System.Drawing.Point(23, 179);
            this.dataGridViewLabelList.Name = "dataGridViewLabelList";
            this.dataGridViewLabelList.RowHeadersWidth = 51;
            this.dataGridViewLabelList.Size = new System.Drawing.Size(729, 400);
            this.dataGridViewLabelList.TabIndex = 10;
            this.dataGridViewLabelList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewLabelList_CellContentClick);
            this.dataGridViewLabelList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewLabelList_CellFormatting);
            // 
            // tbTruncateSAPRef
            // 
            this.tbTruncateSAPRef.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTruncateSAPRef.AutoSize = true;
            this.tbTruncateSAPRef.BackColor = System.Drawing.Color.Transparent;
            this.tbTruncateSAPRef.Location = new System.Drawing.Point(692, 158);
            this.tbTruncateSAPRef.Name = "tbTruncateSAPRef";
            this.tbTruncateSAPRef.Size = new System.Drawing.Size(73, 19);
            this.tbTruncateSAPRef.TabIndex = 61;
            this.tbTruncateSAPRef.TabStop = true;
            this.tbTruncateSAPRef.Text = "Delete All";
            this.tbTruncateSAPRef.Visible = false;
            this.tbTruncateSAPRef.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.tbTruncateSAPRef_LinkClicked);
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
            // refreshLbl
            // 
            this.refreshLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshLbl.AutoSize = true;
            this.refreshLbl.BackColor = System.Drawing.Color.Transparent;
            this.refreshLbl.Location = new System.Drawing.Point(625, 157);
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(61, 19);
            this.refreshLbl.TabIndex = 106;
            this.refreshLbl.TabStop = true;
            this.refreshLbl.Text = "Refresh";
            this.refreshLbl.Visible = false;
            // 
            // importLabelListButton
            // 
            this.importLabelListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.importLabelListButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.importLabelListButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.importLabelListButton.Image = global::SMTPE.Properties.Resources.icons8_import_file_20;
            this.importLabelListButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.importLabelListButton.Location = new System.Drawing.Point(514, 70);
            this.importLabelListButton.Name = "importLabelListButton";
            this.importLabelListButton.Size = new System.Drawing.Size(151, 43);
            this.importLabelListButton.TabIndex = 60;
            this.importLabelListButton.Text = "Import LabelList";
            this.importLabelListButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.importLabelListButton.UseVisualStyleBackColor = true;
            this.importLabelListButton.Click += new System.EventHandler(this.importLabelListButton_Click);
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
            this.backButton.TabIndex = 185;
            this.backButton.Text = "Back";
            this.backButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // LabelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 674);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.refreshLbl);
            this.Controls.Add(this.tbTruncateSAPRef);
            this.Controls.Add(this.importLabelListButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewLabelList);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "LabelList";
            this.Text = "AOHAI Label Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabelList_FormClosing);
            this.Load += new System.EventHandler(this.LabelList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLabelList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewLabelList;
        private System.Windows.Forms.Button importLabelListButton;
        private System.Windows.Forms.LinkLabel tbTruncateSAPRef;
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.LinkLabel refreshLbl;
        private System.Windows.Forms.Button backButton;
    }
}