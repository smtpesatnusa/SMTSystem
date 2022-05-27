using MetroFramework;
using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using System.Drawing;
using System.Data;
using MySql.Data.MySqlClient;

namespace SMTPE
{
    public partial class MainMenu : MaterialForm
    {
        readonly Helper help = new Helper();
        readonly ConnectionDB connectionDB = new ConnectionDB();
        readonly string idUser;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////set centre and full screen
            //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

        }

        //private void CreateMenu()
        //{
        //    string menu = "SELECT a.roleID, b.parentID, b.nodetext FROM tbl_userrole a, tbl_menu b " +
        //        "WHERE a.userid = '" + idUser + "' AND a.roleID = b.NodeID";
        //    using (MySqlDataAdapter adpt = new MySqlDataAdapter(menu, connectionDB.connection))
        //    {
        //        DataTable dt = new DataTable();
        //        adpt.Fill(dt);

        //        // cek jika modelno tsb sudah di upload
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                if (dt.Rows[i]["roleID"].ToString() == "1")
        //                {
        //                    labelPrintToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "2")
        //                {
        //                    substoreToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "3")
        //                {
        //                    incomingFGToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "4")
        //                {
        //                    traceabilityToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "5")
        //                {
        //                    masterDataToolStripMenuItem1.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "6")
        //                {
        //                    administrationToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "7")
        //                {
        //                    ScanToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "8")
        //                {
        //                    dataToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "9")
        //                {
        //                    scanToolStripMenuItem1.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "10")
        //                {
        //                    dataToolStripMenuItem1.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "11")
        //                {
        //                    lineToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "12")
        //                {
        //                    stationToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "13")
        //                {
        //                    modelMasterToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "14")
        //                {
        //                    purchaseOrderToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "15")
        //                {
        //                    wOPTSNToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "16")
        //                {
        //                    userLevelToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "17")
        //                {
        //                    userMenuControlToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "18")
        //                {
        //                    userToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "19")
        //                {
        //                    userRoleToolStripMenuItem.Visible = true;
        //                }
        //                if (dt.Rows[i]["roleID"].ToString() == "20")
        //                {
        //                    changePasswordToolStripMenuItem.Visible = true;
        //                }
        //            }
        //        }
        //    }
        //}


        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            if (MetroMessageBox.Show(this, message, title, buttons, icon) == DialogResult.No)
                e.Cancel = true;
            else
                System.Windows.Forms.Application.ExitThread();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void modelMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modelmasterlist modelmasterlist = new Modelmasterlist();
            modelmasterlist.toolStripUsername.Text = toolStripUsername.Text;            
            modelmasterlist.Show();
            this.Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to logout?";
            string title = "Confirm Logout";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result = MetroMessageBox.Show(this, message, title, buttons, icon);
            //MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.Show();
            }
            else
            {
            }
        }

        private void packingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PackingList packingList = new PackingList();
            packingList.toolStripUsername.Text = toolStripUsername.Text;
            packingList.Show();
            this.Hide();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Userlist userlist = new Userlist();
            userlist.toolStripUsername.Text = toolStripUsername.Text;
            userlist.Show();
            this.Hide();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.usernameLbl.Text = toolStripUsername.Text;
            changePassword.ShowDialog();
        }

        private void scanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LabelPN labelPartnumber = new LabelPN();
            labelPartnumber.toolStripUsername.Text = toolStripUsername.Text;
            labelPartnumber.Show();
            this.Hide();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LabelPNList labelPNList = new LabelPNList();
            labelPNList.toolStripUsername.Text = toolStripUsername.Text;
            labelPNList.Show();
            this.Hide();
        }

        private void userLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserLevellist userLevellist = new UserLevellist();
            userLevellist.toolStripUsername.Text = toolStripUsername.Text;
            userLevellist.Show();
            this.Hide();
        }

        private void userRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserRole userRole = new UserRole();
            userRole.toolStripUsername.Text = toolStripUsername.Text;
            userRole.Show();
            this.Hide();
        }

        private void printLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintLabel printLabel = new PrintLabel();
            printLabel.ShowDialog();
        }

        private void custCodeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustControl custControl = new CustControl();
            custControl.toolStripUsername.Text = toolStripUsername.Text;
            custControl.Show();
            this.Hide();
        }

        private void materialMasterXMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterMaterialXM masterMaterialXM = new MasterMaterialXM();
            masterMaterialXM.toolStripUsername.Text = toolStripUsername.Text;
            masterMaterialXM.Show();
            this.Hide();
        }

        private void materialMasterToolStripMenuItem_Click_Click(object sender, EventArgs e)
        {
            MasterMaterial masterMaterial = new MasterMaterial();
            masterMaterial.toolStripUsername.Text = toolStripUsername.Text;
            masterMaterial.Show();
            this.Hide();
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Departmentlist departmentlist = new Departmentlist();
            departmentlist.toolStripUsername.Text = toolStripUsername.Text;
            departmentlist.Show();
            this.Hide();
        }

        private void scrapPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrapPartnumber scrapPartnumber = new ScrapPartnumber();
            scrapPartnumber.toolStripUsername.Text = toolStripUsername.Text;
            scrapPartnumber.Show();
            this.Hide();
        }
    }
}
