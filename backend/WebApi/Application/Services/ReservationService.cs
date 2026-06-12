using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappers;
using Application.Reservations;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;
using Domain.Repositories;

namespace Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPropertyService _propertyService;
    private readonly IRoomTypeService _roomTypeService;

    public ReservationService(
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork,
        IPropertyService propertyService,
        IRoomTypeService roomTypeService )
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _propertyService = propertyService;
        _roomTypeService = roomTypeService;
    }

    public async Task CancelAsync( int id, CancellationToken ct )
    {
        Reservation reservation = await GetByIdOrThrow( id, ct );

        reservation.SetCanceled( true );

        await _unitOfWork.SaveChangesAsync( ct );
    }

    public async Task<int> CreateAsync( CreateReservationDto dto, CancellationToken ct )
    {
        ThrowIfInvalidDates(
            dto.ArrivalDate,
            dto.DepartureDate,
            dto.ArrivalTime,
            dto.DepartureTime );
        ThrowIfInvalidGuestInfo( dto.GuestName, dto.GuestPhoneNumber );

        PropertyDto property = await GetPropertyByIdOrThrow( dto.PropertyId );
        RoomTypeDto roomType = await GetByRoomTypeIdOrThrow( dto.RoomTypeId, dto.PropertyId );

        Reservation reservation = dto.MapToReservationEntity();

        reservation.Total = CalculateTotal(
            roomType.DailyPrice,
            reservation.ArrivalDate,
            reservation.DepartureDate );
        reservation.Currency = roomType.Currency;

        await CheckAvailabilityAsync(
            reservation.RoomTypeId,
            reservation.ArrivalDate,
            reservation.DepartureDate,
            roomType.TotalRoomsCount,
            ct );

        await _reservationRepository.AddAsync( reservation, ct );
        await _unitOfWork.SaveChangesAsync( ct );

        return reservation.Id;
    }

    public async Task<ReservationDto> GetByIdAsync( int reservationId, CancellationToken ct )
    {
        Reservation reservation = await GetByIdOrThrow( reservationId, ct );

        return reservation.MapToReservationDto();
    }

    public async Task<IReadOnlyList<ReservationDto>> GetListAsync( ReservationFilterDto filter, CancellationToken ct )
    {
        var domainFilter = new ReservationFilter
        {
            PropertyId = filter.PropertyId,
            GuestName = filter.GuestName,
            DateFrom = filter.DateFrom,
            DateTo = filter.DateTo,
        };

        IEnumerable<Reservation> reservations = await _reservationRepository.GetByFiltersAsync( domainFilter, ct );

        return reservations.Select( r => r.MapToReservationDto() ).ToList();
    }

    private async Task<Reservation> GetByIdOrThrow( int reservationId, CancellationToken ct )
    {
        return await _reservationRepository.GetByIdAsync( reservationId, ct )
            ?? throw new EntityNotFoundException( $"Бронь с id {reservationId} не найдена." );
    }
    private static void ThrowIfInvalidDates(
        DateOnly arrivalDate,
        DateOnly departureDate,
        TimeOnly arrivalTime,
        TimeOnly departureTime )
    {
        DateOnly today = DateOnly.FromDateTime( DateTime.Today );

        if ( arrivalDate < today )
        {
            throw new ApplicationValidationException( "Дата заезда не может быть в прошлом." );
        }

        if ( departureDate <= arrivalDate )
        {
            throw new ApplicationValidationException( "Дата выезда должна быть строго позже даты заезда." );
        }

        if ( departureDate == arrivalDate && departureTime <= arrivalTime )
        {
            throw new ApplicationValidationException( "При заезде и выезде в один день время выезда должно быть позже времени заезда." );
        }
    }

    private static void ThrowIfInvalidGuestInfo( string guestName, string guestPhoneNumber )
    {
        if ( string.IsNullOrWhiteSpace( guestName ) )
        {
            throw new ApplicationValidationException( "Имя гостя не может быть пустым." );
        }

        if ( string.IsNullOrWhiteSpace( guestPhoneNumber ) )
        {
            throw new ApplicationValidationException( "Номер телефона гостя не может быть пустым." );
        }
    }

    private async Task<PropertyDto> GetPropertyByIdOrThrow( int propertyId )
    {
        return await _propertyService.GetByIdAsync( propertyId, CancellationToken.None );
    }

    private async Task<RoomTypeDto> GetByRoomTypeIdOrThrow( int roomTypeId, int propertyId )
    {
        RoomTypeDto roomType = await _roomTypeService.GetByIdAsync( roomTypeId, CancellationToken.None );

        if ( roomType.PropertyId != propertyId )
        {
            throw new ApplicationValidationException( "Тип номера не относится к указанному средству размещения." );
        }

        return roomType;
    }

    private static decimal CalculateTotal( decimal dailyPrice, DateOnly arrivalDate, DateOnly departureDate )
    {
        int nights = departureDate.DayNumber - arrivalDate.DayNumber;

        return dailyPrice * nights;
    }

    private async Task CheckAvailabilityAsync(
        int roomTypeId,
        DateOnly arrivalDate,
        DateOnly departureDate,
        int totalRoomsCount,
        CancellationToken ct )
    {
        IEnumerable<Reservation> overlaps = await _reservationRepository.GetOverlappingAsync(
            roomTypeId, arrivalDate, departureDate, ct );

        int bookedRooms = overlaps.Count();

        if ( bookedRooms >= totalRoomsCount )
        {
            throw new DomainException( "На выбранные даты нет доступных номеров данного типа." );
        }
    }
}
