using System.Net;
using System.Text.Json;
using Domain.Exceptions;

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
        catch ( DomainException ex )
        {
            context.Response.StatusCode = ( int )HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new { error = ex.Message };
            await context.Response.WriteAsync( JsonSerializer.Serialize( response ) );
        }
    }
}
