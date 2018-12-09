using Ajj.Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Ajj.Controllers
{
    public class UploadController : Controller
    {
        private object _IJobsRepository;

        public UploadController(IJobRepository jobRepository)
        {
            _IJobsRepository = jobRepository;
        }

        public IActionResult AllJobs()
        {
            return View();
        }
    }
}