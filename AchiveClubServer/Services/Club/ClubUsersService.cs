using AchiveClubServer.Data.DTO;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class ClubUsersService
    {
        private string connectionString = null;
        private IUserRepository _userRepository;
        public ClubUsersService(string connection, IUserRepository userRepository)
        {
            connectionString = connection;
            _userRepository = userRepository;
        }

        public List<User> GetUsersByClubId(int clubId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select Id from Users U where U.ClubRefId = @Id";
                var usersId = db.Query<int>(sqlQuery, new { Id = clubId }).ToList();
                return usersId.Select(id=>_userRepository.GetById(id)).ToList();
            }
        }
    }
}
