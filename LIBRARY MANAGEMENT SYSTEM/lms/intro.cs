using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace lms
{
    public partial class intro : Form
    {
        public intro()
        {
            Thread t = new Thread(new ThreadStart(splashStart));
            t.Start();
            Thread.Sleep(4450);
            InitializeComponent();

            t.Abort();
        }
        public void splashStart()
        {
            Application.Run(new start_loading_page());
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            admin_login lg = new admin_login();
            lg.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            student_login sl = new student_login();
            sl.Show();
        }
    }
}
