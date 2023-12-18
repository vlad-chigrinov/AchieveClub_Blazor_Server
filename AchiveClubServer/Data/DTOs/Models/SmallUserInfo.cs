using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveClubServer.Data.DTO
{
    public class SmallUserInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public string ClubIcon { get; set; }
        public int XPSum { get; set; }
    }
}
