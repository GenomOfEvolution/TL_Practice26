using API.DTO;
using Application.DTO;

namespace API.Mappers;

public static class CreatePropertyRQToCreatePropertyDtoMapper
{
    public static CreatePropertyDto Map( CreatePropertyRQ request )
    {
        return new CreatePropertyDto
        {
            Name = request.Name,
            Country = request.Country,
            City = request.City,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
    }
}
