using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTypoCopperPieces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CooperPieces",
                table: "Sheets",
                newName: "CopperPieces");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CopperPieces",
                table: "Sheets",
                newName: "CooperPieces");
        }
    }
}
