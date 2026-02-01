using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace BudgetPlaner.Api.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken canellationToken)
        {
            if (exception is ValidationException validationException)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validierungsfehler",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Detail = "Ein oder mehrere Validierungsfehler sind aufgetreten."
                };

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

                problemDetails.Extensions.Add("errors", errors);

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: canellationToken);

                return true;
            }

            _logger.LogError(exception, "Ein unerwarteter Fehler ist aufgetreten.");

            return false;
        }
    }
}
