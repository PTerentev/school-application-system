using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplicationSystem.Infrastructure.Common.Options;

namespace ApplicationSystem.Web.Infrastructure.ServiceExtensions
{
    /// <summary>
    /// Configuration service extensions.
    /// </summary>
    internal static class ConfigurationServiceExtension
    {
        /// <summary>
        /// Add configuration options.
        /// </summary>
        public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Name));
            services.Configure<AdminAccountOptions>(configuration.GetSection(AdminAccountOptions.Name));
            services.Configure<AttachmentValidationOptions>(configuration.GetSection(AttachmentValidationOptions.Name));

            return services;
        }
    }
}
