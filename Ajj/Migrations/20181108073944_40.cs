using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRegistered",
                table: "clients",
                newName: "IsActive");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "clients",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "clients",
                newName: "IsRegistered");
        }
    }
}
