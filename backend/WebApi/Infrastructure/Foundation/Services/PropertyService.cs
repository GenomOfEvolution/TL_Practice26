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
        ValidateProperty( property );

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
        ValidateProperty( property );

        Property? existing = await _propertyRepository.GetByIdAsync( property.Id, ct );

        if ( existing is null )
        {
            return;
        }

        existing.Update( property );

        await _unitOfWork.SaveChangesAsync( ct );
    }

    private static void ValidateProperty( Property property )
    {
        if ( string.IsNullOrWhiteSpace( property.Name ) )
        {
            throw new ArgumentException( "Название средства размещения не может быть пустым.",
                nameof( property.Name ) );
        }

        if ( string.IsNullOrWhiteSpace( property.Country ) )
        {
            throw new ArgumentException( "Страна не может быть пустой.",
                nameof( property.Country ) );
        }

        if ( string.IsNullOrWhiteSpace( property.City ) )
        {
            throw new ArgumentException( "Город не может быть пустым.",
                nameof( property.City ) );
        }

        if ( string.IsNullOrWhiteSpace( property.Address ) )
        {
            throw new ArgumentException( "Адрес не может быть пустым.",
                nameof( property.Address ) );
        }

        if ( property.Latitude < -90.0 || property.Latitude > 90.0 )
        {
            throw new ArgumentException( "Широта (Latitude) должна быть в диапазоне от -90 до 90.",
                nameof( property.Latitude ) );
        }

        if ( property.Longitude < -180.0 || property.Longitude > 180.0 )
        {
            throw new ArgumentException( "Долгота (Longitude) должна быть в диапазоне от -180 до 180.",
                nameof( property.Longitude ) );
        }
    }
}