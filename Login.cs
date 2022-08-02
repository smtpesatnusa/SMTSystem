using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Drawing;
using MaterialSkin;

namespace SMTPE
{
    public partial class Login : MaterialForm
    {
        readonly ConnectionDB con = new ConnectionDB();
        readonly Helper help = new Helper();
        string id, username, password, name, role;

        public Login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
        private void Login_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //icon
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            dateTimeNow.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginVerify();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            loginVerify();   
        }

        private void loginVerify()
        {

            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "")
                {
                    string pass = help.encryption(txtPassword.Text);

                    con.Open();
                    string query = "SELECT id,username,pass,NAME,ROLE FROM tbl_user WHERE username = '" + txtUsername.Text + "' AND pass = '" + pass + "'";
                    MySqlDataReader row;
                    row = con.ExecuteReader(query);
                    if (row.HasRows)
                    {
                        while (row.Read())
                        {
                            id = row["id"].ToString();
                            username = row["username"].ToString();
                            password = row["pass"].ToString();
                            name = row["name"].ToString();
                            role = row["role"].ToString();
                        }
                        MainMenu mm = new MainMenu();
                        mm.toolStripUsername.Text = "Welcome " + name + " " + username + ", " + role + " |";
                        mm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Data not found", "Information");
                        txtPassword.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Fill Username and Password field to Login", "Information");
                }
            }
            catch
            {
                MessageBox.Show("Connection Error", "Information");
            }
        }


        
    }
}