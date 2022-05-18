
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
            var clubUsers = _userRatingService.UserRating.Where(u=>u.User.ClubRefId == clubId).Take(10);
            var medals = clubUsers.Select(u => new UserMedal { Medal = 8, User = u.User.Id }).ToList();
            return medals;
        }
    }
}