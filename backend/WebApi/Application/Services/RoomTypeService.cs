using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

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

    public async Task<int> CreateAsync( CreateRoomTypeDto dto, CancellationToken ct )
    {
        ThrowIfInvalidRoomType( dto.Name, dto.DailyPrice, dto.MinPersonCount, dto.MaxPersonCount, dto.TotalRoomsCount );

        await _propertyService.GetByIdAsync( dto.PropertyId, CancellationToken.None );

        RoomType roomType = dto.MapToRoomTypeEntity();

        await _roomTypeRepository.AddAsync( roomType, ct );
        await _unitOfWork.SaveChangesAsync( ct );

        return roomType.Id;
    }

    public async Task DeleteAsync( int id, CancellationToken ct )
    {
        RoomType roomType = await GetByIdOrThrow( id, ct );

        _roomTypeRepository.Delete( roomType );
        await _unitOfWork.SaveChangesAsync( ct );
    }

    public async Task<RoomTypeDto> GetByIdAsync( int id, CancellationToken ct )
    {
        RoomType roomType = await GetByIdOrThrow( id, ct );

        return roomType.MapToRoomTypeDto();
    }

    public async Task<IReadOnlyList<RoomTypeDto>> GetByPropertyIdAsync( int propertyId, CancellationToken ct )
    {
        await _propertyService.GetByIdAsync( propertyId, ct );

        IEnumerable<RoomType> roomTypes = await _roomTypeRepository.GetByPropertyIdAsync( propertyId, ct );

        return roomTypes.Select( r => r.MapToRoomTypeDto() ).ToList();
    }

    public async Task UpdateAsync( UpdateRoomTypeDto dto, CancellationToken ct )
    {
        ThrowIfInvalidRoomType( dto.Name, dto.DailyPrice, dto.MinPersonCount, dto.MaxPersonCount, dto.TotalRoomsCount );

        await _propertyService.GetByIdAsync( dto.PropertyId, CancellationToken.None );

        RoomType existing = await GetByIdOrThrow( dto.Id, ct );

        existing.Name = dto.Name;
        existing.PropertyId = dto.PropertyId;
        existing.DailyPrice = dto.DailyPrice;
        existing.MinPersonCount = dto.MinPersonCount;
        existing.MaxPersonCount = dto.MaxPersonCount;
        existing.TotalRoomsCount = dto.TotalRoomsCount;
        existing.Services = dto.Services;
        existing.Amenities = dto.Amenities;
        existing.Currency = dto.Currency;

        await _unitOfWork.SaveChangesAsync( ct );
    }

    private async Task<RoomType> GetByIdOrThrow( int id, CancellationToken ct )
    {
        return await _roomTypeRepository.GetByIdAsync( id, ct )
            ?? throw new EntityNotFoundException( $"Тип номера с id {id} не найден." );
    }

    private static void ThrowIfInvalidRoomType(
        string name,
        decimal dailyPrice,
        int minPersonCount,
        int maxPersonCount,
        int totalRoomsCount )
    {
        if ( string.IsNullOrWhiteSpace( name ) )
        {
            throw new ApplicationValidationException( "Name не может быть пустым." );
        }

        if ( dailyPrice < 0 )
        {
            throw new ApplicationValidationException( "DailyPrice не может быть отрицательной." );
        }

        if ( minPersonCount <= 0 )
        {
            throw new ApplicationValidationException( "MinPersonCount должен быть больше 0." );
        }

        if ( minPersonCount > maxPersonCount )
        {
            throw new ApplicationValidationException( "MinPersonCount не может превышать MaxPersonCount." );
        }

        if ( totalRoomsCount < 0 )
        {
            throw new ApplicationValidationException( "TotalRoomsCount должен быть больше 0." );
        }
    }
}
