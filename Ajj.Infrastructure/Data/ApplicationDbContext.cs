using Ajj.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ajj.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<JobApply> jobapplies { get; set; }
        public DbSet<Province> provinces { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<PostalCode> postalcodes { get; set; }
        public DbSet<JobSeeker> jobseekers { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<JobCategory> jobcategories { get; set; }
        public DbSet<JapaneseLevel> japaneselevels { get; set; }
        public DbSet<ContractType> contractypes { get; set; }
        public DbSet<JobSkill> jobskills { get; set; }
        public DbSet<VisaCategory> visacategories { get; set; }
        public DbSet<VisaJobMap> visajobmap { get; set; }
        public DbSet<CompanyImage> companyimages { get; set; }
        public DbSet<BusinessStream> businessstream { get; set; }
        public DbSet<CompanyUser> companyusers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<JobApply>()
                .HasKey(c => new { c.JobID, c.UserID });

            builder.Entity<VisaJobMap>()
                .HasKey(c => new { c.BusinessStreamId, c.VisaCategoryId });

            builder.Entity<CompanyUser>()
                .HasKey(c => new { c.ApplicationUserId, c.ClientId });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "ajjusers");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "ajjroles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("ajjuserroles");
                //in case you chagned the TKey type
                //entity.HasKey(key => new { key.UserId, key.RoleId });
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("ajjuserclaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("ajjuserlogins");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("ajjusertokens");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("ajjroleclaims");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
            });


            builder.Entity<Job>(entity =>
            {
                entity.ToTable("ajjjob");
                //in case you chagned the TKey type
                //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
            });
            //builder.Entity<Job>()
            //    .Property(x => x.PostDate)
            //    .HasDefaultValueSql("getdate()");
        }
    }
}