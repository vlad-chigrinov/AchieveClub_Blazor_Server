using System;

namespace AchiveClubServer.Data.DTO
{
    public class CompletedAchievement
    {
        public int Id { get; set; }
        public int SupervisorId { get; set; }       
        public int UserId { get; set; }  
        public int AchiveId { get; set; }
        public DateTime DateOfCompletion { get; set; }
    }
}
