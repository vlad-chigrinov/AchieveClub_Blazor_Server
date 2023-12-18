using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchieveClubServer.Data.DTO;

namespace AchieveClubServer.Services
{
    public class ClubNamesService
    {
        private string connectionString = null;
        public ClubNamesService(string connection)
        {
            connectionString = connection;
        }

        public List<ClubNameInfo> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<ClubNameInfo>("select Id, Title from Clubs").ToList();
            }
        }
    }
}
