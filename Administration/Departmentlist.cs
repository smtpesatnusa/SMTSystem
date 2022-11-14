using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Departmentlist : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();

        string idUser;

        public Departmentlist()
        {
            InitializeComponent();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewDepartmentList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("name LIKE '%" + tbSearch.Text + "%'or description LIKE '%" + tbSearch.Text + "%'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            help.dateTimeNow(dateTimeNow);
        }
        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            tbSearch.Clear();

            // remove data in datagridview result
            dataGridViewDepartmentList.DataSource = null;
            dataGridViewDepartmentList.Refresh();

            while (dataGridViewDepartmentList.Columns.Count > 0)
            {
                dataGridViewDepartmentList.Columns.RemoveAt(0);
            }

            LoadData();

            dataGridViewDepartmentList.Update();
            dataGridViewDepartmentList.Refresh();
        }

        private void LoadData()
        {
            try
            {
                connectionDB.connection.Open();
                string query = "SELECT name,description from tbl_department ORDER BY id DESC";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);
                    dataGridViewDepartmentList.DataSource = dset.Tables[0];

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewDepartmentList.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    btnDelete.UseColumnTextForButtonValue = true;
                }
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            tbuserLevel.Clear();
            tbDesc.Clear();
            tbuserLevel.Focus();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbuserLevel.Text == "" || tbDesc.Text == "")
            {
                MessageBox.Show(this, "Unable Add Department with let Department or description blank", "Add Department", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string department = tbuserLevel.Text;
                    string desc = tbDesc.Text;

                    string cekdpt = "SELECT * FROM tbl_department WHERE name = '" + department + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekdpt, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika dpt tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to add department, Department already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbuserLevel.Clear();
                            tbDesc.Clear();
                            tbuserLevel.Focus();
                        }
                        else
                        {
                            connectionDB.connection.Open();
                            string queryAdddpt = "INSERT INTO tbl_department (name, description, createDate, createBy) VALUES " +
                                "('" + department + "', '" + desc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";

                            string[] allQuery = { queryAdddpt };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "Department Successfully Added", "Add Department", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbuserLevel.Clear();
                            tbDesc.Clear();
                            refresh();
                            tbuserLevel.Focus();
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

        private void tbuserLevel_TextChanged(object sender, EventArgs e)
        {
            //CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            //TextInfo textInfo = cultureInfo.TextInfo;
            //tbuserLevel.Text = textInfo.ToTitleCase(tbuserLevel.Text.ToLower());
            //tbuserLevel.Select(tbuserLevel.Text.Length, 0);
        }

        private void UserLevellist_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");

            LoadData();
        }

        private void UserLevellist_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MaterialDialog materialDialog = new MaterialDialog(this, title, message, "OK", true, "Cancel");
            DialogResult result = materialDialog.ShowDialog(this);
            if (result.ToString() == "OK")
            {
                System.Windows.Forms.Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(result.ToString(), 750);
                SnackBarMessage.Show(this);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void dataGridViewDepartmentList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "Name", "Description" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewDepartmentList.Columns[i].HeaderText = "" + title[i];
            }
            dataGridViewDepartmentList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }

        private void dataGridViewDepartmentList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewDepartmentList.SelectedCells[0].RowIndex;
            string departmentslctd = dataGridViewDepartmentList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 2)
            {
                string message = "Do you want to delete this Department " + departmentslctd + "?";
                string title = "Delete Department";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var cmd = new MySqlCommand("", connectionDB.connection);

                        string querydeletePO = "DELETE FROM tbl_department WHERE name = '" + departmentslctd + "'";
                        connectionDB.connection.Open();

                        string[] allQuery = { querydeletePO };
                        for (int j = 0; j < allQuery.Length; j++)
                        {
                            cmd.CommandText = allQuery[j];
                            //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                            cmd.ExecuteNonQuery();
                            //Jalankan perintah / query dalam CommandText pada database
                        }

                        connectionDB.connection.Close();
                        Departmentlist departmentlist = new Departmentlist();
                        departmentlist.toolStripUsername.Text = toolStripUsername.Text;
                        departmentlist.Show();
                        this.Hide();
                        MessageBox.Show("Record Deleted successfully", "Department List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show("Unable to remove selected department", "Department List Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
