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
        public int ProfileId                { get; set; }
        public bool    Attendance         { get; set; }
        public bool    Lunch              { get; set; }
        public decimal LunchPrice         { get; set; }
        public decimal TakeOutPrice       { get; set; }
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
            DataRow LunchRow = tDayDataSet.Services.FindByServiceId(1); //Не расширяемая ссылка на Сервис
            DataRow RTRow = tDayDataSet.Services.FindByServiceId(2);
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
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets;
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
                    TakeOutPrice = Decimal.Zero;
                    MiscellaneousPrice = Decimal.Zero;
                    VanPrice = Decimal.Zero;
                    BookOfTickets = Decimal.Zero;
                    Total = LunchPrice + TakeOutPrice + MiscellaneousPrice + VanPrice + RoundTripPrice + BookOfTickets;
                    break;
            }

        }

    }
}
