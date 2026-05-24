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
        await _unitOfWork.Properties.AddAsync( property );
        await _unitOfWork.SaveChangesAsync();

        return property;
    }

    public async Task DeleteAsync( int id )
    {
        var property = await _unitOfWork.Properties.GetByIdAsync( id );

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
        _unitOfWork.Properties.Update( property );
        await _unitOfWork.SaveChangesAsync();
    }
}
