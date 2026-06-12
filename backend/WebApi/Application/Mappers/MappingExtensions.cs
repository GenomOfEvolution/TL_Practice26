using Application.DTO;
using Application.Reservations;
using Domain.Entities;

namespace Application.Mappers;

public static class MappingExtensions
{
    public static Property MapToPropertyEntity( this CreatePropertyDto dto )
    {
        return new Property
        {
            Name = dto.Name,
            Country = dto.Country,
            City = dto.City,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };
    }

    public static RoomType MapToRoomTypeEntity( this CreateRoomTypeDto dto )
    {
        return new RoomType
        {
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            TotalRoomsCount = dto.TotalRoomsCount,
            Services = dto.Services,
            Amenities = dto.Amenities,
            Currency = dto.Currency,
        };
    }

    public static Reservation MapToReservationEntity( this CreateReservationDto dto )
    {
        return new Reservation
        {
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureDate = dto.DepartureDate,
            DepartureTime = dto.DepartureTime,
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber,
        };
    }

    public static PropertyDto MapToPropertyDto( this Property entity )
    {
        return new PropertyDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Country = entity.Country,
            City = entity.City,
            Address = entity.Address,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
        };
    }

    public static RoomTypeDto MapToRoomTypeDto( this RoomType entity )
    {
        return new RoomTypeDto
        {
            Id = entity.Id,
            PropertyId = entity.PropertyId,
            Name = entity.Name,
            DailyPrice = entity.DailyPrice,
            MinPersonCount = entity.MinPersonCount,
            MaxPersonCount = entity.MaxPersonCount,
            TotalRoomsCount = entity.TotalRoomsCount,
            Services = entity.Services,
            Amenities = entity.Amenities,
            Currency = entity.Currency,
        };
    }

    public static ReservationDto MapToReservationDto( this Reservation entity )
    {
        return new ReservationDto
        {
            Id = entity.Id,
            PropertyId = entity.PropertyId,
            RoomTypeId = entity.RoomTypeId,
            ArrivalDate = entity.ArrivalDate,
            ArrivalTime = entity.ArrivalTime,
            DepartureDate = entity.DepartureDate,
            DepartureTime = entity.DepartureTime,
            GuestName = entity.GuestName,
            GuestPhoneNumber = entity.GuestPhoneNumber,
            Total = entity.Total,
            Currency = entity.Currency,
            IsCanceled = entity.IsCanceled,
        };
    }
}
