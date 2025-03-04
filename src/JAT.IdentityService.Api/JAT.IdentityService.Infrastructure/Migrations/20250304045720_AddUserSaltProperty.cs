using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAT.IdentityService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSaltProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Users",
                type: "BLOB",
                maxLength: 16,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");
        }
    }
}
