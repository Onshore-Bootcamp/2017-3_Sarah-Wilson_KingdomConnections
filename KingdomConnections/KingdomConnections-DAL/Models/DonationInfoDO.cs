using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_DAL.Models
{
    public class DonationInfoDO : IDonationInfoDO
    {
        public long DonationID { get; set; }

        public long UserID { get; set; }

        public decimal Amount_of_Donation { get; set; }

        public DateTime Date_of_Donation { get; set; }

        public bool Tax_Receipt_Given { get; set; }

        public string Department_Need { get; set; }


    }
}
