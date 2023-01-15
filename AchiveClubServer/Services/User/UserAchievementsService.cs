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
        private AchieveCompleteRatioCounter _achieveCompleteRatioCounter;
        public UserAchievementsService(string connection, IAchieveRepository achieveRepository, AchieveCompleteRatioCounter achieveCompleteRatioCounter)
        {
            connectionString = connection;
            _achieveRepository = achieveRepository;
            _achieveCompleteRatioCounter = achieveCompleteRatioCounter;
        }

        public List<UserPageAchieveItem> GetAchievementsByUserId(int userId)
        {
            List<int> completedAchievementsId;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select AchiveId from CompletedAchivements C where C.UserId = @Id";
                completedAchievementsId = db.Query<int>(sqlQuery, new { Id = userId }).ToList();
            }
            var achievements = _achieveRepository.GetAll().OrderBy(a => a.Xp);
            var achieveItems = new List<UserPageAchieveItem>();
            foreach (var achieve in achievements)
            {
                var achieveItem = new UserPageAchieveItem
                {
                    Id = achieve.Id,
                    Xp = achieve.Xp,
                    Title = achieve.Title,
                    Description = achieve.Description,
                    LogoURL = achieve.LogoURL,
                    IsMultiple = achieve.IsMultiple,
                };
                if(achieve.IsMultiple)
                {
                    achieveItem.CompletedCount = completedAchievementsId.Count(a => a == achieve.Id);
                    achieveItem.Completed = false;
                    achieveItem.UsersCompleteRatio = 0;
                }
                else
                {
                    achieveItem.Completed = completedAchievementsId.Contains(achieve.Id);
                    achieveItem.UsersCompleteRatio = _achieveCompleteRatioCounter.GetValueByAchieveId(achieve.Id);
                }
                achieveItems.Add(achieveItem);
            }
            return achieveItems;
        }

        public List<UserPageAchieveItem> GetAchievementsByUserId(int userId, int idClub)
        {
            List<int> completedAchievementsId;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select AchiveId from CompletedAchivements C where C.UserId = @Id";
                completedAchievementsId = db.Query<int>(sqlQuery, new { Id = userId }).ToList();
            }
            var achievements = _achieveRepository.GetAll().OrderBy(a => a.Xp);
            var achieveItems = new List<UserPageAchieveItem>();
            foreach (var achieve in achievements)
            {
                var achieveItem = new UserPageAchieveItem
                {
                    Id = achieve.Id,
                    Xp = achieve.Xp,
                    Title = achieve.Title,
                    Description = achieve.Description,
                    LogoURL = achieve.LogoURL,
                    IsMultiple = achieve.IsMultiple,
                };
                if (achieve.IsMultiple)
                {
                    achieveItem.CompletedCount = completedAchievementsId.Count(a => a == achieve.Id);
                    achieveItem.Completed = false;
                    achieveItem.UsersCompleteRatio = 0;
                }
                else
                {
                    achieveItem.Completed = completedAchievementsId.Contains(achieve.Id);
                    achieveItem.UsersCompleteRatio = _achieveCompleteRatioCounter.GetValueByAchieveId(achieve.Id,idClub);
                }
                achieveItems.Add(achieveItem);
            }
            return achieveItems;
        }
    }
}