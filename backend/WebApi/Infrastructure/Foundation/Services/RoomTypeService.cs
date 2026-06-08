using Domain.Entities;
using Domain.Exceptions;
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
        RoomType roomType = await GetByIdAsync( id, ct )
            ?? throw new DomainException( $"Тип номера с id {id} не найден." );

        _roomTypeRepository.Delete( roomType );
        await _unitOfWork.SaveChangesAsync( ct );
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

        RoomType existing = await _roomTypeRepository.GetByIdAsync( roomType.Id, ct )
            ?? throw new DomainException( $"Тип номера с id {roomType.Id} не найден." );

        existing.Update( roomType );

        await _unitOfWork.SaveChangesAsync( ct );
    }

    private async Task ValidateRoomTypeAsync( RoomType roomType )
    {
        _ = await _propertyService.GetByIdAsync( roomType.PropertyId )
            ?? throw new DomainException( "Средство размещения с указанным ID не найдено." );

        if ( string.IsNullOrWhiteSpace( roomType.Name ) )
        {
            throw new DomainException( "Название типа номера не может быть пустым." );
        }

        if ( roomType.DailyPrice < 0 )
        {
            throw new DomainException( "Суточная цена не может быть отрицательной." );
        }

        if ( roomType.MinPersonCount <= 0 )
        {
            throw new DomainException( "Минимальное количество гостей должно быть больше 0." );
        }

        if ( roomType.MinPersonCount > roomType.MaxPersonCount )
        {
            throw new DomainException( "Минимальное количество гостей не может превышать максимальное." );
        }

        if ( roomType.TotalRoomsCount < 0 )
        {
            throw new DomainException( "Общее количество номеров должно быть больше 0." );
        }
    }
}