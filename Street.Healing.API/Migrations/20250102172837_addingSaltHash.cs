using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Street.Healing.API.Migrations
{
    /// <inheritdoc />
    public partial class addingSaltHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "SaltPassword");

            migrationBuilder.AddColumn<string>(
                name: "HashPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SaltPassword",
                table: "Users",
                newName: "Password");
        }
    }
}
