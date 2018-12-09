using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishedDate",
                table: "clients");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "ajjjob",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ajjjob");

            migrationBuilder.AddColumn<string>(
                name: "EstablishedDate",
                table: "clients",
                nullable: true);
        }
    }
}