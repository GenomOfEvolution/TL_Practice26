using Application.DTO;

namespace Application.DTO.Search;

public class SearchResultDto
{
    public PropertyDto Property { get; init; }
    public RoomTypeDto RoomType { get; init; }
    public int RoomsLeft { get; init; }

    public SearchResultDto( PropertyDto property, RoomTypeDto roomType, int roomsLeft )
    {
        Property = property;
        RoomType = roomType;
        RoomsLeft = roomsLeft;
    }
}
