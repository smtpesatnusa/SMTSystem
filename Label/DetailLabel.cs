using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class DetailLabel : MaterialForm
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

        public DetailLabel()
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
                (dataGridViewDetailLabel.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("custname LIKE '{0}%' or processCode LIKE '{0}%' or sapMaterialNumber LIKE '{0}%' or plant LIKE '{0}%'" +
                    " or altBom LIKE '{0}%' or descr LIKE '{0}%' or sapPartNo LIKE '{0}%' or llModel LIKE '{0}%'" +
                    " or llDesc LIKE '{0}%' or apsModelCurrent LIKE '{0}%' or pcsModelCurrent LIKE '{0}%' or sideAMMSCurrent LIKE '{0}%' " +
                    " or CONVERT(pointLL, System.String) LIKE '{0}%' or CONVERT(pointSAP, System.String) LIKE '{0}%' or CONVERT(cavityPerPanel, System.String) LIKE '{0}%' " +
                    " or STATUS LIKE '{0}%' or loadingListRev LIKE '{0}%'", tbSearch.Text);
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
                    recNo = recNo + 1;
                }
            }

            dataGridViewDetailLabel.DataSource = dtTemp;
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
            pageSize = 500; // txtPageSize.Text
            maxRec = dtSource.Rows.Count;
            PageCount = maxRec / pageSize;

            // Adjust the page number if the last page contains a partial page.
            if ((maxRec % pageSize) > 0)
                PageCount = PageCount + 1;

            // Initial seeings
            currentPage = 1;
            recNo = 0;

            // Display the content of the current page.
            LoadPage();
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
            currentPage = currentPage - 1;

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

            currentPage = currentPage + 1;

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


        private void dataGridViewSAPModelMasterList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "ID", "YuanSuanBan",  "Model", "Wo",  "Version", "CreateDate"  ,"PrintDate","IsPrint", "Factory", "Import Date", "Import By", "Status" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewDetailLabel.Columns[i].HeaderText = "" + title[i];
            }

            if (currentPage.ToString() == "1")
            {
                //memberi nomor row
                for (int i = 0; i < dataGridViewDetailLabel.Rows.Count; ++i)
                {
                    int row = i + 1;
                    dataGridViewDetailLabel.Rows[i].HeaderCell.Value = "" + row;
                }
            }
            else
            {
                //memberi nomor row
                for (int i = 0; i < dataGridViewDetailLabel.Rows.Count; ++i)
                {
                    int page = Convert.ToInt32(currentPage.ToString());
                    int temp = (page - 1) * 500;
                    int row = temp + i + 1;
                    dataGridViewDetailLabel.Rows[i].HeaderCell.Value = "" + row;
                }
            }

            dataGridViewDetailLabel.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }


        private void LoadDataLabel()
        {
            try
            {
                connectionDB.connection.Open();

                Sql = "SELECT idIsn, YuanSuanBan, model, WO, VERSION, CreateDate, PrintDate, IsPrint, Factory, importDate, importBy, STATUS FROM tbl_labellist WHERE PO_No = '" + po_No.Text + "'";

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

        private void LabelList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            try
            {
                connectionDB.connection.Open();

                Sql = "SELECT idIsn, YuanSuanBan, model, WO, VERSION, CreateDate, PrintDate, IsPrint, Factory, importDate, importBy, STATUS FROM tbl_labellist WHERE PO_No = '" + po_No.Text+"'";

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

        private void DetailLabel_FormClosing(object sender, FormClosingEventArgs e)
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
            LabelList labelList = new LabelList();
            labelList.toolStripUsername.Text = toolStripUsername.Text;
            labelList.Show();
            this.Hide();
        }

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadDataLabel();

            dataGridViewDetailLabel.Update();
            dataGridViewDetailLabel.Refresh();
        }
    }
}
