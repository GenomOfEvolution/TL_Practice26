using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PropertyService( IPropertyRepository propertyRepository, IUnitOfWork unitOfWork )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync( Property property, CancellationToken ct = default )
    {
        ThrowIfInvalidProperty( property );

        await _propertyRepository.AddAsync( property, ct );
        await _unitOfWork.SaveChangesAsync( ct );

        return property.Id;
    }

    public async Task DeleteAsync( int id, CancellationToken ct = default )
    {
        Property property = await _propertyRepository.GetByIdAsync( id, ct )
            ?? throw new DomainException( $"Средство размещения с id {id} не найдено." );

        _propertyRepository.Delete( property );
        await _unitOfWork.SaveChangesAsync( ct );
    }

    public async Task<IReadOnlyList<Property>> GetAllAsync( CancellationToken ct = default )
    {
        return ( await _propertyRepository.GetAllAsync( ct ) ).ToList();
    }

    public async Task<Property?> GetByIdAsync( int id, CancellationToken ct = default )
    {
        return await _propertyRepository.GetByIdAsync( id, ct );
    }

    public async Task UpdateAsync( Property property, CancellationToken ct = default )
    {
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

        const double MinLatitude = -90.0;
        const double MaxLatitude = 90.0;

        if ( property.Latitude < MinLatitude || property.Latitude > MaxLatitude )
        {
            throw new DomainException( $"{nameof( property.Latitude )} должна быть в диапазоне от {MinLatitude} до {MaxLatitude}." );
        }

        const double MinLongitude = -180.0;
        const double MaxLongitude = 180.0;

        if ( property.Longitude < MinLongitude || property.Longitude > MaxLongitude )
        {
            throw new DomainException( $"{nameof( property.Longitude )} должна быть в диапазоне от {MinLongitude} до {MaxLongitude}." );
        }
    }
}