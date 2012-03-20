using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace TDay
{
    public partial class UsersAdd : Form
    {
        public UsersAdd()
        {
            InitializeComponent();
        }
        public int UserId = -1;
        private void UsersAdd_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.UGroups". При необходимости она может быть перемещена или удалена.
            this.uGroupsTableAdapter.Fill(this.tDayDataSet.UGroups);
            this.usersTableAdapter.Fill(tDayDataSet.Users);
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            if (UserId != -1)
            {
                foreach (DataRow Row in tDayDataSet.Users)
                {
                    if ((int)Row["UserId"] == UserId)
                    {
                        textBox1.Text = Row["Login"].ToString();
                        textBox2.Text = Row["Password"].ToString();
                        comboBox1.SelectedValue = (int)Row["UGroup"];
                        //comboBox1.Select();
                        break;
                    }
                }
                button1.Text = "Edit";
            }
            else
            {
                button1.Text = "Add";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UserId == -1)
            {
                usersTableAdapter.Insert(textBox1.Text, textBox2.Text, (int)comboBox1.SelectedValue);
                ReLoad(sender, e);
                this.Close();
            }
            else
            {
                foreach (DataRow Row in tDayDataSet.Users)
                {
                    if ((int)Row["UserId"] == UserId)
                    {
                        Row["Login"] = textBox1.Text;
                        Row["Password"] = textBox2.Text;
                        Row["UGroup"] = comboBox1.SelectedValue;
                        break;
                    }
                }
                usersTableAdapter.Update(tDayDataSet.Users);
                ReLoad(sender, e);
                this.Close();
            }
        }

        private static void ReLoad(object sender, EventArgs e)
        {
            Form mainForm = Application.OpenForms["Users"];
            if (mainForm != null)
            {
                MethodInfo form1_Load = mainForm.GetType().GetMethod("Users_Load", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
                form1_Load.Invoke(mainForm, new object[] { sender, e });
            }
        }
    }
}
