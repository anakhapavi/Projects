using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web;

namespace RecuirementManagement.Repository
{
    public class CanidateRepositorycs
    {
        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }

        ///insert dtails
        public bool AddCandidateDetails(CandidateRegistration candidateRegistration)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Registration", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@firstName", candidateRegistration.firstName);
            command.Parameters.AddWithValue("@dob", candidateRegistration.dob);
            command.Parameters.AddWithValue("@gender", candidateRegistration.gender);
            command.Parameters.AddWithValue("@phone", candidateRegistration.phone);
            command.Parameters.AddWithValue("@email", candidateRegistration.email);
            command.Parameters.AddWithValue("@address", candidateRegistration.address);
            command.Parameters.AddWithValue("@state", candidateRegistration.state);
            command.Parameters.AddWithValue("@city", candidateRegistration.city);
            command.Parameters.AddWithValue("@image", candidateRegistration.image);
            command.Parameters.AddWithValue("@username", candidateRegistration.username);
            command.Parameters.AddWithValue("@password", candidateRegistration.password);
            command.Parameters.AddWithValue("@cpassword", candidateRegistration.confirmPassword);
            command.Parameters.AddWithValue("@usertype", candidateRegistration.usertype);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// View Details <summary>
        /// View Details
        /// </summary>
        public List<CandidateRegistration> GetCandidateDetails()
        {
            Connection();
            List<CandidateRegistration> RegistrationList = new List<CandidateRegistration>();
            SqlCommand command = new SqlCommand("SPV_Registration",connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            connection.Close();
            foreach (DataRow dr in table.Rows)
                RegistrationList.Add(
                    new CandidateRegistration
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
                        image = dr["image"].ToString(),
                        username = dr["username"].ToString(),
                        password = dr["password"].ToString(),
                        confirmPassword = dr["confirmPassword"].ToString()
                    }
                    );
                return RegistrationList;
        }
    }
    }
