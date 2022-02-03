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

    public partial class renew_books : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        int bk_id;
        string bk_name;
        DateTime br_date;
        DateTime due_date;
        DateTime due;
        int fines=0;
        int chargefine=0;
        int totalfine;
        int maxfine;
        DateTime today;
        int m=0;
        int flag;
        public renew_books()
        {
            InitializeComponent();
        }

       /* private void dataGridView1_Click(object sender, EventArgs e)
        {
            

                bk_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            due_date = (Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value));
            
        }*/

        private void renew_books_Load(object sender, EventArgs e)
        {
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from borrow_books where student_id='" + Program.SID + "'", msc);
                dataGridView1.Rows.Clear();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[1].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();
                    DataReader dr = new DataReader();
                    DateTime DT;
                    DT = Convert.ToDateTime(item["borrow_date"]);
                    dataGridView1.Rows[n].Cells[2].Value = DT.ToString("dd-MM-yyyy");
                    DateTime bdt = Convert.ToDateTime(dataGridView1.Rows[n].Cells[2].Value.ToString());
                    DateTime now = DateTime.Today;
                    due = Convert.ToDateTime(item["Due_date"]);
                    dataGridView1.Rows[n].Cells[3].Value = due.ToString("dd-MM-yyyy");
                  
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

            fines = 0;
            chargefine = 0;
            bk_id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            bk_name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            br_date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value);
            due_date = (Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value));
             today = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            int dd = Convert.ToInt32((today- due_date).Days);
            if (dd > 0)
            {
                fines = dd * 2;
            }
            else
            {
                fines = 0;

            }
            

        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand c = new MySqlCommand("select MAX(fines) from renew_books where student_id='" + Program.SID + "' and book_id='" + bk_id + "'", msc);
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
                    {
                        m = 0;
                        flag = 0;

                    }
                    else
                    {
                        m = Convert.ToInt32(c.ExecuteScalar());
                        flag = 1;
                    }

                    }

                if (flag == 0)
                {
                    Program.renew_today =Convert.ToDateTime( DateTime.Today.ToString("dd/MM/yyyy"));
                    DateTime new_renew = Convert.ToDateTime((due_date.AddDays(15)).ToString("dd/MM/yyyy"));
                    if (Program.renew_today > (new_renew))
                    {
                        chargefine =Convert.ToInt32((((Program.renew_today) - (new_renew)).TotalDays)*2);
                    }
                    else chargefine = 0;
          
                    MySqlCommand cm = new MySqlCommand("insert into renew_books(student_id,book_id,book_name,borrow_date,last_due_date,fines,new_due_date,renew_date)values('" + Program.SID + "','" + bk_id + "','" + bk_name + "','" + br_date.ToString("dd/MM/yyyy") + "','" + due_date.ToString("dd/MM/yyyy") + "','" + (fines-chargefine) + "','" + (due_date.AddDays(15)).ToString("dd/MM/yyyy") + "','" + DateTime.Now.ToString("dd/MM/yyyy") + "')", msc);
                    int x = cm.ExecuteNonQuery();

                    if (x > 0)
                    {



                    }
                    MySqlCommand cmd = new MySqlCommand("update borrow_books set due_date='" + (due_date.AddDays(15)).ToString("dd/MM/yyyy") + "' where student_id='" + Program.SID + "' and book_id='" + bk_id + "'", msc);
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        MessageBox.Show("Renew Successfull");

                    }
                    MySqlCommand cmd1 = new MySqlCommand("select * from borrow_books where student_id='" + Program.SID + "'", msc);
                    dataGridView1.Rows.Clear();
                    cmd1.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter mda = new MySqlDataAdapter(cmd1);
                    mda.Fill(dt);
                    foreach (DataRow item in dt.Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item[1].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();
                        //DataReader dr = new DataReader();
                        DateTime DT;
                        DT = Convert.ToDateTime(item["borrow_date"]);
                        dataGridView1.Rows[n].Cells[2].Value = DT.ToString("dd-MM-yyyy");
                        DateTime bdt = Convert.ToDateTime(dataGridView1.Rows[n].Cells[2].Value.ToString());
                        DateTime now = DateTime.Today;
                        due = Convert.ToDateTime(item["Due_date"]);
                        dataGridView1.Rows[n].Cells[3].Value = due.ToString("dd-MM-yyyy");

                    }
                }
                else if(flag==1)
                {
                    MessageBox.Show("Not renewable.It has renewed once time.Please return the book");
                }
                
                msc.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            student_home sh = new student_home();
            sh.Show();
        }
    }
}
