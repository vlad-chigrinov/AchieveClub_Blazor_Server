
using System.Linq;
using System.Collections.Generic;
using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
{
    public class ClubRatingMedals : IMedalsService
    {
        private UserRatingStorage _userRatingService;
        private IClubRepository _clubRepository;
        public ClubRatingMedals(UserRatingStorage userRatingStorage, IClubRepository clubRepository)
        {
            _userRatingService = userRatingStorage;
            _clubRepository = clubRepository;
        }

        public List<UserMedal> GetAllMedals()
        {
            var medals = _clubRepository
                .GetAll()
                .Select(c => CalculateMedalsByClubId(c.Id))
                .Aggregate((l1, l2) => l1.Union(l2).ToList());
            return medals;
        }

        public List<UserMedal> CalculateMedalsByClubId(int clubId)
        {
            List<UserWithRatingAndScore> clubUsers25 = _userRatingService.UserRating.Where(u => u.User.ClubRefId == clubId).Take(25).ToList();
            List<UserWithRatingAndScore> clubUsers10 = _userRatingService.UserRating.Where(u=>u.User.ClubRefId == clubId).Take(10).ToList();
            var clubUsers3 = _userRatingService.UserRating.Where(u => u.User.ClubRefId == clubId).Take(3);
            for(int i=0; i<10;i++)
            {
                    clubUsers25.RemoveAt(0);
            }
            for (int i = 0; i < 3; i++)
            {
                clubUsers10.RemoveAt(0);
            }
            var medals = clubUsers25.Select(u => new UserMedal { Medal = 20, User = u.User.Id }).ToList();
            medals.AddRange(clubUsers10.Select(u => new UserMedal { Medal = 18, User = u.User.Id }).ToList());
            medals.AddRange(clubUsers3.Select(u => new UserMedal { Medal = 14, User = u.User.Id }).ToList());
            return medals;
        }
    }
}