using ClosedXML.Excel;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class ForecastList : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();

        public ForecastList()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewFCTList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("forecastlist LIKE '%{0}%' or importBy LIKE '%{0}%'", tbSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT forecastlist, importDate, importBy FROM tbl_forecastlist ORDER BY id DESC";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewFCTList.DataSource = dset.Tables[0];

                    // add button detail in datagridview table
                    DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                    dataGridViewFCTList.Columns.Add(btnDetail);
                    btnDetail.HeaderText = "";
                    btnDetail.Text = "Detail";
                    btnDetail.Name = "btnDetail";
                    btnDetail.UseColumnTextForButtonValue = true;

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewFCTList.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    btnDelete.UseColumnTextForButtonValue = true;
                }
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // remove data in datagridview result
            dataGridViewFCTList.DataSource = null;
            dataGridViewFCTList.Refresh();

            while (dataGridViewFCTList.Columns.Count > 0)
            {
                dataGridViewFCTList.Columns.RemoveAt(0);
            }

            LoadData();
            dataGridViewFCTList.Update();
            dataGridViewFCTList.Refresh();
        }

        private void dataGridViewPlList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
            // Set table title
            string[] title = { "FORECAST LIST", "IMPORT DATE", "IMPORT BY" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewFCTList.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewFCTList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewFCTList.Columns[1].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";
        }

        private void dataGridViewPlList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewFCTList.SelectedCells[0].RowIndex;
            string fctslctd = dataGridViewFCTList.Rows[i].Cells[0].Value.ToString();   

            if (e.ColumnIndex == 3)
            {
                DetailForecastList detailForecast = new DetailForecastList();
                detailForecast.toolStripUsername.Text = toolStripUsername.Text;
                detailForecast.tbForecastListNo.Text = fctslctd;
                detailForecast.Show();
                this.Hide();
            }

            if (e.ColumnIndex == 4)
            {
                string message = "Do you want to delete this Plan " + fctslctd + "?";
                string title = "Delete Production Plan List";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);

                    string querydelete = "DELETE FROM tbl_forecastlist WHERE forecastList = '" + fctslctd + "'";
                    connectionDB.connection.Open();

                    string[] allQuery = { querydelete};
                    for (int j = 0; j < allQuery.Length; j++)
                    {
                        cmd.CommandText = allQuery[j];
                        //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                        cmd.ExecuteNonQuery();
                        //Jalankan perintah / query dalam CommandText pada database
                    }

                    connectionDB.connection.Close();
                    ForecastList forecast = new ForecastList();
                    forecast.toolStripUsername.Text = toolStripUsername.Text;
                    forecast.Show();
                    this.Hide();
                    MessageBox.Show("Record Deleted successfully", "Forecast List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void ForecastList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadData();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            ImportForecastList importForecastList = new ImportForecastList();
            importForecastList.toolStripUsername.Text = toolStripUsername.Text;
            importForecastList.Show();
            this.Hide();
        }

        private void ForecastList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void truncateLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure want to delete All this Production Plan List Data ?";
            string title = "Delete Production Plan List";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result = MessageBox.Show(this, message, title, buttons, icon);
            if (result == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("", connectionDB.connection);

                string querydeletefctlist = "SET FOREIGN_KEY_CHECKS = 0; TRUNCATE tbl_forecastlist;";
                string querydeletefctdetail = "TRUNCATE tbl_forecastdetail";

                connectionDB.connection.Open();

                string[] allQuery = { querydeletefctlist, querydeletefctdetail };
                for (int j = 0; j < allQuery.Length; j++)
                {
                    cmd.CommandText = allQuery[j];
                    //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                    cmd.ExecuteNonQuery();
                    //Jalankan perintah / query dalam CommandText pada database
                }

                connectionDB.connection.Close();
                ForecastList forecastList = new ForecastList();
                forecastList.toolStripUsername.Text = toolStripUsername.Text;
                this.Hide();
                forecastList.Show();
                MessageBox.Show(this, "Record Deleted successfully", "Production Plan List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
