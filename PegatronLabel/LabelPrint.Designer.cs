namespace SMTPE
{
    partial class LabelPrint
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
            this.BackButton = new MaterialSkin.Controls.MaterialButton();
            this.cmbType = new MaterialSkin.Controls.MaterialComboBox();
            this.selectBtn = new MaterialSkin.Controls.MaterialButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // BackButton
            // 
            this.BackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BackButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BackButton.Depth = 0;
            this.BackButton.HighEmphasis = true;
            this.BackButton.Icon = null;
            this.BackButton.Location = new System.Drawing.Point(932, 74);
            this.BackButton.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.BackButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.BackButton.Name = "BackButton";
            this.BackButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BackButton.Size = new System.Drawing.Size(64, 36);
            this.BackButton.TabIndex = 265;
            this.BackButton.Text = "Back";
            this.BackButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BackButton.UseAccentColor = false;
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.AutoResize = false;
            this.cmbType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbType.Depth = 0;
            this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbType.DropDownHeight = 174;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.DropDownWidth = 121;
            this.cmbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.IntegralHeight = false;
            this.cmbType.ItemHeight = 43;
            this.cmbType.Items.AddRange(new object[] {
            "PCB Single"});
            this.cmbType.Location = new System.Drawing.Point(79, 60);
            this.cmbType.MaxDropDownItems = 4;
            this.cmbType.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(338, 49);
            this.cmbType.StartIndex = 0;
            this.cmbType.TabIndex = 268;
            // 
            // selectBtn
            // 
            this.selectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBtn.AutoSize = false;
            this.selectBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.selectBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.selectBtn.Depth = 0;
            this.selectBtn.HighEmphasis = true;
            this.selectBtn.Icon = null;
            this.selectBtn.Location = new System.Drawing.Point(425, 60);
            this.selectBtn.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.selectBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.selectBtn.Name = "selectBtn";
            this.selectBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.selectBtn.Size = new System.Drawing.Size(132, 49);
            this.selectBtn.TabIndex = 269;
            this.selectBtn.Text = "Select";
            this.selectBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.selectBtn.UseAccentColor = false;
            this.selectBtn.UseVisualStyleBackColor = true;
            this.selectBtn.Click += new System.EventHandler(this.selectBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.selectBtn);
            this.panel1.Controls.Add(this.cmbType);
            this.panel1.Location = new System.Drawing.Point(209, 321);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 182);
            this.panel1.TabIndex = 270;
            // 
            // LabelPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "LabelPrint";
            this.Text = " Label Print";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LabelPrint_FormClosing);
            this.Load += new System.EventHandler(this.PegatronLabelList_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ToolStripStatusLabel toolStripUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel dateTimeNow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer timer;
        private MaterialSkin.Controls.MaterialButton BackButton;
        private MaterialSkin.Controls.MaterialComboBox cmbType;
        private MaterialSkin.Controls.MaterialButton selectBtn;
        private System.Windows.Forms.Panel panel1;
    }
}