using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndWorkingTime",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartWorkingTime",
                table: "ajjjob",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndWorkingTime",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "StartWorkingTime",
                table: "ajjjob");
        }
    }
}
