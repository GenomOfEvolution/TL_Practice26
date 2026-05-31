using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Foundation.DbSeeds;

public static class RoomTypeDbSeed
{
    public static List<RoomType> GetData()
    {
        return
        [
            // Grand Plaza Hotel (PropertyId=1)
            new RoomType
            {
                Id = 1,
                PropertyId = 1,
                Name = "Standard",
                DailyPrice = 5000m,
                MinPersonCount = 1,
                MaxPersonCount = 2,
                TotalRoomsCount = 20,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.Hairdryer
                ],
                Currency = Currency.Rubles
            },
            new RoomType
            {
                Id = 2,
                PropertyId = 1,
                Name = "Deluxe",
                DailyPrice = 8000m,
                MinPersonCount = 1,
                MaxPersonCount = 3,
                TotalRoomsCount = 10,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Gym,
                    ServiceType.Spa
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.MiniBar,
                    AmenitiesType.Bathtub
                ],
                Currency = Currency.Rubles
            },
            new RoomType
            {
                Id = 3,
                PropertyId = 1,
                Name = "Suite",
                DailyPrice = 15000m,
                MinPersonCount = 1,
                MaxPersonCount = 4,
                TotalRoomsCount = 5,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Gym,
                    ServiceType.Spa,
                    ServiceType.RoomService,
                    ServiceType.Concierge
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.MiniBar,
                    AmenitiesType.Safe,
                    AmenitiesType.Balcony,
                    AmenitiesType.Bathtub,
                    AmenitiesType.CoffeeMaker
                ],
                Currency = Currency.Rubles
            },
            // Seaside Resort (PropertyId=2)
            new RoomType
            {
                Id = 4,
                PropertyId = 2,
                Name = "Standard",
                DailyPrice = 3000m,
                MinPersonCount = 1,
                MaxPersonCount = 2,
                TotalRoomsCount = 30,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Parking
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.Hairdryer
                ],
                Currency = Currency.Rubles
            },
            new RoomType
            {
                Id = 5,
                PropertyId = 2,
                Name = "Family Room",
                DailyPrice = 5000m,
                MinPersonCount = 2,
                MaxPersonCount = 4,
                TotalRoomsCount = 15,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Parking,
                    ServiceType.Laundry
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.MiniBar,
                    AmenitiesType.Balcony
                ],
                Currency = Currency.Rubles
            },
            // Mountain Lodge (PropertyId=3)
            new RoomType
            {
                Id = 6,
                PropertyId = 3,
                Name = "Standard",
                DailyPrice = 4000m,
                MinPersonCount = 1,
                MaxPersonCount = 2,
                TotalRoomsCount = 10,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Parking
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.Hairdryer
                ],
                Currency = Currency.Rubles
            },
            new RoomType
            {
                Id = 7,
                PropertyId = 3,
                Name = "Chalet",
                DailyPrice = 12000m,
                MinPersonCount = 1,
                MaxPersonCount = 6,
                TotalRoomsCount = 5,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Parking
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.MiniBar,
                    AmenitiesType.Balcony,
                    AmenitiesType.Bathtub,
                    AmenitiesType.CoffeeMaker
                ],
                Currency = Currency.Rubles
            },
            // City Inn (PropertyId=4)
            new RoomType
            {
                Id = 8,
                PropertyId = 4,
                Name = "Standard",
                DailyPrice = 2500m,
                MinPersonCount = 1,
                MaxPersonCount = 2,
                TotalRoomsCount = 15,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.Hairdryer
                ],
                Currency = Currency.Rubles
            },
            new RoomType
            {
                Id = 9,
                PropertyId = 4,
                Name = "Business Room",
                DailyPrice = 4000m,
                MinPersonCount = 1,
                MaxPersonCount = 2,
                TotalRoomsCount = 8,
                Services = [
                    ServiceType.WiFi,
                    ServiceType.Breakfast,
                    ServiceType.Laundry,
                    ServiceType.Gym
                ],
                Amenities = [
                    AmenitiesType.AirConditioning,
                    AmenitiesType.TV,
                    AmenitiesType.MiniBar,
                    AmenitiesType.CoffeeMaker
                ],
                Currency = Currency.Rubles
            }
        ];
    }
}
