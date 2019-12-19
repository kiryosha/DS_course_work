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
    public partial class change_d : Form
    {
        public void update_d()
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.change_data(Settings.Default["user_id"].ToString());

            string username = dt.Rows[0].Field<string>("username");
            string email = dt.Rows[0].Field<string>("email");
            string fio = dt.Rows[0].Field<string>("fio");
            string address = dt.Rows[0].Field<string>("address");
            DateTime date_b = dt.Rows[0].Field<DateTime>("birthday_date");
            string phone_n = dt.Rows[0].Field<string>("phone_number");
            string card_n = dt.Rows[0].Field<string>("card_number");

            label1.Text = "Логин: " + username;
            label2.Text = "Почта: " + email;
            label3.Text = "ФИО: " + fio;
            label4.Text = "Адрес: " + address;
            label5.Text = "День рождения: " + date_b.ToShortDateString();
            label6.Text = "Номер телефона: " + phone_n;
            label7.Text = "Номер карты: " + card_n;
        }
        public change_d()
        {
            InitializeComponent();
            update_d();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_data(Settings.Default["user_id"].ToString(), "people", "username", textBox1.Text);
                MessageBox.Show("Логин изменен!");
                update_d();
                textBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_data(Settings.Default["user_id"].ToString(), "people", "email", textBox2.Text);
                MessageBox.Show("Почта изменена!");
                update_d();
                textBox2.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_data(Settings.Default["user_id"].ToString(), "people", "fio", textBox3.Text);
                MessageBox.Show("ФИО изменено!");
                update_d();
                textBox3.Text = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string b_date = dateTimePicker1.Value.ToShortDateString();
            string date_now = DateTime.Now.ToShortDateString();
            if (b_date == date_now)
            {
                MessageBox.Show("Дата не может быть сегодняшней!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_date(Settings.Default["user_id"].ToString(), dateTimePicker1.Value);
                MessageBox.Show("Дата дня рождения изменена!");
                update_d();
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_data(Settings.Default["user_id"].ToString(), "people_info", "phone_number", textBox5.Text);
                MessageBox.Show("Номер телефона изменен!");
                update_d();
                textBox5.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_data(Settings.Default["user_id"].ToString(), "people_info_two", "card_number", textBox6.Text);
                MessageBox.Show("Номер карты изменен!");
                update_d();
                textBox6.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
            }
            else
            {
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                client.update_data(Settings.Default["user_id"].ToString(), "people", "address", textBox4.Text);
                MessageBox.Show("Адрес был изменен!");
                update_d();
                textBox4.Text = "";
            }
        }
    }
}
