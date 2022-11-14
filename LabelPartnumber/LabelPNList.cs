using ClosedXML.Excel;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class LabelPNList : MaterialForm
    {
        string idUser;
        readonly LoadForm lf = new LoadForm();
        readonly ConnectionDB connectionDB = new ConnectionDB();
        private DataSet ds;
        private DataTable dtSource;
        private int PageCount;
        private int maxRec;
        private int pageSize;
        private int currentPage;
        private int recNo;
        private string Sql;

        public LabelPNList()
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
                if (tbSearch.Text == "")
                {
                    Sql = "SELECT  partnosn, partnocust, STATUS, createdate, createby FROM tbl_scanpn ORDER BY id DESC";
                }
                else
                {
                    Sql = "SELECT partnosn, partnocust, STATUS, createdate, createby FROM tbl_scanpn WHERE partnocust like '%" + tbSearch.Text + "%'OR " +
                        "partnosn LIKE '%" + tbSearch.Text + "%' or STATUS LIKE '%" + tbSearch.Text + "%'or createby LIKE '%" + tbSearch.Text + "%'";
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

        private void LabelList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");

            loadDataLabel();

            //set enable true if row datagridview > 0
            if (dataGridViewLabelPNList.Rows.Count > 0)
            {
                exportButton.Enabled = true;
            }
        }
        
        private void loadDataLabel()
        {
            try
            {
                connectionDB.connection.Open();

                Sql = "SELECT  partnosn, partnocust, STATUS, createdate, createby FROM tbl_scanpn ORDER BY id DESC";

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
            while (dataGridViewLabelPNList.Columns.Count > 0)
            {
                dataGridViewLabelPNList.Columns.RemoveAt(0);
            }

            if (dtSource.Rows.Count > 0)
            {
                // Copy the rows from the source table to fill the temporary table.
                for (int i = startRec; i <= endRec - 1; i++)
                {
                    dtTemp.ImportRow(dtSource.Rows[i]);
                    recNo ++;
                }
            }

            dataGridViewLabelPNList.DataSource = dtTemp;

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
            pageSize = 50; // txtPageSize.Text
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
        private void LabelList_FormClosing(object sender, FormClosingEventArgs e)
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
        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
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

        private void dataGridViewLabelPNList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //give back color if not wrong PN
            if (e.Value != null && e.Value.ToString() == "WRONG PN")
            {
                dataGridViewLabelPNList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            }

            //change date format
            dataGridViewLabelPNList.Columns[3].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            ExportReportToExcel();
        }

        private void ExportReportToExcel()
        {
            try
            {
                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                int totalRowData;
                directoryFile = directoryFile + "\\Report Label PN";
                using (var workbook = new XLWorkbook())
                {
                    // get data from sql and export to excel
                    try
                    {
                        string query = "SELECT  partnosn, partnocust, STATUS, createdate, createby FROM tbl_scanpn ORDER BY id DESC";
                        using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                        {
                            DataTable dt = new DataTable();
                            adpt.Fill(dt);

                            totalRowData = dt.Rows.Count;
                            if (dt.Rows.Count > 0)
                            {
                                var worksheet = workbook.Worksheets.Add("Sheet1");

                                //to hide gridlines
                                worksheet.ShowGridLines = false;

                                // set column width
                                worksheet.Column(1).Width = 7.43;
                                worksheet.Column(2).Width = 24.89;
                                worksheet.Column(3).Width = 24.89;
                                worksheet.Column(4).Width = 17.71;
                                worksheet.Column(5).Width = 15;
                                worksheet.Column(6).Width = 11;

                                worksheet.Rows().Height = 16.25;
                                worksheet.Row(1).Height = 25.5;

                                worksheet.PageSetup.Margins.Top = 0.5;
                                worksheet.PageSetup.Margins.Bottom = 0.25;
                                worksheet.PageSetup.Margins.Left = 0.25;
                                worksheet.PageSetup.Margins.Right = 0;
                                worksheet.PageSetup.Margins.Header = 0.5;
                                worksheet.PageSetup.Margins.Footer = 0.25;
                                worksheet.PageSetup.CenterHorizontally = true;

                                worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 6)).Merge();
                                worksheet.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                                worksheet.Cell(1, 1).Style.Font.Bold = true;
                                worksheet.Cell(1, 1).Style.Font.FontSize = 20;
                                worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.RoyalBlue;
                                worksheet.Cell(1, 1).Style.Font.Bold = true;
                                worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(1, 1).Value = "CHECKING PARTNUMBER REPORT";
                                worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Font.FontSize = 10;
                                worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Font.FontName = "Times New Roman";
                                worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Font.Italic = true;
                                worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, 6)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                                worksheet.Cell(2, 5).Value = "Report Date :";
                                worksheet.Cell(2, 6).Value = DateTime.Now.ToString("dd-MM-yyyy");
                                worksheet.Cell(3, 5).Value = "By :";
                                worksheet.Cell(3, 6).Value = idUser;

                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Font.FontName = "Times New Roman";
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Font.FontSize = 10;
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Font.Bold = true;
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Fill.BackgroundColor = XLColor.Yellow;
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                                worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                                worksheet.Cell(4, 1).Value = "NO";
                                worksheet.Cell(4, 2).Value = "PARTNUMBER SN";
                                worksheet.Cell(4, 3).Value = "PARTNUMBER CUST";
                                worksheet.Cell(4, 4).Value = "STATUS";
                                worksheet.Cell(4, 5).Value = "CREATE DATE";
                                worksheet.Cell(4, 6).Value = "CREATE BY";
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(worksheet.Cell(4, 1), worksheet.Cell(4, 6)).Style.Border.BottomBorder = XLBorderStyleValues.Double;
                                worksheet.Cell(4, 1).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                                worksheet.Cell(4, 6).Style.Border.RightBorder = XLBorderStyleValues.Medium;

                                int cellRowIndex = 5;
                                int cellColumnIndex = 1;

                                worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(totalRowData + cellRowIndex, 6)).Style.Font.FontName = "Times New Roman";
                                worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(totalRowData + cellRowIndex, 6)).Style.Font.FontSize = 9;

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dt.Columns.Count; j++)
                                    {
                                        worksheet.Cell(cellRowIndex + i, cellColumnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                                        worksheet.Cell(cellRowIndex + i, cellColumnIndex).Value = i + 1;
                                        worksheet.Cell(cellRowIndex + i, cellColumnIndex + j+1).Value = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                                    }
                                }

                                int endPart = totalRowData + cellRowIndex;

                                // setup border 
                                worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 6)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(worksheet.Cell(cellRowIndex-1, 2), worksheet.Cell(endPart - 1, 6)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 1)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                                worksheet.Range(worksheet.Cell(cellRowIndex, 6), worksheet.Cell(endPart - 1, 6)).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                                worksheet.Range(worksheet.Cell(endPart, 1), worksheet.Cell(endPart, 6)).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                                workbook.SaveAs(directoryFile + "\\" + DateTime.Now.ToString("MM-dd-yyyy") + ".xlsx");
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
                MessageBox.Show(this, "Excel File Success Export", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\" + DateTime.Now.ToString("MM-dd-yyyy") + ".xlsx");
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }
    }
}
