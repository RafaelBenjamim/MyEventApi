using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEventApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentProviderToStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfinitePayHandle",
                table: "Stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PagBankAppId",
                table: "Stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PagBankAppKey",
                table: "Stores",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentProvider",
                table: "Stores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalPaymentId",
                table: "Registrations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfinitePayHandle",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "PagBankAppId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "PagBankAppKey",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "PaymentProvider",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ExternalPaymentId",
                table: "Registrations");
        }
    }
}
