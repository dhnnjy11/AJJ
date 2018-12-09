using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ajj.Migrations
{
    public partial class twentynine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "jobseekers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "JobSkill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SkillName = table.Column<string>(nullable: true),
                    BusinessStreamId = table.Column<int>(nullable: false),
                    JobCategoryId = table.Column<int>(nullable: false),
                    JobSeekerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSkill_businessstream_BusinessStreamId",
                        column: x => x.BusinessStreamId,
                        principalTable: "businessstream",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkill_jobcategories_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "jobcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkill_jobseekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_BusinessStreamId",
                table: "JobSkill",
                column: "BusinessStreamId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_JobCategoryId",
                table: "JobSkill",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkill_JobSeekerId",
                table: "JobSkill",
                column: "JobSeekerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSkill");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "jobseekers");
        }
    }
}
