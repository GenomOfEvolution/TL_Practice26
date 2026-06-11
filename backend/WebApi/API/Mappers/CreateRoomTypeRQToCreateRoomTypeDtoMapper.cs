using API.DTO;
using Application.DTO;
using Domain.Enums;

namespace API.Mappers;

public static class CreateRoomTypeRQToCreateRoomTypeDtoMapper
{
    public static CreateRoomTypeDto Map( CreateRoomTypeRQ request )
    {
        return new CreateRoomTypeDto
        {
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
