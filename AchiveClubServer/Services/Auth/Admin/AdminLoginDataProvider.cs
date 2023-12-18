using System.Linq;
using System;
using AchieveClubServer.Data.DTO;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AchieveClubServer.Services
{
    public class AdminLoginDataProvider
    {
        private string connectionString = null;
        public AdminLoginDataProvider(string connection)
        {
            connectionString = connection;
        }

        public Admin LoginAdmin(LoginParams loginParams)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select * from Admins A " +
                    "where A.Password = @Password and A.Name = @Email";

                var result = db.Query<Admin>(sqlQuery, loginParams).FirstOrDefault();

                return result ?? throw new Exception("Admin login failed");
            }
        }
    }
}
