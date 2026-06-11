using API.DTO;
using Application.Search;

namespace API.Mappers;

public static class SearchRQToSearchFilterDtoMapper
{
    public static SearchFilterDto Map( SearchRQ request )
    {
        return new SearchFilterDto
        {
            City = request.City,
            ArrivalDate = request.ArrivalDate,
            DepartureDate = request.DepartureDate,
            Guests = request.Guests,
            MaxPrice = request.MaxPrice,
        };
    }
}
