using ClosedXML.Excel;
using MaterialSkin.Controls;
using MetroFramework;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class DetailPackingList : MaterialForm
    {
        LoadForm lf = new LoadForm();
        ConnectionDB connectionDB = new ConnectionDB();

        public DetailPackingList()
        {
            InitializeComponent();
        }

        //The below is the key for showing Progress bar
        private void StartProgress(String strStatusText)
        {
            LoadForm lf = new LoadForm();
            ShowProgress();
        }
        private void CloseProgress()
        {
            //Thread.Sleep(200);
            while (!this.IsHandleCreated)
                System.Threading.Thread.Sleep(200);
            lf.Invoke(new Action(lf.Close));
        }
        private void ShowProgress()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        lf.ShowDialog();
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Thread th = new Thread(ShowProgress);
                    th.IsBackground = false;
                    th.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            PackingList packingList = new PackingList();
            packingList.toolStripUsername.Text = toolStripUsername.Text;
            packingList.Show();
            this.Hide();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void LoadDataPackingList()
        {
            try
            {
                string query = "SELECT palletNo, projectmodel, soandline, poandline, partno, " +
                    "tbl_packingdetail.desc, model, qtyperctn, totalctn, totalqty, unit, cou," +
                    " netweight, grossweight, volume FROM tbl_packingdetail WHERE packingno = '" + tbPackingListNo.Text + "'";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewPlDetail.DataSource = dset.Tables[0];
                }

                totalLbl.Text = dataGridViewPlDetail.Rows.Count.ToString();
                connectionDB.connection.Close();
            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void PackingList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadDataPackingList();
        }

        private void PackingList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dataGridViewPlDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //memberi nomor row
            for (int i = 0; i < dataGridViewPlDetail.Rows.Count; ++i)
            {
                int row = i + 1;
                dataGridViewPlDetail.Rows[i].HeaderCell.Value = "" + row;
            }

            // not allow to sort table
            for (int i = 0; i < dataGridViewPlDetail.Columns.Count; i++)
            {
                dataGridViewPlDetail.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridViewPlDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
        }

        private void planBtn_Click(object sender, EventArgs e)
        {
            exportExcelPlan();
        }

        private void exportExcelPlan()
        {
            try
            {
                string directoryFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                directoryFile = directoryFile + "\\Inbound SMT\\" + tbPackingListNo.Text;
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Plan-" + tbPackingListNo.Text);
                    worksheet.Rows().Style.Font.FontName = "Calibri";
                    worksheet.Rows().Style.Font.FontSize = 11;

                    //to show gridlines
                    worksheet.ShowGridLines = true;

                    //set column width
                    worksheet.Column(1).Width = 12;
                    worksheet.Column(2).Width = 18;
                    worksheet.Column(3).Width = 23.78;
                    worksheet.Column(4).Width = 14.78;
                    worksheet.Column(5).Width = 18.89;
                    worksheet.Column(6).Width = 11.33;
                    worksheet.Column(7).Width = 11.33;
                    worksheet.Column(8).Width = 11.33;
                    worksheet.Column(9).Width = 9.89;
                    worksheet.Column(10).Width = 14.78;
                    worksheet.Column(11).Width = 15.22;
                    worksheet.Column(12).Width = 17;
                    worksheet.Column(13).Width = 10.44;
                    worksheet.Column(14).Width = 11;
                    worksheet.Column(15).Width = 17.11;
                    worksheet.Column(16).Width = 11.67;
                    worksheet.Column(17).Width = 8.44;
                    worksheet.Column(18).Width = 9.33;
                    worksheet.Column(19).Width = 10;
                    worksheet.Column(20).Width = 34;
                    worksheet.Column(21).Width = 14.44;
                    worksheet.Column(22).Width = 16.89;
                    worksheet.Column(23).Width = 27.33;

                    worksheet.Rows().Height = 14.4;
                    worksheet.Row(1).Height = 29.3;

                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 13)).Style.Fill.BackgroundColor = XLColor.Cyan;

                    worksheet.PageSetup.Margins.Top = 0.5;
                    worksheet.PageSetup.Margins.Bottom = 0.25;
                    worksheet.PageSetup.Margins.Left = 0.25;
                    worksheet.PageSetup.Margins.Right = 0;
                    worksheet.PageSetup.Margins.Header = 0.5;
                    worksheet.PageSetup.Margins.Footer = 0.25;
                    worksheet.PageSetup.CenterHorizontally = true;

                    worksheet.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Row(1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    worksheet.Cell(1, 1).Value = "Project Model";
                    worksheet.Cell(1, 2).Value = "SO NO.&Line";
                    worksheet.Cell(1, 3).Value = "Customer  PO# & Line";
                    worksheet.Cell(1, 4).Value = "MI Part NO.";
                    worksheet.Cell(1, 5).Value = "Description";
                    worksheet.Cell(1, 6).Value = "Q'ty/CTN";
                    worksheet.Cell(1, 7).Value = "Total CTNS";
                    worksheet.Cell(1, 8).Value = "Total Q'ty";
                    worksheet.Cell(1, 9).Value = "Country of origin ";
                    worksheet.Cell(1, 10).Value = "Unit";
                    worksheet.Cell(1, 11).Value = "Net Weight       (KGS)";
                    worksheet.Cell(1, 12).Value = "Gross Weight   (KGS)";
                    worksheet.Cell(1, 13).Value = "Volume (m³)";
                    worksheet.Cell(1, 15).Value = "PackPnglPst NO.：";
                    worksheet.Cell(1, 16).Value = "Invoice Date:";
                    worksheet.Cell(1, 18).Value = "Ship term:";
                    worksheet.Cell(1, 19).Value = "Incoterms:";
                    worksheet.Cell(1, 20).Value = "Payment Term: ";
                    worksheet.Cell(1, 21).Value = "Port of  Loading: ";
                    worksheet.Cell(1, 22).Value = "Port of Destination:";

                    int cellRowIndex = 2;
                    int cellColumnIndex = 1;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewPlDetail.Rows.Count; i++)
                    {                        
                        //from column 1-5
                        for (int j = 1; j < 6; j++)
                        {
                            worksheet.Cell(i + cellRowIndex, j).Value = "'"+dataGridViewPlDetail.Rows[i].Cells[j].Value.ToString();
                        }
                        //from column 7-14
                        for (int j = 7; j < 15; j++)
                        {

                            worksheet.Cell(i + cellRowIndex, j - 1).Value = dataGridViewPlDetail.Rows[i].Cells[j].Value.ToString();
                        }

                    }

                    for (int i = 0; i < dataGridViewPlDetail.Rows.Count; i++)
                    {
                        worksheet.Cell(cellRowIndex + i, 15).Value = tbPackingListNo.Text;
                        worksheet.Cell(cellRowIndex + i, 16).Value = tbInvoiceDate.Text;
                        worksheet.Cell(cellRowIndex + i, 18).Value = tbShipTerm.Text;
                        worksheet.Cell(cellRowIndex + i, 19).Value = tbIncoterms.Text;
                        worksheet.Cell(cellRowIndex + i, 20).Value = tbPaymentTerm.Text;
                        worksheet.Cell(cellRowIndex + i, 21).Value = tbPortLoading.Text;
                        worksheet.Cell(cellRowIndex + i, 22).Value = tbDestination.Text;
                    }

                    int endPart = dataGridViewPlDetail.Rows.Count + cellRowIndex;

                    workbook.SaveAs(directoryFile + "\\Plan-" + tbPackingListNo.Text + ".xlsx");
                }

                MessageBox.Show(this, "Excel File Success Generated", "Generate Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\Plan-" + tbPackingListNo.Text + ".xlsx");
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }

    }
}
