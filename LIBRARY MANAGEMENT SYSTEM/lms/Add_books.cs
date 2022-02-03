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
    public partial class Add_books : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public Add_books()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || richTextBox2.Text == "" || richTextBox3.Text == "" || richTextBox4.Text == "" || richTextBox6.Text == "" || richTextBox7.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Please,fill all fields");
            }
            else
            {
                try
                {
                    msc.Close();
                    msc.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into books(book_id,book_name,book_author,book_publication,purchase_date,price,quantity,stock_quantity)values('" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "','" + dateTimePicker1.Text + "','" + richTextBox6.Text + "','" + richTextBox7.Text + "','" + richTextBox7.Text + "')", msc);
                    int x = cmd.ExecuteNonQuery();

                    if (x > 0)
                    {
                        MessageBox.Show("The book is added");


                    }
                    msc.Close();
                }
                catch (MySqlException ex)
                {

                    int errorcode = ex.Number;
                    if (errorcode == 1062)
                        MessageBox.Show("The book is already added");
                    else
                        MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Home hm = new Home();
            hm.Show();
        }
    }
}
