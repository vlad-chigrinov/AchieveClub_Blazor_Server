namespace AchieveClubServer.Data.DTO
{
    public class UserWithRatingAndScore
    {
        public User User { get; set; }
        public int RatingNumber { get; set; }
        public int Score { get; set; }
    }
}
