using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEventApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Registrations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "Registrations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "HasDiscount",
                table: "Registrations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Events",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "HasDiscount",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Events");
        }
    }
}
