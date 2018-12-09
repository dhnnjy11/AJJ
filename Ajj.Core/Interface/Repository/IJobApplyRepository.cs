using Ajj.Core.Entities;
using System.Collections.Generic;

namespace Ajj.Core.Interface
{
    public interface IJobApplyRepository : IRepository<JobApply>
    {
        List<JobApply> GetAppliedCount(long userId);

        int TotalJobAppliedTodayByCandidate(string UserId);

        int JobAppliedCount(ApplicationUser user);

        bool IsAlreadyApplied(string userId, long jobId);
    }
}