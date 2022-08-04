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

            if (cmbForecastFile.Text == "" || cmbForecastFile.Text == "" || dataGridViewProdPlanList.Rows.Count == 0)
            {
                saveButton.Enabled = true;
                MessageBox.Show(this, "Unable to import Forecast List without choose file properly", "Forecast List", MessageBoxButtons.OK, MessageBoxIcon.Error);
                homeButton.Enabled = true;
                BackButton.Enabled = true;
            }
            else
            {
                try
                {
                    StartProgress("Loading...");
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
                            CloseProgress();
                            MessageBox.Show(this, "Unable to import Forecast List, Forecast List already insert", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            resetData();
                        }
                        else
                        {
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
            dataGridViewProdPlanList.DataSource = null;
            dataGridViewProdPlanList.Refresh();

            while (dataGridViewProdPlanList.Columns.Count > 0)
            {
                dataGridViewProdPlanList.Columns.RemoveAt(0);
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
                string insert = "INSERT INTO tbl_forecastlist (forecastList, importDate, importBy) VALUES ('" + forecastNo + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + idUser + "')";
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
                        for (int i = 0; i < dataGridViewProdPlanList.Rows.Count; i++)
                        {
                            // insert data plan only 
                            if (dataGridViewProdPlanList.Rows[i].Cells[0].Value.ToString() != "")
                            {
                                string model = dataGridViewProdPlanList.Rows[i].Cells[0].Value.ToString();
                                string modelNo = dataGridViewProdPlanList.Rows[i].Cells[1].Value.ToString();
                                string desc = dataGridViewProdPlanList.Rows[i].Cells[2].Value.ToString();
                                string detail = dataGridViewProdPlanList.Rows[i].Cells[3].Value.ToString();
                                string date1 = dataGridViewProdPlanList.Rows[i].Cells[4].Value.ToString();
                                string date2 = dataGridViewProdPlanList.Rows[i].Cells[5].Value.ToString();
                                string date3 = dataGridViewProdPlanList.Rows[i].Cells[6].Value.ToString();
                                string date4 = dataGridViewProdPlanList.Rows[i].Cells[7].Value.ToString();
                                string date5 = dataGridViewProdPlanList.Rows[i].Cells[8].Value.ToString();
                                string date6 = dataGridViewProdPlanList.Rows[i].Cells[9].Value.ToString();
                                string date7 = dataGridViewProdPlanList.Rows[i].Cells[10].Value.ToString();
                                string date8 = dataGridViewProdPlanList.Rows[i].Cells[11].Value.ToString();
                                string date9 = dataGridViewProdPlanList.Rows[i].Cells[12].Value.ToString();
                                string date10 = dataGridViewProdPlanList.Rows[i].Cells[13].Value.ToString();
                                string date11 = dataGridViewProdPlanList.Rows[i].Cells[14].Value.ToString();
                                string date12 = dataGridViewProdPlanList.Rows[i].Cells[15].Value.ToString();
                                string date13 = dataGridViewProdPlanList.Rows[i].Cells[16].Value.ToString();
                                string date14 = dataGridViewProdPlanList.Rows[i].Cells[17].Value.ToString();
                                string date15 = dataGridViewProdPlanList.Rows[i].Cells[18].Value.ToString();
                                string date16 = dataGridViewProdPlanList.Rows[i].Cells[19].Value.ToString();
                                string date17 = dataGridViewProdPlanList.Rows[i].Cells[20].Value.ToString();
                                string date18 = dataGridViewProdPlanList.Rows[i].Cells[21].Value.ToString();
                                string date19 = dataGridViewProdPlanList.Rows[i].Cells[22].Value.ToString();
                                string date20 = dataGridViewProdPlanList.Rows[i].Cells[23].Value.ToString();
                                string date21 = dataGridViewProdPlanList.Rows[i].Cells[24].Value.ToString();
                                string date22 = dataGridViewProdPlanList.Rows[i].Cells[25].Value.ToString();
                                string date23 = dataGridViewProdPlanList.Rows[i].Cells[26].Value.ToString();
                                string date24 = dataGridViewProdPlanList.Rows[i].Cells[27].Value.ToString();
                                string date25 = dataGridViewProdPlanList.Rows[i].Cells[28].Value.ToString();
                                string date26 = dataGridViewProdPlanList.Rows[i].Cells[29].Value.ToString();
                                string date27 = dataGridViewProdPlanList.Rows[i].Cells[30].Value.ToString();
                                string date28 = dataGridViewProdPlanList.Rows[i].Cells[31].Value.ToString();
                                string date29 = dataGridViewProdPlanList.Rows[i].Cells[32].Value.ToString();
                                string date30 = dataGridViewProdPlanList.Rows[i].Cells[33].Value.ToString();
                                string date31 = dataGridViewProdPlanList.Rows[i].Cells[34].Value.ToString();

                                string StrQuery = "INSERT INTO tbl_forecastdetail " +
                                    "(forecastid, model, modelno, descr, detail, date1,date2,date3,date4,date5,date6,date7,date8,date9,date10,date11,date12,date13,date14,date15" +
                                    ",date16,date17,date18,date19,date20,date21,date22,date23,date24,date25,date26,date27,date28,date29,date30,date31) " +
                                    "VALUES ('"
                                     + idForecast + "','"
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
                        MessageBox.Show(this, "Import Forecast List complete", "Forecast List", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Fail to import Forecast file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
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
            for (int i = 0; i < dataGridViewProdPlanList.Columns.Count; i++)
            {
                dataGridViewProdPlanList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // to resize column
            dataGridViewProdPlanList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                dataGridViewProdPlanList.DataSource = dtExcel;

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

                int totalrow = dataGridViewProdPlanList.Rows.Count;
                rowIndexBatas = rowIndexBatas - 2;

                //delete data start from text stencil No
                for (int i = totalrow - 1; i > rowIndexBatas; i--)
                {
                    dataGridViewProdPlanList.Rows.RemoveAt(i);
                }

                // change - and remove other remarks 
                for (int i = 0; i < dataGridViewProdPlanList.Rows.Count; i++)
                {
                    // get model detail
                    if (dataGridViewProdPlanList.Rows[i].Cells[2].Value.ToString().Contains(" MB"))
                    {
                        //get model name
                        var model = dataGridViewProdPlanList.Rows[i].Cells[2].Value.ToString().Split(' ');
                        dataGridViewProdPlanList.Rows[i].Cells[0].Value = model[0];
                    }

                    for (int j = 0; j <= dataGridViewProdPlanList.Columns.Count - 1; j++)
                    {
                        if (dataGridViewProdPlanList.Rows[i].Cells[j].Value.ToString() == "-")
                        {
                            dataGridViewProdPlanList.Rows[i].Cells[j].Value = 0;
                        }
                    }
                }
                totalLbl.Text = dataGridViewProdPlanList.Rows.Count.ToString();

                //to give color if not found model in datagridview
                int countmissingmodel = 0;
                string modelUPH;
                missingmodel = "";

                for (int i = 0; i < dataGridViewProdPlanList.Rows.Count; ++i)
                {
                    //Only check data MB
                    if (dataGridViewProdPlanList.Rows[i].Cells[0].Value.ToString() != "" )
                    {
                        modelUPH = dataGridViewProdPlanList.Rows[i].Cells[0].Value.ToString();
                        // cek model exist or not in db
                        string cekmodel = "SELECT b.model FROM tbl_masteruph a, tbl_model b WHERE b.id = a.model AND b.model = '" + modelUPH + "'";
                        using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekmodel, connectionDB.connection))
                        {
                            DataSet ds = new DataSet();
                            dscmd.Fill(ds);
                            // cek jika ada model tsb
                            if (ds.Tables[0].Rows.Count < 1)
                            {
                                dataGridViewProdPlanList.Rows[i].DefaultCellStyle.BackColor = Color.Red;
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

                totalLbl.Text = dataGridViewProdPlanList.Rows.Count.ToString();
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

        private void ImportForecastList_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
