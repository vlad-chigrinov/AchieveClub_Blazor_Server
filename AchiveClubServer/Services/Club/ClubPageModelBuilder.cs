using AchiveClubServer.Data.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class ClubPageModelBuilder
    {
        private ClubUsersService _users;
        private IClubRepository _clubs;
        private ClubRatingService _clubRatingService;
        private UserRatingService _userRatingService;
        public ClubPageModelBuilder(ClubUsersService users, IClubRepository clubs, ClubRatingService clubRatingService, UserRatingService userRatingService)
        {
            _users = users;
            _clubs = clubs;
            _clubRatingService = clubRatingService;
            _userRatingService = userRatingService;
        }
        public ClubPageModel Build(int clubId)
        {
            var top3Users = _userRatingService
                .GetUserRating()
                .Where(u => u.User.ClubRefId == clubId)
                .Take(3)
                .ToList();
            int clubRatingNumber = _clubRatingService.GetClubRating().Where(club => club.Club.Id == clubId).Select(club=>club.RatingNumber).FirstOrDefault();
            var club = _clubs.GetById(clubId);

            return new ClubPageModel
            {
                Club = club,
                RatingPosition = clubRatingNumber,
                Top3Users = top3Users
            };
        }
    }
}
