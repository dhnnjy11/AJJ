using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_ajjusers_ApplicationUserId",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "clients",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_clients_ApplicationUserId",
                table: "clients",
                newName: "IX_clients_ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_ajjusers_ApplicationUserID",
                table: "clients",
                column: "ApplicationUserID",
                principalTable: "ajjusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_ajjusers_ApplicationUserID",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "clients",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_clients_ApplicationUserID",
                table: "clients",
                newName: "IX_clients_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "clients",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_clients_ajjusers_ApplicationUserId",
                table: "clients",
                column: "ApplicationUserId",
                principalTable: "ajjusers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}