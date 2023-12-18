using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;
using AchieveClubServer.Data.DTO;

namespace AchieveClubServer.Services
{
    public class UserRatingService
    {
        private UserScoreService _scoreService;
        private IUserRepository _userRepository;

        public UserRatingService(UserScoreService scoreService, IUserRepository userRepository)
        {
            _scoreService = scoreService;
            _userRepository = userRepository;
        }

        public List<UserWithRatingAndScore> GetUserRating()
        {
            var users = _userRepository.GetAll();
            users.RemoveAll(u => u.Email == "anonim@example.com");
            var usersWithScore = new List<UserWithRatingAndScore>();
            users.ForEach(user => usersWithScore.Add(new UserWithRatingAndScore
            {
                User = user,
                Score = _scoreService.GetUserXP(user.Id)
            }));
            var sortedUsersWithScore = usersWithScore.OrderBy(user => user.Score).Reverse().ToList();
            sortedUsersWithScore.ForEach(user => user.RatingNumber = sortedUsersWithScore.IndexOf(user));
            return sortedUsersWithScore;
        }
    }
}
