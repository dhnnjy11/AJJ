using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class twentyfive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 25, 15, 16, 58, 506, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 25, 12, 48, 52, 300, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "ajjjob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ajjjob_JobCategoryId",
                table: "ajjjob",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ajjjob_jobcategories_JobCategoryId",
                table: "ajjjob",
                column: "JobCategoryId",
                principalTable: "jobcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ajjjob_jobcategories_JobCategoryId",
                table: "ajjjob");

            migrationBuilder.DropIndex(
                name: "IX_ajjjob_JobCategoryId",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "ajjjob");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 25, 12, 48, 52, 300, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 25, 15, 16, 58, 506, DateTimeKind.Local));
        }
    }
}