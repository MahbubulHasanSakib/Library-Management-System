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
    public partial class Add_students : Form
    {
        public static string connection = "SERVER=127.0.0.1;UID=root;PASSWORD=;DATABASE=lms";
        MySqlConnection msc = new MySqlConnection(connection);
        public Add_students()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "" || richTextBox3.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Please,fill all fields");
            }
            else
            {
                try
                {
                    msc.Close();
                    msc.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into students(id,name,date,department,photo)values('" + richTextBox1.Text + "','" + richTextBox3.Text + "','" + dateTimePicker1.Text + "','" + comboBox1.SelectedItem + "',@image)", msc);
                    cmd.Parameters.AddWithValue("@image",saveimage());
                    int x = cmd.ExecuteNonQuery();

                    if (x > 0)
                    {
                        MessageBox.Show("The student is added");


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

        private byte[] saveimage()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
            return ms.GetBuffer();

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(opf.FileName);
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
