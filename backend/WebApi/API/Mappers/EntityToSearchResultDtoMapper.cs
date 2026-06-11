using API.DTO;
using Application.Search;

namespace API.Mappers;

public static class EntityToSearchResultDtoMapper
{
    public static SearchResultDTO Map( SearchResultDto result )
    {
        return new SearchResultDTO
        {
            Property = PropertyDtoToPropertyRPMapper.Map( result.Property! ),
            RoomType = RoomTypeDtoToRoomTypeRPMapper.Map( result.RoomType! ),
            RoomsLeft = result.RoomsLeft
        };
    }
}
