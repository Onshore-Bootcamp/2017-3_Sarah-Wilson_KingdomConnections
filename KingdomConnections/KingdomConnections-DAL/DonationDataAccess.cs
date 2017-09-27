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
    public class DonationDataAccess
    {
        //Define the Connection String 
        public string connectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=KingdomConnections; Trusted_Connection=True;";

        //Method to Creating a Donation
        public void CreateDonation(IDonationInfoDO donation)
        {
            try
            {

                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("CREATE_DONATIONS", connectToSql))
                    {
                        try
                        {
                            //Command parameters go here
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 25;

                            command.Parameters.AddWithValue("@UserID", donation.UserID);
                            command.Parameters.AddWithValue("@Amount_of_Donation", donation.Amount_of_Donation);
                            command.Parameters.AddWithValue("@Date_of_Donation", donation.Date_of_Donation);
                            command.Parameters.AddWithValue("@Tax_Receipt_Given", donation.Tax_Receipt_Given);
                            command.Parameters.AddWithValue("@Department_Need", donation.Department_Need);
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
                //Log Error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Sarah Boot Camp\Documents\C#\KingdomConnections\log files\text.txt"))
                {
                    fileWriter.WriteLine(e.Message);
                }
                throw e;
            }
            finally
            {
                //Do Nothing
            }
        }

        // Method to View All Donations
        public List<IDonationInfoDO> ViewAllDonations()
        {
            // Instantiate a List of Users
            List<IDonationInfoDO> listOfDonations = new List<IDonationInfoDO>();

            try
            {
                using (SqlConnection connectionToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("VIEW_All_DONATIONS", connectionToSql))
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
                                    // New instantiation of a DO
                                    IDonationInfoDO donationDO = new DonationInfoDO()
                                    {
                                        // Assign values from the reader to the new DO
                                        DonationID = reader.GetInt64(0),
                                        UserID = reader.GetInt64(1),
                                        Amount_of_Donation = reader.GetDecimal(2),
                                        Date_of_Donation = reader.GetDateTime(3),
                                        Tax_Receipt_Given = reader.GetBoolean(4),
                                        Department_Need = reader.GetString(5),
                                    };


                                    //POPULATE the list
                                    listOfDonations.Add(donationDO);

                                }
                            }

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {

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
            return listOfDonations;
        }


        //Method to Delete Donation by ID 
        public void DeleteDonationByID(long DonationID)
        {
            try
            {
                //Using statements for the connections and commands
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand deleteCommand = new SqlCommand("DELETE_DONATION_BY_ID", connectToSql))
                    {
                        try
                        {
                            //command params
                            deleteCommand.CommandType = CommandType.StoredProcedure;
                            deleteCommand.CommandTimeout = 25;
                            deleteCommand.Parameters.AddWithValue("@DonationID", DonationID);

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


        //Method to Update Donationsby ID
        public void UpdateDonationByID(IDonationInfoDO iDonation)
        {
            try
            {
                //Using Statements for Connections and Commands to SQL 
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand updatecommand = new SqlCommand("UPDATE_DONATION_BY_ID", connectToSql))
                    {
                        try
                        {

                            //command Params 
                            updatecommand.CommandType = CommandType.StoredProcedure;
                            updatecommand.CommandTimeout = 25;
                            updatecommand.Parameters.AddWithValue("@DonationID", iDonation.DonationID);
                            updatecommand.Parameters.AddWithValue("@UserID", iDonation.UserID);
                            updatecommand.Parameters.AddWithValue("@Amount_of_Donation", iDonation.Amount_of_Donation);
                            updatecommand.Parameters.AddWithValue("@Date_of_Donation", iDonation.Date_of_Donation);
                            updatecommand.Parameters.AddWithValue("@Tax_Receipt_Given", iDonation.Tax_Receipt_Given);
                            updatecommand.Parameters.AddWithValue("@Department_Need", iDonation.Department_Need);



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


        //Method to View Donations by ID
        public IDonationInfoDO ViewDonationByID(long donationID)
        {
            DonationInfoDO donation = new DonationInfoDO();

            try
            {
                //Using Statements for Connections and Commands
                using (SqlConnection connectToSql = new SqlConnection(connectionString))
                {
                    using (SqlCommand viewCommand = new SqlCommand("VIEW_ALL_DONATIONS_BY_ID", connectToSql))
                    {
                        try
                        {
                            //viewcommands and params
                            viewCommand.CommandType = CommandType.StoredProcedure;
                            viewCommand.CommandTimeout = 25;
                            viewCommand.Parameters.AddWithValue("@DonationID", donationID);

                            //open connection
                            connectToSql.Open();
                            using (SqlDataReader reader = viewCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    donation.DonationID = reader.GetInt64(0);
                                    donation.UserID = reader.GetInt64(1);
                                    donation.Amount_of_Donation = reader.GetDecimal(2);
                                    donation.Date_of_Donation = reader.GetDateTime(3);
                                    donation.Tax_Receipt_Given = reader.GetBoolean(4);
                                    donation.Department_Need = reader.GetString(5);

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

            }

            return donation;

        }

    }
}
