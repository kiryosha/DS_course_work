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
    public partial class tovar_table : Form
    {
        public void vivod()
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_tovar(Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            dataGridView1.DataSource = dt;

        }

        public void vivod_sklad()
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_sklad(Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            dataGridView3.DataSource = dt;
        }

        public void vivod_ing()
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_ing(Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            dataGridView2.DataSource = dt;
        }

        public void Button_Lable_false()
        {

            vivod();
            vivod_ing();
            vivod_sklad();
            button20.Visible = false;
            button18.Visible = false;
            button15.Visible = false;
            label10.Visible = false;
            textBox10.Visible = false;
            button14.Visible = false;
            button6.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button10.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            label9.Visible = false;
            textBox9.Visible = false;
            button12.Visible = false;
            button11.Visible = false;
            button19.Visible = false;
            button22.Visible = false;

        }
        public tovar_table()
        {
            InitializeComponent();
            Button_Lable_false();
            if (Settings.Default["role"].ToString() != "admin")
            {
                //tabPage5.Parent = null;
                tabControl1.TabPages.Remove(tabPage5);
                tabPage6.Parent = null;
            }
            if (Settings.Default["role_bd"].ToString() == "buyer")
            {
                tabPage2.Parent = null;
                tabPage4.Parent = null;
                button14.Enabled = false;
                button13.Enabled = false;
                button6.Enabled = false;
                button5.Enabled = false;
                button2.Enabled = false;
                button13.Enabled = false;

            }

            if (Settings.Default["role_bd"].ToString() == "provider")
            {
                button7.Enabled = false;
                button14.Enabled = false;
                button13.Enabled = false;
                button6.Enabled = false;
                button5.Enabled = false;
                button2.Enabled = false;
                button13.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            label1.Visible = true;
            textBox1.Visible = true;
            button13.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button3.Visible = false;
            button5.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            button13.Visible = false;
            button19.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            bool IsDigit2 = textBox2.Text.All(char.IsDigit);
            bool IsDigit3 = textBox3.Text.All(char.IsDigit);
            bool IsDigit5 = textBox3.Text.All(char.IsDigit);
            string res = client.search_sklad(textBox4.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else
            {
                if(IsDigit2 == true && IsDigit3 == true && IsDigit5 == true)
                {
                    if(res == "yes")
                    {
                        tabControl1.TabPages.Remove(tabPage5);
                        button13.Visible = true;
                        button3.Visible = true;
                        button2.Visible = true;
                        button16.Visible = false;
                        button5.Visible = false;
                        Button_Lable_false();

                        string result = client.search_tovar(textBox1.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                        if (result == "yes")
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox4.Text = "";
                            textBox3.Text = "";
                            textBox5.Text = "";
                            MessageBox.Show("Товар уже существует!");
                        }
                        else
                        {
                            client.search_add_tovar(textBox1.Text, textBox2.Text, textBox3.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                            client.search_add_tovar_on_sklad(textBox1.Text, textBox4.Text, textBox5.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox4.Text = "";
                            textBox3.Text = "";
                            textBox5.Text = "";
                            tabPage5.Parent = null;
                            MessageBox.Show("Товар создан!");
                        }
                        DataTable dt = client.vivod_tovar(Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Такого склада не существует!");
                        textBox4.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Введите цифры!");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            button13.Visible = false;
            button4.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            Button_Lable_false();
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.searc_tovar_name(textBox1.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            dataGridView1.DataSource = dt;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            button13.Visible = true;
            vivod();
            button2.Visible = true;
            button3.Visible = true;
            button6.Visible = false;
            textBox1.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button21.Visible = false;
            button8.Visible = false;
            button11.Visible = true;
            button7.Visible = false;
            label6.Visible = true;
            textBox6.Visible = true;
            button10.Visible = false;
            button9.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button21.Visible = false;
            button20.Visible = true;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button12.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.vivod_tovar_bez(Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            tabControl1.SelectedTab = tabPage2;
            dataGridView2.DataSource = dt;
            button9.Visible = false;
            button10.Visible = true;
            button8.Visible = false;
            button7.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button21.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            vivod_ing();
            tabControl1.SelectedTab = tabPage2;
            button10.Visible = false;
            button8.Visible = true;
            button7.Visible = true;
            button9.Visible = true;
            button21.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string result = client.search_tovar(textBox9.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            string res = client.search_ing_provider(textBox7.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            bool IsDigit8 = textBox8.Text.All(char.IsDigit);
            if (textBox9.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else
            {
                if(result == "yes")
                {
                    if(res == "yes")
                    {
                        if(IsDigit8 == true)
                        {
                            label6.Visible = false;
                            label7.Visible = false;
                            label8.Visible = false;
                            label9.Visible = false;
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                            button7.Visible = true;
                            button8.Visible = true;
                            button9.Visible = true;
                            button12.Visible = false;
                            button20.Visible = false;
                            button21.Visible = true;
                            client.add_ing(textBox9.Text, textBox6.Text, textBox7.Text, textBox8.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                            MessageBox.Show("Ингредиент добавлен!");
                            vivod_ing();
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox8.Text = "";
                            textBox9.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Введите цифры!");
                            textBox8.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого поставщика не существует!");
                        textBox7.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Такой товар не существует!");
                    textBox9.Text = "";
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            DataTable dt = client.search_ing(textBox6.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            tabControl1.SelectedTab = tabPage2;
            dataGridView2.DataSource = dt;
            button11.Visible = false;
            button10.Visible = true;
            textBox6.Text = "";
            label6.Visible = false;
            textBox6.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Visible = false;
            button14.Visible = true;
            label1.Visible = true;
            textBox1.Visible = true;
            button3.Visible = false;
            button2.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string result = client.search_tovar(textBox1.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            if(result == "yes")
            {
                client.drop_tovar_name(textBox1.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                label1.Visible = false;
                textBox1.Text = "";
                textBox1.Visible = false;
                button14.Visible = false;
                button13.Visible = true;
                button3.Visible = true;
                button2.Visible = true;
                vivod();
                vivod_ing();
                MessageBox.Show("Товар успешно удален!");
            }
            else
            {
                label1.Visible = false;
                textBox1.Text = "";
                textBox1.Visible = false;
                button14.Visible = false;
                button13.Visible = true;
                button3.Visible = true;
                button2.Visible = true;
                MessageBox.Show("Товара не существует!");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            label10.Visible = true;
            textBox10.Visible = true;
            button15.Visible = true;
            button16.Visible = false;
            button17.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button15.Visible = false;
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string result = client.search_sklad(textBox10.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            if(textBox10.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
                textBox10.Text ="";
            }
            else
            {
                if(result == "yes")
                {
                    MessageBox.Show("Склад существует!");
                    textBox10.Text = "";
                }
                else
                {
                    label10.Visible = false;
                    textBox10.Visible = false;
                    button15.Visible = false;
                    button16.Visible = true;
                    button17.Visible = true;
                    client.add_sklad(textBox10.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                    vivod_sklad();
                    MessageBox.Show("Склад добавлен!");
                    textBox10.Text = "";
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            label10.Visible = true;
            textBox10.Visible = true;
            button18.Visible = true;
            button17.Visible = false;
            button16.Visible = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string result = client.search_sklad(textBox10.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
            if (textBox10.Text == "")
            {
                MessageBox.Show("Поле не может быть пустым!");
                textBox10.Text = "";
            }
            else
            {
                if(result == "no")
                {
                    MessageBox.Show("Склад не существует!");
                    textBox10.Text = "";
                }
                else
                {
                    label10.Visible = false;
                    textBox10.Visible = false;
                    button18.Visible = false;
                    button17.Visible = true;
                    button16.Visible = true;
                    client.delete_sklad(textBox10.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                    vivod_sklad();
                    MessageBox.Show("Склад удален!");
                    textBox10.Text = "";
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (Settings.Default["role"].ToString() != "admin")
            {
                tabControl1.TabPages.Add(tabPage5);
            }
            tabControl1.SelectedTab = tabPage5;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            tablepeople u = new tablepeople();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            button11.Visible = false;
            button21.Visible = false;
            button8.Visible = false;
            button7.Visible = false;
            label6.Visible = true;
            textBox6.Visible = true;
            button10.Visible = false;
            button9.Visible = false;
            button22.Visible = true;
            label7.Visible = true;
            textBox7.Visible = true;

        }

        private void button22_Click(object sender, EventArgs e)
        {
            if(textBox6.Text == "")
            {
                MessageBox.Show("Название не может быть пустым!");
                textBox6.Text = "";
            }
            else
            {
                if(textBox7.Text == "")
                {
                    MessageBox.Show("Поле ФИО должно быть заполненно!");
                    textBox7.Text = "";
                }
                var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
                string result = client.search_ing_provider(textBox7.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                if(result == "yes")
                {
                    string res = client.drop_ing(textBox6.Text, textBox7.Text, Settings.Default["role_bd"].ToString(), Settings.Default["token"].ToString());
                    if(res == "yes")
                    {
                        vivod_ing();
                        MessageBox.Show("Ингредиент успешно удален!");
                        textBox7.Text = "";
                        textBox6.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Такого ингредиента не существует!");
                        textBox7.Text = "";
                        textBox6.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Такого поставщика не существует!");
                    textBox7.Text = "";
                }
            }
        }
    }
}
