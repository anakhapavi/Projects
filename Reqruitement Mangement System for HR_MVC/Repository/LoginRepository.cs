using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RecuirementManagement.Models; 

namespace RecuirementManagement.Repository
{
    public class LoginRepository
    {

        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }
        public bool AddLogin(Login login)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Login", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", login.username);
            command.Parameters.AddWithValue("@password",login.password);
            command.Parameters.AddWithValue("@usertype",login.usertype);
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
        public string AuthenticateUser(string username, string password)
        {
            Connection();
            SqlCommand command = new SqlCommand("SP_AutenticateUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            connection.Open();
            string userType = (string)command.ExecuteScalar();
            return userType;
        }


    }
}
