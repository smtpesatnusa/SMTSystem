
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
            this.woNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.runningNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.modeltb = new MaterialSkin.Controls.MaterialTextBox();
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
            this.saveBtn.Location = new System.Drawing.Point(183, 344);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.saveBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.saveBtn.Size = new System.Drawing.Size(126, 44);
            this.saveBtn.TabIndex = 257;
            this.saveBtn.Text = "Save";
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
            this.userdetail.Location = new System.Drawing.Point(52, 369);
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
            this.sequencetb.Location = new System.Drawing.Point(40, 93);
            this.sequencetb.MaxLength = 2;
            this.sequencetb.MouseState = MaterialSkin.MouseState.OUT;
            this.sequencetb.Multiline = false;
            this.sequencetb.Name = "sequencetb";
            this.sequencetb.Size = new System.Drawing.Size(424, 50);
            this.sequencetb.TabIndex = 262;
            this.sequencetb.Text = "";
            this.sequencetb.TrailingIcon = null;
            this.sequencetb.TextChanged += new System.EventHandler(this.sequencetb_TextChanged);
            // 
            // woNumbertb
            // 
            this.woNumbertb.AnimateReadOnly = false;
            this.woNumbertb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.woNumbertb.Depth = 0;
            this.woNumbertb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.woNumbertb.Hint = "WO Number";
            this.woNumbertb.LeadingIcon = null;
            this.woNumbertb.Location = new System.Drawing.Point(44, 149);
            this.woNumbertb.MaxLength = 8;
            this.woNumbertb.MouseState = MaterialSkin.MouseState.OUT;
            this.woNumbertb.Multiline = false;
            this.woNumbertb.Name = "woNumbertb";
            this.woNumbertb.Size = new System.Drawing.Size(424, 50);
            this.woNumbertb.TabIndex = 263;
            this.woNumbertb.Text = "";
            this.woNumbertb.TrailingIcon = null;
            this.woNumbertb.Leave += new System.EventHandler(this.woNumbertb_Leave);
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
            this.runningNumbertb.Size = new System.Drawing.Size(424, 50);
            this.runningNumbertb.TabIndex = 264;
            this.runningNumbertb.Text = "";
            this.runningNumbertb.TrailingIcon = null;
            this.runningNumbertb.TextChanged += new System.EventHandler(this.runningNumbertb_TextChanged);
            // 
            // modeltb
            // 
            this.modeltb.AnimateReadOnly = false;
            this.modeltb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modeltb.Depth = 0;
            this.modeltb.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.modeltb.Hint = "Model";
            this.modeltb.LeadingIcon = null;
            this.modeltb.Location = new System.Drawing.Point(44, 261);
            this.modeltb.MaxLength = 12;
            this.modeltb.MouseState = MaterialSkin.MouseState.OUT;
            this.modeltb.Multiline = false;
            this.modeltb.Name = "modeltb";
            this.modeltb.Size = new System.Drawing.Size(424, 50);
            this.modeltb.TabIndex = 265;
            this.modeltb.Text = "";
            this.modeltb.TrailingIcon = null;
            this.modeltb.Leave += new System.EventHandler(this.modeltb_Leave);
            // 
            // AddLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 425);
            this.Controls.Add(this.modeltb);
            this.Controls.Add(this.runningNumbertb);
            this.Controls.Add(this.woNumbertb);
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
        private MaterialSkin.Controls.MaterialTextBox woNumbertb;
        private MaterialSkin.Controls.MaterialTextBox runningNumbertb;
        private MaterialSkin.Controls.MaterialTextBox modeltb;
    }
}

