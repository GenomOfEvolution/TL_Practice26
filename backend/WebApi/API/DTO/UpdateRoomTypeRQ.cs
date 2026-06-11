using Domain.Enums;

namespace API.DTO;

public class UpdateRoomTypeRQ
{
    public int PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int TotalRoomsCount { get; set; }
    public List<ServiceType> Services { get; set; } = [];
    public List<AmenitiesType> Amenities { get; set; } = [];
    public Currency Currency { get; set; }
}
