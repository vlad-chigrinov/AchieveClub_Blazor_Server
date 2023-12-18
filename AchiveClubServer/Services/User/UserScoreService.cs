using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchieveClubServer.Data.DTO;

namespace AchieveClubServer.Services
{
    public class UserScoreService
    {

        private string connectionString = null;
        public UserScoreService(string connection)
        {
            connectionString = connection;
        }
        public int GetUserXP(int userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select SUM(A.Xp) " +
                    "from Users U " +
                    "join CompletedAchievements C on C.UserRefId = U.Id " +
                    "join Achievements A on C.AchieveRefId = A.Id " +
                    "where U.Id = @Id";
                var argument = new { Id = userId };
                int? result =  db.Query<int?>(sqlQuery, argument).FirstOrDefault();
                return result ?? 0;
            }
        }

    }
}
