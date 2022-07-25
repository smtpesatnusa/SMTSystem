using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class AddMasterMaterial : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser, dept;

        public AddMasterMaterial()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbPartnumberSN.Text == "" || tbDesc.Text == "" || tbFtype.Text == "" || tbLoc.Text == "")
            {
                MessageBox.Show(this, "Unable Add Employee with let any field blank", "Add Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string pnsn = tbPartnumberSN.Text;
                    string desc = tbDesc.Text;
                    string ftype = tbFtype.Text;
                    string loc = tbLoc.Text;

                    string cekdata = "SELECT * FROM tbl_masterpartmaterial WHERE material = '"+pnsn+"'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekdata, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to add material, partnumber already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbPartnumberSN.Clear();
                            tbDesc.Clear();
                            tbFtype.Clear();
                            tbLoc.Clear();
                        }
                        else
                        {
                            connectionDB.connection.Open();
                            string queryAdd = "INSERT INTO tbl_masterpartmaterial (material, description, f_type, location, importDate, importBy) " +
                                "VALUES ('" + pnsn + "', '" + desc + "','" + ftype + "','" + loc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";

                            string[] allQuery = { queryAdd };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Master Material Successfully Added", "Add Master Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void AddMasterMaterial_Load(object sender, EventArgs e)
        {
            //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
        }
    }
}
