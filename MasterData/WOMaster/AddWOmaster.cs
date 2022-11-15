using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class AddWOmaster : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        public AddWOmaster()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbWO.Text == "" || tbModel.Text == "" )
            {
                MessageBox.Show(this, "Unable Add Master WO Pegatron with let WO No or Model Blank", "Add Master WO Pegatron", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tbWO.TextLength != 8)
            {
                MessageBox.Show("Please fill WO Number with 8 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tbModel.TextLength != 12)
            {
                MessageBox.Show("Please fill Model with 12 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);

                    string wo = tbWO.Text;
                    string model = tbModel.Text;

                    connectionDB.connection.Open();
                    //Buka koneksi
                    string cek = "SELECT * FROM tbl_wopegatron WHERE wonumber = '" + wo + "'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cek, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        dscmd.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Unable to add Master WO Pegatron, WO already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbWO.Text = string.Empty;
                            tbModel.Text = string.Empty;
                        }
                        else
                        {
                            string queryAdd= "INSERT INTO tbl_wopegatron (wonumber, model, createBy, createDate) VALUES ('" + wo + "', '" + model + "','" + idUser +"','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";

                            string[] allQuery = { queryAdd};
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Master WO Pegatron Successfully Added", "Add Master WO Pegatron", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();;
                        }
                    }
                }
                catch (Exception ex)
                {
                    connectionDB.connection.Close();
                    MessageBox.Show(this, "Unable to delete master WO pegatron, WO already used", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }            
            }
        }


        private void AddWOmaster_Load(object sender, EventArgs e)
        {
            //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
            tbWO.Select();
        }

        private void tbWO_Leave(object sender, EventArgs e)
        {
            tbWO.Text = tbWO.Text.ToUpper();
        }

        private void tbModel_Leave(object sender, EventArgs e)
        {
            tbModel.Text = tbModel.Text.ToUpper();
        }
    }
}
