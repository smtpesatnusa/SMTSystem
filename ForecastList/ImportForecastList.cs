using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMTPE
{
    public partial class ImportForecastList : MaterialForm
    {
        LoadForm lf = new LoadForm();
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;
        string fileExt;
        string query;
        string queryDetail;
        string queryGetTotal;

        string FileName;
        string missingpn;
        string missingmodel;

        public ImportForecastList()
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                saveButton.Enabled = false;

                if (cmbForecastFile.Text == "" || cmbForecastFile.Text == "" || dataGridViewForecastList.Rows.Count == 0)
                {
                    saveButton.Enabled = true;
                    MessageBox.Show(this, "Unable to import Forecast List without choose file properly", "Forecast List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    homeButton.Enabled = true;
                    backButton.Enabled = true;
                }
                else
                {
                    try
                    {
                        StartProgress("Loading...");
                        var cmd = new MySqlCommand("", connectionDB.connection);
                        string forecast = cmbForecastFile.Text;
                        string cekmodel = "SELECT * FROM tbl_forecastlist WHERE forecastList = '"+cmbForecastFile.Text+"'";
                        using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                        {
                            DataSet ds = new DataSet();
                            dscmd.Fill(ds);

                            // cek jika modelno tsb sudah di upload
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                connectionDB.connection.Close();
                                CloseProgress();
                                MessageBox.Show(this, "Unable to import Forecast List, Forecast List already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                resetData();
                            }
                            else
                            {
                                //insert data
                                insertDataPacking();

                                this.Hide();
                                ForecastList fl = new ForecastList();
                                fl.toolStripUsername.Text = toolStripUsername.Text;
                                fl.Show();
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
            tbfilepath.Text = string.Empty;
            cmbForecastFile.DataSource = null;
            Loadbutton.Enabled = false;

            // remove data in datagridview result
            dataGridViewForecastList.DataSource = null;
            dataGridViewForecastList.Refresh();

            while (dataGridViewForecastList.Columns.Count > 0)
            {
                dataGridViewForecastList.Columns.RemoveAt(0);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ForecastList forecastList = new ForecastList();
            forecastList.toolStripUsername.Text = toolStripUsername.Text;
            this.Hide();
            forecastList.Show();
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

                string forecastNo = cmbForecastFile.Text;

                //insert packinglistdate
                string insertPackingList = "INSERT INTO tbl_forecastlist (forecastList, importDate, importBy) VALUES ('" + forecastNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + idUser + "')";
                cmd.CommandText = insertPackingList;
                cmd.ExecuteNonQuery();

                ////insert detail PL
                //for (int i = 0; i < dataGridViewForecastList.Rows.Count; i++)
                //{
                //    string model = dataGridViewForecastList.Rows[i].Cells[0].Value.ToString();
                //    string modelNo = dataGridViewForecastList.Rows[i].Cells[1].Value.ToString();
                //    string desc = dataGridViewForecastList.Rows[i].Cells[2].Value.ToString();
                //    string poandline = dataGridViewForecastList.Rows[i].Cells[3].Value.ToString();
                //    string detail = dataGridViewForecastList.Rows[i].Cells[4].Value.ToString();
                //    string date1 = dataGridViewForecastList.Rows[i].Cells[5].Value.ToString();
                //    string date2 = dataGridViewForecastList.Rows[i].Cells[6].Value.ToString();
                //    string date3 = dataGridViewForecastList.Rows[i].Cells[7].Value.ToString();
                //    string date4 = dataGridViewForecastList.Rows[i].Cells[8].Value.ToString();
                //    string date5 = dataGridViewForecastList.Rows[i].Cells[9].Value.ToString();
                //    string date6 = dataGridViewForecastList.Rows[i].Cells[10].Value.ToString();
                //    string date7 = dataGridViewForecastList.Rows[i].Cells[11].Value.ToString();
                //    string date8 = dataGridViewForecastList.Rows[i].Cells[12].Value.ToString();
                //    string date9 = dataGridViewForecastList.Rows[i].Cells[13].Value.ToString();
                //    string date10 = dataGridViewForecastList.Rows[i].Cells[14].Value.ToString();
                //    string date11 = dataGridViewForecastList.Rows[i].Cells[15].Value.ToString();
                //    string date12 = dataGridViewForecastList.Rows[i].Cells[16].Value.ToString();
                //    string date13 = dataGridViewForecastList.Rows[i].Cells[17].Value.ToString();
                //    string date14 = dataGridViewForecastList.Rows[i].Cells[18].Value.ToString();
                //    string date15 = dataGridViewForecastList.Rows[i].Cells[19].Value.ToString();
                //    string date16 = dataGridViewForecastList.Rows[i].Cells[20].Value.ToString();
                //    string date17 = dataGridViewForecastList.Rows[i].Cells[21].Value.ToString();
                //    string date18 = dataGridViewForecastList.Rows[i].Cells[22].Value.ToString();
                //    string date19 = dataGridViewForecastList.Rows[i].Cells[23].Value.ToString();
                //    string date20 = dataGridViewForecastList.Rows[i].Cells[24].Value.ToString();
                //    string date21 = dataGridViewForecastList.Rows[i].Cells[25].Value.ToString();
                //    string date22 = dataGridViewForecastList.Rows[i].Cells[26].Value.ToString();
                //    string date23 = dataGridViewForecastList.Rows[i].Cells[27].Value.ToString();
                //    string date24 = dataGridViewForecastList.Rows[i].Cells[28].Value.ToString();
                //    string date25 = dataGridViewForecastList.Rows[i].Cells[29].Value.ToString();
                //    string date26 = dataGridViewForecastList.Rows[i].Cells[30].Value.ToString();
                //    string date27 = dataGridViewForecastList.Rows[i].Cells[31].Value.ToString();
                //    string date28 = dataGridViewForecastList.Rows[i].Cells[32].Value.ToString();
                //    string date29 = dataGridViewForecastList.Rows[i].Cells[33].Value.ToString();
                //    string date30 = dataGridViewForecastList.Rows[i].Cells[34].Value.ToString();
                //    string date31 = dataGridViewForecastList.Rows[i].Cells[35].Value.ToString();
                //    string importDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //    string importBy = idUser;

                //    string StrQuery = "INSERT INTO tbl_forecastdetail " +
                //        "(forecastid, model, modelno, descr, detail, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31) " +
                //        "VALUES ('"
                //         + forecastNo + "','"
                //         + palletNo + "','"
                //         + projectModel + "', '"
                //         + soandline + "', '"
                //         + poandline + "', '"
                //         + partno + "', '"
                //         + desc + "', '"
                //         + model + "', '"
                //         + qtyperctn + "', '"
                //         + totalctn + "', '"
                //         + totalqty + "', '"
                //         + unit + "', '"
                //         + cou + "', '"
                //         + netweight + "', '"
                //         + grossweigth + "', '"
                //         + volume + "'" +
                //         ",null, null); ";

                //    cmd.CommandText = StrQuery;
                //    cmd.ExecuteNonQuery();
                //}

                //stopwatch.Stop();
                //Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");

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

        private void ImportForecastList_Load(object sender, EventArgs e)
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

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "Please Select a File Forecast List";
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StartProgress("Loading...");
                    resetData();
                    FileName = openFileDialog.FileName;
                    tbfilepath.Text = FileName;
                    fileExt = Path.GetExtension(FileName).ToLower(); //get the file extension  

                    //get sheet name 
                    var excelFile = Path.GetFullPath(FileName);
                    var excel = new Excel.Application();
                    var workbook = excel.Workbooks.Open(FileName);
                    Thread.Sleep(2500);

                    // get all sheet name into dropdown list
                    ArrayList sheetname = new ArrayList();
                    foreach (Excel.Worksheet sheet in workbook.Sheets)
                    {
                        sheetname.Add(sheet.Name);
                    }
                    cmbForecastFile.DataSource = sheetname;

                    excel.Workbooks.Close();
                    excel.Quit();

                    cmbForecastFile.Enabled = true;
                    Loadbutton.Enabled = true;

                    CloseProgress();
                }
                catch (Exception ex)
                {
                    CloseProgress();
                    saveButton.Enabled = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridViewForecastList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // not allow to sort table
            for (int i = 0; i < dataGridViewForecastList.Columns.Count; i++)
            {
                dataGridViewForecastList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void Loadbutton_Click(object sender, EventArgs e)
        {
            string sheetName = cmbForecastFile.Text;

            query = "select * from [" + sheetName + "$A3:AI]";
            queryGetTotal = "select * from [" + sheetName + "$D:D]";

            try
            {
                StartProgress("Loading...");

                // baca data pl No dan invoice date
                DataTable dtExcel = new DataTable();
                dtExcel = help.ReadExcel(FileName, fileExt, query); //read excel file   
                dataGridViewForecastList.DataSource = dtExcel;

                // buat cari batas total row
                DataTable dtExcel1 = new DataTable();
                dtExcel1 = help.ReadExcel(FileName, fileExt, queryGetTotal); //read excel file  

                //get datatable row
                DataRow row = dtExcel1.NewRow();

                string searchingFor = "Accm";
                int rowIndexBatas = 0;

                for (int i = 0; i < dtExcel1.Rows.Count; i++)
                {
                    if (dtExcel1.Rows[i][0].ToString().Contains(searchingFor))
                    {
                        rowIndexBatas = i;
                    }
                }

                int totalrow = dataGridViewForecastList.Rows.Count;
                rowIndexBatas = rowIndexBatas-2;

                //delete data start from text stencil No
                for (int i = totalrow - 1; i > rowIndexBatas; i--)
                {
                    dataGridViewForecastList.Rows.RemoveAt(i);
                }

                saveButton.Enabled = true;

                ////to give color if not found model in datagridview
                //int countmissingmodel = 0;
                //string model;
                //missingmodel = "";

                //for (int i = 0; i < dataGridViewPackingList.Rows.Count; ++i)
                //{
                //    model = dataGridViewPackingList.Rows[i].Cells[1].Value.ToString();
                //    // cek pn exist or not in db
                //    string cekmodel = "SELECT model FROM tbl_model WHERE model = '" + model + "'";
                //    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                //    {
                //        DataSet ds = new DataSet();
                //        dscmd.Fill(ds);
                //        // cek jika ada model tsb
                //        if (ds.Tables[0].Rows.Count < 1)
                //        {
                //            dataGridViewPackingList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                //            countmissingmodel++;
                //            missingmodel += model + "\r\n";
                //        }
                //    }
                //}
                CloseProgress();
            }
            catch (Exception ex)
            {
                CloseProgress();
                saveButton.Enabled = false;
                MessageBox.Show(ex.Message);
                MessageBox.Show(this, "Please select Forecast file with correct format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
            }
        }

        private void cmbForecastFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = false;
        }
    }
}
