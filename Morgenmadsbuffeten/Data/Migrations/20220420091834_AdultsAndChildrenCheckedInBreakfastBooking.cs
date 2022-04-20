using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Morgenmadsbuffeten.Data.Migrations
{
    public partial class AdultsAndChildrenCheckedInBreakfastBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedIn",
                table: "BreakfastBookings");

            migrationBuilder.AddColumn<int>(
                name: "AdultsCheckedIn",
                table: "BreakfastBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildrenCheckedIn",
                table: "BreakfastBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultsCheckedIn",
                table: "BreakfastBookings");

            migrationBuilder.DropColumn(
                name: "ChildrenCheckedIn",
                table: "BreakfastBookings");

            migrationBuilder.AddColumn<bool>(
                name: "CheckedIn",
                table: "BreakfastBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
