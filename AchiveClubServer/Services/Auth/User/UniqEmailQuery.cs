using AchieveClubServer.Data.DTO;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AchieveClubServer.Services
{
    public class UniqEmailQuery
    {
        private string connectionString = null;
        public UniqEmailQuery(string connection)
        {
            connectionString = connection;
        }
        public bool IsUniq(string email)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select count(*) from Users where Email = @Email";

                var args = new { Email = email };

                var result = db.Query<int>(sqlQuery, args).First();

                return result == 0;
            }
        }
    }
}
