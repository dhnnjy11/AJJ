using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
               name: "AllowStatus",
               table: "visacategories",
               newName: "AllowJobStatus");

            //migrationBuilder.DropColumn(
            //    name: "AllowStatus",
            //    table: "visacategories");

            //migrationBuilder.AddColumn<string>(
            //    name: "AllowJobStatus",
            //    table: "visacategories",
            //    nullable: false,
            //    defaultValue: "");



            migrationBuilder.AddColumn<string>(
                name: "NeedPermission",
                table: "visacategories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPermitToWork",
                table: "jobseekers",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.RenameColumn(
            //  name: "IsAllowedToWork",
            //  table: "jobseekers",
            //  newName: "IsPermitToWork");


            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "jobseekers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowJobStatus",
                table: "visacategories");

            migrationBuilder.DropColumn(
                name: "NeedPermission",
                table: "visacategories");

            migrationBuilder.DropColumn(
                name: "IsPermitToWork",
                table: "jobseekers");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "jobseekers");

            migrationBuilder.AddColumn<string>(
                name: "AllowStatus",
                table: "visacategories",
                nullable: false,
                defaultValue: "");
        }
    }
}
