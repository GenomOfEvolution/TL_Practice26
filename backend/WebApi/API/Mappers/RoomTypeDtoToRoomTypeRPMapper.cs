using API.DTO;
using Application.DTO;

namespace API.Mappers;

public static class RoomTypeDtoToRoomTypeRPMapper
{
    public static RoomTypeRP Map( RoomTypeDto dto )
    {
        return new RoomTypeRP
        {
            Id = dto.Id,
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            TotalRoomsCount = dto.TotalRoomsCount,
            Services = dto.Services.Select( s => s.ToString() ).ToList(),
            Amenities = dto.Amenities.Select( a => a.ToString() ).ToList(),
            Currency = dto.Currency,
        };
    }
}
