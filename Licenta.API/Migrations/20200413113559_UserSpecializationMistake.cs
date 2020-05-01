using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.API.Migrations
{
    public partial class UserSpecializationMistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecializations_Specializations_DivisionId",
                table: "UserSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_UserSpecializations_DivisionId",
                table: "UserSpecializations");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "UserSpecializations");

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "UserSpecializations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations",
                columns: new[] { "UserId", "SpecializationId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecializations_SpecializationId",
                table: "UserSpecializations",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecializations_Specializations_SpecializationId",
                table: "UserSpecializations",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecializations_Specializations_SpecializationId",
                table: "UserSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_UserSpecializations_SpecializationId",
                table: "UserSpecializations");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "UserSpecializations");

            migrationBuilder.AddColumn<int>(
                name: "DivisionId",
                table: "UserSpecializations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecializations",
                table: "UserSpecializations",
                columns: new[] { "UserId", "DivisionId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecializations_DivisionId",
                table: "UserSpecializations",
                column: "DivisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecializations_Specializations_DivisionId",
                table: "UserSpecializations",
                column: "DivisionId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
