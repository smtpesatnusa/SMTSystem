using MaterialSkin.Controls;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class menuControl : MaterialForm
    {
        Helper help = new Helper();
        ConnectionDB connectionDB = new ConnectionDB();
        DataTable dtSource = null;

        string idUser;

        public menuControl()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            help.dateTimeNow(dateTimeNow);
        }

        private void Modelmasterlist_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Userlist_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var userId = toolStripUsername.Text.Split(' ');
            int idPosition = toolStripUsername.Text.Split(' ').Length - 3;
            idUser = userId[idPosition].Replace(",", "");            

            // display data db to treeview
            dtSource = help.GetData("SELECT NodeID,ParentID,NodeText FROM tbl_menu");
            DataTable dt = help.GetChildData(dtSource, -1);
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode parentNode = new TreeNode();
                parentNode.Text = dr["NodeText"].ToString();
                parentNode.Name = dr["NodeID"].ToString();
                help.AddNodes(ref parentNode, dtSource);
                treeViewMenu.Nodes.Add(parentNode);
            }
            
            // expand all child in treeview
            treeViewMenu.ExpandAll();


            // fill listbox with user data
            string sqluserlist = "SELECT CONCAT(username, ' | ', NAME) AS NAMES FROM tbl_user ORDER BY name";
            help.fill_listbox(sqluserlist, listBoxUser, "names");
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void treeViewMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    help.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
            help.SelectParents(e.Node, e.Node.Checked);
        }


        private void updateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var badge = listBoxUser.Text.Trim().Split('|');
                string badgeId = badge[0];

                //var cmd = new MySqlCommand("", connectionDB.connection);
                //connectionDB.connection.Open();
                //string queryUpdateRole = "INSERT INTO tbl_userrole (userId, roleid) VALUES " +
                //    "('" + listBoxUser.SelectedItem + "', '1')";

                //string[] allQuery = { queryUpdateRole };
                //for (int j = 0; j < allQuery.Length; j++)
                //{
                //    cmd.CommandText = allQuery[j];
                //    //Masukkan perintah/query yang akan dijalankan ke dalam CommandText
                //    cmd.ExecuteNonQuery();
                //    //Jalankan perintah / query dalam CommandText pada database
                //}
                //connectionDB.connection.Close();
                //MessageBox.Show(this, "User Role for " + listBoxUser.Text + " Successfully Updated", "Update User Role", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(this, "User Role for " + listBoxUser.Text + " Successfully Updated", "Update User Role", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                connectionDB.connection.Close();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }
    }
}
