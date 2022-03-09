using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class LabelList : MaterialForm
    {
        LoadForm lf = new LoadForm();
        ConnectionDB connectionDB = new ConnectionDB();
        private DataSet ds;
        private DataTable dtSource;

        public LabelList()
        {
            InitializeComponent();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dataGridViewLabelList.DataSource as DataTable).DefaultView.RowFilter =
                    string.Format("custname LIKE '{0}%' or processCode LIKE '{0}%' or sapMaterialNumber LIKE '{0}%' or plant LIKE '{0}%'" +
                    " or altBom LIKE '{0}%' or descr LIKE '{0}%' or sapPartNo LIKE '{0}%' or llModel LIKE '{0}%'" +
                    " or llDesc LIKE '{0}%' or apsModelCurrent LIKE '{0}%' or pcsModelCurrent LIKE '{0}%' or sideAMMSCurrent LIKE '{0}%' " +
                    " or CONVERT(pointLL, System.String) LIKE '{0}%' or CONVERT(pointSAP, System.String) LIKE '{0}%' or CONVERT(cavityPerPanel, System.String) LIKE '{0}%' " +
                    " or STATUS LIKE '{0}%' or loadingListRev LIKE '{0}%'", tbSearch.Text);
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


        private void LabelList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            try
            {
                connectionDB.connection.Open();

                string query = "SELECT PO_no, qty, importDate, importBy, STATUS FROM tbl_po WHERE STATUS !='create'";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);
                    dataGridViewLabelList.DataSource = dset.Tables[0];

                    // add button delete in datagridview table
                    DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                    dataGridViewLabelList.Columns.Add(btnDelete);
                    btnDelete.HeaderText = "";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";
                    btnDelete.UseColumnTextForButtonValue = true;

                    // add button detail in datagridview table
                    DataGridViewButtonColumn btnDetail = new DataGridViewButtonColumn();
                    dataGridViewLabelList.Columns.Add(btnDetail);
                    btnDetail.HeaderText = "";
                    btnDetail.Text = "Detail";
                    btnDetail.Name = "btnDetail";
                    btnDetail.UseColumnTextForButtonValue = true;
                }

                dataGridViewLabelList.Columns[2].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm:ss";
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewLabelList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //// Set table title
            //string[] title = { "PO NO", "QTY", "Create Date", "Create By" };
            //for (int i = 0; i < title.Length; i++)
            //{
            //    dataGridViewLabelList.Columns[i].HeaderText = "" + title[i];
            //}
            dataGridViewLabelList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }

        private void importLabelListButton_Click(object sender, EventArgs e)
        {
            ImportLabel importLabel = new ImportLabel();
            importLabel.toolStripUsername.Text = toolStripUsername.Text;
            importLabel.Show();
            this.Hide();
        }

        private void LabelList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void tbTruncateSAPRef_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string message = "Are you sure want to delete All this AOHAI Label ?";
            string title = "Delete AOHAI LAbel";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result = MessageBox.Show(this, message, title, buttons, icon);
            if (result == DialogResult.Yes)
            {
                var cmd = new MySqlCommand("", connectionDB.connection);

                string querydeleteSAPmaster = "TRUNCATE tbl_labellist";

                connectionDB.connection.Open();

                string[] allQuery = { querydeleteSAPmaster };
                for (int j = 0; j < allQuery.Length; j++)
                {
                    cmd.CommandText = allQuery[j];
                    //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                    cmd.ExecuteNonQuery();
                    //Jalankan perintah / query dalam CommandText pada database
                }

                connectionDB.connection.Close();
                LabelList labelList = new LabelList();
                labelList.toolStripUsername.Text = toolStripUsername.Text;
                labelList.Show();
                this.Hide();
                MessageBox.Show(this, "Record Deleted successfully", "Label AOHAI", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
            }
        }

        private void dataGridViewLabelList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridViewLabelList.SelectedCells[0].RowIndex;
            string poslctd = dataGridViewLabelList.Rows[i].Cells[0].Value.ToString();

            if (e.ColumnIndex == 5)
            {
                string message = "Do you want to delete this label data with Purchase Order with No " + poslctd + "?";
                string title = "Delete Label Data";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show(this, message, title, buttons, icon);

                if (result == DialogResult.Yes)
                {
                    string cekpn = "SELECT STATUS FROM tbl_po WHERE PO_no = '"+poslctd+"'";
                    using (MySqlDataAdapter dscmd = new MySqlDataAdapter(cekpn, connectionDB.connection))
                    {
                        DataTable ds = new DataTable();
                        dscmd.Fill(ds);
                        // cek jika isn tsb sudah di upload
                        if (ds.Rows.Count > 0)
                        {
                            string statusPO = ds.Rows[0]["STATUS"].ToString();

                            if (statusPO == "create" || statusPO == "import")
                            {
                                var cmd = new MySqlCommand("", connectionDB.connection);
                                string querydeletePO = "DELETE FROM tbl_po WHERE PO_no = '" + poslctd + "'";
                                string querydeleteLabelData = "DELETE FROM tbl_labellist WHERE PO_No = '" + poslctd + "'";
                                connectionDB.connection.Open();

                                string[] allQuery = { querydeletePO, querydeleteLabelData };
                                for (int j = 0; j < allQuery.Length; j++)
                                {
                                    cmd.CommandText = allQuery[j];
                                    //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                                    cmd.ExecuteNonQuery();
                                    //Jalankan perintah / query dalam CommandText pada database
                                }

                                connectionDB.connection.Close();
                                LabelList labelList = new LabelList();
                                labelList.toolStripUsername.Text = toolStripUsername.Text;
                                labelList.Show();
                                this.Hide();
                                MessageBox.Show("Record Deleted successfully", "Label Data List Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Unable delete label Data", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                else
                {
                }
            }
            if (e.ColumnIndex == 6)
            {
                //MainMenu mm = new MainMenu();
                DetailLabel detailLabel = new DetailLabel();
                detailLabel.toolStripUsername.Text = toolStripUsername.Text;
                //detailLabel.ContextMenuStrip = mm.contextMenuStrip;
                detailLabel.po_No.Text = poslctd;
                detailLabel.Show();
                this.Hide();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }
    }
}
