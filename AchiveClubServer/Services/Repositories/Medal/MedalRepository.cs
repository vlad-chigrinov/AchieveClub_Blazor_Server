using AchieveClubServer.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AchieveClubServer.Services
{
    public class MedalRepository : IMedalRepository
    {
        private string connectionString = null;
        public MedalRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Medals WHERE Id = @id";
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

        public Medal GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Medal>("SELECT * FROM Medals WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Medal> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Medal>("SELECT * FROM Medals").ToList();
            }
        }

        public int Insert(Medal medal)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                    "INSERT INTO Medals(Title, Icon)" +
                    "VALUES(@Title, @Icon);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int? clubId = db.Query<int>(sqlQuery, medal).FirstOrDefault();
                return clubId.Value;
            }
        }

        public bool Update(Medal medal)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Medals SET Title = @Title, Icon = @Icon WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, medal);
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
