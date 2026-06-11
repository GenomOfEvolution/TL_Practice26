namespace API.DTO;

public class SearchResultDTO
{
    public PropertyRP Property { get; set; } = null!;
    public RoomTypeRP RoomType { get; set; } = null!;
    public int RoomsLeft { get; set; }
}
