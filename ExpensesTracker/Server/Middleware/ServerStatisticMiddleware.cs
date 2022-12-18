using ExpensesTracker.Server.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Diagnostics;

namespace ExpensesTracker.Server.Middleware
{
    public class ServerStatisticMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerService> _logger;

        public ServerStatisticMiddleware(RequestDelegate next, ILogger<LoggerService> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {   
                var controllerActionDescriptor =
                    context
                    .GetEndpoint()
                    .Metadata
                    .GetMetadata<ControllerActionDescriptor>();

                if (controllerActionDescriptor != null)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    var controllerName = controllerActionDescriptor.ControllerName;
                    var actionName = controllerActionDescriptor.ActionName;

                    await _next(context);

                    sw.Stop();


                    _logger.LogInformation($"It took {sw.ElapsedMilliseconds} ms to perform " +
                        $"this action {actionName} in this controller {controllerName}");
                }
        }
    }
}
