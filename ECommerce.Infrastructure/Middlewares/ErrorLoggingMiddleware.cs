using ECommerce.Domain.Entities.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Middleware
{
    public class ErrorLoggingMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        #endregion Fields

        #region Public Constructors

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught by middleware.");

                // ✅ Open a new scope to resolve AppDbContext
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Logs.Add(new Log
                {
                    Level = "Error",
                    Message = ex.Message,
                    Exception = ex.ToString(),
                    Timestamp = DateTime.UtcNow
                });

                await dbContext.SaveChangesAsync();

                // Handle the response
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred.");
            }
        }

        #endregion Public Methods
    }
}