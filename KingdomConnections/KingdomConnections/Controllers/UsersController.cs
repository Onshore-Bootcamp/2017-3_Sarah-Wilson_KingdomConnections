using KingdomConnections.Custom;
using KingdomConnections.Models;
using KingdomConnections.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using KingdomConnections_DAL;
using System;
using System.Web;
using System.Web.Security;
using System.IO;

namespace KingdomConnections.Controllers
{
    public class UsersController : Controller
    {
        UserDataAccess UserAccess = new UserDataAccess();



        //Get:User
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserInfo"] == null)
            {
                return RedirectToAction("ObtainLogin", "Users");
            }
            return View();

        }

        [HttpGet]
        public ActionResult CreateUser()
        {

            UserInfoVM userInfoVM = new UserInfoVM();
            return View(userInfoVM);
        }

        [HttpPost]
        public ActionResult CreateUser(UserInfoVM iUserInfoVM)
        {
            IUserInfoDO lUserForm = Map.MapUserInfoPOtoDO(iUserInfoVM.UserInfo);
            ActionResult oResponse = null;

            if (ModelState.IsValid)
            {
                try
                {
                    UserAccess.CreateUser(lUserForm);
                    

                    if (Session["UserName"] != null && (string)Session["Role"] != "user")
                    {
                        oResponse = RedirectToAction("ViewAllUsers", "Users");

                    }
                    else
                    {
                        oResponse = RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception e)
                {
                    //Catch the exception and show the error message to the user

                    iUserInfoVM.ErrorMessage = "Let each of you look not ony to his own intrest, but also to the intrestst of others.  Philippians 2:4";


                }
                finally
                {

                }
            }
            else
            {
                oResponse = View(iUserInfoVM);
                
            }

            return oResponse = RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ViewAllUsers()
        {
            ActionResult oResponse = null;
            if (Session["UserName"] != null && (string)Session["Role"] == "admin" || (string)Session["Role"] == "power")
            {
                //Instantiate a new VM
                UserInfoVM viewAllUsers = new UserInfoVM();

                try
                {
                    //Make a data call
                    List<IUserInfoDO> allUsers = UserAccess.ViewAllUsers();
                    //Method call to map data
                    List<UserInfoPO> listOfUserPOs = Map.MapListOfDOsToListOfPOs(allUsers);

                    viewAllUsers.ListOfAllUsers = listOfUserPOs;
                    return View(viewAllUsers);

                }
                catch (Exception e)
                {
                    //catch the error and show user this message
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                    {
                        fileWriter.WriteLine(e.Message);
                    }
                    viewAllUsers.ErrorMessage = "We are unable to process your request at this time, please try back at anthor time.";


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
        public ActionResult DeleteUserByID(long UserID)
        {

            ActionResult oReturn = null;
            if (Session["UserName"] != null && (string)Session["Role"] == "admin")
            {
                try
                {
                    IUserInfoDO userInfo = UserAccess.ViewUserByID(UserID);
                    if (userInfo != null && userInfo.Role != "admin")
                    {
                        UserAccess.DeleteUserByID(UserID);
                        oReturn = RedirectToAction("ViewAllUsers", "Users");

                    }
                    else
                    {
                        oReturn = RedirectToAction("ViewAllUsers", "Users");
                    }

                }
                catch (Exception e)
                {
                    //catch the error and show user this message
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files"))
                    {
                        fileWriter.WriteLine(e.Message);
                    }

                    //todo: print a message unable to delete at this time 
                }

                finally
                {

                }

            }
            else
            {
                oReturn = RedirectToAction("ViewAllUsers", "Users");
            }

            return oReturn;
        }

        [HttpGet]
        public ActionResult UpdateUserByID(int UserID)
        {
            ActionResult oResponse = null;

            if (Session["UserName"] != null && (string)Session["Role"] == "admin" || (string)Session["Role"] == "power")
            {

                UserInfoVM userInfoVM = new UserInfoVM();
                IUserInfoDO user = UserAccess.ViewUserByID(UserID);
                userInfoVM.UserInfo = Map.MapUserInfoDOtoPO(user);
                return View(userInfoVM);

            }
            else
            {
                oResponse = RedirectToAction("ViewAllUsers", "Users");
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateUserByID(UserInfoVM iViewModel)
        {
            IUserInfoDO lUserForm = Map.MapUserInfoPOtoDO(iViewModel.UserInfo);
            ActionResult oResponse = null;
            if (Session["UserName"] != null && (string)Session["Role"] == "admin" || (string)Session["Role"] == "power")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        UserAccess.UpdateUserByID(lUserForm);
                    }
                    catch
                    {
                        //Catch the error and show the error message to the user
                        iViewModel.ErrorMessage = "We Apologize, but we were unable to handle your request at this time.";
                    }
                    finally
                    {
                        //if null, then there was no error
                        if (iViewModel.ErrorMessage == null)
                        {
                            oResponse = RedirectToAction("ViewAllUsers", "Users");
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

            }
            else
            {
                oResponse = RedirectToAction("ObtainLogin", "Users");
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ObtainLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ObtainLogin(LoginVM iLogin)
        {
            ActionResult oResponse = null;

            if (ModelState.IsValid)
            {
                IUserInfoDO login = UserAccess.ViewUserByLogin(iLogin.LoginInfo.Login);

                if (login != null && iLogin.LoginInfo.Password.Equals(login.Password))
                {

                    Session["UserName"] = login.Login;
                    Session["UserID"] = login.UserID;
                    //Session["Password"] = iLogin.LoginInfo.Password;
                    Session["Role"] = login.Role;
                    Session.Timeout = 10;

                    //todo: Make Home page for redirect
                    oResponse = RedirectToAction("Index", "Home");
                }
                else
                {
                    oResponse = View(iLogin);
                }
            }
            else
            {
                oResponse = View(iLogin);
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return View();
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            UserInfoVM viewUserProfile = new UserInfoVM();
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("ObtainLogin", "Users");
                }
                else
                {

                }

                long userID = (long)Session["UserID"];
                //Make a data call
                IUserInfoDO profile = UserAccess.ViewUserByID(userID);
                //Method call to map data
                UserInfoPO userPO = Map.MapUserInfoDOtoPO(profile);

                viewUserProfile.UserInfo = userPO;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //We're not doing anything else
            }
            return View(viewUserProfile);
        }
    }
}