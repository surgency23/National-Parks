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

            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            trann.Dispose();
        }

        [TestMethod]
        public void GetAllDeptartments()
        {
            DepartmentSqlDAL newDept = new DepartmentSqlDAL(connectionString);
            List<Department> listofDepts = newDept.GetDepartments();
            Assert.AreEqual(deptCount + 1, listofDepts.Count());

        }
        [TestMethod]
        public void UpdateDepartments()
        {
            DepartmentSqlDAL newDept = new DepartmentSqlDAL(connectionString);
            Department dept = new Department();
            dept.Id = 1;
            dept.Name = "ThatDept";
            newDept.UpdateDepartment(dept);
            List<Department> listofDepts = newDept.GetDepartments();
            bool istrue = false;
            foreach (Department item in listofDepts)//test this method by searching for the updated name and ID
            {
                if (item.Id==1 && item.Name =="ThatDept")
                {
                    istrue = true;
                }
            }
            Assert.AreEqual(true,istrue);

         //or test by simply seeing if method returns true Assert.IsTrue(newDept.UpdateDepartment(dept));
        }

        [TestMethod]
        public void CreateDepartment()
        {
            DepartmentSqlDAL newDept = new DepartmentSqlDAL(connectionString);
            Department dept = new Department();
            dept.Name = "NewDept";
            newDept.CreateDepartment(dept);
            List<Department> listofDepts = newDept.GetDepartments();
            bool istrue = false;
            foreach (Department item in listofDepts)//test this method by searching for the created name
            {
                if (item.Name == "NewDept")
                {
                    istrue = true;
                }
            }
            Assert.AreEqual(true, istrue);


            //or test by simply seeing if method returns true Assert.IsTrue(newDept.CreateDepartment(dept));

        }
    }
}
