using KingdomConnections.Models;
using KingdomConnections_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingdomConnections.ViewModels
{
    public class VolunteerInfoVM
    {
        public VolunteerInfoPO VolunteerInfo { get; set; }

        public List<VolunteerInfoPO> AllVolunteerInfo { get; set; }

        public List<IVolunteerInfoDO> ListOfVolunteerDOs { get; set; }

        public long GetTopVolunteer { get; set; }

        public string ErrorMessage { get; set; }

        public VolunteerInfoVM()
        {
            AllVolunteerInfo = new List<VolunteerInfoPO>();

            ListOfVolunteerDOs = new List<IVolunteerInfoDO>();

            VolunteerInfo = new VolunteerInfoPO();
        }
    }
}