using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Repository
{
    public class EducationRepositorycs
    {
        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }
        /// <summary>
        ///Add education details
        /// </summary>
        public bool AddEducation(Education education)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Education", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid", education.cid);
            command.Parameters.AddWithValue("@highestQualification", education.highestQualification);
            command.Parameters.AddWithValue("@passYear", education.passYear);
            command.Parameters.AddWithValue("@cgpa", education.cgpa);
            command.Parameters.AddWithValue("@workExperience", education.workExperience);
            command.Parameters.AddWithValue("@noofYears", education.noofYears);
            command.Parameters.AddWithValue("@jobRole", education.jobRole);
            command.Parameters.AddWithValue("@lastSalary", education.lastSalary);
            command.Parameters.AddWithValue("@skills", education.skills);
            command.Parameters.AddWithValue("@status", education.status);
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
        ///Get education details
        /// </summary>
        public List<Education> GetEducation()
        {
            Connection();
            List<Education> EducationList = new List<Education>();
            connection.Open();
            using (SqlCommand command = new SqlCommand("SPV_Education", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.SelectCommand = command;

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow dr in table.Rows)
                    {
                        Education education = new Education
                        {
                            eid = Convert.ToInt32(dr["eid"]),
                            cid = Convert.ToInt32(dr["cid"]),
                            highestQualification = dr["highestQualification"] == DBNull.Value ? null : Convert.ToString(dr["highestQualification"]),
                            // Check for DBNull before conversion
                            passYear = dr["passYear"] == DBNull.Value ? 0 : Convert.ToInt32(dr["passYear"]),
                            cgpa = dr["cgpa"] == DBNull.Value ? 0.0m : Convert.ToDecimal(dr["cgpa"]),
                            workExperience = dr["workExperience"] == DBNull.Value ? null : dr["workExperience"].ToString(),
                            noofYears = dr["noofYears"] == DBNull.Value ? 0 : Convert.ToInt32(dr["noofYears"]),
                            jobRole = dr["jobRole"] == DBNull.Value ? null : Convert.ToString(dr["jobRole"]),
                            lastSalary = dr["lastSalary"] == DBNull.Value ? 0.0m : Convert.ToDecimal(dr["lastSalary"]),
                            skills = dr["skills"] == DBNull.Value ? null : Convert.ToString(dr["skills"]),
                            status = dr["status"] == DBNull.Value ? null : Convert.ToString(dr["status"])
                        };

                        EducationList.Add(education);
                    }

                }
            }
            return EducationList;
        }


        /// <summary>
        /// Edit by Hr 
        /// </summary>
        /// <param name="education"></param>
        /// <returns></returns>
        public bool EditEducation(Education education)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPE_Education", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@eid", education.eid);
            command.Parameters.AddWithValue("@status", education.status);
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
        /// get details for candidate
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<Education> GetEducationDetailsByCid(int cid)
        {
            Connection();
            List<Education> educationList = new List<Education>();
            connection.Open();
            using (SqlCommand command = new SqlCommand("SP_GetEducationDetailsByCid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@cid", cid);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Education education = new Education
                    {
                        eid = (int)reader["eid"],
                        cid = (int)reader["cid"],
                        highestQualification = reader["highestQualification"].ToString(),
                        passYear = (int)reader["passYear"],
                        cgpa = (decimal)reader["cgpa"],
                        workExperience = reader.IsDBNull(reader.GetOrdinal("workExperience")) ? string.Empty : reader["workExperience"].ToString(),
                        noofYears = reader.IsDBNull(reader.GetOrdinal("noofYears")) ? 0 : (int)reader["noofYears"],
                        jobRole = reader["jobRole"].ToString(),
                        lastSalary = reader.IsDBNull(reader.GetOrdinal("lastSalary")) ? 0 : (decimal)reader["lastSalary"],
                        skills = reader["skills"].ToString(),
                        status = reader.IsDBNull(reader.GetOrdinal("status")) ? string.Empty : reader["status"].ToString()
                    };
                    educationList.Add(education);

                }

                return educationList;
            }




        }
    }
}

    
