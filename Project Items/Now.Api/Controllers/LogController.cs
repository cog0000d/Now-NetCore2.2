using Microsoft.AspNetCore.Mvc;
using Now.Data.Interfaces;

namespace Now.Api.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        // GET
        public JsonResult Get()
        {
            return Json(_logRepository.Logs.GetAll());
        }
    }
}