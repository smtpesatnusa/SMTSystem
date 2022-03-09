using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Addmodelmaster : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        public Addmodelmaster()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbModel.Text == "" || tbCode.Text == "" || tbTaping.Text == "")
            {
                MessageBox.Show(this, "Unable Add Model with let Model, Code or Taping Blank", "Add Model", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);

                    string model = tbModel.Text;
                    string code = tbCode.Text;
                    string taping = tbTaping.Text;

                    connectionDB.connection.Open();
                    //Buka koneksi
                    string cekmodel = "SELECT * FROM tbl_model WHERE model = '" + model + "'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        dscmd.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Unable to add Model, Model already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbModel.Text = string.Empty;
                            tbCode.Text = string.Empty;
                        }
                        else
                        {
                            string queryAddmodel= "INSERT INTO tbl_model VALUES (null, '" + model + "', '" + code + "', '" + taping + "','" + idUser +"','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";

                            string[] allQuery = { queryAddmodel };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Model Master Successfully Added", "Add Model", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Addmodelmaster_Load(object sender, EventArgs e)
        {
          // //get user id
           var userId = usernameLbl.Text.Split(' ');
           int idPosition = usernameLbl.Text.Split(' ').Length - 3;
           idUser = userId[idPosition].Replace(",", "");
           tbModel.Select();
        }

        private void tbTaping_TextChanged(object sender, EventArgs e)
        {
            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(tbTaping.Text, "[^0-9]"))
            {
                //MessageBox.Show("Please enter only numbers.");
                tbTaping.Text = tbTaping.Text.Remove(tbTaping.Text.Length - 1);
            }
        }
    }
}
