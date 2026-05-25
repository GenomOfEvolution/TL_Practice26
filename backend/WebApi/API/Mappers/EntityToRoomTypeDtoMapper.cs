using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class EntityToRoomTypeDtoMapper
{
    public static RoomTypeDTO Map( RoomType entity )
    {
        return new RoomTypeDTO
        {
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
