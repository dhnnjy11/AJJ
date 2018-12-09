using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class twentyeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepresentativeName",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "TestField",
                table: "ajjjob");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepresentativeName",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestField",
                table: "ajjjob",
                nullable: false,
                defaultValue: "");
        }
    }
}
