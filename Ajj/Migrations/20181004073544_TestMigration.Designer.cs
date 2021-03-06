﻿// <auto-generated />
using System;
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ajj.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181004073544_TestMigration")]
    partial class TestMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Ajj.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email")
                        .HasColumnName("User_Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("Mobile");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("ajjusers");
                });

            modelBuilder.Entity("Ajj.Core.Entities.BusinessStream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryImageUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Name_jp");

                    b.HasKey("Id");

                    b.ToTable("businessstream");
                });

            modelBuilder.Entity("Ajj.Core.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AboutCompany");

                    b.Property<string>("Address");

                    b.Property<string>("ApplicationUserID");

                    b.Property<int?>("BusinessstreamID");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("ContactPerson");

                    b.Property<int>("PostalCodeID");

                    b.Property<string>("WebsiteUrl");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("BusinessstreamID");

                    b.HasIndex("PostalCodeID");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("Ajj.Core.Entities.ContractType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContractName");

                    b.Property<string>("ContractName_JP");

                    b.HasKey("Id");

                    b.ToTable("contractypes");
                });

            modelBuilder.Entity("Ajj.Core.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("Ajj.Core.Entities.JapaneseLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LevelName");

                    b.Property<string>("LevelName_JP");

                    b.HasKey("Id");

                    b.ToTable("japaneselevels");
                });

            modelBuilder.Entity("Ajj.Core.Entities.Job", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessStreamID");

                    b.Property<int>("ClientId");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactPerson");

                    b.Property<int>("ContractTypeId");

                    b.Property<string>("ContractType_Text");

                    b.Property<string>("Email");

                    b.Property<int>("JapaneseLevelId");

                    b.Property<string>("JapaneseLevel_Text");

                    b.Property<int>("JobCategoryId");

                    b.Property<string>("JobTitle");

                    b.Property<string>("JobTitle_JP");

                    b.Property<string>("NeededStaff");

                    b.Property<string>("OtherRequirement");

                    b.Property<DateTime>("PostDate");

                    b.Property<int>("PostalCodeId");

                    b.Property<string>("RepresentativeName");

                    b.Property<string>("Role");

                    b.Property<string>("Salary_Hourly");

                    b.Property<string>("Salary_Monthly");

                    b.Property<bool>("Status");

                    b.Property<string>("TestField")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("Transporationfee");

                    b.Property<string>("TrasportationIncluded")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("WorkLocationAddress");

                    b.Property<string>("WorkingdaysPerweek");

                    b.Property<string>("Workinghour");

                    b.Property<string>("provinceName");

                    b.HasKey("Id");

                    b.HasIndex("BusinessStreamID");

                    b.HasIndex("ClientId");

                    b.HasIndex("ContractTypeId");

                    b.HasIndex("JapaneseLevelId");

                    b.HasIndex("JobCategoryId");

                    b.HasIndex("PostalCodeId");

                    b.ToTable("ajjjob");
                });

            modelBuilder.Entity("Ajj.Core.Entities.JobApply", b =>
                {
                    b.Property<long>("JobID");

                    b.Property<string>("UserID");

                    b.Property<DateTime>("ApplyDate");

                    b.Property<string>("Experience");

                    b.Property<bool>("IsExperience");

                    b.Property<int?>("JobSeekerId");

                    b.Property<string>("JobTitle");

                    b.HasKey("JobID", "UserID");

                    b.HasIndex("JobSeekerId");

                    b.HasIndex("UserID");

                    b.ToTable("jobapplies");
                });

            modelBuilder.Entity("Ajj.Core.Entities.JobCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessStreamId");

                    b.Property<string>("CategoryName");

                    b.Property<string>("CategoryName_JP");

                    b.HasKey("Id");

                    b.HasIndex("BusinessStreamId");

                    b.ToTable("jobcategories");
                });

            modelBuilder.Entity("Ajj.Core.Entities.JobSeeker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Age")
                        .HasColumnName("Birth_Age");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("BirthDay")
                        .HasColumnName("Birth_Day");

                    b.Property<string>("BirthMonth")
                        .HasColumnName("Birth_Month");

                    b.Property<string>("BirthYear")
                        .HasColumnName("Birth_Year");

                    b.Property<string>("City");

                    b.Property<int>("CountryID");

                    b.Property<string>("FirstName")
                        .HasColumnName("First_Name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)))
                        .HasColumnName("Radio_Sex");

                    b.Property<string>("InJapan")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("LastName")
                        .HasColumnName("Last_Name");

                    b.Property<string>("OtherCountry");

                    b.Property<string>("PostalAddrss");

                    b.Property<int>("ProvinceID");

                    b.Property<string>("SubVisaType");

                    b.Property<string>("Town");

                    b.Property<string>("Visa");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProvinceID");

                    b.ToTable("jobseekers");
                });

            modelBuilder.Entity("Ajj.Core.Entities.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<string>("CityName_En");

                    b.Property<string>("Code");

                    b.Property<int>("ProvinceID");

                    b.Property<string>("ProvinceName");

                    b.Property<string>("Town");

                    b.Property<string>("Town_En");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceID");

                    b.ToTable("postalcodes");
                });

            modelBuilder.Entity("Ajj.Core.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("HasJob");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("Name_Jp");

                    b.HasKey("Id");

                    b.ToTable("provinces");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("ajjroles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("ajjroleclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ajjuserclaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("ajjuserlogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("ajjuserroles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("ajjusertokens");
                });

            modelBuilder.Entity("Ajj.Core.Entities.Client", b =>
                {
                    b.HasOne("Ajj.Core.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("Ajj.Core.Entities.BusinessStream", "Businessstream")
                        .WithMany()
                        .HasForeignKey("BusinessstreamID");

                    b.HasOne("Ajj.Core.Entities.PostalCode", "PostalCode")
                        .WithMany()
                        .HasForeignKey("PostalCodeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Core.Entities.Job", b =>
                {
                    b.HasOne("Ajj.Core.Entities.BusinessStream", "BusinessStream")
                        .WithMany()
                        .HasForeignKey("BusinessStreamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.ContractType", "ContractType")
                        .WithMany()
                        .HasForeignKey("ContractTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.JapaneseLevel", "JapaneseLevel")
                        .WithMany()
                        .HasForeignKey("JapaneseLevelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.JobCategory", "JobCategory")
                        .WithMany()
                        .HasForeignKey("JobCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.PostalCode", "PostalCode")
                        .WithMany()
                        .HasForeignKey("PostalCodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Core.Entities.JobApply", b =>
                {
                    b.HasOne("Ajj.Core.Entities.Job", "Job")
                        .WithMany("JobsApply")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.JobSeeker")
                        .WithMany("JobsApply")
                        .HasForeignKey("JobSeekerId");

                    b.HasOne("Ajj.Core.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Core.Entities.JobCategory", b =>
                {
                    b.HasOne("Ajj.Core.Entities.BusinessStream", "businessstream")
                        .WithMany()
                        .HasForeignKey("BusinessStreamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Core.Entities.JobSeeker", b =>
                {
                    b.HasOne("Ajj.Core.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Ajj.Core.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.Province", "Province")
                        .WithMany("Jobseekers")
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Core.Entities.PostalCode", b =>
                {
                    b.HasOne("Ajj.Core.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Ajj.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Ajj.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Ajj.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
