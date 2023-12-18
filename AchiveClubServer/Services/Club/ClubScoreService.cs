using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using System;

namespace AchieveClubServer.Services
{
    public class ClubScoreService
    {
        private UserRatingService _userRatingService;
        public ClubScoreService(UserRatingService userRatingService)
        {
            _userRatingService = userRatingService;
        }
        public int GetClubAvgXP(int clubId, int usersCount)
        {
            var users = _userRatingService
                .GetUserRating()
                .Where(u => u.User.ClubRefId == clubId);
            if(usersCount != 0)
            {
                double avgXp = users.Sum(u=>u.Score) / usersCount;
                return Convert.ToInt32(avgXp);
            }
            else
            {
                return 0;
            }
        }
    }
}
