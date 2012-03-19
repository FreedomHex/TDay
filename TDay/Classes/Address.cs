using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Address:IDisposable
    {
        private bool disposed = false;
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.AddressesTableAdapter addressesTableAdapter = new TDayDataSetTableAdapters.AddressesTableAdapter();
        public int AdressId { get; set; }
        public int ProfileId { get; set; }
        public string Addres { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Cell { get; set; }

        public Address()
        {

        }
        public Address(int ProfileUID)
        {
            addressesTableAdapter.Fill(tDayDataSet.Addresses);
            foreach (DataRow Row in tDayDataSet.Addresses)
            {
                if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                {
                    Addres = Row["Address"].ToString();
                    City = Row["City"].ToString();
                    Province = Row["Province"].ToString();
                    Country = Row["Country"].ToString();
                    PostalCode = Row["PostalCode"].ToString();
                    Phone = Row["Phone"].ToString();
                    Email = Row["Email"].ToString();
                    Cell = Row["Cell"].ToString();
                    AdressId = int.Parse(Row["AddressId"].ToString());
                    break;
                }
            }
        }
        public void AddAdressTo(Profile Profile)
        {
            addressesTableAdapter.Insert(Profile.ProfileUID, Addres, City, Province, Country, PostalCode, Phone, Cell, Email);
        }
        public void Update()
        {
            addressesTableAdapter.Fill(tDayDataSet.Addresses);
            DataRow Row = tDayDataSet.Addresses.FindByAddressId(this.AdressId);
            Row["Address"] = Addres;
            Row["City"] = City;
            Row["Province"] = Province;
            Row["Country"] = Country;
            Row["Phone"] = Phone;
            Row["Email"] = Email;
            Row["Cell"] = Cell;
            Row["PostalCode"] = PostalCode;
            addressesTableAdapter.Update(tDayDataSet.Addresses);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }
        ~Address()
        {
            Dispose(false);
        }
    }
}
