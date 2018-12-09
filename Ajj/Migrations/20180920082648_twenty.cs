using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class twenty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Povince",
                table: "jobseekers",
                newName: "Town");

            migrationBuilder.RenameColumn(
                name: "OtherVisaType",
                table: "jobseekers",
                newName: "SubVisaType");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "jobseekers",
                newName: "OtherCountry");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 921, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 923, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Town",
                table: "jobseekers",
                newName: "Povince");

            migrationBuilder.RenameColumn(
                name: "SubVisaType",
                table: "jobseekers",
                newName: "OtherVisaType");

            migrationBuilder.RenameColumn(
                name: "OtherCountry",
                table: "jobseekers",
                newName: "Nationality");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 921, DateTimeKind.Local),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 923, DateTimeKind.Local),
                oldClrType: typeof(DateTime));
        }
    }
}