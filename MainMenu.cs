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
        readonly ConnectionDB connectionDB = new ConnectionDB();
        string idUser;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");

            // to display menu based on user role
            CreateMenu();
        }

        private void CreateMenu()
        {
            string menu = "SELECT a.roleID, b.parentID, b.nodetext, b.toolStripMenu FROM tbl_userrole a, tbl_menu b " +
                "WHERE a.userid = '" + idUser + "' AND a.roleID = b.NodeID";
            using (MySqlDataAdapter adpt = new MySqlDataAdapter(menu, connectionDB.connection))
            {
                DataTable dt = new DataTable();
                adpt.Fill(dt);

                // cek jika ada selected menu 
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["roleID"].ToString() == "1")
                        {
                            packingListToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "2")
                        {
                            labelPartnumberToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "3")
                        {
                            scrapPartToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "4")
                        {
                            printLabelToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "5")
                        {
                            masterDataToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "6")
                        {
                            administrationToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "7")
                        {
                            scanToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "8")
                        {
                            dataToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "9")
                        {
                            custCodeControlToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "10")
                        {
                            modelMasterToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "11")
                        {
                            materialMasterToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "12")
                        {
                            materialMasterXMToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "13")
                        {
                            departmentToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "14")
                        {
                            userLevelToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "15")
                        {
                            userToolStripMenuItem.Visible = true;
                        }
                        if (dt.Rows[i]["roleID"].ToString() == "16")
                        {
                            userRoleToolStripMenuItem.Visible = true;
                        }
                    }
                }
            }
        }

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
            //PrintLabelNew printLabel = new PrintLabelNew();
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

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Departmentlist departmentlist = new Departmentlist();
            departmentlist.toolStripUsername.Text = toolStripUsername.Text;
            departmentlist.Show();
            this.Hide();
        }

        private void scrapPartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrapPartnumberList scrapPartnumber = new ScrapPartnumberList();
            scrapPartnumber.toolStripUsername.Text = toolStripUsername.Text;
            scrapPartnumber.Show();
            this.Hide();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            changePassword.usernameLbl.Text = toolStripUsername.Text;
            changePassword.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            if (MetroMessageBox.Show(this, message, title, buttons, icon) == DialogResult.Yes)
            {
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void materialMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterMaterial masterMaterial = new MasterMaterial();
            masterMaterial.toolStripUsername.Text = toolStripUsername.Text;
            masterMaterial.Show();
            this.Hide();
        }
    }
}
