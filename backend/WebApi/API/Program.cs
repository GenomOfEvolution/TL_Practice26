using Infrastructure.Foundation;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddInfrastructure( builder.Configuration.GetConnectionString( "DefaultConnection" )! );

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
