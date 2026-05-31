using Domain.Repositories;
using Domain.Services;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Foundation;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, string connectionString )
    {
        services.AddDbContext<AppDbContext>( options =>
            options.UseSqlServer( connectionString, sql =>
                sql.MigrationsAssembly( "Infrastructure.Migrations" ) ) );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IRoomTypeService, RoomTypeService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ISearchService, SearchService>();

        return services;
    }
}
