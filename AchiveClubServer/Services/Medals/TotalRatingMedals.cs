
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
            List<UserMedal> medals3 = _userStorage.UserRating.Take(3).Select(u => new UserMedal { Medal = 21, User = u.User.Id }).ToList();
            List<UserMedal> medals10 =_userStorage.UserRating.Take(10).Select(u => new UserMedal { Medal = 22, User = u.User.Id }).ToList();
            List<UserMedal> medals25 = _userStorage.UserRating.Take(25).Select(u => new UserMedal { Medal = 23, User = u.User.Id }).ToList();
            List<UserMedal> medals50 = _userStorage.UserRating.Take(50).Select(u => new UserMedal { Medal = 24, User = u.User.Id }).ToList();
            List<UserMedal> medals100 =_userStorage.UserRating.Take(100).Select(u => new UserMedal { Medal = 25, User = u.User.Id }).ToList();
            List<UserMedal> medals250 = _userStorage.UserRating.Take(250).Select(u => new UserMedal { Medal = 26, User = u.User.Id }).ToList();         
            medals250 = deleteRepeating(medals100, medals250);
            medals100 = deleteRepeating(medals50, medals100);
            medals50 = deleteRepeating(medals25, medals50);
            medals25 = deleteRepeating(medals10, medals25);
            medals10 = deleteRepeating(medals3, medals10);
            medals3.AddRange(medals10);
            medals3.AddRange(medals25);
            medals3.AddRange(medals50);
            medals3.AddRange(medals100);
            medals3.AddRange(medals250);
            return medals3;
        }

        private List<UserMedal> deleteRepeating(List<UserMedal> medals1, List<UserMedal> medals2)
        {
            for (int j = 0; j < medals1.Count; j++)
            {
                for (int i = 0; i < medals2.Count; i++)
                {
                    if (medals1[j].User == medals2[i].User)
                    {
                        medals2.RemoveAt(i);
                        i--;
                    }
                }
            }
            return medals2;
        }
    }
}