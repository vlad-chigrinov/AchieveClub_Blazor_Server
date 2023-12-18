using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchieveClubServer.Data.DTO;

namespace AchieveClubServer.Services
{
    public class UserCounter
    {
        private string connectionString = null;
        public UserCounter(string connection)
        {
            connectionString = connection;
        }

        public int GetValue()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<int>("select Count(*) from Users").FirstOrDefault();
            }
        }
    }
}