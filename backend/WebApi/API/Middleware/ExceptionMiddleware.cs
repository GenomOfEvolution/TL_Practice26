using System.Net;
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
        catch ( EntityNotFoundException ex )
        {
            await WriteProblemDetailsAsync( context, ( int )HttpStatusCode.NotFound, ex.Message );
        }
        catch ( ApplicationValidationException ex )
        {
            await WriteProblemDetailsAsync( context, ( int )HttpStatusCode.BadRequest, ex.Message );
        }
        catch ( DomainException ex )
        {
            await WriteProblemDetailsAsync( context, ( int )HttpStatusCode.BadRequest, ex.Message );
        }
    }

    private static async Task WriteProblemDetailsAsync( HttpContext context, int statusCode, string detail )
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitleForStatusCode( statusCode ),
            Detail = detail,
            Instance = context.Request.Path,
        };

        await context.Response.WriteAsJsonAsync( problemDetails );
    }

    private static string GetTitleForStatusCode( int statusCode ) => statusCode switch
    {
        400 => "Bad Request",
        404 => "Not Found",
        _ => "Error",
    };
}
