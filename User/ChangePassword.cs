using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class ChangePassword : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        Helper help = new Helper();
        string idUser;

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbcrnpass.Text == "" || tbnewpass.Text == "" || tbvrypass.Text == "")
            {
                MessageBox.Show(this, "Unable Change with let Current Password or new Password Blank", "Chane Passw0rd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string crnpass = tbcrnpass.Text;
                    crnpass = help.encryption(crnpass);
                    string newpass = tbnewpass.Text;
                    newpass = help.encryption(newpass);
                    string vrypass = tbvrypass.Text;

                    connectionDB.connection.Open();
                    //Buka koneksi
                    string cekcrnpass= "SELECT * FROM tbl_user WHERE username = '" + idUser + "' and pass ='"+ crnpass +"'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekcrnpass, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        dscmd.Fill(ds);
                        connectionDB.connection.Close();
                        // cek jika username dan pass nya benar
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            connectionDB.connection.Open();
                            string querychangepass = "update tbl_user set pass='" + newpass + "' where username='" + idUser + "'";

                            string[] allQuery = { querychangepass };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Password Successfully Changed, Please login again", "Password change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            Application.Restart();
                            
                        }
                        else
                        {
                            MessageBox.Show(this, "Current Password wrong", "Password change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            connectionDB.connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    connectionDB.connection.Close();
                    MessageBox.Show(ex.Message.ToString());
                }            
            }
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            // //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");

        }

        private void tbvrypass_TextChanged(object sender, EventArgs e)
        {
            if (tbnewpass.Text == tbvrypass.Text)
            {
                lblnotmatch.Visible = false;
                saveBtn.Enabled = true;
            }
            else
            {
                saveBtn.Enabled = false;
                lblnotmatch.Visible = true;
            }
        }
    }
}
