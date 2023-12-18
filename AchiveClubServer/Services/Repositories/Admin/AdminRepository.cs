using AchieveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace AchieveClubServer.Services
{
    public class AdminRepository : IAdminRepository
    {
        private string connectionString = null;
        public AdminRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Admins WHERE Id = @id";
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

        public Admin GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Admin>("SELECT * FROM Admins WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Admin> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Admin>("SELECT * FROM Admins").ToList();
            }
        }

        public int Insert(Admin admin)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Admins(Name, [Password]) VALUES(@Name, @Password); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? adminId = db.Query<int>(sqlQuery, admin).FirstOrDefault();
                return adminId.Value;
            }
        }

        public bool Update(Admin admin)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Admins SET Name = @Name, [Password] = @Password WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, admin);
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
