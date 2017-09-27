using KingdomConnections_BAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KingdomConnections_BAL
{
    public class VolunteerBuisnessLogic
    {
        // Write a method to get the Top Volunteer 
        public long GetTopVolunteer(List<IVolunteerInfoBO> mylist)
        {
            long UserId = 0;
            long Time_Logged = 0;
            try
            {
                IEnumerable<IGrouping<long, IVolunteerInfoBO>> volunteerList = mylist.GroupBy(list => list.UserID);

                foreach (IGrouping<long, IVolunteerInfoBO> volunteer in volunteerList)
                {
                    if (volunteer.Sum(x => x.Time_Logged) > Time_Logged)
                    {
                        UserId = volunteer.Key;
                        Time_Logged = volunteer.Sum(x => x.Time_Logged);
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }
            catch (Exception e)
            {
                //Log Error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                {
                    fileWriter.WriteLine(e.Message);
                }
                throw e;
            }
            finally
            {
                //do nothing else
            }
            return UserId;
        }
    }
}










