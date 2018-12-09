using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _38 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "clients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "clients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HasJob",
                table: "businessstream",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "HasJob",
                table: "businessstream");
        }
    }
}
