using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class Modeluphlist : MaterialForm
    {
        LoadForm lf = new LoadForm();
        ConnectionDB connectionDB = new ConnectionDB();

        public Modeluphlist()
        {
            InitializeComponent();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewModeluphlist.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("model LIKE '{0}%' or uph LIKE '{0}%'", tbSearch.Text);
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

        private void refreshLbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // remove data in datagridview result
            dataGridViewModeluphlist.DataSource = null;
            dataGridViewModeluphlist.Refresh();

            while (dataGridViewModeluphlist.Columns.Count > 0)
            {
                dataGridViewModeluphlist.Columns.RemoveAt(0);
            }

            LoadData();

            dataGridViewModeluphlist.Update();
            dataGridViewModeluphlist.Refresh();
        }

        private void LoadData()
        {
            try
            {
                connectionDB.connection.Open();

                string query = "SELECT b.model, a.uph FROM tbl_masteruph a, tbl_model b WHERE a.model = b.id ORDER BY a.id DESC";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewModeluphlist.DataSource = dset.Tables[0];

                    // add button edit in datagridview table
                    DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                    dataGridViewModeluphlist.Columns.Add(btnEdit);
                    btnEdit.HeaderText = "";
                    btnEdit.Text = "Edit";
                    btnEdit.Name = "btnEdit";
                    btnEdit.UseColumnTextForButtonValue = true;

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewModeluphlist.Columns.Add(btnDelete);
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

        private void dataGridViewModellist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewModeluphlist.SelectedCells[0].RowIndex;
            string modeluphslctd = dataGridViewModeluphlist.Rows[i].Cells[0].Value.ToString();
            string uphslctd = dataGridViewModeluphlist.Rows[i].Cells[1].Value.ToString();

            if (e.ColumnIndex == 2)
            {
                Editmodeluph editmodeluph = new Editmodeluph();
                editmodeluph.usernameLbl.Text = toolStripUsername.Text;
                editmodeluph.tbModel.Text = modeluphslctd;
                editmodeluph.tbUph.Text = uphslctd;
                editmodeluph.ShowDialog();
            }
            if (e.ColumnIndex == 3)
            {
                string message = "Do you want to delete this Model UPH " + modeluphslctd + "?";
                string title = "Delete Model UPH";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    var cmd = new MySqlCommand("", connectionDB.connection);

                    string querydelete = "DELETE FROM tbl_masteruph WHERE model IN (SELECT id FROM tbl_model WHERE model = '"+modeluphslctd+"')";
                    connectionDB.connection.Open();

                    string[] allQuery = { querydelete };
                    for (int j = 0; j < allQuery.Length; j++)
                    {
                        cmd.CommandText = allQuery[j];
                        //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                        cmd.ExecuteNonQuery();
                        //Jalankan perintah / query dalam CommandText pada database
                    }

                    connectionDB.connection.Close();
                    Modeluphlist ml = new Modeluphlist();
                    ml.toolStripUsername.Text = toolStripUsername.Text;
                    ml.Show();
                    this.Hide();
                    MessageBox.Show("Record Deleted successfully", "Model UPH List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void addmodel_Click(object sender, EventArgs e)
        {
            Addmodeluph addmodeluph = new Addmodeluph();
            addmodeluph.usernameLbl.Text = toolStripUsername.Text;
            addmodeluph.Show();            
        }

        private void Modelmasterlist_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MaterialDialog materialDialog = new MaterialDialog(this, title, message, "OK", true, "Cancel");
            DialogResult result = materialDialog.ShowDialog(this);
            if (result.ToString() == "OK")
            {
                System.Windows.Forms.Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(result.ToString(), 750);
                SnackBarMessage.Show(this);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void dataGridViewModellist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "MODEL", "UPH" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewModeluphlist.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewModeluphlist.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            
        }

        private void Modeluphlist_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadData();
        }
    }
    
}
