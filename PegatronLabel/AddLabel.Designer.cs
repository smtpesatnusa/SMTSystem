
namespace SMTPE
{
    partial class AddLabel
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
            this.saveBtn = new MaterialSkin.Controls.MaterialButton();
            this.userdetail = new System.Windows.Forms.Label();
            this.sequencetb = new MaterialSkin.Controls.MaterialTextBox();
            this.runningNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.modeltb = new MaterialSkin.Controls.MaterialTextBox();
            this.cmbWO = new System.Windows.Forms.ComboBox();
            this.remarktb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.AutoSize = false;
            this.saveBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.saveBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.saveBtn.Depth = 0;
            this.saveBtn.HighEmphasis = true;
            this.saveBtn.Icon = null;
            this.saveBtn.Location = new System.Drawing.Point(195, 429);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.saveBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveBtn.Size = new System.Drawing.Size(126, 44);
            this.saveBtn.TabIndex = 267;
            this.saveBtn.Text = "Print";
            this.saveBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.saveBtn.UseAccentColor = false;
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // userdetail
            // 
            this.userdetail.AutoSize = true;
            this.userdetail.BackColor = System.Drawing.Color.Transparent;
            this.userdetail.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.userdetail.Location = new System.Drawing.Point(64, 454);
            this.userdetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userdetail.Name = "userdetail";
            this.userdetail.Size = new System.Drawing.Size(77, 19);
            this.userdetail.TabIndex = 261;
            this.userdetail.Text = "username";
            this.userdetail.Visible = false;
            // 
            // sequencetb
            // 
            this.sequencetb.AnimateReadOnly = false;
            this.sequencetb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sequencetb.Depth = 0;
            this.sequencetb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sequencetb.Hint = "Sequence";
            this.sequencetb.LeadingIcon = null;
            this.sequencetb.Location = new System.Drawing.Point(44, 93);
            this.sequencetb.MaxLength = 2;
            this.sequencetb.MouseState = MaterialSkin.MouseState.OUT;
            this.sequencetb.Multiline = false;
            this.sequencetb.Name = "sequencetb";
            this.sequencetb.Size = new System.Drawing.Size(428, 50);
            this.sequencetb.TabIndex = 262;
            this.sequencetb.Text = "";
            this.sequencetb.TrailingIcon = null;
            this.sequencetb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sequencetb_KeyPress);
            // 
            // runningNumbertb
            // 
            this.runningNumbertb.AnimateReadOnly = false;
            this.runningNumbertb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.runningNumbertb.Depth = 0;
            this.runningNumbertb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.runningNumbertb.Hint = "Running Number";
            this.runningNumbertb.LeadingIcon = null;
            this.runningNumbertb.Location = new System.Drawing.Point(44, 205);
            this.runningNumbertb.MaxLength = 7;
            this.runningNumbertb.MouseState = MaterialSkin.MouseState.OUT;
            this.runningNumbertb.Multiline = false;
            this.runningNumbertb.Name = "runningNumbertb";
            this.runningNumbertb.Size = new System.Drawing.Size(428, 50);
            this.runningNumbertb.TabIndex = 264;
            this.runningNumbertb.Text = "";
            this.runningNumbertb.TrailingIcon = null;
            this.runningNumbertb.TextChanged += new System.EventHandler(this.runningNumbertb_TextChanged);
            this.runningNumbertb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.runningNumbertb_KeyPress);
            // 
            // modeltb
            // 
            this.modeltb.AnimateReadOnly = false;
            this.modeltb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modeltb.Depth = 0;
            this.modeltb.Enabled = false;
            this.modeltb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.modeltb.Hint = "Model";
            this.modeltb.LeadingIcon = null;
            this.modeltb.Location = new System.Drawing.Point(44, 261);
            this.modeltb.MaxLength = 12;
            this.modeltb.MouseState = MaterialSkin.MouseState.OUT;
            this.modeltb.Multiline = false;
            this.modeltb.Name = "modeltb";
            this.modeltb.Size = new System.Drawing.Size(428, 50);
            this.modeltb.TabIndex = 265;
            this.modeltb.Text = "";
            this.modeltb.TrailingIcon = null;
            this.modeltb.Leave += new System.EventHandler(this.modeltb_Leave);
            // 
            // cmbWO
            // 
            this.cmbWO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbWO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbWO.DropDownHeight = 150;
            this.cmbWO.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWO.FormattingEnabled = true;
            this.cmbWO.IntegralHeight = false;
            this.cmbWO.ItemHeight = 31;
            this.cmbWO.Location = new System.Drawing.Point(44, 155);
            this.cmbWO.Name = "cmbWO";
            this.cmbWO.Size = new System.Drawing.Size(428, 39);
            this.cmbWO.TabIndex = 263;
            this.cmbWO.SelectedIndexChanged += new System.EventHandler(this.cmbWO_SelectedIndexChanged);
            // 
            // remarktb
            // 
            this.remarktb.Location = new System.Drawing.Point(44, 338);
            this.remarktb.Multiline = true;
            this.remarktb.Name = "remarktb";
            this.remarktb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.remarktb.Size = new System.Drawing.Size(428, 70);
            this.remarktb.TabIndex = 266;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.label1.Location = new System.Drawing.Point(40, 316);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 19);
            this.label1.TabIndex = 267;
            this.label1.Text = "Remark";
            // 
            // AddLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 501);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remarktb);
            this.Controls.Add(this.cmbWO);
            this.Controls.Add(this.modeltb);
            this.Controls.Add(this.runningNumbertb);
            this.Controls.Add(this.sequencetb);
            this.Controls.Add(this.userdetail);
            this.Controls.Add(this.saveBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLabel";
            this.Padding = new System.Windows.Forms.Padding(2, 52, 2, 2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pegatron Label";
            this.Load += new System.EventHandler(this.PrintLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialButton saveBtn;
        public System.Windows.Forms.Label userdetail;
        private MaterialSkin.Controls.MaterialTextBox sequencetb;
        private MaterialSkin.Controls.MaterialTextBox runningNumbertb;
        private MaterialSkin.Controls.MaterialTextBox modeltb;
        private System.Windows.Forms.ComboBox cmbWO;
        private System.Windows.Forms.TextBox remarktb;
        public System.Windows.Forms.Label label1;
    }
}

