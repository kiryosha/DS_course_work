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
    public partial class deluser : Form
    {
        public deluser()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string result = client.deluser(textBox2.Text);
            if (result == "no")
            {
                MessageBox.Show("Пользователя не существует!");
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("При удалении поставщика будут удалены его ингредиенты!");
                MessageBox.Show("Пользователь удален!");
                textBox2.Text = "";
            }
        }
    }
}
