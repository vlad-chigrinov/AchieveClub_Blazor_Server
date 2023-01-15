using AchiveClubServer.Data.DTO;
using System.Collections.Generic;
using System.Linq;

namespace AchiveClubServer.Services
{
    public class MedalService
    {
        private ClubRatingMedals _clubRatingMedals;
        private TotalRatingMedals _totalRatingMedals;
        private IUserMedalRepository _userMedalRepository;
        public MedalService(ClubRatingMedals clubRatingMedals, IUserMedalRepository userMedalRepository, TotalRatingMedals totalRatingMedals)
        {
            _clubRatingMedals = clubRatingMedals;
            _userMedalRepository = userMedalRepository;
            _totalRatingMedals = totalRatingMedals;
        }

        public void CalculateMedals()
        {
            List<IMedalsService> medalsServices = new List<IMedalsService>();
            medalsServices.Add(_clubRatingMedals);
           // medalsServices.Add(_totalRatingMedals);

            List<UserMedal> userMedals = medalsServices.Select(s => s.GetAllMedals()).Aggregate((l1, l2) => l1.Union(l2).ToList());
            userMedals.ForEach(m => _userMedalRepository.Insert(m));
        }
    }
}