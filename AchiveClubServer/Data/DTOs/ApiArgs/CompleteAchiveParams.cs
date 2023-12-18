using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveClubServer.Data.DTO
{
    public class CompleteAchieveParams
    {
        public List<int> AchievementsId { get; set; }
        public int UserId { get; set; }
        public string SupervisorKey { get; set; }
    }
}
