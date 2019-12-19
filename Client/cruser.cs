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
    public partial class cruser : Form
    {
        public cruser()
        {
            InitializeComponent();
            textBox6.Enabled = false;
            textBox7.Enabled = false;

            if(login.reg_u == "yes")
            {
                radioButton4.Checked = true;

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool proverka = false;
            if (!radioButton1.Checked)
            {
                if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    proverka = true;
                }
                if(!radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked)
                {
                    proverka = true;
                }
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
                {
                    proverka = true;
                }
            }
            if(proverka == false)
            {
                string role = "user";
                string role_bd = "";
                if (radioButton2.Checked)
                {
                    role = "admin";
                }
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                string result = client.search(textBox1.Text);
                if (result == "no")
                {
                    client.addpeople(textBox1.Text, textBox2.Text, textBox3.Text, role);
                    client.addpeople_info(textBox1.Text, dateTimePicker1.Value, textBox4.Text);
                    client.addpeople_info_two(textBox1.Text, textBox5.Text);
                    if (radioButton1.Checked)
                    {
                        role_bd = "staff";

                    }
                    if (radioButton3.Checked)
                    {
                        role_bd = "provider";
                    }
                    if (radioButton4.Checked)
                    {
                        role_bd = "buyer";
                    }
                    client.addpeople_post(textBox1.Text, textBox7.Text, textBox6.Text, role_bd);
                    MessageBox.Show("Пользователь успешно создан!");
                    if (login.reg_u == "yes")
                    {
                        MessageBox.Show("Задан стандартный пароль qwerty1 (После авторизации нужно сменить его в лк)!");
                        this.Close();
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox7.Text = "";
                    textBox6.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
                else
                {
                    MessageBox.Show("Пользователь существует!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox7.Text = "";
                    textBox6.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("Все данные должны быть заполнены!");
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            if (!radioButton1.Checked)
            {
                textBox6.Enabled = false;
                textBox7.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
