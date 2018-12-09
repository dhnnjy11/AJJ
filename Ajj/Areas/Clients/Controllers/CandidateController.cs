using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ajj.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("clients")]
    [Authorize(Roles = "client")]
    public class CandidateController : Controller
    {
    }
}