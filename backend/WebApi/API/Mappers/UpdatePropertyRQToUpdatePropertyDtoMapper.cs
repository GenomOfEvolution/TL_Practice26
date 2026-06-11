using API.DTO;
using Application.DTO;

namespace API.Mappers;

public static class UpdatePropertyRQToUpdatePropertyDtoMapper
{
    public static UpdatePropertyDto Map( UpdatePropertyRQ request, int id )
    {
        return new UpdatePropertyDto
        {
            Id = id,
            Name = request.Name,
            Country = request.Country,
            City = request.City,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
    }
}
