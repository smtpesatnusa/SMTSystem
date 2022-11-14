using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class CustControl : MaterialForm
    {
        readonly ConnectionDB connectionDB = new ConnectionDB();

        string idUser;
        string role;

        public CustControl()
        {
            InitializeComponent();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewCustList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("codeCust LIKE '%" + tbSearch.Text + "%' or createBy LIKE '%" + tbSearch.Text + "%'");
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
            dataGridViewCustList.DataSource = null;
            dataGridViewCustList.Refresh();

            while (dataGridViewCustList.Columns.Count > 0)
            {
                dataGridViewCustList.Columns.RemoveAt(0);
            }

            LoadDataCust();

            dataGridViewCustList.Update();
            dataGridViewCustList.Refresh();
        }

        private void LoadDataCust()
        {
            try
            {
                string query = "SELECT codeCust, createBy, createDate FROM tbl_controlcust ORDER BY id DESC";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);
                    dataGridViewCustList.DataSource = dset.Tables[0];

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewCustList.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    btnDelete.UseColumnTextForButtonValue = true;
                }
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }


        private void addCustControl()
        {
            try
            {
                var cmd = new MySqlCommand("", connectionDB.connection);
                string custCode = tbCustCode.Text.ToUpper();

                connectionDB.connection.Open();
                //Buka koneksi
                string cek = "SELECT codeCust FROM tbl_controlcust WHERE codeCust = '" + custCode + "'";
                using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cek, connectionDB.connection))
                {
                    DataSet ds = new DataSet();
                    dscmd.Fill(ds);

                    // cek jika cust tsb sudah ada
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show(this, "Unable to add Customer code, Customer code already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbCustCode.Text = string.Empty;
                    }
                    else
                    {
                        string queryAddPO = "INSERT INTO tbl_controlcust (codeCust, createBy, createdate) " +
                            "VALUES('" + custCode + "', '" + idUser + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";

                        string[] allQuery = { queryAddPO };
                        for (int j = 0; j < allQuery.Length; j++)
                        {
                            cmd.CommandText = allQuery[j];
                            //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                            cmd.ExecuteNonQuery();
                            //Jalankan perintah / query dalam CommandText pada database
                        }
                        connectionDB.connection.Close();

                        CustControl custControl = new CustControl();
                        custControl.toolStripUsername.Text = toolStripUsername.Text;
                        custControl.Show();
                        this.Hide();
                        MessageBox.Show(this, "Customer Code Successfully Added", "Add Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbCustCode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CustControl_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
            role = toolStripUsername.Text.Substring(toolStripUsername.Text.LastIndexOf(','));
            role = role.Replace(" ", "").Replace(",", "").Replace("|", "").ToLower();

            LoadDataCust();
        }

        private void dataGridViewCustList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "Cust Code", "Create By", "Create Date" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewCustList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewCustList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewCustList.Columns[2].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";
        }

        private void dataGridViewCustList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewCustList.SelectedCells[0].RowIndex;
            string codeslctd = dataGridViewCustList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 3)
            {
                if (role == "super")
                {
                    string message = "Do you want to delete this Customer Code " + codeslctd + "?";
                    string title = "Delete Customer Code";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            var cmd = new MySqlCommand("", connectionDB.connection);

                            string querydelete = "DELETE FROM tbl_controlcust WHERE codeCust = '" + codeslctd + "'";
                            connectionDB.connection.Open();

                            string[] allQuery = { querydelete };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }

                            connectionDB.connection.Close();
                            CustControl custControl = new CustControl();
                            custControl.toolStripUsername.Text = toolStripUsername.Text;
                            custControl.Show();
                            this.Hide();
                            MessageBox.Show("Record Deleted successfully", "Customer Code Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            connectionDB.connection.Close();
                            MessageBox.Show("Unable to remove selected Customer Code", "Customer Code Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This role unable to delete Customer Code", "Customer Code Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                    
            }
        }

        private void CustControl_FormClosing(object sender, FormClosingEventArgs e)
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

        private void addCustCode_Click(object sender, EventArgs e)
        {
            if (tbCustCode.Text != "")
            {
                if (role == "super")
                {
                    addCustControl();
                    tbCustCode.Clear();
                    tbCustCode.Select();
                }
                else
                {
                    MessageBox.Show("This role unable to add Customer Code", "Customer Code Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbCustCode.Clear();
                    tbCustCode.Select();
                }
            }
        }

        private void tbCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tbCustCode.Text != "")
            {
                if (role == "super")
                {
                    addCustControl();
                    tbCustCode.Clear();
                    tbCustCode.Select();
                }
                else
                {
                    MessageBox.Show("This role unable to add Customer Code", "Customer Code Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbCustCode.Clear();
                    tbCustCode.Select();
                }
            }
        }
    }
}
