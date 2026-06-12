using Application.DTO;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Exceptions;
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
        Property property = CreatePropertyDtoToEntityMapper.Map( dto );

        ThrowIfInvalidProperty( property );

        await _propertyRepository.AddAsync( property, ct );
        await _unitOfWork.SaveChangesAsync( ct );

        return property.Id;
    }

    public async Task DeleteAsync( int id, CancellationToken ct )
    {
        Property property = await _propertyRepository.GetByIdAsync( id, ct )
            ?? throw new DomainException( $"Средство размещения с id {id} не найдено." );

        _propertyRepository.Delete( property );
        await _unitOfWork.SaveChangesAsync( ct );
    }

    public async Task<IReadOnlyList<PropertyDto>> GetAllAsync( CancellationToken ct )
    {
        IReadOnlyList<Property> properties = await _propertyRepository.GetAllAsync( ct );

        return properties.Select( EntityToPropertyDtoMapper.Map ).ToList();
    }

    public async Task<PropertyDto?> GetByIdAsync( int id, CancellationToken ct )
    {
        Property? property = await _propertyRepository.GetByIdAsync( id, ct );

        return property is null ? null : EntityToPropertyDtoMapper.Map( property );
    }

    public async Task UpdateAsync( UpdatePropertyDto dto, CancellationToken ct )
    {
        Property property = UpdatePropertyDtoToEntityMapper.Map( dto );

        ThrowIfInvalidProperty( property );

        Property? existing = await _propertyRepository.GetByIdAsync( property.Id, ct );

        if ( existing is null )
        {
            throw new DomainException( $"Средство размещения с id {property.Id} не найдено." );
        }

        existing.Update( property );

        await _unitOfWork.SaveChangesAsync( ct );
    }

    private static void ThrowIfInvalidProperty( Property property )
    {
        if ( string.IsNullOrWhiteSpace( property.Name ) )
        {
            throw new DomainException( $"{nameof( property.Name )} не может быть пустым." );
        }

        if ( string.IsNullOrWhiteSpace( property.Country ) )
        {
            throw new DomainException( $"{nameof( property.Country )} не может быть пустой." );
        }

        if ( string.IsNullOrWhiteSpace( property.City ) )
        {
            throw new DomainException( $"{nameof( property.City )} не может быть пустым." );
        }

        if ( string.IsNullOrWhiteSpace( property.Address ) )
        {
            throw new DomainException( $"{nameof( property.Address )} не может быть пустым." );
        }

        const double minLatitude = -90.0;
        const double maxLatitude = 90.0;

        if ( property.Latitude < minLatitude || property.Latitude > maxLatitude )
        {
            throw new DomainException( $"{nameof( property.Latitude )} должна быть в диапазоне от {minLatitude} до {maxLatitude}." );
        }

        const double minLongitude = -180.0;
        const double maxLongitude = 180.0;

        if ( property.Longitude < minLongitude || property.Longitude > maxLongitude )
        {
            throw new DomainException( $"{nameof( property.Longitude )} должна быть в диапазоне от {minLongitude} до {maxLongitude}." );
        }
    }
}
