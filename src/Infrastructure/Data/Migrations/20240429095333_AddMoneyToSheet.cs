using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoneyToSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Money_CooperPieces",
                table: "Sheets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Money_ElectrumPieces",
                table: "Sheets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Money_GoldPieces",
                table: "Sheets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Money_PlatinumPieces",
                table: "Sheets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Money_SilverPieces",
                table: "Sheets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money_CooperPieces",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Money_ElectrumPieces",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Money_GoldPieces",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Money_PlatinumPieces",
                table: "Sheets");

            migrationBuilder.DropColumn(
                name: "Money_SilverPieces",
                table: "Sheets");
        }
    }
}
