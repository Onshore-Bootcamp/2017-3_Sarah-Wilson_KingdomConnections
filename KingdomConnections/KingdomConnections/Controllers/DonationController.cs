using KingdomConnections.Custom;
using KingdomConnections.Models;
using KingdomConnections.ViewModels;
using KingdomConnections_DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KingdomConnections.Controllers
{
    public class DonationController : Controller
    {

        UserDataAccess UserAccess = new UserDataAccess();
        DonationDataAccess DonationAccess = new DonationDataAccess();


        // GET: Donation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateDonation(long userID)
        {
            ActionResult oResponse = null;
            //todo: session check to redirect users who are not logged in, because you cannot donate if you are not logged in.
            if (Session["UserName"] != null && ((string)Session["Role"] == "user" || (string)Session["Role"] == "power" || (string)Session["Role"] == "admin"))
            {

                DonationInfoVM donationInfoVM = new DonationInfoVM();
                donationInfoVM.DonationInfo.UserID = userID;
                donationInfoVM.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
                oResponse = View(donationInfoVM);
            }
            else
            {
                oResponse = RedirectToAction("ObtainLogin", "Users");
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult CreateDonation(DonationInfoVM iDonationInfoView)
        {
            ActionResult oResponse = null;
            if (Session["UserName"] != null && ((string)Session["Role"] == "user" || (string)Session["Role"] == "power" || (string)Session["Role"] == "admin"))
            {
                IDonationInfoDO lDonationForm = Map.MapDonationInfoPOtoDO(iDonationInfoView.DonationInfo);
                

                if (ModelState.IsValid)
                {
                    try
                    {
                        DonationAccess.CreateDonation(lDonationForm);
                        oResponse = RedirectToAction("Profile", "Users");
                    }
                    catch (Exception e)
                    {
                        //catch the error and show user this message
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files"))
                        {
                            fileWriter.WriteLine(e.Message);
                        }
                        iDonationInfoView.ErrorMessage = "We are unable to process your request at this time, please try back at anthor time.";
                        oResponse = View(iDonationInfoView);

                        //fill drop down
                        iDonationInfoView.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
                        oResponse = View(iDonationInfoView);
                    }


                    finally
                    {
                    }
                }
                else
                {
                    iDonationInfoView.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
                     oResponse = View(iDonationInfoView);
                    //fill drop down
                }

            }
            else
            {
                oResponse = RedirectToAction("ObtainLogin", "Users");
            }

            return oResponse;

        }

        [HttpGet]
        public ActionResult ViewAllDonations()
        {
            ActionResult oResponse = null;

            if (Session["UserName"] != null && (string)Session["Role"] == "power" || (string)Session["Role"] == "admin")
            {  
                //Instantiate a new VM
                DonationInfoVM viewAllDonations = new DonationInfoVM();
                try
                {
                    //Make a data call
                    List<IDonationInfoDO> allDonations = DonationAccess.ViewAllDonations();
                    //Method call to map data
                    List<DonationInfoPO> listOfDonationPOs = Map.MapListOfDonationDOsToListOfPOs(allDonations);
                    viewAllDonations.ListOfAllDonations = listOfDonationPOs;
                    return View(viewAllDonations);
                }
                catch (Exception e)
                {
                    //catch the error and show user this message
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files"))
                    {
                        fileWriter.WriteLine(e.Message);
                    }
                    viewAllDonations.ErrorMessage = "We are unable to process your request at this time, please try back at anthor time.";
                }
                finally
                {
                    //We're not doing anything else
                   
                }
            }
            else
            {
                oResponse = RedirectToAction("ObtainLogin", "Users");
            }

            return oResponse;

        }
        [HttpGet]
        public ActionResult DeleteDonationByID(long donationID)
        {
            ActionResult oReturn = null;
            if (Session["UserName"] != null && (string)Session["Role"] == "admin")
            {
                try
                {

                    DonationAccess.DeleteDonationByID(donationID);
                    oReturn = RedirectToAction("ViewAllDonations", "Donation");
                }
                catch (Exception e)
                {
                    //catch the error 
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                    {
                        fileWriter.WriteLine(e.Message);
                    }
                   
                }
                finally
                {

                }

            }
            else
            {
                oReturn = RedirectToAction("ViewAllDonations", "Donation");
            }
            return oReturn;
        }

        [HttpGet]
        public ActionResult UpdateDonationsByID(int DonationID)
        {
            DonationInfoVM donationInfoVM = new DonationInfoVM();
          donationInfoVM.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
            IDonationInfoDO donation = DonationAccess.ViewDonationByID(DonationID);
            donationInfoVM.DonationInfo = Map.MapDonationInfoDOtoPO(donation);
            return View(donationInfoVM);
        }

        [HttpPost]
        public ActionResult UpdateDonationsByID(DonationInfoVM iViewModel)
        {
            IDonationInfoDO lDonationForm = Map.MapDonationInfoPOtoDO(iViewModel.DonationInfo);
            ActionResult oResponse = null;

            if (ModelState.IsValid)
            {
                try
                {
                    DonationAccess.UpdateDonationByID(lDonationForm);
                    oResponse = RedirectToAction("ViewAllDonations", "Donation");
                }
                catch
                {
                    //Catch the error and show the error message to the user
                    iViewModel.ErrorMessage = "We Apologize, but we were unable to handle your request at this time.";
                    iViewModel.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
                }
                finally
                {
                    //if null, then there was no error
                    if (iViewModel.ErrorMessage == null)
                    {
                        oResponse = RedirectToAction("ViewAllDonations", "Donation");
                    }
                    //If not null, there WAS an error 
                    else
                    {
                        iViewModel.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
                        oResponse = View(iViewModel);
                    }

                }

            }
            else
            {
                iViewModel.Departments = new SelectList(new List<string>() { "Food Bank", "Missions", "Children", "Housing", "Praise Jam", "Bible Study" });
                oResponse = View(iViewModel);
            }
            return oResponse;
        }


    }

}




