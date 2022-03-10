using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class PackingList : MaterialForm
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

        public PackingList()
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

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewPlList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("material LIKE '{0}%'", tbSearch.Text);
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

        private void LoadDataPackingList()
        {
            try
            {
                string query = "SELECT packingList, invoiceDate, shipterm, incoterm, paymentterm, portofloading, destination, importDate, importBy FROM tbl_packinglist ORDER BY id DESC";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewPlList.DataSource = dset.Tables[0];

                    // add button detail in datagridview table
                    DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                    dataGridViewPlList.Columns.Add(btnDetail);
                    btnDetail.HeaderText = "";
                    btnDetail.Text = "Detail";
                    btnDetail.Name = "btnDetail";
                    btnDetail.UseColumnTextForButtonValue = true;

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewPlList.Columns.Add(btnDelete);
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

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // remove data in datagridview result
            dataGridViewPlList.DataSource = null;
            dataGridViewPlList.Refresh();

            while (dataGridViewPlList.Columns.Count > 0)
            {
                dataGridViewPlList.Columns.RemoveAt(0);
            }

            LoadDataPackingList();
            dataGridViewPlList.Update();
            dataGridViewPlList.Refresh();
        }

        private void importPlButton_Click(object sender, EventArgs e)
        {
            ImportPackingList importPackingList = new ImportPackingList();
            importPackingList.toolStripUsername.Text = toolStripUsername.Text;
            importPackingList.Show();
            this.Hide();
        }

        private void PackingList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadDataPackingList();
        }

        private void PackingList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dataGridViewPlList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
            // Set table title
            string[] title = { "PACKING LIST", "INVOICE DATE", "SHIP TERM", "INCOTERM", "PAYMENT TERM", "PORT OF LOADING", "DESTINATION", "IMPORT DATE", "IMPORT BY" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewPlList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewPlList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewPlList.Columns[7].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";
        }

        private void dataGridViewPlList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewPlList.SelectedCells[0].RowIndex;
            string plslctd = dataGridViewPlList.Rows[i].Cells[0].Value.ToString();
            string invoiceDate = dataGridViewPlList.Rows[i].Cells[1].Value.ToString();
            string shipTerm = dataGridViewPlList.Rows[i].Cells[2].Value.ToString();
            string incoTerm = dataGridViewPlList.Rows[i].Cells[3].Value.ToString();
            string paymentTerm = dataGridViewPlList.Rows[i].Cells[4].Value.ToString();
            string portLoading = dataGridViewPlList.Rows[i].Cells[5].Value.ToString();
            string destination = dataGridViewPlList.Rows[i].Cells[6].Value.ToString();            

            if (e.ColumnIndex == 9)
            {
                DetailPackingList detailPackingList = new DetailPackingList();
                detailPackingList.toolStripUsername.Text = toolStripUsername.Text;
                detailPackingList.tbPackingListNo.Text = plslctd;
                detailPackingList.tbInvoiceDate.Text = invoiceDate;
                detailPackingList.tbShipTerm.Text = shipTerm;
                detailPackingList.tbIncoterms.Text = incoTerm;
                detailPackingList.tbPaymentTerm.Text = paymentTerm;
                detailPackingList.tbPortLoading.Text = portLoading;
                detailPackingList.tbDestination.Text = destination;
                detailPackingList.Show();
                this.Hide();
            }

            if (e.ColumnIndex == 10)
            {
                string message = "Do you want to delete this Packing List " + plslctd + "?";
                string title = "Delete Packing List";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);

                    string querydeletePO = "DELETE FROM tbl_packinglist WHERE packingList = '" + plslctd + "'";
                    string querydeletePODetail = "DELETE FROM tbl_packingdetail WHERE packingNo = '" + plslctd + "'";
                    connectionDB.connection.Open();

                    string[] allQuery = { querydeletePO, querydeletePODetail };
                    for (int j = 0; j < allQuery.Length; j++)
                    {
                        cmd.CommandText = allQuery[j];
                        //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                        cmd.ExecuteNonQuery();
                        //Jalankan perintah / query dalam CommandText pada database
                    }

                    connectionDB.connection.Close();
                    PackingList pl = new PackingList();
                    pl.toolStripUsername.Text = toolStripUsername.Text;
                    pl.Show();
                    this.Hide();
                    MessageBox.Show("Record Deleted successfully", "Packing List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                }
            }
        }
    }
}
