using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveClubServer.Data.DTO
{
    public class TopClubsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LogoURL { get; set; }
        public int AvgXP { get; set; }
    }
}
