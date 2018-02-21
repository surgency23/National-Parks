using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectDB.DAL
{
    public class ProjectSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAllProjects = "Select * from project";
        private const string SQL_employeeAssignment = @"INSERT INTO [dbo].[project_employee] ([project_id] ,[employee_id]) VALUES (@project_id, @employee_id)";
        private const string SQL_employeeRemoval = @"delete FROM project_employee WHERE project_id = @project_id AND employee_id = @employee_id";
        private const string SQL_newProject = "INSERT INTO[dbo].[project] ([name] ,[from_date] ,[to_date]) VALUES (@name ,@from_date ,@to_date)";

        // Single Parameter Constructor
        public ProjectSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> AllProjects = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllProjects, conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        Project p = new Project();
                        p.ProjectId = Convert.ToInt32(reader["project_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.StartDate = Convert.ToDateTime(reader["from_date"]);
                        p.EndDate = Convert.ToDateTime(reader["to_date"]);

                        AllProjects.Add(p);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return AllProjects;
        }

        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_employeeAssignment,conn);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
           
        }

        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_employeeRemoval,conn);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);

                }
            }
            catch(SqlException ex)
            {
                throw;
            }
         
        }

        public bool CreateProject(Project newProject)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_newProject, conn);
                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@from_date", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@to_date", newProject.EndDate);
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
