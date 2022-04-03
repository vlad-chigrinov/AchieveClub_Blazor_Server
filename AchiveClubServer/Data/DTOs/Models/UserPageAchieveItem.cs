using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClubServer.Data.DTO
{
    public class UserPageAchieveItem
    {
        public int Id { get; set; }
        public int Xp { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public bool Completed { get; set; } = false;
        public bool Selected { get; set; } = false;
        public double UsersCompleteRatio { get; set; }
    }
}