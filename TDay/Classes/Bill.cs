using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Bill
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.DaysTableAdapter daysTableAdapter = new TDayDataSetTableAdapters.DaysTableAdapter();
        TDayDataSetTableAdapters.BillsTableAdapter billsTableAdapter = new TDayDataSetTableAdapters.BillsTableAdapter();
        TDayDataSetTableAdapters.ProfilesTableAdapter profilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
        public DateTime FirstDayOfMonth { get; set; }
        public DateTime LastDayOfMonth { get; set; }
        public static int ItemsCount { get; set; }
       
        public Bill()
        {
            FirstDayOfMonth = GetFirstMonthDay(DateTime.Now.Date);
            LastDayOfMonth = GetLastMonthDay(DateTime.Now.Date);
        }

        public void Create()
        {
            
            //daysTableAdapter.FillByMonth(tDayDataSet.Days, FirstDayOfMonth, LastDayOfMonth);
            if (!IsCreate())
            {
                profilesTableAdapter.Fill(tDayDataSet.Profiles);
                foreach (DataRow _profile in tDayDataSet.Profiles)
                {
                    if ((int)_profile["Category"] < (int)Enums.Category.BoardMember)
                    {
                        BillsItem Item = new BillsItem((int)_profile["ProfileId"], FirstDayOfMonth, LastDayOfMonth);
                        Item.Insert();
                    }
                }
                //billsTableAdapter.DeleteByMonth(FirstDayOfMonth, LastDayOfMonth); //Удаляем все существующие записи за текущий месяц
            }
        }
        public static DateTime GetFirstMonthDay(DateTime Time)
        {
            DateTime _FirthDay = Time.AddDays(((int)Time.Day * (-1))+1);
            return _FirthDay;
        }
        public static DateTime GetLastMonthDay(DateTime Time)
        {
            DateTime _LastDay = Time.AddDays(DateTime.DaysInMonth(Time.Year, Time.Month) - Time.Day);
            return _LastDay;
        }

        public bool IsCreate()
        {
            bool _IsInBills = false;
            TDayDataSet tempSet = new TDayDataSet();
            billsTableAdapter.FillByMonth(tempSet.Bills, FirstDayOfMonth, LastDayOfMonth);
            if (tempSet.Bills.Rows.Count > 0)
            {
                _IsInBills = true;
            }
            tempSet.Dispose();
            return _IsInBills;
        }

        //private bool CheckNewBills()
        //{
        //    bool _IsNewBill = true;
        //    int _TempItemsCount = 0;
        //    TDayDataSet tempSet = new TDayDataSet();
        //    profilesTableAdapter.Fill(tempSet.Profiles);
        //    daysTableAdapter.FillByMonth(tempSet.Days, FirstDayOfMonth, LastDayOfMonth);
        //    foreach (DataRow _profile in tempSet.Profiles)
        //    {
        //        if ((int)_profile["Category"] < (int)Enums.Category.BoardMember)
        //        {
        //            foreach (DataRow _DayItem in tempSet.Days)
        //            {
        //                if ((int)_profile["ProfileId"] == (int)_DayItem["ProfileId"])
        //                {
        //                    _TempItemsCount++;
        //                }
        //            }
        //        }
        //    }
        //    if (_TempItemsCount == ItemsCount)
        //    {
        //        _IsNewBill = false;
                
        //    }
        //    else
        //    {
        //        ItemsCount = _TempItemsCount;
        //    }
        //    tempSet.Dispose();
        //    return _IsNewBill;
        //}
        
    }
}
