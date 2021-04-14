using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ApplicationSystem.UseCases.Authority.Behaviors;

namespace ApplicationSystem.UseCases.Extensions
{
    /// <summary>
    /// Use cases service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add Use Cases services.
        /// </summary>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AccessAuthorityPermissionBehavior<,>));

            return services;
        }
    }
}
