using AchieveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System;

namespace AchieveClubServer.Services
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
                var sqlQuery = "DELETE FROM Achievements WHERE Id = @id";
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
                return db.Query<Achievement>("SELECT * FROM Achievements WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Achievement> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Achievement>("SELECT * FROM Achievements").ToList();
            }
        }

        public int Insert(Achievement achieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO Achievements(Xp, Title, Description, LogoURL, IsMultiple)" +
                    "VALUES(@Xp, @Title, @Description, @LogoURL, @IsMultiple);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? achieveId = db.Query<int>(sqlQuery, achieve).FirstOrDefault();
                return achieveId.Value;
            }
        }

        public bool Update(Achievement achieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Achievements SET Xp = @Xp, Title = @Title, Description = @Description, LogoURL = @LogoURL, IsMultiple = @IsMultiple WHERE Id = @Id";
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
