namespace AchieveClubServer.Services
{
    public class AchieveCompleteRatioCounter
    {
        private AchieveCompleteCounter _achieveCompleteCounter;
        private UserCounter _userCounter;

        public AchieveCompleteRatioCounter(AchieveCompleteCounter achieveCompleteCounter, UserCounter userCounter)
        {
            _achieveCompleteCounter = achieveCompleteCounter;
            _userCounter = userCounter;
        }

        public double GetValueByAchieveId(int id)
        {
            int usersCount = _userCounter.GetValue();
            int completedCount = _achieveCompleteCounter.GetValueById(id);

            return ((double)completedCount / (double)usersCount) * 100.0;
        }
    }
}