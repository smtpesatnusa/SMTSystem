using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class EditPublicHoliday : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser, dept;

        public EditPublicHoliday()
        {
            InitializeComponent();
        }

        private void AddPublicHoliday_Load(object sender, EventArgs e)
        {
            //get user id
            var userId = usernameLbl.Text.Split(' ');
            int idPosition = usernameLbl.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show(this, "Unable Add Public Holiday with let any field blank", "Add Public Holiday", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string name = tbName.Text;
                    string date = dateTimePickerDate.Text;

                    string cekdata = "SELECT * FROM tbl_masterholiday WHERE date = '" + date + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekdata, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika tgl tsb sudah di insert
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to add public holiday, Public Holiday already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbName.Clear();
                        }
                        else
                        {
                            connectionDB.connection.Open();
                            string queryAdd = "INSERT INTO tbl_masterholiday (name, date, createDate, createBy) " +
                                "VALUES ('" + name + "', '" + date + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";

                            string[] allQuery = { queryAdd };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Public Holiday Successfully Added", "Add Public Holiday", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    }
}
