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
    public partial class mainm : Form
    {
        public mainm()
        {
            InitializeComponent();
            label1.Text = "Ваш логин: " + Settings.Default["username"].ToString();
            label2.Text = "Ваша роль: " + Settings.Default["role_bd"].ToString();
            if (Settings.Default["role"].ToString() == "user")
            {
                button6.Visible = false;
                button7.Visible = false;
            }
            else
                label2.Text = "Выша роль: admin";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            client.drop_token(Settings.Default["username"].ToString(), Settings.Default["user_id"].ToString());
            Settings.Default["role"] = "";
            Settings.Default["token"] = "";
            Settings.Default["username"] = "";
            Settings.Default["user_id"] = "";
            Settings.Default.Save();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cruser u = new cruser();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            changepass u = new changepass();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tablepeople u = new tablepeople();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tovar_table u = new tovar_table();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            change_d u = new change_d();
            this.Hide();
            u.ShowDialog();
            this.Show();
        }
    }
}
