
using Ajj.Core.Configuration;
using Ajj.Core.Entities;
using Ajj.Core.Interface;
using Ajj.Core.Interface.Repository;
using Ajj.Core.Services;
using Ajj.Infrastructure.Data;
using Ajj.Infrastructure.Repository;
using Ajj.Infrastructure.Services;
using Ajj.Interface;
using Ajj.Service;
using Ajj.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTIC.Service.Email;
using ReflectionIT.Mvc.Paging;

namespace Ajj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                x =>
                {
                    x.MigrationsHistoryTable("__efmigrationshistory");
                    x.MigrationsAssembly("Ajj");
                }

                //don't use schema name hard-coded
                ));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //   .AddEntityFrameworkStores<ApplicationDbContext>()
            //   .AddDefaultTokenProviders();
            services.AddScoped<IPasswordHasher<ApplicationUser>, CustomPasswordHasher<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = "968736596645060";
            //    facebookOptions.AppSecret = "613739d09f56a3df7ab46d6b37b9f0f3";
           
            //});

            // must be added before AddIdentity()
            // services.AddScoped<IPasswordHasher<ApplicationUser>, BCryptPasswordHasher<ApplicationUser>>();
            services.AddSingleton<IUnityOfWork, UnityOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IJobApplyRepository, JobApplyRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IPostalCodeRepository, PostalCodeRepository>();
            services.AddTransient<IJobSeekerRepository, JobSeekerRepository>();
            services.AddTransient<IEmailSender, AuthEmailSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IBusinessStreamRepository, BusinessStreamRepository>();            
            services.AddTransient<IJobCategoryRepository, JobCategoryRepository>();
            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<IJobSkillRepository, JobSkillRepository>();
            services.AddTransient<IJobSeekerService, JobSeekerServices>();
            services.AddTransient<IRepository<CompanyImage>, Repository<CompanyImage>>();
            //services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IAPICallingService, APICallingService>();
            services.AddTransient<IMaketoAPICallingService, MarketoAPICallingService>();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton(Configuration.GetSection("GBAPISettings").Get<GBAPISettings>());
            services.AddSingleton(Configuration.GetSection("MarketoApiSettings").Get<MarketoApiSettings>());
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAutoFillService, AutoFillService>();

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddMvc()
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Account/Manage");
                options.Conventions.AuthorizePage("/Account/Logout");
            });
           

            //services.Configure<ForwardedHeadersOptions>(options =>
            //{
            //    options.ForwardedHeaders =
            //        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            //});

            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                app.UseDatabaseErrorPage();
                // app.UseStatusCodePagesWithReExecute("/Error/Error/{0}")
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseExceptionHandler("/Error");
                //  app.UseStatusCodePagesWithReExecute("/Error");
            }

            
            //  

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "adminareas",
                  template: "{area:exists}/{controller=Client}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
            app.UseStaticFiles();

            //IdentityDataIntializer.SeedRoles(roleManager);
        }
    }
}