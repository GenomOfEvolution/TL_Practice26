using API.DTO;
using Domain.Entities;

namespace API.Mappers;

public static class EntityToPropertyDtoMapper
{
    public static PropertyDTO Map( Property entity )
    {
        return new PropertyDTO
        {
            Name = entity.Name,
            Country = entity.Country,
            City = entity.City,
            Address = entity.Address,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
        };
    }
}
