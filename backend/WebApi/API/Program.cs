using API.Middleware;
using Application;
using Infrastructure.Foundation;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddApplication();
builder.Services.AddInfrastructure( builder.Configuration );

builder.Services.AddControllers()
    .AddJsonOptions( options =>
        options.JsonSerializerOptions.Converters.Add( new System.Text.Json.Serialization.JsonStringEnumConverter() ) );
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
