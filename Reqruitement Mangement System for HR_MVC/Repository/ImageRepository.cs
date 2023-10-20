using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Repository
{
    public class ImageRepository
    {
        private SqlConnection connection;
        public void Connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GetDataBaseConnection"].ToString();
            connection = new SqlConnection(connectionString);
        }
        public bool UploadImage( byte[] image)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPI_Image", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@image", image);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            return i >= 1;
        }
        public byte[] GetImageDataById(int imgid)
        {
            Connection();
            SqlCommand command = new SqlCommand("SPV_Image", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@imgid", imgid);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    return (byte[])reader["image"];
                }
            }

            return null; 
        }
        public List<byte[]> GetAllImagesData()
        {
            Connection();
            SqlCommand command = new SqlCommand("SPV_AllImages", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 60;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<byte[]> imageList = new List<byte[]>();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    imageList.Add((byte[])reader["image"]);
                }
            }

            return imageList;
        }




    }
}
