using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companyusers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companyusers",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    ApplicationUserId1 = table.Column<string>(nullable: true)
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
    }
}
