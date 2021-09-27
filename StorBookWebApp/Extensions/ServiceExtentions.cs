using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StorBookWebApp.Middleware;
using StorBookWebApp.Shared.AutoMapSetup;

namespace StorBookWebApp.Extensions
{
    public static class ServiceExtentions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapProfileSetup));
        }
    }
}
