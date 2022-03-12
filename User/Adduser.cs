using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Adduser : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        Helper help = new Helper();
        string idUser;

        public Adduser()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbuserid.Text == "" || tbname.Text == "" || cmbuserlevel.Text == "")
            {
                MessageBox.Show(this, "Unable Add User with let UserID, Username or Level blank", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string userid = tbuserid.Text;
                    string username = tbname.Text;
                    string userrole = tbuserrole.Text;
                    string password = help.encryption("Passw0rd");

                    connectionDB.connection.Open();
                    //Buka koneksi
                    string cekmodel = "SELECT * FROM tbl_user WHERE username = '" + userid + "'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        dscmd.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Unable to add user, UserID already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbuserid.Text = string.Empty;
                            tbname.Text = string.Empty;
                            tbuserrole.Text = string.Empty;
                        }
                        else
                        {
                            string queryAddmodel= "INSERT INTO tbl_user VALUES (null, '" + userid + "', '" + username + "','"+password+"','" + cmbuserlevel.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";

                            string[] allQuery = { queryAddmodel };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "User Successfully Added", "Add Model", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();;
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

        private void Adduser_Load(object sender, EventArgs e)
        {


            // //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
            // tbmodelno.Select();

            connectionDB.connection.Open();
            string queryuserlevel = "SELECT * FROM tbl_userlevel";
            try
            {
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryuserlevel, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);

                    if (dset.Rows.Count > 0)
                    {
                        for (int j = 0; j < dset.Rows.Count; j++)
                        {
                            cmbuserlevel.Items.Add(dset.Rows[j]["Level_User"]);
                            cmbuserlevel.ValueMember = dset.Rows[j]["Level_User"].ToString();
                        }
                    }
                    else
                    {
                    }
                }

            

                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }
    

        private void tbuserid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                tbname.Focus();
            }
        }
    }
}
