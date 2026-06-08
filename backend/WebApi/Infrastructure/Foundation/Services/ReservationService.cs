using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

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

    public async Task CancelAsync( int id, CancellationToken ct = default )
    {
        Reservation? reservation = await GetByIdAsync( id, ct );

        if ( reservation != null )
        {
            reservation.SetCanceled( true );

            await _unitOfWork.SaveChangesAsync( ct );
        }
    }

    public async Task<int> CreateAsync( Reservation reservation, CancellationToken ct = default )
    {
        ThrowIfInvalidDates(
            reservation.ArrivalDate,
            reservation.DepartureDate,
            reservation.ArrivalTime,
            reservation.DepartureTime );
        ThrowIfInvalidGuestInfo( reservation.GuestName, reservation.GuestPhoneNumber );

        Property property = await ThrowIfPropertyNotExists( reservation.PropertyId );
        RoomType roomType = await ThrowIfInvalidRoomType( reservation.RoomTypeId, reservation.PropertyId );

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

    public async Task<Reservation?> GetByIdAsync( int id, CancellationToken ct = default )
    {
        return await _reservationRepository.GetByIdAsync( id, ct );
    }

    public async Task<IReadOnlyList<Reservation>> GetListAsync( ReservationFilter filter, CancellationToken ct = default )
    {
        return ( await _reservationRepository.GetByFiltersAsync( filter, ct ) ).ToList();
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
            throw new ArgumentException( "Дата заезда не может быть в прошлом.",
                nameof( arrivalDate ) );
        }

        if ( departureDate <= arrivalDate )
        {
            throw new ArgumentException( "Дата выезда должна быть строго позже даты заезда.",
                nameof( departureDate ) );
        }

        if ( departureDate == arrivalDate && departureTime <= arrivalTime )
        {
            throw new ArgumentException( "При заезде и выезде в один день время выезда должно быть позже времени заезда.",
                nameof( departureTime ) );
        }
    }

    private static void ThrowIfInvalidGuestInfo( string guestName, string guestPhoneNumber )
    {
        if ( string.IsNullOrWhiteSpace( guestName ) )
        {
            throw new ArgumentException( "Имя гостя не может быть пустым.",
                nameof( guestName ) );
        }

        if ( string.IsNullOrWhiteSpace( guestPhoneNumber ) )
        {
            throw new ArgumentException( "Номер телефона гостя не может быть пустым.",
                nameof( guestPhoneNumber ) );
        }
    }

    private async Task<Property> ThrowIfPropertyNotExists( int propertyId )
    {
        return await _propertyService.GetByIdAsync( propertyId )
            ?? throw new ArgumentException( "Средство размещения с указанным ID не найдено.", nameof( propertyId ) );
    }

    private async Task<RoomType> ThrowIfInvalidRoomType( int roomTypeId, int propertyId )
    {
        RoomType roomType = await _roomTypeService.GetByIdAsync( roomTypeId )
            ?? throw new ArgumentException( "Тип номера с указанным ID не найден.",
            nameof( roomTypeId ) );

        if ( roomType.PropertyId != propertyId )
        {
            throw new ArgumentException( "Тип номера не относится к указанному средству размещения.",
                nameof( roomTypeId ) );
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
        CancellationToken ct = default )
    {
        IEnumerable<Reservation> overlaps = await _reservationRepository.GetOverlappingAsync(
            roomTypeId, arrivalDate, departureDate, ct );

        int bookedRooms = overlaps.Count();

        if ( bookedRooms >= totalRoomsCount )
        {
            throw new InvalidOperationException( "На выбранные даты нет доступных номеров данного типа." );
        }
    }
}
