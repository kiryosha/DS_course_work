using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Properties;


namespace Client
{
    public partial class login : Forms
    {
        public static string reg_u = "";
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            var con = client.login(textBox1.Text, textBox2.Text);
            Settings.Default["role_bd"] = client.role_bd(con.Item5);
            string result_con = con.Item1;
            if (result_con == "yes")
            {
                Settings.Default["role"] = con.Item2;
                Settings.Default["token"] = con.Item4;
                Settings.Default["username"] = con.Item3;
                Settings.Default["user_id"] = con.Item5;
                Settings.Default.Save();

                mainm u = new mainm();
                this.Hide();
                u.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Пользователя не существует!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reg_u = "yes";
            cruser u = new cruser();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }
    }
}
