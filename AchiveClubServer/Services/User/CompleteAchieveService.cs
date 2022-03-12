using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchiveClubServer.Data.DTO;
using System;

namespace AchiveClubServer.Services
{
    public class CompleteAchieveService
    {
        private string connectionString = null;
        private ICompletedAchieveRepository _completedAchieveRepository;
        public CompleteAchieveService(ICompletedAchieveRepository completedAchieveRepository, string connection)
        {
            _completedAchieveRepository = completedAchieveRepository;
            connectionString = connection;
        }
        public CompletedAchievement Complete(CompleteAchiveParams completeParams)
        {
            completeParams.SupervisorKey = completeParams.SupervisorKey.ToUpper();
            var completedAchieve = new CompletedAchievement
            {
                UserId = completeParams.UserId,
                AchiveId = completeParams.AchieveId,
                DateOfCompletion = DateTime.Now
            };

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select S.Id from Supervisors S where S.[Key] = @Key";
                var argument = new { Key = completeParams.SupervisorKey };
                int? completedAchieveId = db.Query<int?>(sqlQuery, argument).FirstOrDefault();
                if (completedAchieveId.HasValue == false)
                {
                    throw new Exception("Complete achieve exception!");
                }
                completedAchieve.SupervisorId = completedAchieveId.Value;
            }

            completedAchieve.Id = _completedAchieveRepository.Insert(completedAchieve);

            return completedAchieve;
        }
    }
}