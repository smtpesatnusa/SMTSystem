using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class ImportMasterMaterial : MaterialForm
    {
        LoadForm lf = new LoadForm();
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        DataTable dtExcel = new DataTable();

        public ImportMasterMaterial()
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
            openFileDialogMM.Title = "Please Select a File Master Material";
            openFileDialogMM.Filter = "Excel Files|*.xls;*.xlsx;";
            if (openFileDialogMM.ShowDialog() == DialogResult.OK)
            {
                StartProgress("Loading...");
                string MMFileName = openFileDialogMM.FileName;
                tbfilepathMM.Text = MMFileName;
                try
                {
                    string path = MMFileName.ToLower();
                    int sheet = 1;
                    //DataTable dtExcel = new DataTable();
                    //dtExcel = help.GetDataFromExcel(path, sheet); //read excel file  
                    dtExcel = help.ReadDataExcel(path); //read excel file  
                    dataGridViewMasterMaterial.DataSource = dtExcel;
                    saveButton.Enabled = true;
                    CloseProgress();
                }
                catch (Exception ex)
                {
                    CloseProgress();
                    MessageBox.Show(ex.Message);
                    MessageBox.Show(this, "Please select Master Material file with correct format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error 
                }
            }
        }

        public static void BatchUpdate(DataTable dataTable, Int32 batchSize)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            using (MySqlConnection connection = new MySqlConnection(ConnectionDB.strProvider))
            {
                // Create a SqlDataAdapter.  
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                //// Set the UPDATE command and parameters.  
                //adapter.UpdateCommand = new SqlCommand(
                //    "UPDATE Production.ProductCategory SET "
                //    + "Name=@Name WHERE ProductCategoryID=@ProdCatID;",
                //    connection);
                //adapter.UpdateCommand.Parameters.Add("@Name",
                //   SqlDbType.NVarChar, 50, "Name");
                //adapter.UpdateCommand.Parameters.Add("@ProdCatID",
                //   SqlDbType.Int, 4, "ProductCategoryID");
                //adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;

                // Set the INSERT command and parameter.  
                adapter.InsertCommand = new MySqlCommand(
                    "INSERT INTO tbl_mastermaterial (material, materialdescription, lastChange, materialtype, baseUoM, createdby, materialOri) VALUES " +
                    "(@material, @materialdescription, @lastChange, @materialtype, @baseUoM, @createdby, @materialOri);",
                    connection);
                adapter.InsertCommand.Parameters.Add("@material", MySqlDbType.VarChar, 50, "material");
                adapter.InsertCommand.Parameters.Add("@materialdescription", MySqlDbType.VarChar, 50, "Material description");
                adapter.InsertCommand.Parameters.Add("@lastChange", MySqlDbType.VarChar, 50, "Last Change");
                adapter.InsertCommand.Parameters.Add("@materialtype", MySqlDbType.VarChar, 50, "Material type");
                adapter.InsertCommand.Parameters.Add("@baseUoM", MySqlDbType.VarChar, 50, "Base Unit of Measure");
                adapter.InsertCommand.Parameters.Add("@createdby", MySqlDbType.VarChar, 50, "Created by");
                adapter.InsertCommand.Parameters.Add("@materialOri", MySqlDbType.VarChar, 50, "xx");
                adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                //// Set the DELETE command and parameter.  
                //adapter.DeleteCommand = new SqlCommand(
                //    "DELETE FROM Production.ProductCategory "
                //    + "WHERE ProductCategoryID=@ProdCatID;", connection);
                //adapter.DeleteCommand.Parameters.Add("@ProdCatID",
                //  SqlDbType.Int, 4, "ProductCategoryID");
                //adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;

                adapter.InsertCommand.CommandTimeout = 0;

                // Set the batch size.  
                adapter.UpdateBatchSize = batchSize;

                // Execute the update.  
                adapter.Update(dataTable);
            }

            stopwatch.Stop();
            Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");
        }


        public static void BulkToMySQL()
        {
            string host = "localhost";
            string database = "newpedev";
            string userDB = "root";
            string password = "";

            string ConnectionString = "server=" + host + ";Database=" + database + ";User ID=" + userDB + ";Password=" + password;
            MySqlCommand sCommand = new MySqlCommand("INSERT INTO tbl_mastermaterial (material, materialdescription) VALUES ");
            using (MySqlConnection mConnection = new MySqlConnection(ConnectionString))
            {
                List<string> Rows = new List<string>();
                for (int i = 0; i < 100000; i++)
                {
                    Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString("test"), MySqlHelper.EscapeString("test")));
                }

                //sCommand.Append(string.Join(",", Rows));
                //sCommand.Append(";");
                mConnection.Open();
                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                {
                    myCmd.CommandType = CommandType.Text;
                    myCmd.ExecuteNonQuery();
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
                    MessageBox.Show(this, "Unable to import Master Material without choose file properly", "Master Material", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    homeButton.Enabled = true;
                    backButton.Enabled = true;
                }

                else
                {
                    //CSVToMySQL();

                    ////BatchUpdate(dtExcel, 10000);

                    bgWorker.WorkerSupportsCancellation = true;

                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync();

                    CloseProgress();

                    MessageBox.Show(this, "Master Material will Uploaded in Background", "Master Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MasterMaterial mm = new MasterMaterial();
                    mm.toolStripUsername.Text = toolStripUsername.Text;
                    mm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MasterMaterial mm = new MasterMaterial();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            this.Hide();
            mm.Show();
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

        private void dataGridViewMasterMaterial_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // not allow to sort table
            for (int i = 0; i < dataGridViewMasterMaterial.Columns.Count; i++)
            {
                dataGridViewMasterMaterial.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //// Set table title 
            //string[] title = { "Material", "Plant", "Valuation Type",  "Material Description", "Last Change", "Material type", "Material Group", "Base UoM", "Purchasing Group", "ABC Indicator", "MRP Type", "Valuation Class", "Price control", "Price", "Currency", "Price unit", "Created by"};

            //for (int i = 0; i < title.Length; i++)
            //{
            //    dataGridViewMasterMaterial.Columns[i].HeaderText = "" + title[i];
            //}

            //        //memberi nomor row
            //        for (int i = 0; i < dataGridViewMasterMaterial.Rows.Count; ++i)
            //        {
            //            int row = i + 1;
            //            dataGridViewMasterMaterial.Rows[i].HeaderCell.Value = "" + row;
            //        }

            //dataGridViewMasterMaterial.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }

        private void ImportMasterMaterial_FormClosing(object sender, FormClosingEventArgs e)
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

        private void ImportMasterMaterial_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var cmd = new MySqlCommand("", connectionDB.connection);
                connectionDB.connection.Open();
                //Buka koneksi

                for (int i = 0; i < dataGridViewMasterMaterial.Rows.Count; i++)
                {
                    string material = dataGridViewMasterMaterial.Rows[i].Cells[0].Value.ToString();
                    string materialOri = material.Substring(2);
                    //materialOri = materialOri.Substring(0, materialOri.Length - 1);
                    string materialDesc = dataGridViewMasterMaterial.Rows[i].Cells[3].Value.ToString().Replace("'", "''");
                    string lastChange = dataGridViewMasterMaterial.Rows[i].Cells[4].Value.ToString();
                    string materialType = dataGridViewMasterMaterial.Rows[i].Cells[5].Value.ToString();
                    string baseUOM = dataGridViewMasterMaterial.Rows[i].Cells[7].Value.ToString();
                    string createdBy = dataGridViewMasterMaterial.Rows[i].Cells[16].Value.ToString();
                    string ftype = "-";

                    // query insert data part code
                    string StrQuery = "INSERT INTO tbl_mastermaterial VALUES (null,'"
                         + material + "','"
                         + materialDesc + "', '"
                         + lastChange + "', '"
                         + materialType + "', '"
                         + baseUOM + "', '"
                         + createdBy + "', '"
                         + ftype + "','"
                         + idUser + "','-'); ";

                    cmd.CommandText = StrQuery;
                    cmd.ExecuteNonQuery();
                }

                stopwatch.Stop();
                Debug.WriteLine(" inserts took " + stopwatch.ElapsedMilliseconds + " ms");

                connectionDB.connection.Close();
                //Tutup koneksi
                MessageBox.Show(this, "Import Master Material complete", "Master Material", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }

        }

        public static void CSVToMySQL()
        {
            string Command = "INSERT INTO tbl_mastermaterial (material, materialdescription, lastChange, materialtype, baseUoM, createdby, materialOri) VALUES " +
                    "(@material, @materialdescription, @lastChange, @materialtype, @baseUoM, @createdby, @materialOri);";

            using (MySqlConnection mConnection = new MySqlConnection(ConnectionDB.strProvider))
            {
                mConnection.Open();

                for (int i = 0; i < 100000; i++) //inserting 100k items
                    using (MySqlCommand myCmd = new MySqlCommand(Command, mConnection))
                    {
                        myCmd.CommandType = CommandType.Text;
                        myCmd.Parameters.AddWithValue("@material", "material");
                        myCmd.Parameters.AddWithValue("@materialdescription", "Material description");
                        myCmd.Parameters.AddWithValue("@lastChange", "Last Change");
                        myCmd.Parameters.AddWithValue("@materialtype", "Material type");
                        myCmd.Parameters.AddWithValue("@baseUoM", "Base Unit of Measure");
                        myCmd.Parameters.AddWithValue("@createdby", "Created by");
                        myCmd.Parameters.AddWithValue("@materialOri", "xx");

                        myCmd.ExecuteNonQuery();
                    }
            }
        }
    }
}
