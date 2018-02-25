using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.Collections.Generic;
using System.Data;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Tests
{
    [TestClass]
    public class SiteTest
    {

        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NationalPark;Integrated Security=True";
        private int siteCount = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand cmd;
                conn.Open();

                cmd = new SqlCommand("SELECT top (5) * FROM site WHERE campground_id = 1 AND site.site_id NOT IN ( SELECT site.site_id FROM site JOIN reservation ON site.site_id = reservation.site_id WHERE reservation.from_date BETWEEN '2018-02-21' AND '2018-02-23' OR reservation.to_date BETWEEN '2018-02-21' AND '2018-02-23' OR (reservation.from_date < '2018-02-21' AND reservation.to_date > '2018-02-23'))", conn);
                siteCount = (int)cmd.ExecuteScalar();
            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod]
        public void SiteCountTest()
        {
            SiteDAL theseSites = new SiteDAL(connectionString);
            List<SiteModel> siteList = theseSites.AvailableSiteSearch("1", "02-21-2018", "02-23-2018");
            Assert.AreEqual(siteCount + 1, siteList.Count);
        }

    }
}
