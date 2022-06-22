using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class ScrapPartnumber : MaterialForm
    {
        private static BarTender.Application btApp = new BarTender.Application();
        private static BarTender.Format btFormat = new BarTender.Format();
        string dateNow = DateTime.Now.ToString("dd/MM/yyyy");

        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();

        string idUser;
        string desc;
        string cust;
        string ftype;
        string loc;
        string pnSN;
        string pn;
        string date;
        string qty;
        string printPn;

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
            btApp.Quit(BarTender.BtSaveOptions.btSaveChanges);
            ScrapPartnumberList scrapPartnumberList = new ScrapPartnumberList();
            scrapPartnumberList.toolStripUsername.Text = toolStripUsername.Text;
            scrapPartnumberList.Show();
            this.Hide();
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
                string query = "SELECT a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department , " +
                    "(SELECT CONCAT(a.requestedby, ' | ', b.name) FROM tbl_user b WHERE b.username = a.requestedby) requestedby, " +
                    "(SELECT b.name FROM tbl_user b WHERE b.username = a.issuedBy) issuedBy, a.updateDate FROM tbl_scrappart a, " +
                    "tbl_masterpartmaterial c WHERE a.partnosn = c.material AND statusDelete IS NULL AND prfNo = '" + tbPrfNo.Text + "'";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewPRFList.DataSource = dt;

                        string department = dt.Rows[0]["department"].ToString();
                        string requestedBy = dt.Rows[0]["requestedby"].ToString();

                        cmbDepartment.SelectedItem = department;
                        cmbRequestBy.SelectedItem = requestedBy;

                        cmbDepartment.Enabled = false;
                        cmbRequestBy.Enabled = false;

                        tbpnSN.Focus();
                    }
                    else
                    {
                        cmbDepartment.Enabled = true;
                        cmbRequestBy.Enabled = true;

                        cmbDepartment.SelectedIndex = -1;
                        cmbRequestBy.SelectedIndex = -1;
                        // remove data in datagridview result
                        dataGridViewPRFList.DataSource = null;
                        dataGridViewPRFList.Refresh();

                        cmbDepartment.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDetailPN()
        {
            try
            {
                string query = "SELECT (SELECT b.custname FROM tbl_masterpartmaterial a, tbl_customer b WHERE a.material = '" + pn + "' " +
                    "AND b.id = SUBSTRING(material, 1, 2)) custname, a.material, a.description, a.f_type, a.location FROM " +
                    "tbl_masterpartmaterial a WHERE a.material = '" + pn + "'";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cust = dt.Rows[0]["custname"].ToString();
                        desc = dt.Rows[0]["description"].ToString();
                        ftype = dt.Rows[0]["f_type"].ToString();
                        loc = dt.Rows[0]["location"].ToString();
                    }
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
                if (tbpnSN.Text.Length >= 10 && tbpnSN.Text.Length <= 30)
                {
                    string pnSN = tbpnSN.Text.Trim();
                    string pn = pnSN.Remove(pnSN.Length - 3);
                    string custCode = pn.Substring(0, 2);
                    int tpPosition = pnSN.Length - 4;

                    printPn = pnSN.Insert(tpPosition, "(").Insert(tpPosition + 2, ") ");

                    string query = "SELECT material FROM tbl_masterpartmaterial WHERE material = '" + pn + "'";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                    {
                        DataTable dt = new DataTable();
                        adpt.Fill(dt);

                        // cek jika code tsb sudah di control
                        if (dt.Rows.Count > 0)
                        {
                            string query1 = "SELECT a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department, " +
                                "(SELECT CONCAT(a.requestedby, ' | ', b.name) FROM tbl_user b WHERE b.username = a.requestedby) requestedby, " +
                                "(SELECT b.name FROM tbl_user b WHERE b.username = a.issuedBy) issuedBy, a.updateDate FROM tbl_scrappart a, " +
                                "tbl_masterpartmaterial c WHERE a.partnosn = c.material AND statusDelete IS NULL AND prfNo = '" + tbPrfNo.Text + "' AND partnosn = '" + pn + "'";

                            using (MySqlDataAdapter adpt1 = new MySqlDataAdapter(query1, connectionDB.connection))
                            {
                                DataTable dt1 = new DataTable();
                                adpt1.Fill(dt1);

                                // cek jika code tsb sudah di control
                                if (dt1.Rows.Count > 0)
                                {
                                    MessageBox.Show("Scrap Part already input under selected PRF, please check ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tbpnSN.Clear();
                                    tbpnSN.Focus();
                                }
                                else
                                {
                                    tbscrapQty.Select();
                                }
                            }
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
            if (tbpnSN.Text.Length >= 12 && tbpnSN.Text.Length <= 30 && tbscrapQty.Text != ""
                && tbPrfNo.Text.Length == 7 && cmbDepartment.SelectedIndex != -1 && cmbRequestBy.SelectedIndex != -1)
            {
                // insert to data to db
                try
                {
                    pnSN = tbpnSN.Text.Trim();
                    pn = pnSN.Remove(pnSN.Length - 3);
                    qty = tbscrapQty.Text;
                    string prfNo = tbPrfNo.Text;
                    string department = cmbDepartment.Text;
                    var requested = cmbRequestBy.Text.Split('|');
                    string requestBy = requested[0].Replace(" ", "");
                    date = dateNow;

                    string cekdata = "SELECT * FROM tbl_scrappart WHERE partnosn = '" + pn + "' AND qty = '" + qty + "' AND prfno = '" + prfNo + "'AND department = '" + department + "'AND statusDelete IS NULL";
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cekdata, connectionDB.connection))
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
                            try
                            {
                                connectionDB.connection.Open();
                                var cmd = new MySqlCommand("", connectionDB.connection);
                                //insert  data to table
                                string insertdata = "INSERT INTO tbl_scrappart (partnosn, partdetail, qty, prfno, department, requestedBy, issuedBy, updateDate) values " +
                                    "('" + pn + "','" + pnSN + "' ,'" + qty + "','" + prfNo + "','" + department + "','" + requestBy + "','" + idUser + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                                cmd.CommandText = insertdata;
                                cmd.ExecuteNonQuery();
                                connectionDB.connection.Close();

                                // load detail data
                                LoadDetailPN();
                                //printlabel ke printer
                                printLabeltoPrinter();

                                //load data prf
                                LoadDataPRF();

                                tbpnSN.Clear();
                                tbscrapQty.Clear();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return;
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
            }
            else
            {
                MessageBox.Show("Please fill scrap data properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void printLabeltoPrinter()
        {
            btFormat = btApp.Formats.Open(AppDomain.CurrentDomain.BaseDirectory + "scrapLabel.btw", false, "");

            btFormat.SetNamedSubStringValue("Cust", cust);
            btFormat.SetNamedSubStringValue("Date", date);
            btFormat.SetNamedSubStringValue("Desc", desc);
            btFormat.SetNamedSubStringValue("Loc", loc);
            btFormat.SetNamedSubStringValue("PN", pnSN);
            btFormat.SetNamedSubStringValue("Qty", qty);
            btFormat.SetNamedSubStringValue("PNDetail", printPn);

            // get default printer
            PrinterSettings settings = new PrinterSettings();
            string defaultPrinterName = settings.PrinterName;

            //printer selected
            btFormat.Printer = defaultPrinterName;

            //total copies
            int CopiesOfLabel = 1;
            btFormat.IdenticalCopiesOfLabel = CopiesOfLabel;

            btFormat.PrintOut(false, false);

            //to save the label when exiting
            btFormat.Close(BarTender.BtSaveOptions.btSaveChanges);
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
            cmbDepartment.Enabled = true;
            cmbRequestBy.Enabled = true;

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
            btApp.Quit(BarTender.BtSaveOptions.btSaveChanges);
        }

        private void dataGridViewPRFList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewPRFList.Columns[9].DefaultCellStyle.Format = "dd-MM-yyyy";
        }

        private void dataGridViewPRFList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.dataGridViewPRFList.SelectedRows.Count > 0)
                {
                    int i;
                    i = dataGridViewPRFList.SelectedCells[0].RowIndex;
                    string partslctd = dataGridViewPRFList.Rows[i].Cells[0].Value.ToString();
                    string prfslctd = dataGridViewPRFList.Rows[i].Cells[5].Value.ToString();

                    string message = "Do you want to delete this Scrap Part " + partslctd + "?";
                    string title = "Delete Scrap Part";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                    if (result == DialogResult.Yes)
                    {
                        this.dataGridViewPRFList.Rows.Remove(this.dataGridViewPRFList.SelectedRows[0]);
                        e.Handled = true;
                        try
                        {
                            var cmd = new MySqlCommand("", connectionDB.connection);

                            string queryupdateStatusPart = "UPDATE tbl_scrappart SET statusDelete ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ ","+idUser+"' WHERE partnosn = '" +partslctd+"' AND prfno = '"+prfslctd+"'";
                            connectionDB.connection.Open();

                            string[] allQuery = { queryupdateStatusPart };
                            for (int j = 0; j < allQuery.Length; j++)
                            {
                                cmd.CommandText = allQuery[j];
                                //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                cmd.ExecuteNonQuery();
                                //Jalankan perintah / query dalam CommandText pada database
                            }

                            connectionDB.connection.Close();
                            MessageBox.Show("Record Deleted successfully", "Scrap Part List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            connectionDB.connection.Close();
                            MessageBox.Show("Unable to remove selected Partnumber", "Scrap Part List Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void ScrapPartnumber_Load(object sender, EventArgs e)
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

        private void dataGridViewPRFList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridViewPRFList.SelectedRows.Count > 0)
            {
                int i;
                i = dataGridViewPRFList.SelectedCells[0].RowIndex;
                string partslctd = dataGridViewPRFList.Rows[i].Cells[0].Value.ToString();
                string prfslctd = dataGridViewPRFList.Rows[i].Cells[5].Value.ToString();

                string message = "Do you want to Reprint this Scrap Part " + partslctd + "?";
                string title = "Reprint Scrap Part";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    // get part with $$
                    try
                    {
                        string query = "SELECT partdetail, qty, updatedate FROM tbl_scrappart WHERE partnosn = '"+partslctd+"' AND prfNo = '"+prfslctd+"'";
                        using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                        {
                            DataTable dt = new DataTable();
                            adpt.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                string pndetail = dt.Rows[0]["partdetail"].ToString();
                                //date = dt.Rows[0]["updatedate"].ToString();
                                date = dateNow;
                                qty = dt.Rows[0]["qty"].ToString();
                                int tpPosition = pndetail.Length - 4;
                                printPn = pndetail.Insert(tpPosition, "(").Insert(tpPosition + 2, ") ");
                                pn = partslctd;
                                pnSN = pndetail;

                                // load detail pn to print
                                LoadDetailPN();
                                // print label
                                printLabeltoPrinter();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
