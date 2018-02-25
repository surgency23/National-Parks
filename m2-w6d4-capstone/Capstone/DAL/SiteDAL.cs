using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SiteDAL
    {
        private string connectionString;
        private const string SQL_GetSites = "SELECT TOP(5) * FROM site WHERE campground_id = @campgroundID AND site.site_id NOT IN( SELECT site.site_id FROM site JOIN reservation ON site.site_id = reservation.site_id WHERE reservation.from_date BETWEEN @arrival AND @departure OR reservation.to_date BETWEEN @arrival AND @departure OR (reservation.from_date<@arrival AND reservation.to_date> @departure))";

        public SiteDAL(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }
        public List<SiteModel> AvailableSiteSearch(string campgroundId, string startDate, string departDate)
        {
            List<SiteModel> availableSites = new List<SiteModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSites, conn);
                    cmd.Parameters.AddWithValue("@campgroundID", campgroundId);
                    cmd.Parameters.AddWithValue("@arrival", startDate);
                    cmd.Parameters.AddWithValue("@departure", departDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
        

                        SiteModel s = new SiteModel();
                        s.SiteId = Convert.ToInt32(reader["site_id"]);
                        s.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.HandicapAccessiblity = Convert.ToString(reader["accessible"]);
                        s.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.Utilities = Convert.ToString(reader["utilities"]);
              

                        availableSites.Add(s);
                    }
                }
            }catch(SqlException ex)
            {
                throw;

            }


            return availableSites;
        }

     
    }
}
