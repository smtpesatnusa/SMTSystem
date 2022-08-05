using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Editmodeluph : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        public Editmodeluph()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbUph.Text == "" )
            {
                MessageBox.Show(this, "Unable Add UPH Model with let UPH Blank", "Edit UPH Model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string model = tbModel.Text;
                    string uph = tbUph.Text;

                    connectionDB.connection.Open();
                    //Buka koneksi

                    string queryUpdate = "UPDATE tbl_masteruph a, tbl_model b SET a.uph = '"+uph+"' WHERE a.model = b.id AND b.model = '"+model+"'";

                    string[] allQuery = { queryUpdate };
                    for (int j = 0; j < allQuery.Length; j++)
                    {
                        cmd.CommandText = allQuery[j];
                        //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                        cmd.ExecuteNonQuery();
                        //Jalankan perintah / query dalam CommandText pada database
                    }
                    connectionDB.connection.Close();
                    MessageBox.Show(this, "UPH Successfully Updated", "Edit Model UPH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    connectionDB.connection.Close();
                    MessageBox.Show(ex.Message.ToString());
                }            
            }
        }


        private void tbUph_TextChanged(object sender, EventArgs e)
        {
            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(tbUph.Text, "[^0-9]"))
            {
                tbUph.Text = tbUph.Text.Remove(tbUph.Text.Length - 1);
            }
        }

        private void Editmodeluph_Load(object sender, EventArgs e)
        {
            //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
        }
    }
}
