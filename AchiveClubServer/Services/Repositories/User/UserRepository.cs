using AchieveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace AchieveClubServer.Services
{
    public class UserRepository : IUserRepository
    {
        private string connectionString = null;
        public UserRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Users WHERE Id = @id";
                try
                {
                    db.Execute(sqlQuery, new { id });
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public User GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT *, ClubRefId as ClubId FROM Users WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<User> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<User>("SELECT *, ClubRefId as ClubId FROM Users").ToList();
            }
        }
        
        public int Insert(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO " +
                    "Users(FirstName, LastName, Email, Password, Avatar, ClubRefId) " +
                    "VALUES(@FirstName, @LastName, @Email, @Password, @Avatar, @ClubRefId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                return userId.Value;
            }
        }

        public bool Update(User user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password, Avatar = @Avatar, ClubRefId = @ClubRefId WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, user);
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }
    }
}
