using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDay
{
    public class Profile
    {
        public string Name                      { get; set; }
        public DateTime DateOfBirdh             { get; set; }
        public string Address                   { get; set; }
        public string City                      { get; set; }
        public string Province                  { get; set; }
        public string Country                   { get; set; }
        public string PostalCode                { get; set; }
        public string Phone                     { get; set; }
        public string Email                     { get; set; }
        public string EmergyContactName         { get; set; }
        public string EmergyContactPhone        { get; set; }
        public string Relation                  { get; set; }
        public Enums.Attendance Attendance      { get; set; }

    }
}
