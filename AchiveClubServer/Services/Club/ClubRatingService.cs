using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
{
    public class ClubRatingService
    {
        private ClubScoreService _scoreService;
        private IClubRepository _clubRepository;

        public ClubRatingService(ClubScoreService scoreService, IClubRepository clubRepository)
        {
            _scoreService = scoreService;
            _clubRepository = clubRepository;
        }

        public List<ClubWithScoreAndRating> GetClubRating()
        {
            var clubs = _clubRepository.GetAll();
            var clubsWithScore = new List<ClubWithScoreAndRating>();
            clubs.ForEach(club => clubsWithScore.Add(new ClubWithScoreAndRating
            {
                Club = club,
                AvgXP = _scoreService.GetClubAvgXP(club.Id, club.UsersCount)
            }));
            var sortedClubsWithScore = clubsWithScore.OrderBy(club => club.AvgXP).Reverse().ToList();
            sortedClubsWithScore.ForEach(club => club.RatingNumber = sortedClubsWithScore.IndexOf(club));
            return sortedClubsWithScore;
        }
    }
}
