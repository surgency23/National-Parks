using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.Models;
using ProjectDB.DAL;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.Collections.Generic;
using System.Data;

namespace ProjectDB
{
    [TestClass]
    public class EmployeeTest
    {
        private TransactionScope trann;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Projects;Integrated Security=True";
        private int empCount = 0;
        private int tempEmp = 0;
        private int lazyEmps = 0;

        [TestInitialize]
        public void Initialize()
        {
            trann = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM employee", conn);
                empCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO [dbo].[employee] ([department_id] ,[first_name] ,[last_name] ,[job_title] ,[birth_date] ,[gender] ,[hire_date]) VALUES (2, 'John', 'Fulton', 'IT Guy 1', '12-25-1980', 'M', '01-01-2018'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                tempEmp = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("select COUNT(*) from employee Left join project_employee on project_employee.employee_id = employee.employee_id Left join project on project.project_id = project_employee.project_id where project_employee.project_id IS NULL", conn);
                lazyEmps = (int)cmd.ExecuteScalar();
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            trann.Dispose();
        }

        [TestMethod]
        public void getAllEmp()
        {
            EmployeeSqlDAL newEmp = new EmployeeSqlDAL(connectionString);
            List<Employee> listOfEmp = newEmp.GetAllEmployees();
            Assert.AreEqual(empCount + 1, listOfEmp.Count());

        }
        [TestMethod]
        public void SearchTest()
        {
            EmployeeSqlDAL thisemp = new EmployeeSqlDAL(connectionString);
            List<Employee> listOfEmps = thisemp.Search("John", "Fulton");
            Assert.AreEqual(tempEmp, listOfEmps[0].EmployeeId);

        }
        [TestMethod]
        public void LazyEmpTest()
        {
            EmployeeSqlDAL thisemp = new EmployeeSqlDAL(connectionString);
            List<Employee> listOfEmps = thisemp.GetEmployeesWithoutProjects();
            Assert.AreEqual(lazyEmps, listOfEmps.Count());

        }
    }
}
