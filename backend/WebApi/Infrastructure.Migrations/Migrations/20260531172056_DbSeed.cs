using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "Address", "City", "Country", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, "ул. Тверская, 10", "Москва", "Россия", 55.759999999999998, 37.619999999999997, "Grand Plaza Hotel" },
                    { 2, "ул. Приморская, 25", "Сочи", "Россия", 43.590000000000003, 39.729999999999997, "Seaside Resort" },
                    { 3, "Альпийская ул., 5", "Красная Поляна", "Россия", 43.68, 40.200000000000003, "Mountain Lodge" },
                    { 4, "Невский пр., 50", "Санкт-Петербург", "Россия", 59.93, 30.32, "City Inn" }
                });

            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "Id", "Amenities", "Currency", "DailyPrice", "MaxPersonCount", "MinPersonCount", "Name", "PropertyId", "Services", "TotalRoomsCount" },
                values: new object[,]
                {
                    { 1, "AirConditioning,TV,Hairdryer", 0, 5000m, 2, 1, "Standard", 1, "WiFi,Breakfast", 20 },
                    { 2, "AirConditioning,TV,MiniBar,Bathtub", 0, 8000m, 3, 1, "Deluxe", 1, "WiFi,Breakfast,Gym,Spa", 10 },
                    { 3, "AirConditioning,TV,MiniBar,Safe,Balcony,Bathtub,CoffeeMaker", 0, 15000m, 4, 1, "Suite", 1, "WiFi,Breakfast,Gym,Spa,RoomService,Concierge", 5 },
                    { 4, "AirConditioning,TV,Hairdryer", 0, 3000m, 2, 1, "Standard", 2, "WiFi,Breakfast,Parking", 30 },
                    { 5, "AirConditioning,TV,MiniBar,Balcony", 0, 5000m, 4, 2, "Family Room", 2, "WiFi,Breakfast,Parking,Laundry", 15 },
                    { 6, "AirConditioning,TV,Hairdryer", 0, 4000m, 2, 1, "Standard", 3, "WiFi,Breakfast,Parking", 10 },
                    { 7, "AirConditioning,TV,MiniBar,Balcony,Bathtub,CoffeeMaker", 0, 12000m, 6, 1, "Chalet", 3, "WiFi,Breakfast,Parking", 5 },
                    { 8, "AirConditioning,TV,Hairdryer", 0, 2500m, 2, 1, "Standard", 4, "WiFi,Breakfast", 15 },
                    { 9, "AirConditioning,TV,MiniBar,CoffeeMaker", 0, 4000m, 2, 1, "Business Room", 4, "WiFi,Breakfast,Laundry,Gym", 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
