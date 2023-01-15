using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AchiveClubServer.Services
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
                    "from CompletedAchivements as C " +
                    "join Users as U " +
                    "on C.UserId = U.Id " +
                    "where C.AchiveId = @Id ",
                    new {Id = id})
                    .FirstOrDefault();
            }
        }
        public int GetValueById(int id, int idClub)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<int>("select Count(*) " +
                    "from CompletedAchivements as C " +
                    "join Users as U " +
                    "on C.UserId = U.Id " +
                    "where C.AchiveId = @Id and U.ClubRefId="+idClub,
                    new { Id = id })
                    .FirstOrDefault();
            }
        }
    }
}