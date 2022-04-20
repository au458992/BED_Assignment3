using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morgenmadsbuffeten.Data.Migrations
{
    public partial class RoomBookingSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomBookings",
                columns: new[] { "RoomNumber", "Adults", "Children", "DateFrom", "DateTo" },
                values: new object[] { 1L, 2, 3, new DateTime(2022, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoomBookings",
                keyColumn: "RoomNumber",
                keyValue: 1L);
        }
    }
}
