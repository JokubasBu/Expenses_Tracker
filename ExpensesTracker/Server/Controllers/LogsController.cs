using ExpensesTracker.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ExpensesTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILoggerService _logger;
        public LogsController(ILoggerService logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PutLog(LogMessage message)
        {

            _logger.LogMessage(message.Message);

            return Ok();

        }
    }
}
