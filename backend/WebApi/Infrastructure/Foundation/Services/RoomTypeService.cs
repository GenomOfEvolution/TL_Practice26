using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPropertyService _propertyService;

    public RoomTypeService( IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork, IPropertyService propertyService )
    {
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
        _propertyService = propertyService;
    }

    public async Task<int> CreateAsync( RoomType roomType, CancellationToken ct = default )
    {
        await ValidateRoomTypeAsync( roomType );

        await _roomTypeRepository.AddAsync( roomType, ct );
        await _unitOfWork.SaveChangesAsync( ct );

        return roomType.Id;
    }

    public async Task DeleteAsync( int id, CancellationToken ct = default )
    {
        RoomType? roomType = await GetByIdAsync( id, ct );

        if ( roomType is not null )
        {
            _roomTypeRepository.Delete( roomType );
            await _unitOfWork.SaveChangesAsync( ct );
        }
    }

    public async Task<RoomType?> GetByIdAsync( int id, CancellationToken ct = default )
    {
        return await _roomTypeRepository.GetByIdAsync( id, ct );
    }

    public async Task<IReadOnlyList<RoomType>> GetByPropertyIdAsync( int propertyId, CancellationToken ct = default )
    {
        return ( await _roomTypeRepository.GetByPropertyIdAsync( propertyId, ct ) ).ToList();
    }

    public async Task UpdateAsync( RoomType roomType, CancellationToken ct = default )
    {
        await ValidateRoomTypeAsync( roomType );

        RoomType? existing = await _roomTypeRepository.GetByIdAsync( roomType.Id, ct );

        if ( existing is null )
        {
            return;
        }

        existing.Update( roomType );

        await _unitOfWork.SaveChangesAsync( ct );
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