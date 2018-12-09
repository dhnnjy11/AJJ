using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class eleven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 15, 20, 18, 25, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 27, 10, 41, 11, 185, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 15, 20, 18, 27, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 27, 10, 41, 11, 188, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "BusinessStreamID",
                table: "ajjjob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JapaneseLevel",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ajjjob_BusinessStreamID",
                table: "ajjjob",
                column: "BusinessStreamID");

            migrationBuilder.AddForeignKey(
                name: "FK_ajjjob_businessstream_BusinessStreamID",
                table: "ajjjob",
                column: "BusinessStreamID",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ajjjob_businessstream_BusinessStreamID",
                table: "ajjjob");

            migrationBuilder.DropIndex(
                name: "IX_ajjjob_BusinessStreamID",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "BusinessStreamID",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "JapaneseLevel",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "ajjjob");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 10, 41, 11, 185, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 27, 15, 20, 18, 25, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 8, 27, 10, 41, 11, 188, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 8, 27, 15, 20, 18, 27, DateTimeKind.Local));
        }
    }
}