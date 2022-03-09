using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Substore : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        Helper help = new Helper();

        string idUser;
        int paletNumber;

        public Substore()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }


        private void cmbWO_SelectedIndexChanged(object sender, EventArgs e)
        {
            completeBtn.Enabled = true;

            connectionDB.connection.Open();

            //get latest batch
            string queryBatch = "SELECT batchSubstore FROM tbl_pcblist WHERE woPtsn ='" + cmbWO.Text + "' ORDER BY batchSubstore DESC";
            using (MySqlDataAdapter adpt1 = new MySqlDataAdapter(queryBatch, connectionDB.connection))
            {
                DataTable dset1 = new DataTable();
                adpt1.Fill(dset1);

                if (dset1.Rows.Count > 0)
                {
                    paletNumber = Convert.ToInt32(dset1.Rows[0]["batchSubstore"].ToString()) + 1;
                }
                else
                {
                    paletNumber = 1;
                }
            }


            string query = "SELECT * FROM tbl_woptsn where WO_PTSN='" + cmbWO.Text + "'";
            try
            {
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

                tbPalet.Clear();
                tbPalet.Text = cmbWO.Text + "/" + paletNumber.ToString();
                tbWO.Text = string.Empty;
                tbMarking.Text = string.Empty;
                tbYuansuanban.Text = string.Empty;
                tbPCBSN.Text = string.Empty;
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }


        private void ExportLabelSubstoreToExcel()
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
                    worksheet.Column(3).Width = 26;
                    worksheet.Column(4).Width = 59;
                    worksheet.Column(5).Width = 17.71;
                    worksheet.Column(6).Width = 14.44;

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
                    worksheet.Cell(1, 1).Value = "AOHAI SUBSTORE";
                    worksheet.Cell(2, 6).Style.Font.FontSize = 10;
                    worksheet.Cell(2, 6).Style.Font.FontName = "Times New Roman";
                    worksheet.Cell(2, 6).Style.Font.Italic = true;
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(10, 1)).Style.Font.FontName = "Courier New";
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(10, 1)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(10, 1)).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "WO PTSN NO     : " + woPTSN.Text;
                    worksheet.Cell(4, 1).Value = "WO CUST        : " + woCust.Text;
                    worksheet.Cell(5, 1).Value = "PO             : " + po.Text;
                    worksheet.Cell(6, 1).Value = "LOT SIZE PO    : " + lotSize.Text;
                    worksheet.Cell(7, 1).Value = "IC CODE        : " + icCodeLbl.Text;
                    worksheet.Cell(8, 1).Value = "PALLET NO      :" + tbPalet.Text;
                    worksheet.Cell(9, 1).Value = "QTY PCB        : ";
                    worksheet.Cell(10, 1).Value = "DATE           : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(11, 1).Value = "NO";
                    worksheet.Cell(11, 2).Value = "WORKORDER";
                    worksheet.Cell(11, 3).Value = "PCB SN";
                    worksheet.Cell(11, 4).Value = "MARKING";
                    worksheet.Cell(11, 5).Value = "YUANSUANBAN";
                    worksheet.Cell(11, 6).Value = "CREATED DATE";
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(11, 1), worksheet.Cell(11, 6)).Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    worksheet.Cell(11, 1).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Cell(11, 6).Style.Border.RightBorder = XLBorderStyleValues.Medium;

                    int cellRowIndex = 12;
                    int cellColumnIndex = 1;

                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(dataGridViewSubstore.Rows.Count + cellRowIndex, 9)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(dataGridViewSubstore.Rows.Count + cellRowIndex, 9)).Style.Font.FontSize = 9;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewSubstore.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewSubstore.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cell(cellRowIndex + j, cellColumnIndex).Value = i + 1;
                                worksheet.Cell(cellRowIndex + j, cellColumnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                            }
                            else
                            {
                                worksheet.Cell(i + cellRowIndex, j + cellColumnIndex).Value = dataGridViewSubstore.Rows[i].Cells[j].Value.ToString();
                            }

                        }
                    }
                    workbook.SaveAs(directoryFile + "\\Substore\\" + woPTSN.Text + "-" + paletNumber + ".xlsx");
                }

                MessageBox.Show(this, "Excel File Success Generated", "Generate Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\Substore\\" + woPTSN.Text + "-" + paletNumber + ".xlsx");
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }

        }


        private void tbPCBSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //cek PCB Data
                if (tbPCBSN.Text != "")
                {
                    if (tbPCBSN.Text.Length >= 28 && tbPCBSN.Text.Length <= 32)
                    {
                        string query = "SELECT * FROM tbl_pcblist WHERE pcbsn = '" + tbPCBSN.Text + "'";
                        using (MySqlDataAdapter dscmd = new MySqlDataAdapter(query, connectionDB.connection))
                        {
                            DataTable dset = new DataTable();
                            dscmd.Fill(dset);

                            // cek jika isn tsb sudah di upload
                            if (dset.Rows.Count > 0)
                            {
                                string marking = dset.Rows[0]["pcbsn"].ToString();
                                MessageBox.Show("PCBSN already in FG, please check!!! !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tbPCBSN.Text = string.Empty;
                                tbMarking.Text = string.Empty;
                                tbWO.Text = string.Empty;
                            }
                            else
                            {
                                tbMarking.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("NOT FOUND", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbPCBSN.Text = string.Empty;
                    }
                }
            }
        }

        private void tbMarking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (tbMarking.Text != "")
                {
                    if (tbMarking.Text.Length > 18)
                    {
                        tbWO.Text = tbMarking.Text.Substring(0, 18);
                    }
                    else
                    {
                        tbWO.Text = string.Empty;
                    }

                    if (tbMarking.Text.Length >= 66)
                    {
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
                            string queryCekLabel = "SELECT * FROM tbl_isn_list WHERE Marking = '" + tbMarking.Text + "'";
                            using (MySqlDataAdapter dscmdCekLabel = new MySqlDataAdapter(queryCekLabel, connectionDB.connection))
                            {
                                DataTable dset = new DataTable();
                                dscmdCekLabel.Fill(dset);

                                // cek jika isn tsb sudah di upload
                                if (dset.Rows.Count == 0)
                                {
                                    MessageBox.Show("NOT FOUND MARKING IN PRINT LABEL !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tbMarking.Text = string.Empty;
                                    tbWO.Text = string.Empty;
                                }
                                else
                                {
                                    if (icCodeLbl.Text.Contains(icCode) == true)
                                    {
                                        string query = "SELECT * FROM tbl_pcblist WHERE Marking = '" + tbMarking.Text + "'";
                                        using (MySqlDataAdapter dscmd = new MySqlDataAdapter(query, connectionDB.connection))
                                        {
                                            DataTable dset2 = new DataTable();
                                            dscmd.Fill(dset2);

                                            // cek jika isn tsb sudah di upload
                                            if (dset2.Rows.Count > 0)
                                            {
                                                string marking = dset2.Rows[0]["Marking"].ToString();
                                                MessageBox.Show("MARKING IC already in FG, please check!!! !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                        MessageBox.Show("Wrong IC Code !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tbMarking.Text = string.Empty;
                                        tbWO.Text = string.Empty;
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("NOT FOUND", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbMarking.Text = string.Empty;
                    }
                }
            }
        }

        private void tbYuansuanban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (tbYuansuanban.Text != "")
                {
                    //cek if data no blank WO PCBSN marking yuansuanban
                    if (tbWO.Text != "" && tbPCBSN.Text != "" && tbMarking.Text != "" && tbYuansuanban.Text != "")
                    {
                        if (tbYuansuanban.Text.Length == 17)
                        {
                            //cek data from label list
                            string queryCekYuansuanban = "SELECT * FROM tbl_labellist WHERE yuansuanban = '" + tbYuansuanban.Text + "' and WO = '" + tbWO.Text + "' and status  = 'print'";
                            using (MySqlDataAdapter dscmd = new MySqlDataAdapter(queryCekYuansuanban, connectionDB.connection))
                            {
                                DataTable dset = new DataTable();
                                dscmd.Fill(dset);
                                // cek jika isn tsb ada di list import
                                if (dset.Rows.Count > 0)
                                {
                                    string query = "SELECT * FROM tbl_pcblist WHERE YuanSuanBan = '" + tbYuansuanban.Text + "'";
                                    using (MySqlDataAdapter dscmd1 = new MySqlDataAdapter(query, connectionDB.connection))
                                    {
                                        DataTable dset1 = new DataTable();
                                        dscmd1.Fill(dset1);
                                        // cek jika isn tsb sudah di print
                                        if (dset1.Rows.Count > 0)
                                        {
                                            string yuansuanban = dset1.Rows[0]["YuanSuanBan"].ToString();
                                            MessageBox.Show("DUPLICATE YuanSuanBan !!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            tbYuansuanban.Text = string.Empty;
                                        }
                                        else
                                        {
                                            connectionDB.connection.Open();
                                            var cmd = new MySqlCommand("", connectionDB.connection);
                                            //update Status
                                            string updateisnstatus = "UPDATE tbl_labellist SET STATUS = 'fg', fgDate = '" + DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss") + "', fgBy = '" + idUser + "', paletName = '" + tbPalet.Text + "' WHERE yuansuanban = '" + tbYuansuanban.Text + "'";
                                            cmd.CommandText = updateisnstatus;
                                            cmd.ExecuteNonQuery();

                                            //insert table
                                            string insertisnlist = "INSERT INTO tbl_pcblist values (NULL,'" + cmbWO.Text + "','" + tbPCBSN.Text + "','" + tbMarking.Text + "','" + tbYuansuanban.Text + "','" + woCust.Text + "',null, null, '" + DateTime.Now.ToString("yyyyy-MM-dd HH:mm:ss") + "','" + idUser + "','" + paletNumber + "','" + session.Text + "','fg' )";
                                            cmd.CommandText = insertisnlist;
                                            int a = cmd.ExecuteNonQuery();

                                            if (a > 0)
                                            {
                                                MessageBox.Show("Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                string queryLabelList = "SELECT id,workorder,pcbsn, marking,yuansuanban,fgdate FROM tbl_pcblist WHERE SESSION = '" + session.Text + "'";

                                                using (MySqlDataAdapter adpts = new MySqlDataAdapter(queryLabelList, connectionDB.connection))
                                                {
                                                    DataSet dsets = new DataSet();

                                                    adpts.Fill(dsets);

                                                    dataGridViewSubstore.DataSource = dsets.Tables[0];
                                                }
                                                qtyData.Text = dataGridViewSubstore.Rows.Count.ToString();

                                                tbPCBSN.Text = string.Empty;
                                                tbWO.Text = string.Empty;
                                                tbMarking.Text = string.Empty;
                                                tbYuansuanban.Text = string.Empty;
                                            }
                                            connectionDB.connection.Close();
                                            tbPCBSN.Focus();
                                        }
                                    }
                                }
                                else
                                {
                                    ///cek data from label list if fg
                                    string queryCekYuansuanbanFG = "SELECT * FROM tbl_labellist WHERE yuansuanban = '" + tbYuansuanban.Text + "' and WO = '" + tbWO.Text + "' and status  = 'fg'";
                                    using (MySqlDataAdapter dscmds = new MySqlDataAdapter(queryCekYuansuanbanFG, connectionDB.connection))
                                    {
                                        DataTable dsets = new DataTable();
                                        dscmds.Fill(dsets);
                                        // cek jika isn tsb ada di list import
                                        if (dsets.Rows.Count > 0)
                                        {
                                            MessageBox.Show("YuanSuanBan already in FG, please check!!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            tbYuansuanban.Text = string.Empty;
                                        }
                                        else
                                        {
                                            MessageBox.Show("YuanSuanBan not print yet or not found from file label list, please check!!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            tbYuansuanban.Text = string.Empty;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong Yuansuanban, please fill properly", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void Substore_Load(object sender, EventArgs e)
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

        private void Substore_FormClosing(object sender, FormClosingEventArgs e)
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

        private void completeBtn_Click(object sender, EventArgs e)
        {
            if (qtyData.Text != "0")
            {
                MessageBox.Show("Data Complete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ExportLabelSubstoreToExcel();
                session.Text = (toolStripUsername.Text.Split(',')[1].Replace(" |", String.Empty) + "" + help.randomText(15)).Replace(" ", String.Empty);
                paletNumber++;
                tbPalet.Text = cmbWO.Text + "/" + paletNumber.ToString();
                qtyData.Text = "0";
                dataGridViewSubstore.DataSource = null;
                dataGridViewSubstore.Refresh();
            }            
        }
    }
}
