using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SnakeCaseNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Property_PropertyId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_RoomType_RoomTypeId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RoomTypeId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "Services",
                table: "RoomType",
                newName: "services");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoomType",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "RoomType",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "Amenities",
                table: "RoomType",
                newName: "amenities");

            migrationBuilder.RenameColumn(
                name: "TotalRoomsCount",
                table: "RoomType",
                newName: "total_rooms_count");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "RoomType",
                newName: "property_id");

            migrationBuilder.RenameColumn(
                name: "MinPersonCount",
                table: "RoomType",
                newName: "min_person_count");

            migrationBuilder.RenameColumn(
                name: "MaxPersonCount",
                table: "RoomType",
                newName: "max_person_count");

            migrationBuilder.RenameColumn(
                name: "DailyPrice",
                table: "RoomType",
                newName: "daily_price");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoomType",
                newName: "id_roomtype");

            migrationBuilder.RenameIndex(
                name: "IX_RoomType_PropertyId",
                table: "RoomType",
                newName: "ix_room_type_property_id");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Reservation",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Reservation",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "RoomTypeId",
                table: "Reservation",
                newName: "room_type_id");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                table: "Reservation",
                newName: "property_id");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "Reservation",
                newName: "is_canceled");

            migrationBuilder.RenameColumn(
                name: "GuestPhoneNumber",
                table: "Reservation",
                newName: "guest_phone_number");

            migrationBuilder.RenameColumn(
                name: "GuestName",
                table: "Reservation",
                newName: "guest_name");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Reservation",
                newName: "departure_time");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Reservation",
                newName: "departure_date");

            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "Reservation",
                newName: "arrival_time");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Reservation",
                newName: "arrival_date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservation",
                newName: "id_reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PropertyId",
                table: "Reservation",
                newName: "ix_reservation_property_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Property",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Property",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Property",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Property",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Property",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Property",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Property",
                newName: "id_property");

            migrationBuilder.AlterColumn<bool>(
                name: "is_canceled",
                table: "Reservation",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_room_type",
                table: "RoomType",
                column: "id_roomtype");

            migrationBuilder.AddPrimaryKey(
                name: "pk_reservation",
                table: "Reservation",
                column: "id_reservation");

            migrationBuilder.AddPrimaryKey(
                name: "pk_property",
                table: "Property",
                column: "id_property");

            migrationBuilder.CreateIndex(
                name: "IX_roomtype_id_property",
                table: "Reservation",
                columns: new[] { "room_type_id", "property_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_reservation_property_property_id",
                table: "Reservation",
                column: "property_id",
                principalTable: "Property",
                principalColumn: "id_property",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_reservation_room_type_room_type_id",
                table: "Reservation",
                column: "room_type_id",
                principalTable: "RoomType",
                principalColumn: "id_roomtype",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_room_type_property_property_id",
                table: "RoomType",
                column: "property_id",
                principalTable: "Property",
                principalColumn: "id_property",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_reservation_property_property_id",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "fk_reservation_room_type_room_type_id",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "fk_room_type_property_property_id",
                table: "RoomType");

            migrationBuilder.DropPrimaryKey(
                name: "pk_room_type",
                table: "RoomType");

            migrationBuilder.DropPrimaryKey(
                name: "pk_reservation",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_roomtype_id_property",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "pk_property",
                table: "Property");

            migrationBuilder.RenameColumn(
                name: "services",
                table: "RoomType",
                newName: "Services");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "RoomType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "RoomType",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "amenities",
                table: "RoomType",
                newName: "Amenities");

            migrationBuilder.RenameColumn(
                name: "total_rooms_count",
                table: "RoomType",
                newName: "TotalRoomsCount");

            migrationBuilder.RenameColumn(
                name: "property_id",
                table: "RoomType",
                newName: "PropertyId");

            migrationBuilder.RenameColumn(
                name: "min_person_count",
                table: "RoomType",
                newName: "MinPersonCount");

            migrationBuilder.RenameColumn(
                name: "max_person_count",
                table: "RoomType",
                newName: "MaxPersonCount");

            migrationBuilder.RenameColumn(
                name: "daily_price",
                table: "RoomType",
                newName: "DailyPrice");

            migrationBuilder.RenameColumn(
                name: "id_roomtype",
                table: "RoomType",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_room_type_property_id",
                table: "RoomType",
                newName: "IX_RoomType_PropertyId");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "Reservation",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "Reservation",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "room_type_id",
                table: "Reservation",
                newName: "RoomTypeId");

            migrationBuilder.RenameColumn(
                name: "property_id",
                table: "Reservation",
                newName: "PropertyId");

            migrationBuilder.RenameColumn(
                name: "is_canceled",
                table: "Reservation",
                newName: "IsCanceled");

            migrationBuilder.RenameColumn(
                name: "guest_phone_number",
                table: "Reservation",
                newName: "GuestPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "guest_name",
                table: "Reservation",
                newName: "GuestName");

            migrationBuilder.RenameColumn(
                name: "departure_time",
                table: "Reservation",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "departure_date",
                table: "Reservation",
                newName: "DepartureDate");

            migrationBuilder.RenameColumn(
                name: "arrival_time",
                table: "Reservation",
                newName: "ArrivalTime");

            migrationBuilder.RenameColumn(
                name: "arrival_date",
                table: "Reservation",
                newName: "ArrivalDate");

            migrationBuilder.RenameColumn(
                name: "id_reservation",
                table: "Reservation",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "ix_reservation_property_id",
                table: "Reservation",
                newName: "IX_Reservation_PropertyId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Property",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Property",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Property",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "Property",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Property",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Property",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "id_property",
                table: "Property",
                newName: "Id");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCanceled",
                table: "Reservation",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RoomTypeId",
                table: "Reservation",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Property_PropertyId",
                table: "Reservation",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_RoomType_RoomTypeId",
                table: "Reservation",
                column: "RoomTypeId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
