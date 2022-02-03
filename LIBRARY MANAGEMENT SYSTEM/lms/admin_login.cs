using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
namespace lms
{
    public partial class admin_login : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public admin_login()
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
                string UserName = richTextBox1.Text;
                string password = textBox1.Text;
                MySqlCommand cmd = new MySqlCommand("select Username,Password from librarian where Username='" + UserName + "'and password='" + password + "'", msc);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    this.Hide();
                    Home hm = new Home();
                    hm.Show();

                }
                else
                {
                    MessageBox.Show("Invalid Login please check username and password");
                }
                msc.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            change_password_admin cpa = new change_password_admin();
            cpa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Admin_Registration ar = new Admin_Registration();
            ar.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();
        }

        

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void richTextBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            intro i = new intro();
            i.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
