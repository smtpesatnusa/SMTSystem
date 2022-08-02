using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class ImportMasterMaterialXM : MaterialForm
    {
        LoadForm lf = new LoadForm();
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        public ImportMasterMaterialXM()
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
                    Thread th = new Thread(ShowProgress)
                    {
                        IsBackground = false
                    };
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
                    DataTable dtExcel = new DataTable();
                    dtExcel = help.GetDataFromExcel(path, sheet); //read excel file  
                    dataGridViewMasterMaterial.DataSource = dtExcel;
                    totalLbl.Text = dataGridViewMasterMaterial.Rows.Count.ToString();
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

                    bgWorker.WorkerSupportsCancellation = true;

                    if (!bgWorker.IsBusy)
                        bgWorker.RunWorkerAsync();

                    CloseProgress();

                    MessageBox.Show(this, "Master Material will Uploaded in Background", "Master Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    MasterMaterialXM mm = new MasterMaterialXM();
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
            MasterMaterialXM mm = new MasterMaterialXM();
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
        }

        private void ImportMasterMaterial_FormClosing(object sender, FormClosingEventArgs e)
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

        private void ImportMasterMaterial_Load(object sender, EventArgs e)
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
                    string importDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string importBy = idUser;

                    // query insert data part code
                    string StrQuery = "INSERT INTO tbl_mastermaterial VALUES (null,'"
                         + material + "','"
                         + importDate + "', '"
                         + importBy + "'); ";

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
    }
}
