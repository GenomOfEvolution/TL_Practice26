using API.DTO;
using Domain.Results;

namespace API.Mappers;

public static class EntityToSearchResultDtoMapper
{
    public static SearchResultDTO Map( SearchResult result )
    {
        return new SearchResultDTO
        {
            Property = EntityToPropertyDtoMapper.Map( result.Property! ),
            RoomType = EntityToRoomTypeDtoMapper.Map( result.RoomType! ),
            RoomsLeft = result.RoomsLeft
        };
    }
}
