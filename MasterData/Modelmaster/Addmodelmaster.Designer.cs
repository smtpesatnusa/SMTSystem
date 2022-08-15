
namespace SMTPE
{
    partial class Addmodelmaster
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
            this.saveBtn = new System.Windows.Forms.Button();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbModel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.tbTaping = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.saveBtn.Location = new System.Drawing.Point(300, 290);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(120, 43);
            this.saveBtn.TabIndex = 18;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // tbCode
            // 
            this.tbCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCode.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbCode.Location = new System.Drawing.Point(239, 153);
            this.tbCode.Margin = new System.Windows.Forms.Padding(4);
            this.tbCode.MaxLength = 2;
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(277, 26);
            this.tbCode.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.label1.Location = new System.Drawing.Point(63, 156);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "Code";
            // 
            // tbModel
            // 
            this.tbModel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbModel.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbModel.Location = new System.Drawing.Point(239, 97);
            this.tbModel.Margin = new System.Windows.Forms.Padding(4);
            this.tbModel.Name = "tbModel";
            this.tbModel.Size = new System.Drawing.Size(277, 26);
            this.tbModel.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.label5.Location = new System.Drawing.Point(63, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 19);
            this.label5.TabIndex = 41;
            this.label5.Text = "Model";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.BackColor = System.Drawing.Color.Transparent;
            this.usernameLbl.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.usernameLbl.Location = new System.Drawing.Point(60, 290);
            this.usernameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(77, 19);
            this.usernameLbl.TabIndex = 43;
            this.usernameLbl.Text = "username";
            this.usernameLbl.Visible = false;
            // 
            // tbTaping
            // 
            this.tbTaping.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbTaping.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbTaping.Location = new System.Drawing.Point(239, 213);
            this.tbTaping.Margin = new System.Windows.Forms.Padding(4);
            this.tbTaping.MaxLength = 1;
            this.tbTaping.Name = "tbTaping";
            this.tbTaping.Size = new System.Drawing.Size(277, 26);
            this.tbTaping.TabIndex = 45;
            this.tbTaping.TextChanged += new System.EventHandler(this.tbTaping_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.label2.Location = new System.Drawing.Point(63, 216);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 19);
            this.label2.TabIndex = 44;
            this.label2.Text = "Taping";
            // 
            // Addmodelmaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 371);
            this.Controls.Add(this.tbTaping);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.tbModel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Addmodelmaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Model Master";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Addmodelmaster_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveBtn;
        public System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tbModel;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label usernameLbl;
        public System.Windows.Forms.TextBox tbTaping;
        private System.Windows.Forms.Label label2;
    }
}