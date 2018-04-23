using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{//creates list of parks from sql table
    public class ParkDAL
    {
        private string connectionString;
        private const string SQL_GetParks = "SELECT * FROM PARK ORDER BY NAME";
        private string parkDesc = "";

        public ParkDAL(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }

        public List<ParkModel> GetAllParks()
        {
            List<ParkModel> parkList = new List<ParkModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetParks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ParkModel newPark = new ParkModel();
                        newPark.ParkId = Convert.ToInt32(reader["Park_id"]);
                        newPark.ParkName = Convert.ToString(reader["name"]);
                        newPark.ParkLocation = Convert.ToString(reader["location"]);
                        newPark.ParkEstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        newPark.Area = Convert.ToInt32(reader["area"]);
                        newPark.Visitors = Convert.ToInt32(reader["visitors"]);
                        newPark.ParkDescription = Convert.ToString(reader["description"]);
                        parkDesc = Convert.ToString(reader["description"]);
                        parkList.Add(newPark);
                    }
                }

            }
            catch (SqlException ex)
            {

                throw;
            }
            return parkList;

        }

    }
}
