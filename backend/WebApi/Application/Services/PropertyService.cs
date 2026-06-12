using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PropertyService( IPropertyRepository propertyRepository, IUnitOfWork unitOfWork )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync( CreatePropertyDto dto, CancellationToken ct )
    {
        ThrowIfInvalidProperty( dto.Name, dto.Country, dto.City, dto.Address, dto.Latitude, dto.Longitude );

        Property property = CreatePropertyDtoToEntityMapper.Map( dto );

        await _propertyRepository.AddAsync( property, ct );
        await _unitOfWork.SaveChangesAsync( ct );

        return property.Id;
    }

    public async Task DeleteAsync( int id, CancellationToken ct )
    {
        Property property = await GetByIdOrThrow( id, ct );

        _propertyRepository.Delete( property );
        await _unitOfWork.SaveChangesAsync( ct );
    }

    public async Task<IReadOnlyList<PropertyDto>> GetAllAsync( CancellationToken ct )
    {
        IReadOnlyList<Property> properties = await _propertyRepository.GetAllAsync( ct );

        return properties.Select( EntityToPropertyDtoMapper.Map ).ToList();
    }

    public async Task<PropertyDto> GetByIdAsync( int id, CancellationToken ct )
    {
        Property property = await GetByIdOrThrow( id, ct );

        return EntityToPropertyDtoMapper.Map( property );
    }

    public async Task UpdateAsync( UpdatePropertyDto dto, CancellationToken ct )
    {
        ThrowIfInvalidProperty( dto.Name, dto.Country, dto.City, dto.Address, dto.Latitude, dto.Longitude );

        Property existing = await GetByIdOrThrow( dto.Id, ct );

        existing.Name = dto.Name;
        existing.Country = dto.Country;
        existing.City = dto.City;
        existing.Address = dto.Address;
        existing.Latitude = dto.Latitude;
        existing.Longitude = dto.Longitude;

        await _unitOfWork.SaveChangesAsync( ct );
    }

    private async Task<Property> GetByIdOrThrow( int id, CancellationToken ct )
    {
        return await _propertyRepository.GetByIdAsync( id, ct )
            ?? throw new EntityNotFoundException( $"Средство размещения с id {id} не найдено." );
    }

    private static void ThrowIfInvalidProperty(
        string name,
        string country,
        string city,
        string address,
        double latitude,
        double longitude )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ApplicationValidationException( "Name не может быть пустым." );
        }

        if ( string.IsNullOrWhiteSpace( country ) )
        {
            throw new ApplicationValidationException( "Country не может быть пустой." );
        }

        if ( string.IsNullOrWhiteSpace( city ) )
        {
            throw new ApplicationValidationException( "City не может быть пустым." );
        }

        if ( string.IsNullOrWhiteSpace( address ) )
        {
            throw new ApplicationValidationException( "Address не может быть пустым." );
        }

        const double minLatitude = -90.0;
        const double maxLatitude = 90.0;

        if ( latitude < minLatitude || latitude > maxLatitude )
        {
            throw new ApplicationValidationException( $"Latitude должна быть в диапазоне от {minLatitude} до {maxLatitude}." );
        }

        const double minLongitude = -180.0;
        const double maxLongitude = 180.0;

        if ( longitude < minLongitude || longitude > maxLongitude )
        {
            throw new ApplicationValidationException( $"Longitude должна быть в диапазоне от {minLongitude} до {maxLongitude}." );
        }
    }
}
