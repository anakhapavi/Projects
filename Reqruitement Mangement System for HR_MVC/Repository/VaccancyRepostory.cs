using RecuirementManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecuirementManagement.Repository
{
    public class VaccancyRepository
    {
        private string connectionString;

        public VaccancyRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool InsertVaccancy(Vaccancy vaccancy)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SPI_Vaccancy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@vid", vaccancy.vid);
                    command.Parameters.AddWithValue("@jobRole", vaccancy.jobRole);
                    command.Parameters.AddWithValue("@skills", vaccancy.skills);
                    command.Parameters.AddWithValue("@available", vaccancy.available);
                    command.Parameters.AddWithValue("@experience", vaccancy.experience);
                    command.Parameters.AddWithValue("@currentStatus", vaccancy.currentStatus);

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    return id > 0;
                }
            }
        }

        public List<Vaccancy> GetVaccancies()
        {
            List<Vaccancy> VaccancyList = new List<Vaccancy>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SPV_Vaccancy", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {
                            Vaccancy vaccancy = new Vaccancy
                            {
                                vid = Convert.ToInt32(dr["vid"]),
                                jobRole = Convert.ToString(dr["jobRole"]),
                                skills = Convert.ToString(dr["skills"]),
                                available = Convert.ToString(dr["available"]),
                                experience = Convert.ToString(dr["experience"]),
                                currentStatus = Convert.ToString(dr["currentStatus"])
                            };

                            VaccancyList.Add(vaccancy);
                        }
                    }
                }
            }

            return VaccancyList;
        }

       

    }
}
