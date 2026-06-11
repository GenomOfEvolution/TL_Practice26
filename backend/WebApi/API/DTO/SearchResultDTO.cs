namespace API.DTO;

public class SearchResultDto
{
    public PropertyRP? Property { get; init; }
    public RoomTypeRP? RoomType { get; init; }
    public int RoomsLeft { get; init; }
}
