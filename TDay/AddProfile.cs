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
    public partial class AddProfile : Form
    {
        public AddProfile()
        {
            InitializeComponent();
        }

        private void AddProfile_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Categories". При необходимости она может быть перемещена или удалена.
            this.categoriesTableAdapter.Fill(this.tDayDataSet.Categories);
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabStop = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.Visible = false;
            toolStripLabel1.Visible = true;
            toolStripLabel2.Visible = true;
            toolStripTextBox1.Visible = true;
            toolStripTextBox2.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxCategory.SelectedIndex)
            {
                case 0:
                    tabControl1.SelectedTab = Client;
                    break;
                case 1:
                    tabControl1.SelectedTab = Employee;
                    break;
                case 2:
                    tabControl1.SelectedTab = Volunteer;
                    break;
                case 3:
                    tabControl1.SelectedTab = Board_Member;
                    break;
                case 4:
                    tabControl1.SelectedTab = Other;
                    break;

            }
        }
    }
}
