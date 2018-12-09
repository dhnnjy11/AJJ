﻿// <auto-generated />
using Ajj.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Ajj.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180830061813_Twelth")]
    partial class Twelth
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Ajj.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2018, 8, 30, 15, 18, 13, 285, DateTimeKind.Local));

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

            modelBuilder.Entity("Ajj.Models.businessstream", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("businessstream");
                });

            modelBuilder.Entity("Ajj.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AboutCompany");

                    b.Property<string>("Address");

                    b.Property<string>("ApplicationUserID");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactEmail");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("ContactPerson");

                    b.Property<int>("PostalCodeID");

                    b.Property<string>("WebsiteUrl");

                    b.Property<int?>("businessstreamID");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserID");

                    b.HasIndex("PostalCodeID");

                    b.HasIndex("businessstreamID");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("Ajj.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("Ajj.Models.Job", b =>
                {
                    b.Property<long>("job_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BusinessContent");

                    b.Property<int>("BusinessStreamID");

                    b.Property<string>("Category");

                    b.Property<int>("ClientId");

                    b.Property<int>("CompanyId");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ContactDepartment");

                    b.Property<string>("ContactPerson");

                    b.Property<string>("ContractType");

                    b.Property<string>("Email");

                    b.Property<string>("Frequencyofwork");

                    b.Property<string>("HPURL");

                    b.Property<string>("HQAddress");

                    b.Property<string>("HQFax");

                    b.Property<string>("HQTel");

                    b.Property<string>("JapaneseLevel");

                    b.Property<string>("JobTitle");

                    b.Property<long>("Job_type_id");

                    b.Property<string>("NeededStaff");

                    b.Property<DateTime>("PostDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2018, 8, 30, 15, 18, 13, 287, DateTimeKind.Local));

                    b.Property<string>("RepresentativeName");

                    b.Property<string>("Role");

                    b.Property<string>("Salary");

                    b.Property<bool>("Status");

                    b.Property<string>("Transporationfee");

                    b.Property<string>("UniqueId");

                    b.Property<string>("WorkLocationAddress");

                    b.Property<string>("WorkingdaysPerweek");

                    b.Property<string>("Workinghour");

                    b.Property<string>("WorkinghourPerday");

                    b.Property<string>("provinceName");

                    b.HasKey("job_id");

                    b.HasIndex("BusinessStreamID");

                    b.HasIndex("ClientId");

                    b.ToTable("ajjjob");
                });

            modelBuilder.Entity("Ajj.Models.JobApply", b =>
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

            modelBuilder.Entity("Ajj.Models.JobSeeker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Age")
                        .HasColumnName("Birth_Age");

                    b.Property<string>("ApplicationId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("BirthDay")
                        .HasColumnName("Birth_Day");

                    b.Property<string>("BirthMonth")
                        .HasColumnName("Birth_Month");

                    b.Property<string>("BirthYear")
                        .HasColumnName("Birth_Year");

                    b.Property<string>("City");

                    b.Property<int>("CountryID");

                    b.Property<string>("DateofBirth");

                    b.Property<string>("FirstName")
                        .HasColumnName("First_Name");

                    b.Property<char>("Gender")
                        .HasColumnName("Radio_Sex");

                    b.Property<char>("InJapan");

                    b.Property<string>("LastName")
                        .HasColumnName("Last_Name");

                    b.Property<string>("Nationality");

                    b.Property<string>("OtherVisaType");

                    b.Property<string>("PostalAddrss");

                    b.Property<string>("Povince");

                    b.Property<int>("ProvinceID");

                    b.Property<string>("Visa");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CountryID");

                    b.HasIndex("ProvinceID");

                    b.ToTable("jobseekers");
                });

            modelBuilder.Entity("Ajj.Models.PostalCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<string>("Code");

                    b.Property<int>("ProvinceID");

                    b.Property<string>("ProvinceName");

                    b.Property<string>("Town");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceID");

                    b.ToTable("postalcodes");
                });

            modelBuilder.Entity("Ajj.Models.Province", b =>
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

            modelBuilder.Entity("Ajj.Models.Client", b =>
                {
                    b.HasOne("Ajj.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserID");

                    b.HasOne("Ajj.Models.PostalCode", "PostalCode")
                        .WithMany()
                        .HasForeignKey("PostalCodeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Models.businessstream", "businessstream")
                        .WithMany()
                        .HasForeignKey("businessstreamID");
                });

            modelBuilder.Entity("Ajj.Models.Job", b =>
                {
                    b.HasOne("Ajj.Models.businessstream", "BusinessStream")
                        .WithMany()
                        .HasForeignKey("BusinessStreamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Models.JobApply", b =>
                {
                    b.HasOne("Ajj.Models.Job", "Job")
                        .WithMany("JobsApply")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Models.JobSeeker")
                        .WithMany("JobsApply")
                        .HasForeignKey("JobSeekerId");

                    b.HasOne("Ajj.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Models.JobSeeker", b =>
                {
                    b.HasOne("Ajj.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Ajj.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ajj.Models.Province", "Province")
                        .WithMany("Jobseekers")
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ajj.Models.PostalCode", b =>
                {
                    b.HasOne("Ajj.Models.Province", "Province")
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
                    b.HasOne("Ajj.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Ajj.Models.ApplicationUser")
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

                    b.HasOne("Ajj.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Ajj.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
