using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationSystem.UseCases.Authority.Behaviors
{
    /// <summary>
    /// Authority permission behavior.
    /// </summary>
    public class AuthorityPermissionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IAccessUserPrincipalService accessUserPrincipalService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationDbContext">Application database context.</param>
        /// <param name="accessUserPrincipalService">Access user principal service.</param>
        public AuthorityPermissionBehavior(ApplicationDbContext applicationDbContext, IAccessUserPrincipalService accessUserPrincipalService)
        {
            this.accessUserPrincipalService = accessUserPrincipalService;
            this.applicationDbContext = applicationDbContext;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userId = accessUserPrincipalService.GetUserId();

            var authority = applicationDbContext.Authorities.Where(a => a.Id == request.AuthorityId).Single();
            if (!authority.Users.Any(u => u.Id == userId))
            {
                return new ValidationResult("The user does not have access to the authority.");
            }

            return ValidationResult.Success;
        }
    }
}
