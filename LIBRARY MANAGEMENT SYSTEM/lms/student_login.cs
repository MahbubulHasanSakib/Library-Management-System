using MySql.Data.MySqlClient;
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
    public partial class student_login : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public student_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Please,fill all fields");
            }
            else
            {
                msc.Close();
                msc.Open();
                string id = richTextBox1.Text;
                Program.SID = id;
                string password= textBox1.Text;
                MySqlCommand cmd = new MySqlCommand("select id,name from students where id='" + id + "'", msc);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (password == id)
                    {
                        this.Hide();
                        Program.NAME = dt.Rows[0]["Name"].ToString();
                        student_home shm = new student_home();
                        shm.Show();
                       

                    }
                    else
                    {
                        MessageBox.Show("Invalid Login please check username and password");
                    }
                }
                else
                {
                    MessageBox.Show("The student is not registered");
                }
                msc.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();
        }
    }
}
