using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class seventeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 115, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 7, 17, 21, 50, 826, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 117, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 7, 17, 21, 50, 828, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "jobcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusinessStreamId = table.Column<int>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_jobcategories_businessstream_BusinessStreamId",
                        column: x => x.BusinessStreamId,
                        principalTable: "businessstream",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_jobcategories_BusinessStreamId",
                table: "jobcategories",
                column: "BusinessStreamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jobcategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "ajjusers",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 7, 17, 21, 50, 826, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 115, DateTimeKind.Local));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "ajjjob",
                nullable: false,
                defaultValue: new DateTime(2018, 9, 7, 17, 21, 50, 828, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 9, 7, 17, 25, 42, 117, DateTimeKind.Local));
        }
    }
}