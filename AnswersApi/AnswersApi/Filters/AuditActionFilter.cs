using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AnswersApi.Filters
{
    /// <summary>
    /// Фильтр аудита для всех запросов
    /// </summary>
    public class AuditActionFilter: IActionFilter
    {
        private readonly ILogger _logger;
        public AuditActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AuditActionFilter>();
        }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executed");
        }
    }
}