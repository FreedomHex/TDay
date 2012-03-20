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
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.ToShortDateString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.ToShortDateString() + ".pdf"); }
            }
            catch (IOException)
            {
                string Proc = GetFileProcessName("Attendance_" + _CurrentDay.Date.ToShortDateString().ToString() + ".pdf");
                if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.ToShortDateString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.ToShortDateString() + ".pdf"); }
            }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.ToShortDateString() + ".pdf", FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            PdfPTable table = new PdfPTable(14);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.WidthPercentage = 100;

            AddHeader(table, "Attendance", 20, new BaseColor(Color.DimGray), new BaseColor(Color.WhiteSmoke));
            AddHeader(table, _CurrentDay.Date.DayOfWeek + "  " + _CurrentDay.Date.ToShortDateString(), 16, new BaseColor(Color.DimGray), new BaseColor(Color.White));
            daysTableAdapter.Fill(tDayDataSet.Days, _CurrentDay.Date);
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
            int Counter = 1;
            int TotalLC = 0;
            double TotalLCP = 0;
            double TotalTOP = 0;
            double TotalMisoP = 0;
            double TotalVan = 0;
            double TotalP = 0;
            double TotalRTP = 0;
            double TotalBFT = 0;
            double TotalT = 0;
            foreach (DataRow Row in tDayDataSet.Days)
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
                AddPriviewCell(table, Row["ProgramPrice"].ToString(), 1);
                TotalP += Convert.ToDouble(Row["ProgramPrice"]);
                AddPriviewCell(table, Row["VanPrice"].ToString(), 1);
                TotalVan += Convert.ToDouble(Row["VanPrice"]);
                AddPriviewCell(table, Row["RoundTripPrice"].ToString(), 1);
                TotalRTP += Convert.ToDouble(Row["RoundTripPrice"]);
                AddPriviewCell(table, Row["BookOfTickets"].ToString(), 1);
                TotalBFT += Convert.ToDouble(Row["BookOfTickets"]);
                AddPriviewCell(table, Row["Total"].ToString(), 1);
                Counter++;

            }
            TotalT += TotalLCP + TotalMisoP + TotalTOP + TotalVan + TotalP + TotalRTP + TotalBFT;
            AddPriviewCell(table, "", 1);
            AddPriviewCell(table, "Total:", 3);
            AddPriviewCell(table, tDayDataSet.Days.Rows.Count.ToString(), 1);
            AddPriviewCell(table, TotalLC.ToString(), 1);
            AddPriviewCell(table, TotalLCP.ToString(), 1);
            AddPriviewCell(table, TotalTOP.ToString(), 1);
            AddPriviewCell(table, TotalMisoP.ToString(), 1);
            AddPriviewCell(table, TotalP.ToString(), 1);
            AddPriviewCell(table, TotalVan.ToString(), 1);
            AddPriviewCell(table, TotalRTP.ToString(), 1);
            AddPriviewCell(table, TotalBFT.ToString(), 1);
            AddPriviewCell(table, TotalT.ToString(), 1);
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Attendance_" + _CurrentDay.Date.ToShortDateString() + ".pdf");
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
            PdfPTable table = new PdfPTable(20);
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
                            AddValueCell(table, (bool)Row["Monday"], true, 1);
                            AddValueCell(table, (bool)Row["Tuesday"], true, 1);
                            AddValueCell(table, (bool)Row["Wednesday"], true, 1);
                            AddValueCell(table, (bool)Row["Thursday"], true, 1);
                            AddValueCell(table, (bool)Row["Friday"], true, 1);
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
                            AddValueCell(table, (bool)Row["Monday"], true, 1);
                            AddValueCell(table, (bool)Row["Tuesday"], true, 1);
                            AddValueCell(table, (bool)Row["Wednesday"], true, 1);
                            AddValueCell(table, (bool)Row["Thursday"], true, 1);
                            AddValueCell(table, (bool)Row["Friday"], true, 1);
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
                            AddValueCell(table, (bool)Row["Monday"], true, 1);
                            AddValueCell(table, (bool)Row["Tuesday"], true, 1);
                            AddValueCell(table, (bool)Row["Wednesday"], true, 1);
                            AddValueCell(table, (bool)Row["Thursday"], true, 1);
                            AddValueCell(table, (bool)Row["Friday"], true, 1);
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
                            AddValueCell(table, (bool)Row["Monday"], true, 1);
                            AddValueCell(table, (bool)Row["Tuesday"], true, 1);
                            AddValueCell(table, (bool)Row["Wednesday"], true, 1);
                            AddValueCell(table, (bool)Row["Thursday"], true, 1);
                            AddValueCell(table, (bool)Row["Friday"], true, 1);
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
                            AddValueCell(table, (bool)Row["Monday"], true, 1);
                            AddValueCell(table, (bool)Row["Tuesday"], true, 1);
                            AddValueCell(table, (bool)Row["Wednesday"], true, 1);
                            AddValueCell(table, (bool)Row["Thursday"], true, 1);
                            AddValueCell(table, (bool)Row["Friday"], true, 1);
                            Counter++;
                        }
                        break;
                }
               

            }
            Doc.Add(table);
            Doc.Close();
            Process.Start(System.Windows.Forms.Application.UserAppDataPath + @"\Transportation.pdf");
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

    }

}
