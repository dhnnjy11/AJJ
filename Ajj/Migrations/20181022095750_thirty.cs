using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class thirty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_businessstream_BusinessStreamId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_jobcategories_JobCategoryId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_jobseekers_JobSeekerId",
                table: "JobSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobSkill",
                table: "JobSkill");

            migrationBuilder.RenameTable(
                name: "JobSkill",
                newName: "jobskills");

            migrationBuilder.RenameIndex(
                name: "IX_JobSkill_JobSeekerId",
                table: "jobskills",
                newName: "IX_jobskills_JobSeekerId");

            migrationBuilder.RenameIndex(
                name: "IX_JobSkill_JobCategoryId",
                table: "jobskills",
                newName: "IX_jobskills_JobCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_JobSkill_BusinessStreamId",
                table: "jobskills",
                newName: "IX_jobskills_BusinessStreamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_jobskills",
                table: "jobskills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_jobskills_businessstream_BusinessStreamId",
                table: "jobskills",
                column: "BusinessStreamId",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jobskills_jobcategories_JobCategoryId",
                table: "jobskills",
                column: "JobCategoryId",
                principalTable: "jobcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jobskills_jobseekers_JobSeekerId",
                table: "jobskills",
                column: "JobSeekerId",
                principalTable: "jobseekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jobskills_businessstream_BusinessStreamId",
                table: "jobskills");

            migrationBuilder.DropForeignKey(
                name: "FK_jobskills_jobcategories_JobCategoryId",
                table: "jobskills");

            migrationBuilder.DropForeignKey(
                name: "FK_jobskills_jobseekers_JobSeekerId",
                table: "jobskills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_jobskills",
                table: "jobskills");

            migrationBuilder.RenameTable(
                name: "jobskills",
                newName: "JobSkill");

            migrationBuilder.RenameIndex(
                name: "IX_jobskills_JobSeekerId",
                table: "JobSkill",
                newName: "IX_JobSkill_JobSeekerId");

            migrationBuilder.RenameIndex(
                name: "IX_jobskills_JobCategoryId",
                table: "JobSkill",
                newName: "IX_JobSkill_JobCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_jobskills_BusinessStreamId",
                table: "JobSkill",
                newName: "IX_JobSkill_BusinessStreamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobSkill",
                table: "JobSkill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_businessstream_BusinessStreamId",
                table: "JobSkill",
                column: "BusinessStreamId",
                principalTable: "businessstream",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_jobcategories_JobCategoryId",
                table: "JobSkill",
                column: "JobCategoryId",
                principalTable: "jobcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_jobseekers_JobSeekerId",
                table: "JobSkill",
                column: "JobSeekerId",
                principalTable: "jobseekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
