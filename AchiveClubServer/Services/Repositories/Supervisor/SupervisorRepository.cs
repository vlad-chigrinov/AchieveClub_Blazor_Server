using AchiveClubServer.Data.DTO;

using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private string connectionString = null;
        public SupervisorRepository(string connection)
        {
            connectionString = connection;
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Supervisors WHERE Id = @id";
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

        public Supervisor GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Supervisor>("SELECT * FROM Supervisors WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public List<Supervisor> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Supervisor>("SELECT * FROM Supervisors").ToList();
            }
        }

        public int Insert(Supervisor sipervisor)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Supervisors(Name, [Key]) VALUES(@Name, @Key); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? supervisorId = db.Query<int>(sqlQuery, sipervisor).FirstOrDefault();
                return supervisorId.Value;
            }
        }

        public bool Update(Supervisor supervisor)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Supervisors SET Name = @Name, [Key] = @Key WHERE Id = @Id";
                try
                {
                    db.Execute(sqlQuery, supervisor);
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
