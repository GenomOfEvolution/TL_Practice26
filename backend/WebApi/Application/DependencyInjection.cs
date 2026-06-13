using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication( this IServiceCollection services )
    {
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IRoomTypeService, RoomTypeService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ISearchService, SearchService>();

        return services;
    }
}
