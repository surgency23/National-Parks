using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class CampGroundDAL
    {
        private string connectionString;
        private const string SQL_CampgroundsInPark = "SELECT * FROM CAMPGROUND WHERE PARK_ID = @PARK_ID";

        public CampGroundDAL(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }

        public List<CampgroundModel> CampgroundsInPark(string parkID)
        {
            List<CampgroundModel> campgroundsPerPark = new List<CampgroundModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CampgroundsInPark, conn);

                    cmd.Parameters.AddWithValue("@PARK_ID", parkID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CampgroundModel nc = new CampgroundModel();
                        nc.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        nc.ParkId = Convert.ToInt32(reader["park_id"]);
                        nc.CampgroundName = Convert.ToString(reader["name"]);
                        nc.CampgroundOpenMonth = Convert.ToInt32(reader["open_from_mm"]);
                        nc.CampgroundCloseMonth = Convert.ToInt32(reader["open_to_mm"]);
                        nc.DailyCost = Convert.ToDecimal(reader["daily_fee"]);

                        campgroundsPerPark.Add(nc);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return campgroundsPerPark;

        }
        public bool IsOffSeason()/*maybe should be in the reservation method */
        {
            return false;
        }

    }
}

