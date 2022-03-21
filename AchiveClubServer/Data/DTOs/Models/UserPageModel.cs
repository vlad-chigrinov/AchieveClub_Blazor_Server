using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClubServer.Data.DTO
{
    public class UserPageModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public int ClubId { get; set; }
        public string ClubTitle { get; set; }
        public List<UserPageAchieveItem> Achivements { get; set; } = new List<UserPageAchieveItem>();
        public int AchievementsCount => Achivements.Count;
        public int CompletedAchievementsCount => Achivements.Count(a => a.Completed);
        public int CompletedRatio => (int)(((float)CompletedAchievementsCount / (float)AchievementsCount) * 100);
        public int XPSum => Achivements.Where(a => a.Completed).Sum(a => a.Xp);
    }
}
