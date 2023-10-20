using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Repository
{
    public class ScheduleRepository
    {
        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// add Schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public bool AddSchedule(Schedule schedule)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Schedule", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid",schedule.cid);
            command.Parameters.AddWithValue("@vid", schedule.vid);
            command.Parameters.AddWithValue("@appid",schedule.appid);
            command.Parameters.AddWithValue("@scheduleDate",schedule.scheduleDate);
            command.Parameters.AddWithValue("@scheduleTime",schedule.scheduleTime);
            command.Parameters.AddWithValue("@organizer",schedule.organizer);
            command.Parameters.AddWithValue("@link",schedule.link);
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
        /// get details
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<Schedule> GetSchedules(int cid)
        {
            Connection();
            List<Schedule> ScheduleList = new List<Schedule>();
            SqlCommand command = new SqlCommand("SP_GetSchedulesByCid", connection); 
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid", cid); 
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            connection.Close();
            foreach (DataRow dr in table.Rows)
                ScheduleList.Add(
                    new Schedule
                    {
                        sid = Convert.ToInt32(dr["sid"]),
                        cid = Convert.ToInt32(dr["cid"]),
                        vid = Convert.ToInt32(dr["appid"]),
                        scheduleDate = Convert.ToDateTime(dr["scheduleDate"]),
                        scheduleTime = (TimeSpan)dr["scheduleTime"],
                        organizer = dr["organizer"].ToString(),
                        link = dr["link"].ToString()
                    }
                );
            return ScheduleList;
        }
        /// <summary>
        /// get schedule details for candidate
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<ScheduleWithDetails> GetSchedulesWithDetails(int cid)
        {
            Connection();
            List<ScheduleWithDetails> ScheduleList = new List<ScheduleWithDetails>();
            SqlCommand command = new SqlCommand("SP_GetSchedulesByCidWithDetails", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cid", cid);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            connection.Open();
            adapter.Fill(table);
            connection.Close();
            foreach (DataRow dr in table.Rows)
                ScheduleList.Add(
                    new ScheduleWithDetails
                    {
                        sid = Convert.ToInt32(dr["sid"]),
                        cid = Convert.ToInt32(dr["cid"]),
                        vid = Convert.ToInt32(dr["vid"]),
                        appid = Convert.ToInt32(dr["appid"]),
                        scheduleDate = Convert.ToDateTime(dr["scheduleDate"]),
                        scheduleTime = (TimeSpan)dr["scheduleTime"],
                        organizer = dr["organizer"].ToString(),
                        link = dr["link"].ToString(),
                        firstName = dr["firstName"].ToString(),
                        lastName = dr["lastName"].ToString(),
                        jobRole = dr["jobRole"].ToString()
                    }

                );

            return ScheduleList;
        }





    }
}
