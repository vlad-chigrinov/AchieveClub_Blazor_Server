using AchiveClubServer.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AchiveClubServer.Services
{
    public class UsersMedalRepository : IUsersMedalRepository
    {
        private string connectionString = null;
        public UsersMedalRepository(string connection)
        {
            connectionString = connection;
        }
        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM UsersMedals WHERE Id = @id";
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

        public List<UsersMedal> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<UsersMedal>("SELECT * FROM UsersMedals").ToList();
            }
        }

        public UsersMedal GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<UsersMedal>("SELECT * FROM UsersMedals WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public int Insert(UsersMedal value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO UsersMedals(user, medal)" +
                    "VALUES(@User, @Medal);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? clubId = db.Query<int>(sqlQuery, value).FirstOrDefault();
                return clubId.Value;
            }
        }

        public bool Update(UsersMedal value)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE UsersMedals SET User = @User, Medal = @Medal WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, value);
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
