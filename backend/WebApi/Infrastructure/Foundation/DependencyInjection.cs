using Domain.Repositories;
using Infrastructure.Foundation.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Foundation;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure( this IServiceCollection services, IConfiguration configuration )
    {
        ArgumentNullException.ThrowIfNull( configuration );

        string? connectionString = configuration.GetConnectionString( "DefaultConnection" );

        services.AddDbContext<AppDbContext>( options =>
            options.UseSqlServer( connectionString, sql =>
            {
                sql.MigrationsAssembly( "Infrastructure.Migrations" );
                sql.MigrationsHistoryTable( "__EFMigrationsHistory" );
            } )
            .UseSnakeCaseNamingConvention() );


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

        return services;
    }
}
