using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertEnumListsToInts : Migration
    {
        private const string UpdateServices = @"
UPDATE ""RoomType""
SET ""services"" = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
    REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
        ""services"",
    'Breakfast', '0'), 'WiFi', '1'), 'Parking', '2'), 'AirportTransfer', '3'), 'Laundry', '4'),
    'Gym', '5'), 'Spa', '6'), 'RoomService', '7'), 'Concierge', '8'), 'PetFriendly', '9'),
    'breakfast', '0'), 'wifi', '1'), 'parking', '2'), 'airporttransfer', '3'), 'laundry', '4'),
    'gym', '5'), 'spa', '6'), 'roomservice', '7'), 'concierge', '8'), 'petfriendly', '9')
";

        private const string UpdateAmenities = @"
UPDATE ""RoomType""
SET ""amenities"" = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
    REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
        ""amenities"",
    'AirConditioning', '0'), 'TV', '1'), 'MiniBar', '2'), 'Safe', '3'), 'Balcony', '4'),
    'Bathtub', '5'), 'CoffeeMaker', '6'), 'Hairdryer', '7'),
    'airconditioning', '0'), 'tv', '1'), 'minibar', '2'), 'safe', '3'), 'balcony', '4'),
    'bathtub', '5'), 'coffeemaker', '6'), 'hairdryer', '7')
";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql( UpdateServices );
            migrationBuilder.Sql( UpdateAmenities );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            const string revertServices = @"
UPDATE ""RoomType""
SET ""services"" = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
    REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
        ""services"",
    '0', 'Breakfast'), '1', 'WiFi'), '2', 'Parking'), '3', 'AirportTransfer'), '4', 'Laundry'),
    '5', 'Gym'), '6', 'Spa'), '7', 'RoomService'), '8', 'Concierge'), '9', 'PetFriendly'),
    '0', 'breakfast'), '1', 'wifi'), '2', 'parking'), '3', 'airporttransfer'), '4', 'laundry'),
    '5', 'gym'), '6', 'spa'), '7', 'roomservice'), '8', 'concierge'), '9', 'petfriendly')
";

            const string revertAmenities = @"
UPDATE ""RoomType""
SET ""amenities"" = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
    REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
        ""amenities"",
    '0', 'AirConditioning'), '1', 'TV'), '2', 'MiniBar'), '3', 'Safe'), '4', 'Balcony'),
    '5', 'Bathtub'), '6', 'CoffeeMaker'), '7', 'Hairdryer'),
    '0', 'airconditioning'), '1', 'tv'), '2', 'minibar'), '3', 'safe'), '4', 'balcony'),
    '5', 'bathtub'), '6', 'coffeemaker'), '7', 'hairdryer')
";

            migrationBuilder.Sql( revertServices );
            migrationBuilder.Sql( revertAmenities );
        }
    }
}
