using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_DAL
{
    public interface IDonationInfoDO
    {
        long DonationID { get; set; }

        long UserID { get; set; }

        decimal Amount_of_Donation { get; set; }

        DateTime Date_of_Donation { get; set; }

        bool Tax_Receipt_Given { get; set; }

        string Department_Need { get; set; }
    }
}