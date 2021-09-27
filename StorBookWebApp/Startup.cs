using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StorBookWebApp.Data;
using StorBookWebApp.DTOs.ImageDTOs;
using StorBookWebApp.Extensions;
using StorBookWebApp.Models;
using System;

namespace StorBookWebApp
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

            services.AddControllersWithViews();

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlite(Configuration.GetConnectionString("DefaultConnString"));
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                //string connStr = string.Empty;

                if (env == "Development")
                {
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnString"));
                }
                else
                {
                    var connStr = GetHerokuConnectionString();
                  
                    options.UseNpgsql(connStr);
                }
            });


            // Configure Caching
            services.AddSession();
            services.ConfigureHttpCacheHeaders();


            // Configure Identity
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            // Configure Cloudinary
            services.Configure<ImageUploadDTO>(Configuration.GetSection("CloudSettings"));
            services.AddCloudinary(CloudinaryServiceExtension.GetAccount(Configuration));

            //This policy will determine who can use the API
            services.ConfigureCors();

            // Configure AutoMapper
            services.ConfigureAutoMappers();

            // Register Fluent Validation Services
            services.AddDependencyInjection();

            services.AddControllers(config =>
            {
                /*config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 120
                });*/
            }).AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<AppUser> userManager, ApplicationDbContext context)
        {


            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }



            app.UseHttpsRedirection();


            var seed = new DbInitializer(env);
            seed.SeedData(context, userManager).Wait();

            app.UseHttpCacheHeaders();

            app.ConfigureCustomExceptionHandler();

            app.UseCors("AllowAll");

            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static string GetHerokuConnectionString()
        {
            // Get the Database URL from the ENV variables in Heroku
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            // parse the connection string
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};" +
                   $"Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        public static void AddDbContextAndConfigurations(IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsProduction())
            {
                //configure postgres for production environment
                services.AddDbContextPool<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(GetHerokuConnectionString(), x => x.MigrationsAssembly("ProductionMigrations"));
                    services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
                });
                
            }
            else
            {
                //Configure sqlite if the environment is development
                string SqliteConnectionString = config.GetConnectionString("DefaultConnString");
                services.AddDbContextPool<ApplicationDbContext>(options =>
                {
                    options.UseSqlite(SqliteConnectionString, x => x.MigrationsAssembly("DevelopmentMigrations"));
                    services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
                });
            }
        }

    }
}
