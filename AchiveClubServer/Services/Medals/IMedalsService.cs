using System.Collections.Generic;
using AchieveClubServer.Data.DTO;

namespace AchieveClubServer.Services
{
    public interface IMedalsService
    {
        public List<UserMedal> GetAllMedals();
    }
}