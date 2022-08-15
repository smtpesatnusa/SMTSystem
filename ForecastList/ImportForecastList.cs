using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        string monthyear;

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
            saveButton.Enabled = false;

            if (cmbForecastFile.Text == "" || cmbForecastFile.Text == "" || dataGridViewPublicHolidayList.Rows.Count == 0)
            {
                saveButton.Enabled = true;
                MessageBox.Show(this, "Unable to import Production Plan List without choose file properly", "Production Plan List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                homeButton.Enabled = true;
                BackButton.Enabled = true;
            }
            else
            {
                try
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    string forecast = cmbForecastFile.Text;
                    string cekmodel = "SELECT * FROM tbl_forecastlist WHERE forecastList = '" + cmbForecastFile.Text + "'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        dscmd.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Unable to import Production Plan List, Production Plan List already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            StartProgress("Loading...");
                            //insert data
                            insertData();

                            //dataGridViewForecastList.DataSource = null;
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

        private void resetData()
        {
            tbfilepath.Text = string.Empty;
            cmbForecastFile.DataSource = null;
            Loadbutton.Enabled = false;

            // remove data in datagridview result
            dataGridViewPublicHolidayList.DataSource = null;
            dataGridViewPublicHolidayList.Refresh();

            while (dataGridViewPublicHolidayList.Columns.Count > 0)
            {
                dataGridViewPublicHolidayList.Columns.RemoveAt(0);
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

        private void insertData()
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var cmd = new MySqlCommand("", connectionDB.connection);
                connectionDB.connection.Open();
                //Buka koneksi

                string forecastNo = cmbForecastFile.Text;

                //insert listdata
                string insert = "INSERT INTO tbl_forecastlist (monthyear, forecastList, importDate, importBy) VALUES ('"+monthyear+"','" + forecastNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + idUser + "')";
                cmd.CommandText = insert;
                cmd.ExecuteNonQuery();

                string query = "SELECT * FROM tbl_forecastlist WHERE forecastList = '" + cmbForecastFile.Text + "'";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string idForecast = dt.Rows[0]["id"].ToString();
                        //insert detail
                        for (int i = 0; i < dataGridViewPublicHolidayList.Rows.Count; i++)
                        {
                            // insert data plan only 
                            if (dataGridViewPublicHolidayList.Rows[i].Cells[0].Value.ToString() != "")
                            {
                                string model = dataGridViewPublicHolidayList.Rows[i].Cells[0].Value.ToString();
                                string modelNo = dataGridViewPublicHolidayList.Rows[i].Cells[1].Value.ToString();
                                string desc = dataGridViewPublicHolidayList.Rows[i].Cells[2].Value.ToString();
                                string detail = dataGridViewPublicHolidayList.Rows[i].Cells[3].Value.ToString();
                                string date1 = dataGridViewPublicHolidayList.Rows[i].Cells[4].Value.ToString();
                                string date2 = dataGridViewPublicHolidayList.Rows[i].Cells[5].Value.ToString();
                                string date3 = dataGridViewPublicHolidayList.Rows[i].Cells[6].Value.ToString();
                                string date4 = dataGridViewPublicHolidayList.Rows[i].Cells[7].Value.ToString();
                                string date5 = dataGridViewPublicHolidayList.Rows[i].Cells[8].Value.ToString();
                                string date6 = dataGridViewPublicHolidayList.Rows[i].Cells[9].Value.ToString();
                                string date7 = dataGridViewPublicHolidayList.Rows[i].Cells[10].Value.ToString();
                                string date8 = dataGridViewPublicHolidayList.Rows[i].Cells[11].Value.ToString();
                                string date9 = dataGridViewPublicHolidayList.Rows[i].Cells[12].Value.ToString();
                                string date10 = dataGridViewPublicHolidayList.Rows[i].Cells[13].Value.ToString();
                                string date11 = dataGridViewPublicHolidayList.Rows[i].Cells[14].Value.ToString();
                                string date12 = dataGridViewPublicHolidayList.Rows[i].Cells[15].Value.ToString();
                                string date13 = dataGridViewPublicHolidayList.Rows[i].Cells[16].Value.ToString();
                                string date14 = dataGridViewPublicHolidayList.Rows[i].Cells[17].Value.ToString();
                                string date15 = dataGridViewPublicHolidayList.Rows[i].Cells[18].Value.ToString();
                                string date16 = dataGridViewPublicHolidayList.Rows[i].Cells[19].Value.ToString();
                                string date17 = dataGridViewPublicHolidayList.Rows[i].Cells[20].Value.ToString();
                                string date18 = dataGridViewPublicHolidayList.Rows[i].Cells[21].Value.ToString();
                                string date19 = dataGridViewPublicHolidayList.Rows[i].Cells[22].Value.ToString();
                                string date20 = dataGridViewPublicHolidayList.Rows[i].Cells[23].Value.ToString();
                                string date21 = dataGridViewPublicHolidayList.Rows[i].Cells[24].Value.ToString();
                                string date22 = dataGridViewPublicHolidayList.Rows[i].Cells[25].Value.ToString();
                                string date23 = dataGridViewPublicHolidayList.Rows[i].Cells[26].Value.ToString();
                                string date24 = dataGridViewPublicHolidayList.Rows[i].Cells[27].Value.ToString();
                                string date25 = dataGridViewPublicHolidayList.Rows[i].Cells[28].Value.ToString();
                                string date26 = dataGridViewPublicHolidayList.Rows[i].Cells[29].Value.ToString();
                                string date27 = dataGridViewPublicHolidayList.Rows[i].Cells[30].Value.ToString();
                                string date28 = dataGridViewPublicHolidayList.Rows[i].Cells[31].Value.ToString();
                                string date29 = dataGridViewPublicHolidayList.Rows[i].Cells[32].Value.ToString();
                                string date30 = dataGridViewPublicHolidayList.Rows[i].Cells[33].Value.ToString();
                                string date31 = dataGridViewPublicHolidayList.Rows[i].Cells[34].Value.ToString();

                                string StrQuery = "INSERT INTO tbl_forecastdetail " +
                                    "(forecastid, model, modelno, descr, detail, date1,date2,date3,date4,date5,date6,date7,date8,date9,date10,date11,date12,date13,date14,date15" +
                                    ",date16,date17,date18,date19,date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,date30,date31) " +
                                    "VALUES ('"+ idForecast + "','"
                                     + model + "','"
                                     + modelNo + "', '"
                                     + desc + "', '"
                                     + detail + "', '"
                                     + date1 + "', '"
                                     + date2 + "', '"
                                     + date3 + "', '"
                                     + date4 + "', '"
                                     + date5 + "', '"
                                     + date6 + "', '"
                                     + date7 + "', '"
                                     + date8 + "', '"
                                     + date9 + "', '"
                                     + date10 + "', '"
                                     + date11 + "', '"
                                     + date12 + "', '"
                                     + date13 + "', '"
                                     + date14 + "', '"
                                     + date15 + "', '"
                                     + date16 + "', '"
                                     + date17 + "', '"
                                     + date18 + "', '"
                                     + date19 + "', '"
                                     + date20 + "', '"
                                     + date21 + "', '"
                                     + date22 + "', '"
                                     + date23 + "', '"
                                     + date24 + "', '"
                                     + date25 + "', '"
                                     + date26 + "', '"
                                     + date27 + "', '"
                                     + date28 + "', '"
                                     + date29 + "', '"
                                     + date30 + "', '"
                                     + date31 + "')";

                                cmd.CommandText = StrQuery;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        stopwatch.Stop();
                        Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");

                        connectionDB.connection.Close();
                        //Tutup koneksi

                        CloseProgress();
                        MessageBox.Show(this, "Import Production Plan List complete", "Production Plan List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        CloseProgress();
                        MessageBox.Show(this, "Fail to import Production Plan file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                string cekmodel = "SELECT * FROM tbl_forecastlist WHERE monthyear = '" + monthyear + "'";
                using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                {
                    DataSet ds = new DataSet();
                    dscmd.Fill(ds);

                    // cek jika modelno tsb sudah di upload
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show(this, "Unable to import Production Plan List, Production Plan List selected month year already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resetData();
                    }
                    else
                    {
                        openFileDialog.Title = "Please Select a File  Production Plan List";
                        openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
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
                    }
                }
            }
            catch (Exception ex)
            {
                CloseProgress();
                saveButton.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewForecastList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
            {
                // not allow to sort table
                for (int i = 0; i < dataGridViewPublicHolidayList.Columns.Count; i++)
                {
                    dataGridViewPublicHolidayList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                // to resize column
                dataGridViewPublicHolidayList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                    dataGridViewPublicHolidayList.DataSource = dtExcel;

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

                    int totalrow = dataGridViewPublicHolidayList.Rows.Count;
                    rowIndexBatas = rowIndexBatas - 2;

                    //delete data start from text stencil No
                    for (int i = totalrow - 1; i > rowIndexBatas; i--)
                    {
                        dataGridViewPublicHolidayList.Rows.RemoveAt(i);
                    }

                    // change - and remove other remarks 
                    for (int i = 0; i < dataGridViewPublicHolidayList.Rows.Count; i++)
                    {
                        // get model detail
                        if (dataGridViewPublicHolidayList.Rows[i].Cells[2].Value.ToString().Contains(" MB"))
                        {
                            //get model name
                            var model = dataGridViewPublicHolidayList.Rows[i].Cells[2].Value.ToString().Split(' ');
                            dataGridViewPublicHolidayList.Rows[i].Cells[0].Value = model[0];
                        }

                        for (int j = 0; j <= dataGridViewPublicHolidayList.Columns.Count - 1; j++)
                        {
                            if (dataGridViewPublicHolidayList.Rows[i].Cells[j].Value.ToString() == "-")
                            {
                                dataGridViewPublicHolidayList.Rows[i].Cells[j].Value = 0;
                            }
                        }
                    }
                    totalLbl.Text = dataGridViewPublicHolidayList.Rows.Count.ToString();

                    //to give color if not found model in datagridview
                    int countmissingmodel = 0;
                    string modelUPH;
                    missingmodel = "";

                    for (int i = 0; i < dataGridViewPublicHolidayList.Rows.Count; ++i)
                    {
                        //Only check data MB
                        if (dataGridViewPublicHolidayList.Rows[i].Cells[0].Value.ToString() != "")
                        {
                            modelUPH = dataGridViewPublicHolidayList.Rows[i].Cells[0].Value.ToString();
                            // cek model exist or not in db
                            string cekmodel = "SELECT b.model FROM tbl_masteruph a, tbl_model b WHERE b.id = a.model AND b.model = '" + modelUPH + "'";
                            using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                            {
                                DataSet ds = new DataSet();
                                dscmd.Fill(ds);
                                // cek jika ada model tsb
                                if (ds.Tables[0].Rows.Count < 1)
                                {
                                    dataGridViewPublicHolidayList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    countmissingmodel++;
                                    missingmodel += modelUPH + "\r\n";
                                }
                            }
                        }
                    }

                    if (countmissingmodel > 0)
                    {
                        CloseProgress();
                        MessageBox.Show(this, "Missing model from model UPH, please register model uph first ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                        saveButton.Enabled = false;
                    }
                    else
                    {
                        saveButton.Enabled = true;
                        CloseProgress();
                    }

                    totalLbl.Text = dataGridViewPublicHolidayList.Rows.Count.ToString();
                }
                catch (Exception ex)
                {
                    CloseProgress();
                    saveButton.Enabled = false;
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(this, "Please select  Production Plan file with correct format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                }
            }

            private void cmbForecastFile_SelectedIndexChanged(object sender, EventArgs e)
            {
                saveButton.Enabled = false;
            }

            private void ImportForecastList_FormClosing(object sender, FormClosingEventArgs e)
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
        }
    }
