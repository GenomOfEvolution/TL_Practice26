using Domain.Enums;

namespace API.DTO;

public class UpdateRoomTypeRequset
{
    public int PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DailyPrice { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int TotalRoomsCount { get; set; }
    public List<string> Services { get; set; } = [];
    public List<string> Amenities { get; set; } = [];
    public Currency Currency { get; set; }
}
