using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class ReservatorDAL
    {   //Sql_reservation inserts a new reservation into database from user input name and dates
        //sql_idGrabber finds name of inserted reservation for testing purposes to see if the users reservation actually worked
        private string connectionString;
        private const string SQL_reservation = @"INSERT INTO[dbo].[reservation] ([site_id] ,[name] ,[from_date] ,[to_date],[create_date])VALUES(@siteId, @name, @startDate, @endDate, @creationDate)";
        private const string SQL_idGrabber = @"select * from reservation WHERE name = @name";

        public ReservatorDAL(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public void MakeReservation(string siteId, string name, string startDate, string endDate)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_reservation, connection);
                    cmd.Parameters.AddWithValue("@siteId", siteId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Parameters.AddWithValue("@creationDate", DateTime.Now);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReservationModel r = new ReservationModel();
                        r.ReservationId = Convert.ToInt32(reader["reservation_id"]);
                        r.SiteId = Convert.ToInt32(reader["site_id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.FromDate = Convert.ToDateTime(reader["from_date"]);
                        r.EndDate = Convert.ToDateTime(reader["to_date"]);
                        r.CreationDate = Convert.ToDateTime(reader["create_date"]);

                        

                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            
        }
        public List<ReservationModel> IdGrabber(string name)
        {
            List<ReservationModel> thisList = new List<ReservationModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_idGrabber, conn);
                    cmd.Parameters.AddWithValue("@name", name);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        ReservationModel r = new ReservationModel();
                        r.ReservationId = Convert.ToInt32(reader["reservation_id"]);
                        r.SiteId = Convert.ToInt32(reader["site_id"]);
                        r.Name = Convert.ToString(reader["name"]);
                        r.FromDate = Convert.ToDateTime(reader["from_date"]);
                        r.EndDate = Convert.ToDateTime(reader["to_date"]);
                        r.CreationDate = Convert.ToDateTime(reader["create_date"]);


                        thisList.Add(r);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return thisList;
        }
    }
}
