using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.API.Migrations
{
    public partial class ModifyLaboratorySeminar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Seminars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Laboratories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Seminars");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Laboratories");
        }
    }
}
