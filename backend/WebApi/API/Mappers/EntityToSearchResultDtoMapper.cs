using API.DTO;
using Application.Search;

namespace API.Mappers;

public static class EntityToSearchResultDtoMapper
{
    public static API.DTO.SearchResultDto Map( Application.Search.SearchResultDto result )
    {
        return new API.DTO.SearchResultDto
        {
            Property = PropertyDtoToPropertyRPMapper.Map( result.Property! ),
            RoomType = RoomTypeDtoToRoomTypeRPMapper.Map( result.RoomType! ),
            RoomsLeft = result.RoomsLeft
        };
    }
}
