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
            toolStripTextBox_EmName.Visible = true;
            toolStripTextBox_EmPhone.Visible = true;
            toolStripLabel3.Visible = true;
            toolStripTextBox1.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxCategory.SelectedIndex)
            {
                case 0:
                    tabControl1.SelectedTab = ClientTab;
                    break;
                case 1:
                    tabControl1.SelectedTab = EmployeeTab;
                    break;
                case 2:
                    tabControl1.SelectedTab = VolunteerTab;
                    break;
                case 3:
                    tabControl1.SelectedTab = Board_Member;
                    textBox_BorOccupation.Visible = true;
                    label60.Visible = true;
                    break;
                case 4:
                    tabControl1.SelectedTab = Board_Member;
                    textBox_BorOccupation.Visible = false;
                    label60.Visible = false;
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBoxCategory.SelectedValue.ToString())
            {
                #region Client
                case "1":
                    Client _Client = new Client();
                    _Client.Name = textBox_ClientName.Text;
                    _Client.DateOfBirdh = DateTime.Parse(textBox_ClientBirth.Text);
                    _Client.Member = checkBox_ClientMebmber.Checked;
                    _Client.ParisNumber = textBox_ClientParis.Text;
                    _Client.DoctorName = textBox_ClientDocName.Text;
                    _Client.DoctorPhone = textBox_ClientDocPhone.Text;
                    _Client.Create();
                    Address address = new Address();
                    address.Addres = textBox_ClientAddress.Text;
                    address.City = textBox_ClientCity.Text;
                    address.Province = textBox_ClientProvince.Text;
                    address.Country = textBox_ClientCountry.Text;
                    address.PostalCode = textBox_ClientPostal.Text;
                    address.Phone = textBox_ClientPhone.Text;
                    address.Email = textBox_ClientEmail.Text;
                    address.AddAdressTo(_Client);
                    address.Dispose();
                    Attendance attendance = new Attendance();
                    attendance.Monday = attendance_mon.Checked;
                    attendance.Tuesday = attendance_tue.Checked;
                    attendance.Wednesday = attendance_wed.Checked;
                    attendance.Thursday = attendance_thu.Checked;
                    attendance.Friday = attendance_fri.Checked;
                    attendance.AddAttendanceTo(_Client);
                    Transportation trans = new Transportation();
                    trans.Monday = trans_mon.Checked;
                    trans.Tuesday = trans_tue.Checked;
                    trans.Wednesday = trans_wed.Checked;
                    trans.Thursday = trans_thu.Checked;
                    trans.Friday = trans_fri.Checked;
                    trans.HandyDARTNumber = textBox_ClientHD.Text;
                    trans.AddTransportationTo(_Client);
                    EmergencyContact Contact = new EmergencyContact();
                    Contact.Name = textBox_ClientEmerName.Text;
                    Contact.Phone = textBox_ClientEmerPhone.Text;
                    Contact.Relation = textBox_ClientRelation.Text;
                    Contact.AddEmergencyContactTo(_Client);
                    Contact.Dispose();
                    if (toolStripTextBox_EmName.Visible)
                    {
                        EmergencyContact DopCont = new EmergencyContact();
                        DopCont.Name = toolStripTextBox_EmName.Text;
                        DopCont.Phone = toolStripTextBox_EmPhone.Text;
                        DopCont.Relation = toolStripTextBox1.Text;
                        DopCont.AddEmergencyContactTo(_Client);
                        DopCont.Dispose();
                    }
                    ReLoad(sender, e);
                    this.Close();
                    break;
                #endregion
                #region Employee
                case "2":
                    Employee employee = new Employee();
                    employee.Name = textBox_EmpName.Text;
                    employee.DateOfBirdh = DateTime.Parse(textBox_EmpBirth.Text);
                    employee.HireDate = DateTime.Parse(textBox_EmpHireDate.Text);
                    employee.SIN = textBox_EmpSin.Text;
                    employee.Position = textBox_EmpPosition.Text;
                    employee.PositionType = GetPositionType();
                    employee.Create();
                    Address address_emp = new Address();
                    address_emp.Addres = textBox_EmpAddress.Text;
                    address_emp.City = textBox_EmpCity.Text;
                    address_emp.Province = textBox_EmpProv.Text;
                    address_emp.Country = textBox_EmpCounrty.Text;
                    address_emp.PostalCode = textBox_EmpPostal.Text;
                    address_emp.Phone = textBox_EmpPhone.Text;
                    address_emp.Email = textBox_EmpEmail.Text;
                    address_emp.Cell = textBox_EmpCell.Text;
                    address_emp.AddAdressTo(employee);
                    address_emp.Dispose();
                    EmergencyContact ContactEmp = new EmergencyContact();
                    ContactEmp.Name = textBox_EpEmerName.Text;
                    ContactEmp.Phone = textBox_EmpEmerPhone.Text;
                    ContactEmp.Relation = textBox_EmpRelation.Text;
                    ContactEmp.AddEmergencyContactTo(employee);
                    ContactEmp.Dispose();
                    Attendance attendance_emp = new Attendance();
                    attendance_emp.Monday = attendance_em_mon.Checked;
                    attendance_emp.Tuesday = attendance_tue.Checked;
                    attendance_emp.Wednesday = attendance_em_wed.Checked;
                    attendance_emp.Thursday = attendance_em_thu.Checked;
                    attendance_emp.Friday = attendance_em_fri.Checked;
                    attendance_emp.AddAttendanceTo(employee);
                    attendance_emp.Dispose();
                    ReLoad(sender, e);
                    this.Close();
                    break;
                #endregion
                #region Volonteer
                case "3":
                    Profile volonteer = new Profile();
                    volonteer.Name = textBox_VolName.Text;
                    volonteer.DateOfBirdh = DateTime.Parse(textBox_VolBirth.Text);
                    volonteer.Create(Enums.Category.Volunteer);
                    Address adress_vol = new Address();
                    adress_vol.Addres = textBox_VolAdress.Text;
                    adress_vol.City = textBox_VolCity.Text;
                    adress_vol.Province = textBox_VolProvince.Text;
                    adress_vol.Country = textBox_VolCountry.Text;
                    adress_vol.PostalCode = textBox_VolPostal.Text;
                    adress_vol.Phone = textBox_VolPhone.Text;
                    adress_vol.Email = textBox_ValEmail.Text;
                    adress_vol.Cell = textBox_VolCell.Text;
                    adress_vol.AddAdressTo(volonteer);
                    adress_vol.Dispose();
                    EmergencyContact ContactVol = new EmergencyContact();
                    ContactVol.Name = textBox_VolEmeName.Text;
                    ContactVol.Phone = textBox_VolEmePhone.Text;
                    ContactVol.Relation = textBox_VolRelation.Text;
                    ContactVol.AddEmergencyContactTo(volonteer);
                    ContactVol.Dispose();
                    Attendance attendance_vol = new Attendance();
                    attendance_vol.Monday = attendance_vol_mon.Checked;
                    attendance_vol.Tuesday = attendance_vol_tue.Checked;
                    attendance_vol.Wednesday = attendance_vol_wed.Checked;
                    attendance_vol.Thursday = attendance_vol_thu.Checked;
                    attendance_vol.Friday = attendance_vol_fri.Checked;
                    attendance_vol.AddAttendanceTo(volonteer);
                    attendance_vol.Dispose();
                    ReLoad(sender, e);
                    this.Close();
                    break;
                #endregion
                #region BoardMember
                case "4":
                    Profile board = new Profile();
                    board.Name = textBox_BorName.Text;
                    board.Occupation = textBox_BorOccupation.Text;
                    board.DateOfBirdh = DateTime.Parse(textBox_BorBirth.Text);
                    board.Create(Enums.Category.BoardMember);
                    Address adress_bor = new Address();
                    adress_bor.Addres = textBox_BorAdress.Text;
                    adress_bor.City = textBox_BorCity.Text;
                    adress_bor.Province = textBox_BorProvince.Text;
                    adress_bor.Country = textBox_BorCountry.Text;
                    adress_bor.PostalCode = textBox_BorPostal.Text;
                    adress_bor.Phone = textBox_BorPhone.Text;
                    adress_bor.Email = textBox_BoeEmail.Text;
                    adress_bor.Cell = textBox_BorCell.Text;
                    adress_bor.AddAdressTo(board);
                    adress_bor.Dispose();
                    ReLoad(sender, e);
                    this.Close();
                    break;
                #endregion
                #region Other
                case "5":
                    Profile other = new Profile();
                    other.Name = textBox_BorName.Text;
                    other.Occupation = textBox_BorOccupation.Text;
                    other.DateOfBirdh = DateTime.Parse(textBox_BorBirth.Text);
                    other.Create(Enums.Category.Other);
                    Address adress_other = new Address();
                    adress_other.Addres = textBox_BorAdress.Text;
                    adress_other.City = textBox_BorCity.Text;
                    adress_other.Province = textBox_BorProvince.Text;
                    adress_other.Country = textBox_BorCountry.Text;
                    adress_other.PostalCode = textBox_BorPostal.Text;
                    adress_other.Phone = textBox_BorPhone.Text;
                    adress_other.Email = textBox_BoeEmail.Text;
                    adress_other.Cell = textBox_BorCell.Text;
                    adress_other.AddAdressTo(other);
                    adress_other.Dispose();
                    ReLoad(sender, e);
                    this.Close();
                    break;
                #endregion


            }
        }

        private string GetPositionType()
        {
            string PosType = String.Empty;
            if (radioButton1.Checked)
            {
                PosType = "Causal";
            }
            if (radioButton2.Checked)
            {
                PosType = "Part time";
            }
            if (radioButton3.Checked)
            {
                PosType = "Full time";
            }
            return PosType;
        }

        private static void ReLoad(object sender, EventArgs e)
        {
            Form mainForm = Application.OpenForms["MainFrame"];
            if (mainForm != null)
            {
                MethodInfo form1_Load = mainForm.GetType().GetMethod("MainFrame_Load", BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);
                form1_Load.Invoke(mainForm, new object[] { sender, e });
            }
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
                        AddProfileErrorProvider.SetError(Box, "Invalid date format \nThe date must be in the format (dd.mm.yyyy) \nand contain only numbers included in the date range");
                    }
                    else
                    {
                        AddProfileErrorProvider.Clear();
                    }
                    break;
            }
        }
    }
}
