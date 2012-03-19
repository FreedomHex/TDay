using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class EmergencyContact
    {
        private bool disposed = false;
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.EmergencyContactsTableAdapter emergencyContactsTableAdapter = new TDayDataSetTableAdapters.EmergencyContactsTableAdapter();
        public int EmergencyId  { get; set; }
        public int ProfileId { get; set; }
        public string Name      { get; set; }
        public string Phone     { get; set; }
        public string Relation  { get; set; }

        public EmergencyContact()
        {
            
        }
        public EmergencyContact(int ProfileUID, bool DopEmergy)
        {
            emergencyContactsTableAdapter.Fill(tDayDataSet.EmergencyContacts);
            if (!DopEmergy)
            {
                foreach (DataRow Row in tDayDataSet.EmergencyContacts)
                {
                    if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                    {
                        Name = Row["EmergencyContactName"].ToString();
                        Phone = Row["EmergencyContactPhone"].ToString();
                        Relation = Row["Relation"].ToString();
                        EmergencyId = int.Parse(Row["EmergencyId"].ToString());
                        break;
                    }
                }
            }
            else
            {
                foreach (DataRow Row in tDayDataSet.EmergencyContacts)
                {
                    if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                    {
                        if (Row["EmergencyContactName"].ToString() != Name)
                        {
                            Name = Row["EmergencyContactName"].ToString();
                            Phone = Row["EmergencyContactPhone"].ToString();
                            Relation = Row["Relation"].ToString();
                            EmergencyId = int.Parse(Row["EmergencyId"].ToString());
                        }
                    }
                }
            }
        }
        public void AddEmergencyContactTo(Profile Profile)
        {
            emergencyContactsTableAdapter.Insert(Profile.ProfileUID, this.Name, this.Phone, this.Relation);
        }
        public void Update()
        {
            emergencyContactsTableAdapter.Fill(tDayDataSet.EmergencyContacts);
            DataRow Row = tDayDataSet.EmergencyContacts.FindByEmergencyId(EmergencyId);
            if (Row != null)
            {
                Row["EmergencyContactName"] = Name;
                Row["EmergencyContactPhone"] = Phone;
                Row["Relation"] = Relation;
                emergencyContactsTableAdapter.Update(tDayDataSet.EmergencyContacts);
            }
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
        ~EmergencyContact()
        {
            Dispose(false);
        }

    }
}
