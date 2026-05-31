using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class PropertyService : IPropertyService
{
    private readonly IUnitOfWork _unitOfWork;

    public PropertyService( IUnitOfWork unitOfWork )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Property> CreateAsync( Property property )
    {
        ValidateProperty( property );

        await _unitOfWork.Properties.AddAsync( property );
        await _unitOfWork.SaveChangesAsync();

        return property;
    }

    public async Task DeleteAsync( int id )
    {
        Property? property = await _unitOfWork.Properties.GetByIdAsync( id );

        if ( property is not null )
        {
            _unitOfWork.Properties.Delete( property );
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await _unitOfWork.Properties.GetAllAsync();
    }

    public async Task<Property?> GetByIdAsync( int id )
    {
        return await _unitOfWork.Properties.GetByIdAsync( id );
    }

    public async Task UpdateAsync( Property property )
    {
        ValidateProperty( property );

        Property? existing = await _unitOfWork.Properties.GetByIdAsync( property.Id );

        if ( existing is null )
        {
            return;
        }

        existing.Update( property );
        _unitOfWork.Properties.Update( existing );

        await _unitOfWork.SaveChangesAsync();
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