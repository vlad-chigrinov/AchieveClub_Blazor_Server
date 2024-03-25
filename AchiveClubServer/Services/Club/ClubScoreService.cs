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
        private UserRatingStorage _userRatingStorage;
        public ClubScoreService(UserRatingStorage userRatingStorage)
        {
            _userRatingStorage = userRatingStorage;
        }
        public int GetClubAvgXP(int clubId)
        {
                var clubUsers = _userRatingStorage
                .UserRating
                .Where(u => u.User.ClubRefId == clubId);
                double avgXp = clubUsers.Sum(u=>u.Score) / clubUsers.Count();
                return Convert.ToInt32(avgXp);
        }
    }
}
