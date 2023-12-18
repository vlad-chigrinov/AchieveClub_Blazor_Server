
using System.Linq;
using System.Collections.Generic;
using AchieveClubServer.Data.DTO;

namespace AchieveClubServer.Services
{
    public class TotalRatingMedals : IMedalsService
    {
        private UserRatingStorage _userStorage;
        public TotalRatingMedals(UserRatingStorage userRatingStorage)
        {
            _userStorage = userRatingStorage;
        }

        public List<UserMedal> GetAllMedals()
        {
            var medals = _userStorage.UserRating.Take(100).Select(u => new UserMedal { MedalRefId = 1, UserRefId = u.User.Id }).ToList();
            return medals;
        }
    }
}