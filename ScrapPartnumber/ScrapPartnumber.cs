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
            ScrapPartnumberList scrapPartnumberList = new ScrapPartnumberList();
            scrapPartnumberList.toolStripUsername.Text = toolStripUsername.Text;
            scrapPartnumberList.Show();
            this.Hide();
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

            tbPrfNo.Select();
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

        private void LoadDataPRF()
        {
            try
            {
                string query = "SELECT a.partnosn, b.description,a. qty, a.prfNo, a.department , b.f_type, b.location, " +
                    "a.updateDate FROM tbl_scrappart a, tbl_masterpartmaterial b WHERE a.partnosn = b.material AND " +
                    "a.prfNo LIKE '%"+tbPrfNo.Text+"%'";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewPRFList.DataSource = dset.Tables[0];
                }
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
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
                if (tbPrfNo.TextLength == 7)
                {
                    LoadDataPRF();
                }                
            }
        }

        private void tbpnSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string pnSN = tbpnSN.Text.Trim();
                string pn = pnSN.Remove(pnSN.Length - 3);

                if (pnSN.Length >= 12 && pnSN.Length <= 22)
                {
                    string query = "SELECT material FROM tbl_masterpartmaterial WHERE material LIKE '%" + pn + "%'";
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
                    tbPrint.Focus();
                }
                else
                {
                    MessageBox.Show("Please fill scrap Qty properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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
                    tbpnSN.Focus();
                }
            }
        }

        private void tbPrint_Click(object sender, EventArgs e)
        {
            printlabel();
        }

        private void tbPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                printlabel();
            }
        }

        private void printlabel()
        {
            if (tbpnSN.Text.Length >= 12 && tbpnSN.Text.Length <= 22 && tbscrapQty.Text != "" 
                && tbPrfNo.Text.Length == 7 && cmbDepartment.SelectedIndex != -1 && cmbRequestBy.SelectedIndex != -1 )
            {
                // insert to data to db
                try
                {
                    string pnSN = tbpnSN.Text.Trim();
                    string pn = pnSN.Remove(pnSN.Length - 3);
                    string qty = tbscrapQty.Text;
                    string prfNo = tbPrfNo.Text;
                    string department = cmbDepartment.Text;
                    var requested = cmbRequestBy.Text.Split('|');
                    string requestBy = requested[0].Replace(" ", "");

                    //MessageBox.Show(pnsn +" " +qty+ " " + prfNo + " " + department + " " + requestBy + " |" + idUser + "", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    string cekdata = "SELECT * FROM tbl_scrappart WHERE partnosn = '"+pn+ "' AND qty = '" + qty + "' AND prfno = '" + prfNo + "'AND department = '" + department + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekdata , connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to add scrap data, scrap data already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            
                        }
                        else
                        {
                            connectionDB.connection.Open();
                            var cmd = new MySqlCommand("", connectionDB.connection);
                            //insert  data to table
                            string insertdata = "INSERT INTO tbl_scrappart (partnosn, qty, prfno, department, requestedBy, issuedBy, updateDate) values " +
                                "('" + pn + "','" + qty + "','" + prfNo + "','" + department + "','" + requestBy + "','" + idUser + "','" + DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss") + "')";
                            cmd.CommandText = insertdata;
                            cmd.ExecuteNonQuery();
                            connectionDB.connection.Close();

                            //load data prf
                            LoadDataPRF();
                        }
                    }
                }
                catch (Exception ex)
                {
                    connectionDB.connection.Close();
                    // tampilkan pesan error
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please fill scrap data properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbNew_Click(object sender, EventArgs e)
        {
            resetInput();
        }

        private void resetInput()
        {
            tbpnSN.Clear();
            tbscrapQty.Clear();
            tbPrfNo.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbRequestBy.SelectedIndex = -1;

            // remove data in datagridview result
            dataGridViewPRFList.DataSource = null;
            dataGridViewPRFList.Refresh();

        }

        private void ScrapPartnumber_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dataGridViewPRFList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewPRFList.Columns[7].DefaultCellStyle.Format = "dd-MM-yyyy";
        }
    }
}
