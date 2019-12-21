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
    public partial class changepass : Form
    {
        public changepass()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Пароль не может быть пустым!");
            }
            else
            {
                if (textBox2.Text.Length > 16 && textBox1.Text.Length > 16)
                {
                    MessageBox.Show("Длина пароля не может быть больше 16 символов!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                    string result = client.change_pass(textBox2.Text, textBox1.Text, Settings.Default["user_id"].ToString(), Settings.Default["token"].ToString());
                    if (result == "no")
                    {
                        MessageBox.Show("Старый пароль введен неверно!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Пароль изменен!");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
        }
    }
}
