using System;
using System.Windows.Forms;

namespace SMTPE
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            //set full with taskbar below
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            pictureBox1.Image = SMTPE.Properties.Resources._4V0b;
        }
    }
}
