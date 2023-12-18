using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchieveClubServer.Data.DTO
{
    public class UserMedal
    {
     
            public int Id { get; set; }
            public int UserRefId { get; set; }
            public int MedalRefId { get; set; }
        
    }
}
