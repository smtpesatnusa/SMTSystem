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
        string idUser,dept;

        public AddMasterMaterial()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            //if (tbPartnumberSN.Text == "" || tbBadgeid.Text == "" || tbName.Text == "" || cmbGender.Text == "" ||
            //    dateTimePickerDOJ.Text == "" || cmbLevel.Text == "" || cmbDepartment.Text == "" || cmbLineCode.Text == "")
            //{
            //    MessageBox.Show(this, "Unable Add Employee with let any field blank", "Add Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //if (tbBadgeid.Text.Length != 6)
            //{
            //    MessageBox.Show("Wrong Badge ID, please fill Badge ID Properly", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    tbBadgeid.Text = string.Empty;
            //}
            //else
            //{
            //    try
            //    {
            //        var cmd = new MySqlCommand("", connectionDB.connection);
            //        string rfid = tbPartnumberSN.Text;
            //        string badgeid = tbBadgeid.Text;
            //        string name = tbName.Text;
            //        string gender = cmbGender.Text;
            //        string level = cmbLevel.Text;
            //        string department = cmbDepartment.Text;
            //        string linecode = cmbLineCode.Text;
            //        string shift = cmbShift.Text;

            //        // date
            //        string _Date = dateTimePickerDOJ.Text;
            //        DateTime dt = Convert.ToDateTime(_Date);
            //        string doj = dt.ToString("yyyy-MM-dd");


            //        string cekdata = "SELECT * FROM tbl_employee WHERE badgeID = '" + badgeid + "' OR rfidNo = '" + rfid + "'";
            //        using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekdata, connectionDB.connection))
            //        {
            //            DataSet ds = new DataSet();
            //            adpt.Fill(ds);

            //            // cek jika modelno tsb sudah di upload
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                MessageBox.Show(this, "Unable to add employee, BadgeID or RFID No already used by other employee", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                tbPartnumberSN.Text = string.Empty;
            //                tbBadgeid.Text = string.Empty;
            //            }
            //            else
            //            {
            //                connectionDB.connection.Open();
            //                string queryAdd = "INSERT INTO tbl_employee (badgeID, rfidNo, name, gender, doj, level, dept, linecode, shift , createDate, createBy) " +
            //                    "VALUES ('" + badgeid + "', '" + rfid + "','" + name + "','" + gender + "','" + doj + "','" + level + "','" + department + "','" + linecode + "','" + shift + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";

            //                string[] allQuery = { queryAdd };
            //                for (int j = 0; j < allQuery.Length; j++)
            //                {
            //                    cmd.CommandText = allQuery[j];
            //                    //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
            //                    cmd.ExecuteNonQuery();
            //                    //Jalankan perintah / query dalam CommandText pada database
            //                }
            //                connectionDB.connection.Close();
            //                MessageBox.Show(this, "Employee Successfully Added", "Add Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                this.Close();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        connectionDB.connection.Close();
            //        MessageBox.Show(ex.Message.ToString());
            //    }
            //}
        }

        private void AddMasterMaterial_Load(object sender, EventArgs e)
        {
            //get user id
            var userId = userdetail.Text.Split('|');
            idUser = userId[0].Trim();
            dept = userId[1].Trim();

        }
    }
}
