using MetroFramework;
using System;
using System.Windows.Forms;
using MaterialSkin.Controls;
using System.Drawing;

namespace SMTPE
{
    public partial class MainMenu : MaterialForm
    {
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

        private void materialMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MasterMaterial masterMaterial = new MasterMaterial();
            masterMaterial.toolStripUsername.Text = toolStripUsername.Text;
            masterMaterial.Show();
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
    }
}
