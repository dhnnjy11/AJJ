using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class eigth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostalCodeID",
                table: "clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 160, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 21, 12, 14, 48, 837, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 163, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 21, 12, 14, 48, 840, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_clients_PostalCodeID",
                table: "clients",
                column: "PostalCodeID");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_postalcodes_PostalCodeID",
                table: "clients",
                column: "PostalCodeID",
                principalTable: "postalcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_postalcodes_PostalCodeID",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_PostalCodeID",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "PostalCodeID",
                table: "clients");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 21, 12, 14, 48, 837, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 160, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 21, 12, 14, 48, 840, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 163, DateTimeKind.Local));
        }
    }
}