using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IndexAndPkNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "property_id",
                table: "RoomType",
                newName: "id_property");

            migrationBuilder.RenameIndex(
                name: "ix_room_type_property_id",
                table: "RoomType",
                newName: "IX_RoomType_PropertyId");

            migrationBuilder.RenameColumn(
                name: "room_type_id",
                table: "Reservation",
                newName: "id_roomtype");

            migrationBuilder.RenameColumn(
                name: "property_id",
                table: "Reservation",
                newName: "id_property");

            migrationBuilder.RenameIndex(
                name: "IX_roomtype_id_property",
                table: "Reservation",
                newName: "IX_Reservation_RoomTypeId_PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_property",
                table: "RoomType",
                newName: "property_id");

            migrationBuilder.RenameIndex(
                name: "IX_RoomType_PropertyId",
                table: "RoomType",
                newName: "ix_room_type_property_id");

            migrationBuilder.RenameColumn(
                name: "id_roomtype",
                table: "Reservation",
                newName: "room_type_id");

            migrationBuilder.RenameColumn(
                name: "id_property",
                table: "Reservation",
                newName: "property_id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_RoomTypeId_PropertyId",
                table: "Reservation",
                newName: "IX_roomtype_id_property");
        }
    }
}
