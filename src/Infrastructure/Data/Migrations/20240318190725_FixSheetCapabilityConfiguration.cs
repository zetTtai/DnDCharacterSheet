using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSheetCapabilityConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SheetSavingThrow_Capabilities_SheetId",
                table: "SheetSavingThrow");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetSkill_Capabilities_SheetId",
                table: "SheetSkill");

            migrationBuilder.CreateIndex(
                name: "IX_SheetSkill_CapabilityId",
                table: "SheetSkill",
                column: "CapabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetSavingThrow_CapabilityId",
                table: "SheetSavingThrow",
                column: "CapabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetSavingThrow_Capabilities_CapabilityId",
                table: "SheetSavingThrow",
                column: "CapabilityId",
                principalTable: "Capabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
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
            migrationBuilder.DropForeignKey(
                name: "FK_SheetSavingThrow_Capabilities_CapabilityId",
                table: "SheetSavingThrow");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetSkill_Capabilities_CapabilityId",
                table: "SheetSkill");

            migrationBuilder.DropIndex(
                name: "IX_SheetSkill_CapabilityId",
                table: "SheetSkill");

            migrationBuilder.DropIndex(
                name: "IX_SheetSavingThrow_CapabilityId",
                table: "SheetSavingThrow");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetSavingThrow_Capabilities_SheetId",
                table: "SheetSavingThrow",
                column: "SheetId",
                principalTable: "Capabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheetSkill_Capabilities_SheetId",
                table: "SheetSkill",
                column: "SheetId",
                principalTable: "Capabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
