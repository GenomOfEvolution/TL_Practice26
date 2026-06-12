namespace API.DTO;

public class SearchResultDto
{
    public PropertyResponse? Property { get; init; }
    public RoomTypeResponse? RoomType { get; init; }
    public int RoomsLeft { get; init; }
}
