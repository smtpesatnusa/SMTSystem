using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMTPE
{
    public partial class ImportLabel : MaterialForm
    {
        LoadForm lf = new LoadForm();
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();

        string filePathMM = string.Empty;
        string fileExtMM = string.Empty;
        string queryMM = string.Empty;
        string idUser;

        string duplicateIsn = string.Empty;
        string qtyPO;
        int rowCountLabel;
        string PO; 


        public ImportLabel()
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

        private void browseLL_Click(object sender, EventArgs e)
        {
            //get PO text
            PO = cmbPO.Text;

            openFileDialogSAPRefModelMaster.Title = "Please Select a File Master Material";
            openFileDialogSAPRefModelMaster.Filter = "Excel Files|*.xls;*.xlsx;";
            if (openFileDialogSAPRefModelMaster.ShowDialog() == DialogResult.OK)
            {
                StartProgress("Loading...");
                string MMFileName = openFileDialogSAPRefModelMaster.FileName;
                tbfilepathMM.Text = MMFileName;
                fileExtMM = Path.GetExtension(MMFileName).ToLower(); //get the file extension

                //get sheet number 1 name 
                var excelFile = Path.GetFullPath(MMFileName);
                var excel = new Excel.Application();
                var workbook = excel.Workbooks.Open(MMFileName);
                Thread.Sleep(2500);
                var sheet = (Excel.Worksheet)workbook.Worksheets.Item[1]; // 1 is the first item, this is NOT a zero-based collection
                string sheetName = sheet.Name;

                //total row excel
                rowCountLabel = sheet.UsedRange.Rows.Count - 1;
                excelRow.Text = rowCountLabel.ToString();

                excel.Workbooks.Close();
                excel.Quit();

                queryMM = "select * from [" + sheetName + "$A1:I]";

                if (fileExtMM.CompareTo(".xls") == 0 || fileExtMM.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = help.ReadExcel(MMFileName, fileExtMM, queryMM); //read excel file  
                        dataGridViewLabel.Visible = true;
                        dataGridViewLabel.DataSource = dtExcel;

                        //cek if qty with PO data match 
                        if (poQty.Text != excelRow.Text)
                        {
                            CloseProgress();
                            MessageBox.Show(this, "Data Label in excel and Qty PO not match please check", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                            saveButton.Enabled = false;
                        }
                        else
                        {
                            //to give color in blank cell tp and pn in master material
                            int countduplicate = 0;
                            string isn;
                            duplicateIsn = "";

                            for (int i = 0; i < dataGridViewLabel.Rows.Count; ++i)
                            {
                                isn = dataGridViewLabel.Rows[i].Cells[1].Value.ToString();
                                // cek isn exist or not in db
                                string cekpn = "SELECT idIsn, yuansuanban, Model, WO, tbl_labellist.Version, CreateDate, PrintDate, IsPrint, Factory FROM tbl_labellist WHERE yuansuanban = '" + isn + "'";
                                using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekpn, connectionDB.connection))
                                {
                                    DataSet ds = new DataSet();
                                    dscmd.Fill(ds);

                                    // cek jika isn tsb sudah di upload
                                    if (ds.Tables[0].Rows.Count >= 1)
                                    {
                                        dataGridViewLabel.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                        countduplicate++;
                                        duplicateIsn += isn + "\r\n";
                                    }
                                    else
                                    {
                                    }
                                }
                            }

                            if (countduplicate > 0)
                            {
                                CloseProgress();
                                MessageBox.Show(this, "There is any duplicate Data Yuansuanban", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                                ExportDuplicateISN();
                                saveButton.Enabled = false;
                            }
                            else
                            {
                                saveButton.Enabled = true;
                                CloseProgress();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CloseProgress();
                        //MessageBox.Show(this, "Please select AOHAI Label file with correct format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                    }
                }
                else
                {
                    CloseProgress();
                    MessageBox.Show(this, "Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                StartProgress("Loading...");
                saveButton.Enabled = false;

                if (tbfilepathMM.Text == "")
                {
                    CloseProgress();
                    saveButton.Enabled = true;
                    MessageBox.Show(this, "Unable to import Label without choose file properly", "AOHAI Label", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    connectionDB.connection.Open();
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string queryUpdatePO = "UPDATE tbl_po SET STATUS = 'import', importBy = '" + idUser + "', importDAte = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE PO_no = '" + cmbPO.Text + "'";

                    string[] allQuery = { queryUpdatePO };
                    for (int j = 0; j < allQuery.Length; j++)
                    {
                        cmd.CommandText = allQuery[j];
                        //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                        cmd.ExecuteNonQuery();
                        //Jalankan perintah / query dalam CommandText pada database
                    }
                    connectionDB.connection.Close();

                    bgWorker.WorkerSupportsCancellation = true;

                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync();

                    CloseProgress();

                    MessageBox.Show(this, "AOHAI Label will Uploaded in Background", "AOHAI Label", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    LabelList labelList = new LabelList();
                    labelList.toolStripUsername.Text = toolStripUsername.Text;
                    labelList.Show();
                }
            }
            catch (Exception ex)
            {
                CloseProgress();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ImportSAPRefModelMaster_FormClosing(object sender, FormClosingEventArgs e)
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

        private void ImportLabel_Load(object sender, EventArgs e)
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

            connectionDB.connection.Open();
            string queryPo = "SELECT * FROM tbl_po";

            try
            {
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(queryPo, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);

                    if (dset.Rows.Count > 0)
                    {
                        for (int j = 0; j < dset.Rows.Count; j++)
                        {
                            cmbPO.Items.Add(dset.Rows[j]["PO_no"]);
                            cmbPO.ValueMember = dset.Rows[j]["PO_no"].ToString();
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

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var cmd = new MySqlCommand("", connectionDB.connection);
                connectionDB.connection.Open();
                //Buka koneksi

                for (int i = 0; i < dataGridViewLabel.Rows.Count; i++)
                {
                    string id = dataGridViewLabel.Rows[i].Cells[0].Value.ToString();
                    string sn = dataGridViewLabel.Rows[i].Cells[1].Value.ToString();
                    string model = dataGridViewLabel.Rows[i].Cells[2].Value.ToString();
                    string wo = dataGridViewLabel.Rows[i].Cells[3].Value.ToString();
                    string version = dataGridViewLabel.Rows[i].Cells[4].Value.ToString();
                    string createDate = dataGridViewLabel.Rows[i].Cells[5].Value.ToString();
                    string printDate = dataGridViewLabel.Rows[i].Cells[6].Value.ToString();
                    string isPrint = dataGridViewLabel.Rows[i].Cells[7].Value.ToString();
                    string factory = dataGridViewLabel.Rows[i].Cells[8].Value.ToString();

                    // query insert data 
                    string StrQuery = "INSERT INTO tbl_labellist VALUES (null,'"
                         + id + "','"
                         + sn + "', '"
                         + model + "', '"
                         + wo + "', '"
                         + version + "', '"
                         + createDate + "', '"
                         + printDate + "', '"
                         + isPrint + "', '"
                         + factory + "','" + PO + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + idUser + "',null,null,null,null,null,null,null,'import');";

                    cmd.CommandText = StrQuery;
                    cmd.ExecuteNonQuery();
                }

                connectionDB.connection.Close();
                //Tutup koneksi
                MessageBox.Show(this, "Import Label AOHAI complete", "AOHAI Label", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ExportDuplicateISN()
        {
            try
            {
                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                directoryFile = directoryFile + "\\Duplicate AOHAI LAbel\\";
                string randFileName = help.randomText(10);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    //to hide gridlines
                    worksheet.ShowGridLines = false;

                    //pagesetup
                    worksheet.PageSetup.Margins.Top = 0.5;
                    worksheet.PageSetup.Margins.Bottom = 0.25;
                    worksheet.PageSetup.Margins.Left = 0.25;
                    worksheet.PageSetup.Margins.Right = 0;
                    worksheet.PageSetup.Margins.Header = 0.5;
                    worksheet.PageSetup.Margins.Footer = 0.25;
                    worksheet.PageSetup.CenterHorizontally = true;

                    // set column width
                    worksheet.Column(1).Width = 4.5;
                    worksheet.Column(2).Width = 20;

                    worksheet.Rows().Height = 16.25;
                    worksheet.Row(1).Height = 25.5;

                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 9)).Merge();
                    worksheet.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Font.FontSize = 20;
                    worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.RoyalBlue;
                    worksheet.Cell(1, 1).Style.Font.Bold = true;
                    worksheet.Cell(1, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Value = "Duplicate ISN";
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(3, 1)).Style.Font.FontName = "Courier New";
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(3, 1)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(3, 1), worksheet.Cell(3, 1)).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "DATE      : " + DateTime.Now.ToString("dd MMM yyyy");

                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Font.FontSize = 10;
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Font.Bold = true;
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Fill.BackgroundColor = XLColor.Yellow;
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(5, 1).Value = "NO";
                    worksheet.Range(worksheet.Cell(5, 2), worksheet.Cell(5, 9)).Merge();
                    worksheet.Cell(5, 2).Value = "DUPLICATE ISN";
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(5, 1), worksheet.Cell(5, 9)).Style.Border.BottomBorder = XLBorderStyleValues.Double;
                    worksheet.Cell(5, 1).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(5, 2), worksheet.Cell(5, 5)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(5, 7), worksheet.Cell(5, 9)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(5, 9).Style.Border.RightBorder = XLBorderStyleValues.Medium;

                    int cellRowIndex = 6;
                    int cellColumnIndex = 1;

                    //data missing material
                    int totalLines = duplicateIsn.Split('\n').Length;
                    var duplicateISN = duplicateIsn.Split('\n');

                    for (int j = 0; j < totalLines - 1; j++)
                    {
                        worksheet.Cell(cellRowIndex + j, cellColumnIndex).Value = j + 1;
                        worksheet.Cell(cellRowIndex + j, cellColumnIndex).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        worksheet.Cell(cellRowIndex + j, cellColumnIndex + 1).Value = duplicateISN[j].ToString();
                    }

                    int endPart = totalLines - 1 + cellRowIndex;

                    //setup style
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 9)).Style.Font.FontName = "Times New Roman";
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 9)).Style.Font.FontSize = 9;

                    // setup border 
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 9)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(cellRowIndex, 2), worksheet.Cell(endPart - 1, 2)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(cellRowIndex, 1), worksheet.Cell(endPart - 1, 1)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(cellRowIndex, 9), worksheet.Cell(endPart - 1, 9)).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(worksheet.Cell(endPart, 1), worksheet.Cell(endPart, 9)).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                    worksheet.Cell(endPart + 1, 1).Style.Font.FontName = "Arial";
                    worksheet.Cell(endPart + 1, 1).Style.Font.FontSize = 10;
                    worksheet.Cell(endPart + 1, 1).Style.Font.Bold = true;
                    worksheet.Cell(endPart + 1, 1).Value = "FM - SMT - ENG - 011";

                    workbook.SaveAs(directoryFile + "\\" + randFileName + ".xlsx");
                }

                MessageBox.Show(this, "Excel File Success Generated", "Generate Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\" + randFileName + ".xlsx");
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetData();
            connectionDB.connection.Open();
            string query = "SELECT qty from tbl_po where po_no='" + cmbPO.Text + "'";

            try
            {
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);

                    if (dset.Rows.Count > 0)
                    {
                        string qty = dset.Rows[0]["qty"].ToString();
                        poQty.Text = qty;

                    }
                }
                connectionDB.connection.Close();

                browseBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }

        }

        private void resetData()
        {
            excelRow.Text = "";
            tbfilepathMM.Text = "";

            dataGridViewLabel.DataSource = null;
            dataGridViewLabel.Refresh();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            LabelList labelList = new LabelList();
            labelList.toolStripUsername.Text = toolStripUsername.Text;
            labelList.Show();
            this.Hide();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }
    }
}