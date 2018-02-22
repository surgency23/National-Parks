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
    public class ParkTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NationalPark;Integrated Security=True";
        private int parkCount = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM Park", conn);
                parkCount = (int)cmd.ExecuteScalar();


            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }


        [TestMethod]
        public void AllParksTest()
        {
            ParkDAL thisPark = new ParkDAL(connectionString);
            List<ParkModel> listOfParks = thisPark.GetAllParks();
            Assert.AreEqual(parkCount, listOfParks.Count);


        }
    }
}
