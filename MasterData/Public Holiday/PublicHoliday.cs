using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class PublicHoliday : MaterialForm
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

        public PublicHoliday()
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
            while (dataGridViewPublicHolidayList.Columns.Count > 0)
            {
                dataGridViewPublicHolidayList.Columns.RemoveAt(0);
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

            dataGridViewPublicHolidayList.DataSource = dtTemp;

            //// add button edit in datagridview table
            //DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            //dataGridViewPublicHolidayList.Columns.Add(btnEdit);
            //btnEdit.HeaderText = "";
            //btnEdit.Text = "Edit";
            //btnEdit.Name = "btnEdit";
            //btnEdit.UseColumnTextForButtonValue = true;

            // add button delete in datagridview table
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            dataGridViewPublicHolidayList.Columns.Add(btnDelete);
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

        private void LoadData()
        {
            try
            {
                connectionDB.connection.Open();

                Sql = "SELECT name, date FROM tbl_masterholiday ORDER BY id desc";

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

        private void refresh()
        {
            tbSearch.Clear();

            // remove data in datagridview result
            dataGridViewPublicHolidayList.DataSource = null;
            dataGridViewPublicHolidayList.Refresh();

            while (dataGridViewPublicHolidayList.Columns.Count > 0)
            {
                dataGridViewPublicHolidayList.Columns.RemoveAt(0);
            }

            LoadData();

            dataGridViewPublicHolidayList.Update();
            dataGridViewPublicHolidayList.Refresh();
        }

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            refresh();
        }

        private void truncatePublicHolidayLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure want to delete All this Public Holiday Data ?";
            string title = "Delete Public Holiday";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result = MessageBox.Show(this, message, title, buttons, icon);
            if (result == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("", connectionDB.connection);

                string querydelete = "TRUNCATE tbl_masterholiday";

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
                PublicHoliday publicHoliday = new PublicHoliday();
                publicHoliday.toolStripUsername.Text = toolStripUsername.Text;
                this.Hide();
                publicHoliday.Show();
                MessageBox.Show(this, "Record Deleted successfully", "Public Holiday Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewPublicHolidayList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "NAME", "DATE" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewPublicHolidayList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewPublicHolidayList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewPublicHolidayList.Columns[1].DefaultCellStyle.Format = "dddd, dd MMMM yyyy";
        }

        private void PublicHoliday_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadData();
        }

        private void PublicHoliday_FormClosing(object sender, FormClosingEventArgs e)
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

        private void addBtn_Click(object sender, EventArgs e)
        {
            AddPublicHoliday addPublic = new AddPublicHoliday();
            addPublic.usernameLbl.Text = toolStripUsername.Text;
            addPublic.ShowDialog();
        }

        private void dataGridViewPublicHolidayList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewPublicHolidayList.SelectedCells[0].RowIndex;
            string nameslctd = dataGridViewPublicHolidayList.Rows[i].Cells[0].Value.ToString();
            string dateslctd = dataGridViewPublicHolidayList.Rows[i].Cells[1].Value.ToString();

            DateTime dte = Convert.ToDateTime(dateslctd);
            dateslctd = dte.ToString("yyyy-MM-dd");
            //if (e.ColumnIndex == 2)
            //{
            //    //EditPublicHoliday editPublicHoliday = new EditPublicHoliday();
            //    //editPublicHoliday.usernameLbl.Text = toolStripUsername.Text;
            //    //editPublicHoliday.tbModel.Text = modeluphslctd;
            //    //editPublicHoliday.tbUph.Text = uphslctd;
            //    //editPublicHoliday.ShowDialog();
            //}
            if (e.ColumnIndex == 2)
            {
                string message = "Do you want to delete this Public Holiday with name " + nameslctd + "?";
                string title = "Delete Public Holiday";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var cmd = new MySqlCommand("", connectionDB.connection);

                        string querydelete = "DELETE FROM tbl_masterholiday WHERE date = '" + dateslctd + "'";
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
                        MessageBox.Show("Record Deleted successfully", "Public Holiday List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refresh();
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show("Unable to remove selected Public Holiday", "Public Holiday List Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Replace("'", "''");

                if (tbSearch.Text == "")
                {
                    Sql = "SELECT name, date FROM tbl_masterholiday ORDER BY id desc";
                }
                else
                {
                    Sql = "SELECT name, date FROM tbl_masterholiday WHERE name like '%" + search + "%'OR " +
                        "date LIKE '%" + search + "%'";
                }

                LoadDS(Sql);
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
