using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.API.Migrations
{
    public partial class GroupsNewFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubGroups_GroupId",
                table: "SubGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_SpecializationId",
                table: "Groups",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Specializations_SpecializationId",
                table: "Groups",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubGroups_Groups_GroupId",
                table: "SubGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Specializations_SpecializationId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_SubGroups_Groups_GroupId",
                table: "SubGroups");

            migrationBuilder.DropIndex(
                name: "IX_SubGroups_GroupId",
                table: "SubGroups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_SpecializationId",
                table: "Groups");
        }
    }
}
