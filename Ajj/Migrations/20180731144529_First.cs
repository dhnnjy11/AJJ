using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Ajj.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ajjroles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjroles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ajjusers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    User_Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjusers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "businessstream",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessstream", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HasJob = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Name_Jp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ajjroleclaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjroleclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ajjroleclaims_ajjroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ajjroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ajjuserclaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjuserclaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ajjuserclaims_ajjusers_UserId",
                        column: x => x.UserId,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ajjuserlogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjuserlogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ajjuserlogins_ajjusers_UserId",
                        column: x => x.UserId,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ajjuserroles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjuserroles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ajjuserroles_ajjroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ajjroles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ajjuserroles_ajjusers_UserId",
                        column: x => x.UserId,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ajjusertokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjusertokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ajjusertokens_ajjusers_UserId",
                        column: x => x.UserId,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationId = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    businessstreamID = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    EstablishedDate = table.Column<string>(nullable: true),
                    ProfileDesc = table.Column<string>(nullable: true),
                    WebsiteUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clients_ajjusers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_clients_businessstream_businessstreamID",
                        column: x => x.businessstreamID,
                        principalTable: "businessstream",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jobseekers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Birth_Age = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Birth_Day = table.Column<string>(nullable: true),
                    Birth_Month = table.Column<string>(nullable: true),
                    Birth_Year = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CountryID = table.Column<int>(nullable: false),
                    DateofBirth = table.Column<string>(nullable: true),
                    First_Name = table.Column<string>(nullable: true),
                    Radio_Sex = table.Column<char>(nullable: false),
                    InJapan = table.Column<char>(nullable: false),
                    Last_Name = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    OtherVisaType = table.Column<string>(nullable: true),
                    PostalAddrss = table.Column<string>(nullable: true),
                    Povince = table.Column<string>(nullable: true),
                    ProvinceID = table.Column<int>(nullable: false),
                    Visa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobseekers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_jobseekers_ajjusers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_jobseekers_countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jobseekers_provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "postalcodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    ProvinceID = table.Column<int>(nullable: false),
                    ProvinceName = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postalcodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postalcodes_provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ajjjob",
                columns: table => new
                {
                    job_id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusinessContent = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    ContactDepartment = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContractType = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Frequencyofwork = table.Column<string>(nullable: true),
                    HPURL = table.Column<string>(nullable: true),
                    HQAddress = table.Column<string>(nullable: true),
                    HQFax = table.Column<string>(nullable: true),
                    HQTel = table.Column<string>(nullable: true),
                    Job_type_id = table.Column<long>(nullable: false),
                    NeededStaff = table.Column<string>(nullable: true),
                    PostDate = table.Column<DateTime>(nullable: false),
                    RepresentativeName = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Salary = table.Column<string>(nullable: true),
                    Transporationfee = table.Column<string>(nullable: true),
                    UniqueId = table.Column<string>(nullable: true),
                    WorkLocationAddress = table.Column<string>(nullable: true),
                    WorkingdaysPerweek = table.Column<string>(nullable: true),
                    Workinghour = table.Column<string>(nullable: true),
                    WorkinghourPerday = table.Column<string>(nullable: true),
                    provinceName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajjjob", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_ajjjob_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jobapplies",
                columns: table => new
                {
                    JobID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    ApplyDate = table.Column<DateTime>(nullable: false),
                    Experience = table.Column<string>(nullable: true),
                    IsExperience = table.Column<bool>(nullable: false),
                    JobSeekerId = table.Column<int>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobapplies", x => new { x.JobID, x.UserID });
                    table.ForeignKey(
                        name: "FK_jobapplies_ajjjob_JobID",
                        column: x => x.JobID,
                        principalTable: "ajjjob",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jobapplies_jobseekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "jobseekers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_jobapplies_ajjusers_UserID",
                        column: x => x.UserID,
                        principalTable: "ajjusers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ajjjob_ClientId",
                table: "ajjjob",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ajjroleclaims_RoleId",
                table: "ajjroleclaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ajjroles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ajjuserclaims_UserId",
                table: "ajjuserclaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ajjuserlogins_UserId",
                table: "ajjuserlogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ajjuserroles_RoleId",
                table: "ajjuserroles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "ajjusers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "ajjusers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_ApplicationUserId",
                table: "clients",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_clients_businessstreamID",
                table: "clients",
                column: "businessstreamID");

            migrationBuilder.CreateIndex(
                name: "IX_jobapplies_JobSeekerId",
                table: "jobapplies",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_jobapplies_UserID",
                table: "jobapplies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_jobseekers_ApplicationUserId",
                table: "jobseekers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_jobseekers_CountryID",
                table: "jobseekers",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_jobseekers_ProvinceID",
                table: "jobseekers",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_postalcodes_ProvinceID",
                table: "postalcodes",
                column: "ProvinceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ajjroleclaims");

            migrationBuilder.DropTable(
                name: "ajjuserclaims");

            migrationBuilder.DropTable(
                name: "ajjuserlogins");

            migrationBuilder.DropTable(
                name: "ajjuserroles");

            migrationBuilder.DropTable(
                name: "ajjusertokens");

            migrationBuilder.DropTable(
                name: "jobapplies");

            migrationBuilder.DropTable(
                name: "postalcodes");

            migrationBuilder.DropTable(
                name: "ajjroles");

            migrationBuilder.DropTable(
                name: "ajjjob");

            migrationBuilder.DropTable(
                name: "jobseekers");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "ajjusers");

            migrationBuilder.DropTable(
                name: "businessstream");
        }
    }
}