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
    public partial class change_password_admin : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public change_password_admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || richTextBox2.Text == "")
            {
                MessageBox.Show("Please,fill all fields");
            }
            else
            {
                msc.Close();
                msc.Open();
                string UserName = richTextBox1.Text;
                string pass = richTextBox2.Text;
                MySqlCommand cmd = new MySqlCommand("select Username from librarian where Username='" + UserName + "'", msc);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                   
                        MySqlCommand cmd1 = new MySqlCommand("update librarian set password='" +pass+ "' where username='"+UserName+"'", msc);
                        int a = cmd1.ExecuteNonQuery();
                        if (a > 0)
                        {
                            MessageBox.Show("Password is successfully changed");
                        }
                       
                    

                }
                else
                {
                    MessageBox.Show("The admin  is not registered");
                }
                msc.Close();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            admin_login al = new admin_login();
            al.Show();
        }
    }
}
