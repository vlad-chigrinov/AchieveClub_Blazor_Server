using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AchiveClubServer.Services
{
    public class AvatarChanger
    {
        private string connectionString = null;
        public AvatarChanger(string connection)
        {
            connectionString = connection;
        }

        public bool ChangeAvatarById(int id, string avatar)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var args = new { Avatar = avatar, Id = id };
                try
                {
                    db.Query<int>("update Users " +
                        "set Avatar = @Avatar " +
                        "where Id = @Id", args);
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