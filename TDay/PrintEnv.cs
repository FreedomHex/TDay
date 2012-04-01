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
    public partial class PrintEnv : Form
    {
        public PrintEnv()
        {
            InitializeComponent();
        }
        public int ProfileId =-1;
        private PdfPrinter.EnvelopeSize SendSize;


        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            PdfPrinter.PrintEnvelope(1,SendSize, richTextBox1.Text,richTextBox2.Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SendSize = PdfPrinter.EnvelopeSize.Env10;
            toolStripButton2.ForeColor = Color.DimGray;
            toolStripButton3.ForeColor = Color.DimGray;
            toolStripButton4.ForeColor = Color.DimGray;
            toolStripButton5.ForeColor = Color.DimGray;
            toolStripButton1.ForeColor = Color.CornflowerBlue;
            toolStripButton6.Enabled = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SendSize = PdfPrinter.EnvelopeSize.EnvLS;
            toolStripButton1.ForeColor = Color.DimGray;
            toolStripButton3.ForeColor = Color.DimGray;
            toolStripButton4.ForeColor = Color.DimGray;
            toolStripButton5.ForeColor = Color.DimGray;
            toolStripButton2.ForeColor = Color.CornflowerBlue;
            toolStripButton6.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SendSize = PdfPrinter.EnvelopeSize.EnvTY;
            toolStripButton1.ForeColor = Color.DimGray;
            toolStripButton2.ForeColor = Color.DimGray;
            toolStripButton4.ForeColor = Color.DimGray;
            toolStripButton5.ForeColor = Color.DimGray;
            toolStripButton3.ForeColor = Color.CornflowerBlue;
            toolStripButton6.Enabled = true;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SendSize = PdfPrinter.EnvelopeSize.EnvBD2;
            toolStripButton1.ForeColor = Color.DimGray;
            toolStripButton2.ForeColor = Color.DimGray;
            toolStripButton3.ForeColor = Color.DimGray;
            toolStripButton4.ForeColor = Color.DimGray;
            toolStripButton5.ForeColor = Color.CornflowerBlue;
            toolStripButton6.Enabled = true;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SendSize = PdfPrinter.EnvelopeSize.EnvBD1;
            toolStripButton1.ForeColor = Color.DimGray;
            toolStripButton2.ForeColor = Color.DimGray;
            toolStripButton3.ForeColor = Color.DimGray;
            toolStripButton5.ForeColor = Color.DimGray;
            toolStripButton4.ForeColor = Color.CornflowerBlue;
            toolStripButton6.Enabled = true;
        }

        private void PrintEnv_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = TDay.Properties.Settings.Default.PlantString.Remove(TDay.Properties.Settings.Default.PlantString.LastIndexOf("\n"));
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            if (ProfileId != -1)
            {
                Profile _prof = new Profile(ProfileId);
                richTextBox2.AppendText(_prof.Name + "\n");
                richTextBox2.AppendText(_prof.Adress.Addres + "\n");
                richTextBox2.AppendText(_prof.Adress.City + "\n");
                richTextBox2.AppendText(_prof.Adress.PostalCode + "\n");
                richTextBox2.SelectAll();
                richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (!richTextBox1.Enabled)
            {
                richTextBox1.Enabled = true;
                richTextBox1.BackColor = Color.White;
                richTextBox1.ReadOnly = false;
                richTextBox2.Enabled = true;
                richTextBox2.ReadOnly = false;
                richTextBox2.BackColor = Color.White;
            }
            else
            {
                richTextBox1.Enabled = false;
                richTextBox1.BackColor = SystemColors.Control;
                richTextBox1.ReadOnly = true;
                richTextBox2.Enabled = false;
                richTextBox2.ReadOnly = true;
                richTextBox2.BackColor = SystemColors.Control;
            }
        } 

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
