using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace TDay
{
    public static class AuthProvider
    {
        public static bool Login(string Username, string Password)
        {
            bool _isAuthentificated = false;
            TDayDataSet TDaySet = new TDayDataSet();
            TDayDataSetTableAdapters.UsersTableAdapter usersTableAdapter = new TDayDataSetTableAdapters.UsersTableAdapter();
            usersTableAdapter.Fill(TDaySet.Users);
            foreach (DataRow Row in TDaySet.Users)
            {
                if (Row["Login"].ToString() == Username && Row["Password"].ToString() == Password)
                {
                    _isAuthentificated = true;
                    break;
                }
            }
            return _isAuthentificated;
        }
    }
}
