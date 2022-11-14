
namespace SMTPE
{
    partial class PegatronLabel
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
            this.printBtn = new MaterialSkin.Controls.MaterialButton();
            this.userdetail = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.levelUser = new System.Windows.Forms.Label();
            this.woNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.sequencetb = new MaterialSkin.Controls.MaterialTextBox();
            this.runningNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.modeltb = new MaterialSkin.Controls.MaterialTextBox();
            this.qtytb = new MaterialSkin.Controls.MaterialTextBox();
            this.SuspendLayout();
            // 
            // printBtn
            // 
            this.printBtn.AutoSize = false;
            this.printBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.printBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.printBtn.Depth = 0;
            this.printBtn.HighEmphasis = true;
            this.printBtn.Icon = null;
            this.printBtn.Location = new System.Drawing.Point(177, 456);
            this.printBtn.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.printBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.printBtn.Name = "printBtn";
            this.printBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.printBtn.Size = new System.Drawing.Size(243, 44);
            this.printBtn.TabIndex = 287;
            this.printBtn.Text = "Print";
            this.printBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.printBtn.UseAccentColor = false;
            this.printBtn.UseVisualStyleBackColor = true;
            this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
            // 
            // userdetail
            // 
            this.userdetail.AutoSize = true;
            this.userdetail.BackColor = System.Drawing.Color.Transparent;
            this.userdetail.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.userdetail.Location = new System.Drawing.Point(29, 470);
            this.userdetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userdetail.Name = "userdetail";
            this.userdetail.Size = new System.Drawing.Size(77, 19);
            this.userdetail.TabIndex = 261;
            this.userdetail.Text = "username";
            this.userdetail.Visible = false;
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F);
            this.idLabel.Location = new System.Drawing.Point(29, 70);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(26, 20);
            this.idLabel.TabIndex = 288;
            this.idLabel.Text = "ID";
            this.idLabel.Visible = false;
            // 
            // levelUser
            // 
            this.levelUser.AutoSize = true;
            this.levelUser.BackColor = System.Drawing.Color.Transparent;
            this.levelUser.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.levelUser.Location = new System.Drawing.Point(29, 506);
            this.levelUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.levelUser.Name = "levelUser";
            this.levelUser.Size = new System.Drawing.Size(40, 19);
            this.levelUser.TabIndex = 290;
            this.levelUser.Text = "level";
            this.levelUser.Visible = false;
            // 
            // woNumbertb
            // 
            this.woNumbertb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.woNumbertb.AnimateReadOnly = false;
            this.woNumbertb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.woNumbertb.Depth = 0;
            this.woNumbertb.Enabled = false;
            this.woNumbertb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.woNumbertb.Hint = "WO Number";
            this.woNumbertb.LeadingIcon = null;
            this.woNumbertb.Location = new System.Drawing.Point(33, 180);
            this.woNumbertb.MaxLength = 50;
            this.woNumbertb.MouseState = MaterialSkin.MouseState.OUT;
            this.woNumbertb.Multiline = false;
            this.woNumbertb.Name = "woNumbertb";
            this.woNumbertb.Size = new System.Drawing.Size(412, 50);
            this.woNumbertb.TabIndex = 293;
            this.woNumbertb.Text = "";
            this.woNumbertb.TrailingIcon = null;
            // 
            // sequencetb
            // 
            this.sequencetb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sequencetb.AnimateReadOnly = false;
            this.sequencetb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sequencetb.Depth = 0;
            this.sequencetb.Enabled = false;
            this.sequencetb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sequencetb.Hint = "Sequence";
            this.sequencetb.LeadingIcon = null;
            this.sequencetb.Location = new System.Drawing.Point(33, 110);
            this.sequencetb.MaxLength = 50;
            this.sequencetb.MouseState = MaterialSkin.MouseState.OUT;
            this.sequencetb.Multiline = false;
            this.sequencetb.Name = "sequencetb";
            this.sequencetb.Size = new System.Drawing.Size(412, 50);
            this.sequencetb.TabIndex = 292;
            this.sequencetb.Text = "";
            this.sequencetb.TrailingIcon = null;
            // 
            // runningNumbertb
            // 
            this.runningNumbertb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runningNumbertb.AnimateReadOnly = false;
            this.runningNumbertb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.runningNumbertb.Depth = 0;
            this.runningNumbertb.Enabled = false;
            this.runningNumbertb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.runningNumbertb.Hint = "Running Number";
            this.runningNumbertb.LeadingIcon = null;
            this.runningNumbertb.Location = new System.Drawing.Point(33, 250);
            this.runningNumbertb.MaxLength = 50;
            this.runningNumbertb.MouseState = MaterialSkin.MouseState.OUT;
            this.runningNumbertb.Multiline = false;
            this.runningNumbertb.Name = "runningNumbertb";
            this.runningNumbertb.Size = new System.Drawing.Size(412, 50);
            this.runningNumbertb.TabIndex = 294;
            this.runningNumbertb.Text = "";
            this.runningNumbertb.TrailingIcon = null;
            // 
            // modeltb
            // 
            this.modeltb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modeltb.AnimateReadOnly = false;
            this.modeltb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modeltb.Depth = 0;
            this.modeltb.Enabled = false;
            this.modeltb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.modeltb.Hint = "Model";
            this.modeltb.LeadingIcon = null;
            this.modeltb.Location = new System.Drawing.Point(33, 317);
            this.modeltb.MaxLength = 50;
            this.modeltb.MouseState = MaterialSkin.MouseState.OUT;
            this.modeltb.Multiline = false;
            this.modeltb.Name = "modeltb";
            this.modeltb.Size = new System.Drawing.Size(412, 50);
            this.modeltb.TabIndex = 295;
            this.modeltb.Text = "";
            this.modeltb.TrailingIcon = null;
            // 
            // qtytb
            // 
            this.qtytb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qtytb.AnimateReadOnly = false;
            this.qtytb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.qtytb.Depth = 0;
            this.qtytb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.qtytb.Hint = "Qty Print";
            this.qtytb.LeadingIcon = null;
            this.qtytb.Location = new System.Drawing.Point(33, 387);
            this.qtytb.MaxLength = 5;
            this.qtytb.MouseState = MaterialSkin.MouseState.OUT;
            this.qtytb.Multiline = false;
            this.qtytb.Name = "qtytb";
            this.qtytb.Size = new System.Drawing.Size(412, 50);
            this.qtytb.TabIndex = 296;
            this.qtytb.Text = "";
            this.qtytb.TrailingIcon = null;
            this.qtytb.TextChanged += new System.EventHandler(this.qtytb_TextChanged);
            // 
            // PegatronLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 561);
            this.Controls.Add(this.qtytb);
            this.Controls.Add(this.modeltb);
            this.Controls.Add(this.runningNumbertb);
            this.Controls.Add(this.woNumbertb);
            this.Controls.Add(this.sequencetb);
            this.Controls.Add(this.levelUser);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.userdetail);
            this.Controls.Add(this.printBtn);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PegatronLabel";
            this.Padding = new System.Windows.Forms.Padding(2, 52, 2, 2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Pegatron Label";
            this.Load += new System.EventHandler(this.PrintLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialButton printBtn;
        public System.Windows.Forms.Label userdetail;
        public System.Windows.Forms.Label idLabel;
        public System.Windows.Forms.Label levelUser;
        private MaterialSkin.Controls.MaterialTextBox woNumbertb;
        private MaterialSkin.Controls.MaterialTextBox sequencetb;
        private MaterialSkin.Controls.MaterialTextBox runningNumbertb;
        private MaterialSkin.Controls.MaterialTextBox modeltb;
        private MaterialSkin.Controls.MaterialTextBox qtytb;
    }
}

