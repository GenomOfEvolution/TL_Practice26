using API.DTO;
using Application.DTO;

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
            Services = request.Services,
            Amenities = request.Amenities,
            Currency = request.Currency,
        };
    }
}
