using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public static class EntityToRoomTypeDtoMapper
{
    public static RoomTypeDto Map( RoomType entity )
    {
        return new RoomTypeDto
        {
            Id = entity.Id,
            PropertyId = entity.PropertyId,
            Name = entity.Name,
            DailyPrice = entity.DailyPrice,
            MinPersonCount = entity.MinPersonCount,
            MaxPersonCount = entity.MaxPersonCount,
            TotalRoomsCount = entity.TotalRoomsCount,
            Services = entity.Services,
            Amenities = entity.Amenities,
            Currency = entity.Currency,
        };
    }
}
