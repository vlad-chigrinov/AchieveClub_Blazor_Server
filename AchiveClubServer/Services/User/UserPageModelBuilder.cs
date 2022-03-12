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
        private IUserRepository _users;
        private IClubRepository _clubs;
        public UserPageModelBuilder(UserAchievementsService achievements, IUserRepository users, IClubRepository clubs)
        {
            _achievements = achievements;
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
                ClubAddress = club.Address,
                ClubLogoURL = club.LogoURL,
                Achivements = achievements
            };
        }
    }
}
