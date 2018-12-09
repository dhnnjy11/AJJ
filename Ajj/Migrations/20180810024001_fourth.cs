using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_businessstream_businessstreamID",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "FK_clients_provinces_ProvinceID",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_ProvinceID",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "PostalAddrss",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "businessstreamID",
                table: "clients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_clients_businessstream_businessstreamID",
                table: "clients",
                column: "businessstreamID",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_businessstream_businessstreamID",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "businessstreamID",
                table: "clients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
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
                name: "FK_clients_businessstream_businessstreamID",
                table: "clients",
                column: "businessstreamID",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_provinces_ProvinceID",
                table: "clients",
                column: "ProvinceID",
                principalTable: "provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}