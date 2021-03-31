using Microsoft.Extensions.DependencyInjection;
using ApplicationSystem.Infrastructure.Abstractions.Attachments;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using ApplicationSystem.Infrastructure.Abstractions.Emails;
using ApplicationSystem.Infrastructure.Attachments;
using ApplicationSystem.Infrastructure.Authorization;
using ApplicationSystem.Infrastructure.Emails;
using ApplicationSystem.Infrastructure.UseCases;
using MediatR;

namespace ApplicationSystem.Web.Infrastructure.ServiceExtensions
{
    /// <summary>
    /// Infrastructure service extension.
    /// </summary>
    internal static class InfrastructureServiceExtension
    {
        /// <summary>
        /// Add infrastructure services.
        /// </summary>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ISmptService, FakeSmptService>();
            services.AddScoped<IEmailRendererService, EmailRendererService>();
            services.AddScoped<IAccessUserPrincipalService, AccessUserPrincipalService>();
            services.AddScoped<IAccessTokenGenerationService, AccessTokenGenerationService>();
            services.AddScoped<IAttachmentService, FolderAttachmentService>();
            services.AddMediatR(typeof(AutoMapperProfile).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
