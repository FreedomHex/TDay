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
                this.Text = "TDay" + " Version:" + TDay.Properties.Settings.Default.CurrentVersion + "|DebugMode";
            }
            else
            {
                this.Text = "TDay";
            }
        }
        public bool SortByCategory = false;
        public string CategoryFilter = String.Empty;
        public Day CurrentDay = new Day();
        TDayDataSetTableAdapters.ServicesTableAdapter servicesTableAdapter = new TDayDataSetTableAdapters.ServicesTableAdapter();
        private void MainFrame_Load(object sender, EventArgs e)
        {
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
        }

        private void RelDataGrid2(object sender, int RowIndex)
        {
            this.profilesTableAdapter.Fill(this.tDayDataSet.Profiles);
            if (dataGridView1.Rows.Count > 0)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button8.Enabled = true;
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
                    if (client.DopEmergencyContact!=null)
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
                    SortByCategory = false;
                    this.profilesBindingSource.RemoveFilter();
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
                    profilesBindingSource.Filter = CategoryFilter + " AND " + String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                }
                else
                {
                    profilesBindingSource.Filter = String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox1.Text);
                }
            }
            else
            {
                if (SortByCategory)
                {
                    profilesBindingSource.Filter = CategoryFilter;
                }
                else
                {
                    profilesBindingSource.RemoveFilter();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
                     Profile client = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                     if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete a profile "+client.Name+" ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            client.Delete();
                            profilesTableAdapter.Fill(tDayDataSet.Profiles);
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
                     client.Adress.Addres=textBox_ClientAdress.Text;
                     client.Adress.City=textBox_ClientCity.Text;
                     client.Adress.Province=textBox_ClientProvince.Text;
                     client.Adress.Country=textBox_ClientCountry.Text;
                     client.Adress.PostalCode=textBox_ClientPostal.Text;
                     client.Adress.Phone=textBox_ClientPhone.Text;
                     client.Adress.Email=textBox_ClientEmail.Text;
                     client.EmergencyContact.Name=textBox_ClientEmerName.Text;
                     client.EmergencyContact.Phone=textBox_ClientEmPhone.Text;
                     client.EmergencyContact.Relation=textBox_ClientRelation.Text;
                     client.Transportation.HandyDARTNumber=textBox_ClientHD.Text;
                     client.DoctorName=textBox_ClientDocName.Text;
                     client.DoctorPhone=textBoxClientDocPhone.Text;
                     client.Own = radioButton4.Checked;
                     client.PharmacistName=textBox_ClientPharmName.Text;
                     client.PharmacistPhone=textBox_ClientPharmPhone.Text;
                     client.Member=ClientMember.Checked;
                     if (DopEmerClientName.Visible && client.DopEmergencyContact == null)
                     {
                         client.DopEmergencyContact = new EmergencyContact();
                         client.DopEmergencyContact.Name = DopEmerClientName.Text;
                         client.DopEmergencyContact.Phone = DopEmerClientPhone.Text;
                         client.DopEmergencyContact.Relation = toolStripTextBox2.Text;
                         client.DopEmergencyContact.AddEmergencyContactTo(client);
                     }
                     else if(DopEmerClientName.Visible)
                     {
                         client.DopEmergencyContact.Name = DopEmerClientName.Text;
                         client.DopEmergencyContact.Phone = DopEmerClientPhone.Text;
                         client.DopEmergencyContact.Relation = toolStripTextBox2.Text;
                     }
                     
                     client.Attendance.Monday=checkBox2.Checked;
                     client.Attendance.Tuesday=checkBox3.Checked;
                     client.Attendance.Wednesday=checkBox4.Checked;
                     client.Attendance.Thursday=checkBox5.Checked;
                     client.Attendance.Friday=checkBox6.Checked;
                     client.Transportation.Monday=checkBox11.Checked;
                     client.Transportation.Tuesday=checkBox10.Checked;
                     client.Transportation.Wednesday=checkBox9.Checked;
                     client.Transportation.Thursday=checkBox8.Checked;
                     client.Transportation.Friday = checkBox7.Checked;
                     client.Update();
                     profilesTableAdapter.Fill(tDayDataSet.Profiles);
                     dataGridView1.Rows[RowIndex].Selected = true;
                     button8.Enabled = false;
                    break;
                #endregion
                case "2":
                    Employee employee = new Employee(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                    employee.Name = textBox_EmpName.Text;
                    employee.DateOfBirdh=DateTime.Parse(textBox_EmpBirth.Value.ToShortDateString());
                    employee.HireDate=DateTime.Parse(textBox_EmpHire.Value.ToShortDateString());
                    employee.Position=textBox_EmpPosition.Text;
                    employee.SIN=textBox_EmpSin.Text;
                    if (radioButton1.Checked) { employee.PositionType = "Causal"; }
                    if (radioButton2.Checked) { employee.PositionType = "Part time"; }
                    if (radioButton3.Checked) { employee.PositionType = "Full time"; }

                    employee.Adress.Addres = textBox_EmpAdress.Text;
                    employee.Adress.City = textBox_EmpCity.Text;
                    employee.Adress.Province = textBox_EmpProvince.Text;
                    employee.Adress.Country = textBox_EmpCountry.Text;
                    employee.Adress.PostalCode = textBox_EmpPostal.Text;
                    employee.Adress.Phone = textBox_EmpPhone.Text;
                    employee.Adress.Email=textBox_EmpEmail.Text;
                    employee.Adress.Cell=textBox_EmpCell.Text;
                    employee.EmergencyContact.Name=textBox_EmpEmerCN.Text;
                    employee.EmergencyContact.Phone=textBox_EmerCP.Text;
                    employee.EmergencyContact.Relation=textBox_EmpRelation.Text;
                    employee.Attendance.Monday = checkBox16.Checked;
                    employee.Attendance.Tuesday=checkBox15.Checked;
                    employee.Attendance.Wednesday=checkBox14.Checked;
                    employee.Attendance.Thursday=checkBox13.Checked;
                    employee.Attendance.Friday=checkBox12.Checked;
                    employee.Update();
                    profilesTableAdapter.Fill(tDayDataSet.Profiles);
                    dataGridView1.Rows[RowIndex].Selected = true;
                    button8.Enabled = false;
                    break;
                case "3":
                    tabControl2.SelectedTab = VolunteerTab;
                    Profile volonteer = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                    volonteer.Name = textBox_VolName.Text;
                     volonteer.DateOfBirdh= DateTime.Parse(textBox_VolBirth.Value.ToShortDateString());
                     volonteer.Adress.Addres=textBox_VolAdress.Text;
                     volonteer.Adress.City=textBox_VolCity.Text;
                     volonteer.Adress.Province=textBox_VolProvince.Text;
                     volonteer.Adress.Country=textBox_VolCountry.Text;
                     volonteer.Adress.PostalCode=textBox_VolPostal.Text;
                     volonteer.Adress.Phone=textBox_VolPhone.Text;
                     volonteer.Adress.Email=textBox_VolEmail.Text;
                     volonteer.Adress.Cell=textBox_VolCell.Text;
                     volonteer.EmergencyContact.Name=textBox_VolEmerCN.Text;
                     volonteer.EmergencyContact.Phone=textBox_VolEmerCP.Text;
                     volonteer.EmergencyContact.Relation=textBox_VolEmerRelation.Text;
                     volonteer.Attendance.Monday=checkBox21.Checked;
                     volonteer.Attendance.Tuesday=checkBox20.Checked;
                     volonteer.Attendance.Wednesday=checkBox19.Checked;
                     volonteer.Attendance.Thursday=checkBox18.Checked;
                     volonteer.Attendance.Friday=checkBox17.Checked;
                     volonteer.Update();
                     profilesTableAdapter.Fill(tDayDataSet.Profiles);
                     dataGridView1.Rows[RowIndex].Selected = true;
                     button8.Enabled = false;
                     break;
                case "4":
                    
                    textBox_BoardOccupation.Visible = true;
                    label48.Visible = true;
                    Profile board = new Profile(Int32.Parse(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString()));
                    board.Name=textBox_BoardName.Text;
                    board.Occupation=textBox_BoardOccupation.Text;
                    board.DateOfBirdh = DateTime.Parse(textBox_BoardBirth.Value.ToShortDateString());
                    board.Adress.Addres=textBox_BoardAdress.Text;
                    board.Adress.City=textBox_BoardCity.Text;
                    board.Adress.Province=textBox_BoardProvince.Text;
                    board.Adress.Country=textBox_BoardCountry.Text;
                    board.Adress.Cell=textBox_BoardCell.Text;
                    board.Adress.PostalCode=textBox_BoardPostal.Text;
                    board.Adress.Phone=textBox_BoardPhone.Text;
                    board.Adress.Email=textBox_BoardEmail.Text;
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
                            if (DialogResult.Yes == MessageBox.Show("Are you ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
            daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
            textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
            textBox_date.Text = CurrentDay.Date.ToShortDateString();
            ReCountTotals();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CurrentDay.ChangeDate(CurrentDay.Date.AddDays(1));
            daysTableAdapter.Fill(tDayDataSet.Days, CurrentDay.Date);
            textBox_weekday.Text = CurrentDay.Date.DayOfWeek.ToString();
            textBox_date.Text = CurrentDay.Date.ToShortDateString();
            if (CurrentDay.Date == DateTime.Now.Date)
            {
                button10.Enabled = false;
            }
            ReCountTotals();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            panel5.Location = new Point(dataGridView2.Location.X + 4, dataGridView2.Location.Y +dataGridView2.Height-panel5.Height-22);
            panel5.Visible = true;
        }

        private void toolStripTextBox4_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox4.Text.Length > 2)
            {
                profilesBindingSource1.Filter = String.Format("CONVERT({0},'System.String') LIKE '%{1}%'", "Name", toolStripTextBox4.Text);
            }
            else
            {
                profilesBindingSource1.RemoveFilter();
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
            PdfPrinter.PrintClientInfo((int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["profileIdDataGridViewTextBoxColumn"].Value);
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

       
    }
}
