using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using ApplicationSystem.DataAccess;
using ApplicationSystem.DataAccess.Extensions;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using ApplicationSystem.UseCases.Authority.Common;
using ApplicationSystem.Domain.Entities;

namespace ApplicationSystem.UseCases.Authority.Behaviors
{
    /// <summary>
    /// Authority permission behavior.
    /// </summary>
    public class AccessAuthorityPermissionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IAccessUserPrincipalService accessUserPrincipalService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationDbContext">Application database context.</param>
        /// <param name="accessUserPrincipalService">Access user principal service.</param>
        /// <param name="userManager">User manager.</param>
        public AccessAuthorityPermissionBehavior(
            ApplicationDbContext applicationDbContext,
            IAccessUserPrincipalService accessUserPrincipalService,
            UserManager<Domain.Entities.User> userManager)
        {
            this.accessUserPrincipalService = accessUserPrincipalService;
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userId = accessUserPrincipalService.GetUserId();

            if (request is AuthorityRequestBase authorityRequest)
            {
                if (!await userManager.HasAnyRole(userId.GetValueOrDefault(), UserRoles.Admin))
                {
                    var authority = await applicationDbContext.Authorities.Where(a => a.Id == authorityRequest.AuthorityId).SingleAsync();
                    if (!authority.Users.Any(u => u.Id == userId))
                    {
                        throw new ForbiddenException("The user does not have access to the authority.");
                    }
                }
            }

            return await next();
        }
    }
}
