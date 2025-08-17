using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domic.Persistence.Migrations.C
{
    /// <inheritdoc />
    public partial class AddOwnerFieldToAccountEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
