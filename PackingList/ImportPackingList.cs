using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMTPE
{
    public partial class ImportPackingList : MaterialForm
    {
        LoadForm lf = new LoadForm();
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;
        string fileExtPL;
        string queryPL;
        string queryPLDetail;
        string queryGetTotal;

        string PLFileName;
        string missingpn;
        string missingmodel;

        public ImportPackingList()
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

        private void browseLL_Click(object sender, EventArgs e)
        {
            openFileDialogMM.Title = "Please Select a File Packing List";
            openFileDialogMM.Filter = "Excel Files|*.xls;*.xlsx;";
            if (openFileDialogMM.ShowDialog() == DialogResult.OK)
            {
                StartProgress("Loading...");
                resetData();
                PLFileName = openFileDialogMM.FileName;
                tbfilepathPL.Text = PLFileName;
                fileExtPL = Path.GetExtension(PLFileName).ToLower(); //get the file extension  

                //get sheet number 1 name 
                var excelFile = Path.GetFullPath(PLFileName);
                var excel = new Excel.Application();
                var workbook = excel.Workbooks.Open(PLFileName);
                Thread.Sleep(2500);
                var sheet = (Excel.Worksheet)workbook.Worksheets.Item[1]; // 1 is the first item, this is NOT a zero-based collection
                string sheetName = sheet.Name;
                excel.Workbooks.Close();
                excel.Quit();

                queryPL = "select * from [" + sheetName + "$L3:L11]";
                queryPLDetail = "select * from [" + sheetName + "$A13:O]";
                queryGetTotal = "select * from [" + sheetName + "$A13:A]";

                try
                {
                    // baca data pl No dan invoice date
                    DataTable dtExcel = new DataTable();
                    dtExcel = help.ReadExcel(PLFileName, fileExtPL, queryPL); //read excel file  
                    string pl = dtExcel.Rows[0][0].ToString();
                    string invoiceDate = dtExcel.Rows[1][0].ToString();
                    string shipTerm = dtExcel.Rows[3][0].ToString();
                    string incoterm = dtExcel.Rows[4][0].ToString();
                    string payTerm = dtExcel.Rows[5][0].ToString();
                    string portOfLoading = dtExcel.Rows[6][0].ToString();
                    string destination = dtExcel.Rows[7][0].ToString();
                    tbPackingListNo.Text = pl;
                    tbInvoiceDate.Text = invoiceDate;
                    tbShipTerm.Text = shipTerm;
                    tbIncoterms.Text = incoterm;
                    tbPaymentTerm.Text = payTerm;
                    tbPortLoading.Text = portOfLoading;
                    tbDestination.Text = destination;

                    //baca data packing list
                    DataTable dtExcel2 = new DataTable();
                    dtExcel2 = help.ReadExcel(PLFileName, fileExtPL, queryPLDetail); //read excel file  
                    dataGridViewPackingList.DataSource = dtExcel2;

                    // buat cari batas total row
                    DataTable dtExcel1 = new DataTable();
                    dtExcel1 = help.ReadExcel(PLFileName, fileExtPL, queryGetTotal); //read excel file  

                    //get datatable row
                    DataRow row = dtExcel1.NewRow();

                    string searchingFor = "TOTAL: ";
                    int rowIndexBatas = 0;

                    for (int i = 0; i < dtExcel1.Rows.Count; i++)
                    {
                        if (dtExcel1.Rows[i][0].ToString().Contains(searchingFor))
                        {
                            rowIndexBatas = i;
                        }                        
                    }

                    int totalrow = dataGridViewPackingList.Rows.Count;
                    rowIndexBatas = rowIndexBatas - 3;

                    //delete data start from text stencil No
                    for (int i = totalrow - 1; i > rowIndexBatas; i--)
                    {
                        dataGridViewPackingList.Rows.RemoveAt(i);
                    }

                    //to give color if not found model in datagridview
                    int countmissingmodel = 0;
                    string model;
                    missingmodel = "";

                    for (int i = 0; i < dataGridViewPackingList.Rows.Count; ++i)
                    {
                        model = dataGridViewPackingList.Rows[i].Cells[1].Value.ToString();
                        // cek model exist or not in db
                        string cekmodel = "SELECT model FROM tbl_model WHERE model = '"+model+"'";
                        using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                        {
                            DataSet ds = new DataSet();
                            dscmd.Fill(ds);
                            // cek jika ada model tsb
                            if (ds.Tables[0].Rows.Count < 1)
                            {
                                dataGridViewPackingList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                countmissingmodel++;
                                missingmodel += model + "\r\n";
                            }
                        }
                    }

                    if (countmissingmodel > 0)
                    {
                        CloseProgress();
                        MessageBox.Show(this, "Missing model from mastermodel, please register model first ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                        saveButton.Enabled = false;
                    }
                    else
                    {
                        for (int i = 0; i < dataGridViewPackingList.Rows.Count; ++i)
                        {
                            model = dataGridViewPackingList.Rows[i].Cells[1].Value.ToString();
                            string cekmodel = "SELECT model FROM tbl_model WHERE model = '" + model + "'";
                            using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                            {
                                DataSet ds = new DataSet();
                                dscmd.Fill(ds);
                                // cek jika ada model tsb
                                if (ds.Tables[0].Rows.Count < 1)
                                {
                                    dataGridViewPackingList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    countmissingmodel++;
                                    missingmodel += model + "\r\n";
                                }
                            }
                        }

                        totalLbl.Text = dataGridViewPackingList.Rows.Count.ToString();
                        saveButton.Enabled = true;
                        CloseProgress();
                    }


                    ////to give color if not found partnumber
                    //int countmissing = 0;
                    //string pn;
                    //missingpn = "";

                    //for (int i = 0; i < dataGridViewPackingList.Rows.Count; ++i)
                    //{
                    //    pn = dataGridViewPackingList.Rows[i].Cells[4].Value.ToString();
                    //    // cek pn exist or not in db
                    //    string cekpn = "SELECT material FROM tbl_mastermaterial WHERE material = '" + pn + "'";
                    //    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekpn, connectionDB.connection))
                    //    {
                    //        DataSet ds = new DataSet();
                    //        dscmd.Fill(ds);

                    //        // cek jika pn tsb ada di master material
                    //        if (ds.Tables[0].Rows.Count < 1)
                    //        {
                    //            //dataGridViewPackingList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    //            countmissing++;
                    //            missingpn += pn + "\r\n";
                    //        }
                    //        else
                    //        {
                    //        }
                    //    }
                    //}

                }
                catch (Exception ex)
                {
                    CloseProgress();
                    saveButton.Enabled = false;
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(this, "Please select Packing List file with correct format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                }
            }
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                StartProgress("Loading...");
                saveButton.Enabled = false;

                if (tbfilepathPL.Text == "" )
                {
                    CloseProgress();
                    saveButton.Enabled = true;
                    MessageBox.Show(this, "Unable to import Packing List without choose file properly", "Packing List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    homeButton.Enabled = true;
                    BackButton.Enabled = true;
                }

                else if (tbPackingListNo.Text == "" || tbInvoiceDate.Text == "" ||
                    tbShipTerm.Text == "" || tbIncoterms.Text == "" || tbPaymentTerm.Text == "" ||
                    tbPortLoading.Text == "" || tbDestination.Text == "")
                {
                    CloseProgress();
                    saveButton.Enabled = true;
                    MessageBox.Show(this, "Unable to import Packing List without any packing data", "Packing List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    homeButton.Enabled = true;
                    BackButton.Enabled = true;
                }
                else
                {
                    try
                    {
                        var cmd = new MySqlCommand("", connectionDB.connection);

                        string packingno = tbPackingListNo.Text;
                        string cekmodel = "SELECT * FROM tbl_packingdetail WHERE packingNo = '"+packingno+"'";
                        using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                        {
                            DataSet ds = new DataSet();
                            dscmd.Fill(ds);

                            // cek jika modelno tsb sudah di upload
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                connectionDB.connection.Close();
                                CloseProgress();
                                MessageBox.Show(this, "Unable to import Packing List, Packing List already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                resetData();
                            }
                            else
                            {
                                //insert data
                                insertDataPacking();

                                this.Hide();
                                PackingList pl = new PackingList();
                                pl.toolStripUsername.Text = toolStripUsername.Text;
                                pl.Show();
                                this.Hide();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CloseProgress();
                        connectionDB.connection.Close();
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CloseProgress();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void resetData()
        {
            tbfilepathPL.Text = string.Empty;
            tbPackingListNo.Text = string.Empty;
            tbInvoiceDate.Text = string.Empty;
            tbShipTerm.Text = string.Empty;
            tbIncoterms.Text = string.Empty;
            tbPaymentTerm.Text = string.Empty;
            tbPortLoading.Text = string.Empty;
            tbDestination.Text = string.Empty;
            totalLbl.Text = "-";

            // remove data in datagridview result
            dataGridViewPackingList.DataSource = null;
            dataGridViewPackingList.Refresh();

            while (dataGridViewPackingList.Columns.Count > 0)
            {
                dataGridViewPackingList.Columns.RemoveAt(0);
            }

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            PackingList packingList = new PackingList();
            packingList.toolStripUsername.Text = toolStripUsername.Text;
            this.Hide();
            packingList.Show();
        }

        private void homeButton_Click(object sender, EventArgs e)
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

        private void insertDataPacking()
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var cmd = new MySqlCommand("", connectionDB.connection);
                connectionDB.connection.Open();
                //Buka koneksi

                string packingNo = tbPackingListNo.Text;
                string invoiceDate = tbInvoiceDate.Text;
                string shipTerm = tbShipTerm.Text;
                string incoterm = tbIncoterms.Text;
                string paymentterm = tbPaymentTerm.Text;
                string portloading = tbPortLoading.Text;
                string destination = tbDestination.Text;

                //insert packinglistdate
                string insertPackingList = "INSERT INTO tbl_packinglist VALUES(NULL, '" + packingNo + "', '" + invoiceDate + "', '" + shipTerm + "', " +
                    "'" + incoterm + "', '" + paymentterm + "', '" + portloading + "', '" + destination + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + idUser + "')";
                cmd.CommandText = insertPackingList;
                cmd.ExecuteNonQuery();

                //insert detail PL
                for (int i = 0; i < dataGridViewPackingList.Rows.Count; i++)
                {
                    string palletNo = dataGridViewPackingList.Rows[i].Cells[0].Value.ToString();
                    string projectModel = dataGridViewPackingList.Rows[i].Cells[1].Value.ToString();
                    string soandline = dataGridViewPackingList.Rows[i].Cells[2].Value.ToString();
                    string poandline = dataGridViewPackingList.Rows[i].Cells[3].Value.ToString();
                    string partno = dataGridViewPackingList.Rows[i].Cells[4].Value.ToString();
                    string desc = dataGridViewPackingList.Rows[i].Cells[5].Value.ToString();
                    string model = dataGridViewPackingList.Rows[i].Cells[6].Value.ToString();
                    string qtyperctn = dataGridViewPackingList.Rows[i].Cells[7].Value.ToString();
                    string totalctn = dataGridViewPackingList.Rows[i].Cells[8].Value.ToString();
                    string totalqty = dataGridViewPackingList.Rows[i].Cells[9].Value.ToString();
                    string unit = dataGridViewPackingList.Rows[i].Cells[10].Value.ToString();
                    string cou = dataGridViewPackingList.Rows[i].Cells[11].Value.ToString();
                    string netweight = dataGridViewPackingList.Rows[i].Cells[12].Value.ToString();
                    string grossweigth = dataGridViewPackingList.Rows[i].Cells[13].Value.ToString();
                    string volume = dataGridViewPackingList.Rows[i].Cells[14].Value.ToString();

                    string StrQuery = "INSERT INTO tbl_packingdetail VALUES (null,'"
                         + packingNo + "','"
                         + palletNo + "','"
                         + projectModel + "', '"
                         + soandline + "', '"
                         + poandline + "', '"
                         + partno + "', '"
                         + desc + "', '"
                         + model + "', '"
                         + qtyperctn + "', '"
                         + totalctn + "', '"
                         + totalqty + "', '"
                         + unit + "', '"
                         + cou + "', '"
                         + netweight + "', '"
                         + grossweigth + "', '"
                         + volume + "'" +
                         ",null, null); ";

                    cmd.CommandText = StrQuery;
                    cmd.ExecuteNonQuery();
                }

                stopwatch.Stop();
                Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");

                connectionDB.connection.Close();
                //Tutup koneksi

                ////directory file
                //string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                //directoryFile = directoryFile + "\\Packing List";

                ////save as excel file
                //File.Copy(PLFileName, @"" + directoryFile + "\\" + Path.GetFileName(PLFileName));

                CloseProgress();
                MessageBox.Show(this, "Import Packing List complete", "Packing List", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridViewPackingList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // not allow to sort table
            for (int i = 0; i < dataGridViewPackingList.Columns.Count; i++)
            {
                dataGridViewPackingList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ImportPackingList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void ImportPackingList_Load(object sender, EventArgs e)
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
        }
    }
}
