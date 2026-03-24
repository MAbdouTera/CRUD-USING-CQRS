
using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace Api.Exceptions;

public sealed class GlobalExceptionHandler : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = exception switch
        {
            ValidationException ex => new ValidationProblemDetails
            (ex.Errors
            .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(s => s.ErrorMessage)
                  .ToArray()
                     ))
            {
                Title = "ValidationFailed",
                Status = StatusCodes.Status400BadRequest
            },
            NotFoundException ex => new ProblemDetails
            {
                Title = "Resource Not Found",
                Detail = ex.Message,
                Status = StatusCodes.Status404NotFound
            },
            _ => new ProblemDetails
            {
                Title = "Server Error",
                Detail = "An Unhandled Exception",
                Status = StatusCodes.Status500InternalServerError
            }
        };
        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await _problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
        return true;
    }
}