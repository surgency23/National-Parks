using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string connectionString;
        private const string SQL_GetSites = "SELECT site.site_id, site.max_occupancy, site.accessible, site.utilities, campground.daily_fee FROM SITE JOIN campground ON campground.campground_id = site.campground_id WHERE campground.campground_id = @campground_id";

        public ReservationDAL(string dbconnectionString)
        {
            connectionString = dbconnectionString;
        }
        public List<SiteModel> ReservationSearch(string campgroundId, string startDate, string departDate)1
        {
            List<SiteModel> availableSites = new List<SiteModel>();


            return availableSites;
        }

     
    }
}
