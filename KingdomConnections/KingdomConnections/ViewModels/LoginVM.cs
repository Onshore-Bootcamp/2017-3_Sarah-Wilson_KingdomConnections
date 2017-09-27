using KingdomConnections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingdomConnections.ViewModels
{
    public class LoginVM
    {
        public  UserInfoPO LoginInfo { get; set; }

        public string ErrorMessage { get; set; }

    }
}


