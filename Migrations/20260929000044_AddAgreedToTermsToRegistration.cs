using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEventApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAgreedToTermsToRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AgreedToTerms",
                table: "Registrations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreedToTerms",
                table: "Registrations");
        }
    }
}
