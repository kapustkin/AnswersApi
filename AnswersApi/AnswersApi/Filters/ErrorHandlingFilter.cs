using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AnswersApi.Filters
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        public ErrorHandlingFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ErrorHandlingFilter>();
        }
        
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"TraceIdentifier: {context.HttpContext.TraceIdentifier}");
            
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            
            context.ExceptionHandled = true;
        }
    }
}