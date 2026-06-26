using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SafeEvent.Constants; 

namespace SafeEvent.Errors
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
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Excepción atrapada: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Instance = httpContext.Request.Path
            };

            switch (exception)
            {
                case TicketValidacionException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = ErrorMessages.TitleValidacion;
                    problemDetails.Type = ErrorMessages.TypeValidacion;
                    problemDetails.Detail = exception.Message;
                    break;

                case RegisterException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = ErrorMessages.TitleRegistro;
                    problemDetails.Type = ErrorMessages.TypeRegistro;
                    problemDetails.Detail = exception.Message;
                    break;

                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = ErrorMessages.TitleErrorInterno;
                    problemDetails.Type = ErrorMessages.TypeErrorInterno;
                    problemDetails.Detail = "Ocurrió un error inesperado en el servidor.";
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}