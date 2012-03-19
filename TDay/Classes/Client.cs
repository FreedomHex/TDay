using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    class Client:Profile
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.ProfilesTableAdapter ProfilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
        
        public string ParisNumber                           { get; set; }
        public string DoctorName                            { get; set; }
        public string DoctorPhone                           { get; set; }
        public string PharmacistName                        { get; set; }
        public string PharmacistPhone                       { get; set; }
        public Transportation Transportation { get; set; }
        public bool Member                                  { get; set; }
        public EmergencyContact DopEmergencyContact         { get; set; }

        public Client()
        {
        }
        public Client(int ProfileUID)
        {
            this.ProfileUID = ProfileUID;
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            foreach (DataRow Row in tDayDataSet.Profiles)
            {
                if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                {
                    this.Name = Row["Name"].ToString();
                    this.DateOfBirdh = DateTime.Parse(Row["BirthDate"].ToString());
                    this.ParisNumber = Row["PARISNumber"].ToString();
                    this.Member = bool.Parse(Row["Member"].ToString());
                    this.DoctorName = Row["DoctorName"].ToString();
                    this.DoctorPhone = Row["DoctorPhone"].ToString();
                    this.PharmacistName = Row["PharmacistName"].ToString();
                    this.PharmacistPhone = Row["PharmacistPhone"].ToString();
                    this.Adress = new Address(ProfileUID);
                    this.EmergencyContact = new EmergencyContact(ProfileUID, false);
                    this.DopEmergencyContact = new EmergencyContact(ProfileUID, true);
                    if (DopEmergencyContact.EmergencyId == EmergencyContact.EmergencyId)
                    {
                        DopEmergencyContact = null;
                    }
                    this.Attendance = new Attendance(ProfileUID);
                    this.Transportation = new Transportation(ProfileUID);
                }
            }
        }
        public void Create()
        {
            try
            {
                ProfilesTableAdapter.Insert(this.Name, (int)Enums.Category.Client, this.DateOfBirdh, null, null, null, null, null, this.ParisNumber, DoctorName, DoctorPhone, PharmacistName, PharmacistPhone, null,Member,false);
                ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
                this.ProfileUID = tDayDataSet.Profiles[tDayDataSet.Profiles.Count - 1].ProfileId;
            }
            catch (Exception ex)
            {
                ErrorProvider.SetException(Enums.ExceptionType.FunctionException, ex);
            }
        }
        new public void Update()
        {
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            DataRow Row = tDayDataSet.Profiles.FindByProfileId(ProfileUID);
            Row["Name"] = Name;
            Row["BirthDate"] = DateOfBirdh;
            Row["PARISNumber"] = ParisNumber;
            Row["Member"] = Member;
            Row["DoctorName"] = DoctorName;
            Row["DoctorPhone"] = DoctorPhone;
            Row["PharmacistName"] = PharmacistName;
            Row["PharmacistPhone"] = PharmacistPhone;
            ProfilesTableAdapter.Update(tDayDataSet.Profiles);
            Adress.Update();
            Attendance.Update();
            EmergencyContact.Update();
            if (DopEmergencyContact != null)
            {
                DopEmergencyContact.Update();
            }
            Transportation.Update();
        }

       
    }
}
