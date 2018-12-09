using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class ThirtyOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobskills_jobcategories_JobCategoryId",
                table: "jobskills");

            migrationBuilder.DropIndex(
                name: "IX_jobskills_JobCategoryId",
                table: "jobskills");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "jobskills");

            migrationBuilder.AddColumn<int>(
                name: "InitialLoginCount",
                table: "jobseekers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialLoginCount",
                table: "jobseekers");

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "jobskills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_jobskills_JobCategoryId",
                table: "jobskills",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_jobskills_jobcategories_JobCategoryId",
                table: "jobskills",
                column: "JobCategoryId",
                principalTable: "jobcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
