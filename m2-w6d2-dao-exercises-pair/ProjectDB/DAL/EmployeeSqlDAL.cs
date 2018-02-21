using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string connectionString;
        private const string SQL_getEmployees = @"SELECT * FROM employee";
        private const string SQL_searchEmployee = @"SELECT * FROM employee WHERE first_name = @first_name AND last_name = @last_name";
        private const string SQL_LazyEmployees = @"select * from employee Left join project_employee on project_employee.employee_id = employee.employee_id Left join project on project.project_id = project_employee.project_id where project_employee.project_id IS NULL";





        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {

            connectionString = dbConnectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(SQL_getEmployees, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Employee thisemployee = new Employee();
                        thisemployee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        thisemployee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        thisemployee.JobTitle = Convert.ToString(reader["job_title"]);
                        thisemployee.FirstName = Convert.ToString(reader["first_name"]);
                        thisemployee.LastName = Convert.ToString(reader["last_name"]);
                        thisemployee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        thisemployee.Gender = Convert.ToString(reader["gender"]);
                        thisemployee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        employeeList.Add(thisemployee);

                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return employeeList;
          
        }

        public List<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employeeSearch = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_searchEmployee, conn);

                    cmd.Parameters.AddWithValue("@first_name", firstname);
                    cmd.Parameters.AddWithValue("@last_name", lastname);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee thisemployee = new Employee();
                        thisemployee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        thisemployee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        thisemployee.JobTitle = Convert.ToString(reader["job_title"]);
                        thisemployee.FirstName = Convert.ToString(reader["first_name"]);
                        thisemployee.LastName = Convert.ToString(reader["last_name"]);
                        thisemployee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        thisemployee.Gender = Convert.ToString(reader["gender"]);
                        thisemployee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        employeeSearch.Add(thisemployee);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return employeeSearch;
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> LazyEmployeeList = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_LazyEmployees, conn);

                   
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee thisemployee = new Employee();
                        thisemployee.EmployeeId = Convert.ToInt32(reader["employee_id"]);
                        thisemployee.DepartmentId = Convert.ToInt32(reader["department_id"]);
                        thisemployee.JobTitle = Convert.ToString(reader["job_title"]);
                        thisemployee.FirstName = Convert.ToString(reader["first_name"]);
                        thisemployee.LastName = Convert.ToString(reader["last_name"]);
                        thisemployee.BirthDate = Convert.ToDateTime(reader["birth_date"]);
                        thisemployee.Gender = Convert.ToString(reader["gender"]);
                        thisemployee.HireDate = Convert.ToDateTime(reader["hire_date"]);

                        LazyEmployeeList.Add(thisemployee);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return LazyEmployeeList;
        }
    }
}
