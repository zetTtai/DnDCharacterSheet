using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMoneyColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Money_SilverPieces",
                table: "Sheets",
                newName: "SilverPieces");

            migrationBuilder.RenameColumn(
                name: "Money_PlatinumPieces",
                table: "Sheets",
                newName: "PlatinumPieces");

            migrationBuilder.RenameColumn(
                name: "Money_GoldPieces",
                table: "Sheets",
                newName: "GoldPieces");

            migrationBuilder.RenameColumn(
                name: "Money_ElectrumPieces",
                table: "Sheets",
                newName: "ElectrumPieces");

            migrationBuilder.RenameColumn(
                name: "Money_CooperPieces",
                table: "Sheets",
                newName: "CooperPieces");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SilverPieces",
                table: "Sheets",
                newName: "Money_SilverPieces");

            migrationBuilder.RenameColumn(
                name: "PlatinumPieces",
                table: "Sheets",
                newName: "Money_PlatinumPieces");

            migrationBuilder.RenameColumn(
                name: "GoldPieces",
                table: "Sheets",
                newName: "Money_GoldPieces");

            migrationBuilder.RenameColumn(
                name: "ElectrumPieces",
                table: "Sheets",
                newName: "Money_ElectrumPieces");

            migrationBuilder.RenameColumn(
                name: "CooperPieces",
                table: "Sheets",
                newName: "Money_CooperPieces");
        }
    }
}
