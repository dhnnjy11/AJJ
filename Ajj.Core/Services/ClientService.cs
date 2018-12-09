using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Ajj.Core.Services
{
    public class ClientService
    {
        private readonly IJobRepository _jobsRepository;
        private readonly IRepository<CompanyImage> _companyImageRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ClientService(IJobRepository jobsRepository,
            IRepository<CompanyImage> companyImageRepository,
            IHostingEnvironment hostingEnvironment
        )
        {
            _jobsRepository = jobsRepository;
            _companyImageRepository = companyImageRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public void UpdateNewCompanyImage(List<IFormFile> Files, int clientId)
        {

        }
    }
}