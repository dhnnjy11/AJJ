using Ajj.Core.Interface;
using Ajj.Core.Interface.Repository;
using Ajj.Infrastructure.Data;

namespace Ajj.Infrastructure.Repository
{
    internal class JobWrapperRepository : IJobWrapperRepository
    {
        private ApplicationDbContext _context;
        private IJobRepository _job;
        private IPostalCodeRepository _postalCode;
        private IBusinessStreamRepository _businessStream;

        public JobWrapperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IJobRepository Job
        {
            get
            {
                if (_job == null)
                {
                    _job = new JobRepository(_context);
                }

                return _job;
            }
        }

        public IPostalCodeRepository PostalCode
        {
            get
            {
                if (_postalCode == null)
                {
                    _postalCode = new PostalCodeRepository(_context);
                }

                return _postalCode;
            }
        }

        public IBusinessStreamRepository BusinessStream
        {
            get
            {
                if (_businessStream == null)
                {
                    _businessStream = new BusinessStreamRepository(_context);
                }

                return _businessStream;
            }
        }
    }
}