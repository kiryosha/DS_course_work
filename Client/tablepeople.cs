using Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class tablepeople : Form
    {
        public static int str = 1;
        public tablepeople()
        {
            InitializeComponent();
            button7.Visible = false;
            button9.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_people(1);
            dataGridView1.DataSource = dt;


            if (Settings.Default["role_bd"].ToString() == "staff")
            {
                textBox3.Visible = true;
                button1.Visible = false;
                button7.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button6.Visible = false;
                button8.Visible = false;
                string id = "";
                DataTable dt1 = client.vivod_provider(id);
                dataGridView1.DataSource = dt1;

                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            button8.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button7.Visible = true;
            button6.Visible = false;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string id = "";
            DataTable dt = client.vivod_buyer(id);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = true;
            textBox4.Visible = false;
            textBox5.Visible = false;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string id = "";
            DataTable dt = client.vivod_provider(id);
            dataGridView1.DataSource = dt;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button7.Visible = true;
            button6.Visible = false;
            button8.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button10.Visible = true;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox1.Visible = true;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_people(1);
            dataGridView1.DataSource = dt;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button7.Visible = false;
            button6.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = true;
            textBox5.Visible = false;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string id = "";
            DataTable dt = client.vivod_staff(id);
            dataGridView1.DataSource = dt;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button7.Visible = true;
            button6.Visible = false;
            button8.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button10.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = true;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string id = "";
            DataTable dt = client.vivod_admin(id);
            dataGridView1.DataSource = dt;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button7.Visible = true;
            button6.Visible = false;
            button8.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            if (textBox1.Visible != false)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Поле поиска не может быть пустым!");
                }
                else
                {
                    button10.Visible = false;
                    button9.Visible = false;
                    DataTable dt = client.search_fio(textBox1.Text);
                    dataGridView1.DataSource = dt;
                }
            }
            if (textBox2.Visible != false)
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Поле поиска не может быть пустым!");
                }
                else
                {
                    button10.Visible = false;
                    button9.Visible = false;
                    DataTable dt = client.vivod_buyer(textBox2.Text);
                    dataGridView1.DataSource = dt;
                }
            }
            if (textBox3.Visible != false)
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Поле поиска не может быть пустым!");
                }
                else
                {

                    button10.Visible = false;
                    button9.Visible = false;
                    DataTable dt = client.vivod_provider(textBox3.Text);
                    dataGridView1.DataSource = dt;
                }
            }
            if (textBox4.Visible != false)
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Поле поиска не может быть пустым!");
                }
                else
                {
                    button10.Visible = false;
                    button9.Visible = false;
                    DataTable dt = client.vivod_staff(textBox4.Text);
                    dataGridView1.DataSource = dt;
                }
            }
            if (textBox5.Visible != false)
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Поле поиска не может быть пустым!");
                }
                else
                {
                    button10.Visible = false;
                    button9.Visible = false;
                    DataTable dt = client.vivod_admin(textBox5.Text);
                    dataGridView1.DataSource = dt;
                }
            }
            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "")
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button7.Visible = true;
                button8.Visible = false;
                button6.Visible = false;
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            if (Settings.Default["role_bd"].ToString() == "staff")
            {
                button7.Visible = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            deluser u = new deluser();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            str++;
            if (str == 2)
            {
                button9.Visible = true;
            }
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_people(str);
            dataGridView1.DataSource = dt;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            str--;
            if(str == 1)
            {
                button9.Visible = false;
            }
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_people(str);
            dataGridView1.DataSource = dt;
        }
    }
}
