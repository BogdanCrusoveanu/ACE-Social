using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Licenta.API.Migrations
{
    public partial class SemesterDateTimeOffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartDate",
                table: "Semesters",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndDate",
                table: "Semesters",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Semesters",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset));
        }
    }
}
