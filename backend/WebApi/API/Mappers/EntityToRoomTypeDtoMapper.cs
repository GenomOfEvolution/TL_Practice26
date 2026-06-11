using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class EntityToRoomTypeDtoMapper
{
    public static RoomTypeRP Map( RoomType entity )
    {
        return new RoomTypeRP
        {
            Id = entity.Id,
            PropertyId = entity.PropertyId,
            Name = entity.Name,
            DailyPrice = entity.DailyPrice,
            MinPersonCount = entity.MinPersonCount,
            MaxPersonCount = entity.MaxPersonCount,
            TotalRoomsCount = entity.TotalRoomsCount,
            Services = entity.Services.Select( s => s.ToString() ).ToList(),
            Amenities = entity.Amenities.Select( a => a.ToString() ).ToList(),
            Currency = entity.Currency,
        };
    }
}
