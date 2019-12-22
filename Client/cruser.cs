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
    public partial class cruser : Form
    {
        public cruser()
        {
            InitializeComponent();
            textBox6.Enabled = false;
            textBox7.Enabled = false;
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
                string result = client.search(textBox1.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                if (result == "no")
                {
                    client.addpeople(textBox1.Text, textBox2.Text, textBox3.Text, role, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                    client.addpeople_info(textBox1.Text, dateTimePicker1.Value, textBox4.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                    client.addpeople_info_two(textBox1.Text, textBox5.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
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
                    client.addpeople_post(textBox1.Text, textBox7.Text, textBox6.Text, role_bd, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                    MessageBox.Show("Пользователь успешно создан!");
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
