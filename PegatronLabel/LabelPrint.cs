using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class LabelPrint : MaterialForm
    {     

        LoadForm lf = new LoadForm();
        ConnectionDB connectionDB = new ConnectionDB();
        Helper help = new Helper();

        string idUser, dept, username, badgeId;

        public LabelPrint()
        {
            InitializeComponent();
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.toolStripUsername.Text = toolStripUsername.Text;
            mm.Show();
            this.Hide();
        }

        private void LabelPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            string message = "Are you sure you want to close this application?";
            string title = "Confirm Close";
            MaterialDialog materialDialog = new MaterialDialog(this, title, message, "OK", true, "Cancel");
            DialogResult result = materialDialog.ShowDialog(this);
            if (result.ToString() == "OK")
            {
                System.Windows.Forms.Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
                MaterialSnackBar SnackBarMessage = new MaterialSnackBar(result.ToString(), 750);
                SnackBarMessage.Show(this);
            }
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (cmbType.Text == "Single Print")
            {
                AddLabel addLabel = new AddLabel();
                addLabel.userdetail.Text = badgeId;
                addLabel.ShowDialog();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void PegatronLabelList_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
            //icon
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //get user id
            var user = toolStripUsername.Text.Split(',');
            idUser = user[0].Trim();
            var badge = idUser.Split(' ');
            int badgePosition = (badge.Length) - 1;

            // get badge ID employee and username
            badgeId = badge[badgePosition].Trim();
            username = idUser.Replace(" " + badgeId, "").Replace("Welcome ", "");

            cmbType.SelectedIndex = -1;
        }
    }
}
