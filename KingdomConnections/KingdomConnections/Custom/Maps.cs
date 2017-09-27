using KingdomConnections.Models;
using System.Collections.Generic;
using KingdomConnections_DAL;
using KingdomConnections_DAL.Models;
using KingdomConnections.ViewModels;
using KingdomConnections_BAL;
using KingdomConnections_BAL.Models;
using KingdomConnections.Custom;
using System;

namespace KingdomConnections.Custom
{
    public class Map
    {
        // Method to map UserInfoPO to a UserInfoDO
        public static IUserInfoDO MapUserInfoPOtoDO(UserInfoPO iUserInfo)
        {
            IUserInfoDO oUser = new UserInfoDO();

            oUser.UserID = iUserInfo.UserID;
            oUser.Name = iUserInfo.Name;
            oUser.Street_Address = iUserInfo.Street_Address;
            oUser.City_State_Zip = iUserInfo.City_State_Zip;
            oUser.Phone_Number = iUserInfo.Phone_Number;
            oUser.Login = iUserInfo.Login;
            oUser.Password = iUserInfo.Password;
            oUser.Role = iUserInfo.Role;

            return oUser;

        }

        //Method to Map UserInfoDO to a UserInfoPO 
        public static UserInfoPO MapUserInfoDOtoPO(IUserInfoDO iUserInfo)
        {
            //new class Instantiation 
            UserInfoPO oUser = new UserInfoPO();

            oUser.UserID = iUserInfo.UserID;
            oUser.Name = iUserInfo.Name;
            oUser.Street_Address = iUserInfo.Street_Address;
            oUser.City_State_Zip = iUserInfo.City_State_Zip;
            oUser.Phone_Number = iUserInfo.Phone_Number;
            oUser.Login = iUserInfo.Login;
            oUser.Password = iUserInfo.Password;
            oUser.Role = iUserInfo.Role;

            return oUser;
        }

        // Method to Map a list of UserInfoDOs to a list of UserInfoPOs
        public static List<UserInfoPO> MapListOfDOsToListOfPOs(List<IUserInfoDO> iUserDOs)
        {
            //New List instantiantion
            List<UserInfoPO> listOfUserPOs = new List<UserInfoPO>();
            //Foreach loop to map each object in the list 
            foreach (IUserInfoDO user in iUserDOs)
            {
                UserInfoPO userPO = MapUserInfoDOtoPO(user);
                listOfUserPOs.Add(userPO);
            }
            return listOfUserPOs;
        }

        //Method for Mapping DonationInfoPO to a DonationInfoDO 
        public static IDonationInfoDO MapDonationInfoPOtoDO(DonationInfoPO iDonationInfo)
        {
            IDonationInfoDO oDonation = new DonationInfoDO();

            oDonation.DonationID = iDonationInfo.DonationID;
            oDonation.UserID = iDonationInfo.UserID;
            oDonation.Amount_of_Donation = iDonationInfo.Amount_of_Donation;
            oDonation.Date_of_Donation = iDonationInfo.Date_of_Donation;
            oDonation.Tax_Receipt_Given = iDonationInfo.Tax_Receipt_Given;
            oDonation.Department_Need = iDonationInfo.Department_Need;

            return oDonation;
        }

        //Method for Mapping DonationInfoDO to a DonationInfoPO 
        public static DonationInfoPO MapDonationInfoDOtoPO(IDonationInfoDO iDonationInfo)
        {
            DonationInfoPO oDonation = new DonationInfoPO();

            oDonation.DonationID = iDonationInfo.DonationID;
            oDonation.UserID = iDonationInfo.UserID;
            oDonation.Amount_of_Donation = iDonationInfo.Amount_of_Donation;
            oDonation.Date_of_Donation = iDonationInfo.Date_of_Donation;
            oDonation.Tax_Receipt_Given = iDonationInfo.Tax_Receipt_Given;
            oDonation.Department_Need = iDonationInfo.Department_Need;

            return oDonation;
        }


        // Method to Map a list of DonationInfoDOs to a list of DonationInfoPOs
        public static List<DonationInfoPO> MapListOfDonationDOsToListOfPOs(List<IDonationInfoDO> iDonationDOs)
        {
            //New List instantiantion
            List<DonationInfoPO> listOfDonationPOs = new List<DonationInfoPO>();
            //Foreach loop to map each object in the list 
            foreach (IDonationInfoDO donation in iDonationDOs)
            {
                DonationInfoPO donationPO = MapDonationInfoDOtoPO(donation);
                listOfDonationPOs.Add(donationPO);
            }
            return listOfDonationPOs;
        }


        // Method to map VolunteerInfoPO to a VolunteerInfoDO
        public static IVolunteerInfoDO MapVolunteerInfoPOtoDO(VolunteerInfoPO iVolunteerInfo)
        {
            KingdomConnections_DAL.IVolunteerInfoDO oVolunteer = new VolunteerInfoDO();

            oVolunteer.VolunteerID = iVolunteerInfo.VolunteerID;
            oVolunteer.Time_Logged = iVolunteerInfo.Time_Logged;
            oVolunteer.UserID = iVolunteerInfo.UserID;
            oVolunteer.Time_Available = iVolunteerInfo.Time_Available;
            oVolunteer.Date = iVolunteerInfo.Date;
            oVolunteer.Flag = iVolunteerInfo.Flag;
                
                            

            return oVolunteer;

        }


        //Method to Map a VolunteerInfoDO to a VolunteerInfoPO 
        public static VolunteerInfoPO MapVolunteerInfoDOtoPO(KingdomConnections_DAL.IVolunteerInfoDO iVolunteerInfo)
        {
            //new class Instantiation 
            VolunteerInfoPO oVolunteer = new VolunteerInfoPO();

            oVolunteer.VolunteerID = iVolunteerInfo.VolunteerID;
            oVolunteer.Time_Logged = iVolunteerInfo.Time_Logged;
            oVolunteer.UserID = iVolunteerInfo.UserID;
            oVolunteer.Time_Available = iVolunteerInfo.Time_Available;
            oVolunteer.Date = iVolunteerInfo.Date;
            oVolunteer.Flag = iVolunteerInfo.Flag;



            return oVolunteer;
        }

        // Method to Map a list of VolunteerInfoDOs to a list of VolunteerInfoPOs
        public static List<VolunteerInfoPO> MapListOfVolunteerDOsToListOfVolunteerPOs(List<KingdomConnections_DAL.IVolunteerInfoDO> iVolunteerDOs)
        {
            //New List instantiantion
            List<VolunteerInfoPO> listOfVolunteerPOs = new List<VolunteerInfoPO>();
            //Foreach loop to map each object in the list 
            foreach (KingdomConnections_DAL.IVolunteerInfoDO volunteer in iVolunteerDOs)
            {
                VolunteerInfoPO volunteerPO = MapVolunteerInfoDOtoPO(volunteer);
                listOfVolunteerPOs.Add(volunteerPO);
            }
            return listOfVolunteerPOs;
        }





        // Method to map VolunteerInfoPO to a VolunteerInfoBO
        public static IVolunteerInfoBO MapVolunteerInfoPOtoBO(VolunteerInfoPO iVolunteerInfo)
        {
            IVolunteerInfoBO oVolunteer = new VolunteerInfoBO();


            oVolunteer.VolunteerID = iVolunteerInfo.VolunteerID;
            oVolunteer.Time_Logged = iVolunteerInfo.Time_Logged;
            oVolunteer.UserID = iVolunteerInfo.UserID;
            oVolunteer.Time_Available = iVolunteerInfo.Time_Available;
            oVolunteer.Date = iVolunteerInfo.Date;



            return oVolunteer;

        }



        //Method to Map VolunteerInfoBO to a VolunteerInfoDO 
        public static VolunteerInfoBO MapVolunteerInfoDOtoBO(IVolunteerInfoDO iVolunteerInfo)
       {
            //new class Instantiation 
            VolunteerInfoBO oVolunteer = new VolunteerInfoBO();

           oVolunteer.VolunteerID = iVolunteerInfo.VolunteerID;
           oVolunteer.Time_Logged = iVolunteerInfo.Time_Logged;
           oVolunteer.UserID = iVolunteerInfo.UserID;
            oVolunteer.Time_Available = iVolunteerInfo.Time_Available;
           oVolunteer.Date = iVolunteerInfo.Date;



           return oVolunteer;
        }

        // Method to Map a list of VolunteerInfoDOs to a list of VolunteerInfoBOs
        public static List<IVolunteerInfoBO> MapListOfVolunteerDOsToListOfVolunteerBOs(List<IVolunteerInfoDO> iVolunteerDOs)
       {
            //New List instantiantion
            List<IVolunteerInfoBO> listOfVolunteerBOs = new List<IVolunteerInfoBO>();
           //Foreach loop to map each object in the list 
           foreach (IVolunteerInfoDO volunteer in iVolunteerDOs)
           {

                IVolunteerInfoBO volunteerBO = MapVolunteerInfoDOtoBO(volunteer);
                listOfVolunteerBOs.Add(volunteerBO);
           }
            return listOfVolunteerBOs;
       }
     
    }
}







