using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApplicationSystem.DataAccess;
using ApplicationSystem.UseCases.Authority.Dtos;

namespace ApplicationSystem.UseCases.Authority.Queries.GetAllAuthoritiesQuery
{
    /// <summary>
    /// Get authorities query handler.
    /// </summary>
    internal class GetAllAuthoritiesQueryHandler : IRequestHandler<GetAllAuthoritiesQuery, IEnumerable<AuthorityDto>>
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationDbContext">Application database context.</param>
        /// <param name="mapper">Mapper.</param>
        public GetAllAuthoritiesQueryHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AuthorityDto>> Handle(GetAllAuthoritiesQuery request, CancellationToken cancellationToken)
        {
            return await mapper.ProjectTo<AuthorityDto>(applicationDbContext.Authorities).ToListAsync(cancellationToken);
        }
    }
}
