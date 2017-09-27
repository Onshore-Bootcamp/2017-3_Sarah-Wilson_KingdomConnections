using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_DAL
{
    public interface IVolunteerInfoDO
    {
        long VolunteerID { get; set; }

        int Time_Logged { get; set; }

        long UserID { get; set; }

        DateTime Date { get; set; }

        int Time_Available { get; set; }

        bool Flag { get; set; }



    }
}
