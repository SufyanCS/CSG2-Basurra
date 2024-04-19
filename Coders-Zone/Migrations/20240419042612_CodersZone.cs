using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coders_Zone.Migrations
{
    public partial class CodersZone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonId",
                table: "Lessons",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Lessons_LessonId",
                table: "Lessons",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Lessons_LessonId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_LessonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Courses");
        }
    }
}
