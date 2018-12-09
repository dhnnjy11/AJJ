using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalAddrss",
                table: "clients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceID",
                table: "clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_clients_ProvinceID",
                table: "clients",
                column: "ProvinceID");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_provinces_ProvinceID",
                table: "clients",
                column: "ProvinceID",
                principalTable: "provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_provinces_ProvinceID",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_ProvinceID",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "PostalAddrss",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                table: "clients");
        }
    }
}