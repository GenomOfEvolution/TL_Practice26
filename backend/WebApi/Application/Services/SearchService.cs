using Application.DTO;
using Application.Interfaces;
using Application.Mappers;
using Application.Search;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services;

public class SearchService : ISearchService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;

    public SearchService(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
    }

    public async Task<IReadOnlyList<SearchResultDto>> SearchAsync( SearchFilterDto filter, CancellationToken ct )
    {
        var results = new List<SearchResultDto>();

        IReadOnlyList<Property> properties = string.IsNullOrWhiteSpace( filter.City )
            ? await _propertyRepository.GetAllAsync( ct )
            : await _propertyRepository.GetByCityAsync( filter.City, ct );

        foreach ( Property property in properties )
        {
            PropertyDto propertyDto = property.MapToPropertyDto();

            IEnumerable<RoomType> roomTypes = await _roomTypeRepository.GetByPropertyIdAsync( property.Id, ct );

            IEnumerable<RoomType> matchingRoomTypes = roomTypes;

            if ( filter.Guests.HasValue )
            {
                matchingRoomTypes = matchingRoomTypes
                    .Where( rt => rt.MinPersonCount <= filter.Guests.Value && rt.MaxPersonCount >= filter.Guests.Value );
            }

            if ( filter.MaxPrice.HasValue )
            {
                matchingRoomTypes = matchingRoomTypes.Where( rt => rt.DailyPrice <= filter.MaxPrice.Value );
            }

            DateOnly? arrivalDate = filter.ArrivalDate;
            DateOnly? departureDate = filter.DepartureDate;

            foreach ( RoomType roomType in matchingRoomTypes )
            {
                if ( arrivalDate.HasValue && departureDate.HasValue )
                {
                    IEnumerable<Reservation> overlaps = await _reservationRepository.GetOverlappingAsync(
                        roomType.Id, arrivalDate.Value, departureDate.Value, ct );

                    int roomsLeft = roomType.TotalRoomsCount - overlaps.Count();
                    if ( roomsLeft > 0 )
                    {
                        results.Add( new SearchResultDto( propertyDto, roomType.MapToRoomTypeDto(), roomsLeft ) );
                    }
                }
                else
                {
                    results.Add( new SearchResultDto( propertyDto, roomType.MapToRoomTypeDto(), roomType.TotalRoomsCount ) );
                }
            }
        }

        return results;
    }
}
