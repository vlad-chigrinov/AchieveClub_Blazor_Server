using AchiveClubServer.Data.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class ClubPageModelBuilder
    {
        private ClubUsersService _users;
        private IClubRepository _clubs;
        private ClubRatingStorage _clubRatingService;
        private UserRatingStorage _userRatingService;
        public ClubPageModelBuilder(ClubUsersService users, IClubRepository clubs, ClubRatingStorage clubRatingService, UserRatingStorage userRatingService)
        {
            _users = users;
            _clubs = clubs;
            _clubRatingService = clubRatingService;
            _userRatingService = userRatingService;
        }
        public ClubPageModel Build(int clubId)
        {
            var top3Users = _userRatingService
                .UserRating
                .Where(u => u.User.ClubRefId == clubId)
                .Take(10)
                .ToList();
            int clubRatingNumber = _clubRatingService.ClubRating.Where(club => club.Club.Id == clubId).Select(club=>club.RatingNumber).FirstOrDefault();
            var club = _clubs.GetById(clubId);

            return new ClubPageModel
            {
                Club = club,
                RatingPosition = clubRatingNumber,
                TopUsers = top3Users
            };
        }
    }
}
