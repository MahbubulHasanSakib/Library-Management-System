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
    public partial class borrow_books : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        int stk_qntity;
        public borrow_books()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void borrow_books_Load(object sender, EventArgs e)
        {
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from books", msc);
                dataGridView1.Rows.Clear();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item[7].ToString();
                }
                msc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                
                richTextBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                richTextBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                richTextBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                richTextBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                richTextBox6.Text= dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                stk_qntity = Convert.ToInt32(richTextBox6.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stuId = richTextBox5.Text;
            int bookId = Convert.ToInt32(richTextBox1.Text);
            DateTime due_date;
            msc.Close();
            msc.Open();
            MySqlCommand cmd = new MySqlCommand("select student_id,book_id from borrow_books where student_id='" + stuId + "' and book_id='" + bookId + "'", msc);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Sorry,The book is already borrowed by you");
            }
            else
            {
                string id = richTextBox5.Text;
                MySqlCommand cmm = new MySqlCommand("select id from students where id='" + id + "'", msc);
                MySqlDataAdapter d = new MySqlDataAdapter(cmm);
                DataTable DT = new DataTable();
                d.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    if (stk_qntity <= 0)
                        MessageBox.Show("The book is out of stock");
                    else
                    {
                        try
                        {
                            msc.Close();
                            msc.Open();
                            DateTime bdt = Convert.ToDateTime(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                            due_date = Convert.ToDateTime((bdt.AddDays(15)).ToString("dd/MM/yyyy"));
                            MySqlCommand cm = new MySqlCommand("insert into borrow_books(student_id,book_id,book_name,writer,publication,borrow_date,due_date)values('" + richTextBox5.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "','" + dateTimePicker1.Text + "','" + due_date.ToShortDateString() + "')", msc);
                            int x = cm.ExecuteNonQuery();
                            MySqlCommand cmd1 = new MySqlCommand("update books set stock_quantity=stock_quantity-1 where book_name='" + richTextBox2.Text + "'", msc);
                            cmd1.ExecuteNonQuery();

                            if (x > 0)
                            {
                                MessageBox.Show("The book is borrowed");


                            }
                            msc.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                }

                else
                {
                    MessageBox.Show("The student is not registered");
                }
                msc.Close();
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
