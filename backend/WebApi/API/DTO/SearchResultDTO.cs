namespace API.DTO;

public class SearchResultDTO
{
    public PropertyDTO Property { get; set; } = null!;
    public RoomTypeDTO RoomType { get; set; } = null!;
    public int RoomsLeft { get; set; }
}
