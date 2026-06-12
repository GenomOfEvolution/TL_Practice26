using Application.DTO;
using Application.Interfaces;
using Application.Mappers;
using Application.Reservations;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;

namespace Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPropertyService _propertyService;
    private readonly IRoomTypeService _roomTypeService;

    public ReservationService( IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IPropertyService propertyService, IRoomTypeService roomTypeService )
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _propertyService = propertyService;
        _roomTypeService = roomTypeService;
    }

    public async Task CancelAsync( int id, CancellationToken ct )
    {
        ReservationDto? reservationDto = await GetByIdAsync( id, ct );

        if ( reservationDto is null )
        {
            throw new DomainException( $"Бронь с id {id} не найдена." );
        }

        Reservation reservation = await _reservationRepository.GetByIdAsync( id, ct )
            ?? throw new DomainException( $"Бронь с id {id} не найдена." );

        reservation.SetCanceled( true );

        await _unitOfWork.SaveChangesAsync( ct );
    }

    public async Task<int> CreateAsync( CreateReservationDto dto, CancellationToken ct )
    {
        Reservation reservation = CreateReservationDtoToEntityMapper.Map( dto );

        ThrowIfInvalidDates(
            reservation.ArrivalDate,
            reservation.DepartureDate,
            reservation.ArrivalTime,
            reservation.DepartureTime );
        ThrowIfInvalidGuestInfo( reservation.GuestName, reservation.GuestPhoneNumber );

        PropertyDto property = await ThrowIfPropertyNotExists( reservation.PropertyId );
        RoomTypeDto roomType = await ThrowIfInvalidRoomType( reservation.RoomTypeId, reservation.PropertyId );

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

    public async Task<ReservationDto?> GetByIdAsync( int id, CancellationToken ct )
    {
        Reservation? reservation = await _reservationRepository.GetByIdAsync( id, ct );

        return reservation is null ? null : EntityToReservationDtoMapper.Map( reservation );
    }

    public async Task<IReadOnlyList<ReservationDto>> GetListAsync( ReservationFilterDto filter, CancellationToken ct )
    {
        var domainFilter = new Domain.Filters.ReservationFilter
        {
            PropertyId = filter.PropertyId,
            GuestName = filter.GuestName,
            DateFrom = filter.DateFrom,
            DateTo = filter.DateTo,
        };

        IEnumerable<Reservation> reservations = await _reservationRepository.GetByFiltersAsync( domainFilter, ct );

        return reservations.Select( EntityToReservationDtoMapper.Map ).ToList();
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
            throw new DomainException( "Дата заезда не может быть в прошлом." );
        }

        if ( departureDate <= arrivalDate )
        {
            throw new DomainException( "Дата выезда должна быть строго позже даты заезда." );
        }

        if ( departureDate == arrivalDate && departureTime <= arrivalTime )
        {
            throw new DomainException( "При заезде и выезде в один день время выезда должно быть позже времени заезда." );
        }
    }

    private static void ThrowIfInvalidGuestInfo( string guestName, string guestPhoneNumber )
    {
        if ( string.IsNullOrWhiteSpace( guestName ) )
        {
            throw new DomainException( "Имя гостя не может быть пустым." );
        }

        if ( string.IsNullOrWhiteSpace( guestPhoneNumber ) )
        {
            throw new DomainException( "Номер телефона гостя не может быть пустым." );
        }
    }

    private async Task<PropertyDto> ThrowIfPropertyNotExists( int propertyId )
    {
        return await _propertyService.GetByIdAsync( propertyId, CancellationToken.None )
            ?? throw new DomainException( "Средство размещения с указанным ID не найдено." );
    }

    private async Task<RoomTypeDto> ThrowIfInvalidRoomType( int roomTypeId, int propertyId )
    {
        RoomTypeDto roomType = await _roomTypeService.GetByIdAsync( roomTypeId, CancellationToken.None )
            ?? throw new DomainException( "Тип номера с указанным ID не найден." );

        if ( roomType.PropertyId != propertyId )
        {
            throw new DomainException( "Тип номера не относится к указанному средству размещения." );
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
