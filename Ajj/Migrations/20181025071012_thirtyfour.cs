using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class thirtyfour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "visacategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgreed",
                table: "jobseekers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WorkingTime",
                table: "ajjjob",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "visacategories");

            migrationBuilder.DropColumn(
                name: "IsAgreed",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "WorkingTime",
                table: "ajjjob");
        }
    }
}
