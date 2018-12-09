using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class thirtytwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenderRequired",
                table: "ajjjob",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxAge",
                table: "ajjjob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "ajjjob",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "visacategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visacategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "visajobmap",
                columns: table => new
                {
                    BusinessStreamId = table.Column<int>(nullable: false),
                    VisaCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visajobmap", x => new { x.BusinessStreamId, x.VisaCategoryId });
                    table.ForeignKey(
                        name: "FK_visajobmap_businessstream_BusinessStreamId",
                        column: x => x.BusinessStreamId,
                        principalTable: "businessstream",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_visajobmap_visacategories_VisaCategoryId",
                        column: x => x.VisaCategoryId,
                        principalTable: "visacategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_visajobmap_VisaCategoryId",
                table: "visajobmap",
                column: "VisaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_visasubcategories_VisaCategoryId",
                table: "visasubcategories",
                column: "VisaCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "visajobmap");

            migrationBuilder.DropTable(
                name: "visasubcategories");

            migrationBuilder.DropTable(
                name: "visacategories");

            migrationBuilder.DropColumn(
                name: "GenderRequired",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "MaxAge",
                table: "ajjjob");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "ajjjob");
        }
    }
}
