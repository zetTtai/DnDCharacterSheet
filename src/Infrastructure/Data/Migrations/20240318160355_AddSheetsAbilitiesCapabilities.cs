using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class AddSheetsAbilitiesCapabilities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.CreateTable(
            name: "Abilities",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Abilities", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Sheets",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CharacterName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Sheets", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Capabilities",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AbilityId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Capabilities", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_Capabilities_Abilities_AbilityId",
                    column: x => x.AbilityId,
                    principalTable: "Abilities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "SheetAbility",
            columns: table => new
            {
                SheetId = table.Column<int>(type: "int", nullable: false),
                AbilityId = table.Column<int>(type: "int", nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Modifier = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_SheetAbility", x => new { x.SheetId, x.AbilityId });
                _ = table.ForeignKey(
                    name: "FK_SheetAbility_Abilities_AbilityId",
                    column: x => x.AbilityId,
                    principalTable: "Abilities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_SheetAbility_Sheets_SheetId",
                    column: x => x.SheetId,
                    principalTable: "Sheets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "SheetSavingThrow",
            columns: table => new
            {
                SheetId = table.Column<int>(type: "int", nullable: false),
                CapabilityId = table.Column<int>(type: "int", nullable: false),
                Modifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Proficiency = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_SheetSavingThrow", x => new { x.SheetId, x.CapabilityId });
                _ = table.ForeignKey(
                    name: "FK_SheetSavingThrow_Capabilities_SheetId",
                    column: x => x.SheetId,
                    principalTable: "Capabilities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_SheetSavingThrow_Sheets_SheetId",
                    column: x => x.SheetId,
                    principalTable: "Sheets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "SheetSkill",
            columns: table => new
            {
                SheetId = table.Column<int>(type: "int", nullable: false),
                CapabilityId = table.Column<int>(type: "int", nullable: false),
                Modifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Proficiency = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_SheetSkill", x => new { x.SheetId, x.CapabilityId });
                _ = table.ForeignKey(
                    name: "FK_SheetSkill_Capabilities_SheetId",
                    column: x => x.SheetId,
                    principalTable: "Capabilities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_SheetSkill_Sheets_SheetId",
                    column: x => x.SheetId,
                    principalTable: "Sheets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateIndex(
            name: "IX_Capabilities_AbilityId",
            table: "Capabilities",
            column: "AbilityId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_SheetAbility_AbilityId",
            table: "SheetAbility",
            column: "AbilityId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropTable(
            name: "SheetAbility");

        _ = migrationBuilder.DropTable(
            name: "SheetSavingThrow");

        _ = migrationBuilder.DropTable(
            name: "SheetSkill");

        _ = migrationBuilder.DropTable(
            name: "Capabilities");

        _ = migrationBuilder.DropTable(
            name: "Sheets");

        _ = migrationBuilder.DropTable(
            name: "Abilities");
    }
}
