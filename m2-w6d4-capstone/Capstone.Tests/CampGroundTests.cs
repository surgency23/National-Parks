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
    public class CampGroundTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NationalPark;Integrated Security=True";
        private int campGroundCount = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand cmd;
                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM CAMPGROUND WHERE PARK_ID = 1", conn);
                campGroundCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO [dbo].[campground] ([park_id] ,[name],[open_from_mm],[open_to_mm],[daily_fee]) VALUES (1 , 'test CampGround', '01' , '10', '35.00')" , conn);
                cmd.ExecuteNonQuery();

               

                
            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod]
        public void campgroundInformationTest()
        {
            CampGroundDAL thisCampground = new CampGroundDAL(connectionString);
            List<CampgroundModel> campgroundList = thisCampground.CampgroundsInPark("1");
            Assert.AreEqual(campGroundCount+1, campgroundList.Count);
        }

        [TestMethod]
        public void IsOffSeasonTest()
        {
            CampGroundDAL thisCampground = new CampGroundDAL(connectionString);
            bool userDateInput = thisCampground.IsOffSeason();
            Assert.AreEqual(true, userDateInput);

        }

    }
}
