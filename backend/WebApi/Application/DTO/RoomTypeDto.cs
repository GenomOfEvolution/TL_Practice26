using Domain.Enums;

namespace Application.DTO;

public class RoomTypeDto
{
    public int Id { get; set; }
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
