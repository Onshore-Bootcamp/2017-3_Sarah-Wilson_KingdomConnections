using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KingdomConnections_DAL;
using KingdomConnections.ViewModels;
using KingdomConnections.Custom;
using KingdomConnections.Models;
using KingdomConnections_BAL;
using System.IO;

namespace KingdomConnections.Controllers
{
    public class VolunteerController : Controller
    {

        VolunteerDataAccess VolunteerAccess = new VolunteerDataAccess();
        VolunteerBuisnessLogic VolunteerBuisness = new VolunteerBuisnessLogic();

        // GET: Volunteer
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult CreateVolunteer(long userID)
        {
            ActionResult oResponse = null;
            if (Session["UserName"] != null && ((string)Session["Role"] == "user" || (string)Session["Role"] == "power" || (string)Session["Role"] == "admin"))
            {
                VolunteerInfoVM volunteerInfoVM = new VolunteerInfoVM();
                volunteerInfoVM.VolunteerInfo.UserID = userID;
                oResponse = View(volunteerInfoVM);
            }
            else
            {
                oResponse = RedirectToAction("ObtainLogin", "Users");
            }
            return oResponse;

        }

        [HttpPost]
        public ActionResult CreateVolunteer(VolunteerInfoVM iVolunteerInfoVM)
        {
            ActionResult oResponse = null;
            if (Session["UserName"] != null && ((string)Session["Role"] == "user" || (string)Session["Role"] == "power" || (string)Session["Role"] == "admin"))
            {
                IVolunteerInfoDO lVolunteerForm = Map.MapVolunteerInfoPOtoDO(iVolunteerInfoVM.VolunteerInfo);


                if (ModelState.IsValid)
                {
                    try
                    {
                        VolunteerAccess.CreateVolunteer(lVolunteerForm);
                        oResponse = RedirectToAction("Index", "Home");

                    }
                    catch (Exception e)
                    {
                        //catch the error and show user this message
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files"))
                        {
                            fileWriter.WriteLine(e.Message);
                        }

                        iVolunteerInfoVM.ErrorMessage = "Let each of you look not ony to his own intrest, but also to the intrestst of others.  Philippians 2:4";
                        oResponse = View(iVolunteerInfoVM);
                    }
                    finally
                    {
                    }
                }
                else
                {
                    oResponse = View(iVolunteerInfoVM);
                }

            }
            else
            {
                oResponse = RedirectToAction("ObtainLogin", "Users");
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewAllVolunteers()
        {

            //Instantiate a new VM
            VolunteerInfoVM viewAllVolunteers = new VolunteerInfoVM();

            try
            {
                //Make a data call
                List<IVolunteerInfoDO> allVolunteers = VolunteerAccess.ViewAllVolunteers();
                //Method call to map data
                List<VolunteerInfoPO> listOfVolunteerPOs = Map.MapListOfVolunteerDOsToListOfVolunteerPOs(allVolunteers);

                viewAllVolunteers.AllVolunteerInfo = listOfVolunteerPOs;
            }
            catch (Exception e)
            {
                //catch the error and show user this message
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files"))
                {
                    fileWriter.WriteLine(e.Message);
                }
                viewAllVolunteers.ErrorMessage = "We are unable to process your request at this time, please try back at anthor time.";
            }
            finally
            {
                //We're not doing anything else
            }
            return View(viewAllVolunteers);
        }

        [HttpGet]
        public ActionResult DeleteVolunteerByID(long VolunteerID)
        {
            ActionResult oReturn = null;

            try
            {

                VolunteerAccess.DeleteVolunteerByID(VolunteerID);

            }
            catch (Exception e)
            {
                //catch the error and show user this message
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                {
                    fileWriter.WriteLine(e.Message);
                }

            }
            finally
            {
                oReturn = RedirectToAction("ViewAllVolunteers", "Volunteer");
            }
            return oReturn;
        }



        [HttpGet]
        public ActionResult UpdateVolunteerByID(int VolunteerID)
        {
           
            VolunteerInfoVM volunteerInfoVM = new VolunteerInfoVM();
            IVolunteerInfoDO volunteer = VolunteerAccess.ViewVolunteerByID(VolunteerID);
            volunteerInfoVM.VolunteerInfo = Map.MapVolunteerInfoDOtoPO(volunteer);
            return View(volunteerInfoVM);
        }

        [HttpPost]
        public ActionResult UpdateVolunteerByID(VolunteerInfoVM iViewModel)
        {
            IVolunteerInfoDO lVolunteerForm = Map.MapVolunteerInfoPOtoDO(iViewModel.VolunteerInfo);
            ActionResult oResponse = null;

            if (ModelState.IsValid)
            {
                try
                {

                    VolunteerAccess.UpdateVolunteerByID(lVolunteerForm);
                    oResponse = RedirectToAction("ViewAllVolunteers", "Volunteer");
                }
                catch (Exception e)
                {
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\SarahBootCamp\Documents\C#\KingdomConnections\log files"))
                    {
                        fileWriter.WriteLine(e.Message);
                    }
                    iViewModel.ErrorMessage = "We Apologize, but we were unable to handle your request at this time.";
                    ModelState.AddModelError("VolunteerInfo.Date", "We Apologize, but we were unable to handle your request at this time.");
                }
                finally
                {
                    //if null, then there was no error
                    if (iViewModel.ErrorMessage == null)
                    {
                        oResponse = RedirectToAction("ViewAllVolunteers", "Volunteer");
                    }
                    //If not null, there WAS an error 
                    else
                    {
                        oResponse = View(iViewModel);
                    }
                }

            }
            else
            {
                oResponse = View(iViewModel);
            }
            return oResponse;
        }
    }
}