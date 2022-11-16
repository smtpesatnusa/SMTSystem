using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class AddLabel : MaterialForm
    {
        ConnectionDB connectionDB = new ConnectionDB();
        Helper help = new Helper();

        public AddLabel()
        {
            InitializeComponent();
        }

        private void PrintLabel_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //menampilkan data combobox department
            help.displayCmbList("SELECT * FROM tbl_wopegatron ORDER BY id ASC", "woNumber", "id", cmbWO);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sequencetb.Text == "" || cmbWO.Text == "" || runningNumbertb.Text == "" || modeltb.Text == "" )
                {
                    MessageBox.Show("Please fill all field properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (sequencetb.TextLength != 2)
                {
                    MessageBox.Show("Please fill sequence with 2 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (runningNumbertb.TextLength != 7)
                {
                    MessageBox.Show("Please fill Running Number with 7 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (modeltb.TextLength != 12)
                {
                    MessageBox.Show("Please fill Model with 12 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string sequence = sequencetb.Text;
                    string woNo = cmbWO.Text;
                    string runningNo = runningNumbertb.Text;
                    string model = modeltb.Text;
                    string remark = remarktb.Text;

                    //insert data to db
                    var cmd = new MySqlCommand("", connectionDB.connection);
                    connectionDB.connection.Open();
                    // query insert data patlite label and qty
                    string Query = "INSERT INTO tbl_pegatronlabel (sequence, def,  woNumber, runningNumber, model, remark, printDate, printBy) VALUES " +
                        "('" + sequence + "', 'HSF', '" + woNo + "', '" + runningNo + "', '" + model + "', '" + remark + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + userdetail.Text + "' )";

                    cmd.CommandText = Query;
                    cmd.ExecuteNonQuery();

                    // print Pegatron label
                    printLabeltoPrinter();

                    connectionDB.connection.Close();
                    sequencetb.Clear();
                    runningNumbertb.Clear();
                    sequencetb.Select();
                }
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void printLabeltoPrinter()
        {
            LabelManager2.Application labelapp = null;
            LabelManager2.Document labDoc = null;
            //string filePathName = System.Windows.Forms.Application.StartupPath + "\\Document1.lab";
            string filePathName = AppDomain.CurrentDomain.BaseDirectory + "Pcb.lab";
            string text = sequencetb.Text + " HSF " + cmbWO.Text + " " + runningNumbertb.Text + " " + modeltb.Text + " ";
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
                labDoc.Variables.FormVariables.Item("Var0").Value = cmbWO.Text;
                labDoc.Variables.FormVariables.Item("var1").Value = text;
                labDoc.PrintDocument(1);

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


        private void modeltb_Leave(object sender, EventArgs e)
        {
            modeltb.Text = modeltb.Text.ToUpper();
        }

        private void cmbWO_SelectedIndexChanged(object sender, EventArgs e)
        {
            modeltb.Clear();

            try
            {
                string query = "SELECT * FROM tbl_wopegatron WHERE id = '" + cmbWO.SelectedValue + "' ";
                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataTable dset = new DataTable();
                    adpt.Fill(dset);

                    if (dset.Rows.Count > 0)
                    {
                        for (int j = 0; j < dset.Rows.Count; j++)
                        {
                            string model = dset.Rows[0]["model"].ToString();
                            modeltb.Text = model;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }

        private void runningNumbertb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void runningNumbertb_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(runningNumbertb.Text, "[^0-9]"))
            {
                runningNumbertb.Clear();
            }
        }

        private void sequencetb_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }
    }
}
