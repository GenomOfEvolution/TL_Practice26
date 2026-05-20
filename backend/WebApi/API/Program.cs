using Domain.Repositories;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddDbContext<AppDbContext>( options =>
{
    options.UseSqlServer( "Server=localhost\\SQLEXPRESS;Database=hotels;Trusted_Connection=True;TrustServerCertificate=True;" );
} );

builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

var app = builder.Build();

if ( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler( "/Home/Error" );
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.Run();
