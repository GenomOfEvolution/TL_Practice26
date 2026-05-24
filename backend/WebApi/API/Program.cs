using Domain.Repositories;
using Domain.Services;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddDbContext<AppDbContext>( options =>
{
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DefaultConnection" ) );
} );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

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
