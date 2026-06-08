using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Domain.Results;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class SearchService : ISearchService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SearchService( IPropertyRepository propertyRepository, IRoomTypeRepository roomTypeRepository, IReservationRepository reservationRepository, IUnitOfWork unitOfWork )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SearchResult>> SearchAsync( SearchFilter filter, CancellationToken ct = default )
    {
        var results = new List<SearchResult>();

        IEnumerable<Property> properties = await _propertyRepository.GetByCityAsync( filter.City, ct );

        foreach ( var property in properties )
        {
            var roomTypes = await _roomTypeRepository.GetByPropertyIdAsync( property.Id, ct );

            IEnumerable<RoomType> matchingRoomTypes = roomTypes
                .Where( rt => rt.MinPersonCount <= filter.Guests && rt.MaxPersonCount >= filter.Guests );

            if ( filter.MaxPrice.HasValue )
            {
                matchingRoomTypes = matchingRoomTypes.Where( rt => rt.DailyPrice <= filter.MaxPrice.Value );
            }

            foreach ( var roomType in matchingRoomTypes )
            {
                var overlaps = await _reservationRepository.GetOverlappingAsync(
                    roomType.Id, filter.ArrivalDate, filter.DepartureDate, ct );

                int roomsLeft = roomType.TotalRoomsCount - overlaps.Count();

                if ( roomsLeft > 0 )
                {
                    results.Add( new SearchResult( property, roomType, roomsLeft ) );
                }
            }
        }

        return results;
    }
}
