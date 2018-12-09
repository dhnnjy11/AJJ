using Ajj.Core.Entities;
using Ajj.Core.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Ajj.Infrastructure.Services
{
    public class JobPraposalService
    {
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly IJobRepository _jobsRepository;
        private readonly IJobSeekerRepository _jobSeekerRepository;

        public JobPraposalService(IPostalCodeRepository postalCodeRepository,
           IJobRepository jobsRepository, IJobSeekerRepository jobSeekerRepository)
        {
            _jobsRepository = jobsRepository;
            _postalCodeRepository = postalCodeRepository;
            _jobSeekerRepository = jobSeekerRepository;
        }

        //public IEnumerable<Job> GetPraposedJobs(int jobseekerId)
        //{
        //    var jobseeker = _jobSeekerRepository.GetById(jobseekerId);
            

        //}
    }
}