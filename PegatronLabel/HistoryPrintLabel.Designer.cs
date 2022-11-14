
namespace SMTPE
{
    partial class HistoryPrintLabel
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
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.idLabel = new System.Windows.Forms.Label();
            this.modeltb = new MaterialSkin.Controls.MaterialTextBox();
            this.runningNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.woNumbertb = new MaterialSkin.Controls.MaterialTextBox();
            this.sequencetb = new MaterialSkin.Controls.MaterialTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.AllowUserToDeleteRows = false;
            this.dataGridViewHistory.AllowUserToOrderColumns = true;
            this.dataGridViewHistory.AllowUserToResizeRows = false;
            this.dataGridViewHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Location = new System.Drawing.Point(24, 172);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.ReadOnly = true;
            this.dataGridViewHistory.RowHeadersWidth = 51;
            this.dataGridViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistory.Size = new System.Drawing.Size(909, 381);
            this.dataGridViewHistory.TabIndex = 11;
            this.dataGridViewHistory.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewHistory_CellFormatting);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.6F);
            this.idLabel.Location = new System.Drawing.Point(23, 78);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(26, 20);
            this.idLabel.TabIndex = 289;
            this.idLabel.Text = "ID";
            this.idLabel.Visible = false;
            // 
            // modeltb
            // 
            this.modeltb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modeltb.AnimateReadOnly = false;
            this.modeltb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.modeltb.Depth = 0;
            this.modeltb.Enabled = false;
            this.modeltb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.modeltb.Hint = "Model";
            this.modeltb.LeadingIcon = null;
            this.modeltb.Location = new System.Drawing.Point(682, 101);
            this.modeltb.MaxLength = 50;
            this.modeltb.MouseState = MaterialSkin.MouseState.OUT;
            this.modeltb.Multiline = false;
            this.modeltb.Name = "modeltb";
            this.modeltb.Size = new System.Drawing.Size(260, 50);
            this.modeltb.TabIndex = 299;
            this.modeltb.Text = "";
            this.modeltb.TrailingIcon = null;
            // 
            // runningNumbertb
            // 
            this.runningNumbertb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runningNumbertb.AnimateReadOnly = false;
            this.runningNumbertb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.runningNumbertb.Depth = 0;
            this.runningNumbertb.Enabled = false;
            this.runningNumbertb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.runningNumbertb.Hint = "Running Number";
            this.runningNumbertb.LeadingIcon = null;
            this.runningNumbertb.Location = new System.Drawing.Point(446, 101);
            this.runningNumbertb.MaxLength = 50;
            this.runningNumbertb.MouseState = MaterialSkin.MouseState.OUT;
            this.runningNumbertb.Multiline = false;
            this.runningNumbertb.Name = "runningNumbertb";
            this.runningNumbertb.Size = new System.Drawing.Size(230, 50);
            this.runningNumbertb.TabIndex = 298;
            this.runningNumbertb.Text = "";
            this.runningNumbertb.TrailingIcon = null;
            // 
            // woNumbertb
            // 
            this.woNumbertb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.woNumbertb.AnimateReadOnly = false;
            this.woNumbertb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.woNumbertb.Depth = 0;
            this.woNumbertb.Enabled = false;
            this.woNumbertb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.woNumbertb.Hint = "WO Number";
            this.woNumbertb.LeadingIcon = null;
            this.woNumbertb.Location = new System.Drawing.Point(179, 101);
            this.woNumbertb.MaxLength = 50;
            this.woNumbertb.MouseState = MaterialSkin.MouseState.OUT;
            this.woNumbertb.Multiline = false;
            this.woNumbertb.Name = "woNumbertb";
            this.woNumbertb.Size = new System.Drawing.Size(261, 50);
            this.woNumbertb.TabIndex = 297;
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
            this.sequencetb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sequencetb.Hint = "Sequence";
            this.sequencetb.LeadingIcon = null;
            this.sequencetb.Location = new System.Drawing.Point(27, 101);
            this.sequencetb.MaxLength = 50;
            this.sequencetb.MouseState = MaterialSkin.MouseState.OUT;
            this.sequencetb.Multiline = false;
            this.sequencetb.Name = "sequencetb";
            this.sequencetb.Size = new System.Drawing.Size(146, 50);
            this.sequencetb.TabIndex = 296;
            this.sequencetb.Text = "";
            this.sequencetb.TrailingIcon = null;
            // 
            // HistoryPrintLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 576);
            this.Controls.Add(this.modeltb);
            this.Controls.Add(this.runningNumbertb);
            this.Controls.Add(this.woNumbertb);
            this.Controls.Add(this.sequencetb);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.dataGridViewHistory);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistoryPrintLabel";
            this.Padding = new System.Windows.Forms.Padding(2, 52, 2, 2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pegatron Print Label History ";
            this.Load += new System.EventHandler(this.PrintLabel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHistory;
        public System.Windows.Forms.Label idLabel;
        private MaterialSkin.Controls.MaterialTextBox modeltb;
        private MaterialSkin.Controls.MaterialTextBox runningNumbertb;
        private MaterialSkin.Controls.MaterialTextBox woNumbertb;
        private MaterialSkin.Controls.MaterialTextBox sequencetb;
    }
}

