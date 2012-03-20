using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Profile
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.ProfilesTableAdapter ProfilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
        TDayDataSetTableAdapters.TransportationTableAdapter transportationTableAdapter = new TDayDataSetTableAdapters.TransportationTableAdapter();
        public int ProfileUID                       { get; set; }
        public string Name                          { get; set; }
        public string Occupation                    { get; set; }
        public DateTime DateOfBirdh                 { get; set; }
        public Address Adress                       { get; set; }
        public EmergencyContact EmergencyContact    { get; set; }
        public Attendance Attendance                { get; set; }
        public bool DelStatus                       { get; set; }
        public Profile()
        {
            Occupation = String.Empty;
        }
        public Profile(int ProfileUID)
        {
            this.ProfileUID = ProfileUID;
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            foreach (DataRow Row in tDayDataSet.Profiles)
            {
                if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                {
                    this.Name = Row["Name"].ToString();
                    this.DateOfBirdh = DateTime.Parse(Row["BirthDate"].ToString());
                    this.Occupation = Row["Occupation"].ToString();
                    this.Adress = new Address(ProfileUID);
                    this.EmergencyContact = new EmergencyContact(ProfileUID, false);
                    this.Attendance = new Attendance(ProfileUID);
                    break;
                }
            }
        }

        public void Update()
        {
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            DataRow Row = tDayDataSet.Profiles.FindByProfileId(ProfileUID);
            Row["Name"] = Name;
            Row["BirthDate"] = DateOfBirdh;
            Row["Occupation"] = Occupation;
            ProfilesTableAdapter.Update(tDayDataSet.Profiles);
            Adress.Update();
            if (Attendance.AttendanceId!=0)
            {
                Attendance.Update();
            }
            if (EmergencyContact.EmergencyId!=0)
            {
                EmergencyContact.Update();
            }
        }
        public void Create(Enums.Category Category)
        {
            ProfilesTableAdapter.Insert(Name, (int)Category, DateOfBirdh, null, null, null, Occupation, null, null, null, null, null, null, null,null,false,false);
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            this.ProfileUID = tDayDataSet.Profiles[tDayDataSet.Profiles.Count - 1].ProfileId;
        }

        public void Delete()
        {
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            DataRow Row = tDayDataSet.Profiles.FindByProfileId(ProfileUID);
            Row["DelStatus"] = true;
            ProfilesTableAdapter.Update(tDayDataSet.Profiles);
            //ProfilesTableAdapter.Delete(ProfileUID);
            transportationTableAdapter.Delete(ProfileUID);
        }

    }
}
