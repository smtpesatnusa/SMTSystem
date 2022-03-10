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
    public partial class DetailPackingList : MaterialForm
    {
        LoadForm lf = new LoadForm();
        ConnectionDB connectionDB = new ConnectionDB();

        public DetailPackingList()
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
            PackingList packingList = new PackingList();
            packingList.toolStripUsername.Text = toolStripUsername.Text;
            packingList.Show();
            this.Hide();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void LoadDataPackingList()
        {
            try
            {
                string query = "SELECT palletNo, projectmodel, soandline, poandline, partno, " +
                    "tbl_packingdetail.desc, model, qtyperctn, totalctn, totalqty, unit, cou," +
                    " netweight, grossweight, volume FROM tbl_packingdetail WHERE packingno = '"+tbPackingListNo.Text+"'";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewPlDetail.DataSource = dset.Tables[0];
                }

                totalLbl.Text = dataGridViewPlDetail.Rows.Count.ToString();
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
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

        private void dataGridViewPlDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //memberi nomor row
            for (int i = 0; i < dataGridViewPlDetail.Rows.Count; ++i)
            {
                int row = i + 1;
                dataGridViewPlDetail.Rows[i].HeaderCell.Value = "" + row;
            }

            // not allow to sort table
            for (int i = 0; i < dataGridViewPlDetail.Columns.Count; i++)
            {
                dataGridViewPlDetail.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridViewPlDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }
    }
}
