using ClosedXML.Excel;
using MaterialSkin.Controls;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace SMTPE
{
    public partial class DetailForecastList : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();

        int totalDay;
        string monthFile;
        int yearFile;
        int year;
        int month;

        int row;
        int firstDataPosition;

        public DetailForecastList()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ForecastList forecastList = new ForecastList();
            forecastList.toolStripUsername.Text = toolStripUsername.Text;
            forecastList.Show();
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
                // get month year datepicker
                string _Date = tbMonthYear.Text;
                DateTime dt = Convert.ToDateTime(_Date);
                year = Convert.ToInt32(dt.ToString("yyyy"));
                month = Convert.ToInt32(dt.ToString("MM"));
                monthFile = dt.ToString("MMM");
                yearFile = Convert.ToInt32(dt.ToString("yyyy"));

                totalDay = DateTime.DaysInMonth(year, month);

                // to run qry statement based on total day
                string qry = "";

                for (int i = 1; i <= totalDay; i++)
                {
                    qry += "SUM(a.date" + i + ") AS '" + i + "' ,";
                }
                qry = qry.Remove(qry.Length - 1);
                //-----------

                string query = "SELECT a.model, c.uph, " + qry + " FROM tbl_forecastdetail a, tbl_forecastlist b, tbl_masteruph c, tbl_model d WHERE b.id = a.forecastid AND c.model = d.id AND a.model = d.model AND " +
                    "b.forecastlist = '" + tbForecastListNo.Text + "' GROUP BY a.forecastid, a.model, c.uph ";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewFCT.DataSource = dset.Tables[0];
                }

                // to run qry statement based on total day
                string qry2 = "";

                for (int i = 1; i <= totalDay; i++)
                {
                    qry2 += "ROUND(SUM(a.date" + i + ")/(c.uph*7.17),2) AS '" + i + "' ,";
                }
                qry2 = qry2.Remove(qry2.Length - 1);
                //-----------

                string query2 = "SELECT a.model, c.uph, " + qry2 + " FROM tbl_forecastdetail a, tbl_forecastlist b, tbl_masteruph c, tbl_model d WHERE b.id = a.forecastid " +
                    "AND c.model = d.id AND a.model = d.model AND b.forecastlist = '" + tbForecastListNo.Text + "' GROUP BY a.forecastid, a.model, c.uph ";

                using (MySqlDataAdapter adpt2 = new MySqlDataAdapter(query2, connectionDB.connection))
                {
                    DataSet dset2 = new DataSet();
                    adpt2.Fill(dset2);

                    dataGridViewFCT2.DataSource = dset2.Tables[0];
                }
                totalLbl.Text = dataGridViewFCT.Rows.Count.ToString();
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            exportExcel();
        }

        private void dataGridViewFCT_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //memberi nomor row
            for (int i = 0; i < dataGridViewFCT.Rows.Count; ++i)
            {
                int row = i + 1;
                dataGridViewFCT.Rows[i].HeaderCell.Value = "" + row;
            }

            // not allow to sort table
            for (int i = 0; i < dataGridViewFCT.Columns.Count; i++)
            {
                dataGridViewFCT.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridViewFCT.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewFCT.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void DetailForecastList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void DetailForecastList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);

            LoadData();
        }

        private void exportExcel()
        {
            try
            {
                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                directoryFile = directoryFile + "\\ALFASYS\\FCT SMT\\" + tbForecastListNo.Text;
                string filename = tbForecastListNo.Text + ".xlsx";
                using (var workbook = new XLWorkbook())
                {
                    #region worksheet1
                    // worksheet 1
                    var worksheet = workbook.Worksheets.Add(tbForecastListNo.Text);
                    worksheet.Rows().Style.Font.FontName = "Calibri";
                    worksheet.Rows().Style.Font.FontSize = 11;

                    //to show gridlines
                    worksheet.ShowGridLines = false;

                    //set column width
                    worksheet.Columns().Width = 10;
                    worksheet.Column(1).Width = 4;
                    worksheet.Column(2).Width = 12;
                    worksheet.Column(3).Width = 12;
                    worksheet.Column(4).Width = 12;

                    worksheet.Rows().Height = 22;

                    worksheet.PageSetup.Margins.Top = 0.5;
                    worksheet.PageSetup.Margins.Bottom = 0.25;
                    worksheet.PageSetup.Margins.Left = 0.25;
                    worksheet.PageSetup.Margins.Right = 0;
                    worksheet.PageSetup.Margins.Header = 0.5;
                    worksheet.PageSetup.Margins.Footer = 0.25;
                    worksheet.PageSetup.CenterHorizontally = true;

                    // set text center
                    worksheet.Range(worksheet.Cell(2, 2), worksheet.Cell(3, 35)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell(2, 2), worksheet.Cell(3, 35)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Range(worksheet.Cell(2, 2), worksheet.Cell(2, 4)).Merge();
                    worksheet.Cell(2, 2).Value = "PTSN CKD";
                    worksheet.Cell(3, 2).Value = "MODEL";
                    worksheet.Cell(3, 4).Value = "UPH";

                    // set style for header
                    // set dok number format
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(2, totalDay + 4)).Style.NumberFormat.Format = "d-mmm";

                    // set border 
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, totalDay + 4)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, totalDay + 4)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    // set color header
                    worksheet.Range(worksheet.Cell(2, 2), worksheet.Cell(3, 5)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.Black;
                    worksheet.Range(worksheet.Cell(2, 5), worksheet.Cell(3, totalDay + 4)).Style.Font.FontColor = XLColor.White;
                    worksheet.Range(worksheet.Cell(2, 2), worksheet.Cell(3, totalDay + 4)).Style.Font.Bold = true;

                    // date 1 to 31
                    for (int i = 1; i <= totalDay; i++)
                    {
                        string date = monthFile + "/" + i + "/" + yearFile;
                        DateTime dte = Convert.ToDateTime(date);
                        worksheet.Cell(2, 4 + i).Value = date;
                        worksheet.Cell(3, 4 + i).Value = dte.ToString("ddd");

                        // set color red if day is sunday
                        if (worksheet.Cell(3, 4 + i).Value.ToString() == "Sun")
                        {
                            worksheet.Range(worksheet.Cell(2, 4 + i), worksheet.Cell(3, 4 + i)).Style.Fill.BackgroundColor = XLColor.Red;
                            worksheet.Range(worksheet.Cell(2, 4 + i), worksheet.Cell(3, 4 + i)).Style.Font.FontColor = XLColor.Black;
                        }
                        else if (worksheet.Cell(3, 4 + i).Value.ToString() == "Sat")
                        {
                            worksheet.Range(worksheet.Cell(2, 4 + i), worksheet.Cell(3, 4 + i)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 255, 0);
                            worksheet.Range(worksheet.Cell(2, 4 + i), worksheet.Cell(3, 4 + i)).Style.Font.FontColor = XLColor.Black;
                        }

                        // give  cololor blue if public holiday
                        string menu = "SELECT NAME, DATE FROM tbl_masterholiday WHERE DATE = '" + year + "-" + month + "-" + i + "'";
                        using (MySqlDataAdapter adpt = new MySqlDataAdapter(menu, connectionDB.connection))
                        {
                            DataTable dt = new DataTable();
                            adpt.Fill(dt);
                            //cek jika ada tanggal merah
                            if (dt.Rows.Count > 0)
                            {
                                worksheet.Range(worksheet.Cell(2, 4 + i), worksheet.Cell(3, 4 + i)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 176, 240);
                                worksheet.Range(worksheet.Cell(2, 4 + i), worksheet.Cell(3, 4 + i)).Style.Font.FontColor = XLColor.Black;
                            }
                        }
                    }

                    int cellRowIndex = 4;
                    int cellColumnIndex = 3;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewFCT.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewFCT.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cell(i + cellRowIndex, 2).Value = dataGridViewFCT.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                worksheet.Cell(i + cellRowIndex, j + cellColumnIndex).Value = dataGridViewFCT.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }

                    int endPart = dataGridViewFCT.Rows.Count + cellRowIndex;
                    int endCell = dataGridViewFCT.Rows.Count + 3;

                    // set dok number format
                    worksheet.Range(worksheet.Cell(4, 5), worksheet.Cell(endPart + 1, totalDay + 4)).Style.NumberFormat.Format = "#,###;-#,###;-";
                    // give color tan in demand result
                    worksheet.Range(worksheet.Cell(endPart, 2), worksheet.Cell(endPart, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.FromArgb(197, 190, 151);
                    worksheet.Cell(endPart, 3).Style.Font.Bold = true;
                    worksheet.Cell(endPart, 3).Value = "TOTAL DEMAND QTY";

                    #region insert data total demand Qty
                    string[] column1 = { "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ" };
                    for (int i = 0; i < totalDay; i++)
                    {
                        if (i < column1.Length - 1)
                        {
                            if (i == 13)
                            {
                                worksheet.Cell(endPart, i + 5).FormulaA1 = "=SUM(" + column1[i] + cellRowIndex + ":" + column1[i] + endCell + ")";
                            }
                            else
                            {
                                worksheet.Cell(endPart, i + 5).FormulaR1C1 = "=SUM(" + column1[i] + cellRowIndex + ":" + column1[i] + endCell + ")";
                            }
                        }
                    }
                    #endregion                    

                    #region insert total data
                    int cellDemand = dataGridViewFCT.Rows.Count + cellRowIndex;
                    int nextcellDemand = cellDemand + 1;

                    // give color yellow in total result
                    worksheet.Range(worksheet.Cell(endPart + 1, 5), worksheet.Cell(endPart + 1, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.Yellow;

                    string[] column = { "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI" };
                    for (int i = 0; i < totalDay; i++)
                    {
                        if (i < totalDay)
                        {
                            worksheet.Cell(endPart + 1, i + 5).FormulaR1C1 = "=" + column[i + 1] + cellDemand + "+" + column[i] + nextcellDemand + "";
                        }
                    }
                    #endregion

                    int endFCT1 = endPart + 3;
                    int endPart2 = dataGridViewFCT2.Rows.Count + endFCT1;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewFCT2.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewFCT2.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cell(i + endFCT1, 2).Value = dataGridViewFCT2.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                worksheet.Cell(i + endFCT1, j + cellColumnIndex).Value = dataGridViewFCT2.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }

                    // give color tan 
                    worksheet.Range(worksheet.Cell(endPart2, 2), worksheet.Cell(endPart2, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.FromArgb(197, 190, 151);
                    worksheet.Cell(endPart2, 3).Style.Font.Bold = true;
                    worksheet.Cell(endPart2, 3).Value = "TOTAL TEAM REQUIRED";

                    int rowtotal = endPart2 - 1;

                    //change number format
                    worksheet.Range(worksheet.Cell(endFCT1, 5), worksheet.Cell(rowtotal, totalDay + 4)).Style.NumberFormat.Format = "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)";
                    worksheet.Range(worksheet.Cell(endPart2, 5), worksheet.Cell(1000, totalDay + 4)).Style.NumberFormat.Format = "_(* #,#0.0_);_(* (#,#0.0);_(* \"-\"??_);_(@_)";

                    #region insert sum data total team required
                    string[] column2 = {  "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ" };
                    for (int i = 0; i < totalDay; i++)
                    {
                        if (i < totalDay)
                        {
                            if (i == 13)
                            {
                                worksheet.Cell(endPart2, i + 5).FormulaA1 = "=SUM(" + column2[i] + endFCT1 + ":" + column2[i] + rowtotal + ")";
                            }
                            else
                            {
                                worksheet.Cell(endPart2, i + 5).FormulaR1C1 = "=SUM(" + column2[i] + endFCT1 + ":" + column2[i] + rowtotal + ")";
                            }
                        }
                    }
                    #endregion

                    worksheet.Cell(endPart2 + 1, 3).Value = "CURRENT TEAM";
                    // give color red 
                    worksheet.Range(worksheet.Cell(endPart2 + 2, 2), worksheet.Cell(endPart2 + 2, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 185, 184);
                    worksheet.Range(worksheet.Cell(endPart2 + 4, 5), worksheet.Cell(endPart2 + 4, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 185, 184);
                    worksheet.Cell(endPart2 + 2, 3).Value = "SHORTAGE TEAM";

                    #region insert formula Shortage Team
                    int requiredrow = endPart2;
                    int currentrow = requiredrow + 1;

                    string[] column3 = { "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ" };
                    for (int i = 0; i < totalDay; i++)
                    {
                        if (i < totalDay)
                        {
                            worksheet.Cell(endPart2 + 2, i + 5).FormulaR1C1 = "=" + column3[i] + currentrow + "-" + column3[i] + requiredrow + "";
                        }
                    }
                    #endregion

                    worksheet.Cell(endPart2 + 4, 3).Value = "ACCM SHORTAGE TEAM";
                    #region insert formula accm Shortage Team
                    int shortagerow = endPart2 + 2;
                    int accmshortagerow = shortagerow + 2;

                    for (int i = 0; i < column3.Length; i++)
                    {
                        if (i < column3.Length - 1)
                        {
                            worksheet.Cell(accmshortagerow, i + 5).FormulaR1C1 = "=" + column[i + 1] + shortagerow + "+" + column[i] + accmshortagerow + "";
                        }
                    }
                    #endregion

                    #endregion

                    #region worksheet1
                    // worksheet1

                    var worksheet1 = workbook.Worksheets.Add("Schedule");
                    worksheet1.Rows().Style.Font.FontName = "Calibri";
                    worksheet1.Rows().Style.Font.FontSize = 11;

                    //to show gridlines
                    worksheet1.ShowGridLines = false;

                    //set column width
                    worksheet1.Columns().Width = 10;
                    worksheet1.Column(1).Width = 2;
                    worksheet1.Column(2).Width = 23;
                    worksheet1.Column(3).Width = 12;
                    worksheet1.Column(4).Width = 12;
                    worksheet1.Column(5).Width = 12;

                    worksheet1.Rows().Height = 22;

                    worksheet1.PageSetup.Margins.Top = 0.5;
                    worksheet1.PageSetup.Margins.Bottom = 0.25;
                    worksheet1.PageSetup.Margins.Left = 0.25;
                    worksheet1.PageSetup.Margins.Right = 0;
                    worksheet1.PageSetup.Margins.Header = 0.5;
                    worksheet1.PageSetup.Margins.Footer = 0.25;
                    worksheet1.PageSetup.CenterHorizontally = true;

                    // set text center
                    worksheet1.Range(worksheet1.Cell(2, 3), worksheet1.Cell(3, 36)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet1.Range(worksheet1.Cell(2, 3), worksheet1.Cell(3, 36)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet1.Range(worksheet1.Cell(2, 3), worksheet1.Cell(2, 5)).Merge();
                    worksheet1.Cell(2, 3).Value = "PTSN CKD";
                    worksheet1.Cell(3, 3).Value = "MODEL";
                    worksheet1.Cell(3, 5).Value = "UPH";

                    // set style for header
                    // set dok number format
                    worksheet1.Range(worksheet1.Cell(2, 6), worksheet1.Cell(2, totalDay + 5)).Style.NumberFormat.Format = "d-mmm";

                    // set border 
                    worksheet1.Range(worksheet1.Cell(2, 6), worksheet1.Cell(3, totalDay + 5)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet1.Range(worksheet1.Cell(2, 6), worksheet1.Cell(3, totalDay + 5)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    // set color header
                    worksheet1.Range(worksheet1.Cell(2, 3), worksheet1.Cell(3, 6)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet1.Range(worksheet1.Cell(2, 6), worksheet1.Cell(3, totalDay + 5)).Style.Fill.BackgroundColor = XLColor.Black;
                    worksheet1.Range(worksheet1.Cell(2, 6), worksheet1.Cell(3, totalDay + 5)).Style.Font.FontColor = XLColor.White;
                    worksheet1.Range(worksheet1.Cell(2, 3), worksheet1.Cell(3, totalDay + 5)).Style.Font.Bold = true;

                    int cellRowIndexSheet1 = 4;
                    int cellColumnIndexSheet1 = 4;

                    // giving color for sun(red), sat(green) and public holiday (blue)
                    for (int i = 1; i <= totalDay; i++)
                    {
                        string date = monthFile + "/" + i + "/" + yearFile;
                        DateTime dte = Convert.ToDateTime(date);
                        worksheet1.Cell(2, 5 + i).Value = date;
                        worksheet1.Cell(3, 5 + i).Value = dte.ToString("ddd");

                        // set color red if day is sunday
                        if (worksheet1.Cell(3, 5 + i).Value.ToString() == "Sun")
                        {
                            worksheet1.Range(worksheet1.Cell(2, 5 + i), worksheet1.Cell(3, 5 + i)).Style.Fill.BackgroundColor = XLColor.Red;
                            worksheet1.Range(worksheet1.Cell(2, 5 + i), worksheet1.Cell(3, 5 + i)).Style.Font.FontColor = XLColor.Black;
                            //worksheet1.Range(worksheet1.Cell(4, 5 + i), worksheet1.Cell(endPartSheet1, 5 + i)).InsertColumnsBefore(1);
                            ////worksheet1.Range(worksheet1.Cell(4, 5 + i), worksheet1.Cell(endPartSheet1, 5 + i)).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                        else if (worksheet1.Cell(3, 5 + i).Value.ToString() == "Sat")
                        {
                            worksheet1.Range(worksheet1.Cell(2, 5 + i), worksheet1.Cell(3, 5 + i)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 255, 0);
                            worksheet1.Range(worksheet1.Cell(2, 5 + i), worksheet1.Cell(3, 5 + i)).Style.Font.FontColor = XLColor.Black;
                            //worksheet1.Range(worksheet1.Cell(4, 5 + i), worksheet1.Cell(endPartSheet1, 5 + i)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 255, 0);
                        }

                        // give  color blue if public holiday
                        string menu = "SELECT NAME, DATE FROM tbl_masterholiday WHERE DATE = '" + year + "-" + month + "-" + i + "'";
                        using (MySqlDataAdapter adpt = new MySqlDataAdapter(menu, connectionDB.connection))
                        {
                            DataTable dt = new DataTable();
                            adpt.Fill(dt);
                            //cek jika ada tanggal merah
                            if (dt.Rows.Count > 0)
                            {
                                worksheet1.Range(worksheet1.Cell(2, 5 + i), worksheet1.Cell(3, 5 + i)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 176, 240);
                                worksheet1.Range(worksheet1.Cell(2, 5 + i), worksheet1.Cell(3, 5 + i)).Style.Font.FontColor = XLColor.Black;
                                if (worksheet1.Cell(3, 5 + i).Value.ToString() != "Sun")
                                {
                                    //worksheet1.Range(worksheet1.Cell(4, 5 + i), worksheet1.Cell(endPartSheet1, 5 + i)).InsertColumnsBefore(1);
                                }
                                //worksheet1.Range(worksheet1.Cell(4, 5 + i), worksheet1.Cell(endPartSheet1, 5 + i)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 176, 240);
                            }
                        }
                    }
                    //===============

                    // get all data in datagridview with skip 0 value
                    Dictionary<string, int> smtSchedule = new Dictionary<string, int>();

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewFCT.Rows.Count; i++)
                    {
                        // list all fcst and skip 0 value
                        int k = 0;
                        int firstcol = 0;
                        for (int j = 5; j < dataGridViewFCT.Columns.Count; j++)
                        {
                            // to get all data in datagridview c# with skip 0 value 
                            if (dataGridViewFCT.Rows[i].Cells[j].Value.ToString() != "0")
                            {
                                int colposition = j - 5;
                                k++;
                                smtSchedule.Add(i + "," + k + "," + colposition, Convert.ToInt32(dataGridViewFCT.Rows[i].Cells[j].Value.ToString()));
                            }
                        }
                    }

                    for (int i = 0; i < smtSchedule.Count; i++)
                    {
                        //get row
                        var row = smtSchedule.ElementAt(i).Key.ToString().Split(',');
                        int rowPosition = Convert.ToInt32(row[0].Replace(",", ""));
                        int rowPosition1 = (rowPosition * 5) + 4;
                        int rowPosition2 = (rowPosition + 1) * 5;
                        int rowPosition3 = (rowPosition * 5) + 6;

                        int columnPosition = Convert.ToInt32(row[1].Replace(",", ""));

                        // to get first column
                        if (columnPosition == 1)
                        {
                            firstDataPosition = Convert.ToInt32(row[2].Replace(",", ""));
                        }

                        Console.WriteLine(firstDataPosition.ToString());

                        worksheet1.Cell(rowPosition1, 5 + firstDataPosition + columnPosition).Value = smtSchedule.ElementAt(i).Value;
                        worksheet1.Cell(rowPosition2, 6 + firstDataPosition + columnPosition).Value = smtSchedule.ElementAt(i).Value;
                        worksheet1.Cell(rowPosition3, 7 + firstDataPosition + columnPosition).Value = smtSchedule.ElementAt(i).Value;
                    }

                    // get end of row in excel
                    int maxrow = (dataGridViewFCT.Rows.Count * 5) + 3;
                    int endPartSheet1 = maxrow;

                    // moving 1 cell if in sun or public holiday in excel sheet  
                    for (int i = 4; i < endPartSheet1; i++)
                    {
                        for (int j = 5; j < totalDay + 5; j++)
                        {
                            // to get all data in datagridview c# with skip 0 value, with ceck if cell is not blank and the header color is read or blue add 1 cell  
                            if (worksheet1.Cell(i, j).Value != "")
                            {
                                if (worksheet1.Cell(3, j).Style.Fill.BackgroundColor == XLColor.Red || worksheet1.Cell(3, j).Style.Fill.BackgroundColor == XLColor.FromArgb(0, 176, 240))
                                {
                                    worksheet1.Cell(i, j).InsertCellsBefore(1);
                                }
                            }
                        }

                        // store 0 if value is blank
                        for (int j = 6; j < totalDay + 6; j++)
                        {
                            // to get all data in datagridview c# with skip 0 value, with ceck if cell is not blank and the header color is read or blue add 1 cell  
                            if (worksheet1.Cell(i, j).Value == "")
                            {
                                worksheet1.Cell(i, j).Value = 0;
                            }
                        }
                    }

                    // storing Each row and column value to excel sheet  
                    for (int k = 0; k < dataGridViewFCT.Rows.Count; k++)
                    {
                        // display SMT scehdule
                        for (int j = 0; j < dataGridViewFCT.Columns.Count; j++)
                        {
                            row = (5 * k) + 7;
                            worksheet1.Cell(row, 2).Value = "FATP Demand";
                            worksheet1.Cell(row - 1, 2).Value = "Delivery";
                            worksheet1.Cell(row - 2, 2).Value = "SA";
                            worksheet1.Cell(row - 3, 2).Value = "SMT";

                            //display model name
                            if (j == 0)
                            {
                                worksheet1.Cell(row, 3).Value = dataGridViewFCT.Rows[k].Cells[j].Value.ToString();
                            }
                            else
                            {
                                worksheet1.Cell(row, j + cellColumnIndexSheet1).Value = dataGridViewFCT.Rows[k].Cells[j].Value.ToString();
                            }

                            //// to remove row for separate each data per model
                            //if (k > 0)
                            //{
                            //    int blankrow = ((k * 5) + 3) +1;
                            //    worksheet1.Cell(blankrow, j).Value = "";
                            //}                         
                        }
                    }

                    // remove row for separate each data 
                    for (int k = 1; k < dataGridViewFCT.Rows.Count + 1; k++)
                    {
                        int blankrow = ((k * 5) + 3);
                        for (int j = 1; j < dataGridViewFCT.Columns.Count + 4; j++)
                        {
                            worksheet1.Cell(blankrow, j).Value = "";
                        }
                    }

                    //// to delete unnecessary data
                    //worksheet1.Range("AK:AZ").Delete(XLShiftDeletedCells.ShiftCellsLeft); 

                    // set dok number format
                    worksheet1.Range(worksheet1.Cell(4, 5), worksheet1.Cell(endPartSheet1, totalDay + 5)).Style.NumberFormat.Format = "#,###;-#,###;-";

                    #endregion

                    workbook.SaveAs(directoryFile + "\\" + filename);
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

        private void dataGridViewFCT2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //memberi nomor row
            for (int i = 0; i < dataGridViewFCT2.Rows.Count; ++i)
            {
                int row = i + 1;
                dataGridViewFCT2.Rows[i].HeaderCell.Value = "" + row;
            }

            // not allow to sort table
            for (int i = 0; i < dataGridViewFCT2.Columns.Count; i++)
            {
                dataGridViewFCT2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridViewFCT2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewFCT2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
    }
}