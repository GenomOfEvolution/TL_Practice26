using ApiSearchResult = API.DTO.SearchResultDto;
using ApplicationSearchResult = Application.Search.SearchResultDto;
using API.DTO;
using Application.DTO;
using Application.Exceptions;
using Application.Reservations;
using Application.Search;
using Domain.Entities;
using Domain.Enums;

namespace API.Mappers;

public static class MappingExtensions
{
    public static CreatePropertyDto MapToCreatePropertyDto( this CreatePropertyRQ request )
    {
        return new CreatePropertyDto
        {
            Name = request.Name,
            Country = request.Country,
            City = request.City,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
    }

    public static UpdatePropertyDto MapToUpdatePropertyDto( this UpdatePropertyRQ request, int id )
    {
        return new UpdatePropertyDto
        {
            Id = id,
            Name = request.Name,
            Country = request.Country,
            City = request.City,
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
    }

    public static CreateRoomTypeDto MapToCreateRoomTypeDto( this CreateRoomTypeRQ request )
    {
        return new CreateRoomTypeDto
        {
            PropertyId = request.PropertyId,
            Name = request.Name,
            DailyPrice = request.DailyPrice,
            MinPersonCount = request.MinPersonCount,
            MaxPersonCount = request.MaxPersonCount,
            TotalRoomsCount = request.TotalRoomsCount,
            Services = request.Services.ConvertAll( s =>
                Enum.TryParse<ServiceType>( s, out var service )
                    ? service
                    : throw new ApplicationValidationException( $"Недопустимое значение услуги: {s}" ) ),
            Amenities = request.Amenities.ConvertAll( a =>
                Enum.TryParse<AmenitiesType>( a, out var amenity )
                    ? amenity
                    : throw new ApplicationValidationException( $"Недопустимое значение удобства: {a}" ) ),
            Currency = request.Currency,
        };
    }

    public static UpdateRoomTypeDto MapToUpdateRoomTypeDto( this UpdateRoomTypeRQ request, int id )
    {
        return new UpdateRoomTypeDto
        {
            Id = id,
            PropertyId = request.PropertyId,
            Name = request.Name,
            DailyPrice = request.DailyPrice,
            MinPersonCount = request.MinPersonCount,
            MaxPersonCount = request.MaxPersonCount,
            TotalRoomsCount = request.TotalRoomsCount,
            Services = request.Services.Select( s => Enum.Parse<ServiceType>( s ) ).ToList(),
            Amenities = request.Amenities.Select( a => Enum.Parse<AmenitiesType>( a ) ).ToList(),
            Currency = request.Currency,
        };
    }

    public static CreateReservationDto MapToCreateReservationDto( this CreateReservationRQ request )
    {
        return new CreateReservationDto
        {
            PropertyId = request.PropertyId,
            RoomTypeId = request.RoomTypeId,
            ArrivalDate = request.ArrivalDate,
            ArrivalTime = request.ArrivalTime,
            DepartureDate = request.DepartureDate,
            DepartureTime = request.DepartureTime,
            GuestName = request.GuestName,
            GuestPhoneNumber = request.GuestPhoneNumber,
        };
    }

    public static ReservationFilterDto MapToReservationFilterDto( this ReservationFilterRQ request )
    {
        return new ReservationFilterDto
        {
            PropertyId = request.PropertyId,
            GuestName = request.GuestName,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
        };
    }

    public static SearchFilterDto MapToSearchFilterDto( this SearchRQ request )
    {
        return new SearchFilterDto
        {
            City = request.City,
            ArrivalDate = request.ArrivalDate,
            DepartureDate = request.DepartureDate,
            Guests = request.Guests,
            MaxPrice = request.MaxPrice,
        };
    }

    public static PropertyRP MapToPropertyRP( this PropertyDto dto )
    {
        return new PropertyRP
        {
            Id = dto.Id,
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };
    }

    public static RoomTypeRP MapToRoomTypeRP( this RoomTypeDto dto )
    {
        return new RoomTypeRP
        {
            Id = dto.Id,
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            TotalRoomsCount = dto.TotalRoomsCount,
            Services = dto.Services.Select( s => s.ToString() ).ToList(),
            Amenities = dto.Amenities.Select( a => a.ToString() ).ToList(),
            Currency = dto.Currency,
        };
    }

    public static ReservationRP MapToReservationRP( this ReservationDto dto )
    {
        return new ReservationRP
        {
            Id = dto.Id,
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureDate = dto.DepartureDate,
            DepartureTime = dto.DepartureTime,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber,
            Total = dto.Total,
            Currency = dto.Currency,
            IsCanceled = dto.IsCanceled,
        };
    }

    public static ApiSearchResult MapToSearchResultDto( this ApplicationSearchResult result )
    {
        return new ApiSearchResult
        {
            Property = result.Property!.MapToPropertyRP(),
            RoomType = result.RoomType!.MapToRoomTypeRP(),
            RoomsLeft = result.RoomsLeft
        };
    }
}
