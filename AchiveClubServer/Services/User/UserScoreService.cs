using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
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
                    "join CompletedAchivements C on C.UserId = U.Id " +
                    "join Achivements A on C.AchiveId = A.Id " +
                    "where U.Id = @Id";
                var argument = new { Id = userId };
                int? result =  db.Query<int?>(sqlQuery, argument).FirstOrDefault();
                return result ?? 0;
            }
        }

    }
}
