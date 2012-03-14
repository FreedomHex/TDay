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
                    if ((bool)Row[DateTime.Now.DayOfWeek.ToString()])
                    {
                        DayItem Item = new DayItem((int)Row["ProfileId"]);
                        InsertItem(Item);
                    }
                }
            }
        }
        private bool CheckCreated()
        {
            bool _IsCreate = false;
            daysTableAdapter.Fill(tDayDataSet.Days,Date);
            foreach (DataRow Row in tDayDataSet.Days)
            {
                if ((DateTime)Row["Date"] == DateTime.Now.Date)
                {
                    _IsCreate = true;
                    break;
                }
            }
            return _IsCreate;
        }
        private void SetDayOfWeek()
        {
            WeekDay = (int)DateTime.Now.DayOfWeek;
        }

        public void InsertItem(DayItem Item)
        {
            daysTableAdapter.Insert(Date, Item.ProfileId, Item.Lunch, Item.LunchPrice, Item.TakeOutPrice, Item.MiscellaneousPrice, Item.VanPrice, Item.RoundTripPrice, Item.BookOfTickets, Item.Comments, Item.Attendance,Item.Total);
        }

    }
}
