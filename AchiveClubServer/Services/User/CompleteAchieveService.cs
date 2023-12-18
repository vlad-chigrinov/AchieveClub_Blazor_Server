using System.Collections.Generic;
using System.Linq;

using System.Data;
using System.Data.SqlClient;
using Dapper;

using AchieveClubServer.Data.DTO;
using System;

namespace AchieveClubServer.Services
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
        public List<CompletedAchievement> CompleteMultiple(CompleteAchieveParams completeParams)
        {
            int supervisorKey = GetSupervisorIdByKey(completeParams.SupervisorKey);
            var currentDate = DateTime.Now;

            var completedAchievements = new List<CompletedAchievement>();
            foreach(int achieveId in completeParams.AchievementsId)
            {
                var nextAchievement = new CompletedAchievement
                {
                    UserId = completeParams.UserId,
                    AchieveId = achieveId,
                    DateOfCompletion = currentDate,
                    SupervisorId = supervisorKey
                };
                try
                {
                    nextAchievement.Id = _completedAchieveRepository.Insert(nextAchievement);
                }
                catch
                {
                    throw new Exception("Error on complete achievement insert to DB!");
                }
                completedAchievements.Add(nextAchievement);
            }

            return completedAchievements;
        }

        public void DeleteMultiple(CompleteAchieveParams completeParams)
        {
            GetSupervisorIdByKey(completeParams.SupervisorKey);

            foreach(var achieveId in completeParams.AchievementsId)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    var sqlQuery = "DELETE FROM CompletedAchievements WHERE AchieveRefId = @AchieveId and UserRefId = @UserId";
                        db.Execute(sqlQuery, new { AchieveId = achieveId, UserId = completeParams.UserId });
                }
            }
        }

        private int GetSupervisorIdByKey(string key)
        {
            key = key.ToUpper();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "select S.Id from Supervisors S where S.[Key] = @Key";
                var argument = new { Key = key };
                int? completedAchieveId = db.Query<int?>(sqlQuery, argument).FirstOrDefault();
                if (completedAchieveId.HasValue)
                {
                    return completedAchieveId.Value;
                }
                else
                {
                    throw new Exception("Supervisor key not found!");
                }
            }
        }
    }
}