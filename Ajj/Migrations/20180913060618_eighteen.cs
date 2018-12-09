using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class eighteen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 398, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 115, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 401, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 117, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 115, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 398, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 117, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 401, DateTimeKind.Local));
        }
    }
}