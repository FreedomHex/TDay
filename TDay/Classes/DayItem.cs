using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{

    public class DayItem
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.ProfilesTableAdapter profilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
        TDayDataSetTableAdapters.ServicesTableAdapter servicesTableAdapter = new TDayDataSetTableAdapters.ServicesTableAdapter();
        TDayDataSetTableAdapters.DaysTableAdapter daysTableAdapter = new TDayDataSetTableAdapters.DaysTableAdapter();
        DataRow LunchRow;
        DataRow RTRow;
        DataRow PrRow;
        public int ProfileId              { get; set; }
        public int DayId                  { get; set; }
        public DateTime Date              { get; set; }
        public bool    Attendance         { get; set; }
        public bool    Lunch              { get; set; }
        public decimal LunchPrice         { get; set; }
        public decimal TakeOutPrice       { get; set; }
        public decimal ProgramPrice       { get; set; }
        public decimal MiscellaneousPrice { get; set; }
        public decimal VanPrice           { get; set; }
        public decimal RoundTripPrice     { get; set; }
        public decimal BookOfTickets      { get; set; }
        public decimal Total              { get; set; }
        public string  Comments           { get; set; }
        public int WeekDay { get; set; }

        public DayItem( int ProfileUID)
        {
            servicesTableAdapter.Fill(tDayDataSet.Services);
            profilesTableAdapter.Fill(tDayDataSet.Profiles);
            LunchRow = tDayDataSet.Services.FindByServiceId(1); //Не расширяемая ссылка на Сервис
            RTRow = tDayDataSet.Services.FindByServiceId(2);
            PrRow = tDayDataSet.Services.FindByServiceId(3);
            WeekDay = (int) DateTime.Now.DayOfWeek;
            DataRow Prof = tDayDataSet.Profiles.FindByProfileId(ProfileUID);
            ProfileId = ProfileUID;
            switch ((int)Prof["Category"])
            {
                case (int)Enums.Category.Client:
                    Client client = new Client(ProfileUID);
                    Attendance = true;
                    Lunch = true;
                    //Индийский код на на всякий случай, авось расширять буду и Lunch из какой нить задницы всплывет
                    if (Lunch)
                    {
                        LunchPrice = Decimal.Parse(LunchRow["ServiceFee"].ToString());
                    }
                    if (client.Transportation.GetDay(WeekDay))
                    {
                        RoundTripPrice = Decimal.Parse(RTRow["ServiceFee"].ToString());
                    }
                    else
                    {
                        RoundTripPrice = Decimal.Zero;
                    }
                    ProgramPrice = Decimal.Parse(PrRow["ServiceFee"].ToString());
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets+ProgramPrice;
                    break;
                case (int)Enums.Category.Employee:
                    Employee employee = new Employee(ProfileUID);
                    Attendance = true;
                    Lunch = true;
                    //Индийский код на на всякий случай, авось расширять буду и Lunch из какой нить задницы всплывет
                    if (Lunch)
                    {
                        LunchPrice = Decimal.Parse(LunchRow["ServiceFee"].ToString());
                    }
                    ProgramPrice = Decimal.Zero;
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets + ProgramPrice;
                    break;
                case (int)Enums.Category.Volunteer:
                    Attendance = true;
                    Lunch = true;
                    //Индийский код на на всякий случай, авось расширять буду и Lunch из какой нить задницы всплывет
                    if (Lunch)
                    {
                        LunchPrice = Decimal.Parse(LunchRow["ServiceFee"].ToString());
                    }
                    ProgramPrice = Decimal.Zero;
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets + ProgramPrice;
                    break;
                case (int)Enums.Category.BoardMember:
                    Attendance = true;
                    Lunch = true;
                    //Индийский код на на всякий случай, авось расширять буду и Lunch из какой нить задницы всплывет
                    if (Lunch)
                    {
                        LunchPrice = Decimal.Parse(LunchRow["ServiceFee"].ToString());
                    }
                    ProgramPrice = Decimal.Zero;
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets + ProgramPrice;
                    break;
                case (int)Enums.Category.Other:
                    Attendance = true;
                    Lunch = true;
                    //Индийский код на на всякий случай, авось расширять буду и Lunch из какой нить задницы всплывет
                    if (Lunch)
                    {
                        LunchPrice = Decimal.Parse(LunchRow["ServiceFee"].ToString());
                    }
                    ProgramPrice = Decimal.Zero;
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets + ProgramPrice;
                    break;
            }

        }
        public DayItem(int ItemId, DateTime Date)
        {
            servicesTableAdapter.Fill(tDayDataSet.Services);
            daysTableAdapter.Fill(tDayDataSet.Days, Date);
            LunchRow = tDayDataSet.Services.FindByServiceId(1); //Не расширяемая ссылка на Сервис
            PrRow = tDayDataSet.Services.FindByServiceId(3);
            RTRow = tDayDataSet.Services.FindByServiceId(2);
            DataRow Row = tDayDataSet.Days.FindByDayId(ItemId);
            Attendance = (bool)Row["Attendance"];
            Lunch = (bool)Row["Lunch"];
            LunchPrice = Math.Round((decimal)Row["LunchPrice"],2);
            TakeOutPrice = (decimal)Row["TakeOutPrice"];
            MiscellaneousPrice = (decimal)Row["MiscellaneousPrice"];
            VanPrice = (decimal)Row["VanPrice"];
            ProgramPrice = (decimal)Row["ProgramPrice"];
            RoundTripPrice = (decimal)Row["RoundTripPrice"];
            BookOfTickets = (decimal)Row["BookOfTickets"];
            Total = (decimal)Row["Total"];
            Comments = Row["Comments"].ToString();
            DayId = (int)Row["DayId"];
            this.Date = Date;
        }
        public decimal GetLunchPrice()
        {
            return Decimal.Parse(LunchRow["ServiceFee"].ToString());
        }
        public void Update()
        {

            daysTableAdapter.Fill(tDayDataSet.Days, this.Date);
            DataRow Row = tDayDataSet.Days.FindByDayId(this.DayId);
            Row["Attendance"] = Attendance;
            Row["Lunch"] = Lunch;
            Row["LunchPrice"] = LunchPrice;
            Row["TakeOutPrice"]=TakeOutPrice;
            Row["MiscellaneousPrice"]=MiscellaneousPrice;
            Row["VanPrice"]=VanPrice;
            Row["ProgramPrice"] = ProgramPrice;
            Row["RoundTripPrice"]=RoundTripPrice;
            Row["BookOfTickets"]=BookOfTickets;
            Row["Total"] = GetTotal();
            Row["Comments"] = Comments;
            daysTableAdapter.Update(tDayDataSet.Days);
        }

        private decimal GetTotal()
        {
            return LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets+ProgramPrice;
        }
    }
}
