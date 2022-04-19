using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morgenmadsbuffeten.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomBookings",
                columns: table => new
                {
                    RoomNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomBookings", x => x.RoomNumber);
                });

            migrationBuilder.CreateTable(
                name: "BreakfastBookings",
                columns: table => new
                {
                    BreakfastBookingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    RoomBookingRoomNumber = table.Column<long>(type: "bigint", nullable: false),
                    Room = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakfastBookings", x => x.BreakfastBookingId);
                    table.ForeignKey(
                        name: "FK_BreakfastBookings_RoomBookings_RoomBookingRoomNumber",
                        column: x => x.RoomBookingRoomNumber,
                        principalTable: "RoomBookings",
                        principalColumn: "RoomNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakfastBookings_RoomBookingRoomNumber",
                table: "BreakfastBookings",
                column: "RoomBookingRoomNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakfastBookings");

            migrationBuilder.DropTable(
                name: "RoomBookings");
        }
    }
}
