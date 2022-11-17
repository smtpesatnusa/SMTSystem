using LabelManager2;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class PegatronLabel : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        string idslctd, sequenceslctd, defslctd, woslctd, rnslctd, modelslctd;

        public PegatronLabel()
        {
            InitializeComponent();
        }

        private void PrintLabel_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);


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

        private void userLevel()
        {
            string query = "SELECT ROLE FROM tbl_user WHERE username = '" + userdetail.Text + "'";
            using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
            {
                DataTable dset = new DataTable();
                adpt.Fill(dset);
                if (dset.Rows.Count > 0)
                {
                    string roleslctd = dset.Rows[0]["role"].ToString();
                    levelUser.Text = roleslctd;
                }
            }
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            if (qtytb.Text != "")
            {
                int qtyPrint = Convert.ToInt32(qtytb.Text);

                if (qtyPrint == 0 )
                {
                    MessageBox.Show("Please fill qty print properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    modeltb.Clear();
                    modeltb.Select();
                }
                else
                {
                    try
                    {
                        // print Pegatron label
                        printLabeltoPrinter();

                        //update data to db
                        var cmd = new MySqlCommand("", connectionDB.connection);
                        connectionDB.connection.Open();
                        // query insert hisyory
                        string QueryPrint = "INSERT INTO tbl_historyprintpega (labelID, qty, printDate, printBy) VALUES('" + idslctd + "', '" + qtyPrint + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + userdetail.Text + "' )";

                        cmd.CommandText = QueryPrint;
                        cmd.ExecuteNonQuery();

                        connectionDB.connection.Close();
                        this.Close();
                        MessageBox.Show(this, "Pegatron Label Successfully Print", "Print Pegatron Label", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill qty print properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qtytb.Select();
            }
        }

        private void printLabeltoPrinter()
        {
            LabelManager2.Application labelapp = null;
            LabelManager2.Document labDoc = null;
            //string filePathName = System.Windows.Forms.Application.StartupPath + "\\Document1.lab";
            string filePathName = AppDomain.CurrentDomain.BaseDirectory + "Pcb.lab";
            string text = sequenceslctd + " " + defslctd + " " + woslctd + " " + rnslctd + " " + modelslctd + " ";
            string barcode = woNumbertb.Text + "" + runningNumbertb.Text;
            if (!File.Exists(filePathName))
            {
                MessageBox.Show("File not found");
                return;
            }
            try
            {
                labelapp = new LabelManager2.Application();
                labelapp.Documents.Open(filePathName, false);
                labDoc = labelapp.ActiveDocument;
                labDoc.Variables.FormVariables.Item("Var0").Value = barcode;
                labDoc.Variables.FormVariables.Item("var1").Value = text;
                labDoc.PrintDocument(Convert.ToInt32(qtytb.Text));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                labelapp.Documents.CloseAll(true);
                labelapp.Quit();
                labelapp = null;
                labDoc = null;
            }
        }

        private void qtytb_TextChanged(object sender, EventArgs e)
        {
            
            //if user type alphabet
            if (System.Text.RegularExpressions.Regex.IsMatch(qtytb.Text, "[^0-9]"))
            {
                qtytb.Clear();
            }
        }
    }
}
