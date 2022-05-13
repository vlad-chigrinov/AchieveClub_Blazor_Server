using AchiveClubServer.Data.DTO;
using AchiveClubServer.Services;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class UserMedalsService
    {
        private IUsersMedalRepository _userMedalRepository;
        private IMedalRepository _medalRepository;
        public UserMedalsService(IUsersMedalRepository userMedalRepository, IMedalRepository medalRepository)
        {
            _userMedalRepository = userMedalRepository;
            _medalRepository = medalRepository;
        }
        public List<Medal> GetMedalsByUserId(int userId)
        {
            var allUserMedals = _userMedalRepository.GetAll();
            var currentUserMedalIds = allUserMedals.Where(m => m.User == userId).Select(m=>m.Medal).ToList();
            var allMedals = _medalRepository.GetAll();
            var currentUserMedals = allMedals.Where(m => currentUserMedalIds.Contains(m.Id)).ToList();
            return currentUserMedals;
        }
    }
}