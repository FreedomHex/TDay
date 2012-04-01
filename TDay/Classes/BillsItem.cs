using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class BillsItem
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.DaysTableAdapter daysTableAdapter = new TDayDataSetTableAdapters.DaysTableAdapter();
        TDayDataSetTableAdapters.BillsTableAdapter billsTableAdapter = new TDayDataSetTableAdapters.BillsTableAdapter();
        //public int BillId { get; set; }
        public DateTime Date { get; set; }
        public int BillId { get; set; }
        public int ProfileIdBills { get; set; }
        public decimal BillTotal { get; set; }
        public decimal Paid { get; set; }
        public string PaidType { get; set; }
        public DateTime PaidDate { get; set; }
        public decimal PreviousBillTotal { get; set; }
        public decimal PreviousBillPaid { get; set; }
        public DateTime PreviousBillPaidDate { get; set; }
        public Profile Profile { get; set; }

        public BillsItem(int ProfileId, DateTime FirstDayOfMonth, DateTime LastDayOfMonth)
        {
            Date = DateTime.Now.Date;
            ProfileIdBills = ProfileId;
            BillTotal = Decimal.Zero;
            Paid = Decimal.Zero;
            PaidType = "Cash";
            PaidDate = DateTime.Now.Date;
            daysTableAdapter.FillByMonth(tDayDataSet.Days, FirstDayOfMonth, LastDayOfMonth);
            foreach (DataRow Row in tDayDataSet.Days)
            {
                if ((int)Row["ProfileId"] == ProfileId)
                {
                    BillTotal += (decimal)Row["Total"];
                }
            }
            PreviousBillTotal = GetPreviousBillTotal(ProfileId, FirstDayOfMonth, LastDayOfMonth);
            PreviousBillPaid = GetPreviousBillPaid(ProfileId, FirstDayOfMonth, LastDayOfMonth);
            PreviousBillPaidDate = GetPreviousBillPaidDate(ProfileId, FirstDayOfMonth, LastDayOfMonth);
        }

        public BillsItem(int ItemId)
        {
            billsTableAdapter.Fill(tDayDataSet.Bills);
            DataRow _Item = tDayDataSet.Bills.FindByBillId(ItemId);
            
            Date = DateTime.Now.Date;
            BillId = ItemId;
            ProfileIdBills = (int)_Item["ProfileId"];
            BillTotal = GetBillTotal((int)_Item["ProfileId"],Bill.GetFirstMonthDay(Date),Bill.GetLastMonthDay(Date));
            Paid = (decimal)_Item["Paid"];
            PaidDate = (DateTime)_Item["PaidDate"];
            PaidType = (string) _Item["PaidType"];
            Profile = new Profile((int)_Item["ProfileId"]);
            PreviousBillPaid = GetPreviousBillPaid((int)_Item["ProfileId"], Bill.GetFirstMonthDay((DateTime)_Item["Date"]), Bill.GetLastMonthDay((DateTime)_Item["Date"]));
            PreviousBillTotal = (decimal)_Item["PreviousBillTotal"];
            PreviousBillPaidDate = (DateTime)_Item["PreviousBillPaidDate"];
            _Item["BillTotal"] = BillTotal;
            billsTableAdapter.Update(tDayDataSet);
        }
        public void Update()
        {
            billsTableAdapter.Fill(tDayDataSet.Bills);
            DataRow _Item = tDayDataSet.Bills.FindByBillId(BillId);
            _Item["Paid"] = Paid;
            _Item["PaidType"] = PaidType;
            _Item["PaidDate"] = PaidDate;
            billsTableAdapter.Update(tDayDataSet);
        }
        private decimal GetBillTotal(int _ProfileId, DateTime FirstDayOfMonth, DateTime LastDayOfMonth)
        {
            decimal _PreBillTotal = decimal.Zero;
            TDayDataSet tempSet = new TDayDataSet();
            daysTableAdapter.FillByMonth(tempSet.Days, FirstDayOfMonth, LastDayOfMonth);
            foreach (DataRow Row in tempSet.Days)
            {
                if ((int)Row["ProfileId"] == _ProfileId)
                {
                    _PreBillTotal += (decimal)Row["Total"];
                }
            }
            tempSet.Dispose();
            return _PreBillTotal;
        }

        private decimal GetPreviousBillTotal(int _ProfileId, DateTime FirstDayOfMonth, DateTime LastDayOfMonth)
        {
            decimal _PreBillTotal = decimal.Zero;
            TDayDataSet tempSet = new TDayDataSet();
            billsTableAdapter.FillByMonth(tempSet.Bills, FirstDayOfMonth.AddMonths(-1), LastDayOfMonth.AddMonths(-1));
            foreach (DataRow Row in tempSet.Bills)
            {
                if ((int)Row["ProfileId"] == _ProfileId)
                {
                    _PreBillTotal = (decimal)Row["BillTotal"];
                    break;
                }
            }
            tempSet.Dispose();
            return _PreBillTotal;
        }

        private decimal GetPreviousBillPaid(int _ProfileId, DateTime FirstDayOfMonth, DateTime LastDayOfMonth)
        {
            decimal _PreBillTotal = decimal.Zero;
            TDayDataSet tempSet = new TDayDataSet();
            billsTableAdapter.FillByMonth(tempSet.Bills, FirstDayOfMonth.AddMonths(-1), LastDayOfMonth.AddMonths(-1));
            foreach (DataRow Row in tempSet.Bills)
            {
                if ((int)Row["ProfileId"] == _ProfileId)
                {
                    _PreBillTotal = (decimal)Row["Paid"];
                    break;
                }
            }
            tempSet.Dispose();
            return _PreBillTotal;
        }

        private DateTime GetPreviousBillPaidDate(int _ProfileId, DateTime FirstDayOfMonth, DateTime LastDayOfMonth)
        {
            DateTime _PreBillTotal = DateTime.Now.Date;
            TDayDataSet tempSet = new TDayDataSet();
            billsTableAdapter.FillByMonth(tempSet.Bills, FirstDayOfMonth.AddMonths(-1), LastDayOfMonth.AddMonths(-1));
            foreach (DataRow Row in tempSet.Bills)
            {
                if ((int)Row["ProfileId"] == _ProfileId)
                {
                    _PreBillTotal = (DateTime)Row["PaidDate"];
                    break;
                }
            }
            tempSet.Dispose();
            return _PreBillTotal;
        }

        public void Insert()
        {
            billsTableAdapter.Insert(Date, ProfileIdBills, BillTotal, Paid, PaidType, PaidDate, PreviousBillTotal, PreviousBillPaid, PreviousBillPaidDate);
        }

    }
}
