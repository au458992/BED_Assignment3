using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morgenmadsbuffeten.Data.Migrations
{
    public partial class OrderAndFromToAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "RoomBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTo",
                table: "RoomBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AdultsOrdered",
                table: "BreakfastBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenOrdered",
                table: "BreakfastBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "DateTo",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "AdultsOrdered",
                table: "BreakfastBookings");

            migrationBuilder.DropColumn(
                name: "ChildrenOrdered",
                table: "BreakfastBookings");
        }
    }
}
