using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StorBookWebApp.Data;
using StorBookWebApp.Models;

namespace StorBookWebApp.Extensions
{
    public static class IdentityServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            //builder.AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();
        }
    }
}
