using KingdomConnections.Models;
using KingdomConnections_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KingdomConnections.ViewModels
{
    public class DonationInfoVM
    {
       public DonationInfoVM()
        {
            AllDonationInfo = new List<DonationInfoPO>();

            ListOfDonationDOs = new List<IDonationInfoDO>();

            DonationInfo = new DonationInfoPO();

           
        }

        public List<DonationInfoPO> ListOfAllDonations;

        public DonationInfoPO DonationInfo { get; set; }

        public List<DonationInfoPO> AllDonationInfo { get; set; }

        public List<IDonationInfoDO> ListOfDonationDOs { get; set; }

        public SelectList Departments { get; set; }

        public string  ErrorMessage { get; set; }

    }
}