using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Properties;

namespace Client
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var client = new ServiceReference.Service_kursClient("NetTcpBinding_IService_kurs");
            string result = client.search_sessions(Settings.Default["token"].ToString());
            if(result == "yes")
            {
                Application.Run(new mainm());
            }
            else
            {
                Application.Run(new login());
            }
        }
    }
}
