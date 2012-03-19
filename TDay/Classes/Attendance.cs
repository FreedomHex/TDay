using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TDay
{
    public class Attendance
    {
        private bool disposed = false;
        TDayDataSet tDayDataSet = new TDayDataSet();
        TDayDataSetTableAdapters.AttendanceTableAdapter attendanceTableAdapter = new TDayDataSetTableAdapters.AttendanceTableAdapter();
        public int ProfileId{ get; set; }
        public int AttendanceId { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }

        public Attendance()
        {

        }

        public Attendance(int ProfileUID)
        {
            attendanceTableAdapter.Fill(tDayDataSet.Attendance);
            foreach (DataRow Row in tDayDataSet.Attendance)
            {
                if (Row["ProfileId"].ToString() == ProfileUID.ToString())
                {
                    Monday = bool.Parse(Row["Monday"].ToString());
                    Tuesday = bool.Parse(Row["Tuesday"].ToString());
                    Wednesday = bool.Parse(Row["Wednesday"].ToString());
                    Thursday = bool.Parse(Row["Thursday"].ToString());
                    Friday = bool.Parse(Row["Friday"].ToString());
                    AttendanceId = int.Parse(Row["AttendanceId"].ToString());
                    break;
                }
            }
        }
        public void AddAttendanceTo(Profile Profile)
        {
            attendanceTableAdapter.Insert(Profile.ProfileUID, Monday, Tuesday, Wednesday, Thursday, Friday);
        }
        public void Update()
        {
            attendanceTableAdapter.Fill(tDayDataSet.Attendance);
            DataRow Row = tDayDataSet.Attendance.FindByAttendanceId(AttendanceId);
            Row["Monday"] = Monday;
            Row["Tuesday"] = Tuesday;
            Row["Wednesday"] = Wednesday;
            Row["Thursday"] = Thursday;
            Row["Friday"] = Friday;
            attendanceTableAdapter.Update(tDayDataSet.Attendance);
        }

        public bool GetDay(int DW)
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
        ~Attendance()
        {
            Dispose(false);
        }
    }
}
