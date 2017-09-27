using System.Collections.Generic;
using System.Web.Mvc;
using KingdomConnections_DAL;
using KingdomConnections.Custom;
using KingdomConnections_BAL;
using System;
using System.IO;
using KingdomConnections.ViewModels;
using KingdomConnections.Models;

namespace KingdomConnections.Controllers
{
    public class HomeController : Controller
    {
        VolunteerDataAccess VolunteerAccess = new VolunteerDataAccess();
        VolunteerBuisnessLogic VolunteerBuisness = new VolunteerBuisnessLogic();
        UserDataAccess UserAccess = new UserDataAccess();
        UserInfoVM userInfoVM = new UserInfoVM();
        public ActionResult Index()
        {
            try
            {
                // Get the list of Volunteers and map your list

                List<IVolunteerInfoBO> volunteerList = Map.MapListOfVolunteerDOsToListOfVolunteerBOs(VolunteerAccess.ViewAllVolunteers());
                long TopVolunteer = VolunteerBuisness.GetTopVolunteer(volunteerList);
                if (TopVolunteer != 0)
                {
                    IUserInfoDO UserInfo = UserAccess.ViewUserByID(TopVolunteer);

                    TempData.Add("VolunteerName", UserInfo.Name);

                }
                else
                {
                    //do nothing 
                }


            }

            catch (Exception e)
            {
                //Log Error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files"))
                {
                    fileWriter.WriteLine(e.Message);
                }
        
            }
            finally
            {

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}