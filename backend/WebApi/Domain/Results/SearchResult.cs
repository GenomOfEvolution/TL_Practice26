using Domain.Entities;

namespace Domain.Results;

public class SearchResult
{
    public Property Property { get; set; } = null!;
    public RoomType RoomType { get; set; } = null!;
    public int RoomsLeft { get; set; }

    public SearchResult()
    {
    }

    public SearchResult( Property property, RoomType roomType, int roomsLeft )
    {
        Property = property;
        RoomType = roomType;
        RoomsLeft = roomsLeft;
    }
}