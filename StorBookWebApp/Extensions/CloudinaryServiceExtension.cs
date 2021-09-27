using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StorBookWebApp.Extensions
{
    public static class CloudinaryServiceExtension
    {
        public static IServiceCollection AddCloudinary(this IServiceCollection services,
            Account account, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            services.Add(new ServiceDescriptor(typeof(Cloudinary), c => new Cloudinary(account), lifetime));
            return services;
        }

        public static Account GetAccount(IConfiguration Configuration)
        {
            var cloudSettings = Configuration.GetSection("CloudSettings");
            Account account = new(
                                cloudSettings.GetSection("CloudName").Value,
                                cloudSettings.GetSection("ApiKey").Value,
                                cloudSettings.GetSection("ApiSecret").Value);
            return account;
        }
    }
}
