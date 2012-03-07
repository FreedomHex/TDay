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
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            this.Text = "TDay" + " Version:" + TDay.Properties.Settings.Default.CurrentVersion;
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Categories". При необходимости она может быть перемещена или удалена.
            this.categoriesTableAdapter.Fill(this.tDayDataSet.Categories);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Profiles". При необходимости она может быть перемещена или удалена.
            this.profilesTableAdapter.Fill(this.tDayDataSet.Profiles);
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.Appearance = TabAppearance.Buttons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabStop = false;
            tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl2.Appearance = TabAppearance.Buttons;
            tabControl2.ItemSize = new System.Drawing.Size(0, 1);
            tabControl2.SizeMode = TabSizeMode.Fixed;
            tabControl2.TabStop = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Attendance;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Profiles;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Transportation;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Bills;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddProfile src = new AddProfile();
            src.ShowDialog();
        }

        

        

        
    }
}
