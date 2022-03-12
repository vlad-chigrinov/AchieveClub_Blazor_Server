using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchiveClubServer.Data.DTO
{
    public class CompleteAchiveParams
    {
        public int AchieveId { get; set; }
        public int UserId { get; set; }
        public string SupervisorKey { get; set; }
    }
}
