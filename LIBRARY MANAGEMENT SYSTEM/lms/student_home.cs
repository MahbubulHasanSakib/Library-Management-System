using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lms
{
    public partial class student_home : Form
    {
        public student_home()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            fines_std fn = new fines_std();
            fn.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();
        }

        private void student_home_Load(object sender, EventArgs e)
        {
            label3.Text = "WELCOME  " + (Program.NAME).ToUpper(); ;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            renew_books rb = new renew_books();
            rb.Show();
        }
    }
}
