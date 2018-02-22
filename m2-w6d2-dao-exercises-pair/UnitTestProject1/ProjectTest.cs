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
    public class ProjectTest
    {
        private TransactionScope trann;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Projects;Integrated Security=True";
        private int ProjectCount = 0;
        private int NewProject = 0;
        private int tempEmp = 0;

        [TestInitialize]
        public void Initialize()
        {
            trann = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM project", conn);
                ProjectCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("INSERT INTO [dbo].[project] ([name],[from_date],[to_date]) VALUES ('fizz' , '12-20-1994' , '12-20-2018'); ", conn);
                NewProject = cmd.ExecuteNonQuery();

                cmd = new SqlCommand("INSERT INTO [dbo].[employee] ([department_id] ,[first_name] ,[last_name] ,[job_title] ,[birth_date] ,[gender] ,[hire_date]) VALUES (2, 'John', 'Fulton', 'IT Guy 1', '12-25-1980', 'M', '01-01-2018'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                tempEmp = (int)cmd.ExecuteScalar();


            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            trann.Dispose();
        }

        [TestMethod]
        public void GetAllProjects()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            List<Project> listOfProjects = newProject.GetAllProjects();
            Assert.AreEqual(ProjectCount + 1, listOfProjects.Count());

        }

        [TestMethod]
        public void AssignEmployeeToProject()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);  
            Assert.AreEqual(true, newProject.AssignEmployeeToProject(4, tempEmp));

        }

        [TestMethod]
        public void RemoveEmployeeFromProject()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            newProject.AssignEmployeeToProject(4, tempEmp);
            Assert.AreEqual(true, newProject.RemoveEmployeeFromProject(4, tempEmp));

        }
        [TestMethod]
        public void CreateProject()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            Project testProject = new Project();
            testProject.Name = "TestProject";
            testProject.StartDate = Convert.ToDateTime( "12-20-1994");
            testProject.EndDate = Convert.ToDateTime("01-01-2018");

            Assert.IsTrue(newProject.CreateProject(testProject));


        }
    }
}
