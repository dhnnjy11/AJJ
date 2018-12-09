using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class twentySix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Salary_Monthly",
            //    table: "ajjjob",
            //    nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 25, 15, 16, 58, 506, DateTimeKind.Local));

            //migrationBuilder.AddColumn<string>(
            //    name: "Salary_Hourly",
            //    table: "ajjjob",
            //    nullable: true);

            //migrationBuilder.AddColumn<char>(
            //    name: "TrasportationIncluded",
            //    table: "ajjjob",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Salary_Hourly",
            //    table: "ajjjob");

            //migrationBuilder.DropColumn(
            //    name: "TrasportationIncluded",
            //    table: "ajjjob");

            //migrationBuilder.DropColumn(
            //    name: "Salary_Monthly",
            //    table: "ajjjob");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 25, 15, 16, 58, 506, DateTimeKind.Local),
                oldClrType: typeof(DateTime));
        }
    }
}