using Application.DTO;

namespace Application.Search;

public class SearchResultDto
{
    public PropertyDto? Property { get; set; }
    public RoomTypeDto? RoomType { get; set; }
    public int RoomsLeft { get; set; }

    public SearchResultDto( PropertyDto property, RoomTypeDto roomType, int roomsLeft )
    {
        Property = property;
        RoomType = roomType;
        RoomsLeft = roomsLeft;
    }
}
