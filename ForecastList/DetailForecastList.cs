using ClosedXML.Excel;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class DetailForecastList : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        
        int totalDay;
        string monthFile;
        int yearFile;

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
                int year = Convert.ToInt32(dt.ToString("yyyy"));
                int month = Convert.ToInt32(dt.ToString("MM"));
                monthFile = dt.ToString("MMM");
                yearFile = Convert.ToInt32(dt.ToString("yyyy"));

                totalDay = DateTime.DaysInMonth(year, month);

                // to run qry statement based on total day
                string qry = "";

                for (int i = 1; i <= totalDay; i++)
                {
                    qry += "SUM(a.date"+i+ ") AS '" + i + "' ,";
                }
                qry = qry.Remove(qry.Length - 1);
                //-----------

                string query = "SELECT a.model, c.uph, "+qry+" FROM tbl_forecastdetail a, tbl_forecastlist b, tbl_masteruph c, tbl_model d WHERE b.id = a.forecastid AND c.model = d.id AND a.model = d.model AND " +
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

            //// Set table title
            //string[] title = { "MODEL", "UPH", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            //for (int i = 0; i < title.Length; i++)
            //{
            //    dataGridViewFCT.Columns[i].HeaderText = "" + title[i];
            //}

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
                Application.ExitThread();
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
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadData();
        }

        public List<DateTime> getAllDates(int year, int month)
        {
            var ret = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                ret.Add(new DateTime(year, month, i));
            }
            return ret;
        }


        private void exportExcel()
        {
            try
            {

                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                directoryFile = directoryFile + "\\ALFASYS\\FCT SMT\\" + tbForecastListNo.Text;
                string filename =  tbForecastListNo.Text+ ".xlsx";
                using (var workbook = new XLWorkbook())
                {
                    // worksheet plan
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

                    worksheet.Rows().Height = 18;

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
                    
                    // date 1 to 31
                    for (int i = 1; i <= totalDay; i++)
                    {
                        string date = monthFile+"/"+i + "/" + yearFile;
                        DateTime dt = Convert.ToDateTime(date);
                        worksheet.Cell(2, 4+i).Value = date;
                        worksheet.Cell(3, 4+i).Value = dt.ToString("ddd");
                    }

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
                    int nextcellDemand = cellDemand +1;

                    // give color yellow in total result
                    worksheet.Range(worksheet.Cell(endPart+1, 5), worksheet.Cell(endPart+1, totalDay+4)).Style.Fill.BackgroundColor = XLColor.Yellow;
                                        
                   string[] column = { "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                        "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI" };
                    for (int i = 0; i < totalDay; i++)
                    {
                        if (i< totalDay)
                        {
                            worksheet.Cell(endPart + 1, i + 5).FormulaR1C1 = "=" + column[i+1] + cellDemand + "+" + column[i] + nextcellDemand + "";
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
                                worksheet.Cell(endPart2, i + 5).FormulaR1C1 = "=SUM(" + column2[i]+ endFCT1 + ":" + column2[i] + rowtotal + ")";
                            }
                        }
                    }
                    #endregion

                    worksheet.Cell(endPart2+1, 3).Value = "CURRENT TEAM";
                    // give color red 
                    worksheet.Range(worksheet.Cell(endPart2+2, 2), worksheet.Cell(endPart2+2, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 185, 184);
                    worksheet.Range(worksheet.Cell(endPart2+4, 5), worksheet.Cell(endPart2+4, totalDay + 4)).Style.Fill.BackgroundColor = XLColor.FromArgb(230, 185, 184);
                    worksheet.Cell(endPart2+2, 3).Value = "SHORTAGE TEAM";

                    #region insert formula Shortage Team
                    int requiredrow = endPart2;
                    int currentrow = requiredrow+1;

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

            //// Set table title
            //string[] title = { "MODEL", "UPH", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            //for (int i = 0; i < title.Length; i++)
            //{
            //    dataGridViewFCT2.Columns[i].HeaderText = "" + title[i];
            //}

            dataGridViewFCT2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
    }
}
