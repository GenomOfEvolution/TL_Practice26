using Domain.Entities;

namespace Infrastructure.Foundation.DbSeeds;

public static class PropertyDbSeed
{
    public static List<Property> GetData()
    {
        return
        [
            new Property
            {
                Id = 1,
                Name = "Grand Plaza Hotel",
                Country = "Россия",
                City = "Москва",
                Address = "ул. Тверская, 10",
                Latitude = 55.76,
                Longitude = 37.62
            },
            new Property
            {
                Id = 2,
                Name = "Seaside Resort",
                Country = "Россия",
                City = "Сочи",
                Address = "ул. Приморская, 25",
                Latitude = 43.59,
                Longitude = 39.73
            },
            new Property
            {
                Id = 3,
                Name = "Mountain Lodge",
                Country = "Россия",
                City = "Красная Поляна",
                Address = "Альпийская ул., 5",
                Latitude = 43.68,
                Longitude = 40.20
            },
            new Property
            {
                Id = 4,
                Name = "City Inn",
                Country = "Россия",
                City = "Санкт-Петербург",
                Address = "Невский пр., 50",
                Latitude = 59.93,
                Longitude = 30.32
            }
        ];
    }
}
