using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "ajjusers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ajjusers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ajjusers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "ajjusers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "ajjusers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "ajjusers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "companyusers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ApplicationUserId1 = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companyusers", x => new { x.ApplicationUserId, x.ClientId });
                    table.ForeignKey(
                        name: "FK_companyusers_ajjusers_ApplicationUserId1",
                        column: x => x.ApplicationUserId1,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_companyusers_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companyusers_ApplicationUserId1",
                table: "companyusers",
                column: "ApplicationUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_companyusers_ClientId",
                table: "companyusers",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companyusers");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "ajjusers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ajjusers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ajjusers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "ajjusers");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "ajjusers");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "ajjusers");
        }
    }
}
