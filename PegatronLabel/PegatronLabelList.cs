using ClosedXML.Excel;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class PegatronLabelList : MaterialForm
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

        Helper help = new Helper();

        string idUser, dept, username, badgeId;

        public PegatronLabelList()
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

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
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
            currentPage--;

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

            currentPage++;

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

            //remove button
            while (dataGridViewBarcodeList.Columns.Count > 0)
            {
                dataGridViewBarcodeList.Columns.RemoveAt(0);
            }

            if (dtSource.Rows.Count > 0)
            {
                // Copy the rows from the source table to fill the temporary table.
                for (int i = startRec; i <= endRec - 1; i++)
                {
                    dtTemp.ImportRow(dtSource.Rows[i]);
                    recNo++;
                }
            }


            dataGridViewBarcodeList.DataSource = dtTemp;
            // add button print in datagridview table
            DataGridViewButtonColumn btnPrint = new DataGridViewButtonColumn();
            dataGridViewBarcodeList.Columns.Add(btnPrint);
            btnPrint.HeaderText = "";
            btnPrint.Text = "Print";
            btnPrint.Name = "btnPrint";
            btnPrint.UseColumnTextForButtonValue = true;

            // add button history in datagridview table
            DataGridViewButtonColumn btnHistory = new DataGridViewButtonColumn();
            dataGridViewBarcodeList.Columns.Add(btnHistory);
            btnHistory.HeaderText = "";
            btnHistory.Text = "History";
            btnHistory.Name = "btnHistory";
            btnHistory.UseColumnTextForButtonValue = true;

            // add button delete in datagridview table
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            dataGridViewBarcodeList.Columns.Add(btnDelete);
            btnDelete.HeaderText = "";
            btnDelete.Text = "Delete";
            btnDelete.Name = "btnDelete";
            btnDelete.UseColumnTextForButtonValue = true;

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
                PageCount++;

            // Initial seeings
            currentPage = 1;
            recNo = 0;

            // Display the content of the current page.
            LoadPage();
        }

        private void addScrapPartButton_Click(object sender, EventArgs e)
        {
            AddLabel addLabel = new AddLabel();
            addLabel.userdetail.Text = badgeId;
            addLabel.ShowDialog();
        }

        private void ScrapPartnumberList_FormClosing(object sender, FormClosingEventArgs e)
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


        private void dataGridViewQRCodeList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "ID", "Sequence", "WO Number", "Running Number", "Model", "Create Date", "Create By" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewBarcodeList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewBarcodeList.Columns[4].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm";
            dataGridViewBarcodeList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewBarcodeList.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewBarcodeList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            refresh();
        }

        private void refresh()
        {
            tbSearch.Clear();

            // remove data in datagridview result
            dataGridViewBarcodeList.DataSource = null;
            dataGridViewBarcodeList.Refresh();

            while (dataGridViewBarcodeList.Columns.Count > 0)
            {
                dataGridViewBarcodeList.Columns.RemoveAt(0);
            }

            loadData();

            dataGridViewBarcodeList.Update();
            dataGridViewBarcodeList.Refresh();
        }

        private void dataGridViewQRCodeList_Paint(object sender, PaintEventArgs e)
        {
            help.norecord_dgv(dataGridViewBarcodeList, e);
        }


        private void dataGridViewQRCodeList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewBarcodeList.SelectedCells[0].RowIndex;
            string idslctd = dataGridViewBarcodeList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 7)
            {
                PegatronLabel pegatronLabel = new PegatronLabel();
                pegatronLabel.idLabel.Text = idslctd;
                pegatronLabel.userdetail.Text = badgeId;
                pegatronLabel.ShowDialog();
            }
            if (e.ColumnIndex == 8)
            {
                HistoryPrintLabel historyPrintLabel = new HistoryPrintLabel();
                historyPrintLabel.idLabel.Text = idslctd;
                historyPrintLabel.ShowDialog();
            }
            if (e.ColumnIndex == 9)
            {
                string message = "Do you want to delete this Pegatron Label with ID " + idslctd + "?";
                string title = "Delete Pegatron Label";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var cmd = new MySqlCommand("", connectionDB.connection);

                        string querydelete = "DELETE FROM tbl_pegatronlabel WHERE id = '" + idslctd+"'";
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
                        MessageBox.Show("Record Deleted successfully", "Pegatron Label Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refresh();
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        //MessageBox.Show(ex.Message);
                        MessageBox.Show("Unable to remove Pegatron Label that already print", "Pegatron Label Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void PegatronLabelList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var user = toolStripUsername.Text.Split(',');
            idUser = user[0].Trim();
            var badge = idUser.Split(' ');
            int badgePosition = (badge.Length) - 1;

            // get badge ID employee and username
            badgeId = badge[badgePosition].Trim();
            username = idUser.Replace(" " + badgeId, "").Replace("Welcome ", "");

            loadData();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbSearch.Text == "")
                {
                    Sql = "SELECT a.id, a.sequence, a.woNumber, a.runningNumber, a.model, a.createDate, b.name FROM tbl_pegatronlabel a, " +
                        "tbl_user b WHERE a.createBy = b.username ORDER BY a.id DESC";
                }
                else
                {
                    Sql = "SELECT a.id, a.sequence, a.woNumber, a.runningNumber, a.model, a.createDate, b.name FROM tbl_pegatronlabel a, " +
                        "tbl_user b WHERE a.createBy = b.username and (a.id like '%" + tbSearch.Text + "%' OR a.sequence like '%" + tbSearch.Text + "%' " +
                        "OR a.woNumber like '%" + tbSearch.Text + "%' OR a.runningNumber like '%" + tbSearch.Text + "%' OR a.model like '%" + tbSearch.Text + "%' " +
                        "OR a.createDate like '%" + tbSearch.Text + "%' OR b.name like '%" + tbSearch.Text + "%')";
                }
                LoadDS(Sql);
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void loadData()
        {
            try
            {
                connectionDB.connection.Open();

                Sql = "SELECT a.id, a.sequence, a.woNumber, a.runningNumber, a.model, a.createDate, b.name FROM tbl_pegatronlabel a, tbl_user b WHERE a.createBy = b.username ORDER BY a.id DESC";

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
    }
}
