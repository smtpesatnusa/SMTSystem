
namespace SMTPE
{
    partial class PrintLabelNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintLabelNew));
            this.label1 = new System.Windows.Forms.Label();
            this.InitprinterComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PrintLabNum = new System.Windows.Forms.TextBox();
            this.BarcodeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PrintButton = new System.Windows.Forms.Button();
            this.DateTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.printLabelDocument = new System.Drawing.Printing.PrintDocument();
            this.printLabelPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 95);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer Name";
            // 
            // InitprinterComboBox
            // 
            this.InitprinterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InitprinterComboBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InitprinterComboBox.FormattingEnabled = true;
            this.InitprinterComboBox.Location = new System.Drawing.Point(231, 95);
            this.InitprinterComboBox.Margin = new System.Windows.Forms.Padding(6);
            this.InitprinterComboBox.Name = "InitprinterComboBox";
            this.InitprinterComboBox.Size = new System.Drawing.Size(516, 33);
            this.InitprinterComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 264);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Copies";
            // 
            // PrintLabNum
            // 
            this.PrintLabNum.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintLabNum.Location = new System.Drawing.Point(233, 264);
            this.PrintLabNum.Margin = new System.Windows.Forms.Padding(6);
            this.PrintLabNum.Name = "PrintLabNum";
            this.PrintLabNum.ReadOnly = true;
            this.PrintLabNum.Size = new System.Drawing.Size(514, 32);
            this.PrintLabNum.TabIndex = 3;
            this.PrintLabNum.Text = "1";
            // 
            // BarcodeTextBox
            // 
            this.BarcodeTextBox.Font = new System.Drawing.Font("Verdana", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarcodeTextBox.Location = new System.Drawing.Point(231, 329);
            this.BarcodeTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.BarcodeTextBox.Name = "BarcodeTextBox";
            this.BarcodeTextBox.Size = new System.Drawing.Size(512, 153);
            this.BarcodeTextBox.TabIndex = 6;
            this.BarcodeTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BarcodeTextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 319);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Label Text";
            // 
            // PrintButton
            // 
            this.PrintButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintButton.Location = new System.Drawing.Point(397, 506);
            this.PrintButton.Margin = new System.Windows.Forms.Padding(6);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(146, 117);
            this.PrintButton.TabIndex = 8;
            this.PrintButton.Text = "Print";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // DateTextBox
            // 
            this.DateTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateTextBox.Location = new System.Drawing.Point(231, 209);
            this.DateTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.DateTextBox.Name = "DateTextBox";
            this.DateTextBox.ReadOnly = true;
            this.DateTextBox.Size = new System.Drawing.Size(514, 32);
            this.DateTextBox.TabIndex = 12;
            this.DateTextBox.Text = "04.13.2022";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(36, 209);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Date";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.Location = new System.Drawing.Point(233, 152);
            this.NameTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(514, 32);
            this.NameTextBox.TabIndex = 10;
            this.NameTextBox.Text = "NUSA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(38, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Name";
            // 
            // printLabelDocument
            // 
            this.printLabelDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printLabelDocument_PrintPage);
            // 
            // printLabelPreviewDialog
            // 
            this.printLabelPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printLabelPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printLabelPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printLabelPreviewDialog.Enabled = true;
            this.printLabelPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printLabelPreviewDialog.Icon")));
            this.printLabelPreviewDialog.Name = "printLabelPreviewDialog";
            this.printLabelPreviewDialog.Visible = false;
            // 
            // PrintLabelNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 665);
            this.Controls.Add(this.DateTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BarcodeTextBox);
            this.Controls.Add(this.PrintLabNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InitprinterComboBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintLabelNew";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Label";
            this.Load += new System.EventHandler(this.PrintLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox InitprinterComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PrintLabNum;
        private System.Windows.Forms.TextBox BarcodeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.TextBox DateTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Drawing.Printing.PrintDocument printLabelDocument;
        private System.Windows.Forms.PrintPreviewDialog printLabelPreviewDialog;
    }
}

