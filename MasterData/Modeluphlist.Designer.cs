
namespace SMTPE
{
    partial class Modeluphlist
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
            this.dataGridViewModeluphlist = new System.Windows.Forms.DataGridView();
            this.toolStripUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTimeNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.refreshLbl = new System.Windows.Forms.LinkLabel();
            this.BackButton = new MaterialSkin.Controls.MaterialButton();
            this.addBtn = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModeluphlist)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.Location = new System.Drawing.Point(1138, 122);
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
            this.label1.Location = new System.Drawing.Point(1084, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Search";
            // 
            // dataGridViewModeluphlist
            // 
            this.dataGridViewModeluphlist.AllowUserToAddRows = false;
            this.dataGridViewModeluphlist.AllowUserToDeleteRows = false;
            this.dataGridViewModeluphlist.AllowUserToOrderColumns = true;
            this.dataGridViewModeluphlist.AllowUserToResizeRows = false;
            this.dataGridViewModeluphlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewModeluphlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewModeluphlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModeluphlist.Location = new System.Drawing.Point(23, 179);
            this.dataGridViewModeluphlist.Name = "dataGridViewModeluphlist";
            this.dataGridViewModeluphlist.RowHeadersWidth = 51;
            this.dataGridViewModeluphlist.Size = new System.Drawing.Size(1315, 494);
            this.dataGridViewModeluphlist.TabIndex = 10;
            this.dataGridViewModeluphlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewModellist_CellContentClick);
            this.dataGridViewModeluphlist.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewModellist_CellFormatting);
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
            this.statusStrip1.Size = new System.Drawing.Size(1360, 26);
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
            this.refreshLbl.Location = new System.Drawing.Point(1277, 157);
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(61, 19);
            this.refreshLbl.TabIndex = 107;
            this.refreshLbl.TabStop = true;
            this.refreshLbl.Text = "Refresh";
            this.refreshLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.refreshLbl_LinkClicked);
            // 
            // BackButton
            // 
            this.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BackButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BackButton.Depth = 0;
            this.BackButton.HighEmphasis = true;
            this.BackButton.Icon = global::SMTPE.Properties.Resources.icons8_reply_arrow_20;
            this.BackButton.Location = new System.Drawing.Point(1251, 76);
            this.BackButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.BackButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.BackButton.Name = "BackButton";
            this.BackButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BackButton.Size = new System.Drawing.Size(87, 36);
            this.BackButton.TabIndex = 265;
            this.BackButton.Text = "Back";
            this.BackButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BackButton.UseAccentColor = false;
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // addBtn
            // 
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.addBtn.Depth = 0;
            this.addBtn.HighEmphasis = true;
            this.addBtn.Icon = global::SMTPE.Properties.Resources.icons8_add_20__2_;
            this.addBtn.Location = new System.Drawing.Point(1163, 76);
            this.addBtn.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.addBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.addBtn.Name = "addBtn";
            this.addBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.addBtn.Size = new System.Drawing.Size(78, 36);
            this.addBtn.TabIndex = 267;
            this.addBtn.Text = "Add";
            this.addBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.addBtn.UseAccentColor = false;
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addmodel_Click);
            // 
            // Modeluphlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.refreshLbl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewModeluphlist);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "Modeluphlist";
            this.Text = "Model UPH List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Modelmasterlist_FormClosing);
            this.Load += new System.EventHandler(this.Modeluphlist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModeluphlist)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewModeluphlist;
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.LinkLabel refreshLbl;
        private MaterialSkin.Controls.MaterialButton BackButton;
        private MaterialSkin.Controls.MaterialButton addBtn;
    }
}