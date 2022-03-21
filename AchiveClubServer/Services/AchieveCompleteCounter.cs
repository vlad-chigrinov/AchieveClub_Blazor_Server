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
                return db.Query<int>("select * " +
                    "from CompletedAchivements as C " +
                    "where C.AchiveId = @Id",
                    new {Id = id})
                    .FirstOrDefault();
            }
        }
    }
}