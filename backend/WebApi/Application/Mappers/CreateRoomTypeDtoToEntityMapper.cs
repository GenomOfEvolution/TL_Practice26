using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public static class CreateRoomTypeDtoToEntityMapper
{
    public static RoomType Map( CreateRoomTypeDto dto )
    {
        return new RoomType
        {
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            TotalRoomsCount = dto.TotalRoomsCount,
            Services = dto.Services,
            Amenities = dto.Amenities,
            Currency = dto.Currency,
        };
    }
}
