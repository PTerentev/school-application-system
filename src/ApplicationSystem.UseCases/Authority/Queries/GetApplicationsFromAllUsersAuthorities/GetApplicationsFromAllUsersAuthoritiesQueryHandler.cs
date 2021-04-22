using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MediatR;
using AutoMapper;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Common.Dtos;
using ApplicationSystem.Infrastructure.Common.Application;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using ApplicationSystem.DataAccess.Extensions;

namespace ApplicationSystem.UseCases.Authority.Queries.GetApplicationsFromAllUsersAuthorities
{
    /// <summary>
    /// Get applications from all users authorities query handler.
    /// </summary>
    internal class GetApplicationsFromAllUsersAuthoritiesQueryHandler : IRequestHandler<GetApplicationsFromAllUsersAuthoritiesQuery, IEnumerable<ApplicationDto>>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IAccessUserPrincipalService accessUserPrincipalService;
        private readonly UserManager<Domain.Entities.User> userManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GetApplicationsFromAllUsersAuthoritiesQueryHandler(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IAccessUserPrincipalService accessUserPrincipalService,
            UserManager<Domain.Entities.User> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.accessUserPrincipalService = accessUserPrincipalService;
            this.userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ApplicationDto>> Handle(GetApplicationsFromAllUsersAuthoritiesQuery request, CancellationToken cancellationToken)
        {
            var userId = accessUserPrincipalService.GetUserId();

            var query = dbContext.Applications
                .Where(a => a.Status == ApplicationStatus.Sent);

            if (!await userManager.HasAnyRole(userId.GetValueOrDefault(), Domain.Entities.UserRoles.Admin))
            {
                var usersAuthorities = await dbContext.Users.Where(u => u.Id == userId).SelectMany(u => u.Authorities).ToListAsync(cancellationToken);
                query = query.Where(a => usersAuthorities.Any(ua => ua.Id == a.AuthorityId));
            }

            return await mapper.ProjectTo<ApplicationDto>(query).ToListAsync(cancellationToken);
        }
    }
}
