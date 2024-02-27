using DataAccess.Context;
using DataAccess.Entities;
using DemoAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Services
{
    public class UserService
    {
        // For Entity Framework
        private readonly DemoAPIContext _dbContext;

        // For ADO .NET
        //private readonly string _connectionString = "Data Source=SQL6031.site4now.net;Initial Catalog=db_a1e9a4_reiniscoming;User Id=db_a1e9a4_reiniscoming_admin;Password=rein12345";

        public UserService(DemoAPIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            // For ADO .NET
            //var sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();
            //var sqlCommand = new SqlCommand("SELECT Id, UserName, Name, Email, Password FROM Users", sqlConnection);
            //var sqlDataReader = sqlCommand.ExecuteReader();
            //var users = new List<User>();
            //while (sqlDataReader.Read())
            //{
            //    User user = new User();
            //    user.Id = sqlDataReader.GetString(0);
            //    user.UserName = sqlDataReader.GetString(1);
            //    user.Name = sqlDataReader.GetString(2);
            //    user.Email = sqlDataReader.GetString(3);
            //    user.Password = sqlDataReader.GetString(4);

            //    users.Add(user);
            //}
            //sqlConnection.Close();
            //return users;

            // For Entity Framework
            return _dbContext.Users.AsNoTracking().ToList();
        }

        public User GetUserById(string id)
        {
            // For ADO .NET
            //var sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();
            //var sqlCommand = new SqlCommand("SELECT Id, UserName, Name, Email, Password FROM Users Where Id = @Id", sqlConnection);
            //sqlCommand.Parameters.AddWithValue("@Id", id);
            //var sqlDataReader = sqlCommand.ExecuteReader();

            //if (sqlDataReader.Read())
            //{
            //    User user = new User();
            //    user.Id = sqlDataReader.GetString(0);
            //    user.UserName = sqlDataReader.GetString(1);
            //    user.Name = sqlDataReader.GetString(2);
            //    user.Email = sqlDataReader.GetString(3);
            //    user.Password = sqlDataReader.GetString(4);

            //    return user;
            //}
            //sqlConnection.Close();

            //return null;

            // For Entity Framework
            return _dbContext.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(UserResquest userRequest)
        {
            // For ADO .NET
            //var sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();
            //var sqlCommand = new SqlCommand("INSERT INTO Users (Id, UserName, Name, Email, Password) " +
            //    "VALUES (@Id, @UserName, @Name, @Email, @Password)", sqlConnection);
            //sqlCommand.Parameters.AddWithValue("@Id", Guid.NewGuid().ToString());
            //sqlCommand.Parameters.AddWithValue("@UserName", userRequest.UserName);
            //sqlCommand.Parameters.AddWithValue("@Name", userRequest.Name);
            //sqlCommand.Parameters.AddWithValue("@Email", userRequest.Email);
            //sqlCommand.Parameters.AddWithValue("@Password", userRequest.Password);
            //sqlCommand.ExecuteNonQuery();
            //sqlConnection.Close();

            // For Entity Framework
            var toAdd = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userRequest.UserName,
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = userRequest.Password
            };
            _dbContext.Users.Add(toAdd);
            _dbContext.SaveChanges();
        }

        public bool UpdateUser(string id, UserResquest userRequest) 
        {
            // For ADO .NET
            //var user = GetUserById(id);
            //if (user == null)
            //{
            //    return false;
            //}

            //var sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();
            //var sqlCommand = new SqlCommand("UPDATE Users SET Username = @UserName, Name = @Name, Email = @Email, Password = @Password Where Id = @id", sqlConnection);
            //sqlCommand.Parameters.AddWithValue("@Id", id);
            //sqlCommand.Parameters.AddWithValue("@UserName", userRequest.UserName);
            //sqlCommand.Parameters.AddWithValue("@Name", userRequest.Name);
            //sqlCommand.Parameters.AddWithValue("@Email", userRequest.Email);
            //sqlCommand.Parameters.AddWithValue("@Password", userRequest.Password);
            //bool isSuccessUpdate = sqlCommand.ExecuteNonQuery() > 0;
            //sqlConnection.Close();

            //return isSuccessUpdate;

            // For Entity Framework
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.UserName = userRequest.UserName;
                existingUser.Email = userRequest.Email;
                existingUser.Name = userRequest.Name;
                existingUser.Password = userRequest.Password;

                return _dbContext.SaveChanges() > 0;
            }

            return false;
        }

        public bool UpdateEmail(string id, string email)
        {
            // For ADO .NET
            //var user = GetUserById(id);

            //if (user == null)
            //{
            //    return false;
            //}

            //var sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();
            //var sqlCommand = new SqlCommand("UPDATE Users SET Email = @Email Where Id = @id", sqlConnection);
            //sqlCommand.Parameters.AddWithValue("@Id", id);  
            //sqlCommand.Parameters.AddWithValue("@Email", email);

            //bool isSuccessUpdate = sqlCommand.ExecuteNonQuery() > 0;
            //sqlConnection.Close();

            //return isSuccessUpdate;

            // For Entity Framework
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Email = email;

                return _dbContext.SaveChanges() > 0;
            }

            return false;
        }


        public bool DeleteUser(string id) 
        {
            // For ADO .NET
            //var sqlConnection = new SqlConnection(_connectionString);
            //sqlConnection.Open();

            //var sqlCommand = new SqlCommand("Delete FROM Users Where Id = @id", sqlConnection);
            //sqlCommand.Parameters.AddWithValue("@Id", id);

            //bool isSuccessDelete = sqlCommand.ExecuteNonQuery() > 0;
            //sqlConnection.Close();

            //return isSuccessDelete;

            // For Entity Framework
            var deleteuser = _dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (deleteuser != null)
            {
                _dbContext.Users.Remove(deleteuser);
                return _dbContext.SaveChanges() > 0;

            }

            return false;
        }
    }
}
