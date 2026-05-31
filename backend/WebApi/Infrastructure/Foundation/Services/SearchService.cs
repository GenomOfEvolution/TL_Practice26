using Domain.Entities;
using Domain.Filters;
using Domain.Repositories;
using Domain.Results;
using Domain.Services;

namespace Infrastructure.Foundation.Services;

public class SearchService : ISearchService
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchService( IUnitOfWork unitOfWork )
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SearchResult>> SearchAsync( SearchFilter filter )
    {
        var results = new List<SearchResult>();

        IEnumerable<Property> properties = await _unitOfWork.Properties.GetByCityAsync( filter.City );

        foreach ( var property in properties )
        {
            var roomTypes = await _unitOfWork.RoomTypes.GetByPropertyIdAsync( property.Id );

            IEnumerable<RoomType> matchingRoomTypes = roomTypes
                .Where( rt => rt.MinPersonCount <= filter.Guests && rt.MaxPersonCount >= filter.Guests );

            if ( filter.MaxPrice.HasValue )
            {
                matchingRoomTypes = matchingRoomTypes.Where( rt => rt.DailyPrice <= filter.MaxPrice.Value );
            }

            foreach ( var roomType in matchingRoomTypes )
            {
                var overlaps = await _unitOfWork.Reservations.GetOverlappingAsync(
                    roomType.Id, filter.ArrivalDate, filter.DepartureDate );

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
