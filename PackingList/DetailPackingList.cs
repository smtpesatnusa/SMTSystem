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

        string partnumber, materialSAP, model, prefix, suffix;

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
                    dataGridViewPlDetail.Columns.Add("columnMaterial", "material");
                    dataGridViewPlDetail.Columns.Add("columnMaterialStatus", "status");
                }

                //get material name by model and check in master material
                for (int i = 0; i < dataGridViewPlDetail.Rows.Count; i++)
                {
                    //get prefix and suffix
                    model = dataGridViewPlDetail.Rows[i].Cells[1].Value.ToString();
                    partnumber = dataGridViewPlDetail.Rows[i].Cells[4].Value.ToString();

                    string querymodel = "SELECT * FROM tbl_model WHERE model = '" + model + "'";
                    using (MySqlDataAdapter adptmodel = new MySqlDataAdapter(querymodel, connectionDB.connection))
                    {
                        DataTable dsetmodel = new DataTable();
                        adptmodel.Fill(dsetmodel);
                        if (dsetmodel.Rows.Count > 0)
                        {
                            prefix = dsetmodel.Rows[0]["code"].ToString();
                            suffix = dsetmodel.Rows[0]["taping"].ToString();
                        }
                        else
                        {
                            dataGridViewPlDetail.Rows[i].Cells[16].Value = "Model not Found in Master Model";
                        }
                    }

                    materialSAP = prefix + "" + partnumber + "" + suffix;
                    dataGridViewPlDetail.Rows[i].Cells[15].Value = materialSAP;

                    string querymaterial = "SELECT * FROM tbl_mastermaterial WHERE material = '"+materialSAP+"'";
                    using (MySqlDataAdapter adptmaterial = new MySqlDataAdapter(querymaterial, connectionDB.connection))
                    {
                        DataTable dsetmaterial = new DataTable();
                        adptmaterial.Fill(dsetmaterial);
                        if (dsetmaterial.Rows.Count > 0)
                        {
                            dataGridViewPlDetail.Rows[i].Cells[16].Value = "Material Ok";
                        }
                        else
                        {
                            dataGridViewPlDetail.Rows[i].Cells[16].Value = "Missing Material";
                        }
                    }
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


        private void LoadDataInbound()
        {
            try
            {
                string query = "SELECT projectModel, REPLACE(packingNo, 'P','I') AS uniqueId, 'PTSN', 'PTSN', 'S01', 'ZPCC', '', 'IDR', '1000000013', '', partno, ''," +
                    " '', 'SM11', 'R001', SUM(totalqty) AS total, 'PC', '0', '1000', '', 'X', SUBSTRING_INDEX(soandline, '/', 1) AS supplierInvoice," +
                    "SUBSTRING_INDEX(soandline, '/', -1) AS supplierInvoice, 'SI4B', REPLACE(packingNo, 'P', 'I') AS shipInvoice," +
                    "'','','','','','','','','','','', 'ID', '', '', tbl_packingdetail.desc, '', '' FROM tbl_packingdetail " +
                    "WHERE packingNo = '"+tbPackingListNo.Text+ "' GROUP BY partno ORDER BY id ";

                using (MySqlDataAdapter adpt = new MySqlDataAdapter(query, connectionDB.connection))
                {
                    DataSet dset = new DataSet();
                    adpt.Fill(dset);

                    dataGridViewInbound.DataSource = dset.Tables[0];
                }

                //get material name by model and check in master material
                for (int i = 0; i < dataGridViewInbound.Rows.Count; i++)
                {
                    //get prefix and suffix
                    model = dataGridViewInbound.Rows[i].Cells[0].Value.ToString();
                    partnumber = dataGridViewInbound.Rows[i].Cells[10].Value.ToString();
                    //set item no
                    int itemno = (i + 1) * 10;
                    dataGridViewInbound.Rows[i].Cells[9].Value = itemno.ToString();
                    //set invoiceDate
                    string invoiceDate = DateTime.ParseExact(tbInvoiceDate.Text, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    dataGridViewInbound.Rows[i].Cells[6].Value = invoiceDate.ToString();

                    //DateTime dateTimeETA = DateTime.Parse(invoiceDate);
                    //dataGridViewInbound.Rows[i].Cells[19].Value = dateTimeETA.AddDays(2);

                    string querymodel = "SELECT * FROM tbl_model WHERE model = '" + model + "'";
                    using (MySqlDataAdapter adptmodel = new MySqlDataAdapter(querymodel, connectionDB.connection))
                    {
                        DataTable dsetmodel = new DataTable();
                        adptmodel.Fill(dsetmodel);
                        if (dsetmodel.Rows.Count > 0)
                        {
                            prefix = dsetmodel.Rows[0]["code"].ToString();
                            suffix = dsetmodel.Rows[0]["taping"].ToString();
                        }
                        else
                        {
                            dataGridViewInbound.Rows[i].Cells[40].Value = "Model not Found in Master Model";
                        }
                    }

                    materialSAP = prefix + "" + partnumber + "" + suffix;
                    dataGridViewInbound.Rows[i].Cells[10].Value = materialSAP;

                    string querymaterial = "SELECT * FROM tbl_mastermaterial WHERE material = '" + materialSAP + "'";
                    using (MySqlDataAdapter adptmaterial = new MySqlDataAdapter(querymaterial, connectionDB.connection))
                    {
                        DataTable dsetmaterial = new DataTable();
                        adptmaterial.Fill(dsetmaterial);
                        if (dsetmaterial.Rows.Count > 0)
                        {
                            dataGridViewInbound.Rows[i].Cells[41].Value = "Material Ok";
                        }
                        else
                        {
                            dataGridViewInbound.Rows[i].Cells[41].Value = "Missing Material";
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


        private void PackingList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            LoadDataPackingList();
            LoadDataInbound();
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
                string filename =  tbPackingListNo.Text.Replace("P","") + " - Template ASN - Upload to SAP.xlsx";
                using (var workbook = new XLWorkbook())
                {
                    //worksheet SAP upload inbound
                    var worksheetInbound = workbook.Worksheets.Add("MW-031 Template Upload Inbound ");
                    worksheetInbound.Rows().Style.Font.FontName = "Calibri";
                    worksheetInbound.Rows().Style.Font.FontSize = 11;

                    //to show gridlines
                    worksheetInbound.ShowGridLines = true;

                    //set column width
                    worksheetInbound.Column(1).Width = 12;
                    worksheetInbound.Column(2).Width = 18;
                    worksheetInbound.Column(3).Width = 23.78;
                    worksheetInbound.Column(4).Width = 14.78;
                    worksheetInbound.Column(5).Width = 18.89;
                    worksheetInbound.Column(6).Width = 11.33;
                    worksheetInbound.Column(7).Width = 11.33;
                    worksheetInbound.Column(8).Width = 11.33;
                    worksheetInbound.Column(9).Width = 9.89;
                    worksheetInbound.Column(10).Width = 14.78;
                    worksheetInbound.Column(11).Width = 15.22;
                    worksheetInbound.Column(12).Width = 17;
                    worksheetInbound.Column(13).Width = 10.44;
                    worksheetInbound.Column(14).Width = 11;
                    worksheetInbound.Column(15).Width = 17.11;
                    worksheetInbound.Column(16).Width = 11.67;
                    worksheetInbound.Column(17).Width = 8.44;
                    worksheetInbound.Column(18).Width = 9.33;
                    worksheetInbound.Column(19).Width = 10;
                    worksheetInbound.Column(20).Width = 34;
                    worksheetInbound.Column(21).Width = 14.44;
                    worksheetInbound.Column(22).Width = 16.89;
                    worksheetInbound.Column(23).Width = 27.33;

                    worksheetInbound.Rows().Height = 14.4;
                    worksheetInbound.Row(2).Height = 110.1;

                    worksheetInbound.PageSetup.Margins.Top = 0.5;
                    worksheetInbound.PageSetup.Margins.Bottom = 0.25;
                    worksheetInbound.PageSetup.Margins.Left = 0.25;
                    worksheetInbound.PageSetup.Margins.Right = 0;
                    worksheetInbound.PageSetup.Margins.Header = 0.5;
                    worksheetInbound.PageSetup.Margins.Footer = 0.25;
                    worksheetInbound.PageSetup.CenterHorizontally = true;

                    worksheetInbound.Row(1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheetInbound.Row(1).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Set title row 1
                    string[] header = {"", "VERUR_LA",  "BUKRS", "EKORG", "EKGRP", "BSART", "EBDAT", "WAERS", "ELIFN", "EBELP", "MATNR40", "ZMATNR", "ZKUNNR",
                        "WERKS", "LGORT_D", "QTY_PO", "BSTME", "BAPICUREXT", "EPEIN", "LFDAT", "UMSON", "KDMAT", "LIFEXPOS", "ZSTYPE" , "ZSHPIV" , "ZBORNR" ,
                        "ZBORDT", "ZFORWD", "ZREMARKS", "ZNCV", "ZNCV_REASON", "PARVW_TO", "PRVW_TI", "PARVW_IO",   "PARVW_II", "UNREZ", "BORGR_GRP", "ZHAND", "ZLOCAL", "MAKTX", "NOREFERENCE" };
                    for (int i = 1; i < header.Length; i++)
                    {
                        worksheetInbound.Cell(1, i).Value = "" + header[i];
                    }

                    // set border 
                    worksheetInbound.Range(worksheetInbound.Cell(1, 1), worksheetInbound.Cell(5, 40)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheetInbound.Range(worksheetInbound.Cell(1, 1), worksheetInbound.Cell(5, 40)).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheetInbound.Range(worksheetInbound.Cell(1, 1), worksheetInbound.Cell(5, 40)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    // set color header with rgb row 1
                    worksheetInbound.Range(worksheetInbound.Cell(1, 1), worksheetInbound.Cell(1, 40)).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 32, 96);
                    //set font color
                    worksheetInbound.Range(worksheetInbound.Cell(1, 1), worksheetInbound.Cell(1, 40)).Style.Font.FontColor = XLColor.White;

                    // Set title row 3
                    string[] row2 = {"", "Unique ID \nReference no from Customer : ASN no/ Midoc no etc",   "Company Code\nFill PTSN", "Purch Org\nFill PTSN",
                        "Purchasing grup\nFill S01", "Document Type\nConsign: ZPCC Buy & Sell : ZPOB ", "Document Date\nReference date from Customer Doc", "Currency" ,
                        "Vendor\nBP Code ", "Item no\nIncrement of 10 ", "Material\n(SAP Material No) Can be blank if Colom K & L is filled. (SAP will mapping combination field K&L)",
                        "Customer Material\nIF Column D blank, mandatory to fill this column", "Customer\nIf Column D blank, mandatory to fill this column",    "Plant", "Storage Location",
                        "Quantity PO", "PO UoM", "Price\nOptional for Local Batam Max 2 decimal points", "Price Unit", "Delivery Date (ETA)", "Free Item\n- Fill X for ZPCC\n- For Buy and Sell fill X if Cust send free sample ",
                        "Supplier Invoice No\nMandatory for Xiaomi", "Ext Item no(Invoice item No)\nMandatory for Xiaomi", "Shipping Invoice Type\nSI1I: SHP - INV IMPORT\nSI2N: SHP - INV Non - BTM FTZ\nSI3N: SHP - INV Non - BTM Non - FTZ\nSI4B: SHP - INV BATAM",
                        "Shipping Invoice\nExternal Shipping Invoice No", "BL Original", "BL Original Date", "Forwarder BL Original\n(BP Code)", "Remarks", "NCV Ind\nFill X for NCV Item ",
                        "NCV Reason\nFill Reason if NCV Ind is X",  "Transporter on Customer\n(BP Code)",   "Transporter on PTSN\n(BP Code)",
                        "Insurance on Customer\n(BP Code)", "Insurance on PTSN\n(BP Code)", "Our Reference\n (For Roundtrip Indicator, Fill ROUNDTRIP for Inbound Roundtrip) ",
                        "Inbound Delivery Group\n(Local / Overseas ID)\nLocal: fill ID\nOverseas: blank ",  "Hand Carry\nPut X if material is Hand Carry",  "Local Vendor\nPut X if vendor is Local Vendor ",
                        "Description\nMandatory for ASN Import and NCV Item Non Stock ", "No Reference\nFill X if line item is non managed stock for NCV item . Line item with No Reference = X will be inputed only in Shipping Invoice Doc"};
                    for (int i = 1; i < row2.Length; i++)
                    {
                        worksheetInbound.Cell(2, i).Value = "" + row2[i];
                    }

                    //wrap text in row 2
                    worksheetInbound.Range(worksheetInbound.Cell(2, 1), worksheetInbound.Cell(2, 40)).Style.Alignment.WrapText = true;

                    // set color header with rgb row 2
                    worksheetInbound.Range(worksheetInbound.Cell(2, 1), worksheetInbound.Cell(2, 40)).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 226, 243);
                    //set align center
                    //worksheetInbound.Range(worksheetInbound.Cell(2, 1), worksheetInbound.Cell(2, 40)).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheetInbound.Range(worksheetInbound.Cell(2, 1), worksheetInbound.Cell(2, 40)).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Set title row 3
                    string[] row3 = {"", "Mandatory", "Mandatory", "Mandatory", "Mandatory" , "Mandatory", "Mandatory", "Mandatory", "Mandatory", "Mandatory",
                        "Conditional", "Conditional", "Conditional", "Mandatory", "Mandatory", "Mandatory", "Mandatory", "Conditional", "Optional", "Mandatory",
                        "Optional", "Optional", "Optional", "Mandatory", "Mandatory", "Optional", "Optional", "Optional", "Optional", "Optional", "Optional", "Optional",
                        "Optional", "Optional", "Optional", "Optional", "Optional", "Optional", "Optional", "Conditional", "Conditional" };
                    for (int i = 1; i < row3.Length; i++)
                    {
                        worksheetInbound.Cell(3, i).Value = "" + row3[i];
                    }

                    // set color header with rgb row 3
                    worksheetInbound.Range(worksheetInbound.Cell(3, 1), worksheetInbound.Cell(3, 40)).Style.Fill.BackgroundColor = XLColor.FromArgb(251, 228, 213);

                    // Set title row 4
                    string[] row4 = {"", "CHAR", "CHAR" , "CHAR", "CHAR", "CHAR", "DATS", "CUKY", "CHAR", "NUMC", "CHAR", "CHAR", "CHAR", "CHAR", "CHAR",
                        "BSTMG", "UNIT", "DEC", "DEC", "DATS", "CHAR", "CHAR", "NUMC", "CHAR", "CHAR", "CHAR", "DATE", "CHAR", "CHAR", "CHAR", "CHAR", "CHAR",
                        "CHAR", "CHAR", "CHAR", "CHAR", "CHAR", "CHAR", "CHAR", "CHAR", "CHAR" };
                    for (int i = 1; i < row4.Length; i++)
                    {
                        worksheetInbound.Cell(4, i).Value = "" + row4[i];
                    }

                    // Set title row 5
                    string[] row5 = {"", "35", "4", "4", "4", "4", "8", "5", "10", "5", "40", "80", "10", "4", "4", "13+3", "3", "", "5", "8", "1", "35", "6", "4",
                        "40", "35", "10", "59", "1", "50", "10", "10", "10", "10", "12", "35","", "1", "1", "40", "1" };
                    for (int i = 1; i < row5.Length; i++)
                    {
                        worksheetInbound.Cell(5, i).Value = "" + row5[i];
                    }

                    int cellRowIndexInbound = 6;
                    int cellColumnIndexInbound = 1;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewInbound.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridViewInbound.Columns.Count ; j++)
                        {
                            worksheetInbound.Cell(i + cellRowIndexInbound, j).Value = "'" + dataGridViewInbound.Rows[i].Cells[j].Value.ToString();
                        }
                    }

                    int endPartInbound = dataGridViewInbound.Rows.Count + cellRowIndexInbound;


                    // worksheet plan
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

                    // set border 
                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 22)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 23)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    // set color header with rgb
                    worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(1, 13)).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 226, 243);
                    worksheet.Range(worksheet.Cell(1, 15), worksheet.Cell(1, 22)).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 226, 243);

                    int cellRowIndex = 2;
                    int cellColumnIndex = 1;

                    // storing Each row and column value to excel sheet  
                    for (int i = 0; i < dataGridViewPlDetail.Rows.Count; i++)
                    {
                        //from column 1-5
                        for (int j = 1; j < 6; j++)
                        {
                            worksheet.Cell(i + cellRowIndex, j).Value = "'" + dataGridViewPlDetail.Rows[i].Cells[j].Value.ToString();
                        }
                        //from column 7-14
                        for (int j = 7; j < 15; j++)
                        {
                            string data = dataGridViewPlDetail.Rows[i].Cells[j].Value.ToString();
                            if (data == "0")
                            {
                                data = "";
                            }
                            worksheet.Cell(i + cellRowIndex, j - 1).Value = data;
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
                        worksheet.Cell(cellRowIndex + i, 23).Value = dataGridViewPlDetail.Rows[i].Cells[16].Value.ToString();

                    }

                    int endPart = dataGridViewPlDetail.Rows.Count + cellRowIndex;

                    workbook.SaveAs(directoryFile + "\\" + filename);
                }

                MessageBox.Show(this, "Excel File Success Generated", "Generate Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"" + directoryFile + "\\" + filename);
            }
            catch (Exception ex)
            {
                // tampilkan pesan error
                MessageBox.Show(ex.Message);
            }
        }
    }
}
