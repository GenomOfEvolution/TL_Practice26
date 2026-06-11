using API.DTO;
using Application.DTO;

namespace API.Mappers;

public static class PropertyDtoToPropertyRPMapper
{
    public static PropertyRP Map( PropertyDto dto )
    {
        return new PropertyRP
        {
            Id = dto.Id,
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };
    }
}
