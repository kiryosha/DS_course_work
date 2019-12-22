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
    public partial class reg_buyer : Form
    {
        public reg_buyer()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if(textBox2.Text != "")
                {
                    if(textBox3.Text != "")
                    {
                        if(textBox4.Text != "")
                        {
                            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                            string resilt = client.add_buyer(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                            if(resilt == "no")
                            {
                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox3.Text = "";
                                textBox4.Text = "";
                                MessageBox.Show("Юзер успешно создан!");
                                this.Close();
                            }
                            else
                            {
                                textBox1.Text = "";
                                MessageBox.Show("Юзер c таким логином существует!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Адрес не может быть пустым!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Имя не может быть пустым!");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль не может быть пустым!");
                }
            }
            else
            {
                MessageBox.Show("Логин не может быть пустым!");
            }
        }
    }
}
