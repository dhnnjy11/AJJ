using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequiredAge",
                table: "ajjjob",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredAge",
                table: "ajjjob");
        }
    }
}
