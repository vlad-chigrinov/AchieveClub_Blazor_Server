using AchieveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace AchieveClubServer.Services
{
    public class CompletedAchieveRepository : ICompletedAchieveRepository
    {
        private string connectionString = null;
        public CompletedAchieveRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM CompletedAchievements WHERE Id = @id";
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

        public CompletedAchievement GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<CompletedAchievement>("SELECT * FROM CompletedAchievements WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<CompletedAchievement> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<CompletedAchievement>("SELECT * FROM CompletedAchievements").ToList();
            }
        }

        public int Insert(CompletedAchievement completedAchieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO CompletedAchievements(SupervisorRefId, UserRefId, AchieveRefId, DateOfCompletion) " +
                    "VALUES(@SupervisorId, @UserId, @AchieveId, @DateOfCompletion); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? completedAchieveId = db.Query<int>(sqlQuery, completedAchieve).FirstOrDefault();
                return completedAchieveId.Value;
            }
        }

        public bool Update(CompletedAchievement completedAchieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE CompletedAchievements " +
                    "SET SupervisorRefId = @SupervisorId, UserRefId = @UserId, AchieveRefId = @AchieveId, DateOfCompletion = @DateOfCompletion " +
                    "WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, completedAchieve);
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
