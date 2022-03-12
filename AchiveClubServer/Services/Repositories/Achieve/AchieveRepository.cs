using AchiveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System;

namespace AchiveClubServer.Services
{
    public class AchieveRepository : IAchieveRepository
    {
        private string connectionString = null;
        public AchieveRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Achivements WHERE Id = @id";
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

        public Achievement GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Achievement>("SELECT * FROM Achivements WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Achievement> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Achievement>("SELECT * FROM Achivements").ToList();
            }
        }

        public int Insert(Achievement achieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO Achivements(Xp, Title, Description, LogoURL)" +
                    "VALUES(@Xp, @Title, @Description, @LogoURL);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? achieveId = db.Query<int>(sqlQuery, achieve).FirstOrDefault();
                return achieveId.Value;
            }
        }

        public bool Update(Achievement achieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Achivements SET Xp = @Xp, Title = @Title, Description = @Description, LogoURL = @LogoURL WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, achieve);
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
