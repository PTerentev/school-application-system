using ApplicationSystem.DataAccess;
using ApplicationSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationSystem.Web.Infrastructure.ServiceExtensions
{
    /// <summary>
    /// Domain service extensions.
    /// </summary>
    internal static class DomainServiceExtension
    {
        /// <summary>
        /// Add domain services.
        /// </summary>
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var passwordSettings = new PasswordOptions();
            configuration.Bind("PasswordSettings", passwordSettings);

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password = passwordSettings;
            });

            return services;
        }
    }
}
