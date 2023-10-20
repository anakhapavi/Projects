using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace RecuirementManagement.Repository
{
    public class ApplicationRepository
    {
        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }
        
        /// <summary>
        /// add details
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool AddApplications(Application application)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Applications",connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid",application.cid);
            command.Parameters.AddWithValue("@eid",application.eid);
            command.Parameters.AddWithValue("@vid",application.vid);
            command.Parameters.AddWithValue("@status",application.status);
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
        /// <summary>
        /// view details
        /// </summary>
        /// <returns></returns>
        public List<Application> GetApplications()
        {
            Connection ();
            List<Application> ApplicationList = new List<Application>();
            SqlCommand command = new SqlCommand("SPV_Applications", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            connection.Close();
            foreach (DataRow dr in table.Rows)
                ApplicationList.Add(
                    new Application
                    { 
                       appid = Convert.ToInt32(dr["appid"]),
                       cid = Convert.ToInt32(dr["cid"]),
                       eid = Convert.ToInt32(dr["eid"]),
                       vid = Convert.ToInt32(dr["vid"]),
                       status = Convert.ToString(dr["status"])

                    }
                    );
                return ApplicationList;
        }
        /// <summary>
        /// get all applied candidates
        /// </summary>
        /// <returns></returns>
        public List<ApplicationDetails> GetAllApplicationsDetails()
        {
            Connection();
            List<ApplicationDetails> applicationDetailsList = new List<ApplicationDetails>();
            SqlCommand command = new SqlCommand("SP_ApplicationDetails", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            connection.Close();

            foreach (DataRow dr in table.Rows)
            {
                var applicationDetails = new ApplicationDetails
                {
                    appid = Convert.ToInt32(dr["appid"]),
                    cid = Convert.ToInt32(dr["cid"]),
                    eid = Convert.ToInt32(dr["eid"]),
                    vid = Convert.ToInt32(dr["vid"]),
                    firstName = Convert.ToString(dr["firstName"]),
                    lastName = Convert.ToString(dr["lastName"]),
                    highestQualification = Convert.ToString(dr["highestQualification"]),
                    workExperience = Convert.ToString(dr["workExperience"]),
                    noofYears = Convert.ToInt32(dr["noofYears"]),
                    skills = Convert.ToString(dr["skills"]),
                    jobRole = Convert.ToString(dr["jobRole"]),
                    status = Convert.ToString(dr["status"])
                };

                applicationDetailsList.Add(applicationDetails);
            }

            return applicationDetailsList;
        }

        /// <summary>
        /// get only scheduled candidates
        /// </summary>
        /// <returns></returns>
        public List<ApplicationDetails> GetScheduledApplications()
        {
            Connection();
            List<ApplicationDetails> applicationDetailsList = new List<ApplicationDetails>();
            SqlCommand command = new SqlCommand("SP_GetScheduleStatusUser", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            connection.Close();

            foreach (DataRow dr in table.Rows)
            {
                var applicationDetails = new ApplicationDetails
                {
                    appid = Convert.ToInt32(dr["appid"]),
                    cid = Convert.ToInt32(dr["cid"]),
                    eid = Convert.ToInt32(dr["eid"]),
                    vid = Convert.ToInt32(dr["vid"]),
                    firstName = Convert.ToString(dr["firstName"]),
                    lastName = Convert.ToString(dr["lastName"]),
                    highestQualification = Convert.ToString(dr["highestQualification"]),
                    workExperience = Convert.ToString(dr["workExperience"]),
                    noofYears = Convert.ToInt32(dr["noofYears"]),
                    skills = Convert.ToString(dr["skills"]),
                    jobRole = Convert.ToString(dr["jobRole"]),
                    status = Convert.ToString(dr["status"])
                };

                applicationDetailsList.Add(applicationDetails);
            }

            return applicationDetailsList;
        }
        /// <summary>
        /// edit details
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public bool EditApplications(Application application)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPE_Applications", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@appid",application.appid);
            command.Parameters.AddWithValue("@status",application.status);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i >= 0)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        public Application GetApplicationById(int? appid)
        {
            Connection();
            Application application = null;

            try
            {
                SqlCommand command = new SqlCommand("SP_GetApplicationById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@appid", appid);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    application = new Application
                    {
                        appid = Convert.ToInt32(reader["appid"]),
                    };
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return application;
        }




    }
}
