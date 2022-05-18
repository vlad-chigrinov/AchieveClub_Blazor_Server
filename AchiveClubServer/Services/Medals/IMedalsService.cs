using System.Collections.Generic;
using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
{
    public interface IMedalsService
    {
        public List<UserMedal> GetAllMedals();
    }
}