using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDay
{
    public partial class Conn : Form
    {
        public Conn()
        {
            InitializeComponent();
        }

        private void Conn_Load(object sender, EventArgs e)
        {
            string ConnString = TDay.Properties.Settings.Default.TDayConnectionString;
            string[] Pars = ConnString.Split(';');
            foreach (string Value in Pars)
            {
                switch (Value.Split('=')[0])
                {
                    case "Data Source":
                        textBox_ServerName.Text = Value.Split('=')[1];
                        break;
                    case "Initial Catalog":
                        textBox_Database.Text = Value.Split('=')[1];
                        break;
                    case "User ID":
                        textBox_ServerUser.Text = Value.Split('=')[1];
                        break;
                    case "Password":
                        textBox_ServerPasswd.Text = Value.Split('=')[1];
                        break;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnString = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", textBox_ServerName.Text,textBox_Database.Text,textBox_ServerUser.Text,textBox_ServerPasswd.Text);
            //TDay.Properties.Settings.Default.TDayConnectionString = ConnString;
           // TDay.Properties.Settings.Default.Save();
            this.Close();

        }
    }
}
