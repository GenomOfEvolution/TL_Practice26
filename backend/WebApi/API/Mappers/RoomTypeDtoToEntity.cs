using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class RoomTypeDtoToEntity
{
    public static RoomType Map( RoomTypeDTO roomTypeDTO, int id = 0 )
    {
        return new RoomType
        {
            Id = id,
            PropertyId = roomTypeDTO.PropertyId,
            Name = roomTypeDTO.Name,
            DailyPrice = roomTypeDTO.DailyPrice,
            MinPersonCount = roomTypeDTO.MinPersonCount,
            MaxPersonCount = roomTypeDTO.MaxPersonCount,
            TotalRoomsCount = roomTypeDTO.TotalRoomsCount,
            Services = roomTypeDTO.Services,
            Amenities = roomTypeDTO.Amenities,
            Currency = roomTypeDTO.Currency,
        };
    }
}
