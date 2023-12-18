using AchieveClubServer.Data.DTO;
using AchieveClubServer.Services;
using System.Collections.Generic;
using System.Linq;

namespace AchieveClubServer.Services
{
    public class UserMedalsService
    {
        private IUserMedalRepository _userMedalRepository;
        private IMedalRepository _medalRepository;
        public UserMedalsService(IUserMedalRepository userMedalRepository, IMedalRepository medalRepository)
        {
            _userMedalRepository = userMedalRepository;
            _medalRepository = medalRepository;
        }
        public List<Medal> GetMedalsByUserId(int userId)
        {
            var allUserMedals = _userMedalRepository.GetAll();
            var currentUserMedalIds = allUserMedals.Where(m => m.UserRefId == userId).Select(m=>m.MedalRefId).ToList();
            var allMedals = _medalRepository.GetAll();
            var currentUserMedals = allMedals.Where(m => currentUserMedalIds.Contains(m.Id)).ToList();
            return currentUserMedals;
        }
    }
}