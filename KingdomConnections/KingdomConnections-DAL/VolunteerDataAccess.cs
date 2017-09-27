using KingdomConnections_DAL;
using KingdomConnections_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingdomConnections_DAL
{
    public class VolunteerDataAccess
    {
        //Define the Connection String 
        public string connectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=KingdomConnections; Trusted_Connection=True;";

        //Method to Creating a New Volunteer
        public void CreateVolunteer(IVolunteerInfoDO volunteer)
        {
            try
            {

                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("CREATE_VOLUNTEER", connectToSql))
                    {
                        try
                        {
                            //Command parameters go here passing in the values that I recieved from the user
                            
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 25;
                            command.Parameters.AddWithValue("@Time_Logged", volunteer.Time_Logged);
                            command.Parameters.AddWithValue("@UserID", volunteer.UserID);
                            command.Parameters.AddWithValue("@Date", volunteer.Date);
                            command.Parameters.AddWithValue("@Time_Available", volunteer.Time_Available);
                            command.Parameters.AddWithValue("@Flag", volunteer.Flag);



                            connectToSql.Open();
                            command.ExecuteNonQuery();

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            //close and dispose

                            command.Dispose();
                            connectToSql.Close();
                            connectToSql.Dispose();
                        }


                    }
                }

            }
            catch (Exception e)

            { //Log Error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                {
                    fileWriter.WriteLine(e.Message);
                }
                throw e;
            }
            finally
            {

            }

        }

        // Method to View All Volunteers
        public List<IVolunteerInfoDO> ViewAllVolunteers()
        {
            // Instantiate a List of Users
            List<IVolunteerInfoDO> listOfVolunteers = new List<IVolunteerInfoDO>();

            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("View_All_VOLUNTEERS", connectionToSql))
                    {


                        try
                        {
                            //command params
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 25;

                            // open SQL  Connection
                            connectionToSql.Open();
                            //use reader
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //New instantiation of a DO 
                                    IVolunteerInfoDO volunteerDO = new VolunteerInfoDO();
                                    //Assign values from the reader to the new DO
                                    volunteerDO.VolunteerID = reader.GetInt64(0);
                                    volunteerDO.Time_Logged = reader.GetInt32(1);
                                    volunteerDO.UserID = reader.GetInt64(2);
                                    volunteerDO.Date = reader.GetDateTime(3);
                                    volunteerDO.Time_Available = reader.GetInt32(4);
                                    volunteerDO.Flag = reader.GetBoolean(5);


                                    //POPULATE the list 
                                    listOfVolunteers.Add(volunteerDO);

                                }
                            }

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            //close and dispose
                            command.Dispose();
                            connectionToSql.Close();
                            connectionToSql.Dispose();
                        }

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

            }

            return listOfVolunteers;
        }



        //Method to Delete Volunteer by ID 
        public void DeleteVolunteerByID(long VolunteerID)
        {
            try
            {
                //Using statements for the connections and commands
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE_VOLUNTEER_BY_ID", connectToSql))
                    {
                        try
                        {
                            //command params
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            deleteCommand.CommandTimeout = 25;
                            deleteCommand.Parameters.AddWithValue("@VolunteerID", VolunteerID);

                            //Open Connection
                            connectToSql.Open();
                            //Excute Delete Command 
                            deleteCommand.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                         
                            throw e;
                        }
                        finally
                        {
                            //close and dispose
                            connectToSql.Close();
                            connectToSql.Dispose();
                        }
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
                // do nothing 
            }
        }



        //Method to Update User by ID
        public void UpdateVolunteerByID(IVolunteerInfoDO iVolunteer)
        {
            try
            {
                //Using Statements for Connections and Commands to SQL 
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand updatecommand = new SqlCommand("UPDATE_VOLUNTEER_BY_ID", connectToSql))
                    {
                        try
                        {

                            //command Params 
                            updatecommand.CommandType = CommandType.StoredProcedure;
                            updatecommand.CommandTimeout = 25;
                            updatecommand.Parameters.AddWithValue("@VolunteerID", iVolunteer.VolunteerID);
                            updatecommand.Parameters.AddWithValue("@Time_Logged", iVolunteer.Time_Logged);
                            updatecommand.Parameters.AddWithValue("@UserID", iVolunteer.UserID);
                            updatecommand.Parameters.AddWithValue("@Date", iVolunteer.Date);
                            updatecommand.Parameters.AddWithValue("@Time_Available", iVolunteer.Time_Available);
                            updatecommand.Parameters.AddWithValue("@Flag", iVolunteer.Flag);



                            // Open Connection
                            connectToSql.Open();
                            //Excute Update Command
                            updatecommand.ExecuteNonQuery();


                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            //close and dispose
                            connectToSql.Close();
                            connectToSql.Dispose();
                        }
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
                // do nothing
            }

        }

        //Method to View Volunteer by ID
        public IVolunteerInfoDO ViewVolunteerByID(long volunteerID)
        {
            VolunteerInfoDO volunteer = new VolunteerInfoDO();

            try
            {
                //Using Statements for Connections and Commands
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand viewCommand = new SqlCommand("VIEW_VOLUNTEERS_BY_ID", connectToSql))
                    {
                        try
                        {
                            //viewcommands and params
                            viewCommand.CommandType = CommandType.StoredProcedure;
                            viewCommand.CommandTimeout = 25;
                            viewCommand.Parameters.AddWithValue("@VolunteerID", volunteerID);

                            //open connection
                            connectToSql.Open();
                            using (SqlDataReader reader = viewCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    volunteer.VolunteerID = reader.GetInt64(0);
                                    volunteer.Time_Logged = reader.GetInt32(1);
                                    volunteer.UserID = reader.GetInt64(2);
                                    volunteer.Date = reader.GetDateTime(3);
                                    volunteer.Time_Available = reader.GetInt32(4);
                                    volunteer.Flag = reader.GetBoolean(5);

                                }
                            }

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            //close and dispose
                            connectToSql.Close();
                            connectToSql.Dispose();
                        }

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
                //do nothing
            }

            return volunteer;

        }
    }
}
