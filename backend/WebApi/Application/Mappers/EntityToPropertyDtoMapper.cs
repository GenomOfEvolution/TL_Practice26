using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public static class EntityToPropertyDtoMapper
{
    public static PropertyDto Map( Property entity )
    {
        return new PropertyDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Country = entity.Country,
            City = entity.City,
            Address = entity.Address,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
        };
    }
}
