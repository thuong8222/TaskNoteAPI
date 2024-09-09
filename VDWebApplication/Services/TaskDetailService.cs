using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using VDWebApplication.Models;

namespace VDWebApplication.Services
{
    public class TaskDetailService
    {
        string connectionString = "Data Source=DESKTOP-DA74FGD;Initial Catalog=Vd;User ID=sa;Password=123123;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        //string connectionString = "Data Source=103.75.185.128;Initial Catalog=Vd;User ID=sa;Password=!Sway@2710#;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        protected IDbConnection _connection;

        public TaskDetailService()
        {
            _connection = new SqlConnection(connectionString);
        }
        public TaskDetail GetTaskDetailById(string? id)
        {
            string query = "select * from [taskdetail] where 1=1";
            if (!string.IsNullOrEmpty(id))
            {
                query += " and TaskDetailId=@id ";
            }

            return _connection.QueryFirstOrDefault<TaskDetail>(query, new { id });
        }
        public List <TaskDetail> GetListTaskDetail(string? taskId)
        {
            string query = "select * from [taskdetail] where TaskId= @taskId";

            


            return _connection.Query<TaskDetail>(query, new {taskId}).ToList();
        }
        public void CreateTaskDetail(TaskDetail model)
        {
            string query = "INSERT INTO [dbo].[TaskDetail] ([TaskId],[TaskDetailId],[Description],[Status]) VALUES (@TaskId,@TaskDetailId,@Description,@Status)";
            _connection.Execute(query, model);

        }
        public void UpdateTotalJob(string taskId,int quantity)
        {
            string query = "UPDATE [dbo].[Tasks]  SET [TotalJob] += @quantity WHERE [TaskId] = @taskId";
            _connection.Execute(query, new { taskId, quantity });
        }
        public void UpdateTotalCompletedJob(string taskId, int quantity )
        {
            string query = "UPDATE [dbo].[Tasks] SET [TotalCompletedJob] += @quantity WHERE [TaskId] = @taskId ";
            _connection.Execute(query, new { taskId, quantity });
        }
        public void UpdateTaskDetail(TaskDetail model)
        {
            string query = "UPDATE [dbo].[TaskDetail] SET [TaskId]=@TaskId,[TaskDetailId]=@TaskDetailId,[Description]=@Description,[Status]=@Status WHERE [TaskDetailId]= @TaskDetailId";
            _connection.Execute(query, model);
        }
        public void DeleteTaskDetail(string taskDetailId)
        {
            string query = "DELETE FROM [dbo].[TaskDetail] WHERE [TaskDetailId]=@taskDetailId";
            _connection.Execute(query, new { taskDetailId });
        }
    }
}
