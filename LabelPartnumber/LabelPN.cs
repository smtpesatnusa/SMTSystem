using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class LabelPN : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();

        string idUser;

        public LabelPN()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void LabelPartnumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MaterialDialog materialDialog = new MaterialDialog(this, title, message, "OK", true, "Cancel");
            DialogResult result = materialDialog.ShowDialog(this);
            if (result.ToString() == "OK")
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(result.ToString(), 750);
                SnackBarMessage.Show(this);
            }
        }

        private void LabelPartnumber_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");

            tbpnSN.Select();
        }

        private void tbpnSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codeCust;
                codeCust = tbpnSN.Text.Substring(0, 2);
                tbpnSN.Text = tbpnSN.Text.Trim();
                string pnSN = tbpnSN.Text.Trim();

                if (pnSN.Length >= 18 && pnSN.Length <= 22)
                {
                    string query = "SELECT codeCust FROM tbl_controlcust WHERE codeCust = '" + codeCust + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                    {
                        DataTable dt = new DataTable();
                        adpt.Fill(dt);

                        // cek jika code tsb sudah di control
                        if (dt.Rows.Count > 0)
                        {
                            tbpnCust.Select();
                        }
                        else
                        {
                            MessageBox.Show("Please control Part No SN in CORAMES", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tbpnSN.Clear();
                            tbpnSN.Select();
                        }
                    }                    
                }
                else
                {
                    MessageBox.Show("Wrong Part No SN, please fill properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbpnSN.Clear();
                    tbpnSN.Select();
                }
            }
        }

        private void tbpnCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbpnCust.Text = tbpnCust.Text.Trim();
                string pnCust = tbpnCust.Text.Trim();
                string pnSN = tbpnSN.Text.Trim();
                if (pnCust.Length >= 12 && pnCust.Length <= 15)
                {
                    if (pnSN.Length >= 18 && pnSN.Length <= 22)
                    {
                        // cek jika pnsn contain pncust
                        if (pnSN.Contains(pnCust) == true)
                        {
                            statusPN.BackColor = Color.ForestGreen;
                            statusPN.Text = "MATCH";
                            statusPN.Visible = true;
                            tbpnSN.Clear();
                            tbpnCust.Clear();
                            tbpnSN.Select();
                        }
                        else
                        {
                            statusPN.BackColor = Color.Red;
                            statusPN.Text = "WRONG PN";
                            statusPN.Visible = true;
                            tbpnSN.Clear();
                            tbpnCust.Clear();
                            tbpnSN.Select();
                        }

                        insertData(pnSN, pnCust);
                    }
                    else
                    {
                        MessageBox.Show("Wrong Part No SN, please fill properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbpnSN.Clear();
                        tbpnSN.Select();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Part No Cust, please fill properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbpnCust.Clear();
                    tbpnCust.Select();
                }
            }
        }

        private void insertData(string pnSN, string pnCust)
        {
            // insert to data to db
            try
            {
                connectionDB.connection.Open();
                var cmd = new MySqlCommand("", connectionDB.connection);
                //insert  data to table
                string insertdata = "INSERT INTO tbl_scanpn (partnosn, partnocust, status, createDate, createBy) values " +
                    "('" + pnSN + "','" + pnCust + "','" + statusPN.Text + "','" + DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";
                cmd.CommandText = insertdata;
                cmd.ExecuteNonQuery();
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }
    }
}
