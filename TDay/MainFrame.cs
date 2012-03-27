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
            if (TDay.Properties.Settings.Default.DebugMode)
            {
                this.Text = "TDay";
                this.Text = "TDay" + " Version:" + TDay.Properties.Settings.Default.CurrentVersion + "|DebugMode";
            }
            else
            {
                this.Text = "TDay";
            }
        }
        public bool SortByCategory = false;
        public string CategoryFilter = String.Empty;
        public string _TransportDay = String.Empty;
        public Day CurrentDay = new Day();
        TDayDataSetTableAdapters.ServicesTableAdapter servicesTableAdapter = new TDayDataSetTableAdapters.ServicesTableAdapter();
        private void MainFrame_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Bills". При необходимости она может быть перемещена или удалена.
            this.billsTableAdapter.Fill(this.tDayDataSet.Bills);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Transportation". При необходимости она может быть перемещена или удалена.
            transportationTableAdapter.Fill(this.tDayDataSet.Transportation);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Days". При необходимости она может быть перемещена или удалена.
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Categories". При необходимости она может быть перемещена или удалена.
            this.categoriesTableAdapter.Fill(this.tDayDataSet.Categories);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tDayDataSet.Profiles". При необходимости она может быть перемещена или удалена.
            this.profilesTableAdapter.FillAll(this.tDayDataSet.Profiles);
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
            toolStripComboBox1.SelectedIndex = 0;
            DataGridViewCellEventArgs sen = new DataGridViewCellEventArgs(0, 0);
            CurrentDay.CreateDay();
            this.daysTableAdapter.Fill(this.tDayDataSet.Days,CurrentDay.Date);
            textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
            textBox_date.Text = CurrentDay.Date.ToShortDateString();
            ReCountTotals();
            CheckPermission();
            
        }

        private void RelDataGrid2(object sender, int RowIndex)
        {
            this.profilesTableAdapter.Fill(this.tDayDataSet.Profiles);
            if (dataGridView1.Rows.Count > 0 && RowIndex<dataGridView1.Rows.Count)
            {
                DataGridViewCellEventArgs sel = new DataGridViewCellEventArgs(0, RowIndex);
                dataGridView1_CellClick(this, sel);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentDay.CreateDay();
            profilesTableAdapter.FillAll(tDayDataSet.Profiles);
            daysTableAdapter.Fill(tDayDataSet.Days,CurrentDay.Date);
            tabControl1.SelectedTab = Attendance;
            ReCountTotals();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Profiles;
            profilesTableAdapter.Fill(tDayDataSet.Profiles);
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewCellEventArgs sel = new DataGridViewCellEventArgs(0,0);
                dataGridView1_CellClick(sender, sel);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Transportation;
            transportationTableAdapter.Fill(tDayDataSet.Transportation);
            button12_Click(sender, e);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Bill _Bill = new Bill();
            _Bill.Create();
            billsTableAdapter.Fill(tDayDataSet.Bills);
            tabControl1.SelectedTab = Bills;
            richTextBox1.Text = TDay.Properties.Settings.Default.PlantString;
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox2.SelectAll();
            richTextBox2.SelectionAlignment = HorizontalAlignment.Center;
            daysBindingSource2.Filter = "ProfileId=-1";
            daysTableAdapter.FillByMonth(tDayDataSet.Days, _Bill.FirstDayOfMonth, _Bill.LastDayOfMonth);
            dataGridView6.Sort(dateDataGridViewTextBoxColumn2, ListSortDirection.Ascending);
            profilesTableAdapter.FillAll(tDayDataSet.Profiles);
            if (dataGridView5.Rows.Count > 0)
            {
                dataGridView5_CellClick(sender,new DataGridViewCellEventArgs(0,0));
            }

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddProfile src = new AddProfile();
            src.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button8.Enabled = true;
             if (e.RowIndex < dataGridView1.Rows.Count && e.RowIndex>=0)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;

                switch (dataGridView1.Rows[e.RowIndex].Cells["categoryDataGridViewTextBoxColumn"].Value.ToString())
                {
                    #region Client
                    case "1":
                        tabControl2.SelectedTab = ClientTab;
                        Client client = new Client(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        textBox_ClientName.Text = client.Name;
                        textBox_ClientBirth.Text = client.DateOfBirdh.ToShortDateString();
                        textBox_ClientParis.Text = client.ParisNumber;
                        textBox_ClientAdress.Text = client.Adress.Addres;
                        textBox_ClientCity.Text = client.Adress.City;
                        textBox_ClientProvince.Text = client.Adress.Province;
                        textBox_ClientCountry.Text = client.Adress.Country;
                        textBox_ClientPostal.Text = client.Adress.PostalCode;
                        textBox_ClientPhone.Text = client.Adress.Phone;
                        textBox_ClientEmail.Text = client.Adress.Email;
                        radioButton4.Checked = client.Own;
                        radioButton5.Checked = !client.Own;
                        if (!client.Own)
                        {
                            label5.Visible = true;
                            textBox_ClientHD.Visible = true;
                        }
                        else
                        {
                            label5.Visible = false;
                            textBox_ClientHD.Visible = false;
                        }
                        textBox_ClientEmerName.Text = client.EmergencyContact.Name;
                        textBox_ClientEmPhone.Text = client.EmergencyContact.Phone;
                        textBox_ClientRelation.Text = client.EmergencyContact.Relation;
                        textBox_ClientHD.Text = client.Transportation.HandyDARTNumber;
                        textBox_ClientDocName.Text = client.DoctorName;
                        textBoxClientDocPhone.Text = client.DoctorPhone;
                        textBox_ClientPharmName.Text = client.PharmacistName;
                        textBox_ClientPharmPhone.Text = client.PharmacistPhone;
                        ClientMember.Checked = client.Member;
                        if (client.DopEmergencyContact != null)
                        {
                            toolStripLabel1.Visible = true;
                            toolStripLabel2.Visible = true;
                            toolStripLabel4.Visible = true;
                            DopEmerClientName.Visible = true;
                            DopEmerClientPhone.Visible = true;
                            toolStripTextBox2.Visible = true;
                            toolStripButton2.Visible = false;
                            DopEmerClientName.Text = client.DopEmergencyContact.Name;
                            DopEmerClientPhone.Text = client.DopEmergencyContact.Phone;
                            toolStripTextBox2.Text = client.DopEmergencyContact.Relation;
                        }
                        else
                        {
                            toolStripButton2.Visible = true;
                            toolStripLabel1.Visible = false;
                            toolStripLabel2.Visible = false;
                            toolStripLabel4.Visible = false;
                            DopEmerClientName.Visible = false;
                            toolStripTextBox2.Visible = false;
                            DopEmerClientPhone.Visible = false;
                        }
                        checkBox2.Checked = client.Attendance.Monday;
                        checkBox3.Checked = client.Attendance.Tuesday;
                        checkBox4.Checked = client.Attendance.Wednesday;
                        checkBox5.Checked = client.Attendance.Thursday;
                        checkBox6.Checked = client.Attendance.Friday;

                        checkBox11.Checked = client.Transportation.Monday;
                        checkBox10.Checked = client.Transportation.Tuesday;
                        checkBox9.Checked = client.Transportation.Wednesday;
                        checkBox8.Checked = client.Transportation.Thursday;
                        checkBox7.Checked = client.Transportation.Friday;
                        break;
                    #endregion
                    #region Employee
                    case "2":
                        tabControl2.SelectedTab = EmployeeTab;
                        Employee employee = new Employee(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        textBox_EmpName.Text = employee.Name;
                        textBox_EmpBirth.Text = employee.DateOfBirdh.ToShortDateString();
                        textBox_EmpHire.Text = employee.HireDate.ToShortDateString();
                        textBox_EmpPosition.Text = employee.Position;
                        textBox_EmpSin.Text = employee.SIN;
                        switch (employee.PositionType)
                        {
                            case "Causal":
                                radioButton1.Checked = true;
                                break;
                            case "Part time":
                                radioButton2.Checked = true;
                                break;
                            case "Full time":
                                radioButton3.Checked = true;
                                break;
                        }
                        textBox_EmpAdress.Text = employee.Adress.Addres;
                        textBox_EmpCity.Text = employee.Adress.City;
                        textBox_EmpProvince.Text = employee.Adress.Province;
                        textBox_EmpCountry.Text = employee.Adress.Country;
                        textBox_EmpPostal.Text = employee.Adress.PostalCode;
                        textBox_EmpPhone.Text = employee.Adress.Phone;
                        textBox_EmpEmail.Text = employee.Adress.Email;
                        textBox_EmpCell.Text = employee.Adress.Cell;
                        textBox_EmpEmerCN.Text = employee.EmergencyContact.Name;
                        textBox_EmerCP.Text = employee.EmergencyContact.Phone;
                        textBox_EmpRelation.Text = employee.EmergencyContact.Relation;
                        checkBox16.Checked = employee.Attendance.Monday;
                        checkBox15.Checked = employee.Attendance.Tuesday;
                        checkBox14.Checked = employee.Attendance.Wednesday;
                        checkBox13.Checked = employee.Attendance.Thursday;
                        checkBox12.Checked = employee.Attendance.Friday;
                        break;
                    #endregion
                    #region Volonteer
                    case "3":
                        tabControl2.SelectedTab = VolunteerTab;
                        Profile volonteer = new Profile(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        textBox_VolName.Text = volonteer.Name;
                        textBox_VolBirth.Text = volonteer.DateOfBirdh.ToShortDateString();
                        textBox_VolAdress.Text = volonteer.Adress.Addres;
                        textBox_VolCity.Text = volonteer.Adress.City;
                        textBox_VolProvince.Text = volonteer.Adress.Province;
                        textBox_VolCountry.Text = volonteer.Adress.Country;
                        textBox_VolPostal.Text = volonteer.Adress.PostalCode;
                        textBox_VolPhone.Text = volonteer.Adress.Phone;
                        textBox_VolEmail.Text = volonteer.Adress.Email;
                        textBox_VolCell.Text = volonteer.Adress.Cell;
                        textBox_VolEmerCN.Text = volonteer.EmergencyContact.Name;
                        textBox_VolEmerCP.Text = volonteer.EmergencyContact.Phone;
                        textBox_VolEmerRelation.Text = volonteer.EmergencyContact.Relation;
                        checkBox21.Checked = volonteer.Attendance.Monday;
                        checkBox20.Checked = volonteer.Attendance.Tuesday;
                        checkBox19.Checked = volonteer.Attendance.Wednesday;
                        checkBox18.Checked = volonteer.Attendance.Thursday;
                        checkBox17.Checked = volonteer.Attendance.Friday;
                        break;
                    #endregion
                    #region BoardMember
                    case "4":
                        tabControl2.SelectedTab = Board_MemberTab;
                        textBox_BoardOccupation.Visible = true;
                        label48.Visible = true;
                        Profile board = new Profile(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        textBox_BoardName.Text = board.Name;
                        textBox_BoardOccupation.Text = board.Occupation;
                        textBox_BoardBirth.Text = board.DateOfBirdh.ToShortDateString();
                        textBox_BoardAdress.Text = board.Adress.Addres;
                        textBox_BoardCity.Text = board.Adress.City;
                        textBox_BoardProvince.Text = board.Adress.Province;
                        textBox_BoardCountry.Text = board.Adress.Country;
                        textBox_BoardCell.Text = board.Adress.Cell;
                        textBox_BoardPostal.Text = board.Adress.PostalCode;
                        textBox_BoardPhone.Text = board.Adress.Phone;
                        textBox_BoardEmail.Text = board.Adress.Email;
                        break;
                    #endregion
                    #region Other
                    case "5":
                        tabControl2.SelectedTab = Board_MemberTab;
                        textBox_BoardOccupation.Visible = false;
                        label48.Visible = false;
                        board = new Profile(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        textBox_BoardName.Text = board.Name;
                        textBox_BoardOccupation.Text = board.Occupation;
                        textBox_BoardBirth.Text = board.DateOfBirdh.ToShortDateString();
                        textBox_BoardAdress.Text = board.Adress.Addres;
                        textBox_BoardCity.Text = board.Adress.City;
                        textBox_BoardProvince.Text = board.Adress.Province;
                        textBox_BoardCountry.Text = board.Adress.Country;
                        textBox_BoardCell.Text = board.Adress.Cell;
                        textBox_BoardPostal.Text = board.Adress.PostalCode;
                        textBox_BoardPhone.Text = board.Adress.Phone;
                        textBox_BoardEmail.Text = board.Adress.Email;
                        break;
                    #endregion
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripLabel1.Visible = true;
            toolStripLabel2.Visible = true;
            toolStripLabel4.Visible = true;
            DopEmerClientName.Visible = true;
            DopEmerClientPhone.Visible = true;
            toolStripButton2.Visible = false;
            toolStripTextBox2.Visible = true;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.Items[toolStripComboBox1.SelectedIndex].ToString())
            {
                case "All":
                    switch(Session.Group){
                        case 1:
                             SortByCategory = false;
                              this.profilesBindingSource.RemoveFilter();
                            break;
                        case 2:
                            SortByCategory = false;
                        this.profilesBindingSource.Filter = "Category<2 OR Category>2";
                            break;
                        case 3:
                            SortByCategory = false;
                            this.profilesBindingSource.Filter = "Category=1";
                            break;
                    }
                    break;
                case "Client":
                    SortByCategory = true;
                    this.profilesBindingSource.Filter = "Category = 1";
                    CategoryFilter = "Category = 1";
                    break;
                case "Employee":
                    SortByCategory = true;
                    this.profilesBindingSource.Filter = "Category = 2";
                    CategoryFilter = "Category = 2";
                    break;
                case "Volunteer":
                    SortByCategory = true;
                    this.profilesBindingSource.Filter = "Category = 3";
                    CategoryFilter = "Category = 3";
                    break;
                case "Board Member":
                    SortByCategory = true;
                    this.profilesBindingSource.Filter = "Category = 4";
                    CategoryFilter = "Category = 4";
                    break;
                case "Other":
                    SortByCategory = true;
                    this.profilesBindingSource.Filter = "Category = 5";
                    CategoryFilter = "Category = 5";
                    break;
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Length > 2)
            {
                if (SortByCategory)
                {
                    switch (Session.Group)
                    {
                        case 1:
                            profilesBindingSource.Filter = CategoryFilter + " AND " + String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                            break;
                        case 2:
                            profilesBindingSource.Filter = "(Category<2 OR Category>2) AND "+ CategoryFilter + " AND " + String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                            break;
                        case 3:
                            profilesBindingSource.Filter = "Category=1 AND " + CategoryFilter + " AND " + String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                            break;
                    }
                   
                }
                else
                {
                    switch (Session.Group)
                    {
                        case 1:
                            profilesBindingSource.Filter = String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                            break;
                        case 2:
                            profilesBindingSource.Filter ="(Category<2 OR Category>2) AND "+ String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                            break;
                        case 3:
                            profilesBindingSource.Filter = "Category=1 AND " + String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                            break;
                    }
                    
                }
            }
            else
            {
                if (SortByCategory)
                {
                    switch (Session.Group)
                    {
                        case 1:
                            profilesBindingSource.Filter = CategoryFilter;
                            break;
                        case 2:
                            profilesBindingSource.Filter = CategoryFilter + " AND (Category<2 OR Category>2)";
                            break;
                        case 3:
                            profilesBindingSource.Filter = CategoryFilter + " AND Category=1";
                            break;
                    }
                    
                }
                else
                {
                    switch (Session.Group)
                    {
                        case 1:
                            profilesBindingSource.RemoveFilter();
                            break;
                        case 2:
                            profilesBindingSource.RemoveFilter();
                            profilesBindingSource.Filter = "Category<2 OR Category>2";
                            break;
                        case 3:
                            profilesBindingSource.RemoveFilter();
                            profilesBindingSource.Filter = "Category=1";
                            break;
                    }
                    
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Profile client = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete a profile " + client.Name + " ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    client.Delete();
                    profilesTableAdapter.Fill(tDayDataSet.Profiles);
                }
            }
        }

        private void textBox_ClientName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox_ClientBirth_TextChanged(object sender, EventArgs e)
        {
            button8.Enabled = true;
        }

        private void CheckDateFormat(TextBox Box)
        {
            switch (Box.Text.Length)
            {
                case 2:
                    Box.Text += ".";
                    Box.SelectionStart = Box.TextLength;
                    Box.ScrollToCaret();
                    Box.Refresh();
                    break;
                case 5:
                    Box.Text += ".";
                    Box.SelectionStart = Box.TextLength;
                    Box.ScrollToCaret();
                    Box.Refresh();
                    break;
                case 10:
                    DateTime Test = new DateTime();
                    if (!DateTime.TryParse(Box.Text, out Test))
                    {
                        button8.Enabled = false;
                        AddProfileErrorProvider.SetError(Box, "Invalid date format \nThe date must be in the format (dd.mm.yyyy) \nand contain only numbers included in the date range");
                    }
                    else
                    {
                        AddProfileErrorProvider.Clear();
                        button8.Enabled = true;
                    }
                    break;
            }
        }

        private void textBox_EmpBirth_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_EmpHire_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox_VolBirth_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox_BoardBirth_TextChanged(object sender, EventArgs e)
        {
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int RowIndex = dataGridView1.SelectedCells[0].RowIndex;
                switch (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["categoryDataGridViewTextBoxColumn"].Value.ToString())
                {
                    #region Client
                    case "1":
                        tabControl2.SelectedTab = ClientTab;
                        Client client = new Client(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        client.Name = textBox_ClientName.Text;
                        client.DateOfBirdh = DateTime.Parse(textBox_ClientBirth.Value.ToShortDateString());
                        client.ParisNumber = textBox_ClientParis.Text;
                        client.Adress.Addres = textBox_ClientAdress.Text;
                        client.Adress.City = textBox_ClientCity.Text;
                        client.Adress.Province = textBox_ClientProvince.Text;
                        client.Adress.Country = textBox_ClientCountry.Text;
                        client.Adress.PostalCode = textBox_ClientPostal.Text;
                        client.Adress.Phone = textBox_ClientPhone.Text;
                        client.Adress.Email = textBox_ClientEmail.Text;
                        client.EmergencyContact.Name = textBox_ClientEmerName.Text;
                        client.EmergencyContact.Phone = textBox_ClientEmPhone.Text;
                        client.EmergencyContact.Relation = textBox_ClientRelation.Text;
                        client.Transportation.HandyDARTNumber = textBox_ClientHD.Text;
                        client.DoctorName = textBox_ClientDocName.Text;
                        client.DoctorPhone = textBoxClientDocPhone.Text;
                        client.Own = radioButton4.Checked;
                        client.PharmacistName = textBox_ClientPharmName.Text;
                        client.PharmacistPhone = textBox_ClientPharmPhone.Text;
                        client.Member = ClientMember.Checked;
                        if (DopEmerClientName.Visible && client.DopEmergencyContact == null)
                        {
                            client.DopEmergencyContact = new EmergencyContact();
                            client.DopEmergencyContact.Name = DopEmerClientName.Text;
                            client.DopEmergencyContact.Phone = DopEmerClientPhone.Text;
                            client.DopEmergencyContact.Relation = toolStripTextBox2.Text;
                            client.DopEmergencyContact.AddEmergencyContactTo(client);
                        }
                        else if (DopEmerClientName.Visible)
                        {
                            client.DopEmergencyContact.Name = DopEmerClientName.Text;
                            client.DopEmergencyContact.Phone = DopEmerClientPhone.Text;
                            client.DopEmergencyContact.Relation = toolStripTextBox2.Text;
                        }

                        client.Attendance.Monday = checkBox2.Checked;
                        client.Attendance.Tuesday = checkBox3.Checked;
                        client.Attendance.Wednesday = checkBox4.Checked;
                        client.Attendance.Thursday = checkBox5.Checked;
                        client.Attendance.Friday = checkBox6.Checked;
                        client.Transportation.Monday = checkBox11.Checked;
                        client.Transportation.Tuesday = checkBox10.Checked;
                        client.Transportation.Wednesday = checkBox9.Checked;
                        client.Transportation.Thursday = checkBox8.Checked;
                        client.Transportation.Friday = checkBox7.Checked;
                        client.Transportation.Address = client.Adress.Addres;
                        client.Transportation.Phone = client.Adress.Phone;
                        if (client.Own) { client.Transportation.Category = "Own"; } else { client.Transportation.Category = "HandyDART"; }
                        client.Update();
                        profilesTableAdapter.Fill(tDayDataSet.Profiles);
                        dataGridView1.Rows[RowIndex].Selected = true;
                        button8.Enabled = false;
                        break;
                    #endregion
                    case "2":
                        Employee employee = new Employee(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        employee.Name = textBox_EmpName.Text;
                        employee.DateOfBirdh = DateTime.Parse(textBox_EmpBirth.Value.ToShortDateString());
                        employee.HireDate = DateTime.Parse(textBox_EmpHire.Value.ToShortDateString());
                        employee.Position = textBox_EmpPosition.Text;
                        employee.SIN = textBox_EmpSin.Text;
                        if (radioButton1.Checked) { employee.PositionType = "Causal"; }
                        if (radioButton2.Checked) { employee.PositionType = "Part time"; }
                        if (radioButton3.Checked) { employee.PositionType = "Full time"; }

                        employee.Adress.Addres = textBox_EmpAdress.Text;
                        employee.Adress.City = textBox_EmpCity.Text;
                        employee.Adress.Province = textBox_EmpProvince.Text;
                        employee.Adress.Country = textBox_EmpCountry.Text;
                        employee.Adress.PostalCode = textBox_EmpPostal.Text;
                        employee.Adress.Phone = textBox_EmpPhone.Text;
                        employee.Adress.Email = textBox_EmpEmail.Text;
                        employee.Adress.Cell = textBox_EmpCell.Text;
                        employee.EmergencyContact.Name = textBox_EmpEmerCN.Text;
                        employee.EmergencyContact.Phone = textBox_EmerCP.Text;
                        employee.EmergencyContact.Relation = textBox_EmpRelation.Text;
                        employee.Attendance.Monday = checkBox16.Checked;
                        employee.Attendance.Tuesday = checkBox15.Checked;
                        employee.Attendance.Wednesday = checkBox14.Checked;
                        employee.Attendance.Thursday = checkBox13.Checked;
                        employee.Attendance.Friday = checkBox12.Checked;
                        employee.Update();
                        profilesTableAdapter.Fill(tDayDataSet.Profiles);
                        dataGridView1.Rows[RowIndex].Selected = true;
                        button8.Enabled = false;
                        break;
                    case "3":
                        tabControl2.SelectedTab = VolunteerTab;
                        Profile volonteer = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        volonteer.Name = textBox_VolName.Text;
                        volonteer.DateOfBirdh = DateTime.Parse(textBox_VolBirth.Value.ToShortDateString());
                        volonteer.Adress.Addres = textBox_VolAdress.Text;
                        volonteer.Adress.City = textBox_VolCity.Text;
                        volonteer.Adress.Province = textBox_VolProvince.Text;
                        volonteer.Adress.Country = textBox_VolCountry.Text;
                        volonteer.Adress.PostalCode = textBox_VolPostal.Text;
                        volonteer.Adress.Phone = textBox_VolPhone.Text;
                        volonteer.Adress.Email = textBox_VolEmail.Text;
                        volonteer.Adress.Cell = textBox_VolCell.Text;
                        volonteer.EmergencyContact.Name = textBox_VolEmerCN.Text;
                        volonteer.EmergencyContact.Phone = textBox_VolEmerCP.Text;
                        volonteer.EmergencyContact.Relation = textBox_VolEmerRelation.Text;
                        volonteer.Attendance.Monday = checkBox21.Checked;
                        volonteer.Attendance.Tuesday = checkBox20.Checked;
                        volonteer.Attendance.Wednesday = checkBox19.Checked;
                        volonteer.Attendance.Thursday = checkBox18.Checked;
                        volonteer.Attendance.Friday = checkBox17.Checked;
                        volonteer.Update();
                        profilesTableAdapter.Fill(tDayDataSet.Profiles);
                        dataGridView1.Rows[RowIndex].Selected = true;
                        button8.Enabled = false;
                        break;
                    case "4":

                        textBox_BoardOccupation.Visible = true;
                        label48.Visible = true;
                        Profile board = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        board.Name = textBox_BoardName.Text;
                        board.Occupation = textBox_BoardOccupation.Text;
                        board.DateOfBirdh = DateTime.Parse(textBox_BoardBirth.Value.ToShortDateString());
                        board.Adress.Addres = textBox_BoardAdress.Text;
                        board.Adress.City = textBox_BoardCity.Text;
                        board.Adress.Province = textBox_BoardProvince.Text;
                        board.Adress.Country = textBox_BoardCountry.Text;
                        board.Adress.Cell = textBox_BoardCell.Text;
                        board.Adress.PostalCode = textBox_BoardPostal.Text;
                        board.Adress.Phone = textBox_BoardPhone.Text;
                        board.Adress.Email = textBox_BoardEmail.Text;
                        board.Update();
                        profilesTableAdapter.Fill(tDayDataSet.Profiles);
                        dataGridView1.Rows[RowIndex].Selected = true;
                        button8.Enabled = false;
                        break;
                    case "5":
                        textBox_BoardOccupation.Visible = false;
                        label48.Visible = false;
                        Profile other = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                        other.Name = textBox_BoardName.Text;
                        other.DateOfBirdh = DateTime.Parse(textBox_BoardBirth.Value.ToShortDateString());
                        other.Adress.Addres = textBox_BoardAdress.Text;
                        other.Adress.City = textBox_BoardCity.Text;
                        other.Adress.Province = textBox_BoardProvince.Text;
                        other.Adress.Country = textBox_BoardCountry.Text;
                        other.Adress.Cell = textBox_BoardCell.Text;
                        other.Adress.PostalCode = textBox_BoardPostal.Text;
                        other.Adress.Phone = textBox_BoardPhone.Text;
                        other.Adress.Email = textBox_BoardEmail.Text;
                        other.Update();
                        profilesTableAdapter.Fill(tDayDataSet.Profiles);
                        dataGridView1.Rows[RowIndex].Selected = true;
                        button8.Enabled = false;
                        break;
                }
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            FormProvider.CurrentRowIndex = e.RowIndex;
            FormProvider.CurrentColIndex = e.ColumnIndex;
            switch (e.ColumnIndex)
            {
                case 3:
                    DayItem Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.LunchPrice = (decimal)dataGridView2.Rows[e.RowIndex].Cells["lunchPriceDataGridViewTextBoxColumn"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 4:
                    Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.TakeOutPrice = (decimal)dataGridView2.Rows[e.RowIndex].Cells["takeOutPriceDataGridViewTextBoxColumn"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 5:
                   Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.MiscellaneousPrice = (decimal)dataGridView2.Rows[e.RowIndex].Cells["miscellaneousPriceDataGridViewTextBoxColumn"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 6:
                     Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.ProgramPrice = (decimal)dataGridView2.Rows[e.RowIndex].Cells["ProgramPrice"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 7:
                    Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.VanPrice = (decimal)dataGridView2.Rows[e.RowIndex].Cells["vanPriceDataGridViewTextBoxColumn"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 8:
                     Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.RoundTripPrice = (decimal)dataGridView2.Rows[e.RowIndex].Cells["roundTripPriceDataGridViewTextBoxColumn"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 9:
                    Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.BookOfTickets = (decimal)dataGridView2.Rows[e.RowIndex].Cells["bookOfTicketsDataGridViewTextBoxColumn"].Value;
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
                case 11:
                    Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                    Item.Comments = dataGridView2.Rows[e.RowIndex].Cells["commentsDataGridViewTextBoxColumn"].Value.ToString();
                    Item.Update();
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    break;
            }
            ReCountTotals();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                FormProvider.CurrentRowIndex = e.RowIndex;
                FormProvider.CurrentColIndex = e.ColumnIndex;
                switch (e.ColumnIndex)
                {
                    case 1:
                        if ((bool)dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                        {
                            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this entry?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                daysTableAdapter.Delete((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value);
                                daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                            }
                            else
                            {
                                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                                dataGridView2.RefreshEdit();
                            }
                        }

                        break;
                    case 2:
                        if ((bool)dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue)
                        {
                            DayItem Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                            Item.Lunch = false;
                            Item.LunchPrice = Decimal.Zero;
                            Item.Update();
                            daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                        }
                        else
                        {
                            DayItem Item = new DayItem((int)dataGridView2.Rows[e.RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                            Item.Lunch = true;
                            Item.LunchPrice = Item.GetLunchPrice();
                            Item.Update();
                            daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                        }
                        break;

                }
                ReCountTotals();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                FormProvider.CurrentRowIndex = e.RowIndex;
                FormProvider.CurrentColIndex = e.ColumnIndex;
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                switch (e.ColumnIndex)
                {
                    case 3:
                            contextMenuStrip1.Tag = e.ColumnIndex;
                            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                break;
                    case 6:
                        if (ProfileProvider.GetCategory((int)dataGridView2.Rows[e.RowIndex].Cells["profileIdDataGridViewTextBoxColumn1"].Value) == 1)
                        {
                            contextMenuStrip1.Tag = e.ColumnIndex;
                            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                        }
                        break;
                    case 8:
                        if (ProfileProvider.GetCategory((int)dataGridView2.Rows[e.RowIndex].Cells["profileIdDataGridViewTextBoxColumn1"].Value) == 1)
                        {
                            contextMenuStrip1.Tag = e.ColumnIndex;
                            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                        }
                        break;
                }
            }
        }

        private void dataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((int)ProfileProvider.GetCategory((int)dataGridView2.Rows[e.RowIndex].Cells["profileIdDataGridViewTextBoxColumn1"].Value) != 1)
            {
                dataGridView2.Rows[e.RowIndex].Cells["vanPriceDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGray;
                dataGridView2.Rows[e.RowIndex].Cells["roundTripPriceDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGray;
                dataGridView2.Rows[e.RowIndex].Cells["bookOfTicketsDataGridViewTextBoxColumn"].Style.BackColor = Color.LightGray;
                dataGridView2.Rows[e.RowIndex].Cells["ProgramPrice"].Style.BackColor = Color.LightGray;
                dataGridView2.Rows[e.RowIndex].Cells["vanPriceDataGridViewTextBoxColumn"].ReadOnly = true;
                dataGridView2.Rows[e.RowIndex].Cells["roundTripPriceDataGridViewTextBoxColumn"].ReadOnly = true;
                dataGridView2.Rows[e.RowIndex].Cells["bookOfTicketsDataGridViewTextBoxColumn"].ReadOnly = true;
                dataGridView2.Rows[e.RowIndex].Cells["profileIdDataGridViewTextBoxColumn1"].ReadOnly = true;
                dataGridView2.Rows[e.RowIndex].Cells["ProgramPrice"].ReadOnly = true;
            }
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView2.Rows.Count > 0 && FormProvider.CurrentRowIndex < dataGridView2.Rows.Count && FormProvider.CurrentRowIndex>=0)
            {
                dataGridView2.ClearSelection();
                dataGridView2.Rows[FormProvider.CurrentRowIndex].Cells[FormProvider.CurrentColIndex].Selected = true;
            }
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Rectangle rec = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex,true);
            Point m = dataGridView2.PointToScreen(Point.Empty);
            toolTip1.Show(e.Exception.Message, this, rec.Location.X+Math.Abs(m.X-this.Location.X)+rec.Width, rec.Location.Y+Math.Abs(m.Y-this.Location.Y)+rec.Height, 3000);
            e.ThrowException = false;
            e.Cancel = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button10.Enabled = true;
            CurrentDay.ChangeDate(CurrentDay.Date.AddDays(-1));
            monthCalendar1.SelectionStart = CurrentDay.Date;
            daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
            textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
            textBox_date.Text = CurrentDay.Date.ToShortDateString();
            ReCountTotals();
            CheckDayLock();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CurrentDay.ChangeDate(CurrentDay.Date.AddDays(1));
            daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
            monthCalendar1.SelectionStart = CurrentDay.Date;
            textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
            textBox_date.Text = CurrentDay.Date.ToShortDateString();
            if (CurrentDay.Date == DateTime.Now.Date)
            {
                button10.Enabled = false;
            }
            ReCountTotals();
            CheckDayLock();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            panel5.Location = new Point(dataGridView2.Location.X + 4, dataGridView2.Location.Y +dataGridView2.Height-panel5.Height-22);
            panel5.Visible = true;
            
            switch (Session.Group)
            {
                case 1:
                    profilesBindingSource1.RemoveFilter();
                    profilesBindingSource1.Filter = "Category<4";
                    break;
                case 2:
                    profilesBindingSource1.Filter = "Category<2";
                    break;
                case 3:
                    profilesBindingSource1.Filter = "Category=1";
                    break;
            }
        }

        private void toolStripTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox4.Text.Length > 2)
            {
                switch (Session.Group)
                {
                    case 1:
                        profilesBindingSource1.Filter = String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox4.Text);
                        break;
                    case 2:
                        profilesBindingSource1.Filter = "(Category<2 OR Category>2) AND "+String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox4.Text);;
                        break;
                    case 3:
                        profilesBindingSource1.Filter = "Category=1 AND "+String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox4.Text);;
                        break;
                }
                
            }
            else
            {
                switch (Session.Group)
                {
                    case 1:
                        profilesBindingSource1.RemoveFilter();
                        break;
                    case 2:
                        profilesBindingSource1.Filter = "Category<2 OR Category>2";
                        break;
                    case 3:
                        profilesBindingSource1.Filter = "Category=1";
                        break;
                }
            }
           
        }

        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            toolStripButton6_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                DayItem Item = new DayItem((int)dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value);

                if (!CurrentDay.IsInDay(Item))
                {
                    CurrentDay.InsertItem(Item);
                    daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                    panel5.Visible = false;
                    ReCountTotals();
                }
                else
                {
                    panel5.Visible = false;
                    foreach (DataGridViewRow Row in dataGridView2.Rows)
                    {
                        if ((int)Row.Cells["profileIdDataGridViewTextBoxColumn1"].Value == Item.ProfileId)
                        {
                            Row.Cells["profileIdDataGridViewTextBoxColumn1"].Selected = true;
                        }
                    }
                }
            }
            
        }

        private void ReCountTotals()
        {
            int TotalLC = 0;
            double TotalLCP = 0;
            double TotalTOP = 0;
            double TotalMisoP = 0;
            double TotalVan = 0;
            double TotalP = 0;
            double TotalRTP = 0;
            double TotalBFT = 0;
            double TotalT = 0;
            foreach(DataGridViewRow Row in dataGridView2.Rows)
            {
                if ((bool)Row.Cells["lunchDataGridViewCheckBoxColumn"].Value) { TotalLC++; }
                TotalLCP += Convert.ToDouble(Row.Cells["lunchPriceDataGridViewTextBoxColumn"].Value.ToString());
                TotalTOP += Convert.ToDouble(Row.Cells["takeOutPriceDataGridViewTextBoxColumn"].Value.ToString());
                TotalMisoP += Convert.ToDouble(Row.Cells["miscellaneousPriceDataGridViewTextBoxColumn"].Value.ToString());
                TotalVan += Convert.ToDouble(Row.Cells["vanPriceDataGridViewTextBoxColumn"].Value.ToString());
                TotalP += Convert.ToDouble(Row.Cells["ProgramPrice"].Value.ToString());
                TotalRTP += Convert.ToDouble(Row.Cells["roundTripPriceDataGridViewTextBoxColumn"].Value.ToString());
                TotalBFT += Convert.ToDouble(Row.Cells["bookOfTicketsDataGridViewTextBoxColumn"].Value.ToString());
                TotalT += Convert.ToDouble(Row.Cells["Total"].Value.ToString());
            }
            toolStripTextBox6.Text = dataGridView2.Rows.Count.ToString();
            toolStripTextBox7.Text = TotalLC.ToString();
            toolStripTextBox8.Text = TotalLCP.ToString();
            toolStripTextBox9.Text = TotalTOP.ToString();
            toolStripTextBox15.Text = TotalMisoP.ToString();
            toolStripTextBox14.Text = TotalP.ToString();
            toolStripTextBox13.Text = TotalVan.ToString();
            toolStripTextBox12.Text = TotalRTP.ToString();
            toolStripTextBox11.Text = TotalBFT.ToString();
            toolStripTextBox16.Text = TotalT.ToString();
        }

        private void noChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contextMenuStrip1.Tag != null)
            {
                switch ((int)contextMenuStrip1.Tag)
                {
                    case 3:
                        DayItem Item = new DayItem((int)dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                        Item.LunchPrice = 0;
                        Item.Lunch = false;
                        Item.Update();
                        daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                        break;
                    case 6:
                        Item = new DayItem((int)dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                        Item.ProgramPrice = 0;
                        Item.Update();
                        daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                        break;
                    case 8:
                        Item = new DayItem((int)dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["dayIdDataGridViewTextBoxColumn"].Value, CurrentDay.Date);
                        Item.RoundTripPrice = 0;
                        Item.Update();
                        daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                        break;
                }
            }
        }

        private void permaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch ((int)contextMenuStrip1.Tag)
            {
                case 3:
                    servicesTableAdapter.Fill(tDayDataSet.Services);
                    DataRow Row = tDayDataSet.Services.FindByServiceId(1);
                    Row["ServiceFee"] = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                    servicesTableAdapter.Update(tDayDataSet);
                    foreach (DataRow Dat in tDayDataSet.Days)
                    {
                        if ((bool)Dat["Lunch"])
                        {
                            Dat["LunchPrice"] = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
                        }
                    }
                    break;
                case 6:
                    servicesTableAdapter.Fill(tDayDataSet.Services);
                    Row = tDayDataSet.Services.FindByServiceId(3);
                    Row["ServiceFee"] = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[6].Value.ToString();
                    servicesTableAdapter.Update(tDayDataSet);
                    foreach (DataRow Dat in tDayDataSet.Days)
                    {
                        if (ProfileProvider.GetCategory((int)Dat["ProfileId"]) == 1)
                        {
                            Dat["ProgramPrice"] = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[6].Value.ToString();
                        }
                    }
                    break;
                case 8:
                    servicesTableAdapter.Fill(tDayDataSet.Services);
                    Row = tDayDataSet.Services.FindByServiceId(2);
                    Row["ServiceFee"] = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[8].Value.ToString();
                    servicesTableAdapter.Update(tDayDataSet);
                    foreach (DataRow Dat in tDayDataSet.Days)
                    {
                        if (ProfileProvider.GetCategory((int)Dat["ProfileId"]) == 1)
                        {
                            Dat["RoundTripPrice"] = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[8].Value.ToString();
                        }
                    }
                    break;
            }
            daysTableAdapter.Update(tDayDataSet);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PdfPrinter.PrintClientInfo((int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["profileIdDataGridViewTextBoxColumn"].Value);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PdfPrinter.PrintAttendance(CurrentDay);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                label5.Visible = true;
                textBox_ClientHD.Visible = true;
            }
            else
            {
                label5.Visible = false;
                textBox_ClientHD.Visible = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton4.Checked)
            {
                label5.Visible = true;
                textBox_ClientHD.Visible = true;
            }
            else
            {
                label5.Visible = false;
                textBox_ClientHD.Visible = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            transportationBindingSource.Filter = "Monday=1";
            mondayDataGridViewCheckBoxColumn.Visible = false;
            tuesdayDataGridViewCheckBoxColumn.Visible = false;
            wednesdayDataGridViewCheckBoxColumn.Visible = false;
            thursdayDataGridViewCheckBoxColumn.Visible = false;
            fridayDataGridViewCheckBoxColumn.Visible = false;
            _TransportDay = "Monday";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            transportationBindingSource.RemoveFilter();
            mondayDataGridViewCheckBoxColumn.Visible = true;
            tuesdayDataGridViewCheckBoxColumn.Visible = true;
            wednesdayDataGridViewCheckBoxColumn.Visible = true;
            thursdayDataGridViewCheckBoxColumn.Visible = true;
            fridayDataGridViewCheckBoxColumn.Visible = true;
            _TransportDay = "Master";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            transportationBindingSource.Filter = "Tuesday=1";
            mondayDataGridViewCheckBoxColumn.Visible = false;
            tuesdayDataGridViewCheckBoxColumn.Visible = false;
            wednesdayDataGridViewCheckBoxColumn.Visible = false;
            thursdayDataGridViewCheckBoxColumn.Visible = false;
            fridayDataGridViewCheckBoxColumn.Visible = false;
            _TransportDay = "Tuesday";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            transportationBindingSource.Filter = "Wednesday=1";
            mondayDataGridViewCheckBoxColumn.Visible = false;
            tuesdayDataGridViewCheckBoxColumn.Visible = false;
            wednesdayDataGridViewCheckBoxColumn.Visible = false;
            thursdayDataGridViewCheckBoxColumn.Visible = false;
            fridayDataGridViewCheckBoxColumn.Visible = false;
            _TransportDay = "Wednesday";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            transportationBindingSource.Filter = "Thursday=1";
            mondayDataGridViewCheckBoxColumn.Visible = false;
            tuesdayDataGridViewCheckBoxColumn.Visible = false;
            wednesdayDataGridViewCheckBoxColumn.Visible = false;
            thursdayDataGridViewCheckBoxColumn.Visible = false;
            fridayDataGridViewCheckBoxColumn.Visible = false;
            _TransportDay = "Thursday";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            transportationBindingSource.Filter = "Friday=1";
            mondayDataGridViewCheckBoxColumn.Visible = false;
            tuesdayDataGridViewCheckBoxColumn.Visible = false;
            wednesdayDataGridViewCheckBoxColumn.Visible = false;
            thursdayDataGridViewCheckBoxColumn.Visible = false;
            fridayDataGridViewCheckBoxColumn.Visible = false;
            _TransportDay = "Friday";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            PdfPrinter.PrintTransportation(_TransportDay);
        }

        private void CheckPermission()
        {
            switch (Session.Group)
            {
                case 1:
                    toolStripDropDownButton2.Visible = true;
                    break;
                case 2:
                    button4.Enabled = false;
                    toolStripComboBox1.Items.RemoveAt(2);
                    break;
                case 3:
                    dataGridView2.DataSource = null;
                    toolStripComboBox1.Items.Clear();
                    toolStripComboBox1.Items.Add("Client");
                    toolStripComboBox1.SelectedIndex = 0;
                    button1.Enabled = false;
                    button4.Enabled = false;
                    tabControl1.SelectedTab = Profiles;
                    break;
            }
        }

        private void usersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Users src = new Users();
            src.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (panel6.Visible)
            {
                panel6.Visible = false;

            }
            else
            {
                panel6.Location = new Point(button19.Location.X, dataGridView2.Location.Y + dataGridView2.Height - panel6.Height);
                monthCalendar1.MaxDate = DateTime.Now.Date;
                panel6.Visible = true;
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (e.Start == DateTime.Now.Date)
            {
                button10.Enabled = false;
                CurrentDay.ChangeDate(CurrentDay.Date = monthCalendar1.SelectionStart);
                daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
                textBox_date.Text = CurrentDay.Date.ToShortDateString();
                ReCountTotals();
                panel6.Visible = false;
                CheckDayLock();
            }
            else
            {
                button10.Enabled = true;
                CurrentDay.ChangeDate(CurrentDay.Date = monthCalendar1.SelectionStart);
                daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
                textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
                textBox_date.Text = CurrentDay.Date.ToShortDateString();
                ReCountTotals();
                panel6.Visible = false;
                CheckDayLock();
            }
        }

        private void CheckDayLock()
        {
            switch (Session.Group)
            {
                case 2:
                    if (CurrentDay.Date != DateTime.Now.Date)
                    {
                        dataGridView2.Enabled = false;
                        toolStripButton5.Enabled = false;
                    }
                    else
                    {
                        dataGridView2.Enabled = true;
                        toolStripButton5.Enabled = true;
                    }
                    break;
                case 3:
                    if (CurrentDay.Date != DateTime.Now.Date)
                    {
                        dataGridView2.Enabled = false;
                        toolStripButton5.Enabled = false;
                    }
                    else
                    {
                        dataGridView2.Enabled = true;
                        toolStripButton5.Enabled = true;
                    }
                    break;
            }
        }

        private void MainFrame_Shown(object sender, EventArgs e)
        {
            this.Opacity = 100;
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BillsItem Item = new BillsItem((int)dataGridView5.Rows[e.RowIndex].Cells["billIdDataGridViewTextBoxColumn"].Value);
                richTextBox2.ResetText();
                richTextBox2.AppendText(String.Format("INVOICE #{0}", dataGridView5.Rows[e.RowIndex].Cells["billIdDataGridViewTextBoxColumn"].Value.ToString())+"\n");
                richTextBox2.AppendText(String.Format("For Services of  {0:MMMM}", Item.Date));
                richTextBox3.ResetText();
                richTextBox3.AppendText(String.Format("{0} \n", Item.Profile.Name));
                richTextBox3.AppendText(String.Format("{0} \n",Item.Profile.Adress.Addres));
                richTextBox3.AppendText(String.Format("{0} \n", Item.Profile.Adress.City));
                richTextBox3.AppendText(String.Format("{0} \n", Item.Profile.Adress.PostalCode));
                if (Item.PreviousBillTotal > Item.PreviousBillPaid)
                {
                    label67.Text = "Overdue";
                    textBox2.Text = Item.PreviousBillPaid.ToString();
                    textBox6.Text = (Item.PreviousBillTotal - Item.PreviousBillPaid + Item.BillTotal).ToString();
                }
                else
                {
                    label67.Text = "Payments/Credits";
                    textBox2.Text = Item.PreviousBillPaid.ToString();
                    textBox6.Text = (Item.PreviousBillTotal - Item.PreviousBillPaid + Item.BillTotal).ToString();
                }
                if (Item.Paid > 0)
                {
                    checkBox1.Checked = true;
                    groupBox25.Enabled = true;
                    switch (Item.PaidType)
                    {
                        case "cash":
                            radioButton6.Checked = true;
                            break;
                        case "cheque":
                            radioButton7.Checked = true;
                            break;
                    }
                    textBox5.Text = Item.Paid.ToString();
                    dateTimePicker1.Value = Item.PaidDate;
                }
                else
                {
                    checkBox1.Checked = false;
                    groupBox25.Enabled = false;
                    textBox5.Text = String.Empty;
                    dateTimePicker1.Value = DateTime.Now.Date;
                }
                textBox1.Text = Item.PreviousBillTotal.ToString();
                textBox3.Text = Item.BillTotal.ToString();
                textBox4.Text = Item.PreviousBillPaidDate.ToShortDateString();
                //daysBindingSource2.RemoveFilter();
                daysBindingSource2.Filter = String.Format("ProfileId = {0}", Item.ProfileIdBills.ToString());
                RecountTotals();
            }
        }
        private void RecountTotals()
        {
            double Misc = 0;
            double Trans = 0;
            double Program = 0;
            double TO = 0;
            double Lanch = 0;
            double Van = 0;
            double BFT = 0;
            foreach (DataGridViewRow Row in dataGridView6.Rows)
            {
                Misc += Double.Parse(Row.Cells["miscellaneousPriceDataGridViewTextBoxColumn1"].Value.ToString());
                Trans += Double.Parse(Row.Cells["roundTripPriceDataGridViewTextBoxColumn1"].Value.ToString());
                Program += Double.Parse(Row.Cells["programPriceDataGridViewTextBoxColumn"].Value.ToString());
                TO += Double.Parse(Row.Cells["takeOutPriceDataGridViewTextBoxColumn1"].Value.ToString());
                Lanch += Double.Parse(Row.Cells["lunchPriceDataGridViewTextBoxColumn1"].Value.ToString());
                Van += Double.Parse(Row.Cells["VanPrice"].Value.ToString());
                BFT += Double.Parse(Row.Cells["BookOfTickets"].Value.ToString());
            }
            toolStripTextBox19.Text = (Misc+Trans+Program+TO+Lanch+Van+BFT).ToString();
            toolStripTextBox20.Text = Lanch.ToString();
            toolStripTextBox21.Text = TO.ToString();
            toolStripTextBox22.Text = Misc.ToString();
            toolStripTextBox23.Text = Program.ToString();
            toolStripTextBox24.Text = Trans.ToString();
            toolStripTextBox25.Text = Van.ToString();
            toolStripTextBox26.Text = BFT.ToString();
        }

        private void toolStrip15_MouseHover(object sender, EventArgs e)
        {
            toolStripButton10.Visible = true;
        }

        private void toolStrip15_MouseLeave(object sender, EventArgs e)
        {
            if (!richTextBox1.Enabled)
            {
                toolStripButton10.Visible = false;
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (!richTextBox1.Enabled)
            {
                richTextBox1.Enabled = true;
                toolStripButton10.Image = TDay.Properties.Resources.Save;
                toolStripButton10.Text = "Save";
            }
            else
            {
                TDay.Properties.Settings.Default.PlantString = richTextBox1.Text;
                TDay.Properties.Settings.Default.Save();
                toolStripButton10.Image = TDay.Properties.Resources.Edit;
                toolStripButton10.Text = "Edit Item";
                richTextBox1.Enabled = false;

            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            panel8.Visible = false;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            panel8.Location = new Point(dataGridView5.Location.X + 4, dataGridView5.Location.Y + dataGridView5.Height - panel8.Height - 22);
            panel8.Visible = true;
           // profilesTableAdapter.FillAll(tDayDataSet.Profiles);
            profilesBindingSource3.Filter = "Category<4 AND DelStatus=0";
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedCells.Count > 0)
            {
                TDayDataSet tempSet = new TDayDataSet();
                Bill _Bill = new Bill();
                billsTableAdapter.FillByMonth(tempSet.Bills, _Bill.FirstDayOfMonth, _Bill.LastDayOfMonth);
                int Index = -1;
                foreach (DataRow Row in tempSet.Bills)
                {
                    if ((int)Row["ProfileId"] == (int)dataGridView7.Rows[dataGridView7.SelectedCells[0].RowIndex].Cells["dataGridViewTextBoxColumn1"].Value)
                    {
                        Index = (int)Row["BillId"];
                            break;
                    }
                }
                if (Index != -1)
                {
                    panel8.Visible = false;
                    foreach (DataGridViewRow Row in dataGridView5.Rows)
                    {
                        if ((int)Row.Cells["billIdDataGridViewTextBoxColumn"].Value == Index)
                        {
                            DataGridViewCellEventArgs eve = new DataGridViewCellEventArgs(0, Row.Index);
                            dataGridView5_CellClick(sender, eve);
                            dataGridView5.Rows[Row.Index].Selected = true;
                            break;
                        }
                    }
                }
                else
                {
                    BillsItem item = new BillsItem((int)dataGridView7.Rows[dataGridView7.SelectedCells[0].RowIndex].Cells["dataGridViewTextBoxColumn1"].Value, _Bill.FirstDayOfMonth, _Bill.LastDayOfMonth);
                    item.Insert();
                    billsTableAdapter.Fill(tDayDataSet.Bills);
                    panel8.Visible = false;
                    dataGridView7.Rows[dataGridView7.Rows.Count - 1].Selected = true;
                }

            }
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this entry?","Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question))
            if (dataGridView5.SelectedCells.Count > 0)
            {
                billsTableAdapter.Delete((int)dataGridView5.Rows[dataGridView5.SelectedCells[0].RowIndex].Cells["billIdDataGridViewTextBoxColumn"].Value);
                billsTableAdapter.Fill(tDayDataSet.Bills);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                groupBox25.Enabled = true;
            }
            else
            {
                groupBox25.Enabled = false;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedCells.Count > 0)
            {
                BillsItem Item = new BillsItem((int)dataGridView5.Rows[dataGridView5.SelectedCells[0].RowIndex].Cells["billIdDataGridViewTextBoxColumn"].Value);
                Decimal Paid = Decimal.Zero;
                Decimal.TryParse(textBox5.Text, out Paid);
                Item.Paid = Paid;
                Item.PaidDate = dateTimePicker1.Value;
                if (radioButton6.Checked)
                {
                    Item.PaidType = "cash";
                }
                if (radioButton7.Checked)
                {
                    Item.PaidType = "cheque";
                }
                Item.Update();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedCells.Count > 0)
            {
                PdfPrinter.PrintBills((int)dataGridView5.Rows[dataGridView5.SelectedCells[0].RowIndex].Cells["billIdDataGridViewTextBoxColumn"].Value);
            }
        }

        private void toolStripTextBox17_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox17.Text.Length > 2)
            {
                string Filter = String.Empty;
                foreach (DataGridViewRow Row in dataGridView5.Rows)
                {
                    if (Row.Cells["profileIdDataGridViewTextBoxColumn4"].EditedFormattedValue.ToString().IndexOf(toolStripTextBox17.Text) != -1)
                    {
                        Filter += "ProfileId =" + Row.Cells["profileIdDataGridViewTextBoxColumn4"].Value.ToString() + " OR ";
                    }
                }
                if (Filter.Length > 0)
                {
                    billsBindingSource.Filter = Filter.Remove(Filter.Length - 3);
                }
            }
            else
            {
                billsBindingSource.RemoveFilter();
            }
        }

        private void toolStripTextBox27_TextChanged(object sender, EventArgs e)
        {
            string TempFilter = "Category<4";
            if(toolStripTextBox27.Text.Length>2)
            {
                profilesBindingSource3.Filter = TempFilter+" AND "+String.Format("Name LIKE '%{0}%'",toolStripTextBox27.Text);
            } else 
            {
                profilesBindingSource3.Filter = "Category<4";
            }
        }

        private void dataGridView5_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

       
    }
}
