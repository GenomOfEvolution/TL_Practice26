using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public static class UpdateRoomTypeDtoToEntityMapper
{
    public static RoomType Map( UpdateRoomTypeDto dto )
    {
        return new RoomType
        {
            Id = dto.Id,
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
