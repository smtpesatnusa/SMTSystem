using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Z.BulkOperations;

namespace SMTPE
{
    public partial class ScrapPartnumberList : MaterialForm
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

        public ScrapPartnumberList()
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

        private void LoadData(string Sql)
        {
            try
            {
                connectionDB.connection.Open();

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

            if (dtSource.Rows.Count > 0)
            {
                // Copy the rows from the source table to fill the temporary table.
                for (int i = startRec; i <= endRec - 1; i++)
                {
                    dtTemp.ImportRow(dtSource.Rows[i]);
                    recNo++;
                }
            }

            dataGridViewScrapPartList.DataSource = dtTemp;

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


        private void tbTruncateMasterMaterial_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure want to delete All this Scrap Part Data ?";
            string title = "Delete Scrap Part";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result = MessageBox.Show(this, message, title, buttons, icon);
            if (result == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("", connectionDB.connection);

                string querydeletemastermaterial = "TRUNCATE tbl_scrappart";

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
                ScrapPartnumberList scrapPartnumberList = new ScrapPartnumberList();
                scrapPartnumberList.toolStripUsername.Text = toolStripUsername.Text;
                this.Hide();
                scrapPartnumberList.Show();
                MessageBox.Show(this, "Record Deleted successfully", "Scrap Part Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
            }
        }

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePickerFrom.Value = DateTime.Today;
            dateTimePickerTo.Value = DateTime.Today;

            Sql = "SELECT SUBSTRING(a.partnosn, 1, 2) AS custCode, a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department , " +
                    "(SELECT b.name FROM tbl_user b WHERE b.username = a.requestedby) requestedby, " +
                    "(SELECT b.name FROM tbl_user b WHERE b.username = a.issuedBy) issuedBy, DATE_FORMAT(a.updateDate, '%d-%m-%Y') AS updateDate FROM " +
                    "tbl_scrappart a, tbl_masterpartmaterial c WHERE a.partnosn = c.material AND statusDelete IS NULL ORDER BY a.id DESC";

            LoadData(Sql);
            dataGridViewScrapPartList.Update();
            dataGridViewScrapPartList.Refresh();
        }

        private void addScrapPartButton_Click(object sender, EventArgs e)
        {
            ScrapPartnumber scrapPartnumber = new ScrapPartnumber();
            scrapPartnumber.toolStripUsername.Text = toolStripUsername.Text;
            scrapPartnumber.Show();
            this.Hide();
        }

        private void ScrapPartnumberList_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            if (MetroMessageBox.Show(this, message, title, buttons, icon) == DialogResult.No)
                e.Cancel = true;
            else
                System.Windows.Forms.Application.ExitThread();
        }

        private void dataGridViewScrapPartList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (currentPage.ToString() == "1")
            {
                //memberi nomor row
                for (int i = 0; i < dataGridViewScrapPartList.Rows.Count; ++i)
                {
                    int row = i + 1;
                    dataGridViewScrapPartList.Rows[i].HeaderCell.Value = "" + row;
                }
            }
            else
            {
                //memberi nomor row
                for (int i = 0; i < dataGridViewScrapPartList.Rows.Count; ++i)
                {
                    int page = Convert.ToInt32(currentPage.ToString());
                    int temp = (page - 1) * 1000;
                    int row = temp + i + 1;
                    dataGridViewScrapPartList.Rows[i].HeaderCell.Value = "" + row;
                }
            }

            // set text capital in first letter
            if (e.ColumnIndex == 8 || e.ColumnIndex == 9)
            {
                e.Value = e.Value.ToString().ToUpper();
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                e.Value = textInfo.ToTitleCase(e.Value.ToString().ToLower());
            }

            // Set table title
            string[] title = { "CUST", "PART NO", "DESCRIPTION", "F.TYPE", "LOCATION", "QTY", "PRF NO", "DEPARTMENT", "REQUSTED BY", "ISSUED BY", "UPDATE DATE" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewScrapPartList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewScrapPartList.Columns[0].AutoSizeMode =  DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewScrapPartList.Columns[5].AutoSizeMode =  DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewScrapPartList.Columns[6].AutoSizeMode =  DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewScrapPartList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewScrapPartList.Columns[10].DefaultCellStyle.Format = "dd-MM-yyyy";

        }

        private void ScrapPartnumberList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // change datetimepicker format
            dateTimePickerFrom.CustomFormat = "dd-MM-yyyy";
            dateTimePickerTo.CustomFormat = "dd-MM-yyyy";

            // add all di cmb cust
            cmbCust.Items.Add("All");
            //menampilkan data combobox department
            help.displayCmbList("SELECT CONCAT(id, ' (', custname, ')') AS cust, id FROM tbl_customer ORDER BY id ", "cust", "id", cmbCust);

            Sql = "SELECT SUBSTRING(a.partnosn, 1, 2) AS custCode, a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department , " +
                "(SELECT b.name FROM tbl_user b WHERE b.username = a.requestedby) requestedby, (SELECT b.name FROM tbl_user b WHERE b.username = a.issuedBy) issuedBy, " +
                "DATE_FORMAT(a.updateDate, '%d-%m-%Y') AS updateDate FROM tbl_scrappart a, tbl_masterpartmaterial c WHERE a.partnosn = c.material " +
                "AND statusDelete IS NULL ORDER BY a.id DESC";

            // load data list
            LoadData(Sql);

            if (dataGridViewScrapPartList.Rows.Count > 0)
            {
                exportBtn.Enabled = true;
            }
        }

        private void Search()
        {
            try
            {
                if (tbSearch.Text == "")
                {
                    Sql = "SELECT SUBSTRING(a.partnosn, 1, 2) AS custCode, a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department , " +
                    "(SELECT b.name FROM tbl_user b WHERE b.username = a.requestedby) requestedby, " +
                    "(SELECT b.name FROM tbl_user b WHERE b.username = a.issuedBy) issuedBy, DATE_FORMAT(a.updateDate, '%d-%m-%Y') AS updateDate FROM " +
                    "tbl_scrappart a, tbl_masterpartmaterial c WHERE a.partnosn = c.material AND statusDelete IS NULL ORDER BY a.id DESC";
                }
                else
                {
                    Sql = "SELECT SUBSTRING(a.partnosn, 1, 2) AS custCode, a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department, " +
                        "(SELECT b.name FROM tbl_user b WHERE b.username = a.requestedby) requestedby, " +
                        "(SELECT b.name FROM tbl_user b WHERE b.username = a.issuedBy) issuedBy, DATE_FORMAT(a.updateDate, '%d-%m-%Y') AS updateDate FROM tbl_scrappart a, " +
                        "tbl_masterpartmaterial c WHERE a.partnosn = c.material AND statusDelete IS NULL AND partnosn LIKE '%" + tbSearch.Text + "%' " +
                        "OR DESCRIPTION LIKE '%" + tbSearch.Text + "%' OR f_type LIKE '%" + tbSearch.Text + "%' OR location LIKE '%" + tbSearch.Text + "%' " +
                        "OR qty LIKE '%" + tbSearch.Text + "%' OR prfNo LIKE '%" + tbSearch.Text + "%' OR department LIKE '%" + tbSearch.Text + "%' " +
                        "OR requestedby LIKE '%" + tbSearch.Text + "%' OR issuedBy LIKE '%" + tbSearch.Text + "%' OR updateDate LIKE '%" + tbSearch.Text + "%' ";

                }
                LoadDS(Sql);
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            exportExcelList();
        }

        private void exportExcelList()
        {
            try
            {
                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                directoryFile = directoryFile + "\\Scrap Part Summary\\" + date;
                string filename = "Summary Scrap Part "+help.randomText(5)+".xlsx";
                using (var workbook = new XLWorkbook())
                {
                    //worksheet SAP upload inbound
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                    worksheet.Rows().Style.Font.FontName = "Calibri";
                    worksheet.Rows().Style.Font.FontSize = 11;

                    //to show gridlines
                    worksheet.ShowGridLines = true;

                    // set column width
                    worksheet.Columns().Width = 13;
                    worksheet.Column(1).Width = 6;
                    worksheet.Column(2).Width = 6;
                    worksheet.Column(3).Width = 17;
                    worksheet.Column(4).Width = 44;
                    worksheet.Column(7).Width = 10;
                    //worksheet.Column(12).Style.NumberFormat.Format = "dd-MM-yyyy";           

                    worksheet.Rows().Height = 16.25;
                    worksheet.Row(1).Height = 25.5;
                    worksheet.Row(4).Height = 28;

                    worksheet.PageSetup.Margins.Top = 0.5;
                    worksheet.PageSetup.Margins.Bottom = 0.25;
                    worksheet.PageSetup.Margins.Left = 0.25;
                    worksheet.PageSetup.Margins.Right = 0;
                    worksheet.PageSetup.Margins.Header = 0.5;
                    worksheet.PageSetup.Margins.Footer = 0.25;
                    worksheet.PageSetup.CenterHorizontally = true;

                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 12)).Merge();
                    worksheet.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Font.FontSize = 20;
                    worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.Black;
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Value = "SUMMARY SCRAP PART REPORT";
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Font.Italic = true;
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet.Cell(2, 11).Value = "Report Date :";
                    worksheet.Cell(2, 12).Value = DateTime.Now.ToString("dd-MM-yyyy");

                    worksheet.Cell(4, 1).Value = "NO";
                    worksheet.Cell(4, 2).Value = "CUST";
                    worksheet.Cell(4, 3).Value = "PART NO";
                    worksheet.Cell(4, 4).Value = "DESCRIPTION";
                    worksheet.Cell(4, 5).Value = "F.TYPE";
                    worksheet.Cell(4, 6).Value = "LOCATION";
                    worksheet.Cell(4, 7).Value = "QTY";
                    worksheet.Cell(4, 8).Value = "PRF NO";
                    worksheet.Cell(4, 9).Value = "DEPARTMENT";
                    worksheet.Cell(4, 10).Value = "REQUESTED BY";
                    worksheet.Cell(4, 11).Value = "ISSUED BY";
                    worksheet.Cell(4, 12).Value = "UPDATE DATE";
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 12)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Cell(4, 1).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(4, 12).Style.Border.RightBorder = XLBorderStyleValues.Medium;

                    int cellRowIndex= 5;
                    int cellColumnIndex = 2;

                    try
                    {
                        connectionDB.connection.Open();
                        using (MySqlDataAdapter adpt = new MySqlDataAdapter(Sql, connectionDB.connection))
                        {
                            DataTable dt = new DataTable();
                            adpt.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                // storing Each row and column value to excel sheet  
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    for (int j = 0; j <= dt.Columns.Count - 1; j++)
                                    {
                                        worksheet.Cell(i + cellRowIndex, 1).Value = i + 1;
                                        worksheet.Cell(i + cellRowIndex, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        if (j == 10)
                                        {
                                            //worksheet.Cell(i + cellRowIndex, j + cellColumnIndex).Value = string.Format("{0}", dt.Rows[i][j].FormattedValue);
                                            worksheet.Cell(i + cellRowIndex, j + cellColumnIndex).Value = dt.Rows[i][j].ToString();
                                        }
                                        else
                                        {
                                            worksheet.Cell(i + cellRowIndex, j + cellColumnIndex).Value = dt.Rows[i][j].ToString();
                                        }
                                    }
                                }
                                int endPart = dt.Rows.Count + cellRowIndex;

                                // setup border 
                                worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 12)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(worksheet.Cell(cellRowIndex - 1, 2), worksheet.Cell(endPart - 1, 12)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 1)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                                worksheet.Range(worksheet.Cell(cellRowIndex, 12), worksheet.Cell(endPart - 1, 12)).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                                worksheet.Range(worksheet.Cell(endPart, 1), worksheet.Cell(endPart, 12)).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                                workbook.SaveAs(directoryFile + "\\" + filename);
                            }
                            else
                            {
                                workbook.SaveAs(directoryFile + "\\" + filename);
                            }
                        }
                        connectionDB.connection.Close();
                    }
                    catch (Exception ex)
                    {
                        connectionDB.connection.Close();
                        MessageBox.Show(ex.Message);
                    }
                }

                MessageBox.Show(this, "Excel File Success Generated", "Generate Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\" + filename);
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerTo.Value.Date < dateTimePickerFrom.Value.Date)
            {
                MessageBox.Show("From Date is greater than To Date");
                dateTimePickerTo.Value = dateTimePickerFrom.Value.AddDays(+1);
            }
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerTo.Value.Date < dateTimePickerFrom.Value.Date)
            {
                MessageBox.Show("From Date is greater than To Date");
                dateTimePickerTo.Value = dateTimePickerFrom.Value.AddDays(+1);
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {
            string from = DateTime.ParseExact(dateTimePickerFrom.Text, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("yyyy'-'MM'-'dd");
            string to = DateTime.ParseExact(dateTimePickerTo.Text, "dd'-'MM'-'yyyy", CultureInfo.InvariantCulture).ToString("yyyy'-'MM'-'dd");

            Sql = "SELECT SUBSTRING(a.partnosn, 1, 2) AS custCode, a.partnosn, c.description, c.f_type, c.location, a. qty, a.prfNo, a.department , " +
                "(SELECT b.name FROM tbl_user b WHERE b.username = a.requestedby) requestedby,  (SELECT b.name FROM tbl_user b WHERE " +
                "b.username = a.issuedBy) issuedBy, DATE_FORMAT(a.updateDate, '%d-%m-%Y') AS updateDate FROM tbl_scrappart a, tbl_masterpartmaterial c WHERE " +
                "a.partnosn = c.material AND statusDelete IS NULL AND updateDate BETWEEN '" + from + " 00:00:01' AND " +
                "'" + to + " 23:59:59' ORDER BY a.id DESC";

            LoadData(Sql);
            dataGridViewScrapPartList.Update();
            dataGridViewScrapPartList.Refresh();
        }
    }
}
