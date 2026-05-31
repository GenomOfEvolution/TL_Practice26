using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPropertyService _propertyService;

    public RoomTypeService( IUnitOfWork unitOfWork, IPropertyService propertyService )
    {
        _unitOfWork = unitOfWork;
        _propertyService = propertyService;
    }

    public async Task<RoomType> CreateAsync( RoomType roomType )
    {
        await ValidateRoomTypeAsync( roomType );

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
        await ValidateRoomTypeAsync( roomType );

        RoomType? existing = await _unitOfWork.RoomTypes.GetByIdAsync( roomType.Id );

        if ( existing is null )
        {
            return;
        }

        existing.Update( roomType );
        _unitOfWork.RoomTypes.Update( existing );

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task ValidateRoomTypeAsync( RoomType roomType )
    {
        Property? property = await _propertyService.GetByIdAsync( roomType.PropertyId )
            ?? throw new ArgumentException( "Средство размещения с указанным ID не найдено.", nameof( roomType.PropertyId ) );

        if ( string.IsNullOrWhiteSpace( roomType.Name ) )
        {
            throw new ArgumentException( "Название типа номера не может быть пустым.",
                nameof( roomType.Name ) );
        }

        if ( roomType.DailyPrice < 0 )
        {
            throw new ArgumentException( "Суточная цена не может быть отрицательной.",
                nameof( roomType.DailyPrice ) );
        }

        if ( roomType.MinPersonCount <= 0 )
        {
            throw new ArgumentException( "Минимальное количество гостей должно быть больше 0.",
                nameof( roomType.MinPersonCount ) );
        }

        if ( roomType.MinPersonCount > roomType.MaxPersonCount )
        {
            throw new ArgumentException( "Минимальное количество гостей не может превышать максимальное.",
                nameof( roomType.MaxPersonCount ) );
        }

        if ( roomType.TotalRoomsCount <= 0 )
        {
            throw new ArgumentException( "Общее количество номеров должно быть больше 0.",
                nameof( roomType.TotalRoomsCount ) );
        }
    }
}