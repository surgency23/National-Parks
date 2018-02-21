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
    public class DeptTest
    {    
        private TransactionScope trann;   
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Projects;Integrated Security=True";
        private int deptCount = 0;
        private int tempDept = 0;
        private int updatedDept = 0;

        [TestInitialize]
        public void Initialize()
        {
            trann = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();
                cmd = new SqlCommand("SELECT COUNT(*) FROM department", conn);
                deptCount = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("insert into department(name) values('ThisDept')", conn);
                tempDept = cmd.ExecuteNonQuery();

                cmd = new SqlCommand("UPDATE department SET name = 'ThatDept' where department.name = 'ThisDept'", conn);
                updatedDept = cmd.ExecuteNonQuery();
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            trann.Dispose();
        }

        [TestMethod]
        public void getAllDepts()
        {
            DepartmentSqlDAL newDept = new DepartmentSqlDAL(connectionString);
            List<Department> listofDepts = newDept.GetDepartments();
            Assert.AreEqual(deptCount + 1, listofDepts.Count());

        }
        [TestMethod]
        public void insertedChangedNameDept()
        {
            DepartmentSqlDAL newDept = new DepartmentSqlDAL(connectionString);
            Assert.AreEqual(1,updatedDept);

        }
    }
}
