using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace lms
{
    public partial class Home : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public Home()
        {
            InitializeComponent();
        }

        private void addNewBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Add_books ab = new Add_books();
            ab.Show();
        }

        private void allBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            All_books ab = new All_books();
            ab.Show();
        }

        private void addStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Add_students ad = new Add_students();
            ad.Show();
        }

        private void studentsInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Students_Info si = new Students_Info();
            si.Show();
        }

        private void borrowBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            borrow_books bb = new borrow_books();
            bb.Show();
        }

        private void returnBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return_books rb = new return_books();
            rb.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void finesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fines fn = new Fines();
            fn.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            Add_books adb = new Add_books();
            adb.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            All_books ab = new All_books();
            ab.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            borrow_books bb = new borrow_books();
            bb.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            return_books rb = new return_books();
            rb.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Hide();
            Add_students AS= new Add_students();
            AS.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Hide();
            Students_Info si = new Students_Info();
            si.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Hide();
            Fines fn = new Fines();
            fn.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            borrowed_booklist bbk = new borrowed_booklist();
            bbk.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Hide();
            returned_booklist rbk = new returned_booklist();
            rbk.Show();
        }
    }
}
