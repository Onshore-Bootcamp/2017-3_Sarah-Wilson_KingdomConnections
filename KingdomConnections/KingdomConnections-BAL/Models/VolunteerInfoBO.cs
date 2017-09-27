using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_BAL.Models
{
   public class VolunteerInfoBO : IVolunteerInfoBO
    {
        public long VolunteerID { get; set; }

        public int Time_Logged { get; set; }

        public long UserID { get; set; }

        public DateTime Date { get; set; }

        public int Time_Available { get; set; }

        public bool Flag { get; set; }
    }
}
