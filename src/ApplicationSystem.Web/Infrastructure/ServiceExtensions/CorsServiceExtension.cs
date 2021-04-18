using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationSystem.Web.Infrastructure.ServiceExtensions
{
    /// <summary>
    /// CORS policy extension.
    /// </summary>
    internal static class CorsServiceExtension
    {
        /// <summary>
        /// Add CORS policies.
        /// </summary>
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicyNames.DevCorsPolicyName,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });

                options.AddPolicy(name: CorsPolicyNames.ProductionPolicyName,
                  builder =>
                  {
                      builder.WithOrigins(configuration["AppSettings:FrontendOrigin"]);
                      builder.AllowAnyOrigin();
                      builder.AllowAnyHeader();
                      builder.AllowAnyMethod();
                  });
            });

            return services;
        }
    }
}
