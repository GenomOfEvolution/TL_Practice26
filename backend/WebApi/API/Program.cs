using API.Middleware;
using Infrastructure.Foundation;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddInfrastructure( builder.Configuration );

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
