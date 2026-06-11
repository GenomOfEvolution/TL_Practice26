using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public static class UpdatePropertyDtoToEntityMapper
{
    public static Property Map( UpdatePropertyDto dto )
    {
        return new Property
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
