using Domain.Entities;

namespace Domain.Results;

public record SearchResult
(
    Property Property,
    RoomType RoomType,
    int RoomsLeft
);