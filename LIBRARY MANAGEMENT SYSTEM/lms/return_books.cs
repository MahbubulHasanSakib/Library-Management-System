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
    public partial class return_books : Form
    {
        string id = "";
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        int bk_id;
        string bk_name;
        DateTime br_date;
        DateTime due_date;
        DateTime due;
        int chargefine;
        int fines;
        int totalfine;
        int maxfine=0;
        int mm;
        int a;
        string sid;
      
        private MySqlCommand cmd1;

        public return_books()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id = textBox1.Text;
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from borrow_books where student_id='"+id+"'", msc);
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
             sid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            fines = 0;
            chargefine = 0;
            bk_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            br_date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[5].Value);
            due_date = (Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[6].Value));
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            int dd = Convert.ToInt32((today - due_date).Days);
            if (dd > 0)
            {
                fines = dd * 2;
            }
            else
            {
                fines = 0;

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string sid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            try
            {
                    msc.Close();
                    msc.Open();
               
                MySqlCommand c = new MySqlCommand("select MAX(fines) from renew_books where student_id='" + sid + "' and book_id='" + bk_id + "'", msc);
                /*if (c.ExecuteNonQuery() > 0)
                {
                    m = Convert.ToInt32(c.ExecuteScalar());
                }
                MessageBox.Show(m.ToString());*/
                c.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                MySqlDataAdapter md = new MySqlDataAdapter(c);
                md.Fill(dt1);

                foreach (DataRow items in dt1.Rows)
                {
                    if (string.IsNullOrEmpty(c.ExecuteScalar().ToString()))
                        a = 0;
                    else
                        a= Convert.ToInt32(c.ExecuteScalar());

                }
               
                /*MySqlCommand cmd = new MySqlCommand("update borrow_books set return_date='" + dateTimePicker1.Value.ToString() + "' where student_id='" + sid + "'", msc);
                cmd.ExecuteNonQuery();*/
                MySqlCommand cmd1 = new MySqlCommand("update books set stock_quantity=stock_quantity+1 where book_name='" + textBox2.Text + "'", msc);
                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("The book is returned");
                /* DataTable d = new DataTable();
                 MySqlDataAdapter m = new MySqlDataAdapter(cm);
                 md.Fill(dt1);
                 foreach (DataRow items in dt1.Rows)
                 {

                     chargefine = Convert.ToInt32(items[5].ToString());

                 }*/


                mm = a + fines;
                
                MySqlCommand cm = new MySqlCommand("insert into return_books(student_id,book_id,book_name,borrow_date,return_date,fines)values('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Text + "','" +mm+ "')", msc);
                    cm.ExecuteNonQuery();
                    MySqlCommand cmd3 = new MySqlCommand("delete from borrow_books where student_id='" + textBox4.Text + "' and book_id='" + textBox5.Text + "'", msc);
                    cmd3.ExecuteNonQuery();
                MySqlCommand cmdd = new MySqlCommand("delete from renew_books where student_id='" + textBox4.Text + "' and book_id='" + textBox5.Text + "'", msc);
                cmdd.ExecuteNonQuery();
                MySqlCommand cmd4 = new MySqlCommand("select * from borrow_books where student_id='" + textBox1.Text + "'", msc);
                    cmd4.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter mda = new MySqlDataAdapter(cmd4);
                    mda.Fill(dt);
                    dataGridView1.DataSource = dt;
      
                msc.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Home hm = new Home();
            hm.Show();
        }
    }
}