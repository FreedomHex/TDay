using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public static class ProfileProvider
    {
        
        public static int GetCategory(int ProfileId)
        {
            TDayDataSet tDayDataSet = new TDayDataSet();
            TDayDataSetTableAdapters.ProfilesTableAdapter ProfilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            DataRow Row = tDayDataSet.Profiles.FindByProfileId(ProfileId);
            return (int)Row["Category"];
        }
    }
}
