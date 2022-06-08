using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Tkx.Lppa;
using DateTime = System.DateTime;

namespace SMTPE
{
    public partial class PrintLabelNew : MaterialForm
    {
        Helper help = new Helper();

        private Tkx.Lppa.Application _csApp;
        private Document _csDoc;

        //private static BarTender.Application btApp = new BarTender.Application();
        //private static BarTender.Format btFormat = new BarTender.Format();

        string dateTimeNow = DateTime.Now.ToString("MM.dd.yyyy");
        string company = "NUSA";

        public PrintLabelNew()
        {
            InitializeComponent();
        }       

        private void PrintLabel_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);

            _csApp = new Tkx.Lppa.Application();

            PrintLabNum.Text = "1";
            DateTextBox.Text = dateTimeNow;
            
            // update printer list
            help.UpdatePrinterList(InitprinterComboBox);
            // select top printer
            InitprinterComboBox.SelectedIndex = 0;

            BarcodeTextBox.Select();
        }
           
        

        private void PrintLabel_FormClosed(object sender, FormClosedEventArgs e)
        {
            _csApp.Quit();
            //btApp.Quit(BarTender.BtSaveOptions.btSaveChanges);
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
                    _csApp.Documents.Open(AppDomain.CurrentDomain.BaseDirectory + "scrapPart.lab");

                    _csDoc.Variables["Cust"].Value = sn_string;
                    _csDoc.Variables["Desc"].Value = company;
                    _csDoc.Variables["Loc"].Value = date;                   


                    //btFormat = btApp.Formats.Open(AppDomain.CurrentDomain.BaseDirectory + "SN.btw", false, "");

                    //btFormat.SetNamedSubStringValue("SN", sn_string);
                    //btFormat.SetNamedSubStringValue("COMPANY", company);
                    //btFormat.SetNamedSubStringValue("DATE", date);

                    ////printer selected
                    //btFormat.Printer = InitprinterComboBox.Text;

                    ////total copies
                    //int CopiesOfLabel = Int32.Parse(this.PrintLabNum.Text.ToString());
                    //btFormat.IdenticalCopiesOfLabel = CopiesOfLabel;

                    //btFormat.PrintOut(false, false);

                    ////to save the label when exiting
                    //btFormat.Close(BarTender.BtSaveOptions.btSaveChanges);
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

        private void printLabelDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            var normalFont = new Font("Calibri", 14);

            var pageBounds = e.MarginBounds;
            var drawingPoint = new PointF(pageBounds.Left, (pageBounds.Top + normalFont.Height));

            graphics.DrawString("Name: Paul", normalFont, Brushes.Black, drawingPoint);

            drawingPoint.Y += normalFont.Height;

            graphics.DrawString("Bought: bike", normalFont, Brushes.Black, drawingPoint);

            e.HasMorePages = false; // No pages after this page.
        }
    }
}
