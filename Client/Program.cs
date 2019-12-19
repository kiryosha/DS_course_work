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
            if(Settings.Default["token"].ToString() == "")
                Application.Run(new login());
            else
            {
                Application.Run(new mainm());
            }
        }
    }
}
