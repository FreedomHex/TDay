using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;

namespace TDay
{
    public static class PdfPrinter
    {
        public static void PrintClientInfo(int ProfileId)
        {
            switch(ProfileProvider.GetCategory(ProfileId))
            {
                #region Client
                case 1:
            Client client = new Client(ProfileId);
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Report_" + ProfileId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath+@"\Report_"+ProfileId.ToString()+".pdf",FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document(PageSize.A4,20,20,20,20);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            AddHeader(table, "Profile Card", 20, new BaseColor(Color.DimGray),new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Category:");
            AddValueCell(table, "Client");
            AddPriviewCell(table, "Name:");
            AddValueCell(table, client.Name);
            AddPriviewCell(table, "Date of Birth:");
            AddValueCell(table, client.DateOfBirdh.ToShortDateString());
            AddPriviewCell(table, "Member:");
            AddValueCell(table, client.Member,true);
            AddPriviewCell(table, "PARIS Number:");
            AddValueCell(table, client.ParisNumber);
            AddHeader(table, "Address", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Address:");
            AddValueCell(table, client.Adress.Addres);
            AddPriviewCell(table, "City:");
            AddValueCell(table, client.Adress.City);
            AddPriviewCell(table, "Province:");
            AddValueCell(table, client.Adress.Province);
            AddPriviewCell(table, "Country:");
            AddValueCell(table, client.Adress.Country);
            AddPriviewCell(table, "Postal Code:");
            AddValueCell(table, client.Adress.PostalCode);
            AddPriviewCell(table, "Phone:");
            AddValueCell(table, client.Adress.Phone);
            AddPriviewCell(table, "Email:");
            AddValueCell(table, client.Adress.Email);
            AddHeader(table, "Emergency", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Emergency CN:");
            AddValueCell(table, client.EmergencyContact.Name);
            AddPriviewCell(table, "Emergency CP:");
            AddValueCell(table, client.EmergencyContact.Phone);
            AddPriviewCell(table, "Emergency Relation:");
            AddValueCell(table, client.EmergencyContact.Relation);
            if (client.DopEmergencyContact != null)
            {
                AddPriviewCell(table, "Emergency CN:");
                AddValueCell(table, client.DopEmergencyContact.Name);
                AddPriviewCell(table, "Emergency CP:");
                AddValueCell(table, client.DopEmergencyContact.Phone);
                AddPriviewCell(table, "Emergency Relation:");
                AddValueCell(table, client.DopEmergencyContact.Relation);
            }
            AddHeader(table, "Medical", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Doctor Name:");
            AddValueCell(table, client.DoctorName);
            AddPriviewCell(table, "Doctor Phone:");
            AddValueCell(table, client.DoctorPhone);
            AddPriviewCell(table, "Pharmacist Name:");
            AddValueCell(table, client.PharmacistName);
            AddPriviewCell(table, "Pharmacist Phone:");
            AddValueCell(table, client.PharmacistPhone);
            AddAttendanceCell(table, client.Attendance);
            AddTransportationCell(table, client.Transportation);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf");
            break;
                #endregion
                #region Employee
                case 2:
            Employee employee = new Employee(ProfileId);
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Report_" + ProfileId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath+@"\Report_"+ProfileId.ToString()+".pdf",FileMode.CreateNew);
            Doc = new iTextSharp.text.Document(PageSize.A4);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            AddHeader(table, "Profile Card", 20, new BaseColor(Color.DimGray),new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Category:");
            AddValueCell(table, "Employee");
            AddPriviewCell(table, "Name:");
            AddValueCell(table, employee.Name);
            AddPriviewCell(table, "Date of Birth:");
            AddValueCell(table, employee.DateOfBirdh.ToShortDateString());
            AddPriviewCell(table, "Hire Date:");
            AddValueCell(table, employee.HireDate.ToShortDateString());
            AddPriviewCell(table, "Position:");
            AddValueCell(table, employee.Position);
            AddPriviewCell(table, "Position Type:");
            AddValueCell(table, employee.PositionType);
            AddPriviewCell(table, "SIN:");
            AddValueCell(table, employee.SIN);
            AddHeader(table, "Address", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Address:");
            AddValueCell(table, employee.Adress.Addres);
            AddPriviewCell(table, "City:");
            AddValueCell(table, employee.Adress.City);
            AddPriviewCell(table, "Province:");
            AddValueCell(table, employee.Adress.Province);
            AddPriviewCell(table, "Country:");
            AddValueCell(table, employee.Adress.Country);
            AddPriviewCell(table, "Postal Code:");
            AddValueCell(table, employee.Adress.PostalCode);
            AddPriviewCell(table, "Phone:");
            AddValueCell(table, employee.Adress.Phone);
            AddPriviewCell(table, "Email:");
            AddValueCell(table, employee.Adress.Email);
            AddPriviewCell(table, "Cell:");
            AddValueCell(table, employee.Adress.Cell);
            AddHeader(table, "Emergency", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Emergency CN:");
            AddValueCell(table, employee.EmergencyContact.Name);
            AddPriviewCell(table, "Emergency CP:");
            AddValueCell(table, employee.EmergencyContact.Phone);
            AddPriviewCell(table, "Emergency Relation:");
            AddValueCell(table, employee.EmergencyContact.Relation);
            AddAttendanceCell(table, employee.Attendance);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf");
            break;
#endregion
                #region Volunteer
                case 3:
                    Profile vol = new Profile(ProfileId);
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Report_" + ProfileId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath+@"\Report_"+ProfileId.ToString()+".pdf",FileMode.CreateNew);
            Doc = new iTextSharp.text.Document(PageSize.A4);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            AddHeader(table, "Profile Card", 20, new BaseColor(Color.DimGray),new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Category:");
            AddValueCell(table, "Volunteer");
            AddPriviewCell(table, "Name:");
            AddValueCell(table, vol.Name);
            AddPriviewCell(table, "Date of Birth:");
            AddValueCell(table, vol.DateOfBirdh.ToShortDateString());
            AddHeader(table, "Address", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Address:");
            AddValueCell(table, vol.Adress.Addres);
            AddPriviewCell(table, "City:");
            AddValueCell(table, vol.Adress.City);
            AddPriviewCell(table, "Province:");
            AddValueCell(table, vol.Adress.Province);
            AddPriviewCell(table, "Country:");
            AddValueCell(table, vol.Adress.Country);
            AddPriviewCell(table, "Postal Code:");
            AddValueCell(table, vol.Adress.PostalCode);
            AddPriviewCell(table, "Phone:");
            AddValueCell(table, vol.Adress.Phone);
            AddPriviewCell(table, "Email:");
            AddValueCell(table, vol.Adress.Email);
            AddPriviewCell(table, "Cell:");
            AddValueCell(table, vol.Adress.Cell);
            AddHeader(table, "Emergency", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Emergency CN:");
            AddValueCell(table, vol.EmergencyContact.Name);
            AddPriviewCell(table, "Emergency CP:");
            AddValueCell(table, vol.EmergencyContact.Phone);
            AddPriviewCell(table, "Emergency Relation:");
            AddValueCell(table, vol.EmergencyContact.Relation);
            AddAttendanceCell(table, vol.Attendance);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf");
            break;
                #endregion
                #region Board Member
                case 4:
            Profile board = new Profile(ProfileId);
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Report_" + ProfileId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath+@"\Report_"+ProfileId.ToString()+".pdf",FileMode.CreateNew);
            Doc = new iTextSharp.text.Document(PageSize.A4);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            AddHeader(table, "Profile Card", 20, new BaseColor(Color.DimGray),new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Category:");
            AddValueCell(table, "Board Member");
            AddPriviewCell(table, "Name:");
            AddValueCell(table, board.Name);
            AddPriviewCell(table, "Date of Birth:");
            AddValueCell(table, board.DateOfBirdh.ToShortDateString());
            AddPriviewCell(table, "Occupation:");
            AddValueCell(table, board.Occupation);
            AddHeader(table, "Address", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Address:");
            AddValueCell(table, board.Adress.Addres);
            AddPriviewCell(table, "City:");
            AddValueCell(table, board.Adress.City);
            AddPriviewCell(table, "Province:");
            AddValueCell(table, board.Adress.Province);
            AddPriviewCell(table, "Country:");
            AddValueCell(table, board.Adress.Country);
            AddPriviewCell(table, "Postal Code:");
            AddValueCell(table, board.Adress.PostalCode);
            AddPriviewCell(table, "Phone:");
            AddValueCell(table, board.Adress.Phone);
            AddPriviewCell(table, "Email:");
            AddValueCell(table, board.Adress.Email);
            AddPriviewCell(table, "Cell:");
            AddValueCell(table, board.Adress.Cell);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf");
            break;
                #endregion
                #region Other
                case 5:
                    board = new Profile(ProfileId);
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Report_" + ProfileId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath+@"\Report_"+ProfileId.ToString()+".pdf",FileMode.CreateNew);
            Doc = new iTextSharp.text.Document(PageSize.A4);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            table = new PdfPTable(5);
            table.WidthPercentage = 100;
            AddHeader(table, "Profile Card", 20, new BaseColor(Color.DimGray),new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Category:");
            AddValueCell(table, "Other");
            AddPriviewCell(table, "Name:");
            AddValueCell(table, board.Name);
            AddPriviewCell(table, "Date of Birth:");
            AddValueCell(table, board.DateOfBirdh.ToShortDateString());
            AddHeader(table, "Address", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Address:");
            AddValueCell(table, board.Adress.Addres);
            AddPriviewCell(table, "City:");
            AddValueCell(table, board.Adress.City);
            AddPriviewCell(table, "Province:");
            AddValueCell(table, board.Adress.Province);
            AddPriviewCell(table, "Country:");
            AddValueCell(table, board.Adress.Country);
            AddPriviewCell(table, "Postal Code:");
            AddValueCell(table, board.Adress.PostalCode);
            AddPriviewCell(table, "Phone:");
            AddValueCell(table, board.Adress.Phone);
            AddPriviewCell(table, "Email:");
            AddValueCell(table, board.Adress.Email);
            AddPriviewCell(table, "Cell:");
            AddValueCell(table, board.Adress.Cell);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf");
            break;
                #endregion

            }
        }

        public static void PrintAttendance(Day _CurrentDay)
        {
            TDayDataSet tDayDataSet = new TDayDataSet();
            TDayDataSetTableAdapters.DaysTableAdapter daysTableAdapter = new TDayDataSetTableAdapters.DaysTableAdapter();
            TDayDataSetTableAdapters.ProfilesTableAdapter profilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();

            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.Day.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.Day.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Attendance_" + _CurrentDay.Date.Day.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.Day.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.Day.ToString() + ".pdf"); }
            }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.Day.ToString()
                + ".pdf", FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            PdfPTable table = new PdfPTable(14);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.WidthPercentage = 100;

            AddHeader(table, "Attendance", 20, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddHeader(table, _CurrentDay.Date.DayOfWeek + "  " + _CurrentDay.Date.ToShortDateString(), 16, new BaseColor(Color.DimGray), new BaseColor(Color.White));
            daysTableAdapter.Fill(tDayDataSet.Days, _CurrentDay.Date);
            
            int Counter = 0;
            int TotalLC = 0;
            double TotalLCP = 0;
            double TotalTOP = 0;
            double TotalMisoP = 0;
            double TotalVan = 0;
            double TotalP = 0;
            double TotalRTP = 0;
            double TotalBFT = 0;
            double TotalT = 0;
            AddHeader(table, "Billed Clients", 12, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Numb", 1);
            AddPriviewCell(table, "Name", 3);
            AddPriviewCell(table, "A", 1);
            AddPriviewCell(table, "LC", 1);
            AddPriviewCell(table, "L$", 1);
            AddPriviewCell(table, "TO$", 1);
            AddPriviewCell(table, "Miso$", 1);
            AddPriviewCell(table, "P$", 1);
            AddPriviewCell(table, "Van", 1);
            AddPriviewCell(table, "RT", 1);
            AddPriviewCell(table, "BFT", 1);
            AddPriviewCell(table, "Total", 1);
            foreach (DataRow Row in tDayDataSet.Days)
            {
                if (ProfileProvider.GetCategory((int)Row["ProfileId"]) == 1)
                {
                    AddPriviewCell(table, Counter.ToString(), 1);
                    AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 3);
                    AddValueCell(table, (bool)Row["Attendance"], true, 1);
                    AddValueCell(table, (bool)Row["Lunch"], true, 1);
                    if ((bool)Row["Lunch"]) { TotalLC++; }
                    AddPriviewCell(table, Row["LunchPrice"].ToString(), 1);
                    TotalLCP += Convert.ToDouble(Row["LunchPrice"]);
                    AddPriviewCell(table, Row["TakeOutPrice"].ToString(), 1);
                    TotalTOP += Convert.ToDouble(Row["TakeOutPrice"]);
                    AddPriviewCell(table, Row["MiscellaneousPrice"].ToString(), 1);
                    TotalMisoP += Convert.ToDouble(Row["MiscellaneousPrice"]);
                    if (ProfileProvider.GetCategory((int)Row["ProfileId"]) == 1)
                    {
                        AddPriviewCell(table, Row["ProgramPrice"].ToString(), 1);
                        TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                        AddPriviewCell(table, Row["VanPrice"].ToString(), 1);
                        TotalVan += Convert.ToDouble(Row["VanPrice"]);
                        AddPriviewCell(table, Row["RoundTripPrice"].ToString(), 1);
                        TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                        AddPriviewCell(table, Row["BookOfTickets"].ToString(), 1);
                        TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                    }
                    else
                    {
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalVan += Convert.ToDouble(Row["VanPrice"]);
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                    }
                    AddPriviewCell(table, Row["Total"].ToString(), 1);
                    Counter++;

                }
                
            }
            AddPriviewCell(table, "", 1);
            AddPriviewCell(table, "Total:", 3);
            AddPriviewCell(table, Counter.ToString(), 1);
            AddPriviewCell(table, TotalLC.ToString(), 1);
            AddPriviewCell(table, TotalLCP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalTOP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalMisoP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalVan.ToString("0.00"), 1);
            AddPriviewCell(table, TotalRTP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalBFT.ToString("0.00"), 1);
            AddPriviewCell(table, (TotalLCP + TotalMisoP + TotalTOP + TotalVan + TotalP + TotalRTP + TotalBFT).ToString("0.00"), 1);

            AddHeader(table, "Staff/Volunteers", 12, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "Numb", 1);
            AddPriviewCell(table, "Name", 3);
            AddPriviewCell(table, "A", 1);
            AddPriviewCell(table, "LC", 1);
            AddPriviewCell(table, "L$", 1);
            AddPriviewCell(table, "TO$", 1);
            AddPriviewCell(table, "Miso$", 1);
            AddPriviewCell(table, "P$", 1);
            AddPriviewCell(table, "Van", 1);
            AddPriviewCell(table, "RT", 1);
            AddPriviewCell(table, "BFT", 1);
            AddPriviewCell(table, "Total", 1);

            Counter = 0;
            TotalLC = 0;
            TotalLCP = 0;
            TotalTOP = 0;
            TotalMisoP = 0;
            TotalVan = 0;
            TotalP = 0;
            TotalRTP = 0;
            TotalBFT = 0;
            TotalT = 0;
            foreach (DataRow Row in tDayDataSet.Days)
            {
                if (ProfileProvider.GetCategory((int)Row["ProfileId"]) != 1)
                {
                    AddPriviewCell(table, Counter.ToString(), 1);
                    AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 3);
                    AddValueCell(table, (bool)Row["Attendance"], true, 1);
                    AddValueCell(table, (bool)Row["Lunch"], true, 1);
                    if ((bool)Row["Lunch"]) { TotalLC++; }
                    AddPriviewCell(table, Row["LunchPrice"].ToString(), 1);
                    TotalLCP += Convert.ToDouble(Row["LunchPrice"]);
                    AddPriviewCell(table, Row["TakeOutPrice"].ToString(), 1);
                    TotalTOP += Convert.ToDouble(Row["TakeOutPrice"]);
                    AddPriviewCell(table, Row["MiscellaneousPrice"].ToString(), 1);
                    TotalMisoP += Convert.ToDouble(Row["MiscellaneousPrice"]);
                    if (ProfileProvider.GetCategory((int)Row["ProfileId"]) == 1)
                    {
                        AddPriviewCell(table, Row["ProgramPrice"].ToString(), 1);
                        TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                        AddPriviewCell(table, Row["VanPrice"].ToString(), 1);
                        TotalVan += Convert.ToDouble(Row["VanPrice"]);
                        AddPriviewCell(table, Row["RoundTripPrice"].ToString(), 1);
                        TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                        AddPriviewCell(table, Row["BookOfTickets"].ToString(), 1);
                        TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                    }
                    else
                    {
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalVan += Convert.ToDouble(Row["VanPrice"]);
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                        AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
                        TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                    }
                    
                    AddPriviewCell(table, Row["Total"].ToString(), 1);
                    Counter++;
                }
                
            }
            AddPriviewCell(table, "", 1);
            AddPriviewCell(table, "Total:", 3);
            AddPriviewCell(table, Counter.ToString(), 1);
            AddPriviewCell(table, TotalLC.ToString(), 1);
            AddPriviewCell(table, TotalLCP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalTOP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalMisoP.ToString("0.00"), 1);
            AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, "", 1, new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(table, (TotalLCP + TotalMisoP + TotalTOP + TotalVan + TotalP + TotalRTP + TotalBFT).ToString("0.00"), 1);

            //********************
            TotalLC = 0;
            TotalLCP = 0;
            TotalTOP = 0;
            TotalMisoP = 0;
            TotalVan = 0;
            TotalP = 0;
            TotalRTP = 0;
            TotalBFT = 0;
            TotalT = 0;
            foreach (DataRow Row in tDayDataSet.Days)
            {
                    if ((bool)Row["Lunch"]) { TotalLC++; }
                    TotalLCP += Convert.ToDouble(Row["LunchPrice"]);
                    TotalTOP += Convert.ToDouble(Row["TakeOutPrice"]);
                    TotalMisoP += Convert.ToDouble(Row["MiscellaneousPrice"]);
                    if (ProfileProvider.GetCategory((int)Row["ProfileId"]) == 1)
                    {
                        TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                        TotalVan += Convert.ToDouble(Row["VanPrice"]);
                        TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                        TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                    }
                    else
                    {
                        TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                        TotalVan += Convert.ToDouble(Row["VanPrice"]);
                        TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                        TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                    }
            }


            //******************
            TotalT += TotalLCP + TotalMisoP + TotalTOP + TotalVan + TotalP + TotalRTP + TotalBFT;
            AddPriviewCell(table, "", 1);
            AddPriviewCell(table, "GrandTotal:", 3);
            AddPriviewCell(table, tDayDataSet.Days.Rows.Count.ToString(), 1);
            AddPriviewCell(table, TotalLC.ToString(), 1);
            AddPriviewCell(table, TotalLCP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalTOP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalMisoP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalVan.ToString("0.00"), 1);
            AddPriviewCell(table, TotalRTP.ToString("0.00"), 1);
            AddPriviewCell(table, TotalBFT.ToString("0.00"), 1);
            AddPriviewCell(table, TotalT.ToString("0.00"), 1);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.Day.ToString() + ".pdf");
        }

        public static void PrintTransportation(string Day)
        {
            TDayDataSet tDayDataSet = new TDayDataSet();
            TDayDataSetTableAdapters.TransportationTableAdapter transportationTableAdapter = new TDayDataSetTableAdapters.TransportationTableAdapter();
            TDayDataSetTableAdapters.ProfilesTableAdapter profilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Transportation.pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf"); }
            }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf", FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            PdfPTable table;
            if (Day == "Master")
            {
                table = new PdfPTable(20);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.WidthPercentage = 100;

                AddHeader(table, "Transportation", 20, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
                AddHeader(table, Day, 16, new BaseColor(Color.DimGray), new BaseColor(Color.White));
                transportationTableAdapter.Fill(tDayDataSet.Transportation);
                AddPriviewCell(table, "Numb", 1);
                AddPriviewCell(table, "Name", 4);
                AddPriviewCell(table, "Category", 2);
                AddPriviewCell(table, "Address", 4);
                AddPriviewCell(table, "Phone", 2);
                AddPriviewCell(table, "HD#", 2);
                AddPriviewCell(table, "Mon", 1);
                AddPriviewCell(table, "Tue", 1);
                AddPriviewCell(table, "Wed", 1);
                AddPriviewCell(table, "Thu", 1);
                AddPriviewCell(table, "Fri", 1);
                //AddPriviewCell(table, "Comment", 1);
            }
            else
            {
                table = new PdfPTable(15);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.WidthPercentage = 100;

                AddHeader(table, "Transportation", 20, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
                AddHeader(table, Day, 16, new BaseColor(Color.DimGray), new BaseColor(Color.White));
                transportationTableAdapter.Fill(tDayDataSet.Transportation);
                AddPriviewCell(table, "Numb", 1);
                AddPriviewCell(table, "Name", 4);
                AddPriviewCell(table, "Category", 2);
                AddPriviewCell(table, "Address", 4);
                AddPriviewCell(table, "Phone", 2);
                AddPriviewCell(table, "HD#", 2);
            }
            int Counter = 1;
            foreach (DataRow Row in tDayDataSet.Transportation)
            {
                switch (Day)
                {
                    case "Master":
                        AddPriviewCell(table, Counter.ToString(), 1);
                        AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 4);
                        AddPriviewCell(table, Row["Category"].ToString(), 2);
                        AddPriviewCell(table, Row["Adress"].ToString(), 4);
                        AddPriviewCell(table, Row["Phone"].ToString(), 2);
                        AddPriviewCell(table, Row["HandyDARTNumber"].ToString(), 2);
                        AddValueCell(table, (bool)Row["Monday"], true, 1);
                        AddValueCell(table, (bool)Row["Tuesday"], true, 1);
                        AddValueCell(table, (bool)Row["Wednesday"], true, 1);
                        AddValueCell(table, (bool)Row["Thursday"], true, 1);
                        AddValueCell(table, (bool)Row["Friday"], true, 1);
                        //AddPriviewCell(table, Row["Comments"].ToString(), 1);
                        Counter++;
                        break;
                    case "Monday":
                        if ((bool)Row["Monday"])
                        {
                            AddPriviewCell(table, Counter.ToString(), 1);
                            AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 4);
                            AddPriviewCell(table, Row["Category"].ToString(), 2);
                            AddPriviewCell(table, Row["Adress"].ToString(), 4);
                            AddPriviewCell(table, Row["Phone"].ToString(), 2);
                            AddPriviewCell(table, Row["HandyDARTNumber"].ToString(), 2);
                            Counter++;
                        }
                        break;
                    case "Tuesday":
                        if ((bool)Row["Tuesday"])
                        {
                            AddPriviewCell(table, Counter.ToString(), 1);
                            AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 4);
                            AddPriviewCell(table, Row["Category"].ToString(), 2);
                            AddPriviewCell(table, Row["Adress"].ToString(), 4);
                            AddPriviewCell(table, Row["Phone"].ToString(), 2);
                            AddPriviewCell(table, Row["HandyDARTNumber"].ToString(), 2);
                            Counter++;
                        }
                        break;
                    case "Wednesday":
                        if ((bool)Row["Wednesday"])
                        {
                            AddPriviewCell(table, Counter.ToString(), 1);
                            AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 4);
                            AddPriviewCell(table, Row["Category"].ToString(), 2);
                            AddPriviewCell(table, Row["Adress"].ToString(), 4);
                            AddPriviewCell(table, Row["Phone"].ToString(), 2);
                            AddPriviewCell(table, Row["HandyDARTNumber"].ToString(), 2);
                            Counter++;
                        }
                        break;
                    case "Thursday":
                        if ((bool)Row["Thursday"])
                        {
                            AddPriviewCell(table, Counter.ToString(), 1);
                            AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 4);
                            AddPriviewCell(table, Row["Category"].ToString(), 2);
                            AddPriviewCell(table, Row["Adress"].ToString(), 4);
                            AddPriviewCell(table, Row["Phone"].ToString(), 2);
                            AddPriviewCell(table, Row["HandyDARTNumber"].ToString(), 2);
                            Counter++;
                        }
                        break;
                    case "Friday":
                        if ((bool)Row["Friday"])
                        {
                            AddPriviewCell(table, Counter.ToString(), 1);
                            AddPriviewCell(table, ProfileProvider.GetName((int)Row["ProfileId"]), 4);
                            AddPriviewCell(table, Row["Category"].ToString(), 2);
                            AddPriviewCell(table, Row["Adress"].ToString(), 4);
                            AddPriviewCell(table, Row["Phone"].ToString(), 2);
                            AddPriviewCell(table, Row["HandyDARTNumber"].ToString(), 2);
                            Counter++;
                        }
                        break;
                }
               

            }
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf");
        }

        public static void PrintBills(int ItemId)
        {
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Bill_" + ItemId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Bill_" + ItemId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Bill_" + ItemId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Bill_" + ItemId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Bill_" + ItemId.ToString() + ".pdf"); }
            }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath + @"\Bill_" + ItemId.ToString() + ".pdf", FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            BillsItem Item = new BillsItem(ItemId);
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            AddPriviewCell(table, TDay.Properties.Settings.Default.PlantString,5);
            AddPriviewCell(table, String.Format("For Services of  {0:MMMM} - {0:yyyy}",Item.Date), 5);
            AddPriviewCell(table, Item.Profile.Name + "\n" + Item.Profile.Adress.Addres + "\n" + Item.Profile.Adress.City + "\n" + Item.Profile.Adress.PostalCode, 2,4, Element.ALIGN_LEFT);
            if (Item.PreviousBillPaid==0)
            {
                AddPriviewCell(table, "Balance Forward \nOverdue \nNew Charges", 1, 3, Element.ALIGN_LEFT);
            }
            else
            {
                AddPriviewCell(table, "Balance Forward \nPayments/Credits\nNew Charges", 1,3,Element.ALIGN_LEFT);
            }
           
            if (Item.PreviousBillPaid == 0)
            {
                AddPriviewCell(table, "", 1, 3, Element.ALIGN_CENTER);
                AddPriviewCell(table, Item.PreviousBillTotal.ToString() + "\n" + Item.PreviousBillTotal.ToString() + "\n" + Item.BillTotal.ToString(), 2, 3, Element.ALIGN_CENTER);
            }
            else
            {
                AddPriviewCell(table, Item.PreviousBillPaidDate.ToShortDateString(), 1, 3, Element.ALIGN_CENTER);
                AddPriviewCell(table, Item.PreviousBillTotal.ToString() + "\n" + Item.PreviousBillPaid.ToString() + "\n" + Item.BillTotal.ToString(), 2, 3, Element.ALIGN_CENTER);
            }
            AddPriviewCell(table, "Balance:", 2,1,Element.ALIGN_LEFT);
            AddPriviewCell(table, (Item.PreviousBillTotal-Item.PreviousBillPaid+Item.BillTotal).ToString(), 1, 2, Element.ALIGN_CENTER);
            Doc.Add(table);
            int TableColums = 0;
            TDayDataSet TempSet = new TDayDataSet();
            TDayDataSetTableAdapters.DaysTableAdapter daysTableAdapter = new TDayDataSetTableAdapters.DaysTableAdapter();
            daysTableAdapter.FillByMonth(TempSet.Days, Bill.GetFirstMonthDay(Item.Date), Bill.GetLastMonthDay(Item.Date));
            double Misc = 0;
            double Trans = 0;
            double Program = 0;
            double TO = 0;
            double Lanch = 0;
            double Van = 0;
            double BFT = 0;
            foreach (DataRow Row in TempSet.Days)
            {
                if ((int)Row["ProfileId"] == Item.ProfileIdBills)
                {
                    Misc += Double.Parse(Row["MiscellaneousPrice"].ToString());
                    Trans += Double.Parse(Row["RoundTripPrice"].ToString());
                    Program += Double.Parse(Row["ProgramPrice"].ToString());
                    TO += Double.Parse(Row["TakeOutPrice"].ToString());
                    Lanch += Double.Parse(Row["LunchPrice"].ToString());
                    Van += Double.Parse(Row["VanPrice"].ToString());
                    BFT += Double.Parse(Row["BookOfTickets"].ToString());
                }
            }
            //Говнокод (((( но к сожалению вообще мозги ничего придумать не могут(((((((((( 3 дня - 2 часа сна...........
            if (Misc > 0) { TableColums++; }
            if (Trans > 0) { TableColums++; }
            if (Program > 0) { TableColums++; }
            if (TO > 0) { TableColums++; }
            if (Lanch > 0) { TableColums++; }
            if (Van > 0) { TableColums++; }
            if (BFT > 0) { TableColums++; }
            PdfPTable tablePart = new PdfPTable(TableColums+3);
            tablePart.WidthPercentage = 100;
            AddHeader(tablePart, " New Charges", 12, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
                AddPriviewCell(tablePart, "Date", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke));
                AddPriviewCell(tablePart, "Comments", 2, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke));
            if (Lanch > 0) { AddPriviewCell(tablePart, "Lunch", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (TO > 0) { AddPriviewCell(tablePart, "TO", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Misc > 0) { AddPriviewCell(tablePart, "Misc", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Program > 0) { AddPriviewCell(tablePart, "Program", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Van > 0) { AddPriviewCell(tablePart, "Van", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Trans > 0) { AddPriviewCell(tablePart, "Trans", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (BFT > 0) { AddPriviewCell(tablePart, "BFT", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)
                ); }
            DataView DV = TempSet.Days.DefaultView;
            DV.Sort = "Date ASC";
            BindingSource TemS = new BindingSource();
            TemS.DataSource = DV;
            int ItemsCount = 0;
            foreach (DataRowView Row in TemS.List)
            {
                if ((int)Row["ProfileId"] == Item.ProfileIdBills)
                {
                    AddPriviewCell(tablePart, ((DateTime)Row["Date"]).ToShortDateString(), 1, 1, Element.ALIGN_CENTER);
                    AddPriviewCell(tablePart, Row["Comments"].ToString(), 2, 1, Element.ALIGN_CENTER);
                    if (Lanch > 0) { AddPriviewCell(tablePart, Row["LunchPrice"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    if (TO > 0) { AddPriviewCell(tablePart, Row["TakeOutPrice"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    if (Misc > 0) { AddPriviewCell(tablePart, Row["MiscellaneousPrice"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    if (Program > 0) { AddPriviewCell(tablePart, Row["ProgramPrice"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    if (Van > 0) { AddPriviewCell(tablePart, Row["VanPrice"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    if (Trans > 0) { AddPriviewCell(tablePart, Row["RoundTripPrice"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    if (BFT > 0) { AddPriviewCell(tablePart, Row["BookOfTickets"].ToString(), 1, 1, Element.ALIGN_CENTER); }
                    ItemsCount++;
                }
            }
            AddPriviewCell(tablePart, ItemsCount.ToString(), 1, 1, Element.ALIGN_CENTER);
            AddPriviewCell(tablePart, "", TableColums + 2, 1, Element.ALIGN_CENTER);
            AddPriviewCell(tablePart, "Totals:", 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke));
            AddPriviewCell(tablePart, (Lanch + TO + Misc + Program + Van + Trans + BFT).ToString(), 2, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke));
            if (Lanch > 0) { AddPriviewCell(tablePart, Lanch.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (TO > 0) { AddPriviewCell(tablePart, TO.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Misc > 0) { AddPriviewCell(tablePart, Misc.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Program > 0) { AddPriviewCell(tablePart, Program.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Van > 0) { AddPriviewCell(tablePart, Van.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (Trans > 0) { AddPriviewCell(tablePart, Trans.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke)); }
            if (BFT > 0){ AddPriviewCell(tablePart, BFT.ToString(), 1, 1, Element.ALIGN_CENTER, new BaseColor(Color.WhiteSmoke) );}
            AddEmpyRow(tablePart);
            AddEmpyRow(tablePart);
            AddEmpyRow(tablePart);
            Doc.Add(tablePart);
            PdfPTable tableFooster = new PdfPTable(6);
            tableFooster.WidthPercentage = 100;
            if (Item.Paid > 0)
            {
                PdfPCell pricell = new PdfPCell(new Phrase("Paid", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                pricell = new PdfPCell(new Phrase(Item.Paid.ToString()+"$", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.UNDERLINE, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_CENTER;
                pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                pricell = new PdfPCell(new Phrase("Cash", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricell.PaddingBottom = 3;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                if (Item.PaidType == "cash")
                {
                    AddValueCell(tableFooster, true, true, 1, false);
                }
                else
                {
                    AddValueCell(tableFooster, false, true, 1, false);
                }
                pricell = new PdfPCell(new Phrase("Cheque", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricell.PaddingBottom = 3;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                if (Item.PaidType == "cheque")
                {
                    AddValueCell(tableFooster, true, true, 1, false);
                }
                else
                {
                    AddValueCell(tableFooster, false, true, 1, false);
                }
            }
            else
            {
                PdfPCell pricell = new PdfPCell(new Phrase("Paid", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                pricell = new PdfPCell(new Phrase("___________$", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_CENTER;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                pricell = new PdfPCell(new Phrase("Cash", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pricell.VerticalAlignment = Element.ALIGN_BOTTOM;
                pricell.PaddingBottom = 3;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                AddValueCell(tableFooster, false, true, 1, false);
                pricell = new PdfPCell(new Phrase("Cheque", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
                pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricell.PaddingBottom = 3;
                pricell.Colspan = 1;
                tableFooster.AddCell(pricell);
                AddValueCell(tableFooster, false, true, 1, false);
            }
            AddEmpyRow(tableFooster);
            AddEmpyRow(tableFooster);
            PdfPCell pricellnew = new PdfPCell(new Phrase("Date", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricellnew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pricellnew.HorizontalAlignment = Element.ALIGN_RIGHT;
            pricellnew.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricellnew.Colspan = 1;
            tableFooster.AddCell(pricellnew);
            if (Item.Paid > 0)
            {
                pricellnew = new PdfPCell(new Phrase(Item.PaidDate.ToShortDateString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.UNDERLINE, new BaseColor(Color.DimGray))));
                pricellnew.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricellnew.HorizontalAlignment = Element.ALIGN_CENTER;
                pricellnew.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricellnew.Colspan = 1;
                tableFooster.AddCell(pricellnew);
            }
            else
            {
                pricellnew = new PdfPCell(new Phrase("___________", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
                pricellnew.Border = iTextSharp.text.Rectangle.NO_BORDER;
                pricellnew.HorizontalAlignment = Element.ALIGN_CENTER;
                pricellnew.VerticalAlignment = Element.ALIGN_MIDDLE;
                pricellnew.Colspan = 1;
                tableFooster.AddCell(pricellnew);
            }
            pricellnew = new PdfPCell(new Phrase("Society Representative", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricellnew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pricellnew.HorizontalAlignment = Element.ALIGN_CENTER;
            pricellnew.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricellnew.Colspan = 2;
            tableFooster.AddCell(pricellnew);
            pricellnew = new PdfPCell(new Phrase("___________________________", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricellnew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pricellnew.HorizontalAlignment = Element.ALIGN_CENTER;
            pricellnew.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricellnew.Colspan = 2;
            tableFooster.AddCell(pricellnew);



            Doc.Add(tableFooster);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Bill_" + ItemId.ToString() + ".pdf");
        }

        public static void PrintEnvelope(int ProfileId, EnvelopeSize Size, string Sender, string Reciver)
        {
            try
            {
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Report_" + ProfileId.ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf", FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document(GetEnvelopeSize(Size),20,20,20,20);
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;
            PdfPCell pricell = new PdfPCell(new Phrase(Sender, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, int.Parse(Math.Round(Doc.PageSize.Width/41,0).ToString()), iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_LEFT;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.SpaceCharRatio = 4;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 2;
            pricell.FixedHeight = ((Doc.PageSize.Height-40) / 10)*3;
            table.AddCell(pricell);
            pricell = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 3;
            table.AddCell(pricell);
            //*******************************Пустой блок
            pricell = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, int.Parse(Math.Round(Doc.PageSize.Width / 41, 0).ToString()), iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_LEFT;
            pricell.VerticalAlignment = Element.ALIGN_TOP;
            pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 5;
            pricell.FixedHeight = ((Doc.PageSize.Height-40) / 10)*4-5;
            table.AddCell(pricell);
            //*******************************
            pricell = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            pricell.HorizontalAlignment = Element.ALIGN_RIGHT;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 3;
            table.AddCell(pricell);
            pricell = new PdfPCell(new Phrase(Reciver, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, int.Parse(Math.Round(Doc.PageSize.Width / 41, 0).ToString()), iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_LEFT;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.SpaceCharRatio = 4;
            pricell.Colspan = 2;
            pricell.FixedHeight = ((Doc.PageSize.Height-40) / 10)*3;
            table.AddCell(pricell);

            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf");
        }


        private static void AddBlockCell(PdfPTable _Table)
        {
            PdfPCell pricell = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.BackgroundColor = new BaseColor(Color.WhiteSmoke);
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 4;
            _Table.AddCell(pricell);
        }

        private static void AddEmpyRow(PdfPTable _Table)
        {
            PdfPCell cell = new PdfPCell();
            cell.Padding = 5;
            cell.Colspan = _Table.NumberOfColumns;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            _Table.AddCell(cell);
        }

        private static void AddHeader(PdfPTable _Table, string Text, int TextWidth, BaseColor _FontColor, BaseColor _BackColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, TextWidth, iTextSharp.text.Font.NORMAL, _FontColor)));
            cell.BackgroundColor = _BackColor;
            cell.Padding = 5;
            cell.Colspan = _Table.NumberOfColumns;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            _Table.AddCell(cell);
        }

        private static void AddPriviewCell(PdfPTable _Table, string Text)
        {
            PdfPCell pricell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 2;
            _Table.AddCell(pricell);
        }

        private static void AddPriviewCell(PdfPTable _Table, string Text, int Colspan)
        {
            PdfPCell pricell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.PaddingBottom = 3;
            pricell.Colspan = Colspan;
            _Table.AddCell(pricell);
        }
        private static void AddPriviewCell(PdfPTable _Table, string Text, int Colspan,BaseColor BackColor)
        {
            PdfPCell pricell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.BackgroundColor = BackColor;
            pricell.PaddingBottom = 3;
            pricell.Colspan = Colspan;
            _Table.AddCell(pricell);
        }

        private static void AddPriviewCell(PdfPTable _Table, string Text, int Colspan,int RowSpan, int Align)
        {
            PdfPCell pricell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.HorizontalAlignment = Align;
            pricell.PaddingBottom = 5;
            pricell.Rowspan = RowSpan;
            pricell.Colspan = Colspan;
            _Table.AddCell(pricell);
        }

        private static void AddPriviewCell(PdfPTable _Table, string Text, int Colspan, int RowSpan, int Align, BaseColor _Color)
        {
            PdfPCell pricell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.HorizontalAlignment = Align;
            pricell.PaddingBottom = 5;
            pricell.BackgroundColor = _Color;
            pricell.Rowspan = RowSpan;
            pricell.Colspan = Colspan;
            _Table.AddCell(pricell);
        }

        private static void AddValueCell(PdfPTable _Table, string Text)
        {
            PdfPCell valcell = new PdfPCell(new Phrase(Text, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            valcell.Colspan = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            valcell.PaddingBottom = 3;
            _Table.AddCell(valcell);
        }

        private static void AddValueCell(PdfPTable _Table, bool Value, bool isCheckBox)
        {
            PdfPCell valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if(Value){ image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE);} 
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 3;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);
        }

        private static void AddValueCell(PdfPTable _Table, bool Value, bool isCheckBox, int Colspan)
        {
            PdfPCell valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (Value) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = Colspan;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);
        }

        private static void AddValueCell(PdfPTable _Table, bool Value, bool isCheckBox, int Colspan, bool Border)
        {
            PdfPCell valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            if (!Border) { valcell.Border = iTextSharp.text.Rectangle.NO_BORDER; }
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (Value) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = Colspan;
            valcell.AddElement(image);
            valcell.PaddingBottom = 1;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);
        }

        private static void AddAttendanceCell(PdfPTable _Table, Attendance attendace)
        {
            AddHeader(_Table, "Attendance", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));

            PdfPCell pricell = new PdfPCell(new Phrase("Monday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Tuesday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Wednesday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Thursday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Friday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            PdfPCell valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (attendace.Monday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (attendace.Tuesday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (attendace.Wednesday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (attendace.Thursday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (attendace.Friday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);
        }

        private static void AddTransportationCell(PdfPTable _Table, Transportation transportation)
        {
            AddHeader(_Table, "Transporation", 16, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));

            PdfPCell pricell = new PdfPCell(new Phrase("Monday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Tuesday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Wednesday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Thursday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            pricell = new PdfPCell(new Phrase("Friday", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            pricell.HorizontalAlignment = Element.ALIGN_CENTER;
            pricell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pricell.PaddingBottom = 3;
            pricell.Colspan = 1;
            _Table.AddCell(pricell);

            PdfPCell valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (transportation.Monday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (transportation.Tuesday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (transportation.Wednesday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (transportation.Thursday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);

            valcell = new PdfPCell(new Phrase(String.Empty, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16, iTextSharp.text.Font.NORMAL, new BaseColor(Color.DimGray))));
            image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.uncheck, BaseColor.WHITE);
            if (transportation.Friday) { image = iTextSharp.text.Image.GetInstance(TDay.Properties.Resources.check, BaseColor.WHITE); }
            image.ScaleToFit(15, 15);
            image.Alignment = Element.ALIGN_CENTER;
            //valcell.Image = image;
            valcell.Colspan = 1;
            valcell.AddElement(image);
            valcell.PaddingBottom = 3;
            valcell.HorizontalAlignment = Element.ALIGN_CENTER;
            valcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _Table.AddCell(valcell);
        }

        public static string GetFileProcessName(string fileName)
        {

                Process[] procs = Process.GetProcesses();
                foreach (Process proc in procs)
                {
                    try
                    {
                        if (proc.MainWindowHandle != new IntPtr(0) && !proc.HasExited)
                        {
                            if (proc.MainWindowTitle.IndexOf(fileName) != -1)
                            {
                                proc.Kill();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            return null;
        }

        public enum EnvelopeSize
        {
            Env10,
            EnvLS,
            EnvTY,
            EnvBD1,
            EnvBD2
        }

        public static iTextSharp.text.Rectangle GetEnvelopeSize(EnvelopeSize Size)
        {
            iTextSharp.text.Rectangle _Rec = new iTextSharp.text.Rectangle(100,100);
            switch (Size)
            {
                case EnvelopeSize.Env10:
                    _Rec = new iTextSharp.text.Rectangle(684, 297);
                    break;
                case EnvelopeSize.EnvLS:
                    _Rec = new iTextSharp.text.Rectangle(792, 612);
                    break;
                case EnvelopeSize.EnvTY:
                    _Rec = new iTextSharp.text.Rectangle(414, 288);
                    break;
                case EnvelopeSize.EnvBD1:
                    _Rec = new iTextSharp.text.Rectangle(522, 369);
                    break;
                case EnvelopeSize.EnvBD2:
                    _Rec = new iTextSharp.text.Rectangle(486, 333);
                    break;
            }
            return _Rec;
        }


    }

}
