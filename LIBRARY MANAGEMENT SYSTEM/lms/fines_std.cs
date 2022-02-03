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
    public partial class fines_std : Form
    {
        DateTime due;
        string id = "";
        int fines=0;
        int chargefine;
        int extraday=0;
        int extra = 0;
        int previousfine;
        int m = 0;
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        private string today;
        public fines_std()
        {
            InitializeComponent();
        }

        private void fines_std_Load(object sender, EventArgs e)
        {
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from borrow_books where student_id='" +Program.SID+ "'", msc);
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
                    m = 0;
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[1].ToString();
                    book_ID = Convert.ToInt32(dataGridView1.Rows[n].Cells[0].Value);
                    dataGridView1.Rows[n].Cells[1].Value = item[2].ToString();
                    DataReader dr = new DataReader();
                    DateTime DT;
                    DT = Convert.ToDateTime(item["borrow_date"]);
                    dataGridView1.Rows[n].Cells[2].Value = DT.ToString("dd-MM-yyyy");
                    DateTime bdt = Convert.ToDateTime(dataGridView1.Rows[n].Cells[2].Value.ToString());
                    DateTime now = DateTime.Today;
                    due = Convert.ToDateTime(item["Due_date"]);
                    dataGridView1.Rows[n].Cells[3].Value = due.ToString("dd-MM-yyyy");
                    DateTime today = Convert.ToDateTime(now.ToString("dd-MM-yyyy"));
                
                        int dd = Convert.ToInt32((today - due).Days);
                        if (dd > 0)
                        {
                            fines = dd * 2;
                        }
                        else
                        {
                            fines = 0;
                        }
                       
                    MySqlCommand c = new MySqlCommand("select MAX(fines) from renew_books where student_id='" + Program.SID + "' and book_id='" + book_ID + "'", msc);
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
                    //MessageBox.Show(m.ToString());
                    /*MySqlCommand cm = new MySqlCommand("select * from renew_books where student_id='" + Program.SID + "' and book_id='" + book_ID+ "'", msc);
                     cm.ExecuteNonQuery();
                     DataTable d= new DataTable();
                     MySqlDataAdapter msd = new MySqlDataAdapter(cm);
                     md.Fill(d);
                     foreach (DataRow items in d.Rows)
                     {
                         //chargefine = 0;
                         //chargefine = Convert.ToInt32(items[5].ToString());

                     }*/
                    /* using (MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms"))
                     {
                         MySqlCommand c = new MySqlCommand("SELECT MAX(fines) from renew_books where student_id='" + Program.SID + "' and book_id='" + book_ID + "'", msc);
                         con.Open();
                         if (c.ExecuteNonQuery() > 0)
                         {
                             previousfine = Convert.ToInt32(c.ExecuteScalar());
                         }
                         con.Close();
                         MessageBox.Show(previousfine.ToString());
                     }*/
                    dataGridView1.Rows[n].Cells[4].Value =(m+fines).ToString();
                    fines = 0;
                }
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
            student_home sh = new student_home();
            sh.Show();
        }
    }
}
