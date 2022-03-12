
namespace SMTPE
{
    partial class Adduser
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
            this.tbname = new System.Windows.Forms.TextBox();
            this.tbusername = new System.Windows.Forms.Label();
            this.tbuserid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.usernameLbl = new System.Windows.Forms.Label();
            this.tbuserrole = new System.Windows.Forms.Label();
            this.cmbuserlevel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // saveBtn
            // 
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveBtn.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.saveBtn.Location = new System.Drawing.Point(313, 274);
            this.saveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(120, 43);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // tbname
            // 
            this.tbname.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbname.Location = new System.Drawing.Point(239, 153);
            this.tbname.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbname.Name = "tbname";
            this.tbname.Size = new System.Drawing.Size(277, 26);
            this.tbname.TabIndex = 1;
            // 
            // tbusername
            // 
            this.tbusername.AutoSize = true;
            this.tbusername.BackColor = System.Drawing.Color.Transparent;
            this.tbusername.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbusername.Location = new System.Drawing.Point(63, 156);
            this.tbusername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tbusername.Name = "tbusername";
            this.tbusername.Size = new System.Drawing.Size(84, 19);
            this.tbusername.TabIndex = 14;
            this.tbusername.Text = "User Name";
            // 
            // tbuserid
            // 
            this.tbuserid.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbuserid.Location = new System.Drawing.Point(239, 97);
            this.tbuserid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbuserid.MaxLength = 6;
            this.tbuserid.Name = "tbuserid";
            this.tbuserid.Size = new System.Drawing.Size(277, 26);
            this.tbuserid.TabIndex = 0;
            this.tbuserid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbuserid_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.label5.Location = new System.Drawing.Point(63, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 19);
            this.label5.TabIndex = 41;
            this.label5.Text = "User ID";
            // 
            // usernameLbl
            // 
            this.usernameLbl.AutoSize = true;
            this.usernameLbl.BackColor = System.Drawing.Color.Transparent;
            this.usernameLbl.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.usernameLbl.Location = new System.Drawing.Point(44, 299);
            this.usernameLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.usernameLbl.Name = "usernameLbl";
            this.usernameLbl.Size = new System.Drawing.Size(77, 19);
            this.usernameLbl.TabIndex = 43;
            this.usernameLbl.Text = "username";
            this.usernameLbl.Visible = false;
            // 
            // tbuserrole
            // 
            this.tbuserrole.AutoSize = true;
            this.tbuserrole.BackColor = System.Drawing.Color.Transparent;
            this.tbuserrole.Font = new System.Drawing.Font("Open Sans", 8.25F);
            this.tbuserrole.Location = new System.Drawing.Point(63, 215);
            this.tbuserrole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tbuserrole.Name = "tbuserrole";
            this.tbuserrole.Size = new System.Drawing.Size(73, 19);
            this.tbuserrole.TabIndex = 44;
            this.tbuserrole.Text = "User Role";
            // 
            // cmbuserlevel
            // 
            this.cmbuserlevel.FormattingEnabled = true;
            this.cmbuserlevel.Location = new System.Drawing.Point(239, 213);
            this.cmbuserlevel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbuserlevel.Name = "cmbuserlevel";
            this.cmbuserlevel.Size = new System.Drawing.Size(277, 24);
            this.cmbuserlevel.TabIndex = 2;
            // 
            // Adduser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 375);
            this.Controls.Add(this.cmbuserlevel);
            this.Controls.Add(this.tbuserrole);
            this.Controls.Add(this.usernameLbl);
            this.Controls.Add(this.tbuserid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.tbname);
            this.Controls.Add(this.tbusername);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Adduser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add User";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Adduser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button saveBtn;
        public System.Windows.Forms.TextBox tbname;
        private System.Windows.Forms.Label tbusername;
        public System.Windows.Forms.TextBox tbuserid;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label usernameLbl;
        private System.Windows.Forms.Label tbuserrole;
        private System.Windows.Forms.ComboBox cmbuserlevel;
    }
}