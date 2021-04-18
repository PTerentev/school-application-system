using Microsoft.Extensions.DependencyInjection;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Attachments;
using ApplicationSystem.Infrastructure.Authorization;
using ApplicationSystem.Infrastructure.Emails;

namespace ApplicationSystem.Infrastructure.Extensions
{
    /// <summary>
    /// Infrastructure services collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add infrastructure services.
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISmptService, FakeSmptService>();
            services.AddScoped<IEmailRendererService, EmailRendererService>();
            services.AddScoped<IAccessUserPrincipalService, AccessUserPrincipalService>();
            services.AddScoped<IAccessTokenGenerationService, AccessTokenGenerationService>();
            services.AddScoped<IAttachmentService, FolderAttachmentService>();

            return services;
        }
    }
}
