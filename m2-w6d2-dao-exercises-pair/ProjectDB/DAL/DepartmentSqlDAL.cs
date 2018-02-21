using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {
        private string connectionString;
        private const string SQL_GetDepartments = "SELECT * FROM department";
        private const string SQL_InsertDepartment = @"insert into department (name) values (@name);";
        private const string SQL_UpdateDepartment = "update department set name = @name where department_id = @department_id";


        /// Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
            List<Department> deptList = new List<Department>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetDepartments,conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Department dept = new Department();
                        dept.Id = Convert.ToInt32(reader["department_id"]);
                        dept.Name = Convert.ToString(reader["name"]);

                        deptList.Add(dept);
                    }
                }

            }
            catch (SqlException ex)
            {

                throw;
            }
            return deptList;
        }

        public bool CreateDepartment(Department newDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_InsertDepartment, conn);
                   // cmd.Parameters.AddWithValue("@department_id", newDepartment.Id);
                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdateDepartment, conn);
                    cmd.Parameters.AddWithValue("@department_id", updatedDepartment.Id);
                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }
    }
}
