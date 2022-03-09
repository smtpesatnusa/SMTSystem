using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class LabelPrint : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        Helper help = new Helper();

        string idUser;
        string username;
        int batchNumber;

        public LabelPrint()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }


        private void cmbWO_SelectedIndexChanged(object sender, EventArgs e)
        {

            connectionDB.connection.Open();

            //get latest batch
            string queryBatch = "SELECT Batch FROM tbl_isn_list WHERE WO_PTSN='" + cmbWO.Text + "' ORDER BY Batch DESC";
            using (MySqlDataAdapter adpt1 = new MySqlDataAdapter(queryBatch, connectionDB.connection))
            {
                DataTable dset1 = new DataTable();
                adpt1.Fill(dset1);

                if (dset1.Rows.Count > 0)
                {
                    batchNumber = Convert.ToInt32(dset1.Rows[0]["Batch"].ToString()) + 1;
                }
                else
                {
                    batchNumber = 1;
                }
            }


            try
            {
                string query = "SELECT * FROM tbl_woptsn where WO_PTSN='" + cmbWO.Text + "'";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);

                    if (dset.Rows.Count > 0)
                    {
                        string workorder_ptsn = dset.Rows[0]["WO_PTSN"].ToString();
                        string workorder_customer = dset.Rows[0]["WO_Customer"].ToString();
                        string po_customer = dset.Rows[0]["PO_Customer"].ToString();
                        string qty = dset.Rows[0]["qtyWO"].ToString();
                        string icCodes = dset.Rows[0]["icCode"].ToString();

                        woCust.Text = workorder_customer;
                        woPTSN.Text = workorder_ptsn;
                        po.Text = po_customer;
                        lotSize.Text = qty;
                        icCodeLbl.Text = icCodes;
                    }
                }

                tbMax.ReadOnly = false;
                tbMax.Text = "0";
                tbMax.Focus();
                tbChecksheet.Clear();
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }

        private void max_TextChanged(object sender, EventArgs e)
        {
            tbWO.Text = string.Empty;
            tbMarking.Text = string.Empty;
            tbYuansuanban.Text = string.Empty;

            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(tbMax.Text, "[^0-9]"))
            {
                //MessageBox.Show("Please enter only numbers.");
                tbMax.Text = tbMax.Text.Remove(tbMax.Text.Length - 1);
            }

            //if user let text 0
            if (tbMax.Text.Trim() != "0")
            {
                tbChecksheet.Text = cmbWO.Text + "/" + batchNumber.ToString();
            }
        }


        private void LabelPrint_FormClosing(object sender, FormClosingEventArgs e)
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

        private void LabelPrint_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            int idusername = toolStripUsername.Text.Split(' ').Length - 4;
            idUser = userId[idPosition].Replace(",", "");
            username = userId[idusername].Replace(",", "");

            //session text
            session.Text = (toolStripUsername.Text.Split(',')[1].Replace(" |", String.Empty) + "" + help.randomText(15)).Replace(" ", String.Empty);

            connectionDB.connection.Open();

            string queryWO = "SELECT * FROM tbl_woptsn";

            try
            {
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryWO, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);

                    if (dset.Rows.Count > 0)
                    {
                        for (int j = 0; j < dset.Rows.Count; j++)
                        {
                            cmbWO.Items.Add(dset.Rows[j]["WO_PTSN"]);
                            cmbWO.ValueMember = dset.Rows[j]["WO_PTSN"].ToString();
                        }
                    }
                    else
                    {
                    }
                }
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportLabelScanToExcel()
        {
            try
            {
                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                directoryFile = directoryFile + "\\Label List AOHAI\\" + cmbWO.Text;
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    //to hide gridlines
                    worksheet.ShowGridLines = false;

                    // set column width
                    worksheet.Column(1).Width = 8.43;
                    worksheet.Column(2).Width = 17.71;
                    worksheet.Column(3).Width = 58.78;
                    worksheet.Column(4).Width = 17.71;
                    worksheet.Column(5).Width = 17.71;

                    worksheet.Rows().Height = 16.25;
                    worksheet.Row(1).Height = 25.5;

                    worksheet.PageSetup.Margins.Top = 0.5;
                    worksheet.PageSetup.Margins.Bottom = 0.25;
                    worksheet.PageSetup.Margins.Left = 0.25;
                    worksheet.PageSetup.Margins.Right = 0;
                    worksheet.PageSetup.Margins.Header = 0.5;
                    worksheet.PageSetup.Margins.Footer = 0.25;
                    worksheet.PageSetup.CenterHorizontally = true;

                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 5)).Merge();
                    worksheet.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Font.FontSize = 20;
                    worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.RoyalBlue;
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Value = "AOHAI LABEL LIST";
                    worksheet.Cell(2, 5).Style.Font.FontSize = 10;
                    worksheet.Cell(2, 5).Style.Font.FontName = "Times New Roman";
                    worksheet.Cell(2, 5).Style.Font.Italic = true;
                    worksheet.Cell(2, 5).Value = "Prepared By " + username + "";
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(10, 1)).Style.Font.FontName = "Courier New";
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(10, 1)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(10, 1)).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "WO PTSN NO     : " + woPTSN.Text;
                    worksheet.Cell(4, 1).Value = "WO CUST        : " + woCust.Text;
                    worksheet.Cell(5, 1).Value = "PO             : " + po.Text;
                    worksheet.Cell(6, 1).Value = "L/S WO PTSN    : " + lotSize.Text;
                    worksheet.Cell(7, 1).Value = "IC CODE        : " + icCodeLbl.Text;
                    worksheet.Cell(8, 1).Value = "CHECKSHEET NAME: " + tbChecksheet.Text;
                    worksheet.Cell(9, 1).Value = "QTY LABEL      : " + tbMax.Text;
                    worksheet.Cell(10, 1).Value = "DATE           : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Fill.BackgroundColor = XLColor.RoyalBlue;
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Font.FontColor = XLColor.White;
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(3, 4).Value = "Production";
                    worksheet.Cell(3, 5).Value = "QC";
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(3, 5)).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(3, 4), worksheet.Cell(10, 4)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(3, 5), worksheet.Cell(10, 5)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(3, 5), worksheet.Cell(10, 5)).Style.Border.RightBorder = XLBorderStyleValues.Medium;

                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(11, 1).Value = "NO";
                    worksheet.Cell(11, 2).Value = "WORKORDER";
                    worksheet.Cell(11, 3).Value = "MARKING";
                    worksheet.Cell(11, 4).Value = "YUANSUANBAN";
                    worksheet.Cell(11, 5).Value = "PREPARED DATE";
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 5)).Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    worksheet.Cell(11, 1).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(11, 5).Style.Border.RightBorder = XLBorderStyleValues.Medium;

                    int cellRowIndex = 12;
                    int cellColumnIndex = 1;

                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(dataGridViewLabelPrint.Rows.Count + cellRowIndex, 9)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(dataGridViewLabelPrint.Rows.Count + cellRowIndex, 9)).Style.Font.FontSize = 9;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewLabelPrint.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewLabelPrint.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cell(cellRowIndex + i, cellColumnIndex).Value = i + 1;
                                worksheet.Cell(cellRowIndex + i, cellColumnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            }
                            else
                            {
                                worksheet.Cell(i + cellRowIndex, j + cellColumnIndex).Value = dataGridViewLabelPrint.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }

                    int endPart = dataGridViewLabelPrint.Rows.Count + cellRowIndex;

                    // setup border 
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 5)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(cellRowIndex, 2), worksheet.Cell(endPart - 1, 5)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 1)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(cellRowIndex, 5), worksheet.Cell(endPart - 1, 5)).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(endPart, 1), worksheet.Cell(endPart, 5)).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                    workbook.SaveAs(directoryFile + "\\CheckSheet\\" + woPTSN.Text + "-" + batchNumber + ".xlsx");
                }

                MessageBox.Show(this, "Excel File Success Generated", "Generate Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\CheckSheet\\" + woPTSN.Text + "-" + batchNumber + ".xlsx");
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }


        private void tbYuansuanban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbYuansuanban.Text != "")
                {
                    //cek if data no blank WO marking yuansuanban
                    if (tbWO.Text != "" && tbMarking.Text != "" && tbYuansuanban.Text != "")
                    {
                        if (tbYuansuanban.Text.Length == 17)
                        {
                            //cek data from label list
                            string queryCekYuansuanban = "SELECT * FROM tbl_labellist WHERE yuansuanban = '" + tbYuansuanban.Text + "' and WO = '" + tbWO.Text + "'";
                            using (MySqlDataAdapter dscmd = new MySqlDataAdapter(queryCekYuansuanban, connectionDB.connection))
                            {
                                DataTable dset = new DataTable();
                                dscmd.Fill(dset);
                                // cek jika isn tsb ada di list import
                                if (dset.Rows.Count > 0)
                                {
                                    string query = "SELECT * FROM tbl_isn_list WHERE YuanSuanBan = '" + tbYuansuanban.Text + "'";
                                    using (MySqlDataAdapter dscmd1 = new MySqlDataAdapter(query, connectionDB.connection))
                                    {
                                        DataTable dset1 = new DataTable();
                                        dscmd1.Fill(dset1);
                                        // cek jika isn tsb sudah di print
                                        if (dset1.Rows.Count > 0)
                                        {
                                            string yuansuanban = dset1.Rows[0]["YuanSuanBan"].ToString();
                                            MessageBox.Show("YuanSuanBan already print!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            tbYuansuanban.Text = string.Empty;
                                        }
                                        else
                                        {
                                            connectionDB.connection.Open();
                                            var cmd = new MySqlCommand("", connectionDB.connection);
                                            //update Status
                                            string updateisnstatus = "UPDATE tbl_labellist SET PrintDate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', IsPrint = 'Y', STATUS = 'print', printSystemDate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', printSystemBy = '" + idUser + "', paletName = '" + tbChecksheet.Text + "' WHERE yuansuanban = '" + tbYuansuanban.Text + "'";
                                            cmd.CommandText = updateisnstatus;
                                            cmd.ExecuteNonQuery();

                                            //insert table
                                            string insertisnlist = "INSERT INTO tbl_isn_list values (NULL,'" + tbWO.Text + "','" + tbMarking.Text + "','" + tbYuansuanban.Text + "','" + cmbWO.Text + "','" + batchNumber + "','" + session.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "')";
                                            cmd.CommandText = insertisnlist;
                                            int a = cmd.ExecuteNonQuery();

                                            if (a > 0)
                                            {
                                                MessageBox.Show("Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                string queryLabelList = "SELECT id,workorder,marking,yuansuanban,createdate FROM tbl_isn_list WHERE SESSION = '" + session.Text + "'";

                                                using (MySqlDataAdapter adpts = new MySqlDataAdapter(queryLabelList, connectionDB.connection))
                                                {
                                                    DataSet dsets = new DataSet();

                                                    adpts.Fill(dsets);

                                                    dataGridViewLabelPrint.DataSource = dsets.Tables[0];
                                                }
                                                qtyData.Text = dataGridViewLabelPrint.Rows.Count.ToString();

                                                tbWO.Text = string.Empty;
                                                tbMarking.Text = string.Empty;
                                                tbYuansuanban.Text = string.Empty;

                                                if (tbMax.Text == qtyData.Text)
                                                {
                                                    MessageBox.Show("Data Complete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    ExportLabelScanToExcel();
                                                    session.Text = (toolStripUsername.Text.Split(',')[1].Replace(" |", String.Empty) + "" + help.randomText(15)).Replace(" ", String.Empty);
                                                    batchNumber++;
                                                    tbChecksheet.Text = cmbWO.Text + "/" + batchNumber.ToString();
                                                    qtyData.Text = "0";
                                                    dataGridViewLabelPrint.DataSource = null;
                                                    dataGridViewLabelPrint.Refresh();
                                                }
                                            }
                                            connectionDB.connection.Close();
                                            tbMarking.Focus();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Not Found YuanSuanBan From File Label List!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tbYuansuanban.Text = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong YuanSuanBan, please fill properly", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tbYuansuanban.Text = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fill data marking or yuansuanban", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void tbMarking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (tbMarking.Text.Length >= 66 && tbMarking.Text.Length < 100)
                {
                    tbWO.Text = tbMarking.Text.Substring(0, 18);
                    string icText = tbMarking.Text;
                    var icName = icText.Split(' ');
                    string icCode = icName[5].ToString().Replace(" ", "");

                    label6.Text = icCode;

                    if (tbWO.Text != woCust.Text)
                    {
                        MessageBox.Show("Workorder Not Match", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbMarking.Text = string.Empty;
                        tbWO.Text = string.Empty;
                    }
                    else
                    {
                        if (icCodeLbl.Text == "")
                        {
                            string query = "SELECT * FROM tbl_isn_list WHERE Marking = '" + tbMarking.Text + "'";
                            using (MySqlDataAdapter dscmd = new MySqlDataAdapter(query, connectionDB.connection))
                            {
                                DataTable dset = new DataTable();
                                dscmd.Fill(dset);

                                // cek jika isn tsb sudah di upload
                                if (dset.Rows.Count > 0)
                                {
                                    string marking = dset.Rows[0]["Marking"].ToString();
                                    MessageBox.Show("DUPLICATE IC MARKING !", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tbMarking.Text = string.Empty;
                                    tbWO.Text = string.Empty;
                                }
                                else
                                {
                                    tbYuansuanban.Focus();
                                }
                            }
                        }
                        else
                        {
                            if (icCodeLbl.Text.Contains(icCode) == true)
                            {
                                string query = "SELECT * FROM tbl_isn_list WHERE Marking = '" + tbMarking.Text + "'";
                                using (MySqlDataAdapter dscmd = new MySqlDataAdapter(query, connectionDB.connection))
                                {
                                    DataTable dset = new DataTable();
                                    dscmd.Fill(dset);

                                    // cek jika isn tsb sudah di upload
                                    if (dset.Rows.Count > 0)
                                    {
                                        string marking = dset.Rows[0]["Marking"].ToString();
                                        MessageBox.Show("DUPLICATE IC MARKING !", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tbMarking.Text = string.Empty;
                                        tbWO.Text = string.Empty;
                                    }
                                    else
                                    {
                                        tbYuansuanban.Focus();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong IC Code !", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tbMarking.Text = string.Empty;
                                tbWO.Text = string.Empty;
                            }
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Wrong Marking Text, please fill properly", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbMarking.Text = string.Empty;
                    tbWO.Text = string.Empty;
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void tbMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbMarking.Focus();
            }
        }
    }
}
