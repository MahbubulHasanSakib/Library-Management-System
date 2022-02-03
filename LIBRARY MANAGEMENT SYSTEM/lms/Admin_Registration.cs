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
    public partial class Admin_Registration : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public Admin_Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || richTextBox3.Text == "" || richTextBox3.Text == "" || richTextBox4.Text == "" || richTextBox5.Text == "")
            {
                MessageBox.Show("Please,fill all fields");
            }
            else
            {
                try
                {
                    msc.Close();
                    msc.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into librarian(name,username,password,phone,email)values('" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "','" + richTextBox5.Text + "')", msc);
                    int x = cmd.ExecuteNonQuery();

                    if (x > 0)
                    {
                        MessageBox.Show("Registration Completed");


                    }
                    msc.Close();
                }
                catch (MySqlException ex)
                {
                    int errorcode = ex.Number;
                    if (errorcode == 1062)
                        MessageBox.Show("The student is already added");
                    else
                        MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            admin_login al = new admin_login();
            al.Show();
        }
    }
}
