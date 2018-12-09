using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class twentythree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 25, 12, 48, 52, 300, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 25, 10, 13, 20, 729, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_ajjjob_contractypes_ContractTypeId",
                table: "ajjjob",
                column: "ContractTypeId",
                principalTable: "contractypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ajjjob_japaneselevels_JapaneseLevelId",
                table: "ajjjob",
                column: "JapaneseLevelId",
                principalTable: "japaneselevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ajjjob_contractypes_ContractTypeId",
                table: "ajjjob");

            migrationBuilder.DropForeignKey(
                name: "FK_ajjjob_japaneselevels_JapaneseLevelId",
                table: "ajjjob");

            migrationBuilder.DropTable(
                name: "contractypes");

            migrationBuilder.DropTable(
                name: "japaneselevels");

            migrationBuilder.DropIndex(
                name: "IX_ajjjob_ContractTypeId",
                table: "ajjjob");

            migrationBuilder.DropIndex(
                name: "IX_ajjjob_JapaneseLevelId",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "ContractTypeId",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "JapaneseLevelId",
                table: "ajjjob");

            migrationBuilder.RenameColumn(
                name: "JapaneseLevel_Text",
                table: "ajjjob",
                newName: "JapaneseLevel");

            migrationBuilder.RenameColumn(
                name: "ContractType_Text",
                table: "ajjjob",
                newName: "ContractType_JP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 25, 10, 13, 20, 729, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 25, 12, 48, 52, 300, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "ContractType",
                table: "ajjjob",
                nullable: true);
        }
    }
}