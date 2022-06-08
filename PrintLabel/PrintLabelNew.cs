using BarcodeLib;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DateTime = System.DateTime;

namespace SMTPE
{
    public partial class PrintLabelNew : MaterialForm
    {
        Helper help = new Helper();
        private int actualPage = 1;

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

            //label size
            printLabelDocument.DefaultPageSettings.PaperSize = new PaperSize("Label", 380, 370); // all sizes are converted from mm to inches & then multiplied by 100.

            PrintLabNum.Text = "1";
            DateTextBox.Text = dateTimeNow;
            
            // update printer list
            help.UpdatePrinterList(InitprinterComboBox);
            // select top printer
            InitprinterComboBox.SelectedIndex = 0;

            BarcodeTextBox.Select();
        }
        
        private void PrintButton_Click(object sender, EventArgs e)
        {
            //printLabel();
            actualPage = 1;
            printLabelPreviewDialog.Document = printLabelDocument;
            printLabelPreviewDialog.ShowDialog();
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

        private void printLabelDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var graphics = e.Graphics;
            Barcode barcode = new Barcode();
            Image barcodeImage;
            Pen pen = new Pen(Color.Black);
            var normalFont = new Font("Consolas", 14);

            var pageBounds = e.MarginBounds;
            var drawingPoint = new PointF(pageBounds.Left, 20);
            barcodeImage = barcode.Encode(TYPE.CODE128, "1");

            graphics.DrawImage(barcodeImage, drawingPoint);
            barcodeImage.Dispose();
            drawingPoint.Y += barcode.Height;
            graphics.DrawString("Name: Paul 1234567890", normalFont, Brushes.Black, drawingPoint);

            e.HasMorePages = false; // No pages after this page.

            //Barcode barcode = new Barcode();
            //Image barcodeImage;
            //Pen pen = new Pen(Color.Black);
            //Font fontSerial = new Font("Consolas", 12);

            //e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            //barcode.Alignment = AlignmentPositions.LEFT;
            //barcode.Height = 37;
            //barcode.Width = 100;
            //barcode.IncludeLabel = false;
            //barcode.LabelFont = fontSerial;
            //barcode.LabelPosition = LabelPositions.BOTTOMCENTER;

            //var graphics = e.Graphics;
            //var normalFont = new Font("Consolas", 12);

            //var pageBounds = e.MarginBounds;
            //var drawingPoint = new PointF(pageBounds.Left, (pageBounds.Top + normalFont.Height));

            //barcodeImage = barcode.Encode(TYPE.CODE128, "27");
            //graphics.DrawImage(barcodeImage, new PointF(5 + 3.0F, 5 + 1.0F));
            //barcodeImage.Dispose();
            //drawingPoint.Y += normalFont.Height;

            //e.HasMorePages = false; // No pages after this page.

            ////graphics.DrawString("Bought: bike", normalFont, Brushes.Black, drawingPoint);

            ////e.HasMorePages = false; // No pages after this page.


            ////graphics.DrawString("Name: Paul", normalFont, Brushes.Black, drawingPoint);

            ////drawingPoint.Y += normalFont.Height;

            ////graphics.DrawString("Bought: bike", normalFont, Brushes.Black, drawingPoint);

            ////e.HasMorePages = false; // No pages after this page.
        }
    }
}
