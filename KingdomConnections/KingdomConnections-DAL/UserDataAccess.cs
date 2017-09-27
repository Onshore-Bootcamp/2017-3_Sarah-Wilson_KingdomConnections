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
    public class UserDataAccess
    {
        //Define the Connection String 
        public string connectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=KingdomConnections; Trusted_Connection=True;";

        //Method to Creating a New User
        public void CreateUser(IUserInfoDO user)
        {
            try
            {

                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("Create_Users", connectToSql))
                    {
                        try
                        {
                            //Command parameters go here
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 25;
                            command.Parameters.AddWithValue("@Name", user.Name);
                            command.Parameters.AddWithValue("@Street_Address", user.Street_Address);
                            command.Parameters.AddWithValue("@City_State_Zip", user.City_State_Zip);
                            command.Parameters.AddWithValue("@Phone_Number", user.Phone_Number);
                            command.Parameters.AddWithValue("@Login", user.Login);
                            command.Parameters.AddWithValue("@Password", user.Password);
                            command.Parameters.AddWithValue("@Role", user.Role);
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

            {
                //log exceptions here 
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


        // Method to View All Users
        public List<IUserInfoDO> ViewAllUsers()
        {
            // Instantiate a List of Users
            List<IUserInfoDO> listOfUsers = new List<IUserInfoDO>();

            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("View_All_Users", connectionToSql))
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
                                    IUserInfoDO userDO = new UserInfoDO();
                                    //Assign values from the reader to the new DO
                                    userDO.UserID = reader.GetInt64(0);
                                    userDO.Name = reader.GetString(1);
                                    userDO.Street_Address = reader.GetString(2);
                                    userDO.City_State_Zip = reader.GetString(3);
                                    userDO.Phone_Number = reader.GetString(4);
                                    userDO.Login = reader.GetString(5);
                                    userDO.Password = reader.GetString(6);
                                    userDO.Role = reader.GetString(7);

                                    //POPULATE the list 
                                    listOfUsers.Add(userDO);

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
                //Log error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                {
                    fileWriter.WriteLine(e.Message);
                }
                throw e;
            }
            finally
            {

            }

            return listOfUsers;
        }


        //Method to Delete User by ID 
        public void DeleteUserByID(long userID)
        {
            try
            {
                //Using statements for the connections and commands
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE_USER_BY_ID", connectToSql))
                    {
                        try
                        {
                            //command params
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            deleteCommand.CommandTimeout = 25;
                            deleteCommand.Parameters.AddWithValue("@UserID", userID);

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
        public void UpdateUserByID(IUserInfoDO iUser)
        {
            try
            {
                //Using Statements for Connections and Commands to SQL 
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand updatecommand = new SqlCommand("UPDATE_USER_BY_ID", connectToSql))
                    {
                        try
                        {

                            //command Params 
                            updatecommand.CommandType = CommandType.StoredProcedure;
                            updatecommand.CommandTimeout = 25;
                            updatecommand.Parameters.AddWithValue("@UserID", iUser.UserID);
                            updatecommand.Parameters.AddWithValue("@Name", iUser.Name);
                            updatecommand.Parameters.AddWithValue("@Street_Address", iUser.Street_Address);
                            updatecommand.Parameters.AddWithValue("@City_State_Zip", iUser.City_State_Zip);
                            updatecommand.Parameters.AddWithValue("@Phone_Number", iUser.Phone_Number);
                            updatecommand.Parameters.AddWithValue("@Login", iUser.Login);
                            updatecommand.Parameters.AddWithValue("@Password", iUser.Password);
                            updatecommand.Parameters.AddWithValue("@Role", iUser.Role);

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

        //Method to View User by ID
        public IUserInfoDO ViewUserByID(long userID)
        {
            UserInfoDO user = new UserInfoDO();

            try
            {
                //Using Statements for Connections and Commands
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand viewCommand = new SqlCommand("VIEW_USERS_BY_ID", connectToSql))
                    {
                        try
                        {
                            //viewcommands and params
                            viewCommand.CommandType = CommandType.StoredProcedure;
                            viewCommand.CommandTimeout = 25;
                            viewCommand.Parameters.AddWithValue("@UserID", userID);

                            //open connection
                            connectToSql.Open();
                            using (SqlDataReader reader = viewCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    user.UserID = reader.GetInt64(0);
                                    user.Name = reader.GetString(1);
                                    user.Street_Address = reader.GetString(2);
                                    user.City_State_Zip = reader.GetString(3);
                                    user.Phone_Number = reader.GetString(4);
                                    user.Login = reader.GetString(5);
                                    user.Password = reader.GetString(6);
                                    user.Role = reader.GetString(7);


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

            return user;

        }

        //Method to Obtain User by Login
        public IUserInfoDO ViewUserByLogin(string iLogin)
        {
            UserInfoDO login = new UserInfoDO();
            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand viewcommand = new SqlCommand("VIEW_USER_BY_LOGIN", connectionToSql))
                    {
                        try
                        {
                            viewcommand.CommandType = CommandType.StoredProcedure;
                            viewcommand.CommandTimeout = 25;
                            viewcommand.Parameters.AddWithValue("@Login", iLogin);

                            connectionToSql.Open();
                            using (SqlDataReader reader = viewcommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    login.UserID = reader.GetInt64(0);
                                    login.Name = reader.GetString(1);
                                    login.Login = reader.GetString(2);
                                    login.Password = reader.GetString(3);
                                    login.Role = reader.GetString(4);
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
            return login;
        }
    }
}

