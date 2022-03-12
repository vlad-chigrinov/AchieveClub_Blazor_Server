using AchiveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace AchiveClubServer.Services
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
                var sqlQuery = "DELETE FROM CompletedAchivements WHERE Id = @id";
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
                return db.Query<CompletedAchievement>("SELECT * FROM CompletedAchivements WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<CompletedAchievement> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<CompletedAchievement>("SELECT * FROM CompletedAchivements").ToList();
            }
        }

        public int Insert(CompletedAchievement completedAchieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO CompletedAchivements(SupervisorId, UserId, AchiveId, DateOfCompletion) " +
                    "VALUES(@SupervisorId, @UserId, @AchiveId, @DateOfCompletion); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? completedAchieveId = db.Query<int>(sqlQuery, completedAchieve).FirstOrDefault();
                return completedAchieveId.Value;
            }
        }

        public bool Update(CompletedAchievement completedAchieve)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE CompletedAchivements " +
                    "SET SupervisorId = @SupervisorId, UserId = @UserId, AchiveId = @AchiveId, DateOfCompletion = @DateOfCompletion " +
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
