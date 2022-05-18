
using System.Linq;
using System.Collections.Generic;
using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
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
            var medals = _userStorage.UserRating.Take(100).Select(u => new UserMedal { Medal = 10, User = u.User.Id }).ToList();
            return medals;
        }
    }
}