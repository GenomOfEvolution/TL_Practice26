using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoomTypeService( IUnitOfWork unitOfWork )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RoomType> CreateAsync( RoomType roomType )
    {
        await _unitOfWork.RoomTypes.AddAsync( roomType );
        await _unitOfWork.SaveChangesAsync();

        return roomType;
    }

    public async Task DeleteAsync( int id )
    {
        RoomType? roomType = await GetByIdAsync( id );

        if ( roomType is not null )
        {
            _unitOfWork.RoomTypes.Delete( roomType );
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<RoomType?> GetByIdAsync( int id )
    {
        return await _unitOfWork.RoomTypes.GetByIdAsync( id );
    }

    public async Task<IEnumerable<RoomType>> GetByPropertyIdAsync( int propertyId )
    {
        return await _unitOfWork.RoomTypes.GetByPropertyIdAsync( propertyId );
    }

    public async Task UpdateAsync( RoomType roomType )
    {
        RoomType? existing = await _unitOfWork.RoomTypes.GetByIdAsync( roomType.Id );

        if ( existing is null )
        {
            return;
        }

        existing.Update( roomType );
        _unitOfWork.RoomTypes.Update( existing );

        await _unitOfWork.SaveChangesAsync();
    }
}
