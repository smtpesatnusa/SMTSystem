using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class ScrapPartnumber : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();

        string idUser;

        public ScrapPartnumber()
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
            string message = "Are you sure you want to logout?";
            string title = "Confirm Logout";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            if (MetroMessageBox.Show(this, message, title, buttons, icon) == DialogResult.No)
                e.Cancel = true;
            else
                System.Windows.Forms.Application.ExitThread();
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
            //menampilkan data combobox department
            help.displayCmbList("SELECT * FROM tbl_department ORDER BY NAME ASC", "name", "name", cmbDepartment);

            //menampilkan data combobox request by
            help.displayCmbList("SELECT CONCAT(username, ' | ', NAME) AS NAMES, username FROM tbl_user ORDER BY NAME", "names", "username", cmbRequestBy);
        }

        private void tbscrapQty_TextChanged(object sender, EventArgs e)
        {
            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(tbscrapQty.Text, "[^0-9]"))
            {
                tbscrapQty.Text = tbscrapQty.Text.Remove(tbscrapQty.Text.Length - 1);
            }
        }

        private void tbPrfNo_TextChanged(object sender, EventArgs e)
        {
            if (tbPrfNo.Text != "")
            {
                //if user type alphabet
                if (System.Text.RegularExpressions.Regex.IsMatch(tbPrfNo.Text, "[^0-9]"))
                {
                    tbPrfNo.Text = tbPrfNo.Text.Remove(tbPrfNo.Text.Length - 1);
                }
            }
        }

        private void tbpnSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string pnSN = tbpnSN.Text.Trim();

                if (pnSN.Length >= 12 && pnSN.Length <= 22)
                {
                    string query = "SELECT material FROM tbl_masterpartmaterial WHERE material LIKE '%" + pnSN + "%'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                    {
                        DataTable dt = new DataTable();
                        adpt.Fill(dt);

                        // cek jika code tsb sudah di control
                        if (dt.Rows.Count > 0)
                        {
                            tbscrapQty.Select();
                        }
                        else
                        {
                            MessageBox.Show("Not Found Part No SN in Master Material, please fill properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void tbscrapQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbscrapQty.Text != "")
                {
                    tbPrfNo.Select();
                }
                else
                {
                    MessageBox.Show("Please fill scrap Qty properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void tbpnSN_TextChanged(object sender, EventArgs e)
        {
            tbpnSN.Text = tbpnSN.Text.ToUpper();
        }

        private void tbPrfNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbPrfNo.Text.Length == 7)
                {
                    cmbDepartment.Focus();
                }
                else
                {
                    MessageBox.Show("Please fill PRF No properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbPrfNo.Select();
                }
            }
        }

        private void cmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbDepartment.SelectedIndex != -1)
                {
                    cmbRequestBy.Focus();
                }
            }
        }

        private void cmbRequestBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbRequestBy.SelectedIndex != -1)
                {
                    tbPrint.Focus();
                }
            }
        }

        private void tbPrint_Click(object sender, EventArgs e)
        {

        }

        private void tbPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
            }
        }

        private void printlabel()
        {
            if (tbpnSN.Text.Length >= 12 && tbpnSN.Text.Length <= 22 && tbscrapQty.Text != "" 
                && tbPrfNo.Text.Length == 7 && cmbDepartment.SelectedIndex != -1 && cmbRequestBy.SelectedIndex != -1 )
            {

            }
            else
            {
                MessageBox.Show("Please fill scrap data properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
