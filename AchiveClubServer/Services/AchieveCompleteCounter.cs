using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AchieveClubServer.Services
{
    public class AchieveCompleteCounter
    {
        private string connectionString = null;
        public AchieveCompleteCounter(string connection)
        {
            connectionString = connection;
        }

        public int GetValueById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<int>("select Count(*) " +
                    "from CompletedAchievements as C " +
                    "join Users as U " +
                    "on C.UserRefId = U.Id " +
                    "where C.AchieveRefId = @Id ",
                    new {Id = id})
                    .FirstOrDefault();
            }
        }
    }
}