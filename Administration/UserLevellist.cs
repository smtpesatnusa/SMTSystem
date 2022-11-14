using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class UserLevellist : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        
        string idUser;

        public UserLevellist()
        {
            InitializeComponent();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewUserLevelList.DataSource as DataTable).DefaultView.RowFilter =
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
            // remove data in datagridview result
            dataGridViewUserLevelList.DataSource = null;
            dataGridViewUserLevelList.Refresh();

            while (dataGridViewUserLevelList.Columns.Count > 0)
            {
                dataGridViewUserLevelList.Columns.RemoveAt(0);
            }

            LoadData();

            dataGridViewUserLevelList.Update();
            dataGridViewUserLevelList.Refresh();
        }

        private void LoadData()
        {
            try
            {
                connectionDB.connection.Open();
                string query = "SELECT name,description from tbl_userlevel ORDER BY id DESC";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);
                    dataGridViewUserLevelList.DataSource = dset.Tables[0];

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewUserLevelList.Columns.Add(btnDelete);
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

        private void dataGridViewUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewUserLevelList.SelectedCells[0].RowIndex;
            string levelslctd = dataGridViewUserLevelList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 2)
            {
                string message = "Do you want to delete this User Level " + levelslctd + "?";
                string title = "Delete User Level";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var cmd = new MySqlCommand("", connectionDB.connection);

                        string querydeletePO = "DELETE FROM tbl_userlevel WHERE name = '" + levelslctd + "'";
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
                        UserLevellist userLevellist = new UserLevellist();
                        userLevellist.toolStripUsername.Text = toolStripUsername.Text;
                        userLevellist.Show();
                        this.Hide();
                        MessageBox.Show("Record Deleted successfully", "User Level List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show("Unable to remove selected user level", "User Level List Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        private void dataGridViewUserList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "Name", "Description"};
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewUserLevelList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewUserLevelList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }


        private void clearBtn_Click(object sender, EventArgs e)
        {
            tbuserLevel.Clear();
            tbDesc.Clear();
            tbuserLevel.Focus();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbuserLevel.Text == "" || tbDesc.Text == "" )
            {
                MessageBox.Show(this, "Unable Add User Level with let User Level or description blank", "Add User Level", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string userlevel = tbuserLevel.Text;
                    string desc = tbDesc.Text;

                    string cekmodel = "SELECT * FROM tbl_userlevel WHERE name = '" + userlevel + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to add user level, User level already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tbuserLevel.Clear();
                            tbDesc.Clear();
                            tbuserLevel.Focus();
                        }
                        else
                        {
                            connectionDB.connection.Open();
                            string queryAddmodel = "INSERT INTO tbl_userlevel (name, description, createDate, createBy) VALUES " +
                                "('" + userlevel + "', '" + desc + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";

                            string[] allQuery = { queryAddmodel };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }
                            connectionDB.connection.Close();
                            MessageBox.Show(this, "User Level Successfully Added", "Add User Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
