using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchiveClubServer.Data.DTO;
using System;

namespace AchiveClubServer.Services
{
    public class UserPageModelBuilder
    {
        private UserAchievementsService _achievements;
        private UserScoreService _userScore;
        private UserMedalsService _userMedalsService;
        private IUserRepository _users;
        private IClubRepository _clubs;
        public UserPageModelBuilder(UserAchievementsService achievements, UserScoreService userScore, UserMedalsService userMedalService, IUserRepository users, IClubRepository clubs)
        {
            _achievements = achievements;
            _userScore = userScore;
            _userMedalsService = userMedalService;
            _users = users;
            _clubs = clubs;
        }
        public UserPageModel Build(int userId)
        {
            var user = _users.GetById(userId);
            var achievements = _achievements.GetAchievementsByUserId(userId);
            var club = _clubs.GetById(user.ClubRefId);
            return new UserPageModel
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                ClubTitle = club.Title,
                ClubId = club.Id,
                Achivements = achievements,
                XPSum = _userScore.GetUserXP(userId),
                Medals = _userMedalsService.GetMedalsByUserId(userId)
            };
        }
    }
}
