using Application.Exceptions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware( RequestDelegate next )
    {
        _next = next;
    }

    public async Task InvokeAsync( HttpContext context )
    {
        try
        {
            await _next( context );
        }
        catch ( Exception exception )
        {
            var problemDetails = GetProblemDetails( exception );

            context.Response.StatusCode = problemDetails.Status.GetValueOrDefault();

            await context.Response.WriteAsJsonAsync( problemDetails );
        }
    }

    private static ProblemDetails GetProblemDetails( Exception exception )
    {
        return exception switch
        {
            EntityNotFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "NotFound",
                Title = "Not Found",
                Detail = exception.Message,
            },
            ApplicationValidationException or DomainException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = exception.Message,
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "ServerError",
                Title = "Server error",
                Detail = "An unexpected error has occured",
            }
        };
    }
}
