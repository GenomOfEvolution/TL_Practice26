using API.DTO;
using Application.DTO;

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
            Services = request.Services,
            Amenities = request.Amenities,
            Currency = request.Currency,
        };
    }
}
