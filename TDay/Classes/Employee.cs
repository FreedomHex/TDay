using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Employee:Profile
    {
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.ProfilesTableAdapter ProfilesTableAdapter = new TDayDataSetTableAdapters.ProfilesTableAdapter();
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public string SIN { get; set; }
        public string PositionType { get; set; }

        public Employee()
        {

        }

        public Employee(int ProfileUID)
        {
            this.ProfileUID = ProfileUID;
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            foreach (DataRow Row in tDayDataSet.Profiles)
            {
                if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                {
                    this.Name = Row["Name"].ToString();
                    this.DateOfBirdh = DateTime.Parse(Row["BirthDate"].ToString());
                    this.HireDate = DateTime.Parse(Row["HireDate"].ToString());
                    this.Position = Row["Position"].ToString();
                    this.PositionType = Row["PositionType"].ToString();
                    this.SIN = Row["SIN"].ToString();
                    this.Adress = new Address(ProfileUID);
                    this.EmergencyContact = new EmergencyContact(ProfileUID, false);
                    this.Attendance = new Attendance(ProfileUID);
                    break;
                }
            }

        }

        public new void Update()
        {
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            DataRow Row = tDayDataSet.Profiles.FindByProfileId(ProfileUID);
            Row["Name"] = Name;
            Row["BirthDate"] = DateOfBirdh;
            Row["HireDate"] = HireDate;
            Row["Position"] = Position;
            Row["PositionType"] = PositionType;
            Row["SIN"] = SIN;
            ProfilesTableAdapter.Update(tDayDataSet.Profiles);
            Adress.Update();
            Attendance.Update();
            EmergencyContact.Update();
        }

        public void Create()
        {
            ProfilesTableAdapter.Insert(this.Name, (int)Enums.Category.Employee, this.DateOfBirdh, this.HireDate, Position, PositionType, null, SIN, null, null, null, null, null, null,null,false);
            ProfilesTableAdapter.Fill(tDayDataSet.Profiles);
            this.ProfileUID = tDayDataSet.Profiles[tDayDataSet.Profiles.Count - 1].ProfileId;
        }
    }
}
