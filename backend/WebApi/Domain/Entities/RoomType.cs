using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Категория номера
/// </summary>
public class RoomType
{
    public int Id { get; private init; }
    public int PropertyId { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal DailyPrice { get; set; }

    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int TotalRoomsCount { get; set; }

    public List<ServiceType> Services { get; set; } = [];
    public List<AmenitiesType> Amenities { get; set; } = [];

    public Currency Currency { get; set; }
}
