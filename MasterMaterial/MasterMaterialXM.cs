using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Z.BulkOperations;

namespace SMTPE
{
    public partial class MasterMaterialXM : MaterialForm
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

        public MasterMaterialXM()
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
                if (tbSearch.Text == "")
                {
                    Sql = "SELECT material, importDate, importBy FROM tbl_mastermaterial ORDER BY id";
                }
                else
                {
                    Sql = "SELECT material, importDate, importBy FROM tbl_mastermaterial WHERE material like '%" + tbSearch.Text + "%'" +
                        "or importBy LIKE '%" + tbSearch.Text + "%'";
                }
                LoadDS(Sql);
                FillGrid();
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

        private void importMMButton_Click(object sender, EventArgs e)
        {
            ImportMasterMaterialXM iMM = new ImportMasterMaterialXM();
            iMM.toolStripUsername.Text = toolStripUsername.Text;
            iMM.Show();
            this.Hide();
        }

        private void MasterMaterial_FormClosing(object sender, FormClosingEventArgs e)
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

        private void MasterMaterial_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadDataMasterMaterial();
        }

        private void LoadDataMasterMaterial()
        {
            try
            {
                connectionDB.connection.Open();

                Sql = "SELECT material, importDate, importBy FROM tbl_mastermaterial ORDER BY id";

                StartProgress("Loading...");

                LoadDS(Sql);
                FillGrid();

                string record = dtSource.Rows.Count.ToString();

                CloseProgress();

                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

            private void btnFirstPage_Click(object sender, EventArgs e)
        {
            if (!CheckFillButton())
                return;

            // Check if you are already at the first page.
            if (currentPage == 1)
            {
                MessageBox.Show("You are at the First Page!");
                return;
            }

            currentPage = 1;
            recNo = 0;

            LoadPage();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            currentPage --;

            // Check if you are already at the first page.
            if (currentPage < 1)
            {
                MessageBox.Show("You are at the First Page!");
                currentPage = 1;
                return;
            }
            else
                recNo = pageSize * (currentPage - 1);

            LoadPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {

            // Check if the user clicked the "Fill Grid" button.
            if (pageSize == 0)
            {
                MessageBox.Show("Set the Page Size, and then click the \"Fill Grid\" button!");
                return;
            }

            currentPage ++;

            if (currentPage > PageCount)
            {
                currentPage = PageCount;

                // Check if you are already at the last page.
                if (recNo == maxRec)
                {
                    MessageBox.Show("You are at the Last Page!");
                    return;
                }
            }

            LoadPage();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (!CheckFillButton())
                return;

            // Check if you are already at the last page.
            if (recNo == maxRec)
            {
                MessageBox.Show("You are at the Last Page!");
                return;
            }

            currentPage = PageCount;

            recNo = pageSize * (currentPage - 1);

            LoadPage();
        }
        private bool CheckFillButton()
        {
            // Check if the user clicks the "Fill Grid" button.
            if (pageSize == 0)
            {
                MessageBox.Show("Set the Page Size, and then click the \"Fill Grid\" button!");
                return false;
            }
            else
                return true;
        }
        private void LoadPage()
        {
            int startRec;
            int endRec;
            DataTable dtTemp;

            // Duplicate or clone the source table to create the temporary table.
            dtTemp = dtSource.Clone();

            if (currentPage == PageCount)
                endRec = maxRec;
            else
                endRec = pageSize * currentPage;

            startRec = recNo;

            if (dtSource.Rows.Count > 0)
            {
                // Copy the rows from the source table to fill the temporary table.
                for (int i = startRec; i <= endRec - 1; i++)
                {
                    dtTemp.ImportRow(dtSource.Rows[i]);
                    recNo ++;
                }
            }

            dataGridViewMasterMaterialList.DataSource = dtTemp;

            DisplayPageInfo();
        }

        private void DisplayPageInfo()
        {
            txtDisplayPageNo.Text = "Page " + currentPage.ToString() + "/ " + PageCount.ToString();
        }

        private void LoadDS(string SQL)
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(SQL, connectionDB.connection);
                ds = new DataSet();

                // Fill the DataSet.
                da.Fill(ds, "Items");

                // Set the source table.
                dtSource = ds.Tables["Items"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillGrid()
        {

            // Set the start and max records. 
            pageSize = 1000; // txtPageSize.Text
            maxRec = dtSource.Rows.Count;
            PageCount = maxRec / pageSize;

            // Adjust the page number if the last page contains a partial page.
            if ((maxRec % pageSize) > 0)
                PageCount ++;

            // Initial seeings
            currentPage = 1;
            recNo = 0;

            // Display the content of the current page.
            LoadPage();
        }

        private void dataGridViewMasterMaterialList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (currentPage.ToString() == "1")
            {
                //memberi nomor row
                for (int i = 0; i < dataGridViewMasterMaterialList.Rows.Count; ++i)
                {
                    int row = i + 1;
                    dataGridViewMasterMaterialList.Rows[i].HeaderCell.Value = "" + row;
                }
            }
            else
            {
                //memberi nomor row
                for (int i = 0; i < dataGridViewMasterMaterialList.Rows.Count; ++i)
                {
                    int page = Convert.ToInt32(currentPage.ToString());
                    int temp = (page - 1) * 1000;
                    int row = temp + i + 1;
                    dataGridViewMasterMaterialList.Rows[i].HeaderCell.Value = "" + row;
                }
            }

            // Set table title
            string[] title = { "MATERIAL", "IMPORT DATE", "IMPORT BY" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewMasterMaterialList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewMasterMaterialList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewMasterMaterialList.Columns[1].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";

        }

        private void tbTruncateMasterMaterial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure want to delete All this Master Material Data ?";
            string title = "Delete Master Material";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result = MessageBox.Show(this, message, title, buttons, icon);
            if (result == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("", connectionDB.connection);

                string querydeletemastermaterial = "TRUNCATE tbl_mastermaterial";

                connectionDB.connection.Open();

                string[] allQuery = { querydeletemastermaterial };
                for (int j = 0; j < allQuery.Length; j++)
                {
                    cmd.CommandText = allQuery[j];
                    //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                    cmd.ExecuteNonQuery();
                    //Jalankan perintah / query dalam CommandText pada database
                }

                connectionDB.connection.Close();
                MasterMaterialXM masterMaterial = new MasterMaterialXM();
                masterMaterial.toolStripUsername.Text = toolStripUsername.Text;
                this.Hide();
                masterMaterial.Show();
                MessageBox.Show(this, "Record Deleted successfully", "Master Material Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
            }
        }

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadDataMasterMaterial();
            dataGridViewMasterMaterialList.Update();
            dataGridViewMasterMaterialList.Refresh();
        }
    }
}
