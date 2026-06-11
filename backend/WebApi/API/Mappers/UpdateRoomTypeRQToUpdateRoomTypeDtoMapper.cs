using API.DTO;
using Application.DTO;
using Domain.Enums;

namespace API.Mappers;

public static class UpdateRoomTypeRQToUpdateRoomTypeDtoMapper
{
    public static UpdateRoomTypeDto Map( UpdateRoomTypeRQ request, int id )
    {
        return new UpdateRoomTypeDto
        {
            Id = id,
            PropertyId = request.PropertyId,
            Name = request.Name,
            DailyPrice = request.DailyPrice,
            MinPersonCount = request.MinPersonCount,
            MaxPersonCount = request.MaxPersonCount,
            TotalRoomsCount = request.TotalRoomsCount,
            Services = request.Services.Select( s => Enum.Parse<ServiceType>( s ) ).ToList(),
            Amenities = request.Amenities.Select( a => Enum.Parse<AmenitiesType>( a ) ).ToList(),
            Currency = request.Currency,
        };
    }
}
