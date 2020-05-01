using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.API.Migrations
{
    public partial class LaboratorySeminarCourseFKS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Seminars_CourseId",
                table: "Seminars",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_CourseId",
                table: "Laboratories",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laboratories_Courses_CourseId",
                table: "Laboratories",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Seminars_Courses_CourseId",
                table: "Seminars",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laboratories_Courses_CourseId",
                table: "Laboratories");

            migrationBuilder.DropForeignKey(
                name: "FK_Seminars_Courses_CourseId",
                table: "Seminars");

            migrationBuilder.DropIndex(
                name: "IX_Seminars_CourseId",
                table: "Seminars");

            migrationBuilder.DropIndex(
                name: "IX_Laboratories_CourseId",
                table: "Laboratories");
        }
    }
}
