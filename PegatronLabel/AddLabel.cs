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

        public AddLabel()
        {
            InitializeComponent();
        }

        private void PrintLabel_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (sequencetb.Text == "" || woNumbertb.Text == "" || runningNumbertb.Text == "" || modeltb.Text == "" )
                {
                    MessageBox.Show("Please fill all field properly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (sequencetb.TextLength != 2)
                {
                    MessageBox.Show("Please fill sequence with 2 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (woNumbertb.TextLength != 8)
                {
                    MessageBox.Show("Please fill WO Number with 8 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string woNo = woNumbertb.Text;
                    string runningNo = runningNumbertb.Text;
                    string model = modeltb.Text;

                    string cek = "SELECT id FROM tbl_pegatronlabel WHERE sequence = '"+sequence+"' AND woNumber = '"+woNo+"' " +
                        "AND runningNumber = '"+runningNo+"' AND model = '"+model+"'";
                        
                    using (MySqlDataAdapter adpt = new MySqlDataAdapter(cek, connectionDB.connection))
                    {
                        DataSet ds = new DataSet();
                        adpt.Fill(ds);

                        // cek jika modelno tsb sudah di upload
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show(this, "Pegatron Label already added, Unable to add pegatron label with similar detail", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            //insert data to db
                            var cmd = new MySqlCommand("", connectionDB.connection);
                            connectionDB.connection.Open();
                            // query insert data patlite label and qty
                            string Query = "INSERT INTO tbl_pegatronlabel (sequence, def,  woNumber, runningNumber, model, createDate, createBy) VALUES " +
                                "('" + sequence + "', 'HSF', '" + woNo + "', '" + runningNo + "', '" + model + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + userdetail.Text + "' )";

                            cmd.CommandText = Query;
                            cmd.ExecuteNonQuery();
                            connectionDB.connection.Close();



                            MessageBox.Show(this, "Pegatron Label Successfully Added", "Add Pegatron Label", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
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
            string text = sequencetb.Text + " HSF " + woNumbertb.Text + " " + runningNumbertb.Text + " " + modeltb.Text + " ";
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
                labDoc.Variables.FormVariables.Item("Var0").Value = woNumbertb.Text;
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



        private void sequencetb_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(sequencetb.Text, "[^0-9]"))
            {
                sequencetb.Text = sequencetb.Text.Remove(sequencetb.Text.Length - 1);
            }
        }

        private void runningNumbertb_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(runningNumbertb.Text, "[^0-9]"))
            {
                runningNumbertb.Clear();
            }
        }

        private void woNumbertb_Leave(object sender, EventArgs e)
        {
            woNumbertb.Text = woNumbertb.Text.ToUpper();
        }

        private void modeltb_Leave(object sender, EventArgs e)
        {
            modeltb.Text = modeltb.Text.ToUpper();
        }
    }
}
