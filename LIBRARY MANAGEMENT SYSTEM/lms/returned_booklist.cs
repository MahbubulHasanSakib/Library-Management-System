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
    public partial class returned_booklist : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public returned_booklist()
        {
            InitializeComponent();
        }

        private void returned_booklist_Load(object sender, EventArgs e)
        {
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from return_books", msc);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                dataGridView1.DataSource = dt;
                msc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Home hm = new Home();
            hm.Show();
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from return_books where student_id like('%" + richTextBox1.Text + "%')", msc);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                dataGridView1.DataSource = dt;
                msc.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}
