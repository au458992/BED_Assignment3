using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morgenmadsbuffeten.Data.Migrations
{
    public partial class CustomFKAddedToBreakfastBookingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakfastBookings_RoomBookings_RoomBookingRoomNumber",
                table: "BreakfastBookings");

            migrationBuilder.DropIndex(
                name: "IX_BreakfastBookings_RoomBookingRoomNumber",
                table: "BreakfastBookings");

            migrationBuilder.DropColumn(
                name: "RoomBookingRoomNumber",
                table: "BreakfastBookings");

            migrationBuilder.CreateIndex(
                name: "IX_BreakfastBookings_Room",
                table: "BreakfastBookings",
                column: "Room");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakfastBookings_RoomBookings_Room",
                table: "BreakfastBookings",
                column: "Room",
                principalTable: "RoomBookings",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BreakfastBookings_RoomBookings_Room",
                table: "BreakfastBookings");

            migrationBuilder.DropIndex(
                name: "IX_BreakfastBookings_Room",
                table: "BreakfastBookings");

            migrationBuilder.AddColumn<long>(
                name: "RoomBookingRoomNumber",
                table: "BreakfastBookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BreakfastBookings_RoomBookingRoomNumber",
                table: "BreakfastBookings",
                column: "RoomBookingRoomNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_BreakfastBookings_RoomBookings_RoomBookingRoomNumber",
                table: "BreakfastBookings",
                column: "RoomBookingRoomNumber",
                principalTable: "RoomBookings",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
