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
using System.IO;
namespace lms
{
public partial class Students_Info : Form
    {
        string student_id = "";
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public Students_Info()
        {
            InitializeComponent();
            Student();
        }

        public bool Autogeneratecolumns { get; private set; }

        private void Student()
        {
            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from students", msc);
                dataGridView1.RowTemplate.Height = 80;
                dataGridView1.Rows.Clear();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                foreach(DataRow item in dt.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item[3].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[4];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            student_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string department = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            richTextBox1.Text = student_id;
            richTextBox2.Text = name;
            comboBox1.Text = department;
            MemoryStream ms = new MemoryStream((byte[])dataGridView1.SelectedRows[0].Cells[3].Value);
            Image im = Image.FromStream(ms);
            pictureBox1.Image = im;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            msc.Close();
            msc.Open();
            MySqlCommand cmd = new MySqlCommand("update students set id='"+ richTextBox1.Text + "', name='"+ richTextBox2.Text + "',department='"+comboBox1.Text+"',photo=@image where id='"+student_id+"'", msc);
            cmd.Parameters.AddWithValue("image",saveimage());
            cmd.ExecuteNonQuery();
            Student();
            msc.Close();
        }
        private byte[] saveimage()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Home hm = new Home();
            hm.Show();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

            try
            {
                msc.Close();
                msc.Open();
                MySqlCommand cmd = new MySqlCommand("select * from students where name like('%" + richTextBox3.Text + "%')", msc);
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
                    dataGridView1.Rows[n].Cells[2].Value = item[3].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item[4];
                }
                msc.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void richTextBox3_MouseClick(object sender, MouseEventArgs e)
        {
            richTextBox3.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlCommand cmdd = new MySqlCommand("delete from students where id='" + student_id + "'", msc);
            cmdd.ExecuteNonQuery();
            MySqlCommand cmd = new MySqlCommand("select * from students", msc);
            dataGridView1.RowTemplate.Height = 80;
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
                dataGridView1.Rows[n].Cells[2].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[4];
            }
        }
    }
    
}
