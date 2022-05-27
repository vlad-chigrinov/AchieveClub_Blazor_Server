
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
            var medals = _userStorage.UserRating.Take(3).Select(u => new UserMedal { Medal = 21, User = u.User.Id }).ToList();
            medals.AddRange(_userStorage.UserRating.Take(10).Select(u => new UserMedal { Medal = 22, User = u.User.Id }).ToList());
            medals.AddRange(_userStorage.UserRating.Take(25).Select(u => new UserMedal { Medal = 23, User = u.User.Id }).ToList());
            medals.AddRange(_userStorage.UserRating.Take(50).Select(u => new UserMedal { Medal = 24, User = u.User.Id }).ToList());
            medals.AddRange(_userStorage.UserRating.Take(100).Select(u => new UserMedal { Medal = 25, User = u.User.Id }).ToList());
            medals.AddRange(_userStorage.UserRating.Take(250).Select(u => new UserMedal { Medal = 26, User = u.User.Id }).ToList());
            return medals;
        }
    }
}