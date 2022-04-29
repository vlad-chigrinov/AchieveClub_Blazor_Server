using AchiveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class ClubRepository : IClubRepository
    {
        private string connectionString = null;
        public ClubRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Clubs WHERE Id = @id";
                try
                {
                    db.Execute(sqlQuery, new { id });
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public Club GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Club>("SELECT * FROM Clubs WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Club> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Club>("SELECT * FROM Clubs").ToList();
            }
        }

        public int Insert(Club club)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO Clubs(Title, Description, Address, LogoURL, UsersCount) " +
                    "VALUES(@Title, @Description, @Address, @LogoURL, @UsersCount);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? clubId = db.Query<int>(sqlQuery, club).FirstOrDefault();
                return clubId.Value;
            }
        }

        public bool Update(Club club)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Clubs SET Title = @Title, Description = @Description, Address = @Address, LogoURL = @LogoURL, UsersCount = @UsersCount WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, club);
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
