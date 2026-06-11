using Application.DTO;
using Domain.Entities;

namespace Application.Mappers;

public static class CreatePropertyDtoToEntityMapper
{
    public static Property Map( CreatePropertyDto dto )
    {
        return new Property
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };
    }
}
