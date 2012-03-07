using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDay
{
    class Client:Profile
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.ProfilesTableAdapter ProfilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();

        public string ParisNumber                           { get; set; }
        public string HdNumber                              { get; set; }
        public Enums.Transportation Transportation          { get; set; }
        
        public Client()
        {

        }
        public void Add()
        {
            try
            {
                ProfilesTableAdapter.Insert(this.Name, (int)Enums.Category.Client, this.DateOfBirdh, null, null, null, null, null, this.ParisNumber, null, null, null, null, null);
            }
            catch (Exception ex)
            {
                ErrorProvider.SetException(Enums.ExceptionType.FunctionException, ex);
            }
        }
    }
}
