using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Addmodeluph : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        Helper help = new Helper();

        public Addmodeluph()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (cmbModel.Text == "" || tbUph.Text == "" )
            {
                MessageBox.Show(this, "Unable Add UPH Model with let Model or UPH Blank", "Add UPH Model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string model = cmbModel.SelectedValue.ToString();
                    string uph = tbUph.Text;

                    connectionDB.connection.Open();
                    //Buka koneksi
                    string cekmodel = "SELECT model, uph FROM tbl_masteruph WHERE model = '"+model+"'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        dscmd.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Unable to add UPH Model, UPH Model already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbModel.Text = string.Empty;
                            tbUph.Text = string.Empty;
                        }
                        else
                        {
                            string queryUpdate = "INSERT INTO tbl_masteruph (model, uph, createBy, createDate) VALUES('"+model+"', '" + uph + "', '" + idUser + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                            string[] allQuery = { queryUpdate };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Model UPH Successfully Added", "Add Model UPH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
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

        private void Addmodeluph_Load(object sender, EventArgs e)
        {
            //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
            cmbModel.Select();

            help.displayCmbList("SELECT id, model FROM tbl_model ORDER BY model", "model", "id", cmbModel);
        }

        private void tbUph_TextChanged(object sender, EventArgs e)
        {
            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(tbUph.Text, "[^0-9]"))
            {
                tbUph.Text = tbUph.Text.Remove(tbUph.Text.Length - 1);
            }
        }
    }
}
