using API.Middleware;
using Application;
using Infrastructure.Foundation;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration,
    typeof( Infrastructure.Migrations.AssemblyMarker ).Assembly.GetName().Name! );

builder.Services.AddControllers();
builder.Services.AddRouting( options => options.LowercaseUrls = true );
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
