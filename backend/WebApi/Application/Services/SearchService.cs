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
    private readonly IUnitOfWork _unitOfWork;

    public SearchService(
        IPropertyRepository propertyRepository,
        IRoomTypeRepository roomTypeRepository,
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork )
    {
        _propertyRepository = propertyRepository;
        _roomTypeRepository = roomTypeRepository;
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<SearchResultDto>> SearchAsync( SearchFilterDto filter, CancellationToken ct )
    {
        var results = new List<SearchResultDto>();

        IReadOnlyList<Property> properties = ( await _propertyRepository.GetByCityAsync( filter.City, ct ) ).ToList();

        foreach ( Property? property in properties )
        {
            PropertyDto propertyDto = EntityToPropertyDtoMapper.Map( property );

            IReadOnlyList<RoomType> roomTypes = ( await _roomTypeRepository.GetByPropertyIdAsync( property.Id, ct ) ).ToList();

            IEnumerable<RoomType> matchingRoomTypes = roomTypes
                .Where( rt => rt.MinPersonCount <= filter.Guests && rt.MaxPersonCount >= filter.Guests );

            if ( filter.MaxPrice.HasValue )
            {
                matchingRoomTypes = matchingRoomTypes.Where( rt => rt.DailyPrice <= filter.MaxPrice.Value );
            }

            foreach ( RoomType? roomType in matchingRoomTypes )
            {
                IEnumerable<Reservation> overlaps = await _reservationRepository.GetOverlappingAsync(
                    roomType.Id, filter.ArrivalDate, filter.DepartureDate, ct );

                int roomsLeft = roomType.TotalRoomsCount - overlaps.Count();

                if ( roomsLeft > 0 )
                {
                    RoomTypeDto roomTypeDto = EntityToRoomTypeDtoMapper.Map( roomType );
                    results.Add( new SearchResultDto( propertyDto, roomTypeDto, roomsLeft ) );
                }
            }
        }

        return results;
    }
}
