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
    public class ReservationTest
    {

        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NationalPark;Integrated Security=True";
        private int reservationCount = 0;
        

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand cmd;

                conn.Open();

                ReservatorDAL thisreservation = new ReservatorDAL(connectionString);
                thisreservation.MakeReservation("4", "testreservation", "02-21-2018", "02-23-2018");

                cmd = new SqlCommand("SELECT COUNT(*) FROM reservation WHERE NAME = 'testreservation'", conn);
                reservationCount = (int)cmd.ExecuteScalar();


            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void reservationTesting()
        {
            ReservatorDAL thisreservation = new ReservatorDAL(connectionString);
           
            
            List<ReservationModel> reservationList = thisreservation.IdGrabber("testreservation");
            Assert.AreEqual(reservationCount, reservationList.Count);
        }
    }
}
