using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lms
{
    public partial class Fines : Form
    {
        string id = "";
        int fines;
        DateTime due;
        int chargefine;
        int extra = 0;
        int extraday = 0;
        int m = 0;
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
       

        public Fines()
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
                MySqlCommand cmd = new MySqlCommand("select * from borrow_books where student_id='" + id + "'", msc);
                dataGridView1.Rows.Clear();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                string st_id = Program.SID;
                int book_ID;
                foreach (DataRow item in dt.Rows)
                {
                    fines = 0;
                    chargefine = 0;
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[1].ToString();
                    book_ID = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
                    dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();
                    DateTime DT;
                    DT = Convert.ToDateTime(item["borrow_date"]);
                    dataGridView1.Rows[n].Cells[2].Value = DT.ToString("dd-MM-yyyy");
                    DateTime bdt = Convert.ToDateTime(dataGridView1.Rows[n].Cells[2].Value.ToString());
                    due = Convert.ToDateTime(item["Due_date"]);
                    dataGridView1.Rows[n].Cells[3].Value = due.ToString("dd-MM-yyyy");
                    /*extraday = Convert.ToInt32((Program.renew_today - due).Days);
                    if (extraday > 0)
                    {
                        extra = extraday * 2;
                    }
                    else extra = 0;*/
                    DateTime today = DateTime.Today;
                    int daydiff = Convert.ToInt32((today - due).Days);
                    if(daydiff>0)
                    {
                        fines = daydiff * 2;
                    }
                    else
                    {
                        fines = 0;
                    }
                    MySqlCommand c = new MySqlCommand("select MAX(fines) from renew_books where student_id='" + id + "' and book_id='" + book_ID + "'", msc);
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
                            m = 0;
                        else
                            m = Convert.ToInt32(c.ExecuteScalar());

                    }
                    /*MySqlCommand cm = new MySqlCommand("select * from renew_books where student_id='" + id + "' and book_id='" + book_ID + "'", msc);
                    cm.ExecuteNonQuery();
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter md = new MySqlDataAdapter(cm);
                    md.Fill(dt1);
                    foreach (DataRow items in dt1.Rows)
                    {

                        chargefine = 0;
                        chargefine = Convert.ToInt32(items[5].ToString());
                        
                    }*/

                    dataGridView1.Rows[n].Cells[4].Value = (fines+m).ToString();
                    
                }
                msc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
