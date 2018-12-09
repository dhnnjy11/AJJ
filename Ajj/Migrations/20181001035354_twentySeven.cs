using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class twentySeven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "CityName_En",
            //    table: "postalcodes",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Town_En",
            //    table: "postalcodes",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "CategoryImageUrl",
            //    table: "businessstream",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName_En",
                table: "postalcodes");

            migrationBuilder.DropColumn(
                name: "Town_En",
                table: "postalcodes");

            migrationBuilder.DropColumn(
                name: "CategoryImageUrl",
                table: "businessstream");
        }
    }
}