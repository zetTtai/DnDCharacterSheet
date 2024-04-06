using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserToSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sheets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sheets");
        }
    }
}
