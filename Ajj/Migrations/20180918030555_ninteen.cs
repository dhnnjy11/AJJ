using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class ninteen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_businessstream_BusinessstreamID",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessstreamID",
                table: "clients",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 921, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 398, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 923, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 401, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_clients_businessstream_BusinessstreamID",
                table: "clients",
                column: "BusinessstreamID",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_businessstream_BusinessstreamID",
                table: "clients");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessstreamID",
                table: "clients",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 398, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 921, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 13, 15, 6, 17, 401, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 18, 12, 5, 54, 923, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_clients_businessstream_BusinessstreamID",
                table: "clients",
                column: "BusinessstreamID",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}