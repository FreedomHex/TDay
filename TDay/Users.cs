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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        public static int CurrentRowIndex;
        private void Users_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.UGroups". При необходимости она может быть перемещена или удалена.
            this.uGroupsTableAdapter.Fill(this.tDayDataSet.UGroups);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.tDayDataSet.Users);
            if (CurrentRowIndex < dataGridView1.Rows.Count && dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[CurrentRowIndex].Selected = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurrentRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            UsersAdd src = new UsersAdd();
            src.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CurrentRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            UsersAdd src = new UsersAdd();
            src.UserId = (int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["userIdDataGridViewTextBoxColumn"].Value;
            src.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CurrentRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this user?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                usersTableAdapter.Delete((int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["userIdDataGridViewTextBoxColumn"].Value);
                usersTableAdapter.Fill(tDayDataSet.Users);
            }
        }
    }
}
