using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using VDWebApplication.Models;

namespace VDWebApplication.Services
{
    public class TaskService
    {
        string connectionString = "Data Source=DESKTOP-DA74FGD;Initial Catalog=Vd;User ID=sa;Password=123123;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        //string connectionString = "Data Source=103.75.185.128;Initial Catalog=Vd;User ID=sa;Password=!Sway@2710#;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        protected IDbConnection _connection;

        public TaskService()
        {
            _connection = new SqlConnection(connectionString);
        }
        public Tasks GetTaskById(string id)
        {
            string query = "select * from [tasks] where 1=1";
            if (!string.IsNullOrEmpty(id))
            {
                query += " and TaskId=@id ";
            }
           

            return _connection.QueryFirstOrDefault<Tasks>(query, new { id });
        }




        public List<Tasks> GetListTask(string? nameSearch)
        {
            string query = "select * from [tasks] where 1=1";

            if (!string.IsNullOrEmpty(nameSearch))
            {
                nameSearch = "%" + nameSearch + "%";
                query += " and TaskTitle like @nameSearch ";
            }
            return _connection.Query<Tasks>(query, new { nameSearch}).ToList();
        }
        public void CreateTask(Tasks model)
        {
            string query = "INSERT INTO [dbo].[Tasks] ([TaskId],[UserId],[TaskTitle],[TotalJob],[TotalCompletedJob],[CreateTime]) VALUES (@TaskId,@UserId,@TaskTitle,@TotalJob,@TotalCompletedJob,GetDate())";
            _connection.Execute(query, model);
        }
        public void UpdateTask(Tasks model)
        {
            string query = "UPDATE [dbo].[Tasks] SET [TaskTitle]=@TaskTitle,[TotalJob]=@TotalJob,[TotalCompletedJob]=@TotalCompletedJob WHERE [TaskId]=@TaskId";
            _connection.Execute(query, model);
        }
        public void DeleteTask(string id)
        {
            string query = "DELETE FROM [dbo].[Tasks] WHERE TaskId=@id";
            _connection.Execute(query, new { id });
        }

    }
}