﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Transportation
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.TransportationTableAdapter transportationTableAdapter = new TDayDataSetTableAdapters.TransportationTableAdapter();
        public int TransportationId { get; set; }
        public int ProfileId { get; set; }
        public string Category { get; set; }
        public string HandyDARTNumber { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public string Comments { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Transportation()
        {
            Category = String.Empty;
            HandyDARTNumber = String.Empty;
        }
        public Transportation(int ProfileUID)
        {
            transportationTableAdapter.Fill(tDayDataSet.Transportation);
            foreach (DataRow Row in tDayDataSet.Transportation)
            {
                if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                {
                    Category = Row["Category"].ToString();
                    HandyDARTNumber = Row["HandyDARTNumber"].ToString();
                    Monday = bool.Parse(Row["Monday"].ToString());
                    Tuesday = bool.Parse(Row["Tuesday"].ToString());
                    Wednesday = bool.Parse(Row["Wednesday"].ToString());
                    Thursday = bool.Parse(Row["Thursday"].ToString());
                    Friday = bool.Parse(Row["Friday"].ToString());
                    Comments = Row["Comments"].ToString();
                    Address = Row["Adress"].ToString();
                    Phone = Row["Phone"].ToString();
                    TransportationId = int.Parse(Row["TransportationId"].ToString());
                    break;
                }
            }
        }
        public void Update()
        {
            transportationTableAdapter.Fill(tDayDataSet.Transportation);
            DataRow Row = tDayDataSet.Transportation.FindByTransportationId(TransportationId);
            Row["Category"] = Category;
            Row["HandyDARTNumber"] = HandyDARTNumber;
            Row["Monday"] = Monday;
            Row["Tuesday"] = Tuesday;
            Row["Wednesday"] = Wednesday;
            Row["Thursday"] = Thursday;
            Row["Friday"] = Friday;
            Row["Comments"] = Comments;
            Row["Adress"] = Address;
            Row["Phone"] = Phone;
            transportationTableAdapter.Update(tDayDataSet.Transportation);
        }
        public void AddTransportationTo(Profile Profile)
        {
            transportationTableAdapter.Insert(Profile.ProfileUID, Category, HandyDARTNumber, Monday, Tuesday, Wednesday, Thursday, Friday, Comments,Address,Phone);
        }

        public bool  GetDay(int DW)
        {
            bool _IsInDay = false;
            switch (DW)
            {
                case 1:
                    if (Monday) { _IsInDay = true; }
                    break;
                case 2:
                    if (Tuesday) { _IsInDay = true; }
                    break;
                case 3:
                    if (Wednesday) { _IsInDay = true; }
                    break;
                case 4:
                    if (Thursday) { _IsInDay = true; }
                    break;
                case 5:
                    if (Friday) { _IsInDay = true; }
                    break;
            }
            return _IsInDay;
        }

    }
}
