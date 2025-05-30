using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingReservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationaddedcancel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenCancelled",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenCancelled",
                table: "Reservations");
        }
    }
}
