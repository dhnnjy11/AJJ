using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class thirtythree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "visasubcategories");

            migrationBuilder.AddColumn<string>(
                name: "AllowStatus",
                table: "visacategories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "visacategories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "visacategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisaCategoryId",
                table: "jobseekers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_jobseekers_VisaCategoryId",
                table: "jobseekers",
                column: "VisaCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_jobseekers_visacategories_VisaCategoryId",
                table: "jobseekers",
                column: "VisaCategoryId",
                principalTable: "visacategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobseekers_visacategories_VisaCategoryId",
                table: "jobseekers");

            migrationBuilder.DropIndex(
                name: "IX_jobseekers_VisaCategoryId",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "AllowStatus",
                table: "visacategories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "visacategories");

            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "visacategories");

            migrationBuilder.DropColumn(
                name: "VisaCategoryId",
                table: "jobseekers");

            migrationBuilder.CreateTable(
                name: "visasubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    VisaCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visasubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visasubcategories_visacategories_VisaCategoryId",
                        column: x => x.VisaCategoryId,
                        principalTable: "visacategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_visasubcategories_VisaCategoryId",
                table: "visasubcategories",
                column: "VisaCategoryId");
        }
    }
}
