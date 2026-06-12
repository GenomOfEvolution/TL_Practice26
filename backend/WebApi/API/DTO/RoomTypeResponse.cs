using Domain.Enums;

namespace API.DTO;

public class RoomTypeResponse
{
    public int Id { get; init; }
    public int PropertyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal DailyPrice { get; init; }
    public int MinPersonCount { get; init; }
    public int MaxPersonCount { get; init; }
    public int TotalRoomsCount { get; init; }
    public List<string> Services { get; init; } = [];
    public List<string> Amenities { get; init; } = [];
    public Currency Currency { get; init; }
}
