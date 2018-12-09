using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EducationStatus",
                table: "jobseekers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JapaneseLevel",
                table: "jobseekers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JlptExam",
                table: "jobseekers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaExpiryDay",
                table: "jobseekers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaExpiryMonth",
                table: "jobseekers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VisaExpiryYear",
                table: "jobseekers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationStatus",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "JapaneseLevel",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "JlptExam",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "VisaExpiryDay",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "VisaExpiryMonth",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "VisaExpiryYear",
                table: "jobseekers");
        }
    }
}
