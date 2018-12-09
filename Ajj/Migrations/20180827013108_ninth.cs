using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class ninth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 10, 31, 7, 972, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 160, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 10, 31, 7, 974, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 163, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 160, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 27, 10, 31, 7, 972, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 22, 15, 42, 8, 163, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 27, 10, 31, 7, 974, DateTimeKind.Local));
        }
    }
}