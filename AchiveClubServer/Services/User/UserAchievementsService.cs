using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
{
    public class UserAchievementsService
    {
        private string connectionString = null;
        private IAchieveRepository _achieveRepository;
        public UserAchievementsService(string connection, IAchieveRepository achieveRepository)
        {
            connectionString = connection;
            _achieveRepository = achieveRepository;
        }

        public List<UserPageAchieveItem> GetAchievementsByUserId(int userId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select AchiveId from CompletedAchivements C where C.UserId = @Id";
                var completedAchievementsId = db.Query<int>(sqlQuery, new { Id = userId }).ToList();
                var achievements = _achieveRepository.GetAll();
                var achieveItems = new List<UserPageAchieveItem>();
                foreach(var achieve in achievements)
                {
                    var achieveItem = new UserPageAchieveItem
                    {
                        Id = achieve.Id,
                        Xp = achieve.Xp,
                        Title = achieve.Title,
                        Description = achieve.Description,
                        LogoURL = achieve.LogoURL
                    };
                    achieveItem.Completed = completedAchievementsId.Contains(achieve.Id);
                    achieveItems.Add(achieveItem);
                }
                return achieveItems;
            }
        }
    }
}
