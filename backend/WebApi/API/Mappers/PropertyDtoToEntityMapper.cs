using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class PropertyDtoToEntityMapper
{
    public static Property Map( PropertyDTO dto, int id = 0 )
    {
        return new Property
        {
            Id = id,
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };
    }
}
