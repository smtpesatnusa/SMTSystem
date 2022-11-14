using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Userlist : MaterialForm
    {
        Helper help = new Helper();
        readonly ConnectionDB connectionDB = new ConnectionDB();

        public Userlist()
        {
            InitializeComponent();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewUserList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("username LIKE '%" + tbSearch.Text + "%' or name LIKE '%" + tbSearch.Text + "%'or role LIKE '%" + tbSearch.Text + "%'");
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
            Refresh();
        }

        private void Refresh()
        {
            tbSearch.Clear();

            // remove data in datagridview result
            dataGridViewUserList.DataSource = null;
            dataGridViewUserList.Refresh();

            while (dataGridViewUserList.Columns.Count > 0)
            {
                dataGridViewUserList.Columns.RemoveAt(0);
            }

            LoadData();

            dataGridViewUserList.Update();
            dataGridViewUserList.Refresh();
        }

        private void LoadData()
        {
            try
            {
                connectionDB.connection.Open();
                string query = "SELECT username, name,role from tbl_user ORDER BY id DESC";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);
                    dataGridViewUserList.DataSource = dset.Tables[0];

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewUserList.Columns.Add(btnDelete);
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


        private void Userlist_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            try
            {
                string queryuserlevel = "SELECT * FROM tbl_userlevel";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryuserlevel, connectionDB.connection))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            cmbuserlevel.Items.Add(dt.Rows[j]["name"]);
                            cmbuserlevel.ValueMember = dt.Rows[j]["name"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }

            // display data in datagridview
            LoadData();
        }

        
        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearInput();
        }

        private void clearInput()
        {
            tbbadgeid.Clear();
            tbname.Clear();
            cmbuserlevel.SelectedIndex = -1;
        }

        private void dataGridViewUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewUserList.SelectedCells[0].RowIndex;
            string poslctd = dataGridViewUserList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 3)
            {
                string message = "Do you want to delete this User with ID " + poslctd + "?";
                string title = "Delete User";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var cmd = new MySqlCommand("", connectionDB.connection);

                        string querydeletePO = "DELETE FROM tbl_user WHERE username = '" + poslctd + "'";
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
                        clearInput();
                        MessageBox.Show("Record Deleted successfully", "User List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Refresh();
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show("Unable to remove selected user", "User List Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void tbbadgeid_TextChanged(object sender, EventArgs e)
        {
            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(tbbadgeid.Text, "[^0-9]"))
            {
                //MessageBox.Show("Please enter only numbers.");
                tbbadgeid.Text = tbbadgeid.Text.Remove(tbbadgeid.Text.Length - 1);
            }
        }

        private void dataGridViewUserList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "Badge ID", "User Name", "Level" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewUserList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewUserList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewUserList.Columns[1].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbbadgeid.Text == "" || tbname.Text == "" || cmbuserlevel.Text == "")
            {
                MessageBox.Show(this, "Unable Add User with let Badge ID, Username or Level blank", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string userid = tbbadgeid.Text;
                    string username = tbname.Text;
                    string userrole = tbuserrole.Text;
                    string password = help.encryption("Passw0rd");

                    string cekmodel = "SELECT * FROM tbl_user WHERE username = '" + userid + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to add user, Badge ID already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clearInput();
                        }
                        else
                        {
                            connectionDB.connection.Open();
                            string queryAddmodel = "INSERT INTO tbl_user VALUES (null, '" + userid + "', '" + username + "','" + password + "','" + cmbuserlevel.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

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
                            clearInput();
                            Refresh();
                            tbbadgeid.Focus();
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

        private void tbname_TextChanged(object sender, EventArgs e)
        {
            CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            tbname.Text = textInfo.ToTitleCase(tbname.Text.ToLower());
            tbname.Select(tbname.Text.Length, 0);
        }

        private void Userlist_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
