using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Userlist : MaterialForm
    {
        LoadForm lf = new LoadForm();
        ConnectionDB connectionDB = new ConnectionDB();
        private DataSet ds;
        private DataTable dtSource;
        private int PageCount;
        private int maxRec;
        private int pageSize;
        private int currentPage;
        private int recNo;
        private string Sql;

        public Userlist()
        {
            InitializeComponent();
        }

        //The below is the key for showing Progress bar
        private void StartProgress(String strStatusText)
        {
            LoadForm lf = new LoadForm();
            ShowProgress();
        }
        private void CloseProgress()
        {
            //Thread.Sleep(200);
            while (!this.IsHandleCreated)
                System.Threading.Thread.Sleep(200);
            lf.Invoke(new Action(lf.Close));
        }
        private void ShowProgress()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        lf.ShowDialog();
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Thread th = new Thread(ShowProgress);
                    th.IsBackground = false;
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewUserList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("username LIKE '{0}%' or name LIKE '{0}%' ", tbSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }
        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // remove data in datagridview result
            dataGridViewUserList.DataSource = null;
            dataGridViewUserList.Refresh();

            while (dataGridViewUserList.Columns.Count > 0)
            {
                dataGridViewUserList.Columns.RemoveAt(0);
            }

            LoadDataModel();

            dataGridViewUserList.Update();
            dataGridViewUserList.Refresh();
        }

        private void LoadDataModel()
        {
            try
            {
                connectionDB.connection.Open();
                string query = "SELECT username, name,role,createdate from tbl_user";
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
        private void Modelmasterlist_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to logout?";
            string title = "Confirm Logout";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            if (MetroMessageBox.Show(this, message, title, buttons, icon) == DialogResult.No)
                e.Cancel = true;
            else
                System.Windows.Forms.Application.ExitThread();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void dataGridViewUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewUserList.SelectedCells[0].RowIndex;
            string poslctd = dataGridViewUserList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 4)
            {
                string message = "Do you want to delete this User with ID " + poslctd + "?";
                string title = "Delete User";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
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
                    Userlist userlist = new Userlist();
                    userlist.toolStripUsername.Text = toolStripUsername.Text;
                    userlist.Show();
                    this.Hide();
                    MessageBox.Show("Record Deleted successfully", "User List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                }
            }
        }

        private void adduser_Click(object sender, EventArgs e)
        {
            Adduser adduser = new Adduser();
            adduser.usernameLbl.Text = toolStripUsername.Text;
            adduser.Show();
        }

        private void Userlist_Load(object sender, EventArgs e)
        {

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            connectionDB.connection.Open();

            string query = "SELECT username, name,role,createDate from tbl_user";

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

        private void dataGridViewUserList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "User ID", "User Name", "Role", "CreateDate" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewUserList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewUserList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }
    }
}
