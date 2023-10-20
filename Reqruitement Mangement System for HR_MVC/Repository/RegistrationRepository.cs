using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using RecuirementManagement.Models;
using System.Configuration;
using System.Security.Cryptography;

namespace RecuirementManagement.Repository
{
    public class RegistrationRepository
    {
        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }
        /// <summary>
        /// Insert user details
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public bool AddInformation(Registration registration)
        {
            int cid;
            Connection();

            SqlCommand command = new SqlCommand("SPI_Registration", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter cidParam = new SqlParameter("@cid", SqlDbType.Int);
            cidParam.Direction = ParameterDirection.Output;
            command.Parameters.Add(cidParam);
            command.Parameters.AddWithValue("@firstName", registration.firstName);
            command.Parameters.AddWithValue("@lastName", registration.lastName);
            command.Parameters.AddWithValue("@dob", registration.dob);
            command.Parameters.AddWithValue("@gender", registration.gender);
            command.Parameters.AddWithValue("@phone", registration.phone);
            command.Parameters.AddWithValue("@email", registration.email);
            command.Parameters.AddWithValue("@address", registration.address);
            command.Parameters.AddWithValue("@state", registration.state);
            command.Parameters.AddWithValue("@city", registration.city);
            command.Parameters.AddWithValue("@username", registration.username);
            command.Parameters.AddWithValue("@password", registration.password);
            command.Parameters.AddWithValue("@cpassword", registration.confirmPassword);
            command.Parameters.AddWithValue("@usertype", registration.usertype);
            connection.Open();
            int i = command.ExecuteNonQuery();
            cid = Convert.ToInt32(command.Parameters["@cid"].Value);
            connection.Close();
            return i > 0;
        }

        /// <summary>
        /// get details
        /// </summary>
        /// <returns></returns>
        public List<Registration> GetInformation()
        {
            Connection();
            List<Registration> RegistartionList = new List<Registration>();
            connection.Open();
            using (SqlCommand command = new SqlCommand("SPV_Registration", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.SelectCommand = command;

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow dr in table.Rows)
                    {
                        Registration registration = new Registration
                        {

                            cid = Convert.ToInt32(dr["cid"]),
                            firstName = dr["firstName"].ToString(),
                            lastName = dr["lastName"].ToString(),
                            dob = (DateTime)dr["dob"],
                            gender = dr["gender"].ToString(),
                            phone = dr["phone"].ToString(),
                            email = dr["email"].ToString(),
                            address = dr["address"].ToString(),
                            state = dr["state"].ToString(),
                            city = dr["city"].ToString(),
                            username = dr["username"].ToString(),
                            password = dr["password"].ToString(),
                            confirmPassword = dr["confirmPassword"].ToString()
                        };

                        RegistartionList.Add(registration);
                    }
                }
            }
            return RegistartionList;
        }

        /// <summary>
        /// Edit Details by hr
        /// </summary>
        public bool EditInformation(Registration registration)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPE_Registration", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid", registration.cid);
            command.Parameters.AddWithValue("@email", registration.email);
            command.Parameters.AddWithValue("state", registration.state);
            command.Parameters.AddWithValue("city", registration.city);
            try
            {
                connection.Open();
                int i = command.ExecuteNonQuery();
                return i >= 1;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public bool DeleteInformation(int cid)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPD_Registration", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid", cid);
            connection.Open();
            int i = command.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// get information to candidates
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Registration> GetCandidateDetailsByUsername(string username)
        {
            Connection();
            List<Registration> registrationList = new List<Registration>();

            connection.Open();

            using (SqlCommand command = new SqlCommand("SP_GetUserDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Registration registration = new Registration
                    {
                        cid = (int)reader["cid"],
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        dob = (DateTime)reader["dob"],
                        gender = reader["gender"].ToString(),
                        phone = reader["phone"].ToString(),
                        email = reader["email"].ToString(),
                        address = reader["address"].ToString(),
                        state = reader["state"].ToString(),
                        city = reader["city"].ToString(),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        confirmPassword = reader["confirmPassword"].ToString(),
                        usertype = reader["usertype"].ToString()

                    };

                    registrationList.Add(registration);
                }
            }

            return registrationList;
        }
        /// <summary>
        /// get details for update
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public Registration GetCandidateDetailsForUpdate(int cid)
        {
            Connection();
            Registration candidate = null;

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SPU_GetCandidateDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cid", cid);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        candidate = new Registration
                        {
                            cid = (int)reader["cid"],
                            email = reader["email"].ToString(),
                            state = reader["state"].ToString(),
                            city = reader["city"].ToString()
                        };
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return candidate;
        }


        //public Registration GetCurrentUserDetails(int cid)
        //{
        //    Connection();
        //    Registration userRegistration = null;

        //    connection.Open();

        //    using (SqlCommand command = new SqlCommand("SP_GetUserDetails", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;

        //        // Add the parameter for the candidate ID (cid)
        //        command.Parameters.Add(new SqlParameter("@cid", SqlDbType.Int) { Value = cid });

        //        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        //        {
        //            DataTable table = new DataTable();
        //            adapter.Fill(table);

        //            // Check if there are any rows returned (assuming one result row per user)
        //            if (table.Rows.Count > 0)
        //            {
        //                DataRow dr = table.Rows[0];

        //                userRegistration = new Registration
        //                {
        //                    cid = Convert.ToInt32(dr["cid"]),
        //                    firstName = dr["firstName"].ToString(),
        //                    lastName = dr["lastName"].ToString(),
        //                    dob = (DateTime)dr["dob"],
        //                    gender = dr["gender"].ToString(),
        //                    email = dr["email"].ToString(),
        //                    address = dr["address"].ToString(),
        //                    state = dr["state"].ToString(),
        //                    city = dr["city"].ToString(),
        //                    username = dr["username"].ToString(),
        //                    password = dr["password"].ToString()
        //                };
        //            }
        //        }
        //    }

        //    return userRegistration;
        //}



    }
}
                    
           
       
      
    

