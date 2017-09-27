using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_DAL.Models
{
     public class UserInfoDO : IUserInfoDO
    {
        public long UserID { get; set; }

        public string Name { get; set; }

        public string Street_Address { get; set; }

        public string City_State_Zip { get; set; }

        public string Phone_Number { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
