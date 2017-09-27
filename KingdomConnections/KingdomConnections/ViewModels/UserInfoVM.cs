using KingdomConnections.Models;
using System.Collections.Generic;
using KingdomConnections_DAL;

namespace KingdomConnections.ViewModels
{
    public class UserInfoVM
    {
        public List<UserInfoPO> ListOfAllUsers;

        public UserInfoPO UserInfo { get; set; }

        public List<UserInfoPO> AllUserInfo { get; set; }

        public List<IUserInfoDO> ListOfUserDOs { get; set; }
        
        public string ErrorMessage { get; set; }
        
    }
}