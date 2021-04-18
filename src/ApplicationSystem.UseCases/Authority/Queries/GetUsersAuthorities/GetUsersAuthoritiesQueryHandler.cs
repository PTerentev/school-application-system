using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.Infrastructure.Abstractions.Authorization;
using ApplicationSystem.UseCases.Authority.Dtos;

namespace ApplicationSystem.UseCases.Authority.Queries.GetUsersAuthorities
{
    /// <summary>
    /// Get authorities query handler.
    /// </summary>
    internal class GetUsersAuthoritiesQueryHandler : IRequestHandler<GetUsersAuthoritiesQuery, IEnumerable<AuthorityDto>>
    {
        private readonly IAccessUserPrincipalService accessUserPrincipalService;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="accessUserPrincipalService">Access user principal service.</param>
        /// <param name="applicationDbContext">Application database context.</param>
        /// <param name="mapper">Mapper.</param>
        public GetUsersAuthoritiesQueryHandler(
            IAccessUserPrincipalService accessUserPrincipalService,
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this.accessUserPrincipalService = accessUserPrincipalService;
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuthorityDto>> Handle(GetUsersAuthoritiesQuery request, CancellationToken cancellationToken)
        {
            var userId = accessUserPrincipalService.GetUserId();

            var query = applicationDbContext.Authorities.Where(a => a.Users.Any(u => u.Id == userId));
            var authorities = await mapper.ProjectTo<AuthorityDto>(query).ToListAsync(cancellationToken);

            return authorities;
        }
    }
}
