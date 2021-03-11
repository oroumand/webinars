using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop02.Clients.Mvc.Middlewares
{
    public class RequestStatusLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestStatusLoggerMiddleware> _logger;

        public RequestStatusLoggerMiddleware(RequestDelegate next, ILogger<RequestStatusLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
            _logger.LogInformation("Request for Path: {Path} Finished with status {Status}", httpContext.Request.Path, httpContext.Response.StatusCode);

        }
    }
}
