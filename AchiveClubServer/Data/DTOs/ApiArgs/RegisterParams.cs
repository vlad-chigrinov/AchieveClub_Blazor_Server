namespace AchiveClubServer.Data.DTO
{
    public class RegisterParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public int ClubId { get; set; }
    }
}
