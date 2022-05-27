using MaterialSkin.Controls;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class PrintLabel : MaterialForm
    {
        private static BarTender.Application btApp = new BarTender.Application();
        private static BarTender.Format btFormat = new BarTender.Format();

        string dateTimeNow = DateTime.Now.ToString("MM.dd.yyyy");
        string company = "NUSA";

        public PrintLabel()
        {
            InitializeComponent();
        }

        private void PrintLabel_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            PrintLabNum.Text = "1";
            DateTextBox.Text = dateTimeNow;

            //Select Printer
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                InitprinterComboBox.Items.Add(printer);
            }
            InitprinterComboBox.SelectedIndex = 0;

            BarcodeTextBox.Select();
        }

        private void PrintLabel_FormClosed(object sender, FormClosedEventArgs e)
        {
            btApp.Quit(BarTender.BtSaveOptions.btSaveChanges);
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            printLabel();
        }

        private void printLabel()
        {
            string sn_string = BarcodeTextBox.Text;
            string date = DateTextBox.Text;

            InitprinterComboBox.Enabled = false;
            BarcodeTextBox.Enabled = false;
            PrintLabNum.Enabled = false;

            // Determine whether the two SN numbers are equal, if not, generate a barcode and print
            if (sn_string != string.Empty)
            {
                try
                {
                    btFormat = btApp.Formats.Open(AppDomain.CurrentDomain.BaseDirectory + "SN.btw", false, "");

                    btFormat.SetNamedSubStringValue("SN", sn_string);
                    btFormat.SetNamedSubStringValue("COMPANY", company);
                    btFormat.SetNamedSubStringValue("DATE", date);

                    //printer selected
                    btFormat.Printer = InitprinterComboBox.Text;

                    //total copies
                    int CopiesOfLabel = Int32.Parse(this.PrintLabNum.Text.ToString());
                    btFormat.IdenticalCopiesOfLabel = CopiesOfLabel;

                    btFormat.PrintOut(false, false);

                    //to save the label when exiting
                    btFormat.Close(BarTender.BtSaveOptions.btSaveChanges);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            InitprinterComboBox.Enabled = true;
            BarcodeTextBox.Enabled = true;
            PrintLabNum.Enabled = true;
            BarcodeTextBox.Clear();
            BarcodeTextBox.Select();
        }

        private void BarcodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (BarcodeTextBox.Text != "")
            //{
            //    //if user type alphabet
            //    if (System.Text.RegularExpressions.Regex.IsMatch(BarcodeTextBox.Text, "[^0-9]"))
            //    {
            //        //MessageBox.Show("Please enter only numbers.");
            //        BarcodeTextBox.Text = BarcodeTextBox.Text.Remove(BarcodeTextBox.Text.Length - 1);
            //    }
            //}
        }

        private void BarcodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (BarcodeTextBox.Text != "")
                {
                    printLabel();
                }
            }                
        }
    }
}
