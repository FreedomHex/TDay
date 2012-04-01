using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Day
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.DaysTableAdapter daysTableAdapter = new TDayDataSetTableAdapters.DaysTableAdapter();
        TDayDataSetTableAdapters.AttendanceTableAdapter attendanceTableAdapter = new TDayDataSetTableAdapters.AttendanceTableAdapter();
        public bool IsCreated  { get; set; }
        public DateTime Date   { get; set; }
        public DayItem[] Items { get; set; }
        public int WeekDay     { get; set; }
        public Day()
        {
            Date = DateTime.Now.Date;
            IsCreated = CheckCreated();
            SetDayOfWeek();
        }

        public void CreateDay()
        {
            IsCreated = CheckCreated();
            if (!IsCreated)
            {
                 attendanceTableAdapter.Fill(tDayDataSet.Attendance);
                    foreach (DataRow Row in tDayDataSet.Attendance)
                    {
                        if (Date.DayOfWeek != DayOfWeek.Saturday && Date.DayOfWeek != DayOfWeek.Sunday)
                        {
                            if ((bool)Row[Date.DayOfWeek.ToString()] == true && (int)Row["ProfileId"] > 0 && !ProfileProvider.GetDelStatus((int)Row["ProfileId"]))
                            {
                                DayItem Item = new DayItem((int)Row["ProfileId"]);
                                InsertItem(Item);
                            }
                        }
                    }
            }
        }
        public bool CheckCreated()
        {
            bool _IsCreate = false;
            daysTableAdapter.Fill(tDayDataSet.Days,Date);
            foreach (DataRow Row in tDayDataSet.Days)
            {
                if ((DateTime)Row["Date"] == Date)
                {
                    _IsCreate = true;
                    break;
                }
            }
            return _IsCreate;
        }

        public bool IsInDay(DayItem Item)
        {
            bool _IsInDay = false;
            daysTableAdapter.Fill(tDayDataSet.Days, Date);
            foreach (DataRow Row in tDayDataSet.Days)
            {
                if ((int)Row["ProfileId"] == Item.ProfileId)
                {
                    _IsInDay = true;
                    break;
                }
            }
            return _IsInDay; 
        }
        private void SetDayOfWeek()
        {
            WeekDay = (int)DateTime.Now.DayOfWeek;
        }
        public void ChangeDate(DateTime _Date)
        {
            this.Date = _Date;
            IsCreated = CheckCreated();
        }
        public void InsertItem(DayItem Item)
        {
            daysTableAdapter.Insert(Date, Item.ProfileId, Item.Lunch, Item.LunchPrice, Item.TakeOutPrice,Item.ProgramPrice,Item.MiscellaneousPrice, Item.VanPrice, Item.RoundTripPrice, Item.BookOfTickets, Item.Comments, Item.Attendance,Item.Total,ProfileProvider.GetCategory(Item.ProfileId));
        }

    }
}
