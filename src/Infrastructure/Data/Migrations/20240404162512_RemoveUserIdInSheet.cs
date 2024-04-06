using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdInSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sheets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sheets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
