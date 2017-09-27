using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_BAL
{
    interface IUserInfoBO
    {
        long UserID { get; set; }

        string Name { get; set; }

        string Street_Address { get; set; }

        string City_State_Zip { get; set; }

        string Phone_Number { get; set; }

        string Login { get; set; }

        string Password { get; set; }

        string Role { get; set; }

    }
}
