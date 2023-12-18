using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchieveClubServer.Data.DTO
{
    public class ClubPageModel
    {
        public Club Club { get; set; }
        public int RatingPosition { get; set; }
        public List<UserWithRatingAndScore> TopUsers { get; set; }
    }
}
