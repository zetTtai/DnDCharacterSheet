using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class FixSheetCapabilityConfiguration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropForeignKey(
            name: "FK_SheetSavingThrow_Capabilities_SheetId",
            table: "SheetSavingThrow");

        _ = migrationBuilder.DropForeignKey(
            name: "FK_SheetSkill_Capabilities_SheetId",
            table: "SheetSkill");

        _ = migrationBuilder.CreateIndex(
            name: "IX_SheetSkill_CapabilityId",
            table: "SheetSkill",
            column: "CapabilityId");

        _ = migrationBuilder.CreateIndex(
            name: "IX_SheetSavingThrow_CapabilityId",
            table: "SheetSavingThrow",
            column: "CapabilityId");

        _ = migrationBuilder.AddForeignKey(
            name: "FK_SheetSavingThrow_Capabilities_CapabilityId",
            table: "SheetSavingThrow",
            column: "CapabilityId",
            principalTable: "Capabilities",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        _ = migrationBuilder.AddForeignKey(
            name: "FK_SheetSkill_Capabilities_CapabilityId",
            table: "SheetSkill",
            column: "CapabilityId",
            principalTable: "Capabilities",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropForeignKey(
            name: "FK_SheetSavingThrow_Capabilities_CapabilityId",
            table: "SheetSavingThrow");

        _ = migrationBuilder.DropForeignKey(
            name: "FK_SheetSkill_Capabilities_CapabilityId",
            table: "SheetSkill");

        _ = migrationBuilder.DropIndex(
            name: "IX_SheetSkill_CapabilityId",
            table: "SheetSkill");

        _ = migrationBuilder.DropIndex(
            name: "IX_SheetSavingThrow_CapabilityId",
            table: "SheetSavingThrow");

        _ = migrationBuilder.AddForeignKey(
            name: "FK_SheetSavingThrow_Capabilities_SheetId",
            table: "SheetSavingThrow",
            column: "SheetId",
            principalTable: "Capabilities",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        _ = migrationBuilder.AddForeignKey(
            name: "FK_SheetSkill_Capabilities_SheetId",
            table: "SheetSkill",
            column: "SheetId",
            principalTable: "Capabilities",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
