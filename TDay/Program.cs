using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace TDay
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ErrorProvider.CheckLogDir();
            using (var mutex = new Mutex(false, "TDay"))
            {
                if (mutex.WaitOne(TimeSpan.FromSeconds(3)))
                {
                    Application.Run(new Auth());
                    if (Auth.isLogon)
                    {
                        Application.Run(new MainFrame());
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show("Another instance of the app is already running");
                }
            }
        }
    }
}
