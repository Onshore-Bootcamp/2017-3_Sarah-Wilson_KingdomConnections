using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingdomConnections.Models
{
    public class VolunteerInfoPO
    {
        public long VolunteerID { get; set; }

        public int Time_Logged { get; set; }

        public long UserID { get; set; }

        public DateTime Date { get; set; }

        public int Time_Available { get; set; }

        public bool Flag { get; set; }


        //public DateTime Time { get; set; }
    }
}