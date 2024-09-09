using System.Data;
using Microsoft.Data.SqlClient;
using VDWebApplication.Models;
using Dapper;
namespace VDWebApplication.Services
{
    public class UserService
    {
       // string connectionString = "Data Source=103.75.185.128;Initial Catalog=Vd;User ID=sa;Password=!Sway@2710#;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        string connectionString = "Data Source=DESKTOP-DA74FGD;Initial Catalog=Vd;User ID=sa;Password=123123;MultipleActiveResultSets=True;persist security info=True;Encrypt=False;Connection Timeout=1800;";
        protected IDbConnection _connection;

        public UserService() {
            _connection = new SqlConnection(connectionString);
        }

        public User GetUserById(string id) {
            string query = "select * from [user] where UserId=@id";
            return _connection.QueryFirstOrDefault<User>(query, new { id });
        }

        public List <User> GetListUser(string? id, string? nameSearch) 
        {
            string query = "select * from [user] where 1=1";

            if (!string.IsNullOrEmpty(id)) {
                query += " and UserId=@id ";
            }
            if (!string.IsNullOrEmpty(nameSearch))
            {
                nameSearch = "%" + nameSearch + "%";
                query += " and UserName like @nameSearch ";
            }
            return _connection.Query<User> (query,new {id, nameSearch } ).ToList(); //co tra du lieu ra
        }
        public void CreateUser(User model) {
            string query = "INSERT INTO [dbo].[User] ([UserId],[UserName],[UserAge],[Role]) VALUES (@UserId,@UserName,@UserAge,@Role)";
            _connection.Execute(query,model); // khng tra ra j ca
        }
        public void UpdateUser(User model)
        {
            string query = "UPDATE [dbo].[User] SET [UserName]=@UserName,[UserAge]=@UserAge,[Role]=@Role WHERE [UserId]=@UserId";
            _connection.Execute(query, model);
        }
        public void DeleteUser(string id)
        {
            string query = "DELETE FROM [dbo].[User] WHERE UserId=@id";
            _connection.Execute(query,new {id });
        }
    }
}
