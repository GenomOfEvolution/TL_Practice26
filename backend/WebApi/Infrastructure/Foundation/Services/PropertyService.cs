using Domain.Entities;
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
        Property? property = await _propertyRepository.GetByIdAsync( id, ct );

        if ( property is not null )
        {
            _propertyRepository.Delete( property );
            await _unitOfWork.SaveChangesAsync( ct );
        }
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
            throw new ArgumentException( $"Средство размещения с id {property.Id} не найдено." );
        }

        existing.Update( property );

        await _unitOfWork.SaveChangesAsync( ct );
    }

    private static void ThrowIfInvalidProperty( Property property )
    {
        if ( string.IsNullOrWhiteSpace( property.Name ) )
        {
            throw new ArgumentException( $"{nameof( property.Name )} не может быть пустым.",
                nameof( property.Name ) );
        }

        if ( string.IsNullOrWhiteSpace( property.Country ) )
        {
            throw new ArgumentException( $"{nameof( property.Country )} не может быть пустой.",
                nameof( property.Country ) );
        }

        if ( string.IsNullOrWhiteSpace( property.City ) )
        {
            throw new ArgumentException( $"{nameof( property.City )} не может быть пустым.",
                nameof( property.City ) );
        }

        if ( string.IsNullOrWhiteSpace( property.Address ) )
        {
            throw new ArgumentException( $"{nameof( property.Address )} не может быть пустым.",
                nameof( property.Address ) );
        }

        const double MinLatitude = -90.0;
        const double MaxLatitude = 90.0;

        if ( property.Latitude < MinLatitude || property.Latitude > MaxLatitude )
        {
            throw new ArgumentException( $"{nameof( property.Latitude )} должна быть в диапазоне от {MinLatitude} до {MaxLatitude}.",
                nameof( property.Latitude ) );
        }

        const double MinLongitude = -180.0;
        const double MaxLongitude = 180.0;

        if ( property.Longitude < MinLongitude || property.Longitude > MaxLongitude )
        {
            throw new ArgumentException( $"{nameof( property.Longitude )} должна быть в диапазоне от {MinLongitude} до {MaxLongitude}.",
                nameof( property.Longitude ) );
        }
    }
}