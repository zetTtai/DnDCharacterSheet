using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDCharacterSheet.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSheetAbilityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "SheetAbility");

            // Ensure all values are valid integers or set to default
            migrationBuilder.Sql(
                "UPDATE \"SheetAbility\" SET \"Value\" = '0' WHERE \"Value\" IS NULL OR \"Value\" NOT SIMILAR TO '\\d+';");

            // Change the column type with explicit casting
            migrationBuilder.Sql(
                "ALTER TABLE \"SheetAbility\" ALTER COLUMN \"Value\" TYPE integer USING \"Value\"::integer;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "SheetAbility",
                type: "text",
                nullable: false,
                defaultValue: "");

            // Reverse the casting when rolling back
            migrationBuilder.Sql(
                "ALTER TABLE \"SheetAbility\" ALTER COLUMN \"Value\" TYPE text USING \"Value\"::text;");
        }
    }
}
