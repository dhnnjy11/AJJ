using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class twentytwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessContent",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "ContactDepartment",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "Frequencyofwork",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "HPURL",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "HQAddress",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "HQFax",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "HQTel",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "Job_type_id",
                table: "ajjjob");

            migrationBuilder.RenameColumn(
                name: "WorkinghourPerday",
                table: "ajjjob",
                newName: "JobTitle_JP");

            migrationBuilder.RenameColumn(
                name: "UniqueId",
                table: "ajjjob",
                newName: "ContractType_JP");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "ajjjob",
                newName: "PostalCodeId");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName_JP",
                table: "jobcategories",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 25, 10, 13, 20, 729, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 20, 19, 24, 6, 499, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ajjjob_PostalCodeId",
                table: "ajjjob",
                column: "PostalCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ajjjob_postalcodes_PostalCodeId",
                table: "ajjjob",
                column: "PostalCodeId",
                principalTable: "postalcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ajjjob_postalcodes_PostalCodeId",
                table: "ajjjob");

            migrationBuilder.DropIndex(
                name: "IX_ajjjob_PostalCodeId",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "CategoryName_JP",
                table: "jobcategories");

            migrationBuilder.RenameColumn(
                name: "PostalCodeId",
                table: "ajjjob",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "JobTitle_JP",
                table: "ajjjob",
                newName: "WorkinghourPerday");

            migrationBuilder.RenameColumn(
                name: "ContractType_JP",
                table: "ajjjob",
                newName: "UniqueId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 20, 19, 24, 6, 499, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 25, 10, 13, 20, 729, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "BusinessContent",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactDepartment",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Frequencyofwork",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HPURL",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HQAddress",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HQFax",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HQTel",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Job_type_id",
                table: "ajjjob",
                nullable: false,
                defaultValue: 0L);
        }
    }
}