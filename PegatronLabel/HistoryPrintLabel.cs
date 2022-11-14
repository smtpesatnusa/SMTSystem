using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class HistoryPrintLabel : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();

        string idslctd, sequenceslctd, defslctd, woslctd, rnslctd, modelslctd;
        public HistoryPrintLabel()
        {
            InitializeComponent();
        }

        private void PrintLabel_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            // display detail pegatron label
            getDataLabel();

            // display history data to datagridview
            getDataHistory();
        }

        private void getDataLabel()
        {
            // get detail label
            try
            {
                string query = "SELECT id, sequence, def, woNumber, runningNumber, model FROM tbl_pegatronlabel WHERE id = '" + idLabel.Text + "'";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);
                    if (dset.Rows.Count > 0)
                    {
                        idslctd = dset.Rows[0]["id"].ToString();
                        sequenceslctd = dset.Rows[0]["sequence"].ToString();
                        defslctd = dset.Rows[0]["def"].ToString();
                        woslctd = dset.Rows[0]["woNumber"].ToString();
                        rnslctd = dset.Rows[0]["runningNumber"].ToString();
                        modelslctd = dset.Rows[0]["model"].ToString();

                        sequencetb.Text = sequenceslctd;
                        woNumbertb.Text = woslctd;
                        runningNumbertb.Text = rnslctd;
                        modeltb.Text = modelslctd;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getDataHistory()
        {
            string query = "SELECT a.qty, a.printDate, b.name FROM tbl_historyprintpega a, tbl_user b, tbl_pegatronlabel c " +
                "WHERE a.printBy = b.username AND c.id = a.labelID AND c.id = '" + idLabel.Text + "' ORDER BY a.id ASC";
                
            using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
            {
                DataSet dset = new DataSet();
                adpt.Fill(dset);
                dataGridViewHistory.DataSource = dset.Tables[0];
            }
        }

        private void dataGridViewHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Set table title
            string[] title = { "Qty", "Print Date", "Print By" };
            for (int i = 0; i < title.Length; i++)
            {
                dataGridViewHistory.Columns[i].HeaderText = "" + title[i];
            }

            dataGridViewHistory.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewHistory.Columns[1].DefaultCellStyle.Format = "dddd, dd MMMM yyyy HH:mm";
            dataGridViewHistory.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;            
        }
    }
}
