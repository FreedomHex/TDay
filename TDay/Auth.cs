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
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }
        public int _LoginPassCount = TDay.Properties.Settings.Default.LoginPassCount;
        public static bool isLogon = false;
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (_LoginPassCount > 1)
            {
                if (AuthProvider.Login(textBoxLogin.Text, textBoxPassword.Text))
                {
                    isLogon = true;
                    this.Close();
                }
                else
                {
                    _LoginPassCount--;
                    ErrorLabel.Text = String.Format("Unsuccessful login attempt, you have {0} attempts left", _LoginPassCount);
                    ErrorLabel.Visible = true;
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void Auth_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    button1_Click(sender, e);
                    break;
                case Keys.Escape:
                    Application.Exit();
                    break;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Conn src = new Conn();
            src.ShowDialog();
        }
    }
}
